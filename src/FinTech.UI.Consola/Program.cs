using FinTech.UI.Consola;
using FinTech.Data;
using FinTech;
#region Registro de servicios
// Servicio de configuración
AppServicios.Register<ILog,ConsoleLog>();
AppServicios.Register<AppConfig>();
var appConfig = AppServicios.Create<AppConfig>();
var config = appConfig.Get();
config.Run = System.DateTime.Now;
appConfig.Save(config);
// Servicios clásicos 3 Capas
AppServicios.Register<IRepoApuntes, RepoApuntesCSV>();
AppServicios.Register<IRepoCategoria, RepoCategoriaCSV>();
AppServicios.Register<Sistema>();
AppServicios.Register<Vista>();
AppServicios.Register<Controlador>();
// Prueba instanciando un servicio con parámetros
// creamos el sistema con sus repositorios
// y luego es utilizado por el controlador
//
// var repoCategorias = new RepoCategoriaCSV();
// var repoApuntes = new RepoApuntesCSV();
// var repositorios = new object[] {repoCategorias, repoApuntes};
// var sistema = IoCContainer.Create<Sistema>(repositorios);
#endregion

// Instanciamos el motor de la aplicación
var controlador = AppServicios.Create<Controlador>();
controlador.Run();
