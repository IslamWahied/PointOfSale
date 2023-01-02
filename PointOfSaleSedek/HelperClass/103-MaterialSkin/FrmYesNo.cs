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
    public partial class FrmYesNo : MaterialSkin.Controls.MaterialForm
    {
        public string Message { get; set; }
        readonly Static st = new Static();
        public FrmYesNo()
        {
            InitializeComponent();
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
           
            this.LblMessage.TextAlign = st.isEnglish() ? ContentAlignment.TopLeft : ContentAlignment.TopRight;
            this.Text = st.isEnglish() ? "Message Confirmation" : "رسالة تاكيد";
            this.btnOk.Text = st.isEnglish() ? "Ok" : "تاكيد";
            this.btnClose.Text = st.isEnglish() ? "Close" : "اغلاق";

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
