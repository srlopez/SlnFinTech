using FinTech.UI.Consola;
using FinTech.Data;
using FinTech;

AppServicios.Register<IRepoApuntes, RepoApuntesCSV>();
AppServicios.Register<IRepoCategoria, RepoCategoriaCSV>();
AppServicios.Register<Sistema>();
AppServicios.Register<Vista>();
AppServicios.Register<Controlador>();
// Servicio de configuración
AppServicios.Register<AppConfig>();
var appConfig = AppServicios.Create<AppConfig>();
var config = appConfig.Get();
config.Run = System.DateTime.Now;
System.Console.WriteLine(config.App);
appConfig.Save(config);

// Prueba instanciando un servicio con parámetros
// creamos el sistema con sus repositorios
// y luego es utilizado por el controlador
//
// var repoCategorias = new RepoCategoriaCSV();
// var repoApuntes = new RepoApuntesCSV();
// var repositorios = new object[] {repoCategorias, repoApuntes};
// var sistema = IoCContainer.Create<Sistema>(repositorios);

var controlador = AppServicios.Create<Controlador>();
controlador.Run();


// var repoCategorias = new RepoCategoriaCSV();
// var repoApuntes = new RepoApuntesCSV();
// var sistema = new Sistema(repoCategorias, repoApuntes);
// var vista = new Vista();
// var controlador = new Controlador(sistema, vista);



