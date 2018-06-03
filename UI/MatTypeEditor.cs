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
    class MatTypeEditor :UITypeEditor
    {

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            

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
            return value; // can also replace the wrapper object here
        }


    }
}
