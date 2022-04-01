using FinTech.Models;

namespace FinTech.UI.WinForms
{
    public partial class MainForm : Form
    {
        private Sistema _sistema;

        private List<Categoria> _categorias;

        private List<ImporteCategoria> _importes;

        private int _catActiva;

        /*
        public event EventHandler<CategoriaNuevaEventArgs> CategoriaNueva;

        public class CategoriaNuevaEventArgs : EventArgs
        {
            public int CategoriaId { get; set; }
        }
        protected virtual void OnCategoriaNueva(CategoriaNuevaEventArgs e)
        {
            EventHandler<CategoriaNuevaEventArgs> handler = CategoriaNueva;
            if (handler != null)
            {
                handler(this, e);
                EstablecerCategoria(e.CategoriaId);
                Repintar();
            }
        }
        */
        public MainForm(Sistema sistema)
        {
            _sistema = sistema;

            InitializeComponent();
            pnlApuntes.Visible = false;
            pnlCategorias.Visible = false;
            pnlVer.Visible = true;

           
            _categorias = _sistema.QryCategorias();

            /*
            var args = new CategoriaNuevaEventArgs() { CategoriaId = 0 };
            OnCategoriaNueva(args);
            */
            EstablecerCategoria(0);
            Repintar();
        }

        // MENU
        private void apuntesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlApuntes.Visible = true;
            pnlCategorias.Visible = false;
            pnlVer.Visible = false;
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlApuntes.Visible = false;
            pnlCategorias.Visible = true;
            pnlVer.Visible = false;
        }

        private void verToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pnlApuntes.Visible = false;
            pnlCategorias.Visible = false;
            pnlVer.Visible = true;
        }

        // RESIZE
        private void pnlBotones_ClientSizeChanged(object sender, EventArgs e) { 
            if (pnlVer.Visible == true) 
                Repintar(); 
        }

        // PINTAMOS
        private void EstablecerCategoria(int catId)
        {
            btnBack.Visible = catId != 0;
            _catActiva = catId;
            _importes = _sistema.QryImporteApuntes(catId);
            var total = _importes.Sum(imp => imp.Importe);

            lblCategoria.Text = catId==0? "Total":_categorias.FirstOrDefault(cat => cat.Id == catId).Descripcion;
            lblTotal.Text = total.ToString();

        }
        private void Repintar()
        {
            var panel = new CalculadoraDeAreas.Area(0,0, pnlBotones.Width, pnlBotones.Height);
            var areas = CalculadoraDeAreas.calcularAreas(panel, _importes);

            pnlBotones.Controls.Clear();
            foreach( CalculadoraDeAreas.Area area in areas.Keys)
            {
                var btn = new Button();
                var txt = areas[area].Categoria + "\n" + areas[area].Importe.ToString();
                btn.Location = new System.Drawing.Point(area.x, area.y);
                btn.Size = new System.Drawing.Size(area.w, area.h);
                btn.UseVisualStyleBackColor = true;
                btn.Text = txt;

                // Por si no se ve el text añadimos un tooltip
                var toolTip1 = new ToolTip();
                //toolTip1.AutoPopDelay = 5000;
                //toolTip1.InitialDelay = 1000;
                //toolTip1.ReshowDelay = 500;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(btn, txt);

                // El indispensable click
                if(_catActiva==0)
                btn.Click += new EventHandler((s, e)=>{
                    EstablecerCategoria(areas[area].CategoriaId);
                    Repintar();
                });


                pnlBotones.Controls.Add(btn);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            EstablecerCategoria(0);
            Repintar();
        }
    }
}