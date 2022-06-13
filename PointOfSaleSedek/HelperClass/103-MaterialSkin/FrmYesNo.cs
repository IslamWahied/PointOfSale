using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSaleSedek._102_MaterialSkin
{
    public partial class FrmYesNo : MaterialSkin.Controls.MaterialForm
    {
        public string Message { get; set; }
        public FrmYesNo()
        {
            InitializeComponent();
           
        }
        void FillLabal()
        {
            LblMessage.Text = Message;
        }

        private void FrmYesNo_Load(object sender, EventArgs e)
        {
            FillLabal();
        }
    }
}
