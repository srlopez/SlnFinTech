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
    public static void Registrar<TImplementation>() => 
        Registrar<TImplementation, TImplementation>();
    public static void Registrar<TInterface, TImplementation>() where TImplementation : TInterface =>
        servicios[typeof(TInterface)] = typeof(TImplementation);
    
    public static TInterface Crear<TInterface>() =>
        (TInterface)Crear(typeof(TInterface));

    private static object Crear(Type type)
    {
        // Reflexión
        // Obtenemos el primer constructor
        var concreteType = servicios[type];
        var defaultConstructor = concreteType.GetConstructors()[0];
        // Obtenemos los parámetros
        var defaultParams = defaultConstructor.GetParameters();
        // Instanciamos los parámetros con recursión
        // Los parámetros de los servicios sólo pueden ser otros servicios
        var parameters = defaultParams.Select(param => Crear(param.ParameterType)).ToArray();
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