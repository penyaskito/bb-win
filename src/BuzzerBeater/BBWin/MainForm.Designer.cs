namespace BBWin
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnArena = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlVistaGeneral = new BBWin.VistaGeneralPanel();
            this.pnlRoster = new BBWin.RosterPanel();
            this.pnlArena = new BBWin.ArenaPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRoster = new System.Windows.Forms.Button();
            this.btnVistaGeneral = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.btnRoster);
            this.splitContainer1.Panel1.Controls.Add(this.btnArena);
            this.splitContainer1.Panel1.Controls.Add(this.btnVistaGeneral);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlVistaGeneral);
            this.splitContainer1.Panel2.Controls.Add(this.pnlRoster);
            this.splitContainer1.Panel2.Controls.Add(this.pnlArena);
            this.splitContainer1.Size = new System.Drawing.Size(1005, 671);
            this.splitContainer1.SplitterDistance = 205;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // btnArena
            // 
            this.btnArena.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnArena.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnArena.Image = global::BBWin.Properties.Resources.icon_arena;
            this.btnArena.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArena.Location = new System.Drawing.Point(0, 80);
            this.btnArena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnArena.Name = "btnArena";
            this.btnArena.Size = new System.Drawing.Size(205, 80);
            this.btnArena.TabIndex = 1;
            this.btnArena.Text = "Pabellón";
            this.btnArena.UseVisualStyleBackColor = true;
            this.btnArena.Click += new System.EventHandler(this.btnArena_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1005, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // pnlVistaGeneral
            // 
            this.pnlVistaGeneral.API = null;
            this.pnlVistaGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVistaGeneral.Location = new System.Drawing.Point(0, 0);
            this.pnlVistaGeneral.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlVistaGeneral.Name = "pnlVistaGeneral";
            this.pnlVistaGeneral.Size = new System.Drawing.Size(795, 671);
            this.pnlVistaGeneral.TabIndex = 2;
            // 
            // pnlRoster
            // 
            this.pnlRoster.API = null;
            this.pnlRoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRoster.Location = new System.Drawing.Point(0, 0);
            this.pnlRoster.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlRoster.Name = "pnlRoster";
            this.pnlRoster.Size = new System.Drawing.Size(795, 671);
            this.pnlRoster.TabIndex = 1;
            this.pnlRoster.Visible = false;
            // 
            // pnlArena
            // 
            this.pnlArena.API = null;
            this.pnlArena.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlArena.Location = new System.Drawing.Point(0, 0);
            this.pnlArena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlArena.Name = "pnlArena";
            this.pnlArena.Size = new System.Drawing.Size(795, 671);
            this.pnlArena.TabIndex = 0;
            this.pnlArena.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureBox1.Image = global::BBWin.Properties.Resources.number_ball;
            this.pictureBox1.Location = new System.Drawing.Point(0, 569);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(205, 102);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnRoster
            // 
            this.btnRoster.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRoster.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnRoster.Image = global::BBWin.Properties.Resources.icon_roster;
            this.btnRoster.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoster.Location = new System.Drawing.Point(0, 160);
            this.btnRoster.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRoster.Name = "btnRoster";
            this.btnRoster.Size = new System.Drawing.Size(205, 80);
            this.btnRoster.TabIndex = 2;
            this.btnRoster.Text = "Plantilla";
            this.btnRoster.UseVisualStyleBackColor = true;
            this.btnRoster.Click += new System.EventHandler(this.btnRoster_Click);
            // 
            // btnVistaGeneral
            // 
            this.btnVistaGeneral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnVistaGeneral.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVistaGeneral.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnVistaGeneral.Image = global::BBWin.Properties.Resources.ww;
            this.btnVistaGeneral.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVistaGeneral.Location = new System.Drawing.Point(0, 0);
            this.btnVistaGeneral.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnVistaGeneral.Name = "btnVistaGeneral";
            this.btnVistaGeneral.Size = new System.Drawing.Size(205, 80);
            this.btnVistaGeneral.TabIndex = 0;
            this.btnVistaGeneral.Text = "Vista General";
            this.btnVistaGeneral.UseVisualStyleBackColor = false;
            this.btnVistaGeneral.Click += new System.EventHandler(this.btnVistaGeneral_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 695);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "BBWin";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;        
        private System.Windows.Forms.Button btnRoster;
        private System.Windows.Forms.Button btnArena;
        private System.Windows.Forms.Button btnVistaGeneral;
        private ArenaPanel pnlArena;
        private RosterPanel pnlRoster;
        private VistaGeneralPanel pnlVistaGeneral;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;

    }
}

