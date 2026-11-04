using System;
using System.Drawing;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>
    /// Tema visual (paleta del logo de la pastelería). Se aplica en tiempo de ejecución a cualquier
    /// formulario derivado de FormBase: colores, tipografía, botones planos y grillas legibles.
    /// </summary>
    internal static class Estilo
    {
        public static readonly Color Cacao = ColorTranslator.FromHtml("#4A2622");
        public static readonly Color Rosa = ColorTranslator.FromHtml("#A6534D");
        public static readonly Color RosaOscuro = ColorTranslator.FromHtml("#8C433D");
        public static readonly Color RosaClaro = ColorTranslator.FromHtml("#F4DFDB");
        public static readonly Color Crema = ColorTranslator.FromHtml("#FFF8F5");
        public static readonly Color Texto = ColorTranslator.FromHtml("#3A2825");
        public static readonly Color Gris = ColorTranslator.FromHtml("#8A736F");
        public static readonly Color Borde = ColorTranslator.FromHtml("#E6D3CF");
        public static readonly Color Peligro = ColorTranslator.FromHtml("#B3382F");
        public static readonly Color Exito = ColorTranslator.FromHtml("#4F8A5B");
        public static readonly Color Neutro = ColorTranslator.FromHtml("#E9D9D5");

        public enum Tipo { Primario, Secundario, Peligro, Exito }

        public static Font Fuente(float tamano = 9.75F, FontStyle estilo = FontStyle.Regular)
        {
            return new Font("Segoe UI", tamano, estilo, GraphicsUnit.Point);
        }

        public static void Aplicar(Form form)
        {
            form.BackColor = Crema;
            form.ForeColor = Texto;
            CambiarFuente(form);
            if (form.Owner == null && form.StartPosition == FormStartPosition.WindowsDefaultLocation)
                form.StartPosition = FormStartPosition.CenterParent;
            foreach (Control c in form.Controls) Aplicar(c);
        }

        public static void Aplicar(Control control)
        {
            // Los controles con Tag = "sinEstilo" (y sus hijos) conservan el formato propio del formulario.
            if (control.Tag is string && (string)control.Tag == "sinEstilo") return;
            var boton = control as Button;
            var grilla = control as DataGridView;
            var texto = control as TextBoxBase;

            if (boton != null) EstilizarBoton(boton, TipoPorNombre(boton.Name));
            else if (grilla != null) EstilizarGrilla(grilla);
            else
            {
                CambiarFuente(control);
                if (control is Label || control is GroupBox || control is CheckBox || control is RadioButton)
                {
                    control.ForeColor = (control is GroupBox) ? Cacao : Texto;
                    if (control is Label && control.Font.Bold && control.Font.Size >= 12) control.ForeColor = Cacao;
                }
                else if (control is Panel || control is TabPage) control.BackColor = Crema;
                if (texto != null) control.BackColor = Color.White;
            }
            foreach (Control hijo in control.Controls) Aplicar(hijo);
        }

        // Cambia sólo la familia (Microsoft Sans Serif / Lucida...) conservando tamaño y estilo.
        private static void CambiarFuente(Control c)
        {
            var f = c.Font;
            if (f.Name == "Segoe UI") return;
            var estilo = f.Style;
            c.Font = new Font("Segoe UI", f.Size, estilo, f.Unit);
        }

        private static Tipo TipoPorNombre(string nombre)
        {
            var n = (nombre ?? "").ToLowerInvariant();
            if (n.Contains("eliminar") || n.Contains("anular")) return Tipo.Peligro;
            if (n.Contains("entregar") || n.Contains("finalizar")) return Tipo.Exito;
            if (n.Contains("cancelar") || n.Contains("cerrar") || n.Contains("salir") || n.Contains("editar")
                || n.Contains("limpiar") || n.Contains("quitar") || n.Contains("exportar")) return Tipo.Secundario;
            return Tipo.Primario;
        }

        public static void EstilizarBoton(Button b, Tipo tipo)
        {
            Color fondo, hover, letra;
            switch (tipo)
            {
                case Tipo.Peligro: fondo = Peligro; hover = ColorTranslator.FromHtml("#962C25"); letra = Color.White; break;
                case Tipo.Exito: fondo = Exito; hover = ColorTranslator.FromHtml("#3F7249"); letra = Color.White; break;
                case Tipo.Secundario: fondo = Neutro; hover = ColorTranslator.FromHtml("#DCC7C2"); letra = Cacao; break;
                default: fondo = Rosa; hover = RosaOscuro; letra = Color.White; break;
            }
            b.Font = Fuente(b.Font.Size, FontStyle.Regular);
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.UseVisualStyleBackColor = false;
            b.Cursor = Cursors.Hand;
            b.TextImageRelation = TextImageRelation.ImageBeforeText;

            Action pintar = () =>
            {
                b.BackColor = b.Enabled ? fondo : ColorTranslator.FromHtml("#EFE6E4");
                b.ForeColor = b.Enabled ? letra : ColorTranslator.FromHtml("#A89895");
                b.Cursor = b.Enabled ? Cursors.Hand : Cursors.Default;
            };
            pintar();
            b.EnabledChanged += (s, e) => pintar();
            b.MouseEnter += (s, e) => { if (b.Enabled) b.BackColor = hover; };
            b.MouseLeave += (s, e) => pintar();
        }

        public static void EstilizarGrilla(DataGridView g)
        {
            g.Font = Fuente(9.5F);
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.EnableHeadersVisualStyles = false;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.GridColor = Borde;
            g.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            g.ColumnHeadersHeight = 32;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.ColumnHeadersDefaultCellStyle.BackColor = Cacao;
            g.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            g.ColumnHeadersDefaultCellStyle.Font = Fuente(9.5F, FontStyle.Bold);
            g.ColumnHeadersDefaultCellStyle.SelectionBackColor = Cacao;
            g.DefaultCellStyle.BackColor = Color.White;
            g.DefaultCellStyle.ForeColor = Texto;
            g.DefaultCellStyle.SelectionBackColor = RosaClaro;
            g.DefaultCellStyle.SelectionForeColor = Cacao;
            g.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
            g.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FCF3F0");
            g.RowTemplate.Height = 28;
            g.RowHeadersVisible = false;
            g.ReadOnly = true;
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.MultiSelect = false;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.DataBindingComplete -= AlTerminarEnlace;
            g.DataBindingComplete += AlTerminarEnlace;
        }

        // Formatea fechas/importes y ajusta anchos después de cada enlace de datos.
        private static void AlTerminarEnlace(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var g = (DataGridView)sender;
            DataGridViewColumn ultima = null;
            foreach (DataGridViewColumn col in g.Columns)
            {
                if (!col.Visible) continue;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                col.MinimumWidth = 60;
                if (col.ValueType == typeof(DateTime) || col.ValueType == typeof(DateTime?))
                    col.DefaultCellStyle.Format = col.Name.ToLowerInvariant().Contains("nacimiento") ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm";
                else if (col.ValueType == typeof(decimal) || col.ValueType == typeof(decimal?))
                {
                    col.DefaultCellStyle.Format = "N2";
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                ultima = col;
            }
            if (ultima != null) ultima.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
