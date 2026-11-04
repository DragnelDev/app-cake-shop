using System;
using System.Threading;
using System.Windows.Forms;

namespace CpPasteleria
{
    internal static class Program
    {
        /// <summary>Punto de entrada: login -> principal -> (cerrar sesión) -> login ...</summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => Util.Error(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => Util.Error(e.ExceptionObject as Exception);

            bool volverAlLogin = true;
            while (volverAlLogin)
            {
                using (var login = new FrmAutenticacion())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;
                }
                using (var principal = new FrmPrincipal())
                {
                    principal.ShowDialog();
                    volverAlLogin = principal.SesionCerrada;
                }
                Sesion.Cerrar();
            }
        }
    }
}
