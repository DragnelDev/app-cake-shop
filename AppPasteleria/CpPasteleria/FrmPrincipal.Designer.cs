namespace CpPasteleria
{
    partial class FrmPrincipal
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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.pnlOpciones = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMarca = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblMarca = new System.Windows.Forms.Label();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.dgvProximas = new System.Windows.Forms.DataGridView();
            this.lblProximas = new System.Windows.Forms.Label();
            this.pnlTarjetas = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSaludo = new System.Windows.Forms.Label();
            this.lblFecha = new System.Windows.Forms.Label();
            this.pnlMenu.SuspendLayout();
            this.pnlMarca.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximas)).BeginInit();
            this.SuspendLayout();
            // pnlMenu
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(74, 38, 34);
            this.pnlMenu.Controls.Add(this.pnlOpciones);
            this.pnlMenu.Controls.Add(this.pnlMarca);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(240, 700);
            this.pnlMenu.Tag = "sinEstilo";
            // pnlMarca
            this.pnlMarca.Controls.Add(this.picLogo);
            this.pnlMarca.Controls.Add(this.lblMarca);
            this.pnlMarca.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMarca.Name = "pnlMarca";
            this.pnlMarca.Size = new System.Drawing.Size(240, 150);
            // picLogo
            this.picLogo.Image = global::CpPasteleria.Properties.Resources.emblema;
            this.picLogo.Location = new System.Drawing.Point(70, 14);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(100, 100);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabStop = false;
            // lblMarca
            this.lblMarca.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMarca.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblMarca.ForeColor = System.Drawing.Color.White;
            this.lblMarca.Name = "lblMarca";
            this.lblMarca.Size = new System.Drawing.Size(240, 30);
            this.lblMarca.Text = "Pastelería Carmen";
            this.lblMarca.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // pnlOpciones
            this.pnlOpciones.AutoScroll = true;
            this.pnlOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOpciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlOpciones.Name = "pnlOpciones";
            this.pnlOpciones.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.pnlOpciones.WrapContents = false;
            // pnlContenido
            this.pnlContenido.Controls.Add(this.dgvProximas);
            this.pnlContenido.Controls.Add(this.lblProximas);
            this.pnlContenido.Controls.Add(this.pnlTarjetas);
            this.pnlContenido.Controls.Add(this.lblFecha);
            this.pnlContenido.Controls.Add(this.lblSaludo);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Padding = new System.Windows.Forms.Padding(28, 20, 28, 20);
            // lblSaludo
            this.lblSaludo.AutoSize = true;
            this.lblSaludo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSaludo.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
            this.lblSaludo.Name = "lblSaludo";
            this.lblSaludo.Text = "Hola";
            // lblFecha
            this.lblFecha.AutoSize = true;
            this.lblFecha.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblFecha.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Padding = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblFecha.Text = "fecha";
            // pnlTarjetas
            this.pnlTarjetas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTarjetas.Name = "pnlTarjetas";
            this.pnlTarjetas.Size = new System.Drawing.Size(800, 236);
            // lblProximas
            this.lblProximas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProximas.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblProximas.Name = "lblProximas";
            this.lblProximas.Padding = new System.Windows.Forms.Padding(0, 12, 0, 6);
            this.lblProximas.Size = new System.Drawing.Size(800, 42);
            this.lblProximas.Text = "Próximas entregas pendientes";
            // dgvProximas
            this.dgvProximas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProximas.Name = "dgvProximas";
            // FrmPrincipal
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlMenu);
            this.MinimumSize = new System.Drawing.Size(980, 640);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pastelería Carmen";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlMarca.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlContenido.ResumeLayout(false);
            this.pnlContenido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProximas)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.FlowLayoutPanel pnlOpciones;
        private System.Windows.Forms.Panel pnlMarca;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblMarca;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.DataGridView dgvProximas;
        private System.Windows.Forms.Label lblProximas;
        private System.Windows.Forms.FlowLayoutPanel pnlTarjetas;
        private System.Windows.Forms.Label lblSaludo;
        private System.Windows.Forms.Label lblFecha;
    }
}
