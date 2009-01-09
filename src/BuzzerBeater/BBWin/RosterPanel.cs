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

        public RosterManager.RosterDiff Roster { get; set; }

        public RosterPanel()
        {
            InitializeComponent();
            dgvPlayers.AutoGenerateColumns = false;
        }

        public void Init()
        {
            Roster = rosterManager.GetCurrentDataWithDiff();
            ShowData(Roster);
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
            object o = GetProperty(property, propertyName);
            if (o != null)
            {
                retValue = o.ToString();
            }
            return retValue;
        }

        private object GetProperty(object property, string propertyName)
        {
            object o = null;
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
                        o = GetProperty(
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
                o = propertyInfo.GetValue(property, null);
            }
            return o;
        }

        private void dgvPlayers_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortOrder order = dgvPlayers.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
            int orderFactor = (order == SortOrder.Ascending) ? -1 : 1;

            dgvPlayers.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection =
                (orderFactor == 1) ? SortOrder.Ascending : SortOrder.Descending;

            Roster.Players.Sort(delegate(RosterManager.PlayerDiff x, RosterManager.PlayerDiff y)
            {
                string propertyName = dgvPlayers.Columns[e.ColumnIndex].DataPropertyName;
                object valueOfX = GetProperty(x, propertyName);
                object valueOfY = GetProperty(y, propertyName);

                if (valueOfX == null && valueOfY == null)
                {
                    return 0 * orderFactor;
                }
                if (valueOfX == null && valueOfY != null)
                {
                    return 1 * orderFactor;
                }
                if (valueOfX != null && valueOfY == null)
                {
                    return -1 * orderFactor;
                }
                int result = orderFactor * ((IComparable)valueOfX).CompareTo(valueOfY);
                return result;
            });

            ShowData(Roster);
            dgvPlayers.Invalidate();
        }
    }
}
