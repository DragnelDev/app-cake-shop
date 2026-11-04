using System;
using System.Windows.Forms;

namespace CpPasteleria
{
    /// <summary>Formulario base: aplica el tema visual de la aplicación al abrirse.</summary>
    public class FormBase : Form
    {
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode) Estilo.Aplicar(this);
            base.OnLoad(e);
        }
    }
}
