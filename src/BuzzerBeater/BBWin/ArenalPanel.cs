using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBWin
{
    public partial class ArenaPanel : UserControl
    {
        public ArenaPanel()
        {
            InitializeComponent();
        }

        public void Init()
        {
            BB.API.Arena a = API.GetArena(null);

            ShowData(a);
        }

        private void ShowData(BB.API.Arena a)
        {
            lblName.Text = a.Name;
            dataGridView1.DataSource = new BB.API.Seat[] { a.Bleachers, a.LowerTier, a.CourtSide, a.Luxury };
        }

        public BB.API API { get; set; }


    }
}
