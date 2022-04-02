namespace FinTech.UI.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuPrincipal = new System.Windows.Forms.MenuStrip();
            this.apuntesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categoriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlApuntes = new System.Windows.Forms.Panel();
            this.pnlCategorias = new System.Windows.Forms.Panel();
            this.pnlImportes = new System.Windows.Forms.Panel();
            this.pnlControles = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.menuPrincipal.SuspendLayout();
            this.pnlImportes.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apuntesToolStripMenuItem,
            this.categoriasToolStripMenuItem,
            this.importesToolStripMenuItem});
            this.menuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.menuPrincipal.Name = "menuPrincipal";
            this.menuPrincipal.Size = new System.Drawing.Size(800, 24);
            this.menuPrincipal.TabIndex = 0;
            this.menuPrincipal.Text = "menuStrip1";
            // 
            // apuntesToolStripMenuItem
            // 
            this.apuntesToolStripMenuItem.Name = "apuntesToolStripMenuItem";
            this.apuntesToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.apuntesToolStripMenuItem.Text = "Apuntes";
            this.apuntesToolStripMenuItem.Click += new System.EventHandler(this.apuntesToolStripMenuItem_Click);
            // 
            // categoriasToolStripMenuItem
            // 
            this.categoriasToolStripMenuItem.Name = "categoriasToolStripMenuItem";
            this.categoriasToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.categoriasToolStripMenuItem.Text = "Categorias";
            this.categoriasToolStripMenuItem.Click += new System.EventHandler(this.categoriasToolStripMenuItem_Click);
            // 
            // importesToolStripMenuItem
            // 
            this.importesToolStripMenuItem.Name = "importesToolStripMenuItem";
            this.importesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.importesToolStripMenuItem.Text = "Importes";
            this.importesToolStripMenuItem.Click += new System.EventHandler(this.importesToolStripMenuItem_Click);
            // 
            // pnlApuntes
            // 
            this.pnlApuntes.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlApuntes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlApuntes.Location = new System.Drawing.Point(0, 24);
            this.pnlApuntes.Name = "pnlApuntes";
            this.pnlApuntes.Size = new System.Drawing.Size(800, 426);
            this.pnlApuntes.TabIndex = 1;
            // 
            // pnlCategorias
            // 
            this.pnlCategorias.BackColor = System.Drawing.SystemColors.HotTrack;
            this.pnlCategorias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCategorias.Location = new System.Drawing.Point(0, 24);
            this.pnlCategorias.Name = "pnlCategorias";
            this.pnlCategorias.Size = new System.Drawing.Size(800, 426);
            this.pnlCategorias.TabIndex = 2;
            // 
            // pnlImportes
            // 
            this.pnlImportes.BackColor = System.Drawing.SystemColors.Window;
            this.pnlImportes.Controls.Add(this.pnlControles);
            this.pnlImportes.Controls.Add(this.btnBack);
            this.pnlImportes.Controls.Add(this.lblTotal);
            this.pnlImportes.Controls.Add(this.lblCategoria);
            this.pnlImportes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImportes.Location = new System.Drawing.Point(0, 24);
            this.pnlImportes.Name = "pnlImportes";
            this.pnlImportes.Size = new System.Drawing.Size(800, 426);
            this.pnlImportes.TabIndex = 3;
            // 
            // pnlControles
            // 
            this.pnlControles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlControles.BackColor = System.Drawing.SystemColors.Window;
            this.pnlControles.Location = new System.Drawing.Point(12, 66);
            this.pnlControles.Name = "pnlControles";
            this.pnlControles.Size = new System.Drawing.Size(774, 348);
            this.pnlControles.TabIndex = 4;
            this.pnlControles.ClientSizeChanged += new System.EventHandler(this.pnlControles_ClientSizeChanged);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(711, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "< Resumen";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.Location = new System.Drawing.Point(218, 21);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(0, 21);
            this.lblTotal.TabIndex = 2;
            // 
            // lblCategoria
            // 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCategoria.Location = new System.Drawing.Point(17, 21);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(0, 21);
            this.lblCategoria.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlImportes);
            this.Controls.Add(this.pnlCategorias);
            this.Controls.Add(this.pnlApuntes);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "MainForm";
            this.Text = "FinTech";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.pnlImportes.ResumeLayout(false);
            this.pnlImportes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuPrincipal;
        private ToolStripMenuItem apuntesToolStripMenuItem;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripMenuItem importesToolStripMenuItem;
        private Panel pnlApuntes;
        private Panel pnlCategorias;
        private Panel pnlImportes;
        private Button btnBack;
        private Label lblTotal;
        private Label lblCategoria;
        private Panel pnlControles;
    }
}