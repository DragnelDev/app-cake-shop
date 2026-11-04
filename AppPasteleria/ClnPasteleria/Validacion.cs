using System;
using System.Linq;
using System.Net.Mail;

namespace ClnPasteleria
{
    /// <summary>Validaciones de formato compartidas entre la capa de negocio y la presentación.</summary>
    public static class Validacion
    {
        /// <summary>Texto recortado o null si queda vacío.</summary>
        public static string TextoONulo(string valor)
        {
            if (valor == null) return null;
            valor = valor.Trim();
            return valor.Length == 0 ? null : valor;
        }

        public static bool SoloDigitos(string valor)
        {
            return !string.IsNullOrEmpty(valor) && valor.All(c => c >= '0' && c <= '9');
        }

        /// <summary>Teléfono: solo dígitos, 7 u 8 (fijo o celular).</summary>
        public static bool EsTelefonoValido(string valor)
        {
            return SoloDigitos(valor) && (valor.Length == 7 || valor.Length == 8);
        }

        public static bool EsEmailValido(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) return false;
            try
            {
                var m = new MailAddress(valor.Trim());
                return m.Address == valor.Trim() && valor.Contains(".");
            }
            catch (FormatException) { return false; }
        }

        /// <summary>Nombre de usuario: 5 a 15 caracteres (letras, números, punto o guion bajo).</summary>
        public static string ValidarNombreUsuario(string usuario)
        {
            usuario = (usuario ?? "").Trim();
            if (usuario.Length < 5) return "El usuario debe tener al menos 5 caracteres.";
            if (usuario.Length > 15) return "El usuario no puede superar los 15 caracteres.";
            if (!usuario.All(c => char.IsLetterOrDigit(c) || c == '.' || c == '_'))
                return "El usuario sólo admite letras, números, punto y guion bajo.";
            return null;
        }

        public static bool EsMayorDeEdad(DateTime fechaNacimiento)
        {
            return fechaNacimiento.Date <= DateTime.Today.AddYears(-18);
        }
    }
}
