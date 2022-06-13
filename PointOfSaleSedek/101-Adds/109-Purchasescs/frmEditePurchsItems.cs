using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PointOfSaleSedek._102_MaterialSkin;
using EntityData;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmEditePurchsItems : DevExpress.XtraEditors.XtraForm
    {
        public Int64 ItemCode;
        PointOfSaleEntities context = new PointOfSaleEntities();
        public frmEditePurchsItems()
        {
            InitializeComponent();
            txtBoxNumber.Enabled = false;
            txtUnitForBoxNumber.Enabled = false;
           
        }

        private void chkUnits_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnits.Checked == true)
            {

                chkBox.Checked = false;
                txtBoxNumber.ResetText();
                txtUnitForBoxNumber.ResetText();
                txtBoxNumber.Enabled = false;
                txtUnitForBoxNumber.Enabled = false;

                txtFinalUnitsNumber.Enabled = true;
                txtFinalUnitsNumber.Text = Convert.ToString(1);
            }
            else
            {

                chkBox.Checked = true;
            }
        }

        private void chkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBox.Checked == true)
            {

                chkUnits.Checked = false;
                txtBoxNumber.Enabled = true;
                txtUnitForBoxNumber.Enabled = true;
                txtFinalUnitsNumber.Enabled = false;
                txtFinalUnitsNumber.ResetText();
            }
            else
            {
                
                chkUnits.Checked = true;
            }
        }

        private void txtBoxNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBox.Checked == true)
                {
                    if (string.IsNullOrWhiteSpace(txtUnitForBoxNumber.Text))
                    {

                        txtUnitForBoxNumber.Text = Convert.ToString(1);


                    }
                    if (string.IsNullOrWhiteSpace(txtBoxNumber.Text))
                    {

                        txtBoxNumber.Text = Convert.ToString(1);


                    }
                    txtFinalUnitsNumber.Text = Convert.ToString((Convert.ToDouble(txtBoxNumber.Text)) * (Convert.ToDouble(txtUnitForBoxNumber.Text)));
                }

            }
            catch
            {
                txtBoxNumber.Text = 1.ToString();
                txtUnitForBoxNumber.Text = 1.ToString();
                txtFinalUnitsNumber.Text = 1.ToString();

            }
        }

        private void txtUnitForBoxNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkBox.Checked == true)
                {
                    if (string.IsNullOrWhiteSpace(txtUnitForBoxNumber.Text))
                    {

                        txtUnitForBoxNumber.Text = Convert.ToString(1);


                    }
                    if (string.IsNullOrWhiteSpace(txtBoxNumber.Text))
                    {

                        txtBoxNumber.Text = Convert.ToString(1);


                    }
                    txtFinalUnitsNumber.Text = Convert.ToString((Convert.ToDouble(txtBoxNumber.Text)) * (Convert.ToDouble(txtUnitForBoxNumber.Text)));

                }
            }
            catch
            {
                txtBoxNumber.Text = 1.ToString();
                txtUnitForBoxNumber.Text = 1.ToString();
                txtFinalUnitsNumber.Text = 1.ToString();
            }
        }

        private void txtFinalUnitsNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (chkUnits.Checked == true)
            {

                if (string.IsNullOrWhiteSpace(txtFinalUnitsNumber.Text))
                {


                    txtFinalUnitsNumber.Text = Convert.ToString(1);

                }


            }
        }



        void AddItem()
        {


            if (string.IsNullOrWhiteSpace(txtFinalUnitsNumber.Text) || string.IsNullOrWhiteSpace(txtCatgoryName.Text) || string.IsNullOrWhiteSpace(txtUnit.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtParCode.Text))
            {


                MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
                return;


            }


            bool TestItemName = context.ItemCards.Any(Item => Item.Name == txtName.Text || Item.ParCode == txtParCode.Text);



            if (Application.OpenForms.OfType<frmPurchasescs>().Any())
            {
                frmPurchasescs frm = (frmPurchasescs)Application.OpenForms["frmPurchasescs"];
                frm.gvItemCard.SetFocusedRowCellValue("CategoryName", txtCatgoryName.Text);
                frm.gvItemCard.SetFocusedRowCellValue("ParCode", txtParCode.Text);
                frm.gvItemCard.SetFocusedRowCellValue("Name", txtName.Text);
                frm.gvItemCard.SetFocusedRowCellValue("UnitName", txtUnit.Text);
                frm.gvItemCard.SetFocusedRowCellValue("PriceBuy",txtPriceBuy.Text);
                frm.gvItemCard.SetFocusedRowCellValue("Price", txtPrice.Text);
                frm.gvItemCard.SetFocusedRowCellValue("Qty", txtFinalUnitsNumber.Text);
                frm.gvItemCard.SetFocusedRowCellValue("Total", Convert.ToDouble(txtPriceBuy.Text)* Convert.ToDouble(txtFinalUnitsNumber.Text));
                frm.gvItemCard.RefreshData();
                HelperClass.HelperClass.ClearValues(tableLayoutPanel88);
                this.Close();
            }

        }




        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void frmEditePurchsItems_Load(object sender, EventArgs e)
        {
            txtPriceBuy.Enabled = true;
            txtPrice.Enabled = true;
            
        }
    }
}