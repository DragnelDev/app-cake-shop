using CadPasteleria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ClnPasteleria
{
    public static class PedidoCln
    {
        /// <summary>Proyección compartida Pedido -> PedidoGestion (se traduce a SQL).</summary>
        internal static readonly Expression<Func<Pedido, PedidoGestion>> Proyeccion = p => new PedidoGestion
        {
            Id = p.id,
            FechaPedido = p.fechaPedido,
            FechaEntrega = p.fechaEntrega,
            Cliente = p.Cliente.nombre + " " + p.Cliente.apellido,
            Celular = p.Cliente.celular,
            Direccion = p.Cliente.direccion,
            TipoEntrega = p.tipoEntrega,
            Total = p.DetallePedido.Sum(d => d.subTotal) ?? 0m,
            EstadoCodigo = p.estadoPedido,
            RegistradoPor = p.usuarioRegistro
        };

        /// <summary>
        /// Registra el pedido con su detalle en UNA sola transacción (antes: dos SaveChanges; si fallaba el
        /// segundo quedaba un pedido vacío). Guarda además el usuario (idUsuario), que antes se omitía y
        /// hacía que los pedidos NO aparecieran en el reporte de ventas.
        /// Los precios se toman de la base de datos, no de lo que informe la interfaz.
        /// </summary>
        public static int registrar(Pedido pedido, IList<DetallePedido> detalles, int idUsuario, string usuarioRegistro)
        {
            if (pedido == null) throw new ArgumentNullException("pedido");
            if (pedido.idCliente == null || pedido.idCliente <= 0)
                throw new ReglaNegocioException("Seleccione un cliente.");
            if (string.IsNullOrWhiteSpace(pedido.tipoEntrega))
                throw new ReglaNegocioException("Seleccione el tipo de entrega.");
            if (pedido.estadoPedido != EstadoPedido.Pendiente && pedido.estadoPedido != EstadoPedido.Entregado)
                throw new ReglaNegocioException("Estado de pedido no válido.");
            if (detalles == null || detalles.Count == 0)
                throw new ReglaNegocioException("Debe agregar al menos un producto al pedido.");

            var ahora = DateTime.Now;
            if (pedido.fechaEntrega.Date < ahora.Date)
                throw new ReglaNegocioException("La fecha de entrega no puede ser anterior a hoy.");

            using (var context = new LabPasteleriaEntities())
            {
                int idCliente = pedido.idCliente.Value;
                if (!context.Cliente.Any(c => c.id == idCliente && c.estado == EstadoRegistro.Activo))
                    throw new ReglaNegocioException("El cliente seleccionado ya no está disponible.");
                if (!context.Usuario.Any(u => u.id == idUsuario && u.estado == EstadoRegistro.Activo))
                    throw new ReglaNegocioException("El usuario de la sesión no está activo.");

                var ids = detalles.Select(d => d.idProducto).Distinct().ToList();
                var productos = context.Producto
                    .Where(p => ids.Contains(p.id) && p.estado == EstadoRegistro.Activo)
                    .ToDictionary(p => p.id);

                pedido.fechaPedido = ahora;
                pedido.fechaRegistro = ahora;
                pedido.usuarioRegistro = usuarioRegistro;
                pedido.idUsuario = idUsuario;
                pedido.estado = EstadoRegistro.Activo;

                foreach (var d in detalles)
                {
                    if (d.cantidad <= 0) throw new ReglaNegocioException("Todas las cantidades deben ser mayores a cero.");
                    Producto producto;
                    if (!productos.TryGetValue(d.idProducto, out producto))
                        throw new ReglaNegocioException("Uno de los productos ya no está disponible. Actualice la lista.");

                    d.precioUnitario = producto.precio;
                    d.fechaRegistro = ahora;
                    d.usuarioRegistro = usuarioRegistro;
                    d.estado = EstadoRegistro.Activo;
                    pedido.DetallePedido.Add(d);
                }

                context.Pedido.Add(pedido);
                context.SaveChanges();
                return pedido.id;
            }
        }

        /// <summary>Pedidos pendientes y entregados hoy (procedimiento paListarPedidos).</summary>
        public static List<paListarPedidos_Result> listar()
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.paListarPedidos().ToList();
            }
        }

        /// <summary>
        /// Pedidos para la gestión (por fecha de entrega). estado = null -> todos los estados.
        /// texto: nombre, celular o CI del cliente.
        /// </summary>
        public static List<PedidoGestion> listarGestion(DateTime desde, DateTime hasta, short? estado, string texto)
        {
            var inicio = desde.Date;
            var fin = hasta.Date.AddDays(1);

            using (var context = new LabPasteleriaEntities())
            {
                var q = context.Pedido.Where(p => p.estado == EstadoRegistro.Activo
                                                  && p.fechaEntrega >= inicio && p.fechaEntrega < fin);
                if (estado.HasValue)
                {
                    var e = estado.Value;
                    q = q.Where(p => p.estadoPedido == e);
                }
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    var t = texto.Trim();
                    q = q.Where(p => (p.Cliente.nombre + " " + p.Cliente.apellido).Contains(t)
                                     || p.Cliente.celular.Contains(t)
                                     || p.Cliente.cedulaIdentidad.Contains(t));
                }
                return q.OrderBy(p => p.fechaEntrega).ThenBy(p => p.id).Select(Proyeccion).ToList();
            }
        }

        public static List<PedidoLinea> listarDetalle(int idPedido)
        {
            using (var context = new LabPasteleriaEntities())
            {
                return context.DetallePedido
                    .Where(d => d.idPedido == idPedido && d.estado != EstadoRegistro.Eliminado)
                    .OrderBy(d => d.id)
                    .Select(d => new PedidoLinea
                    {
                        Producto = d.Producto.nombre,
                        Cantidad = d.cantidad,
                        PrecioUnitario = d.precioUnitario,
                        MetodoPago = d.metodoPago,
                        SubTotal = d.cantidad * d.precioUnitario
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Marca un pedido PENDIENTE como ENTREGADO (fecha de entrega = ahora) o ANULADO.
        /// Un pedido entregado o anulado ya no puede modificarse (registro histórico).
        /// </summary>
        public static void cambiarEstado(int idPedido, short nuevoEstado)
        {
            if (nuevoEstado != EstadoPedido.Entregado && nuevoEstado != EstadoPedido.Anulado)
                throw new ReglaNegocioException("Estado de destino no válido.");

            using (var context = new LabPasteleriaEntities())
            {
                var pedido = context.Pedido.FirstOrDefault(p => p.id == idPedido && p.estado == EstadoRegistro.Activo);
                if (pedido == null) throw new ReglaNegocioException("El pedido no existe.");
                if (pedido.estadoPedido != EstadoPedido.Pendiente)
                    throw new ReglaNegocioException("Sólo se pueden modificar pedidos PENDIENTES (este pedido está "
                                                    + EstadoPedido.Texto(pedido.estadoPedido) + ").");

                pedido.estadoPedido = nuevoEstado;
                if (nuevoEstado == EstadoPedido.Entregado) pedido.fechaEntrega = DateTime.Now;
                context.SaveChanges();
            }
        }

        public static List<paReportePedidos_Result> listarReporte(DateTime fechaInicio, DateTime fechaFin, string nombreCliente)
        {
            if (fechaInicio.Date > fechaFin.Date)
                throw new ReglaNegocioException("La fecha 'Desde' no puede ser posterior a la fecha 'Hasta'.");

            string filtro = string.IsNullOrWhiteSpace(nombreCliente) ? null : nombreCliente.Trim();
            using (var context = new LabPasteleriaEntities())
            {
                return context.paReportePedidos(fechaInicio.Date, fechaFin.Date, filtro).ToList();
            }
        }
    }
}
