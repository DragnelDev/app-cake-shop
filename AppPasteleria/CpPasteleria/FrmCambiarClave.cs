using ClnPasteleria;
using System;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>Permite al usuario autenticado cambiar su propia contraseña.</summary>
    public partial class FrmCambiarClave : FormBase
    {
        public FrmCambiarClave()
        {
            InitializeComponent();
            AcceptButton = btnGuardar;
            CancelButton = btnCancelar;
        }

        private void chkMostrar_CheckedChanged(object sender, EventArgs e)
        {
            txtActual.UseSystemPasswordChar = txtNueva.UseSystemPasswordChar = txtConfirmar.UseSystemPasswordChar = !chkMostrar.Checked;
        }

        private bool validar()
        {
            erpClave.Clear();
            bool ok = true;
            if (txtActual.Text.Length == 0) { erpClave.SetError(txtActual, "Ingrese su contraseña actual"); ok = false; }
            var error = Seguridad.ValidarPolitica(txtNueva.Text);
            if (error != null) { erpClave.SetError(txtNueva, error); ok = false; }
            if (txtNueva.Text != txtConfirmar.Text) { erpClave.SetError(txtConfirmar, "Las contraseñas no coinciden"); ok = false; }
            return ok;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!validar()) return;
            try
            {
                UsuarioCln.cambiarClave(Sesion.IdUsuario, txtActual.Text, txtNueva.Text, Sesion.NombreUsuario);
                Util.Info("Contraseña actualizada correctamente.");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex) { Util.Error(ex); }
        }
    }
}
