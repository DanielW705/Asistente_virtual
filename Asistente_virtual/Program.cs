using System.Net.NetworkInformation;

namespace Asistente_virtual
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                Startup.Initialize();
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Form_asistente());
            }
            else
            {
                MessageBox.Show("Se debe estar conectado a internet","Para poder utilizar al asistente virtual es necesario tener una conexion a internet", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}