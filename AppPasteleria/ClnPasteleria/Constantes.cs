using System;

namespace ClnPasteleria
{
    /// <summary>Roles válidos del sistema (columna Usuario.role).</summary>
    public static class Roles
    {
        public const string Administrador = "ADMINISTRADOR";
        public const string Cajero = "CAJERO";

        public static readonly string[] Todos = { Administrador, Cajero };

        public static bool EsAdministrador(string role)
        {
            return string.Equals(role, Administrador, StringComparison.OrdinalIgnoreCase);
        }
    }

    /// <summary>Estado de un registro (eliminación lógica). Columna "estado" de todas las tablas.</summary>
    public static class EstadoRegistro
    {
        public const short Eliminado = -1;
        public const short Inactivo = 0;
        public const short Activo = 1;
    }

    /// <summary>Estado del pedido (columna Pedido.estadoPedido).</summary>
    public static class EstadoPedido
    {
        public const short Entregado = 0;
        public const short Pendiente = 1;
        public const short Anulado = 2;

        public static string Texto(short estado)
        {
            switch (estado)
            {
                case Entregado: return "ENTREGADO";
                case Pendiente: return "PENDIENTE";
                case Anulado: return "ANULADO";
                default: return "DESCONOCIDO";
            }
        }
    }

    /// <summary>
    /// Error de regla de negocio (dato duplicado, operación no permitida, etc.).
    /// El mensaje es apto para mostrarse tal cual al usuario final.
    /// </summary>
    public class ReglaNegocioException : Exception
    {
        public ReglaNegocioException(string mensaje) : base(mensaje) { }
    }
}
