using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PointOfSaleSedek._102_MaterialSkin
{
    public static class MaterialMessageBox
    {
        public static DialogResult Show(string message, MessageBoxButtons button)
        {
            DialogResult result = DialogResult.None;
            switch (button)
            {
                case MessageBoxButtons.YesNo:
                    using (FrmYesNo yesNo = new FrmYesNo())
                    {
                        
                        yesNo.Message = message;
                        result = yesNo.ShowDialog();
                    }
                    break;
                case MessageBoxButtons.OK:
                    using (FrmYes ok = new FrmYes())
                    {
                        
                        ok.Messages = message;
                        result = ok.ShowDialog();
                    }
                    break;
            }
            return result;

            
        }

    }
}
