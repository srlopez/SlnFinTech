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
        private Sistema _sistema; //Dependencia Lógica de Negocio
        private Vista _vista; //Dependencia Terminal

        #region Modo Terminal [Propiedades]
        private string _usuario; // Usuario Logeado
        private Modo _modo; //Modo de la Interfaz
        private string[] _menuModo = { "Login", "Logout", "Logout Admin" }; // Opciones del menu en funcion del modo

        #endregion

        #region Casos de Uso [Propiedades]
        private Dictionary<(string titulo, Modo modo), Action> _casosDeUso;
        public Controlador(Sistema sistema, Vista vista)
        {
            _sistema = sistema;
            _vista = vista;
            _modo = Modo.Anonimo;
            _usuario = "Anonimo";
            _casosDeUso = new Dictionary<(string, Modo), Action>(){
                { ("Consultar Categorías",Modo.Anonimo), qryCategorias },

                { ("Consultar Apuntes",Modo.Usuario), qryApuntes },
                { ("Registrar Apuntes",Modo.Usuario), crudApuntes },
                { ("Informe Gastos por Categorías",Modo.Usuario), qryImportesXCategoriaSinFiltro },
                { ("Informe Gastos por Usuario",Modo.Usuario),qryImportesXCategoriaFiltradoXUsuario},
                { ("Informe Gastos entre fechas",Modo.Usuario),qryImportesXCategoriaFiltradoXFecha},
                { ("Informe Gastos por Usuario entre fechas",Modo.Usuario),qryImportesXCategoriaFiltradoXUsuarioYFecha},

                { ("Mantenimiento de Categorias",Modo.Admin), crudPrincipalesCategorias },
                { ("Mantenimiento de SubCategorias",Modo.Admin), crudSubCategorias },
                { (_menuModo[(int)_modo],Modo.Anonimo), establecerModoInterfaz },
            };
        }
        #endregion

        #region Run
        public void Run()
        {
            _vista.LimpiarPantalla();

            while (true)
                try
                {
                    // Menu y Obtener opción 
                    var menu = _casosDeUso.Keys.Where(k => k.modo <= _modo).Select(k => k.titulo).ToList<String>();
                    var opcion = _vista.TryObtenerElementoDeLista($"Menu de {_modo}", menu, "Seleciona una opción");
                    // Ejecución de la opción escogida
                    _vista.Mostrar(""); // (opcion);
                    var casoDeUso = _casosDeUso.FirstOrDefault(k => k.Key.titulo == opcion).Value;
                    casoDeUso.Invoke();
                    // Fin opción
                    _vista.MostrarYReturn("Pulsa <Return> para continuar", ConsoleColor.DarkGray);
                    _vista.LimpiarPantalla();
                }
                catch
                {
                    return;
                }
        }

        #endregion

        #region Casos de Uso [Metodos]

        private void qryCategorias()
        {
            try
            {
                var catParent = _vista.TryObtenerElementoDeLista("Selección de Categoria principal", _sistema.QryCategorias(), "Indica la categoría padre");
                var lista = _sistema.QryCategorias(catParent.Id);
                _vista.MostrarListaEnumerada<Categoria>($"Categoria '{catParent.Descripcion}'", lista);
            }
            catch { return; }

        }

        /*
        Inyección de predicado
        */
        private void qryImportesXCategoriaFiltradoXUsuario()
        {
            try
            {
                //string usr = _vista.TryObtenerDatoDeTipo<string>("Introduzca Usuario");
                var usr = _vista.TryObtenerElementoDeLista<string>("Usuarios", _sistema.QryUsuarios(), "Indica un Usuario");
                Func<Apunte, bool> where = (a) => a.Usuario == usr;
                GastosXCategoriaInclude($"Gastos de {usr}", where);
            }
            catch { return; }
        }
        private void qryImportesXCategoriaFiltradoXFecha()
        {
            try
            {
                var inicio = _vista.TryObtenerFecha("Desde Fecha");
                var fin = _vista.TryObtenerFecha("Hasta Fecha");
                Func<Apunte, bool> where = (a) => a.FechaApunte >= inicio && a.FechaApunte < fin;
                GastosXCategoriaInclude("Gastos familiares entre Fechas", where);
            }
            catch { return; }
        }
        private void qryImportesXCategoriaFiltradoXUsuarioYFecha()
        {
            try
            {
                var usr = _vista.TryObtenerElementoDeLista<string>("Usuarios", _sistema.QryUsuarios(), "Indica un Usuario");
                var inicio = _vista.TryObtenerFecha("Desde Fecha");
                var fin = _vista.TryObtenerFecha("Hasta Fecha");
                Func<Apunte, bool> where = (a) => a.Usuario == usr && a.FechaApunte >= inicio && a.FechaApunte < fin;
                GastosXCategoriaInclude($"Gastos de {usr} entre Fechas", where);
            }
            catch { return; }

        }
        private void qryImportesXCategoriaSinFiltro() => GastosXCategoriaInclude("Gastos Familiares");
        /*
        var categorias = _sistema.QryCategorias();
        _vista.MostrarListaEnumerada("Selección de Categoria principal",categorias);
        var cat = _vista.TryObtenerValorEnRangoInt(0,categorias.Count,"Selección de Categoria principal (0=todas)");
        var impList1 = _sistema.QryImporteDeGastoPorCategoria(id: cat.Id);
        _vista.MostrarListaEnumerada<GastoPorCategoria>("Gastos x SubCategoria", impList1);
        */

        private void GastosXCategoriaInclude(string title, Func<Apunte, bool> where = null)
        {
            try
            {
                var gastoList0 = _sistema.QryImporteDeGastoPorCategoria(predicado: where);
                if (gastoList0.Count == 0)
                {
                    _vista.Mostrar("No hay registros seleccionados");
                    return;
                }
                var cat = _vista.TryObtenerElementoDeLista(title, gastoList0, "Indica una categoría");
                var gastoList1 = _sistema.QryImporteDeGastoPorCategoria(id: cat.Id, predicado: where);
                _vista.MostrarListaEnumerada<GastoPorCategoria>($"Gastos en '{cat.Categoria}'", gastoList1);
            }
            catch { return; }
        }
        private void qryApuntes()
        {
            var apList = _sistema.QryApuntes();
            _vista.MostrarListaEnumerada<Apunte>("Consulta de Apuntes", apList);
        }
        private void crudApuntes()
        {
            try
            {
                // Obtencion de información de usuario
                var fecha = _vista.TryObtenerFecha("Fecha del gasto");
                var catPadre = _vista.TryObtenerElementoDeLista("Selección de Categoria principal", _sistema.QryCategorias(), "Indica la categoría padre");
                var catHijo = _vista.TryObtenerElementoDeLista("Selección de SubCategoria", _sistema.QryCategorias(catPadre.Id), "Indica la subcategoría");
                decimal importe = _vista.TryObtenerDatoDeTipo<decimal>("Introduzca Importe");
                string detalle = _vista.TryObtenerDatoDeTipo<string>("Introduzca Detalle");
                Apunte apunte = new Apunte(fecha, catPadre.Id, catHijo.Id, importe, _usuario, detalle);
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
        private void crudPrincipalesCategorias()
        {
            crudCategorias(0, "CRUD Categorias principales");
        }
        private void crudSubCategorias()
        {
            while (true) try
                {
                    var catParent = _vista.TryObtenerElementoDeLista("Selección de Categoria", _sistema.QryCategorias(), "Indica la categoría padre");
                    crudCategorias(catParent.Id, $"CRUD Subcategoria {catParent.Descripcion}");
                }
                catch { return; }
        }
        private void crudCategorias(int parentId, string titulo)
        {
            while (true) try
                {
                    var catList = _sistema.QryCategorias(parentId);
                    _vista.MostrarListaEnumerada<Categoria>(titulo, catList);
                    char cud = _vista.TryObtenerCaracterDeString("C=Create, U=Update, D=Delete", "CUD", 'C');
                    if (cud == 'C') create(parentId);
                    if (cud == 'U') update(selectCategoria(catList));
                    if (cud == 'D') delete(selectCategoria(catList));
                }
                catch (Exception e)
                {
                    _vista.Mostrar($"UC: {e.Message}", ConsoleColor.DarkRed);
                    return;
                }

            void create(int parentId = 0)
            {   // Obtención de datos
                int subId = _vista.TryObtenerDatoDeTipo<int>($"Identificador de categoría");
                string descripcion = _vista.TryObtenerDatoDeTipo<string>($"Descripción #{subId}");
                Categoria categoria = new Categoria(parentId, subId, descripcion);
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

            Categoria selectCategoria(List<Categoria> lista)
            {
                var idx = _vista.TryObtenerValorEnRangoInt(1, lista.Count, "Indica la línea");
                return lista[idx - 1];
            }
        }

        #endregion

        #region Modo Terminal [Metodos]
        private void establecerModoInterfaz()
        {
            switch (_modo)
            {
                case Modo.Usuario:
                case Modo.Admin:
                    establecerAnonimo();
                    break;
                case Modo.Anonimo:
                    try
                    {
                        var username = _vista.TryObtenerDatoDeTipo<string>("Username");
                        var password = _vista.TryObtenerDatoDeTipo<string>("Password").ToLower().Trim(); ;
                        // Sólo validamos que el primer caracter para pasar a modo Admin/User
                        if (!"au".Contains(password[0]))
                            _vista.Mostrar("Acceso no permitido", ConsoleColor.DarkRed);
                        if (password[0] == 'a')
                            establecerAdmin(username);
                        if (password[0] == 'u')
                            establecerUsuario(username);
                    }
                    catch { return; };
                    break;
            }
            void establecerAnonimo()
            {
                _usuario = "anomious";
                _modo = Modo.Anonimo;
                establecerOpcionDeMenu(_menuModo[(int)_modo]);
            };
            void establecerUsuario(string username)
            {
                _usuario = username.ToLower().Trim();
                _modo = Modo.Usuario;
                establecerOpcionDeMenu($"Logout <{username}>");
            };
            void establecerAdmin(string username)
            {
                _usuario = username.ToLower().Trim();
                _modo = Modo.Admin;
                establecerOpcionDeMenu(_menuModo[(int)_modo]);
            };
            void establecerOpcionDeMenu(string opcion)
            {
                var modoKey = _casosDeUso.FirstOrDefault(x => x.Value == establecerModoInterfaz).Key;
                _casosDeUso.Remove(modoKey);
                _casosDeUso.Add((opcion, _modo), establecerModoInterfaz);
            }
        }
        #endregion
    }
}