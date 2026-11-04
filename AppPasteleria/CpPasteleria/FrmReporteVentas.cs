using CadPasteleria;
using ClnPasteleria;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CpPasteleria
{
    public partial class FrmReporteVentas : FormBase
    {
        private List<paReportePedidos_Result> datos = new List<paReportePedidos_Result>();

        public FrmReporteVentas()
        {
            InitializeComponent();
            txtTotalPedido.ReadOnly = true;
        }

        private void FrmReporteVentas_Load(object sender, EventArgs e)
        {
            var hoy = DateTime.Today;
            dtpFechaDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
            dtpFechaHasta.Value = hoy;
            listarReporte();
        }

        private void listarReporte()
        {
            try
            {
                datos = PedidoCln.listarReporte(dtpFechaDesde.Value, dtpFechaHasta.Value, txtCliente.Text);
                dgvLista.DataSource = datos;

                foreach (var c in new[] { "IdPedido", "DetalleProductosJSON", "CelularCliente", "DireccionCliente" })
                    dgvLista.Columns[c].Visible = false;
                dgvLista.Columns["FechaPedido"].HeaderText = "Fecha Pedido";
                dgvLista.Columns["FechaEntrega"].HeaderText = "Fecha Entrega";
                dgvLista.Columns["NombreCliente"].HeaderText = "Cliente";
                dgvLista.Columns["NombreEmpleado"].HeaderText = "Vendedor";
                dgvLista.Columns["TipoEntrega"].HeaderText = "Tipo Entrega";
                dgvLista.Columns["EstadoPedido"].HeaderText = "Estado";
                dgvLista.Columns["MontoTotal"].HeaderText = "Total (Bs)";

                txtTotalPedido.Text = datos.Sum(r => r.MontoTotal ?? 0m).ToString("N2") + " Bs";
                btnGenerarReporte.Enabled = datos.Count > 0;
            }
            catch (Exception ex)
            {
                datos = new List<paReportePedidos_Result>();
                dgvLista.DataSource = null;
                txtTotalPedido.Text = "0.00 Bs";
                btnGenerarReporte.Enabled = false;
                Util.Error(ex);
            }
        }

        private static string csv(object valor)
        {
            var t = Convert.ToString(valor, CultureInfo.InvariantCulture) ?? "";
            return (t.Contains(";") || t.Contains("\"") || t.Contains("\n")) ? "\"" + t.Replace("\"", "\"\"") + "\"" : t;
        }

        /// <summary>Exporta la grilla actual a un CSV (separador ";", abre directo en Excel).</summary>
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            if (datos.Count == 0) return;
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Archivo CSV (*.csv)|*.csv";
                dlg.FileName = "ventas_" + dtpFechaDesde.Value.ToString("yyyyMMdd") + "_" + dtpFechaHasta.Value.ToString("yyyyMMdd") + ".csv";
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    var sb = new StringBuilder();
                    sb.AppendLine("Folio;Fecha Pedido;Fecha Entrega;Cliente;Celular;Vendedor;Tipo Entrega;Estado;Total (Bs)");
                    foreach (var r in datos)
                        sb.AppendLine(string.Join(";", new[]
                        {
                            csv(r.IdPedido), csv(r.FechaPedido.ToString("dd/MM/yyyy HH:mm")), csv(r.FechaEntrega.ToString("dd/MM/yyyy HH:mm")),
                            csv(r.NombreCliente), csv(r.CelularCliente), csv(r.NombreEmpleado), csv(r.TipoEntrega),
                            csv(EstadoPedido.Texto(r.EstadoPedido)), csv((r.MontoTotal ?? 0m).ToString("0.00", CultureInfo.InvariantCulture))
                        }));
                    sb.AppendLine(";;;;;;;TOTAL;" + datos.Sum(r => r.MontoTotal ?? 0m).ToString("0.00", CultureInfo.InvariantCulture));
                    File.WriteAllText(dlg.FileName, sb.ToString(), new UTF8Encoding(true));
                    Util.Info("Reporte exportado correctamente.");
                }
                catch (Exception ex) { Util.Error("No se pudo guardar el archivo: " + ex.Message); }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) { Close(); }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; listarReporte(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { listarReporte(); }
    }
}
