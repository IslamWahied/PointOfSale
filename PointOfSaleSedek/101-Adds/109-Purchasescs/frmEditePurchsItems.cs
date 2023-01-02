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
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmEditePurchsItems : DevExpress.XtraEditors.XtraForm
    {
        public Int64 ItemCode;
        POSEntity context = new POSEntity();
        Static st = new Static();
        public frmEditePurchsItems()
        {
            InitializeComponent();
            langu();
            FillslkWarhouse();
            txtBoxNumber.Enabled = false;
            txtUnitForBoxNumber.Enabled = false;
           
        }


        public void FillslkWarhouse()
        {
            DataTable dt = new DataTable();
            var result = context.warhouse_view.Where(user => user.isDelete == 0 && user.IsDeleted == 0).ToList();
            slkWarhouse.Properties.DataSource = result;
            slkWarhouse.Properties.ValueMember = "Warehouse_Code";
            slkWarhouse.Properties.DisplayMember = "Warehouse_Name";
            slkWarhouse.EditValue = 0;


        }
        void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel88.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Modify Item" : "تعديل صنف بالفاتورة";
            labelControl5.Text = st.isEnglish() ? "Items" : "المنتج";
            labelControl1.Text = st.isEnglish() ? "Category" : "المجموعة";
            labelControl2.Text = st.isEnglish() ? "Item Name" : "اسم الصنف";
            labelControl3.Text = st.isEnglish() ? "Unit" : "وحدة القياس";
            labelControl7.Text = st.isEnglish() ? "Purchasing Price" : "سعر الشراء";
            labelControl4.Text = st.isEnglish() ? "Selling Price" : "سعر البيع";
            labelControl10.Text = st.isEnglish() ? "Unit" : "بالوحده";
            labelControl11.Text = st.isEnglish() ? "Box" : "بالكرتونة";
            labelControl8.Text = st.isEnglish() ? "Count" : "العدد";
            labelControl6.Text = st.isEnglish() ? "Unit Count" : "الوحدات";
            labelControl9.Text = st.isEnglish() ? "Total" : "الاجمالي";
            labelControl12.Text = st.isEnglish() ? "Warhouse" : "المخزن";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";

            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";






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


                MaterialMessageBox.Show(st.isEnglish()?"There are empty fields":"يوجد حقول فارغة", MessageBoxButtons.OK);
                return;


            }

            else if (string.IsNullOrWhiteSpace(slkWarhouse.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select a Warhouse" : "برجاء اختيار مخزن", MessageBoxButtons.OK);
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
                frm.gvItemCard.SetFocusedRowCellValue("Warehouse_Name", slkWarhouse.Text);
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