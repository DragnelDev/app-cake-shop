using System;
using System.Collections.Generic;

namespace ClnPasteleria
{
    /// <summary>Fila de la grilla de gestión de pedidos.</summary>
    public class PedidoGestion
    {
        public int Id { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Cliente { get; set; }
        public string Celular { get; set; }
        public string Direccion { get; set; }
        public string TipoEntrega { get; set; }
        public decimal Total { get; set; }
        public short EstadoCodigo { get; set; }
        public string Estado { get { return EstadoPedido.Texto(EstadoCodigo); } }
        public string RegistradoPor { get; set; }
    }

    /// <summary>Línea de detalle de un pedido.</summary>
    public class PedidoLinea
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string MetodoPago { get; set; }
        public decimal SubTotal { get; set; }
    }

    /// <summary>Fila de la grilla de gestión de usuarios.</summary>
    public class UsuarioResumen
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public string Usuario { get; set; }
        public string Empleado { get; set; }
        public string Rol { get; set; }
        public short EstadoCodigo { get; set; }
        public string Estado { get { return EstadoCodigo == EstadoRegistro.Activo ? "ACTIVO" : "INACTIVO"; } }
        public DateTime FechaRegistro { get; set; }
    }

    /// <summary>Indicadores del panel principal.</summary>
    public class Indicadores
    {
        public int PedidosPendientes { get; set; }
        public int EntregasHoy { get; set; }
        public int EntregasAtrasadas { get; set; }
        public decimal VentasHoy { get; set; }
        public decimal VentasMes { get; set; }
        public int ClientesActivos { get; set; }
        public int ProductosActivos { get; set; }
        public List<PedidoGestion> ProximasEntregas { get; set; }
    }
}
