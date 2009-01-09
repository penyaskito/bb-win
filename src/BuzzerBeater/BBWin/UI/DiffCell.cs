using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Reflection;
using System.IO;

namespace BBWin.UI
{

    public class DiffColumn : DataGridViewColumn
    {
        public DiffColumn()
            : base(new DiffCell())
        {
            Width = 35;
        }
    }

    public class DiffCell : DataGridViewTextBoxCell
    {
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            object baseFormattedValue = base.GetFormattedValue(value, rowIndex, ref cellStyle,
                valueTypeConverter, formattedValueTypeConverter, context);
            int diff = Convert.ToInt32(baseFormattedValue);
            string result = "=";
            if (diff > 0)
            {
                result = string.Format("+{0}", diff);
                cellStyle.BackColor = Color.GreenYellow;
            }
            else if (diff < 0)
            {
                result = string.Format("{0}", diff);
                cellStyle.BackColor = Color.PaleVioletRed;
            }
            return result;
        }
    }
}
