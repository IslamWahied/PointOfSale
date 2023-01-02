
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using System;

using System.Linq;
 
 
using System.Windows.Forms;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmSuperMarketDiscount : DevExpress.XtraEditors.XtraForm
    {

        public decimal lblFinalAmount { get; set; }
        readonly Static st = new Static();
        public frmSuperMarketDiscount()
        {
            InitializeComponent();
            langu();
        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Add Discount" : "اضافة خصم";
            labelControl1.Text = st.isEnglish() ? "Discount value" : "قيمة الخصم";


            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                decimal DisCount = Convert.ToDecimal(txtDiscount.Text);
                decimal FinalAmount = Convert.ToDecimal(this.lblFinalAmount);

                if (DisCount <= FinalAmount)
                {

                    if (Application.OpenForms.OfType<frmSuperMarketSales>().Any())
                    {
                        frmSuperMarketSales frm = (frmSuperMarketSales)Application.OpenForms["frmSuperMarketSales"];
                        frm.lblDiscount.Text = DisCount.ToString();
                        frm.lblFinalTotal.Text = (FinalAmount - DisCount).ToString();
                        this.Close();
                    }

                }
                else
                {

                    MaterialMessageBox.Show("قيمة الخصم اكبر من قيمة الفاتورة", MessageBoxButtons.OK);
                    return;

                }
            }
        }
    }
}