﻿
using PointOfSaleSedek._102_MaterialSkin;
using System;

using System.Linq;
 
 
using System.Windows.Forms;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmSuperMarketDiscount : DevExpress.XtraEditors.XtraForm
    {

        public decimal lblFinalAmount { get; set; }
        public frmSuperMarketDiscount()
        {
            InitializeComponent();
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