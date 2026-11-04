using CadPasteleria;
using ClnPasteleria;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CpPasteleria
{
    public partial class FrmProducto : FormBase
    {
        private bool esNuevo;
        private int idEditando;

        public FrmProducto()
        {
            InitializeComponent();
            txtCodigo.MaxLength = 20;
            nudPrecio.DecimalPlaces = 2;
            nudPrecio.Maximum = 99999;
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
                dgvLista.DataSource = ProductoCln.listarPa(txtBuscar.Text.Trim());
                dgvLista.Columns["id"].Visible = false;
                dgvLista.Columns["estado"].Visible = false;
                dgvLista.Columns["codigo"].HeaderText = "Código";
                dgvLista.Columns["nombre"].HeaderText = "Nombre";
                dgvLista.Columns["unidadMedida"].HeaderText = "Tamaño";
                dgvLista.Columns["precio"].HeaderText = "Precio (Bs)";
                dgvLista.Columns["tipo"].HeaderText = "Tipo";
                dgvLista.Columns["descripcion"].HeaderText = "Descripción";
                dgvLista.Columns["usuarioRegistro"].HeaderText = "Usuario Registro";
                dgvLista.Columns["fechaRegistro"].HeaderText = "Fecha de Registro";
            }
            catch (Exception ex) { Util.Error(ex); }
            configurarPermisos();
        }

        private void FrmProducto_Load(object sender, EventArgs e)
        {
            Size = new Size(682, 298);
            listar();
        }

        // Selecciona el valor en un combo DropDownList sin distinguir mayúsculas; lo agrega si no existe.
        private static void seleccionar(ComboBox cbx, string valor)
        {
            int i = cbx.FindStringExact(valor ?? "");
            if (i < 0 && !string.IsNullOrEmpty(valor)) i = cbx.Items.Add(valor);
            cbx.SelectedIndex = i;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            esNuevo = true;
            idEditando = 0;
            limpiarErrores();
            pnlAcciones.Enabled = false;
            Size = new Size(682, 470);
            txtCodigo.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var id = idSeleccionado();
            if (id == null) return;
            var p = ProductoCln.obtenerUno(id.Value);
            if (p == null) { Util.Advertencia("El producto ya no existe."); listar(); return; }

            esNuevo = false;
            idEditando = id.Value;
            limpiarErrores();
            pnlAcciones.Enabled = false;
            Size = new Size(682, 470);
            txtCodigo.Text = p.codigo;
            txtNombre.Text = p.nombre;
            seleccionar(cbxUnidadMedida, p.unidadMedida);
            seleccionar(cbxTipoProducto, p.tipo);
            txtDescripcion.Text = p.descripcion;
            nudPrecio.Value = Math.Min(p.precio, nudPrecio.Maximum);
            txtCodigo.Focus();
        }

        private void limpiar()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            cbxUnidadMedida.SelectedIndex = -1;
            txtDescripcion.Clear();
            cbxTipoProducto.SelectedIndex = -1;
            nudPrecio.Value = 0;
        }

        private void limpiarErrores()
        {
            erpCodigo.Clear();
            erpNombreProducto.Clear();
            erpUnidadMedida.Clear();
            erpDescripcion.Clear();
            erpPrecio.Clear();
            erpTipoProducto.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Size = new Size(682, 298);
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
            if (string.IsNullOrWhiteSpace(txtCodigo.Text)) { erpCodigo.SetError(txtCodigo, "El código es obligatorio"); ok = false; }
            else if (ProductoCln.existeCodigo(txtCodigo.Text, idEditando)) { erpCodigo.SetError(txtCodigo, "Ya existe un producto con ese código"); ok = false; }

            if (string.IsNullOrWhiteSpace(txtNombre.Text)) { erpNombreProducto.SetError(txtNombre, "El nombre es obligatorio"); ok = false; }
            else if (ProductoCln.existeNombre(txtNombre.Text, idEditando)) { erpNombreProducto.SetError(txtNombre, "Ya existe un producto con ese nombre"); ok = false; }

            if (cbxUnidadMedida.SelectedIndex < 0) { erpUnidadMedida.SetError(cbxUnidadMedida, "Seleccione el tamaño"); ok = false; }
            if (cbxTipoProducto.SelectedIndex < 0) { erpTipoProducto.SetError(cbxTipoProducto, "Seleccione el tipo"); ok = false; }
            if (string.IsNullOrWhiteSpace(txtDescripcion.Text)) { erpDescripcion.SetError(txtDescripcion, "La descripción es obligatoria"); ok = false; }
            if (nudPrecio.Value <= 0) { erpPrecio.SetError(nudPrecio, "El precio debe ser mayor a cero"); ok = false; }
            return ok;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validar()) return;
                var p = new Producto
                {
                    codigo = txtCodigo.Text,
                    nombre = txtNombre.Text,
                    unidadMedida = cbxUnidadMedida.Text,
                    descripcion = txtDescripcion.Text,
                    tipo = cbxTipoProducto.Text,
                    precio = nudPrecio.Value,
                    usuarioRegistro = Sesion.NombreUsuario
                };
                if (esNuevo) ProductoCln.insertar(p);
                else { p.id = idEditando; ProductoCln.actualizar(p); }

                listar();
                btnCancelar.PerformClick();
                Util.Info("Producto guardado correctamente.");
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (!Sesion.EsAdministrador) { Util.Advertencia("Sólo un administrador puede dar de baja productos."); return; }
            var id = idSeleccionado();
            if (id == null) return;
            var codigo = dgvLista.CurrentRow.Cells["codigo"].Value;
            if (!Util.Confirmar("¿Está seguro de dar de baja el producto " + codigo + "?")) return;
            try
            {
                ProductoCln.eliminar(id.Value, Sesion.NombreUsuario);
                listar();
                Util.Info("Producto dado de baja correctamente.");
            }
            catch (Exception ex) { Util.Error(ex); }
        }

        private void btnCerrar_Click(object sender, EventArgs e) { Close(); }
    }
}
