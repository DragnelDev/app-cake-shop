using ClnPasteleria;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>Ventana principal: menú lateral + panel con indicadores del día.</summary>
    public partial class FrmPrincipal : FormBase
    {
        private readonly Label[] valores = new Label[6];

        /// <summary>true si el usuario eligió "Cerrar sesión" (vuelve al login) en lugar de salir.</summary>
        public bool SesionCerrada { get; private set; }

        public FrmPrincipal()
        {
            InitializeComponent();
            construirMenu();
            construirTarjetas();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            lblSaludo.Text = "Hola, " + Sesion.NombreEmpleado;
            lblSaludo.ForeColor = Estilo.Cacao;
            lblFecha.ForeColor = Estilo.Gris;
            lblFecha.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(DateTime.Now.ToString("dddd, d 'de' MMMM 'de' yyyy"))
                            + "   ·   Rol: " + Sesion.Rol;
            lblProximas.ForeColor = Estilo.Cacao;
            actualizarIndicadores();
        }

        // ------------------------------------------------------------------ menú lateral
        private void agregarOpcion(string texto, Action accion, bool separador = false)
        {
            var b = new Button
            {
                Text = "   " + texto,
                Size = new Size(240, 44),
                Margin = new Padding(0, separador ? 14 : 0, 0, 0),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Estilo.Cacao,
                Font = Estilo.Fuente(10.5F),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand,
                Tag = "sinEstilo"
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Estilo.Rosa;
            b.FlatAppearance.MouseDownBackColor = Estilo.RosaOscuro;
            b.Click += (s, e) => accion();
            pnlOpciones.Controls.Add(b);
        }

        private void abrir(Func<Form> crear)
        {
            try
            {
                using (var f = crear()) f.ShowDialog(this);
            }
            catch (Exception ex) { Util.Error(ex); }
            actualizarIndicadores();
        }

        private void construirMenu()
        {
            pnlOpciones.BackColor = Estilo.Cacao;
            pnlMarca.BackColor = Estilo.Cacao;
            lblMarca.BackColor = Estilo.Cacao;

            agregarOpcion("Inicio", actualizarIndicadores);
            agregarOpcion("Nuevo pedido", () => abrir(() => new FrmRegistrarPedido()), true);
            agregarOpcion("Gestión de pedidos", () => abrir(() => new FrmGestionPedidos()));
            agregarOpcion("Clientes", () => abrir(() => new FrmCliente()), true);
            agregarOpcion("Productos", () => abrir(() => new FrmProducto()));
            agregarOpcion("Reporte de ventas", () => abrir(() => new FrmReporteVentas()), true);
            if (Sesion.EsAdministrador)
            {
                agregarOpcion("Empleados", () => abrir(() => new FrmEmpleado()), true);
                agregarOpcion("Usuarios", () => abrir(() => new FrmUsuario()));
            }
            agregarOpcion("Cambiar contraseña", () => abrir(() => new FrmCambiarClave()), true);
            agregarOpcion("Acerca de", () => abrir(() => new FrmAcercaDe()));
            agregarOpcion("Soporte", () => abrir(() => new FrmSoporte()));
            agregarOpcion("Cerrar sesión", cerrarSesion, true);
        }

        private void cerrarSesion()
        {
            if (!Util.Confirmar("¿Cerrar la sesión de " + Sesion.NombreUsuario + "?")) return;
            SesionCerrada = true;
            Close();
        }

        // ------------------------------------------------------------------ indicadores
        private void construirTarjetas()
        {
            pnlTarjetas.BackColor = Estilo.Crema;
            string[] titulos = { "Pedidos pendientes", "Entregas de hoy", "Entregas atrasadas", "Ventas de hoy (Bs)", "Ventas del mes (Bs)", "Clientes / Productos" };
            Color[] acentos = { Estilo.Rosa, Estilo.Exito, Estilo.Peligro, Estilo.Cacao, Estilo.Cacao, Estilo.Gris };
            for (int i = 0; i < titulos.Length; i++)
            {
                var tarjeta = new Panel { Size = new Size(230, 100), Margin = new Padding(0, 0, 16, 16), BackColor = Color.White, Tag = "sinEstilo" };
                var franja = new Panel { Dock = DockStyle.Left, Width = 6, BackColor = acentos[i] };
                var titulo = new Label { Text = titulos[i], AutoSize = false, Location = new Point(18, 12), Size = new Size(200, 22), ForeColor = Estilo.Gris, Font = Estilo.Fuente(10F) };
                valores[i] = new Label { Text = "–", AutoSize = false, Location = new Point(18, 40), Size = new Size(200, 46), ForeColor = acentos[i], Font = Estilo.Fuente(22F, FontStyle.Bold) };
                tarjeta.Controls.Add(valores[i]);
                tarjeta.Controls.Add(titulo);
                tarjeta.Controls.Add(franja);
                pnlTarjetas.Controls.Add(tarjeta);
            }
        }

        private void actualizarIndicadores()
        {
            try
            {
                var ind = IndicadoresCln.obtener();
                valores[0].Text = ind.PedidosPendientes.ToString();
                valores[1].Text = ind.EntregasHoy.ToString();
                valores[2].Text = ind.EntregasAtrasadas.ToString();
                valores[3].Text = ind.VentasHoy.ToString("N2");
                valores[4].Text = ind.VentasMes.ToString("N2");
                valores[5].Text = ind.ClientesActivos + " / " + ind.ProductosActivos;

                dgvProximas.DataSource = ind.ProximasEntregas;
                foreach (var c in new[] { "EstadoCodigo", "Estado", "FechaPedido", "Direccion", "RegistradoPor" })
                    if (dgvProximas.Columns.Contains(c)) dgvProximas.Columns[c].Visible = false;
                dgvProximas.Columns["Id"].HeaderText = "Folio";
                dgvProximas.Columns["FechaEntrega"].HeaderText = "Entrega";
                dgvProximas.Columns["TipoEntrega"].HeaderText = "Tipo Entrega";
                dgvProximas.Columns["Total"].HeaderText = "Total (Bs)";
            }
            catch (Exception ex)
            {
                foreach (var v in valores) v.Text = "–";
                Util.Error(ex);
            }
        }
    }
}
