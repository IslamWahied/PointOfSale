﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeSearchItems : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();

        public frmCafeSearchItems()
        {
            InitializeComponent();
            FillSlkItems();
            slkItem.Focus();
            langu();

        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Search" : "بحث";
            labelControl5.Text = st.isEnglish() ? "BarCode" : "الباركود";
            labelControl1.Text = st.isEnglish() ? "Category" : "المجموعة";
            labelControl2.Text = st.isEnglish() ? "Item Name" : "اسم المنتج";
            labelControl3.Text = st.isEnglish() ? "Unit" : "وحدة القياس";
            labelControl4.Text = st.isEnglish() ? "Selling Price" : "سعر البيع";
            labelControl6.Text = st.isEnglish() ? "Quantity" : "الكمية";
            gridColumn1.Caption = st.isEnglish() ? "ParCode" : "الباركود";
            gridColumn2.Caption = st.isEnglish() ? "Name" : "الاسم";

            btnSave.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
        }

        public void FillSlkItems()
        {
            DataTable dt = new DataTable();
            var result = context.ItemCardViews.Where(user => user.IsDeleted == 0).ToList();
            slkItem.Properties.DataSource = result;
            slkItem.Properties.ValueMember = "ItemCode";
            slkItem.Properties.DisplayMember = "Name";

        }

        private void txtParCode_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text);
                if (item != null)
                {
                    txtCatgoryName.Text = item.CategoryName;
                    txtName.Text = item.Name;
                    txtUnit.Text = item.UnitName;
                    txtPrice.Text = item.Price.ToString();
                   
                }
                else
                {
                    HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
                }

            }
            catch
            {
                HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {

        }
        void Rest()
        {


            txtParCode.ResetText();
            txtPrice.ResetText();
            txtUnit.ResetText();
            
            txtName.ResetText();
            txtQty.Text = "1";
            
           
            slkItem.Text = "";
            txtCatgoryName.ResetText();
           
            slkItem.Focus();

        }
        private void slkItem_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(slkItem.Text))
            {
                try
                {
                    Int64 ItemCode = Convert.ToInt64(slkItem.EditValue);
                    ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ItemCode == ItemCode);
                    if (item != null)
                    {
                        txtQty.Text = "1";
                        txtParCode.Text = item.ParCode.ToString();
                        txtCatgoryName.Text = item.CategoryName;
                        txtName.Text = item.Name;
                        txtUnit.Text = item.UnitName;
                        txtPrice.Text = item.Price.ToString();
                         
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

        private void btnSave_Click(object sender, EventArgs e)
        {
           if (Application.OpenForms.OfType<frmCafeSales>().Any())
           {


                frmCafeSales frm = (frmCafeSales)Application.OpenForms["frmCafeSales"];

                List<SaleDetailView> gcData = new  List<SaleDetailView>();


                if (frm.gcCafeSaleDetail.DataSource != null) {
                    var grd = frm.gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                    gcData = grd;
                }
             

                //if (grd != null)
                //{ 
                //gcData = grd;
                
                //}



                var itemcode = Convert.ToInt64(slkItem.EditValue);

                //var RowCount = frm.gvSaleDetail.RowCount;


                ItemCardView ii = context.ItemCardViews.Where(x => x.ItemCode == itemcode && x.IsDeleted == 0).FirstOrDefault();
                if (ii == null)
                {

                    return;

                }



                if (gcData.Any(xx => xx.ItemCode == ii.ItemCode))
                {

                    double Qty = gcData.FirstOrDefault(xx => xx.ItemCode == ii.ItemCode).Qty;
                    double price = gcData.FirstOrDefault(xx => xx.ItemCode == ii.ItemCode).Price;

                    Qty += Convert.ToDouble(txtQty.Text);

                    gcData.FirstOrDefault(xx => xx.ItemCode == ii.ItemCode).Qty  = Qty;

                    gcData.FirstOrDefault(xx => xx.ItemCode == ii.ItemCode).Total = Qty * price;



                }
                else {
                    SaleDetailView _SaleDetailView = new SaleDetailView()
                    {
                        ItemCode = ii.ItemCode,
                        EntryDate = DateTime.Now,
                        Price = Convert.ToDouble(ii.Price),
                        Qty = 1,
                        Total = Convert.ToDouble(1) * Convert.ToDouble(ii.Price),
                        Name = ii.Name,
                        UnitCode = ii.UnitCode,
                        CategoryCode = ii.CategoryCode,
                        ParCode = ii.ParCode,
                        Operation_Type_Id = 2




                    };
                    gcData.Add(_SaleDetailView);
                }


              

               
                  
                 


       
                frm.gcCafeSaleDetail.DataSource = gcData;
                frm.gcCafeSaleDetail.RefreshDataSource();
                double sum = 0;
                gcData.ForEach(x =>
                {
                    sum += Convert.ToDouble(x.Total);
                });


                frm.lblFinalBeforDesCound.Text = sum.ToString();

                frm.lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(frm.lblDiscount.Text));
                //frm.lblItemQty.Text = (RowCount + 1).ToString();
                txtParCode.ResetText();
                
                txtParCode.Focus();
                 

                this.Close();
           }
        }
    }
}