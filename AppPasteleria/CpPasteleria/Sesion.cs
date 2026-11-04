using CadPasteleria;
using ClnPasteleria;

namespace CpPasteleria
{
    /// <summary>Usuario autenticado de la sesión actual.</summary>
    internal static class Sesion
    {
        public static Usuario Actual { get; private set; }

        public static bool Activa { get { return Actual != null; } }
        public static int IdUsuario { get { return Actual.id; } }
        public static int IdEmpleado { get { return Actual.idEmpleado; } }
        public static string NombreUsuario { get { return Actual.usuario1; } }
        public static string Rol { get { return Actual.role; } }
        public static bool EsAdministrador { get { return Activa && Roles.EsAdministrador(Actual.role); } }

        public static string NombreEmpleado
        {
            get
            {
                var e = Actual == null ? null : Actual.Empleado;
                return e == null ? NombreUsuario : (e.nombre + " " + (e.apellidoPaterno ?? "")).Trim();
            }
        }

        public static void Iniciar(Usuario usuario) { Actual = usuario; }
        public static void Cerrar() { Actual = null; }
    }
}
