using FinTech.Data;
using FinTech;

namespace FinTech.UI.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Servicios
            AppServicios.Register<ILog, AppLog>();
            AppServicios.Register<AppConfig>();
            AppServicios.Register<IRepoApuntes, RepoApuntesCSV>();
            AppServicios.Register<IRepoCategoria, RepoCategoriaCSV>();
            AppServicios.Register<Sistema>();
            var sistema =  AppServicios.Create<Sistema>();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(sistema));
        }
    }
}