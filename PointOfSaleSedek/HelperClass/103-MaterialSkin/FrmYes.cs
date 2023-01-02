using PointOfSaleSedek.HelperClass;
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
    public partial class FrmYes : MaterialSkin.Controls.MaterialForm
    {
        public string Messages { get; set; }
        readonly Static st = new Static();
        public FrmYes()
        {
            InitializeComponent();
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

            Message.TextAlign = st.isEnglish() ? ContentAlignment.TopLeft : ContentAlignment.TopRight;
            this.Text = st.isEnglish() ? "Confirm" : "تاكيد";
            BtnOk.Text = st.isEnglish() ? "Close" : "اغلاق";
           

        }
        void FillLabal()
        {
 
            Message.Text = Messages;
        }
        

        private void FrmYes_Load(object sender, EventArgs e)
        {
            FillLabal();
        }
    }
}
