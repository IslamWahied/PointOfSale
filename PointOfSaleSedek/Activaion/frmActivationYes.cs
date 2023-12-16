using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSaleSedek._Activaion
{
    public partial class frmActivationYes : MaterialSkin.Controls.MaterialForm
    {
        public string message { get; set; }
        public frmActivationYes()
        {
            InitializeComponent();
             
        }
        void FillLabal()
        {
            Message.Text = message;
        }
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

        }

        private void frmYes_Load(object sender, EventArgs e)
        {
            FillLabal();
        }
    }
}
