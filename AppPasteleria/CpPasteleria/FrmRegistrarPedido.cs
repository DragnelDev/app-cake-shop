using CadPasteleria;
using ClnPasteleria;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpPasteleria
{
    public partial class FrmRegistrarPedido : FormBase
    {
        private class ClienteItem
        {
            public int Id { get; set; }
            public string Texto { get; set; }
            public string Direccion { get; set; }
            public string Celular { get; set; }
        }

        private class LineaPedido
        {
            [Browsable(false)] public int IdProducto { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            [DisplayName("Precio Unit. (Bs)")] public decimal PrecioUnitario { get; set; }
            [DisplayName("Método de Pago")] public string MetodoPago { get; set; }
            [DisplayName("SubTotal (Bs)")] public decimal SubTotal { get { return Cantidad * PrecioUnitario; } }
        }

        private readonly BindingList<LineaPedido> lineas = new BindingList<LineaPedido>();
        private List<Producto> productos = new List<Producto>();
        private Producto productoSeleccionado;
        private readonly Button btnQuitarItem = new Button();

        public FrmRegistrarPedido()
        {
            InitializeComponent();

            btnQuitarItem.Name = "btnQuitarItem";
            btnQuitarItem.Text = "Quitar ítem";
            btnQuitarItem.Location = new Point(330, 125);
            btnQuitarItem.Size = new Size(120, 40);
            btnQuitarItem.Click += (s, e) => quitarItemSeleccionado();
            gbxDetalle.Controls.Add(btnQuitarItem);

            nudPrecioUnitario.ReadOnly = true;
            nudPrecioUnitario.Enabled = false;
            nudPrecioUnitario.DecimalPlaces = 2;
            nudPrecioUnitario.Maximum = 99999;
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 999;
            txtTotalPedido.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            cbxCliente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxTipoEntrega.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxMetodoPago.DropDownStyle = ComboBoxStyle.DropDownList;
            dgvTemporal.DataSource = lineas;
            dgvTemporal.KeyDown += (s, e) => { if (e.KeyCode == Keys.Delete) quitarItemSeleccionado(); };
            cbxProducto.KeyDown += cbxProducto_KeyDown;
            cbxProducto.Leave += cbxProducto_Leave;
            cbxProducto.SelectedIndexChanged += (s, e) => validarProducto(false);
        }

        private void FrmRegistroPedido_Load(object sender, EventArgs e)
        {
            try
            {
                dateFechaEntrega.MinDate = DateTime.Today;
                dateFechaEntrega.Value = DateTime.Today;
                lblUsuario.Text = Sesion.NombreUsuario;
                productos = ProductoCln.listar();
                configurarComboProductos();
                cbxEstado.SelectedItem = "PENDIENTE";
                cargarPedidos();
                actualizarTotal();
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        // ---------------------------------------------------------------- pedidos recientes
        private void cargarPedidos()
        {
            dgvPedidos.DataSource = PedidoCln.listar();
            Action<string, string> h = (col, txt) => { if (dgvPedidos.Columns.Contains(col)) dgvPedidos.Columns[col].HeaderText = txt; };
            h("id", "Folio"); h("fechaPedido", "Fecha del Pedido"); h("fechaEntrega", "Fecha de Entrega");
            h("ClienteNombre", "Cliente"); h("ClienteTelefono", "Celular/Fijo"); h("ClienteDireccion", "Dirección");
            h("tipoEntrega", "Tipo de Entrega"); h("usuarioRegistro", "Registrado Por"); h("EstadoNegocio", "Estado");
        }

        // ---------------------------------------------------------------- clientes
        private void buscarCliente(int? seleccionarId = null)
        {
            try
            {
                var filtro = txtBuscarCliente.Text.Trim();
                var items = ClienteCln.buscarPa(filtro).Select(c => new ClienteItem
                {
                    Id = c.id,
                    Texto = (c.nombre + " " + c.apellido + (string.IsNullOrEmpty(c.cedulaIdentidad) ? "" : " (" + c.cedulaIdentidad + ")")),
                    Direccion = c.direccion,
                    Celular = c.celular
                }).ToList();

                cbxCliente.DataSource = null;
                cbxCliente.DisplayMember = "Texto";
                cbxCliente.ValueMember = "Id";
                cbxCliente.DataSource = items;

                if (items.Count == 0)
                {
                    txtDireccion.Clear();
                    txtTelefono.Clear();
                    cbxCliente.SelectedIndex = -1;
                    Util.Info("No se encontraron clientes. Puede registrar uno con 'Agregar Nuevo Cliente'.");
                    return;
                }
                var idx = seleccionarId == null ? 0 : Math.Max(0, items.FindIndex(i => i.Id == seleccionarId.Value));
                cbxCliente.SelectedIndex = idx;
                mostrarCliente();
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void mostrarCliente()
        {
            var item = cbxCliente.SelectedItem as ClienteItem;
            txtDireccion.Text = item == null ? "" : item.Direccion;
            txtTelefono.Text = item == null ? "" : item.Celular;
        }

        private void cbxCliente_SelectedIndexChanged(object sender, EventArgs e) { mostrarCliente(); }

        private void btnBuscar_Click(object sender, EventArgs e) { buscarCliente(); }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCliente())
            {
                frm.ShowDialog(this);
                if (frm.UltimoClienteId.HasValue)
                {
                    var c = ClienteCln.obtenerUno(frm.UltimoClienteId.Value);
                    txtBuscarCliente.Text = c == null ? "" : c.celular;
                    buscarCliente(frm.UltimoClienteId);
                }
            }
        }

        // ---------------------------------------------------------------- productos
        private void configurarComboProductos()
        {
            cbxProducto.DropDownStyle = ComboBoxStyle.DropDown;
            cbxProducto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbxProducto.AutoCompleteSource = AutoCompleteSource.ListItems;
            cbxProducto.DataSource = null;
            cbxProducto.DisplayMember = "nombre";
            cbxProducto.ValueMember = "id";
            cbxProducto.DataSource = productos;
            cbxProducto.SelectedIndex = -1;
            cbxProducto.Text = string.Empty;
        }

        private void cbxProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                validarProducto(true);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void cbxProducto_Leave(object sender, EventArgs e) { validarProducto(true); }

        private void validarProducto(bool avisar)
        {
            var texto = cbxProducto.Text.Trim();
            if (texto.Length == 0) { productoSeleccionado = null; nudPrecioUnitario.Value = 0; return; }
            var p = productos.FirstOrDefault(x => string.Equals(x.nombre, texto, StringComparison.OrdinalIgnoreCase));
            productoSeleccionado = p;
            nudPrecioUnitario.Value = p == null ? 0 : Math.Min(p.precio, nudPrecioUnitario.Maximum);
            if (p == null && avisar) Util.Advertencia("Producto no encontrado. Seleccione uno de la lista.");
        }

        // ---------------------------------------------------------------- detalle
        private void bloquearCabecera(bool bloquear)
        {
            cbxCliente.Enabled = !bloquear;
            cbxTipoEntrega.Enabled = !bloquear;
            cbxEstado.Enabled = !bloquear;
            dateFechaEntrega.Enabled = !bloquear;
            cbxMetodoPago.Enabled = !bloquear;
            txtBuscarCliente.Enabled = !bloquear;
            btnBuscar.Enabled = !bloquear;
            btnAgregarCliente.Enabled = !bloquear;
        }

        private void actualizarTotal()
        {
            txtTotalPedido.Text = lineas.Sum(l => l.SubTotal).ToString("N2") + " Bs";
            btnQuitarItem.Enabled = lineas.Count > 0;
            btnFinalizarPedido.Enabled = lineas.Count > 0;
        }

        private void btnAgregarPedido_Click(object sender, EventArgs e)
        {
            if (cbxCliente.SelectedItem == null) { Util.Advertencia("Seleccione un cliente antes de agregar productos."); return; }
            validarProducto(false);
            if (productoSeleccionado == null) { Util.Advertencia("Seleccione un producto válido."); return; }
            if (cbxMetodoPago.SelectedIndex < 0) { Util.Advertencia("Seleccione un método de pago."); return; }
            if (cbxTipoEntrega.SelectedIndex < 0) { Util.Advertencia("Seleccione un tipo de entrega."); return; }
            if (cbxEstado.SelectedIndex < 0) { Util.Advertencia("Seleccione un estado de entrega."); return; }

            int cantidad = (int)nudCantidad.Value;
            var existente = lineas.FirstOrDefault(l => l.IdProducto == productoSeleccionado.id);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
                lineas.ResetBindings();
            }
            else
            {
                lineas.Add(new LineaPedido
                {
                    IdProducto = productoSeleccionado.id,
                    Producto = productoSeleccionado.nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = productoSeleccionado.precio,
                    MetodoPago = cbxMetodoPago.Text
                });
            }
            actualizarTotal();
            bloquearCabecera(true);
            cbxProducto.SelectedIndex = -1;
            cbxProducto.Text = string.Empty;
            productoSeleccionado = null;
            nudPrecioUnitario.Value = 0;
            nudCantidad.Value = 1;
            cbxProducto.Focus();
        }

        private void quitarItemSeleccionado()
        {
            if (dgvTemporal.CurrentRow == null) return;
            var linea = dgvTemporal.CurrentRow.DataBoundItem as LineaPedido;
            if (linea == null) return;
            lineas.Remove(linea);
            actualizarTotal();
            if (lineas.Count == 0) bloquearCabecera(false);
        }

        private void reiniciarFormulario()
        {
            lineas.Clear();
            bloquearCabecera(false);
            cbxCliente.DataSource = null;
            txtBuscarCliente.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            cbxTipoEntrega.SelectedIndex = -1;
            cbxMetodoPago.SelectedIndex = -1;
            cbxEstado.SelectedItem = "PENDIENTE";
            cbxProducto.SelectedIndex = -1;
            cbxProducto.Text = string.Empty;
            productoSeleccionado = null;
            nudCantidad.Value = 1;
            nudPrecioUnitario.Value = 0;
            dateFechaEntrega.Value = DateTime.Today;
            actualizarTotal();
        }

        private void btnCancelarPedido_Click(object sender, EventArgs e)
        {
            if (lineas.Count > 0 && !Util.Confirmar("¿Descartar el pedido en curso?")) return;
            reiniciarFormulario();
        }

        private void btnFinalizarPedido_Click(object sender, EventArgs e)
        {
            if (lineas.Count == 0) { Util.Advertencia("Agregue productos al pedido."); return; }
            var cliente = cbxCliente.SelectedItem as ClienteItem;
            if (cliente == null) { Util.Advertencia("Seleccione un cliente."); return; }

            try
            {
                var entrega = dateFechaEntrega.Value;
                bool entregado = cbxEstado.Text.Trim().ToUpperInvariant() == "ENTREGADO";
                bool hoy = entrega.Date == DateTime.Today;
                if (entregado && !hoy)
                    Util.Info("El pedido se marcó como ENTREGADO pero la fecha de entrega es futura. Se guardará como PENDIENTE.");

                var pedido = new Pedido
                {
                    fechaEntrega = entrega,
                    idCliente = cliente.Id,
                    tipoEntrega = cbxTipoEntrega.Text.Trim(),
                    estadoPedido = (entregado && hoy) ? EstadoPedido.Entregado : EstadoPedido.Pendiente
                };
                var detalles = lineas.Select(l => new DetallePedido
                {
                    idProducto = l.IdProducto,
                    cantidad = l.Cantidad,
                    metodoPago = l.MetodoPago
                }).ToList();

                int folio = PedidoCln.registrar(pedido, detalles, Sesion.IdUsuario, Sesion.NombreUsuario);
                Util.Info("Pedido registrado correctamente. Folio: " + folio);
                reiniciarFormulario();
                cargarPedidos();
            }
            catch (Exception ex) { Util.Error(ex); }
        }
    }
}
