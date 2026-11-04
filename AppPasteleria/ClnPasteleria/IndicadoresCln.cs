using CadPasteleria;
using System;
using System.Linq;

namespace ClnPasteleria
{
    /// <summary>Datos del panel principal (inicio).</summary>
    public static class IndicadoresCln
    {
        public static Indicadores obtener()
        {
            var hoy = DateTime.Today;
            var manana = hoy.AddDays(1);
            var inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
            var inicioMesSiguiente = inicioMes.AddMonths(1);

            using (var context = new LabPasteleriaEntities())
            {
                var pedidos = context.Pedido.Where(p => p.estado == EstadoRegistro.Activo);
                var pendientes = pedidos.Where(p => p.estadoPedido == EstadoPedido.Pendiente);
                var vendidos = context.DetallePedido.Where(d => d.Pedido.estado == EstadoRegistro.Activo
                                                                && d.Pedido.estadoPedido != EstadoPedido.Anulado);

                return new Indicadores
                {
                    PedidosPendientes = pendientes.Count(),
                    EntregasHoy = pendientes.Count(p => p.fechaEntrega >= hoy && p.fechaEntrega < manana),
                    EntregasAtrasadas = pendientes.Count(p => p.fechaEntrega < hoy),
                    VentasHoy = vendidos.Where(d => d.Pedido.fechaPedido >= hoy && d.Pedido.fechaPedido < manana)
                                        .Sum(d => d.subTotal) ?? 0m,
                    VentasMes = vendidos.Where(d => d.Pedido.fechaPedido >= inicioMes && d.Pedido.fechaPedido < inicioMesSiguiente)
                                        .Sum(d => d.subTotal) ?? 0m,
                    ClientesActivos = context.Cliente.Count(c => c.estado == EstadoRegistro.Activo),
                    ProductosActivos = context.Producto.Count(p => p.estado == EstadoRegistro.Activo),
                    ProximasEntregas = pendientes.OrderBy(p => p.fechaEntrega).ThenBy(p => p.id)
                                                 .Take(8).Select(PedidoCln.Proyeccion).ToList()
                };
            }
        }
    }
}
