namespace CpPasteleria
{
    partial class FrmUsuario
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
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.pnlAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxRol = new System.Windows.Forms.ComboBox();
            this.btnCambiarRol = new System.Windows.Forms.Button();
            this.btnRestablecer = new System.Windows.Forms.Button();
            this.btnActivar = new System.Windows.Forms.Button();
            this.btnDesactivar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.pnlAcciones.SuspendLayout();
            this.SuspendLayout();
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.Location = new System.Drawing.Point(16, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Text = "Gestión de usuarios";
            this.dgvLista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLista.Location = new System.Drawing.Point(16, 50);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.Size = new System.Drawing.Size(768, 310);
            this.dgvLista.TabIndex = 0;
            this.pnlAcciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAcciones.Controls.Add(this.cbxRol);
            this.pnlAcciones.Controls.Add(this.btnCambiarRol);
            this.pnlAcciones.Controls.Add(this.btnRestablecer);
            this.pnlAcciones.Controls.Add(this.btnActivar);
            this.pnlAcciones.Controls.Add(this.btnDesactivar);
            this.pnlAcciones.Controls.Add(this.btnCerrar);
            this.pnlAcciones.Location = new System.Drawing.Point(16, 372);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(768, 46);
            this.cbxRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRol.Name = "cbxRol";
            this.cbxRol.Size = new System.Drawing.Size(120, 23);
            this.cbxRol.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.btnCambiarRol.Name = "btnCambiarRol";
            this.btnCambiarRol.Size = new System.Drawing.Size(105, 36);
            this.btnCambiarRol.Text = "Cambiar rol";
            this.btnCambiarRol.Click += new System.EventHandler(this.btnCambiarRol_Click);
            this.btnRestablecer.Name = "btnRestablecer";
            this.btnRestablecer.Size = new System.Drawing.Size(170, 36);
            this.btnRestablecer.Text = "Restablecer contraseña";
            this.btnRestablecer.Click += new System.EventHandler(this.btnRestablecer_Click);
            this.btnActivar.Name = "btnActivar";
            this.btnActivar.Size = new System.Drawing.Size(90, 36);
            this.btnActivar.Text = "Activar";
            this.btnActivar.Click += new System.EventHandler(this.btnActivar_Click);
            this.btnDesactivar.Name = "btnDesactivar";
            this.btnDesactivar.Size = new System.Drawing.Size(100, 36);
            this.btnDesactivar.Text = "Desactivar";
            this.btnDesactivar.Click += new System.EventHandler(this.btnDesactivar_Click);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 36);
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 430);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.dgvLista);
            this.Controls.Add(this.pnlAcciones);
            this.MinimumSize = new System.Drawing.Size(816, 469);
            this.Name = "FrmUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Usuarios - Pastelería Carmen";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.pnlAcciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.FlowLayoutPanel pnlAcciones;
        private System.Windows.Forms.ComboBox cbxRol;
        private System.Windows.Forms.Button btnCambiarRol;
        private System.Windows.Forms.Button btnRestablecer;
        private System.Windows.Forms.Button btnActivar;
        private System.Windows.Forms.Button btnDesactivar;
        private System.Windows.Forms.Button btnCerrar;
    }
}
