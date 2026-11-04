namespace CpPasteleria
{
    partial class FrmReporteVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmReporteVentas));
            this.gbxDatos = new System.Windows.Forms.GroupBox();
            this.txtTotalPedido = new System.Windows.Forms.TextBox();
            this.lblTotalVendido = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.gbxPedidos = new System.Windows.Forms.GroupBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.dtpFechaHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaDesde = new System.Windows.Forms.DateTimePicker();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblFechaHasta = new System.Windows.Forms.Label();
            this.lblFechaDesde = new System.Windows.Forms.Label();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.gbxDatos.SuspendLayout();
            this.gbxPedidos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxDatos
            // 
            this.gbxDatos.Controls.Add(this.txtTotalPedido);
            this.gbxDatos.Controls.Add(this.lblTotalVendido);
            this.gbxDatos.Controls.Add(this.btnCancelar);
            this.gbxDatos.Location = new System.Drawing.Point(18, 341);
            this.gbxDatos.Name = "gbxDatos";
            this.gbxDatos.Size = new System.Drawing.Size(848, 100);
            this.gbxDatos.TabIndex = 12;
            this.gbxDatos.TabStop = false;
            this.gbxDatos.Text = "Datos";
            // 
            // txtTotalPedido
            // 
            this.txtTotalPedido.Location = new System.Drawing.Point(255, 25);
            this.txtTotalPedido.Name = "txtTotalPedido";
            this.txtTotalPedido.Size = new System.Drawing.Size(152, 20);
            this.txtTotalPedido.TabIndex = 11;
            // 
            // lblTotalVendido
            // 
            this.lblTotalVendido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVendido.Location = new System.Drawing.Point(6, 25);
            this.lblTotalVendido.Name = "lblTotalVendido";
            this.lblTotalVendido.Size = new System.Drawing.Size(243, 20);
            this.lblTotalVendido.TabIndex = 6;
            this.lblTotalVendido.Text = "Total Ganado en el Periodo";
            this.lblTotalVendido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::CpPasteleria.Properties.Resources.cancel;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(729, 54);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 40);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // gbxPedidos
            // 
            this.gbxPedidos.Controls.Add(this.txtCliente);
            this.gbxPedidos.Controls.Add(this.dtpFechaHasta);
            this.gbxPedidos.Controls.Add(this.dtpFechaDesde);
            this.gbxPedidos.Controls.Add(this.btnGenerarReporte);
            this.gbxPedidos.Controls.Add(this.lblCliente);
            this.gbxPedidos.Controls.Add(this.lblFechaHasta);
            this.gbxPedidos.Controls.Add(this.lblFechaDesde);
            this.gbxPedidos.Controls.Add(this.dgvLista);
            this.gbxPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxPedidos.Location = new System.Drawing.Point(17, 70);
            this.gbxPedidos.Name = "gbxPedidos";
            this.gbxPedidos.Size = new System.Drawing.Size(849, 265);
            this.gbxPedidos.TabIndex = 11;
            this.gbxPedidos.TabStop = false;
            this.gbxPedidos.Text = "Lista de Reporte de Pedidos";
            // 
            // txtCliente
            // 
            this.txtCliente.Location = new System.Drawing.Point(535, 42);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(152, 22);
            this.txtCliente.TabIndex = 14;
            // 
            // dtpFechaHasta
            // 
            this.dtpFechaHasta.Location = new System.Drawing.Point(273, 42);
            this.dtpFechaHasta.Name = "dtpFechaHasta";
            this.dtpFechaHasta.Size = new System.Drawing.Size(241, 22);
            this.dtpFechaHasta.TabIndex = 18;
            // 
            // dtpFechaDesde
            // 
            this.dtpFechaDesde.Location = new System.Drawing.Point(9, 41);
            this.dtpFechaDesde.Name = "dtpFechaDesde";
            this.dtpFechaDesde.Size = new System.Drawing.Size(241, 22);
            this.dtpFechaDesde.TabIndex = 17;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnGenerarReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReporte.Image = global::CpPasteleria.Properties.Resources.save;
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(693, 23);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(147, 40);
            this.btnGenerarReporte.TabIndex = 12;
            this.btnGenerarReporte.Text = "Exportar CSV";
            this.btnGenerarReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(572, 23);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(51, 16);
            this.lblCliente.TabIndex = 9;
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFechaHasta
            // 
            this.lblFechaHasta.AutoSize = true;
            this.lblFechaHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaHasta.Location = new System.Drawing.Point(311, 21);
            this.lblFechaHasta.Name = "lblFechaHasta";
            this.lblFechaHasta.Size = new System.Drawing.Size(87, 16);
            this.lblFechaHasta.TabIndex = 8;
            this.lblFechaHasta.Text = "Fecha Hasta:";
            this.lblFechaHasta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFechaDesde
            // 
            this.lblFechaDesde.AutoSize = true;
            this.lblFechaDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaDesde.Location = new System.Drawing.Point(40, 21);
            this.lblFechaDesde.Name = "lblFechaDesde";
            this.lblFechaDesde.Size = new System.Drawing.Size(92, 16);
            this.lblFechaDesde.TabIndex = 7;
            this.lblFechaDesde.Text = "Fecha Desde:";
            this.lblFechaDesde.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToResizeRows = false;
            this.dgvLista.BackgroundColor = System.Drawing.SystemColors.MenuBar;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Location = new System.Drawing.Point(6, 77);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.Size = new System.Drawing.Size(835, 182);
            this.dgvLista.TabIndex = 0;
            // 
            // lblBuscar
            // 
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBuscar.Location = new System.Drawing.Point(135, 42);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(191, 16);
            this.lblBuscar.TabIndex = 10;
            this.lblBuscar.Text = "Buscar por Nombre del cliente:";
            this.lblBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(343, 40);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(267, 20);
            this.txtBuscar.TabIndex = 9;
            this.txtBuscar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscar_KeyPress);
            // 
            // lblPedidos
            // 
            this.lblPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.Location = new System.Drawing.Point(1, 9);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(865, 19);
            this.lblPedidos.TabIndex = 8;
            this.lblPedidos.Text = "Reporte de Pedidos";
            this.lblPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Image = global::CpPasteleria.Properties.Resources.search;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(616, 31);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(88, 40);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // FrmReporteVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 453);
            this.Controls.Add(this.gbxDatos);
            this.Controls.Add(this.gbxPedidos);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblPedidos);
            this.Controls.Add(this.btnBuscar);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmReporteVentas";
            this.Text = "::: Reporte Ventas - Pasteleria :::";
            this.Load += new System.EventHandler(this.FrmReporteVentas_Load);
            this.gbxDatos.ResumeLayout(false);
            this.gbxDatos.PerformLayout();
            this.gbxPedidos.ResumeLayout(false);
            this.gbxPedidos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxDatos;
        private System.Windows.Forms.TextBox txtTotalPedido;
        private System.Windows.Forms.Label lblTotalVendido;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox gbxPedidos;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblFechaHasta;
        private System.Windows.Forms.Label lblFechaDesde;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.DateTimePicker dtpFechaHasta;
        private System.Windows.Forms.DateTimePicker dtpFechaDesde;
        private System.Windows.Forms.TextBox txtCliente;
    }
}