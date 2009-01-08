using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BB;

namespace BBWin
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }

        private API _api;

        public API API
        {
            get
            {
                return _api;
            }
            set
            {
                _api = value;
                ShowVistaGeneral();
            }
        }

        private void btnVistaGeneral_Click(object sender, EventArgs e)
        {
            ShowVistaGeneral();
        }

        private void ShowVistaGeneral()
        {
            ShowAndHidePanels(pnlVistaGeneral);

            pnlVistaGeneral.API = API;
            pnlVistaGeneral.Init();
        }

        private void btnArena_Click(object sender, EventArgs e)
        {
            ShowArena();
        }

        private void ShowArena()
        {
            ShowAndHidePanels(pnlArena);

            pnlArena.API = API;
            pnlArena.Init();
        }

        private void ShowAndHidePanels(UserControl toShow)
        {
            foreach (Control c in splitContainer1.Panel2.Controls)
            {
                if (c != toShow)
                {
                    c.Visible = false;
                }
                else
                {
                    c.Visible = true;
                }
            }
        }

        private void btnRoster_Click(object sender, EventArgs e)
        {
            ShowRoster();
        }

        private void ShowRoster()
        {
            ShowAndHidePanels(pnlRoster);

            pnlRoster.API = API;
            pnlRoster.Init();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

    }
}
