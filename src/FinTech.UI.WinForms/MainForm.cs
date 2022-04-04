using FinTech.Models;
using FinTech.Tools;

namespace FinTech.UI.WinForms
{
    public partial class MainForm : Form
    {
        private Sistema _sistema;
        private List<Categoria> _categorias;


        private int _catPadre;
        private decimal _totalPadre;
        private List<GastoPorCategoria> _importes;



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
            _catPadre = catId;
            _importes = _sistema.QryImporteDeGastoPorCategoria(id: catId);
            _totalPadre = _importes.Sum(imp => imp.Importe);

            lblCategoria.Text = catId == 0 ? "Gasto total" : _categorias.FirstOrDefault(cat => cat.Id == catId).Descripcion;
            lblTotal.Text = _totalPadre.ToString() + " €";
            btnBack.Visible = catId != 0;
        }
        private void CrearControles()
        {
            var areaPanel = new CalculadoraDeSuperficies<GastoPorCategoria>.Area { x = 0, y = 0, w = pnlControles.Width, h = pnlControles.Height };
            var areasCtrl = CalculadoraDeSuperficies<GastoPorCategoria>.CalcularAreas(areaPanel, _importes);

            pnlControles.Controls.Clear();
            foreach (CalculadoraDeSuperficies<GastoPorCategoria>.Area a in areasCtrl.Keys)
            {
                var ctrl = new Button();
                var txt = string.Format("{0}\n{1}€", areasCtrl[a].Nombre, areasCtrl[a].Valor);
                ctrl.Location = new System.Drawing.Point(a.x, a.y);
                ctrl.Size = new System.Drawing.Size(a.w, a.h);
                ctrl.Text = txt;
                ctrl.UseVisualStyleBackColor = true;
                ctrl.FlatStyle = FlatStyle.Flat;
                ctrl.FlatAppearance.BorderColor = Color.LightGray;
                ctrl.BackColor = CalcularColor(_importes.Max(i => i.Importe), areasCtrl[a].Valor);

                // Por si no se ve el text añadimos un tooltip
                var tipCtrl = new ToolTip();
                tipCtrl.AutoPopDelay = 5000;
                tipCtrl.InitialDelay = 1000;
                tipCtrl.ReshowDelay = 500;
                tipCtrl.ShowAlways = true;
                tipCtrl.SetToolTip(ctrl, string.Format("#{0}\n{1}\n{2:0.#%}", areasCtrl[a].Id, txt, areasCtrl[a].Valor / _totalPadre));

                // El indispensable click
                if (_catPadre == 0)
                    ctrl.Click += new EventHandler((s, e) =>
                    {
                        EstablecerCategoria(areasCtrl[a].Id);
                        CrearControles();
                    });


                pnlControles.Controls.Add(ctrl);
            }

            Color CalcularColor(Decimal max, Decimal val)
            {
                var top = 100;

                int blue = 255 - Decimal.ToInt32(top * val / max);
                return Color.FromArgb(
                    blue * 1 / 5,
                    blue * 3 / 5,
                    255
                    );
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            EstablecerCategoria(0);
            CrearControles();
        }

    }
}