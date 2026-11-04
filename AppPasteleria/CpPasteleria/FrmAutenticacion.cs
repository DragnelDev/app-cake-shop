using ClnPasteleria;
using System;
using System.Windows.Forms;

namespace CpPasteleria
{
    public partial class FrmAutenticacion : FormBase
    {
        private int intentosFallidos;

        public FrmAutenticacion()
        {
            InitializeComponent();
            AcceptButton = btnIngresar;
            btnSalir.DialogResult = DialogResult.Cancel;
            CancelButton = btnSalir;
            txtClave.UseSystemPasswordChar = true;
        }

        private bool validar()
        {
            erpUsuario.Clear();
            erpClave.Clear();
            bool ok = true;
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                erpUsuario.SetError(txtUsuario, "El usuario es obligatorio");
                ok = false;
            }
            if (string.IsNullOrEmpty(txtClave.Text))
            {
                erpClave.SetError(txtClave, "La contraseña es obligatoria");
                ok = false;
            }
            return ok;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (!validar()) return;
            try
            {
                UseWaitCursor = true;
                var usuario = UsuarioCln.validar(txtUsuario.Text, txtClave.Text);
                if (usuario == null)
                {
                    intentosFallidos++;
                    txtClave.Clear();
                    txtClave.Focus();
                    Util.Error("Usuario y/o contraseña incorrectos." +
                               (intentosFallidos >= 3 ? "\nSi olvidó su contraseña, pida a un administrador que la restablezca." : ""));
                    return;
                }
                Sesion.Iniciar(usuario);
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                Util.Error(ex);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void txtClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            // El Enter lo maneja AcceptButton.
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
