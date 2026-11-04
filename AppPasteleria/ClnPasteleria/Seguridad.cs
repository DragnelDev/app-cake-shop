using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ClnPasteleria
{
    /// <summary>
    /// Manejo de contraseñas.
    /// Formato actual:  PBKDF2$iteraciones$sal(base64)$hash(base64)   (HMAC-SHA256, sal aleatoria por usuario).
    /// Formato antiguo (AES con llave fija, determinista): sólo se acepta para migrar: al iniciar sesión
    /// correctamente se vuelve a guardar con el formato actual (ver UsuarioCln.validar).
    /// </summary>
    public static class Seguridad
    {
        private const string Prefijo = "PBKDF2";
        private const int Iteraciones = 100000;
        private const int TamanoSal = 16;
        private const int TamanoHash = 32;
        public const int LongitudMinimaClave = 6;

        public static string GenerarHash(string clave)
        {
            if (clave == null) throw new ArgumentNullException("clave");
            var sal = new byte[TamanoSal];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(sal);
            var hash = Derivar(clave, sal, Iteraciones);
            return string.Join("$", Prefijo, Iteraciones, Convert.ToBase64String(sal), Convert.ToBase64String(hash));
        }

        /// <summary>Indica si el valor almacenado tiene el formato actual (PBKDF2).</summary>
        public static bool EsFormatoActual(string almacenado)
        {
            return !string.IsNullOrEmpty(almacenado) && almacenado.StartsWith(Prefijo + "$", StringComparison.Ordinal);
        }

        /// <summary>Verifica la clave contra un hash en formato actual o, si no lo es, contra el formato antiguo.</summary>
        public static bool Verificar(string clave, string almacenado)
        {
            if (string.IsNullOrEmpty(clave) || string.IsNullOrEmpty(almacenado)) return false;
            return EsFormatoActual(almacenado) ? VerificarActual(clave, almacenado) : VerificarAntiguo(clave, almacenado);
        }

        private static bool VerificarActual(string clave, string almacenado)
        {
            try
            {
                var partes = almacenado.Split('$');
                if (partes.Length != 4) return false;
                int iteraciones = int.Parse(partes[1]);
                var sal = Convert.FromBase64String(partes[2]);
                var esperado = Convert.FromBase64String(partes[3]);
                var calculado = Derivar(clave, sal, iteraciones, esperado.Length);
                return ComparacionConstante(esperado, calculado);
            }
            catch (FormatException) { return false; }
            catch (OverflowException) { return false; }
        }

        private static byte[] Derivar(string clave, byte[] sal, int iteraciones, int longitud = TamanoHash)
        {
            using (var kdf = new Rfc2898DeriveBytes(clave, sal, iteraciones, HashAlgorithmName.SHA256))
                return kdf.GetBytes(longitud);
        }

        private static bool ComparacionConstante(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int diferencia = 0;
            for (int i = 0; i < a.Length; i++) diferencia |= a[i] ^ b[i];
            return diferencia == 0;
        }

        /// <summary>
        /// Política mínima de contraseñas. Devuelve el mensaje de error o null si es válida.
        /// </summary>
        public static string ValidarPolitica(string clave)
        {
            if (string.IsNullOrEmpty(clave) || clave.Length < LongitudMinimaClave)
                return "La contraseña debe tener al menos " + LongitudMinimaClave + " caracteres.";
            if (!clave.Any(char.IsLetter) || !clave.Any(char.IsDigit))
                return "La contraseña debe combinar letras y números.";
            return null;
        }

        /// <summary>Contraseña temporal aleatoria (sin caracteres ambiguos como 0/O o 1/l).</summary>
        public static string GenerarClaveTemporal(int longitud = 8)
        {
            const string letras = "abcdefghjkmnpqrstuvwxyz";
            const string numeros = "23456789";
            var todos = letras + numeros;
            var bytes = new byte[longitud];
            using (var rng = RandomNumberGenerator.Create()) rng.GetBytes(bytes);
            var sb = new StringBuilder(longitud);
            // Garantiza al menos una letra y un número (cumple ValidarPolitica).
            sb.Append(letras[bytes[0] % letras.Length]);
            sb.Append(numeros[bytes[1] % numeros.Length]);
            for (int i = 2; i < longitud; i++) sb.Append(todos[bytes[i] % todos.Length]);
            return sb.ToString();
        }

        // ---------------------------------------------------------------------------------------
        //  Compatibilidad con el esquema ANTERIOR (AES determinista). NO usar para claves nuevas.
        //  La llave está en el código fuente: por eso este esquema se considera inseguro y se migra.
        // ---------------------------------------------------------------------------------------
        private static bool VerificarAntiguo(string clave, string almacenado)
        {
            try
            {
                // La comparación ignora mayúsculas porque el script SQL original guardaba el valor en
                // mayúsculas y SQL Server lo comparaba con una collation insensible a mayúsculas.
                return string.Equals(CifrarAntiguo(clave), almacenado, StringComparison.OrdinalIgnoreCase);
            }
            catch (CryptographicException) { return false; }
        }

        private static string CifrarAntiguo(string texto)
        {
            const string llave = "SIS457-1nf0!";
            var salt = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
            var bytes = Encoding.Unicode.GetBytes(texto);
            using (var aes = Aes.Create())
            using (var pdb = new Rfc2898DeriveBytes(llave, salt))
            {
                aes.Key = pdb.GetBytes(32);
                aes.IV = pdb.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        cs.Write(bytes, 0, bytes.Length);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
