using System;
using System.Globalization;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>Mensajes y filtros de teclado reutilizables.</summary>
    internal static class Util
    {
        public const string Titulo = "Pastelería Carmen";

        public static void Info(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Advertencia(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void Error(string mensaje)
        {
            MessageBox.Show(mensaje, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static bool Confirmar(string pregunta)
        {
            return MessageBox.Show(pregunta, Titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        /// <summary>Muestra un error técnico/de negocio con un mensaje amigable.</summary>
        public static void Error(Exception ex)
        {
            Error(ClnPasteleria.Errores.Mensaje(ex));
        }

        /// <summary>KeyPress: sólo dígitos.</summary>
        public static void onlyNumbers(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar));
        }

        /// <summary>KeyPress: letras, espacios y apóstrofe (nombres).</summary>
        public static void onlyLetters(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)
                          || e.KeyChar == ' ' || e.KeyChar == '\'' || e.KeyChar == '.');
        }

        /// <summary>KeyPress: números decimales con el separador de la cultura actual.</summary>
        public static void onlyDecimals(object sender, KeyPressEventArgs e)
        {
            var sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
            var texto = ((Control)sender).Text;
            e.Handled = !(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)
                          || (e.KeyChar == sep && texto.IndexOf(sep) < 0));
        }
    }
}
