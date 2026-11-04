namespace CpPasteleria
{
    partial class FrmCambiarClave
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblActual = new System.Windows.Forms.Label();
            this.lblNueva = new System.Windows.Forms.Label();
            this.lblConfirmar = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.txtNueva = new System.Windows.Forms.TextBox();
            this.txtConfirmar = new System.Windows.Forms.TextBox();
            this.chkMostrar = new System.Windows.Forms.CheckBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.erpClave = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).BeginInit();
            this.SuspendLayout();
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(20, 16);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Cambiar contraseña";
            this.lblActual.AutoSize = true;
            this.lblActual.Location = new System.Drawing.Point(22, 62);
            this.lblActual.Name = "lblActual";
            this.lblActual.Text = "Contraseña actual:";
            this.txtActual.Location = new System.Drawing.Point(180, 59);
            this.txtActual.Name = "txtActual";
            this.txtActual.Size = new System.Drawing.Size(190, 23);
            this.txtActual.TabIndex = 0;
            this.txtActual.UseSystemPasswordChar = true;
            this.lblNueva.AutoSize = true;
            this.lblNueva.Location = new System.Drawing.Point(22, 98);
            this.lblNueva.Name = "lblNueva";
            this.lblNueva.Text = "Nueva contraseña:";
            this.txtNueva.Location = new System.Drawing.Point(180, 95);
            this.txtNueva.Name = "txtNueva";
            this.txtNueva.Size = new System.Drawing.Size(190, 23);
            this.txtNueva.TabIndex = 1;
            this.txtNueva.UseSystemPasswordChar = true;
            this.lblConfirmar.AutoSize = true;
            this.lblConfirmar.Location = new System.Drawing.Point(22, 134);
            this.lblConfirmar.Name = "lblConfirmar";
            this.lblConfirmar.Text = "Repetir contraseña:";
            this.txtConfirmar.Location = new System.Drawing.Point(180, 131);
            this.txtConfirmar.Name = "txtConfirmar";
            this.txtConfirmar.Size = new System.Drawing.Size(190, 23);
            this.txtConfirmar.TabIndex = 2;
            this.txtConfirmar.UseSystemPasswordChar = true;
            this.chkMostrar.AutoSize = true;
            this.chkMostrar.Location = new System.Drawing.Point(180, 165);
            this.chkMostrar.Name = "chkMostrar";
            this.chkMostrar.TabIndex = 3;
            this.chkMostrar.Text = "Mostrar contraseñas";
            this.chkMostrar.CheckedChanged += new System.EventHandler(this.chkMostrar_CheckedChanged);
            this.btnGuardar.Location = new System.Drawing.Point(150, 205);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(105, 36);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.btnCancelar.Location = new System.Drawing.Point(265, 205);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 36);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.erpClave.ContainerControl = this;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 262);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.lblActual);
            this.Controls.Add(this.txtActual);
            this.Controls.Add(this.lblNueva);
            this.Controls.Add(this.txtNueva);
            this.Controls.Add(this.lblConfirmar);
            this.Controls.Add(this.txtConfirmar);
            this.Controls.Add(this.chkMostrar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCambiarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar contraseña";
            ((System.ComponentModel.ISupportInitialize)(this.erpClave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.Label lblNueva;
        private System.Windows.Forms.Label lblConfirmar;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.TextBox txtNueva;
        private System.Windows.Forms.TextBox txtConfirmar;
        private System.Windows.Forms.CheckBox chkMostrar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider erpClave;
    }
}
