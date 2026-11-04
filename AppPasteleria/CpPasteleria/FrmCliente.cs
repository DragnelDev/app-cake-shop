using CadPasteleria;
using ClnPasteleria;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CpPasteleria
{
    public partial class FrmCliente : FormBase
    {
        private bool esNuevo;
        private int idEditando;

        /// <summary>Id del último cliente registrado en esta ventana (lo usa el registro de pedidos).</summary>
        public int? UltimoClienteId { get; private set; }

        public FrmCliente()
        {
            InitializeComponent();
            txtCelular.KeyPress += Util.onlyNumbers;
            txtCelular.MaxLength = 8;
            txtCedulaIdentidad.MaxLength = 12;
        }

        private int? idSeleccionado()
        {
            return dgvLista.CurrentRow == null ? (int?)null : (int)dgvLista.CurrentRow.Cells["id"].Value;
        }

        private void configurarPermisos()
        {
            bool hay = dgvLista.Rows.Count > 0;
            btnEditar.Enabled = hay;
            btnEliminar.Enabled = Sesion.EsAdministrador && hay;
        }

        private void listar()
        {
            try
            {
                var lista = ClienteCln.listarPa(txtBuscar.Text.Trim());
                dgvLista.DataSource = lista;
                dgvLista.Columns["id"].Visible = false;
                dgvLista.Columns["estado"].Visible = false;
                dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula Identidad";
                dgvLista.Columns["nombre"].HeaderText = "Nombre";
                dgvLista.Columns["apellido"].HeaderText = "Apellido";
                dgvLista.Columns["celular"].HeaderText = "Celular";
                dgvLista.Columns["email"].HeaderText = "Email";
                dgvLista.Columns["direccion"].HeaderText = "Dirección";
                dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
                dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            }
            catch (Exception ex) { Util.Error(ex); }
            configurarPermisos();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            Size = new Size(702, 302);
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            idEditando = 0;
            limpiarErrores();
            pnlAcciones.Enabled = false;
            Size = new Size(702, 463);
            txtCedulaIdentidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = idSeleccionado();
            if (id == null) return;
            var cliente = ClienteCln.obtenerUno(id.Value);
            if (cliente == null) { Util.Advertencia("El cliente ya no existe."); listar(); return; }

            esNuevo = false;
            idEditando = id.Value;
            limpiarErrores();
            pnlAcciones.Enabled = false;
            Size = new Size(702, 463);
            txtCedulaIdentidad.Text = cliente.cedulaIdentidad;
            txtNombre.Text = cliente.nombre;
            txtApellido.Text = cliente.apellido;
            txtCelular.Text = cliente.celular;
            txtEmail.Text = cliente.email;
            txtDireccion.Text = cliente.direccion;
            txtNombre.Focus();
        }

        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtCelular.Clear();
            txtEmail.Clear();
            txtDireccion.Clear();
        }

        private void limpiarErrores()
        {
            erpCedulaIdentidad.Clear();
            erpNombreCliente.Clear();
            erpApellidoCliente.Clear();
            erpCelular.Clear();
            erpEmail.Clear();
            erpDireccion.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(702, 302);
            pnlAcciones.Enabled = true;
            limpiarErrores();
            limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { listar(); }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { e.Handled = true; listar(); }
        }

        private bool validar()
        {
            limpiarErrores();
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { erpNombreCliente.SetError(txtNombre, "El nombre es obligatorio"); ok = false; }
            if (string.IsNullOrWhiteSpace(txtApellido.Text)) { erpApellidoCliente.SetError(txtApellido, "El apellido es obligatorio"); ok = false; }
            if (!Validacion.EsTelefonoValido(txtCelular.Text.Trim()))
            { erpCelular.SetError(txtCelular, "Ingrese 7 u 8 dígitos (sólo números)"); ok = false; }
            if (string.IsNullOrWhiteSpace(txtDireccion.Text)) { erpDireccion.SetError(txtDireccion, "La dirección es obligatoria"); ok = false; }

            var email = txtEmail.Text.Trim();
            if (email.Length > 0 && !Validacion.EsEmailValido(email))
            { erpEmail.SetError(txtEmail, "El e-mail no tiene un formato válido"); ok = false; }
            else if (email.Length > 0 && ClienteCln.existeEmail(email, idEditando))
            { erpEmail.SetError(txtEmail, "Ya existe un cliente con ese e-mail"); ok = false; }

            var ci = txtCedulaIdentidad.Text.Trim();
            if (ci.Length > 0 && ClienteCln.existeCedula(ci, idEditando))
            { erpCedulaIdentidad.SetError(txtCedulaIdentidad, "Ya existe un cliente con esa cédula"); ok = false; }
            return ok;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;
                var cliente = new Cliente
                {
                    cedulaIdentidad = txtCedulaIdentidad.Text,
                    nombre = txtNombre.Text,
                    apellido = txtApellido.Text,
                    celular = txtCelular.Text,
                    email = txtEmail.Text,
                    direccion = txtDireccion.Text,
                    usuarioRegistro = Sesion.NombreUsuario
                };
                if (esNuevo) UltimoClienteId = ClienteCln.insertar(cliente);
                else { cliente.id = idEditando; ClienteCln.actualizar(cliente); }

                listar();
                btnCancelar.PerformClick();
                Util.Info("Cliente guardado correctamente.");
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) { Util.Advertencia("Sólo un administrador puede dar de baja clientes."); return; }
            var id = idSeleccionado();
            if (id == null) return;
            var nombre = dgvLista.CurrentRow.Cells["nombre"].Value + " " + dgvLista.CurrentRow.Cells["apellido"].Value;
            if (!Util.Confirmar("¿Está seguro de dar de baja al cliente " + nombre + "?")) return;
            try
            {
                ClienteCln.eliminar(id.Value, Sesion.NombreUsuario);
                listar();
                Util.Info("Cliente dado de baja correctamente.");
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnCerrar_Click(object sender, EventArgs e) { Close(); }
    }
}
