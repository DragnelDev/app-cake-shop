namespace CpPasteleria
{
    partial class FrmRegistrarPedido
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistrarPedido));
            this.gbxPedido = new System.Windows.Forms.GroupBox();
            this.cbxProducto = new System.Windows.Forms.ComboBox();
            this.cbxMetodoPago = new System.Windows.Forms.ComboBox();
            this.dateFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxTipoEntrega = new System.Windows.Forms.ComboBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnAgregarPedido = new System.Windows.Forms.Button();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.lblTelefono = new System.Windows.Forms.Label();
            this.btnAgregarCliente = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscarCliente = new System.Windows.Forms.TextBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.lblPrecio = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudPrecioUnitario = new System.Windows.Forms.NumericUpDown();
            this.lblDireccion = new System.Windows.Forms.Label();
            this.cbxEstado = new System.Windows.Forms.ComboBox();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lblObservaciones = new System.Windows.Forms.Label();
            this.cbxCliente = new System.Windows.Forms.ComboBox();
            this.lblFechaEntrega = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblPedidos = new System.Windows.Forms.Label();
            this.gbxDetalle = new System.Windows.Forms.GroupBox();
            this.dgvTemporal = new System.Windows.Forms.DataGridView();
            this.btnCancelarPedido = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnFinalizarPedido = new System.Windows.Forms.Button();
            this.txtTotalPedido = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.dgvPedidos = new System.Windows.Forms.DataGridView();
            this.gbxPedido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioUnitario)).BeginInit();
            this.gbxDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemporal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxPedido
            // 
            this.gbxPedido.Controls.Add(this.cbxProducto);
            this.gbxPedido.Controls.Add(this.cbxMetodoPago);
            this.gbxPedido.Controls.Add(this.dateFechaEntrega);
            this.gbxPedido.Controls.Add(this.label3);
            this.gbxPedido.Controls.Add(this.cbxTipoEntrega);
            this.gbxPedido.Controls.Add(this.txtTelefono);
            this.gbxPedido.Controls.Add(this.lblCantidad);
            this.gbxPedido.Controls.Add(this.btnAgregarPedido);
            this.gbxPedido.Controls.Add(this.nudCantidad);
            this.gbxPedido.Controls.Add(this.txtDireccion);
            this.gbxPedido.Controls.Add(this.lblTelefono);
            this.gbxPedido.Controls.Add(this.btnAgregarCliente);
            this.gbxPedido.Controls.Add(this.btnBuscar);
            this.gbxPedido.Controls.Add(this.txtBuscarCliente);
            this.gbxPedido.Controls.Add(this.lblProducto);
            this.gbxPedido.Controls.Add(this.lblPrecio);
            this.gbxPedido.Controls.Add(this.label2);
            this.gbxPedido.Controls.Add(this.nudPrecioUnitario);
            this.gbxPedido.Controls.Add(this.lblDireccion);
            this.gbxPedido.Controls.Add(this.cbxEstado);
            this.gbxPedido.Controls.Add(this.lblEstado);
            this.gbxPedido.Controls.Add(this.lblObservaciones);
            this.gbxPedido.Controls.Add(this.cbxCliente);
            this.gbxPedido.Controls.Add(this.lblFechaEntrega);
            this.gbxPedido.Controls.Add(this.lblCliente);
            this.gbxPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxPedido.Location = new System.Drawing.Point(5, 44);
            this.gbxPedido.Name = "gbxPedido";
            this.gbxPedido.Size = new System.Drawing.Size(868, 287);
            this.gbxPedido.TabIndex = 23;
            this.gbxPedido.TabStop = false;
            this.gbxPedido.Text = "Cabecera del Pedido";
            // 
            // cbxProducto
            // 
            this.cbxProducto.DisplayMember = "\"nombre\"";
            this.cbxProducto.FormattingEnabled = true;
            this.cbxProducto.Location = new System.Drawing.Point(156, 171);
            this.cbxProducto.Name = "cbxProducto";
            this.cbxProducto.Size = new System.Drawing.Size(229, 24);
            this.cbxProducto.TabIndex = 40;
            this.cbxProducto.ValueMember = "\"id\"";
            // 
            // cbxMetodoPago
            // 
            this.cbxMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMetodoPago.FormattingEnabled = true;
            this.cbxMetodoPago.Items.AddRange(new object[] {
            "QR",
            "EFECTIVO",
            "TARJETA",
            "BITCOIN",
            "DEPOSITO"});
            this.cbxMetodoPago.Location = new System.Drawing.Point(156, 205);
            this.cbxMetodoPago.Name = "cbxMetodoPago";
            this.cbxMetodoPago.Size = new System.Drawing.Size(229, 24);
            this.cbxMetodoPago.TabIndex = 34;
            // 
            // dateFechaEntrega
            // 
            this.dateFechaEntrega.CustomFormat = "dd/MM/yyyy      HH:mm";
            this.dateFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateFechaEntrega.Location = new System.Drawing.Point(156, 112);
            this.dateFechaEntrega.Name = "dateFechaEntrega";
            this.dateFechaEntrega.Size = new System.Drawing.Size(229, 22);
            this.dateFechaEntrega.TabIndex = 39;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "Metodo de Pago: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxTipoEntrega
            // 
            this.cbxTipoEntrega.AutoCompleteCustomSource.AddRange(new string[] {
            "ENTREGADO",
            "PENDIENTE"});
            this.cbxTipoEntrega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoEntrega.FormattingEnabled = true;
            this.cbxTipoEntrega.Items.AddRange(new object[] {
            "LOCAL (TIENDA)",
            "A DOMICILIO"});
            this.cbxTipoEntrega.Location = new System.Drawing.Point(582, 114);
            this.cbxTipoEntrega.Name = "cbxTipoEntrega";
            this.cbxTipoEntrega.Size = new System.Drawing.Size(200, 24);
            this.cbxTipoEntrega.TabIndex = 38;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Location = new System.Drawing.Point(156, 142);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(229, 22);
            this.txtTelefono.TabIndex = 36;
            // 
            // lblCantidad
            // 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantidad.Location = new System.Drawing.Point(444, 213);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(64, 16);
            this.lblCantidad.TabIndex = 19;
            this.lblCantidad.Text = "Cantidad:";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAgregarPedido
            // 
            this.btnAgregarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarPedido.Image = global::CpPasteleria.Properties.Resources.save;
            this.btnAgregarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarPedido.Location = new System.Drawing.Point(367, 240);
            this.btnAgregarPedido.Name = "btnAgregarPedido";
            this.btnAgregarPedido.Size = new System.Drawing.Size(141, 39);
            this.btnAgregarPedido.TabIndex = 20;
            this.btnAgregarPedido.Text = "Agregar Pedido";
            this.btnAgregarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarPedido.UseVisualStyleBackColor = false;
            this.btnAgregarPedido.Click += new System.EventHandler(this.btnAgregarPedido_Click);
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(582, 211);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new System.Drawing.Size(200, 22);
            this.nudCantidad.TabIndex = 20;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(582, 83);
            this.txtDireccion.MaxLength = 20;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(200, 22);
            this.txtDireccion.TabIndex = 35;
            // 
            // lblTelefono
            // 
            this.lblTelefono.AutoSize = true;
            this.lblTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelefono.Location = new System.Drawing.Point(14, 146);
            this.lblTelefono.Name = "lblTelefono";
            this.lblTelefono.Size = new System.Drawing.Size(91, 16);
            this.lblTelefono.TabIndex = 34;
            this.lblTelefono.Text = "Celular o Fijo: ";
            this.lblTelefono.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAgregarCliente
            // 
            this.btnAgregarCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarCliente.Image = global::CpPasteleria.Properties.Resources._new;
            this.btnAgregarCliente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarCliente.Location = new System.Drawing.Point(682, 18);
            this.btnAgregarCliente.Name = "btnAgregarCliente";
            this.btnAgregarCliente.Size = new System.Drawing.Size(180, 40);
            this.btnAgregarCliente.TabIndex = 33;
            this.btnAgregarCliente.Text = "Agregar Nuevo Cliente";
            this.btnAgregarCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregarCliente.UseVisualStyleBackColor = false;
            this.btnAgregarCliente.Click += new System.EventHandler(this.btnAgregarCliente_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.Image = global::CpPasteleria.Properties.Resources.search;
            this.btnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscar.Location = new System.Drawing.Point(575, 18);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 40);
            this.btnBuscar.TabIndex = 32;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscarCliente
            // 
            this.txtBuscarCliente.Location = new System.Drawing.Point(293, 27);
            this.txtBuscarCliente.MaxLength = 20;
            this.txtBuscarCliente.Name = "txtBuscarCliente";
            this.txtBuscarCliente.Size = new System.Drawing.Size(263, 22);
            this.txtBuscarCliente.TabIndex = 31;
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducto.Location = new System.Drawing.Point(14, 176);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(67, 16);
            this.lblProducto.TabIndex = 17;
            this.lblProducto.Text = "Producto: ";
            this.lblProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPrecio
            // 
            this.lblPrecio.AutoSize = true;
            this.lblPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecio.Location = new System.Drawing.Point(441, 182);
            this.lblPrecio.Name = "lblPrecio";
            this.lblPrecio.Size = new System.Drawing.Size(101, 16);
            this.lblPrecio.TabIndex = 22;
            this.lblPrecio.Text = "Precio Unitario: ";
            this.lblPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "Buscar Cliente por Celular o Fijo, CI, Direccion:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudPrecioUnitario
            // 
            this.nudPrecioUnitario.DecimalPlaces = 2;
            this.nudPrecioUnitario.Enabled = false;
            this.nudPrecioUnitario.Location = new System.Drawing.Point(582, 176);
            this.nudPrecioUnitario.Name = "nudPrecioUnitario";
            this.nudPrecioUnitario.Size = new System.Drawing.Size(200, 22);
            this.nudPrecioUnitario.TabIndex = 21;
            // 
            // lblDireccion
            // 
            this.lblDireccion.AutoSize = true;
            this.lblDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDireccion.Location = new System.Drawing.Point(441, 86);
            this.lblDireccion.Name = "lblDireccion";
            this.lblDireccion.Size = new System.Drawing.Size(67, 16);
            this.lblDireccion.TabIndex = 28;
            this.lblDireccion.Text = "Direccion:";
            this.lblDireccion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxEstado
            // 
            this.cbxEstado.AutoCompleteCustomSource.AddRange(new string[] {
            "ENTREGADO",
            "PENDIENTE"});
            this.cbxEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstado.FormattingEnabled = true;
            this.cbxEstado.Items.AddRange(new object[] {
            "PENDIENTE",
            "ENTREGADO"});
            this.cbxEstado.Location = new System.Drawing.Point(582, 143);
            this.cbxEstado.Name = "cbxEstado";
            this.cbxEstado.Size = new System.Drawing.Size(200, 24);
            this.cbxEstado.TabIndex = 22;
            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstado.Location = new System.Drawing.Point(441, 146);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(53, 16);
            this.lblEstado.TabIndex = 20;
            this.lblEstado.Text = "Estado:";
            this.lblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblObservaciones
            // 
            this.lblObservaciones.AutoSize = true;
            this.lblObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblObservaciones.Location = new System.Drawing.Point(441, 117);
            this.lblObservaciones.Name = "lblObservaciones";
            this.lblObservaciones.Size = new System.Drawing.Size(107, 16);
            this.lblObservaciones.TabIndex = 19;
            this.lblObservaciones.Text = "Tipo de Entrega:";
            this.lblObservaciones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxCliente
            // 
            this.cbxCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCliente.FormattingEnabled = true;
            this.cbxCliente.Location = new System.Drawing.Point(156, 78);
            this.cbxCliente.Name = "cbxCliente";
            this.cbxCliente.Size = new System.Drawing.Size(229, 24);
            this.cbxCliente.TabIndex = 15;
            this.cbxCliente.SelectedIndexChanged += new System.EventHandler(this.cbxCliente_SelectedIndexChanged);
            // 
            // lblFechaEntrega
            // 
            this.lblFechaEntrega.AutoSize = true;
            this.lblFechaEntrega.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFechaEntrega.Location = new System.Drawing.Point(14, 114);
            this.lblFechaEntrega.Name = "lblFechaEntrega";
            this.lblFechaEntrega.Size = new System.Drawing.Size(120, 16);
            this.lblFechaEntrega.TabIndex = 9;
            this.lblFechaEntrega.Text = "Fecha de Entrega: ";
            this.lblFechaEntrega.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.Location = new System.Drawing.Point(14, 81);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(51, 16);
            this.lblCliente.TabIndex = 7;
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPedidos
            // 
            this.lblPedidos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPedidos.Location = new System.Drawing.Point(99, 9);
            this.lblPedidos.Name = "lblPedidos";
            this.lblPedidos.Size = new System.Drawing.Size(664, 32);
            this.lblPedidos.TabIndex = 40;
            this.lblPedidos.Text = "Registro de Pedidos";
            this.lblPedidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbxDetalle
            // 
            this.gbxDetalle.Controls.Add(this.dgvTemporal);
            this.gbxDetalle.Controls.Add(this.btnCancelarPedido);
            this.gbxDetalle.Controls.Add(this.lblTotal);
            this.gbxDetalle.Controls.Add(this.btnFinalizarPedido);
            this.gbxDetalle.Controls.Add(this.txtTotalPedido);
            this.gbxDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxDetalle.Location = new System.Drawing.Point(5, 337);
            this.gbxDetalle.Name = "gbxDetalle";
            this.gbxDetalle.Size = new System.Drawing.Size(868, 175);
            this.gbxDetalle.TabIndex = 41;
            this.gbxDetalle.TabStop = false;
            this.gbxDetalle.Text = "Detalle del Pedido";
            // 
            // dgvTemporal
            // 
            this.dgvTemporal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTemporal.Location = new System.Drawing.Point(17, 21);
            this.dgvTemporal.Name = "dgvTemporal";
            this.dgvTemporal.Size = new System.Drawing.Size(823, 98);
            this.dgvTemporal.TabIndex = 32;
            // 
            // btnCancelarPedido
            // 
            this.btnCancelarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarPedido.Image = global::CpPasteleria.Properties.Resources.cancel;
            this.btnCancelarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarPedido.Location = new System.Drawing.Point(735, 125);
            this.btnCancelarPedido.Name = "btnCancelarPedido";
            this.btnCancelarPedido.Size = new System.Drawing.Size(105, 40);
            this.btnCancelarPedido.TabIndex = 16;
            this.btnCancelarPedido.Text = "Cancelar";
            this.btnCancelarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelarPedido.UseVisualStyleBackColor = false;
            this.btnCancelarPedido.Click += new System.EventHandler(this.btnCancelarPedido_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(25, 131);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(121, 16);
            this.lblTotal.TabIndex = 19;
            this.lblTotal.Text = "TOTAL PEDIDO:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFinalizarPedido
            // 
            this.btnFinalizarPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizarPedido.Image = global::CpPasteleria.Properties.Resources.save;
            this.btnFinalizarPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinalizarPedido.Location = new System.Drawing.Point(563, 125);
            this.btnFinalizarPedido.Name = "btnFinalizarPedido";
            this.btnFinalizarPedido.Size = new System.Drawing.Size(143, 40);
            this.btnFinalizarPedido.TabIndex = 20;
            this.btnFinalizarPedido.Text = "Finalizar Pedido";
            this.btnFinalizarPedido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFinalizarPedido.UseVisualStyleBackColor = false;
            this.btnFinalizarPedido.Click += new System.EventHandler(this.btnFinalizarPedido_Click);
            // 
            // txtTotalPedido
            // 
            this.txtTotalPedido.Location = new System.Drawing.Point(156, 131);
            this.txtTotalPedido.Name = "txtTotalPedido";
            this.txtTotalPedido.Size = new System.Drawing.Size(148, 22);
            this.txtTotalPedido.TabIndex = 17;
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(789, 9);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(10, 16);
            this.lblUsuario.TabIndex = 40;
            this.lblUsuario.Text = ".";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvPedidos
            // 
            this.dgvPedidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPedidos.Location = new System.Drawing.Point(22, 508);
            this.dgvPedidos.Name = "dgvPedidos";
            this.dgvPedidos.Size = new System.Drawing.Size(834, 151);
            this.dgvPedidos.TabIndex = 42;
            // 
            // FrmRegistrarPedido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 677);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.dgvPedidos);
            this.Controls.Add(this.gbxDetalle);
            this.Controls.Add(this.lblPedidos);
            this.Controls.Add(this.gbxPedido);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmRegistrarPedido";
            this.Text = "::: Pedido - Pasteleria :::";
            this.Load += new System.EventHandler(this.FrmRegistroPedido_Load);
            this.gbxPedido.ResumeLayout(false);
            this.gbxPedido.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrecioUnitario)).EndInit();
            this.gbxDetalle.ResumeLayout(false);
            this.gbxDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTemporal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPedidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox gbxPedido;
        private System.Windows.Forms.ComboBox cbxMetodoPago;
        private System.Windows.Forms.DateTimePicker dateFechaEntrega;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxTipoEntrega;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnAgregarPedido;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label lblTelefono;
        private System.Windows.Forms.Button btnAgregarCliente;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscarCliente;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.Label lblPrecio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudPrecioUnitario;
        private System.Windows.Forms.Label lblDireccion;
        private System.Windows.Forms.ComboBox cbxEstado;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Label lblObservaciones;
        private System.Windows.Forms.ComboBox cbxCliente;
        private System.Windows.Forms.Label lblFechaEntrega;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblPedidos;
        private System.Windows.Forms.GroupBox gbxDetalle;
        private System.Windows.Forms.DataGridView dgvTemporal;
        private System.Windows.Forms.Button btnCancelarPedido;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnFinalizarPedido;
        private System.Windows.Forms.TextBox txtTotalPedido;
        private System.Windows.Forms.DataGridView dgvPedidos;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.ComboBox cbxProducto;
    }
}