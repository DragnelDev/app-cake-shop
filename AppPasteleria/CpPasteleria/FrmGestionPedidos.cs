using ClnPasteleria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>Seguimiento de pedidos: filtra, muestra el detalle y permite marcar como ENTREGADO o ANULAR.</summary>
    public partial class FrmGestionPedidos : FormBase
    {
        public FrmGestionPedidos()
        {
            InitializeComponent();
            cbxEstado.Items.AddRange(new object[] { "TODOS", "PENDIENTE", "ENTREGADO", "ANULADO" });
            cbxEstado.SelectedIndex = 1;
        }

        private PedidoGestion seleccionado()
        {
            return dgvPedidos.CurrentRow == null ? null : dgvPedidos.CurrentRow.DataBoundItem as PedidoGestion;
        }

        private short? estadoFiltro()
        {
            switch (cbxEstado.Text)
            {
                case "PENDIENTE": return EstadoPedido.Pendiente;
                case "ENTREGADO": return EstadoPedido.Entregado;
                case "ANULADO": return EstadoPedido.Anulado;
                default: return null;
            }
        }

        private void FrmGestionPedidos_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today.AddDays(30);
            listar();
        }

        private void listar()
        {
            try
            {
                if (dtpDesde.Value.Date > dtpHasta.Value.Date) { Util.Advertencia("La fecha 'desde' no puede ser posterior a 'hasta'."); return; }
                dgvPedidos.DataSource = PedidoCln.listarGestion(dtpDesde.Value, dtpHasta.Value, estadoFiltro(), txtCliente.Text);

                dgvPedidos.Columns["EstadoCodigo"].Visible = false;
                dgvPedidos.Columns["Id"].HeaderText = "Folio";
                dgvPedidos.Columns["FechaPedido"].HeaderText = "Fecha Pedido";
                dgvPedidos.Columns["FechaEntrega"].HeaderText = "Fecha Entrega";
                dgvPedidos.Columns["TipoEntrega"].HeaderText = "Tipo Entrega";
                dgvPedidos.Columns["Total"].HeaderText = "Total (Bs)";
                dgvPedidos.Columns["RegistradoPor"].HeaderText = "Registrado Por";
            }
            catch (Exception ex) { Util.Error(ex); }
            mostrarDetalle();
        }

        private void mostrarDetalle()
        {
            var p = seleccionado();
            btnEntregar.Enabled = btnAnular.Enabled = p != null && p.EstadoCodigo == EstadoPedido.Pendiente;
            if (p == null)
            {
                dgvDetalle.DataSource = null;
                lblTotal.Text = "Total: 0.00 Bs";
                return;
            }
            try
            {
                var lineas = PedidoCln.listarDetalle(p.Id);
                dgvDetalle.DataSource = lineas;
                dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio Unit. (Bs)";
                dgvDetalle.Columns["MetodoPago"].HeaderText = "Método de Pago";
                dgvDetalle.Columns["SubTotal"].HeaderText = "SubTotal (Bs)";
                lblTotal.Text = "Total: " + lineas.Sum(l => l.SubTotal).ToString("N2") + " Bs";
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void dgvPedidos_SelectionChanged(object sender, EventArgs e) { mostrarDetalle(); }

        private void cambiar(short estado, string pregunta, string ok)
        {
            var p = seleccionado();
            if (p == null || !Util.Confirmar(pregunta.Replace("{folio}", p.Id.ToString()))) return;
            try
            {
                PedidoCln.cambiarEstado(p.Id, estado);
                listar();
                Util.Info(ok);
            }
            catch (Exception ex) { Util.Error(ex); listar(); }
        }

        private void btnEntregar_Click(object sender, EventArgs e)
        {
            cambiar(EstadoPedido.Entregado, "¿Marcar el pedido {folio} como ENTREGADO?", "Pedido marcado como entregado.");
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            cambiar(EstadoPedido.Anulado, "¿Anular el pedido {folio}? Esta acción no se puede deshacer y el pedido dejará de contarse en las ventas.", "Pedido anulado.");
        }

        private void btnBuscar_Click(object sender, EventArgs e) { listar(); }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; listar(); }
        }

        private void btnCerrar_Click(object sender, EventArgs e) { Close(); }
    }
}
