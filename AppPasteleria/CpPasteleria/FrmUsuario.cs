using ClnPasteleria;
using System;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>Administración de cuentas de acceso (sólo ADMINISTRADOR): rol, estado y restablecimiento de clave.</summary>
    public partial class FrmUsuario : FormBase
    {
        public FrmUsuario()
        {
            InitializeComponent();
            cbxRol.Items.AddRange(Roles.Todos);
            cbxRol.SelectedIndex = 0;
        }

        private UsuarioResumen seleccionado()
        {
            return dgvLista.CurrentRow == null ? null : dgvLista.CurrentRow.DataBoundItem as UsuarioResumen;
        }

        private void listar()
        {
            try
            {
                dgvLista.DataSource = UsuarioCln.listarResumen();
                dgvLista.Columns["Id"].Visible = false;
                dgvLista.Columns["IdEmpleado"].Visible = false;
                dgvLista.Columns["EstadoCodigo"].Visible = false;
                dgvLista.Columns["FechaRegistro"].HeaderText = "Fecha de Registro";
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            if (Sesion.EsAdministrador) listar();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!Sesion.EsAdministrador)
            {
                Util.Advertencia("Sólo un administrador puede gestionar usuarios.");
                Close();
            }
        }

        private void ejecutar(Action<UsuarioResumen> accion, string ok)
        {
            var u = seleccionado();
            if (u == null) return;
            try
            {
                accion(u);
                listar();
                if (ok != null) Util.Info(ok);
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnCambiarRol_Click(object sender, EventArgs e)
        {
            ejecutar(u => UsuarioCln.cambiarRol(u.Id, cbxRol.Text, Sesion.NombreUsuario), "Rol actualizado.");
        }

        private void btnRestablecer_Click(object sender, EventArgs e)
        {
            var u = seleccionado();
            if (u == null) return;
            if (!Util.Confirmar("¿Generar una contraseña temporal para '" + u.Usuario + "'?\nLa contraseña actual dejará de funcionar.")) return;
            try
            {
                var temporal = UsuarioCln.restablecerClave(u.Id, Sesion.NombreUsuario);
                Util.Info("Contraseña temporal de '" + u.Usuario + "':\n\n" + temporal +
                          "\n\nEntréguesela al usuario; no se volverá a mostrar. Podrá cambiarla desde 'Cambiar contraseña'.");
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            ejecutar(u => UsuarioCln.cambiarEstado(u.Id, true, Sesion.IdUsuario, Sesion.NombreUsuario), "Usuario activado.");
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            var u = seleccionado();
            if (u == null || !Util.Confirmar("¿Desactivar al usuario '" + u.Usuario + "'? No podrá ingresar al sistema.")) return;
            ejecutar(x => UsuarioCln.cambiarEstado(x.Id, false, Sesion.IdUsuario, Sesion.NombreUsuario), "Usuario desactivado.");
        }

        private void btnCerrar_Click(object sender, EventArgs e) { Close(); }
    }
}
