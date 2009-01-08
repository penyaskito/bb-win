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
    public partial class VistaGeneralPanel : UserControl
    {
        public VistaGeneralPanel()
        {
            InitializeComponent();
        }

        public void Init()
        {
            BB.API.Team t = API.GetTeam(null);

            ShowData(t);
        }

        private void ShowData(BB.API.Team t)
        {
            lblUsername.Text = t.UserName;
            lblTeamName.Text = t.TeamName;
            lblShortName.Text = t.ShortName;
            lblLeague.Text = t.League.ToString();
            lblCountry.Text = t.Country.ToString();
        }

        public BB.API API { get; set; }


    }
}
