using FinTech.UI.Consola;
using FinTech.Data;
using FinTech;
/*
Punto de Entrada 
e Inyector de dependencias
*/
var repoCategorias = new RepoCategoriaCSV();
var repoApuntes = new RepoApuntesCSV();
var sistema = new Sistema(repoCategorias, repoApuntes);

var vista = new Vista();
var controlador = new Controlador(sistema, vista);

controlador.Run();
