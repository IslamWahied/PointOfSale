using System;
using System.Collections.Generic;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
 
using DataRep;

using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmAddPurchsItems : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        Static st = new Static();
        public frmAddPurchsItems()
        {
            InitializeComponent();
            langu();
            txtBoxNumber.Enabled = false;
            txtUnitForBoxNumber.Enabled = false;
            FillSlkItems();
            FillslkWarhouse();
            txtFinalUnitsNumber.Text = "1";
            

        }
        void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
             tableLayoutPanel1.RightToLeft  = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Add Item" : "اضافة منتج";
            labelControl5.Text = st.isEnglish() ? "Items" : "المنتج";
            labelControl1.Text = st.isEnglish() ? "Category" : "المجموعة";
            labelControl2.Text = st.isEnglish() ? "Item Name" : "اسم الصنف";
            labelControl3.Text = st.isEnglish() ? "Unit" : "وحدة القياس";
            labelControl7.Text = st.isEnglish() ? "Purchasing Price" : "سعر الشراء";
            labelControl4.Text = st.isEnglish() ? "Selling Price" : "سعر البيع";
            labelControl10.Text = st.isEnglish() ? "Unit" : "بالوحده";
            labelControl11.Text = st.isEnglish() ? "Box" : "بالكرتونة";
            labelControl12.Text = st.isEnglish() ? "Warhouse" : "المخزن";
            labelControl8.Text = st.isEnglish() ? "Box" : "العدد";
            labelControl6.Text = st.isEnglish() ? "Unit Count" : "الوحدات";
            labelControl9.Text = st.isEnglish() ? "Total" : "الاجمالي";
            gridColumn1.Caption = st.isEnglish() ? "ParCode" : "باركود";
            gridColumn2.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";
            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text);
            //    if (item != null)
            //    {
            //        txtName.Text = item.Name;
            //        txtUnit.Text = item.UnitName;
            //        txtPrice.Text = item.Price.ToString();
            //        txtCatgoryName.Text = item.CategoryName;
            //        txtPriceBuy.Text = item.PriceBuy.ToString();
            //    }
            //    else
            //    {
            //        Rest();

            //    }

            //}
            //catch
            //{

            //}
        }


        

        public void FillslkWarhouse()
        {
            DataTable dt = new DataTable();
            var result = context.Warehouses.Where(user => user.isDelete == 0 ).ToList();
            slkWarhouse.Properties.DataSource = result;
            slkWarhouse.Properties.ValueMember = "Warehouse_Code";
            slkWarhouse.Properties.DisplayMember = "Warehouse_Name";
            slkWarhouse.EditValue = 0;

        }
        public void FillSlkItems()
        {
            DataTable dt = new DataTable();
            Int64 branchCode = st.GetBranch_Code();
            var result = context.ItemCardViews.Where(user => user.IsDeleted == 0 && user.Branch_Code == branchCode).ToList();
            slkItem.Properties.DataSource = result;
            slkItem.Properties.ValueMember = "ItemCode";
            slkItem.Properties.DisplayMember = "Name";

        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
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
            else if (chkUnits.Checked == false)
            {

                chkBox.Checked = true;


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


        private void frmAddPurchsItems_Load(object sender, EventArgs e)
        {
            slkItem.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Int64 branchCode = st.GetBranch_Code();

            try
            {
                if (true)
                {
                    Int64 ItemCode = Convert.ToInt64(slkItem.EditValue);
                    ItemCardView item1 = context.ItemCardViews.FirstOrDefault(x => x.ItemCode == ItemCode && x.Branch_Code == branchCode );

                    if (item1 != null)
                    {


                        List<SaleDetailView> DetailList = new List<SaleDetailView>();

                        if (Application.OpenForms.OfType<frmPurchasescs>().Any())
                        {

                            frmPurchasescs frm = (frmPurchasescs)Application.OpenForms["frmPurchasescs"];
                            if (frm.gvItemCard.RowCount > 0)
                            {
                                var DetailListV = frm.gcItemCard.DataSource as List<SaleDetailView>;

                                var x = DetailListV.Any(xx => xx.ItemCode == item1.ItemCode);

                                if (x)
                                {
                                    DetailListV.ForEach(xx =>
                                    {
                                        if (xx.ItemCode == item1.ItemCode)
                                        {

                                            xx.Qty += double.Parse(txtFinalUnitsNumber.Text);
                                            xx.Total = Convert.ToDouble(xx.Qty) * Convert.ToDouble(xx.PriceBuy);

                                        }


                                    });


                                }
                                else
                                {


                                    SaleDetailView Detail = new SaleDetailView
                                    {

                                        ParCode = item1.ParCode,
                                        Name = item1.Name,
                                        UnitName = item1.UnitName,
                                        Price = Convert.ToDouble(txtPrice.Text),
                                        PriceBuy = Convert.ToDouble(txtPriceBuy.Text),
                                        Qty = Convert.ToDouble(txtFinalUnitsNumber.Text),
                                        Total = Convert.ToDouble(txtFinalUnitsNumber.Text) * Convert.ToDouble(txtPriceBuy.Text),
                                        Branches_Code = st.GetBranch_Code(),
                                      Warhouse_Code = 1,
                                        Name_En = item1.Name_En,
                                        Warehouse_Name = "eeee",
                                        CategoryCode = item1.CategoryCode,
                                        ItemCode = item1.ItemCode,
                                        Operation_Type_Id = 2,
                                        EntryDate = DateTime.Now,
                                        UnitCode = item1.UnitCode,
                                        CategoryName = item1.CategoryName,


                                    };
                                    DetailListV.Add(Detail);

                                }

                                frm.gcItemCard.DataSource = DetailListV;
                                frm.gcItemCard.RefreshDataSource();
                                Rest();

                            }

                            else
                            {

                                try
                                {
                                    List<SaleDetailView> DetailListV2 = new List<SaleDetailView>();
                                    if (item1 != null)
                                    {

                                        SaleDetailView Detail = new SaleDetailView
                                        {

                                            ParCode = item1.ParCode,
                                            Name = item1.Name,
                                            UnitName = item1.UnitName,
                                            Price = Convert.ToDouble(txtPrice.Text),
                                            PriceBuy = Convert.ToDouble(txtPriceBuy.Text),
                                            Qty = Convert.ToDouble(txtFinalUnitsNumber.Text),
                                            Total = Convert.ToDouble(txtFinalUnitsNumber.Text) * Convert.ToDouble(txtPriceBuy.Text),
                                            Branches_Code = st.GetBranch_Code(),
                                            Warehouse_Name = slkWarhouse.Text,
                                            Warhouse_Code = Convert.ToInt64(slkWarhouse.EditValue),
                                            CategoryCode = item1.CategoryCode,
                                            ItemCode = item1.ItemCode,
                                            Operation_Type_Id = 2,
                                            EntryDate = DateTime.Now,
                                            UnitCode = item1.UnitCode,
                                            CategoryName = item1.CategoryName,


                                        };
                                        DetailListV2.Add(Detail);
                                        frm.gcItemCard.DataSource = DetailListV2;
                                        frm.gcItemCard.RefreshDataSource();
                              

                                    }


                                }
                                catch
                                {


                                }


                            }


                        }
                        Rest();
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(slkItem.Text))
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "Please select a product" : "برجاء اختيار منتج", MessageBoxButtons.OK);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(slkWarhouse.Text)) {

                        MaterialMessageBox.Show(st.isEnglish() ? "Please select a Warhouse" : "برجاء اختيار مخزن", MessageBoxButtons.OK);
                        return;


                    }
                 
                   

                }


            }
            catch
            {

            }


           

        }
        void Rest()
        {


            //txtParCode.ResetText();
            txtPrice.ResetText();
            txtUnit.ResetText();
            txtPriceBuy.ResetText();

            txtName.ResetText();
            txtFinalUnitsNumber.Text = "1";
            //txtFinalUnitsNumber.ResetText();
            txtUnitForBoxNumber.ResetText();
          
            slkItem.Text="";
            txtCatgoryName.ResetText();
            chkBox.Checked = false;
            chkUnits.Checked = true;
            slkItem.Focus();

        }

        private void جديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rest();
        }

        private void slkItem_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(slkItem.Text))
            {
                try
                {
                    Int64 ItemCode = Convert.ToInt64(slkItem.EditValue);
                    Int64 branchCode = st.GetBranch_Code();
                    ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ItemCode == ItemCode && x.Branch_Code == branchCode);
                    if (item != null)
                    {
                        txtName.Text = item.Name;
                        txtUnit.Text = item.UnitName;
                        txtPrice.Text = item.Price.ToString();
                        txtCatgoryName.Text = item.CategoryName;
                        txtPriceBuy.Text = item.PriceBuy.ToString();
                        txtFinalUnitsNumber.Text = "1";
                    }
                    else
                    {
                        Rest();

                    }

                }
                catch
                {

                }
                //MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
                //return;
            }
            else
            {

                Rest();


            }
        }
    }
}