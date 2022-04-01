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
            this.verToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlApuntes = new System.Windows.Forms.Panel();
            this.pnlCategorias = new System.Windows.Forms.Panel();
            this.pnlVer = new System.Windows.Forms.Panel();
            this.pnlBotones = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.menuPrincipal.SuspendLayout();
            this.pnlVer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPrincipal
            // 
            this.menuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.apuntesToolStripMenuItem,
            this.categoriasToolStripMenuItem,
            this.verToolStripMenuItem});
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
            // verToolStripMenuItem
            // 
            this.verToolStripMenuItem.Name = "verToolStripMenuItem";
            this.verToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.verToolStripMenuItem.Text = "Ver";
            this.verToolStripMenuItem.Click += new System.EventHandler(this.verToolStripMenuItem_Click);
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
            // pnlVer
            // 
            this.pnlVer.BackColor = System.Drawing.SystemColors.Window;
            this.pnlVer.Controls.Add(this.pnlBotones);
            this.pnlVer.Controls.Add(this.btnBack);
            this.pnlVer.Controls.Add(this.lblTotal);
            this.pnlVer.Controls.Add(this.lblCategoria);
            this.pnlVer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVer.Location = new System.Drawing.Point(0, 24);
            this.pnlVer.Name = "pnlVer";
            this.pnlVer.Size = new System.Drawing.Size(800, 426);
            this.pnlVer.TabIndex = 3;
            // 
            // pnlBotones
            // 
            this.pnlBotones.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBotones.BackColor = System.Drawing.SystemColors.Window;
            this.pnlBotones.Location = new System.Drawing.Point(12, 66);
            this.pnlBotones.Name = "pnlBotones";
            this.pnlBotones.Size = new System.Drawing.Size(774, 348);
            this.pnlBotones.TabIndex = 4;
            this.pnlBotones.ClientSizeChanged += new System.EventHandler(this.pnlBotones_ClientSizeChanged);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Location = new System.Drawing.Point(711, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "< Principal";
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
            this.Controls.Add(this.pnlVer);
            this.Controls.Add(this.pnlCategorias);
            this.Controls.Add(this.pnlApuntes);
            this.Controls.Add(this.menuPrincipal);
            this.MainMenuStrip = this.menuPrincipal;
            this.Name = "MainForm";
            this.Text = "FinTech";
            this.menuPrincipal.ResumeLayout(false);
            this.menuPrincipal.PerformLayout();
            this.pnlVer.ResumeLayout(false);
            this.pnlVer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuPrincipal;
        private ToolStripMenuItem apuntesToolStripMenuItem;
        private ToolStripMenuItem categoriasToolStripMenuItem;
        private ToolStripMenuItem verToolStripMenuItem;
        private Panel pnlApuntes;
        private Panel pnlCategorias;
        private Panel pnlVer;
        private Button btnBack;
        private Label lblTotal;
        private Label lblCategoria;
        private Panel pnlBotones;
    }
}