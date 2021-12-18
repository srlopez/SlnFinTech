using System;
using System.Collections.Generic;
using System.Linq;
using FinTech.Models;

namespace FinTech.UI.Consola
{
    enum Modo
    {
        // Modo de la Interfaz
        // En Modo 'Admin' se presentan las opciones de 'Usuario'
        // y las propias de 'Admin'
        Anonimo,
        Usuario,
        Admin
    }

    public class Controlador
    {
        private Sistema _sistema;
        private Vista _vista;

        // Miembros
        private Modo _modo; //Modo de la Interfaz
        private string[] _menuModo = { "Login", "Logout User", "Exit Admin mode" };
        private Dictionary<(string cdu, Modo modo), Action> _casosDeUso;

        private string _usuario;

        public Controlador(Sistema sistema, Vista vista)
        {
            _sistema = sistema;
            _vista = vista;
            _modo = Modo.Anonimo;
            _usuario = "Anonimo";
            _casosDeUso = new Dictionary<(string, Modo), Action>(){
                { ("Consultar Categorias",Modo.Anonimo), qryCategorias },
                { ("Consultar SubCategorias",Modo.Anonimo), qrySubCategorias },
                { ("Consultar Apuntes",Modo.Anonimo), qryApuntes },
                { ("Registrar Apuntes",Modo.Usuario), crudApuntes },
                { ("Informe Importes",Modo.Usuario), qryInformesDeImportes },
                { ("Mantenimiento de Categorias",Modo.Admin), crudCategorias },
                { ("Mantenimiento de SubCategorias",Modo.Admin), crudSubCategorias },
                { (_menuModo[(int)_modo],Modo.Anonimo), establecerModoInterfaz },
            };
        }

        // Ciclo de Menu
        public void Run()
        {
            _vista.LimpiarPantalla();

            while (true)
                try
                {
                    // Menu y Obtener opción 
                    var menu = _casosDeUso.Keys.Where(k => k.modo <= _modo).Select(k => k.cdu).ToList<String>();
                    var cdu = _vista.TryObtenerElementoDeLista($"Menu de {_modo}", menu, "Seleciona una opción");
                    // Ejecución de la opción escogida
                    _vista.Mostrar(cdu);
                    _casosDeUso.FirstOrDefault(k => k.Key.cdu == cdu).Value.Invoke();
                    // Fin opción
                    _vista.MostrarYReturn("Pulsa <Return> para continuar");
                    _vista.LimpiarPantalla();
                }
                catch
                {
                    return;
                }
        }

