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
    public enum DiffImage
    {
        More,
        Equal,
        Less
    }


    public class DiffColumn : DataGridViewColumn
    {
        private DiffImage m_DefaultStatus = DiffImage.Equal;

        public DiffImage DefaultStatus
        {
            get { return m_DefaultStatus; }
            set { m_DefaultStatus = value; }
        }


        public DiffColumn()
            : base(new DiffCell())
        {
            Width = 30;
        }
    }

    public class DiffCell : DataGridViewTextBoxCell
    {
        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle,
            TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            //Image resource = null;
            //DiffImage status = DiffImage.Equal;
            //// Try to get the default value from the containing column
            //DiffColumn owningCol = OwningColumn as DiffColumn;
            //if (owningCol != null)
            //{
            //    status = owningCol.DefaultStatus;
            //}
            //if (value is DiffImage || value is int)
            //{
            //    status = (DiffImage)value;
            //}
            //switch (status)
            //{
            //    case DiffImage.More:
            //        resource = BBWin.Properties.Resources.more;
            //        break;
            //    case DiffImage.Equal:
            //        resource = BBWin.Properties.Resources.equal;
            //        break;
            //    case DiffImage.Less:
            //        resource = BBWin.Properties.Resources.less;
            //        break;
            //    default:
            //        break;
            //}
            //cellStyle.Alignment =
            //   DataGridViewContentAlignment.TopCenter;
            //return resource;

            /*
            DiffColumn owningCol = OwningColumn as DiffColumn;
            String result = string.Empty;

            int valor = 0;
            if (value !=null && value is int)
            {
                valor = (int)value;
            }
            if (valor == 0)
            {
                result = "=";
            }
            else if (valor > 0)
            {
                result = string.Format("+{0}", valor);
            }
            else if (valor < 0)
            {
                result = string.Format("-{0}", valor);
            }
            return result;
             */
            return base.GetFormattedValue(value, rowIndex, ref cellStyle,
             valueTypeConverter,
             formattedValueTypeConverter, context);
        }
    }
}
