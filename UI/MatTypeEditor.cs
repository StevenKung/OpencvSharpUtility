using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using OpenCvSharp;

namespace OpencvSharpUtility.UI
{
    class MatTypeEditor : UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            UserControlPicBox control = new UserControlPicBox
            {
                SrcImg = value as Mat
            };

            Form form = new Form
            {
                AutoSize = true,

            };
            form.Controls.Add(control);
            form.ShowDialog();
            control.Dispose();
            form.Dispose();
            return value; // can also replace the wrapper object here
        }
    }


    class UpDownValueEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            NumericUpDown udControl = new NumericUpDown();
            udControl.Value = Convert.ToDecimal(value);
            editorService.DropDownControl(udControl);
            value = Convert.ChangeType(udControl.Value, value.GetType());
            udControl.Dispose();
            return value;
        }
    }


}