        // Casos de Uso
        private void qryCategorias()
        {
            var lista = _sistema.QryCategorias(0);
            _vista.MostrarListaEnumerada<Categoria>("Categorias principales", lista);
        }
        private void qrySubCategorias()
        {
            var parent = _vista.TryObtenerElementoDeLista("Categorias principales", _sistema.QryCategorias(), "Indica la categoría padre");
            var lista = _sistema.QryCategorias(parent.Id);
            _vista.MostrarListaEnumerada<Categoria>($"SubCategoria {parent.Descripcion}", lista);
        }
        private void qryApuntes()
        {
            var lista = _sistema.QryApuntes();
            _vista.MostrarListaEnumerada<Apunte>("Apuntes", lista);
        }
        private void qryInformesDeImportes() { }
        private void crudApuntes()
        {
            try
            {
                // Obtencion de información de usuario
                var parentcat = _vista.TryObtenerElementoDeLista("Categorias principales", _sistema.QryCategorias(), "Indica la categoría padre");
                var subcat = _vista.TryObtenerElementoDeLista("SubCategorias", _sistema.QryCategorias(parentcat.Id), "Indica la subcategoría");
                decimal importe = _vista.TryObtenerDatoDeTipo<decimal>("Importe");
                string detalle = _vista.TryObtenerDatoDeTipo<string>("Detalle");
                Apunte apunte = new Apunte(parentcat.Id, subcat.Id, importe, _usuario, detalle);
                // Ejecución de Caso de Uso
                _sistema.CmdRegistrarApunte(apunte);
                // Presentación al usuario
                _vista.Mostrar("Registro correcto");
            }
            catch (Exception e)
            {
                _vista.Mostrar($"UC: {e.Message}", ConsoleColor.DarkRed);
            }
        }
        private void crudCategorias()
        {
            crudCategoriasPorId(0, "Categorias principales");
        }
        private void crudSubCategorias()
        {
            while (true) try
                {
                    var parent = _vista.TryObtenerElementoDeLista("Categorias principales", _sistema.QryCategorias(), "Indica la categoría padre");
                    crudCategoriasPorId(parent.Id, $"Subcategoria {parent.Descripcion}");
                }
                catch { return; }
        }
        private void crudCategoriasPorId(int parentId, string titulo)
        {
            while (true) try
                {
                    var lista = _sistema.QryCategorias(parentId);
                    _vista.MostrarListaEnumerada<Categoria>(titulo, lista);
                    char cud = _vista.TryObtenerCharFromString("C=Create, U=Update, D=Delete, F=Fin", "CUDF", 'F');
                    // FIN
                    if (cud == 'F') return;
                    // CREATE
                    if (cud == 'C') create(parentId);
                    else
                    {
                        var idx = _vista.TryObtenerElementoEnRango(0, lista.Count, "Indica la línea");
                        if (idx == 0) continue;
                        Categoria cat = lista[idx - 1];
                        // UPDATE
                        if (cud == 'U') update(cat);
                        // DELETE
                        if (cud == 'D') delete(cat);
                    }
                }
                catch (Exception e)
                {
                    _vista.Mostrar($"UC: {e.Message}", ConsoleColor.DarkRed);
                    return;
                }

            void create(int parentId = 0)
            {   // Obtención de datos
                int id = _vista.TryObtenerDatoDeTipo<int>($"Identificador de categoría");
                string descripcion = _vista.TryObtenerDatoDeTipo<string>($"Descripción #{id}");
                Categoria categoria = new Categoria(parentId, id, descripcion);
                // Verificación Reglas de Negocio
                _sistema.Validador.ValidarCategoriaDescripcion(categoria);
                _sistema.Validador.ValidarCategoriaFechaRegistro();
                // Ejecución de Caso de Uso
                _sistema.CmdRegistrarCategoria(categoria);
                // Presentación al usuario
                //_vista.Mostrar("Registro correcto");
            }
            void update(Categoria cat)
            {
                string descripcion = _vista.TryObtenerDatoDeTipo<string>($"Descripción #{cat.Id}", cat.Descripcion);
                cat.Descripcion = descripcion;
                _sistema.CmdUpdateCategoria(cat);
                //_vista.Mostrar("Registro actualizado");
            }
            void delete(Categoria cat)
            {
                _sistema.CmdDeleteCategoria(cat);
                //_vista.Mostrar("Registro eliminado");
            }
        }
        private void establecerModoInterfaz()
        {
            if (_modo > Modo.Anonimo)
            {
                // Salimos de Modo.Admin y Usuario
                // Logout
                _modo = Modo.Anonimo;
            }
            else
                try
                {
                    var aux = _vista.TryObtenerDatoDeTipo<string>("Username").ToLower().Trim();
                    var password = _vista.TryObtenerDatoDeTipo<string>("Password");
                    // Sólo validamos que el primer caracter sea 'A' para pasar a modo Admin
                    if ("Aa".Contains(password[0]))
                    {
                        // Entramos en Modo Admin
                        _modo = Modo.Admin;
                        _usuario = aux;
                    }
                    else
                    {
                        if ("Uu".Contains(password[0]))
                        {
                            // Entramos en Modo usuario
                            _modo = Modo.Usuario;
                            _usuario = aux;
                        }
                        else
                        {
                            _vista.Mostrar("Acceso no permitido");
                            return;
                        }
                    }
                }
                catch { return; };

            establecerMenu(_modo);
            return;

            void establecerMenu(Modo modo)
            {
                var modoKey = _casosDeUso.FirstOrDefault(x => x.Value == establecerModoInterfaz).Key;
                _casosDeUso.Remove(modoKey);
                _casosDeUso.Add((_menuModo[(int)modo], modo), establecerModoInterfaz);
            }
        }

    }
}