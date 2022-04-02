using FinTech.Models;

namespace FinTech.UI.WinForms
{
    public partial class MainForm : Form
    {
        private Sistema _sistema;

        private List<Categoria> _categorias;

        private List<ImporteCategoria> _importes;

        private int _catActiva;


        public MainForm(Sistema sistema)
        {
            _sistema = sistema;
            _categorias = _sistema.QryCategorias();

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            importesToolStripMenuItem_Click(sender, e);
            EstablecerCategoria(0);
            CrearControles();
        }

        // MENU
        private void apuntesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlApuntes.Visible = true;
            pnlCategorias.Visible = false;
            pnlImportes.Visible = false;
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlApuntes.Visible = false;
            pnlCategorias.Visible = true;
            pnlImportes.Visible = false;
        }

        private void importesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlApuntes.Visible = false;
            pnlCategorias.Visible = false;
            pnlImportes.Visible = true;
        }

        // RESIZE
        private void pnlControles_ClientSizeChanged(object sender, EventArgs e)
        {
            if (pnlImportes.Visible == true)
                CrearControles();
        }

        // PINTAMOS
        private void EstablecerCategoria(int catId)
        {
            _catActiva = catId;
            _importes = _sistema.QryImporteApuntes(catId);

            lblCategoria.Text = catId == 0 ? "Gasto total" : _categorias.FirstOrDefault(cat => cat.Id == catId).Descripcion;
            lblTotal.Text = _importes.Sum(imp => imp.Importe).ToString()+" €";
            btnBack.Visible = catId != 0;
        }
        private void CrearControles()
        {
            var areaContenedora = new CalculadoraDeAreas.Area { x = 0, y = 0, w = pnlControles.Width, h = pnlControles.Height };
            var areasContenidas = CalculadoraDeAreas.CalcularAreas(areaContenedora, _importes);

            pnlControles.Controls.Clear();
            foreach (CalculadoraDeAreas.Area area in areasContenidas.Keys)
            {
                var ctrl = new Button();
                var txt = areasContenidas[area].Categoria + "\n" + areasContenidas[area].Importe.ToString() + " €"; ;
                ctrl.Location = new System.Drawing.Point(area.x, area.y);
                ctrl.Size = new System.Drawing.Size(area.w, area.h);
                ctrl.Text = txt;
                ctrl.UseVisualStyleBackColor = true;
                ctrl.FlatStyle = FlatStyle.Flat;
                ctrl.FlatAppearance.BorderColor = Color.LightGray;
                
                // Por si no se ve el text añadimos un tooltip
                var tipCtrl = new ToolTip();
                tipCtrl.AutoPopDelay = 5000;
                tipCtrl.InitialDelay = 1000;
                tipCtrl.ReshowDelay = 500;
                tipCtrl.ShowAlways = true;
                tipCtrl.SetToolTip(ctrl, "#"+areasContenidas[area].CategoriaId+ "\n" + txt);

                // El indispensable click
                if (_catActiva == 0)
                    ctrl.Click += new EventHandler((s, e) =>
                    {
                        EstablecerCategoria(areasContenidas[area].CategoriaId);
                        CrearControles();
                    });


                pnlControles.Controls.Add(ctrl);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            EstablecerCategoria(0);
            CrearControles();
        }


    }
}