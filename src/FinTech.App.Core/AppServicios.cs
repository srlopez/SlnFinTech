using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinTech
{

// Static para referenciarlo desde todos los componentes
public static class AppServicios
{
    private static readonly Dictionary<Type, (Type, Object)> servicios =
        new Dictionary<Type, (Type, Object)>();

    static AppServicios() { }

    public static void Register<TImplementation>() =>
        Register<TImplementation, TImplementation>();
    public static void Register<TInterface, TImplementation>() where TImplementation : TInterface =>
        servicios[typeof(TInterface)] = (typeof(TImplementation), null);

      

    public static TInterface Create<TInterface>(Object[] parameters = null) =>
            (TInterface)Create(typeof(TInterface), parameters);
    public static object Create(Type type, Object[] concreteParams)
    {
        // Reflexión
        // Verificamos si la instancia ya está creada
        var (concreteType, concreteInstance) = servicios[type];
        if (concreteInstance is not null) return concreteInstance;

        // Obtenemos el primer constructor
        var defaultConstructor = concreteType.GetConstructors()[0];
        // Obtenemos los parámetros
        var defaultParams = defaultConstructor.GetParameters();
        // Instanciamos los parámetros con recursión
        // Los parámetros de los servicios sólo pueden ser otros servicios
        var parameters = concreteParams ?? defaultParams.Select(param => Create(param.ParameterType, null)).ToArray();
        // Construimos la instancia
        concreteInstance = defaultConstructor.Invoke(parameters);
        // Actualizamos el registro
        servicios[type] = (concreteType, concreteInstance);
        // Devolvemos la instancia
        return concreteInstance;
    }

    // public static string ToString()
    // {
    //     var sb = new StringBuilder();
    //     foreach (var (key, (type, instance)) in servicios)
    //         sb.Append($"{l(key)}: {l(type)} {instance! is null}\n");
    //     string l(object s) => s.ToString().Split(".").Last();
    //     return sb.ToString();
    // }
}
public interface IWelcomer {
	    void SayHelloTo(string name);
    }

    public class Welcomer : IWelcomer
    {
        private IWriter writer;

        public Welcomer(IWriter writer) {
            this.writer = writer;
        }

        public void SayHelloTo(string name)
        {
            writer.Write($"Hello {name}!");
        }
    }

    public interface IWriter {
        void Write(string s);
    }

    public class ConsoleWriter : IWriter
    {
        public void Write(string s)
        {
            Console.WriteLine(s);
        }
    }
}
