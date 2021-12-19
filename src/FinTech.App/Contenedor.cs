using System;
using System.Collections.Generic;
using System.Linq;

/*
BETA
Todavía no está para integrar en el código
Los parametros de inicialización de los servicios se haran por configuración


IoCContainer.Registrar<IWelcomer, Welcomer>();
IoCContainer.Registrar<IWriter, ConsoleWriter>();
IoCContainer.Registrar<IRepoApuntes, RepoApuntesCSV>();
IoCContainer.Registrar<IRepoCategoria, RepoCategoriaCSV>();
IoCContainer.Registrar<Sistema>();

var welcomer = IoCContainer.Crear<IWelcomer>();
welcomer.SayHelloTo("World");

var sistema2 = IoCContainer.Crear<Sistema>();
*/

// Static para referenciarlo desde todos los componentes
public static class IoCContainer
{
    private static readonly Dictionary<Type, Type> servicios = new Dictionary<Type, Type>();

    static IoCContainer()
    {

    }

    public static void Register<TImplementation>() =>
        Register<TImplementation, TImplementation>();
    public static void Register<TInterface, TImplementation>() where TImplementation : TInterface =>
        servicios[typeof(TInterface)] = typeof(TImplementation);

    public static TInterface Create<TInterface>() =>
        (TInterface)Create(typeof(TInterface));

    private static object Create(Type type)
    {
        // Reflexión
        // Obtenemos el primer constructor
        var concreteType = servicios[type];
        var defaultConstructor = concreteType.GetConstructors()[0];//Locate(type);
        // Obtenemos los parámetros
        var defaultParams = defaultConstructor.GetParameters();
        // Instanciamos los parámetros con recursión
        // Los parámetros de los servicios sólo pueden ser otros servicios
        var parameters = defaultParams.Select(param => Create(param.ParameterType)).ToArray();
        // Devolvemos el servicio registrado
        return defaultConstructor.Invoke(parameters);
    }

    /*
 IoCContainer.Register<IWelcomer, Welcomer>();
IoCContainer.Register<IWriter, ConsoleWriter>();
IoCContainer.Register<IRepoApuntes, RepoApuntesCSV>();
IoCContainer.Register<IRepoCategoria, RepoCategoriaCSV>();
IoCContainer.Register<Sistema>();

var welcomer = IoCContainer.Create<IWelcomer>();
welcomer.SayHelloTo("World");

var sistema2 = IoCContainer.Create<Sistema>();

var repoCategorias = new RepoCategoriaCSV();
var repoApuntes = new RepoApuntesCSV();
var parameters = new object[] {repoCategorias, repoApuntes};

var s4 = IoCContainer.Constructor<Sistema>( parameters );
var s5 = IoCContainer.Constructor<Sistema>();


var vista = new Vista();
vista.MostrarListaEnumerada<FinTech.Models.Categoria>("milagro4", s4.QryCategorias());
vista.MostrarListaEnumerada<FinTech.Models.Categoria>("milagro5", s5.QryCategorias());

return;


    */

    public static TInterface Constructor<TInterface>(Object[] parameters = null) =>
            (TInterface)Constructor(typeof(TInterface), parameters);
    public static object Constructor(Type type, Object[] concreteParams)
    {
        // Reflexión
        // Obtenemos el primer constructor
        var concreteType = servicios[type];
        var defaultConstructor = concreteType.GetConstructors()[0];
        // Obtenemos los parámetros
        var defaultParams = defaultConstructor.GetParameters();
        // Instanciamos los parámetros con recursión
        // Los parámetros de los servicios sólo pueden ser otros servicios
        var parameters = concreteParams ?? defaultParams.Select(param => Constructor(param.ParameterType, null)).ToArray();
        // Devolvemos el servicio registrado
        return defaultConstructor.Invoke(parameters);
    }
}



public interface IWelcomer
{
    void SayHelloTo(string name);
}

public class Welcomer : IWelcomer
{
    private IWriter writer;

    public Welcomer(IWriter writer)
    {
        this.writer = writer;
    }

    public void SayHelloTo(string name)
    {
        writer.Write($"Hello {name}!");
    }
}



public interface IWriter
{
    void Write(string s);
}

public class ConsoleWriter : IWriter
{
    public void Write(string s)
    {
        Console.WriteLine(s);
    }
}