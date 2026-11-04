using CadPasteleria;
using ClnPasteleria;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CpPasteleria
{
    public partial class FrmEmpleado : FormBase
    {
        private bool esNuevo;
        private int idEditando;
        private int idUsuarioEmpleado;

        public FrmEmpleado()
        {
            InitializeComponent();
            txtCelular.KeyPress += Util.onlyNumbers;
            txtCelular.MaxLength = 8;
            txtCedulaIdentidad.MaxLength = 12;
            txtUsuario.MaxLength = 15;
            cbxRol.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private int? idSeleccionado()
        {
            return dgvLista.CurrentRow == null ? (int?)null : (int)dgvLista.CurrentRow.Cells["id"].Value;
        }

        private void configurarPermisos()
        {
            bool admin = Sesion.EsAdministrador;
            bool hay = dgvLista.Rows.Count > 0;
            btnNuevo.Enabled = admin;
            btnEditar.Enabled = admin && hay;
            btnEliminar.Enabled = admin && hay;
        }

        private void listar()
        {
            try
            {
                dgvLista.DataSource = EmpleadoCln.listarPa(txtBuscar.Text.Trim());
                dgvLista.Columns["id"].Visible = false;
                dgvLista.Columns["estado"].Visible = false;
                dgvLista.Columns["cedulaIdentidad"].HeaderText = "Cédula Identidad";
                dgvLista.Columns["nombre"].HeaderText = "Nombre";
                dgvLista.Columns["apellidoPaterno"].HeaderText = "Apellido Paterno";
                dgvLista.Columns["apellidoMaterno"].HeaderText = "Apellido Materno";
                dgvLista.Columns["fechaNacimiento"].HeaderText = "Fecha Nacimiento";
                dgvLista.Columns["direccion"].HeaderText = "Dirección";
                dgvLista.Columns["celular"].HeaderText = "Celular";
                dgvLista.Columns["role"].HeaderText = "Rol";
                dgvLista.Columns["UsuarioAsignado"].HeaderText = "Usuario Asignado";
                dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
                dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            }
            catch (Exception ex) { Util.Error(ex); }
            configurarPermisos();
        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            Size = new Size(673, 428);
            dtpFechaNacimiento.MaxDate = DateTime.Today.AddYears(-18);
            dtpFechaNacimiento.Value = dtpFechaNacimiento.MaxDate;
            listar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) { Util.Advertencia("Sólo un administrador puede registrar empleados."); return; }
            esNuevo = true;
            idEditando = 0;
            idUsuarioEmpleado = 0;
            limpiarErrores();
            pnlAcciones.Enabled = false;
            Size = new Size(673, 630);
            txtCedulaIdentidad.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) { Util.Advertencia("Sólo un administrador puede editar empleados."); return; }
            var id = idSeleccionado();
            if (id == null) return;
            var empleado = EmpleadoCln.obtenerUno(id.Value);
            if (empleado == null) { Util.Advertencia("El empleado ya no existe."); listar(); return; }

            esNuevo = false;
            idEditando = id.Value;
            limpiarErrores();
            pnlAcciones.Enabled = false;
            Size = new Size(673, 630);

            var usuario = empleado.Usuario.FirstOrDefault(u => u.estado != EstadoRegistro.Eliminado);
            idUsuarioEmpleado = usuario == null ? 0 : usuario.id;

            txtCedulaIdentidad.Text = empleado.cedulaIdentidad;
            txtNombre.Text = empleado.nombre;
            txtApellidoPaterno.Text = empleado.apellidoPaterno;
            txtApellidoMaterno.Text = empleado.apellidoMaterno;
            dtpFechaNacimiento.Value = empleado.fechaNacimiento > dtpFechaNacimiento.MaxDate
                ? dtpFechaNacimiento.MaxDate : (empleado.fechaNacimiento < dtpFechaNacimiento.MinDate ? dtpFechaNacimiento.MinDate : empleado.fechaNacimiento);
            txtDireccion.Text = empleado.direccion;
            txtCelular.Text = empleado.celular.ToString();
            cbxRol.SelectedIndex = usuario == null ? -1 : cbxRol.FindStringExact(usuario.role);
            txtUsuario.Text = usuario == null ? "" : usuario.usuario1;
            txtCedulaIdentidad.Focus();
        }

        private void limpiar()
        {
            txtCedulaIdentidad.Clear();
            txtNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtDireccion.Clear();
            txtCelular.Clear();
            cbxRol.SelectedIndex = -1;
            txtUsuario.Clear();
            dtpFechaNacimiento.Value = dtpFechaNacimiento.MaxDate;
            idUsuarioEmpleado = 0;
        }

        private void limpiarErrores()
        {
            erpCedulaIdentidad.Clear();
            erpNombre.Clear();
            erpApellidoPaterno.Clear();
            erpApellidoMaterno.Clear();
            erpFechaNacimiento.Clear();
            erpDireccion.Clear();
            erpCelular.Clear();
            erpRol.Clear();
            erpUsuario.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(673, 428);
            pnlAcciones.Enabled = true;
            configurarPermisos();
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
            var ci = txtCedulaIdentidad.Text.Trim();
            if (ci.Length == 0) { erpCedulaIdentidad.SetError(txtCedulaIdentidad, "La cédula de identidad es obligatoria"); ok = false; }
            else if (EmpleadoCln.existeCedula(ci, idEditando)) { erpCedulaIdentidad.SetError(txtCedulaIdentidad, "Ya existe un empleado con esa cédula"); ok = false; }

            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { erpNombre.SetError(txtNombre, "El nombre es obligatorio"); ok = false; }
            if (string.IsNullOrWhiteSpace(txtApellidoPaterno.Text)) { erpApellidoPaterno.SetError(txtApellidoPaterno, "El apellido paterno es obligatorio"); ok = false; }
            if (!Validacion.EsMayorDeEdad(dtpFechaNacimiento.Value))
            { erpFechaNacimiento.SetError(dtpFechaNacimiento, "El empleado debe tener 18 años o más"); ok = false; }
            if (string.IsNullOrWhiteSpace(txtDireccion.Text)) { erpDireccion.SetError(txtDireccion, "La dirección es obligatoria"); ok = false; }

            var cel = txtCelular.Text.Trim();
            if (cel.Length != 8 || !Validacion.SoloDigitos(cel)) { erpCelular.SetError(txtCelular, "El celular debe tener exactamente 8 dígitos"); ok = false; }
            if (cbxRol.SelectedIndex < 0) { erpRol.SetError(cbxRol, "El rol es obligatorio"); ok = false; }

            var usuario = txtUsuario.Text.Trim();
            var errorUsuario = Validacion.ValidarNombreUsuario(usuario);
            if (errorUsuario != null) { erpUsuario.SetError(txtUsuario, errorUsuario); ok = false; }
            else if (UsuarioCln.existeUsuario(usuario, idUsuarioEmpleado)) { erpUsuario.SetError(txtUsuario, "Ese nombre de usuario ya existe. Elija otro."); ok = false; }
            return ok;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;
                var empleado = new Empleado
                {
                    cedulaIdentidad = txtCedulaIdentidad.Text,
                    nombre = txtNombre.Text,
                    apellidoPaterno = txtApellidoPaterno.Text,
                    apellidoMaterno = txtApellidoMaterno.Text,
                    fechaNacimiento = dtpFechaNacimiento.Value.Date,
                    direccion = txtDireccion.Text,
                    celular = long.Parse(txtCelular.Text.Trim())
                };

                string claveTemporal;
                if (esNuevo)
                    EmpleadoCln.insertar(empleado, txtUsuario.Text, cbxRol.Text, Sesion.NombreUsuario, out claveTemporal);
                else
                {
                    empleado.id = idEditando;
                    EmpleadoCln.actualizar(empleado, txtUsuario.Text, cbxRol.Text, Sesion.NombreUsuario, out claveTemporal);
                }

                listar();
                btnCancelar.PerformClick();
                var msg = "Empleado guardado correctamente.";
                if (claveTemporal != null)
                    msg += "\n\nUsuario: " + txtUsuario.Text.Trim() + "\nContraseña temporal: " + claveTemporal +
                           "\n\nAnótela ahora: no se volverá a mostrar. El usuario puede cambiarla desde 'Cambiar contraseña'.";
                Util.Info(msg);
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) { Util.Advertencia("Sólo un administrador puede dar de baja empleados."); return; }
            var id = idSeleccionado();
            if (id == null) return;
            var nombre = dgvLista.CurrentRow.Cells["nombre"].Value + " " + dgvLista.CurrentRow.Cells["apellidoPaterno"].Value;
            if (!Util.Confirmar("¿Dar de baja al empleado " + nombre + "?\nSu usuario también quedará dado de baja y ya no podrá ingresar al sistema.")) return;
            try
            {
                EmpleadoCln.eliminar(id.Value, Sesion.NombreUsuario, Sesion.IdEmpleado);
                listar();
                Util.Info("Empleado dado de baja correctamente.");
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnCerrar_Click(object sender, EventArgs e) { Close(); }
    }
}
