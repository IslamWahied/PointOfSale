using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace   PointOfSaleSedek._Activaion
{
    public partial class frmActivaionYesNo : MaterialSkin.Controls.MaterialForm
    {
        public string message { get; set; }
        public frmActivaionYesNo()
        {
            InitializeComponent();
           
        }
        void FillLabal()
        {
            lblMessage.Text = message;
        }

        private void frmYesNo_Load(object sender, EventArgs e)
        {
            FillLabal();
        }
    }
}
