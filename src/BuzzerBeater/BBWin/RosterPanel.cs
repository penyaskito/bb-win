using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using BBLogic;

namespace BBWin
{
    public partial class RosterPanel : UserControl
    {
        private RosterManager rosterManager = null;

        public RosterPanel()
        {
            InitializeComponent();
            dgvPlayers.AutoGenerateColumns = false;
        }

        public void Init()
        {
            RosterManager.RosterDiff r = rosterManager.GetCurrentDataWithDiff();
            ShowData(r);
        }

        private void ShowData(RosterManager.RosterDiff r)
        {
            dgvPlayers.DataSource = r.Players;
        }

        private BB.API _api;
        public BB.API API
        {
            get { return _api; }
            set { _api = value; rosterManager = new RosterManager() { API = _api }; }
        }

        private void dgvPlayers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((dgvPlayers.Rows[e.RowIndex].DataBoundItem != null) &&
                 (dgvPlayers.Columns[e.ColumnIndex].DataPropertyName.Contains(".")))
            {
                e.Value = BindProperty(
                            dgvPlayers.Rows[e.RowIndex].DataBoundItem,
                            dgvPlayers.Columns[e.ColumnIndex].DataPropertyName
                          );
            }
        }

        private string BindProperty(object property, string propertyName)
        {
            string retValue = string.Empty;

            if (propertyName.Contains("."))
            {
                PropertyInfo[] arrayProperties;
                string leftPropertyName;

                leftPropertyName = propertyName.Substring(0, propertyName.IndexOf("."));
                arrayProperties = property.GetType().GetProperties();

                foreach (PropertyInfo propertyInfo in arrayProperties)
                {
                    if (propertyInfo.Name == leftPropertyName)
                    {
                        retValue = BindProperty(
                          propertyInfo.GetValue(property, null),
                          propertyName.Substring(propertyName.IndexOf(".") + 1));
                        break;
                    }
                }
            }
            else
            {
                Type propertyType;
                PropertyInfo propertyInfo;

                propertyType = property.GetType();
                propertyInfo = propertyType.GetProperty(propertyName);
                retValue = propertyInfo.GetValue(property, null).ToString();
            }

            return retValue;
        }



    }
}
