using System;
using System.Collections.Generic;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
 
using PointOfSaleSedek._102_MaterialSkin;
using DataRep;
using PointOfSaleSedek.Model;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    
    public partial class frmPerfumSearchItems : DevExpress.XtraEditors.XtraForm
    {
        readonly Static st = new Static();


        readonly POSEntity context = new POSEntity();
        public frmPerfumSearchItems()
        {
            InitializeComponent();
            langu();
            FillOliSlkItems();

            FillSlkOilQty();

            FillGlassSlkItems();

            FillSlkItems();


        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Search" : "بحث";
 
            labelControl1.Text = st.isEnglish() ? "Name" : "الاسم";
            labelControl5.Text = st.isEnglish() ? "Quantity" : "الكمية";
            labelControl7.Text = st.isEnglish() ? "Bottle" : "زجاج";

            labelControl3.Text = st.isEnglish() ? "Name" : "الاسم";
            labelControl2.Text = st.isEnglish() ? "Quantity" : "الكمية";
            labelControl6.Text = st.isEnglish() ? "Oil" : "زيت";
            labelControl10.Text = st.isEnglish() ? "Other Products" : "منتجات اخري";

            labelControl8.Text = st.isEnglish() ? "Name" : "الاسم";
            labelControl9.Text = st.isEnglish() ? "Quantity" : "الكمية";

            gridColumn1.Caption = st.isEnglish() ? "ParCode" : "الباركود";
            gridColumn2.Caption = st.isEnglish() ? "Name" : "الاسم";

            gridColumn3.Caption = st.isEnglish() ? "ParCode" : "الباركود";
            gridColumn5.Caption = st.isEnglish() ? "Name" : "الاسم";


            gridColumn6.Caption = st.isEnglish() ? "ParCode" : "الباركود";
            gridColumn7.Caption = st.isEnglish() ? "Name" : "الاسم";

            gridColumn4.Caption = st.isEnglish() ? "Quantity" : "الكمية";

            btnSave.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
        }

        public void FillOliSlkItems()
        {
            DataTable dt = new DataTable();
            var result = context.ItemCardViews.Where(Item => Item.IsDeleted == 0 && Item.CategoryCode == 1).ToList();
            slkOilItem.Properties.DataSource = result;
            slkOilItem.Properties.ValueMember = "ItemCode";
            slkOilItem.Properties.DisplayMember = "Name";

        }


        public void FillSlkItems()
        {
            DataTable dt = new DataTable();
            var result = context.ItemCardViews.Where(Item => Item.IsDeleted == 0 && Item.CategoryCode != 1 && Item.CategoryCode != 2).ToList();
            slkAllItems.Properties.DataSource = result;
            slkAllItems.Properties.ValueMember = "ItemCode";
            slkAllItems.Properties.DisplayMember = "Name";

        }



        public void FillGlassSlkItems()
        {
            DataTable dt = new DataTable();
            var result = context.ItemCardViews.Where(Item => Item.IsDeleted == 0 && Item.CategoryCode == 2).ToList();
            slkBottleItem.Properties.DataSource = result;
            slkBottleItem.Properties.ValueMember = "ItemCode";
            slkBottleItem.Properties.DisplayMember = "Name";

        }


        public void FillSlkOilQty()
        {
            List<OilQty> oilQtyList = new List<OilQty>();

            oilQtyList.Add(
               new OilQty { 
                   Qty = 1
                
                }
                );
            oilQtyList.Add(
              new OilQty
              {
                  Qty = 2

              }
               );
            oilQtyList.Add(
              new OilQty
              {
                  Qty = 3

              }

               );



            oilQtyList.Add(
              new OilQty
              {
                  Qty = 4

              }

               );
            oilQtyList.Add(
              new OilQty
              {
                  Qty = 5

              }

               );



            oilQtyList.Add(
              new OilQty
              {
                  Qty = 6

              }

               );


            oilQtyList.Add(
              new OilQty
              {
                  Qty = 7

              }

               );

            oilQtyList.Add(
              new OilQty
              {
                  Qty = 8

              }

               );



            oilQtyList.Add(
              new OilQty
              {
                  Qty = 9

              });


            oilQtyList.Add(
             new OilQty
             {
                 Qty = 10

             });

            oilQtyList.Add(
             new OilQty
             {
                 Qty = 11

             });


            oilQtyList.Add(
             new OilQty
             {
                 Qty = 12

             });

            oilQtyList.Add(
             new OilQty
             {
                 Qty = 13

             });



            oilQtyList.Add(
  new OilQty
  {
      Qty = 14

  });

            oilQtyList.Add(
  new OilQty
  {
      Qty = 15

  });


            oilQtyList.Add(
new OilQty
{

Qty = 16

});

            oilQtyList.Add(
new OilQty
{
Qty = 17

});



            oilQtyList.Add(
new OilQty
{
Qty = 18

});


            oilQtyList.Add(
new OilQty
{
Qty = 19

});

            oilQtyList.Add(
new OilQty
{
Qty = 20

});



            oilQtyList.Add(
new OilQty
{
Qty = 21

});





            oilQtyList.Add(
new OilQty
{
    Qty = 22

});


            oilQtyList.Add(
new OilQty
{
    Qty = 23

});



            oilQtyList.Add(
new OilQty
{
    Qty = 24

});


            oilQtyList.Add(
new OilQty
{
Qty = 25

});




            oilQtyList.Add(
new OilQty
{
    Qty = 26

});


            oilQtyList.Add(
new OilQty
{
    Qty = 27

});

            oilQtyList.Add(
 new OilQty
 {
     Qty = 28

 });
            oilQtyList.Add(
 new OilQty
 {
     Qty = 29

 });

            oilQtyList.Add(
new OilQty
{
Qty = 30

});
            oilQtyList.Add(
new OilQty
{
    Qty = 31

});

            oilQtyList.Add(
new OilQty
{
    Qty = 32

});

            oilQtyList.Add(
new OilQty
{
    Qty = 33

});


            oilQtyList.Add(
new OilQty
{
Qty = 34

});


            oilQtyList.Add(
new OilQty
{
    Qty = 35

});

            oilQtyList.Add(
new OilQty
{
    Qty = 36

});


            oilQtyList.Add(
new OilQty
{
Qty = 37

});





            oilQtyList.Add(
new OilQty
{
    Qty = 38

});


            oilQtyList.Add(
new OilQty
{
    Qty = 39

});



            oilQtyList.Add(
new OilQty
{
Qty = 40

});

































            slkOilQty.Properties.DataSource = oilQtyList;
            slkOilQty.Properties.ValueMember = "Qty";
            slkOilQty.Properties.DisplayMember = "Qty";

        }



        //private void txtParCode_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text);
        //        if (item != null)
        //        {
        //            txtCatgoryName.Text = item.CategoryName;
        //            txtName.Text = item.Name;
        //            txtUnit.Text = item.UnitName;
        //            txtPrice.Text = item.Price.ToString();

        //        }
        //        else
        //        {
        //            HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
        //        }

        //    }
        //    catch
        //    {
        //        HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
        //    }
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPrice_EditValueChanged(object sender, EventArgs e)
        {

        }
        //void Rest()
        //{


        //    txtParCode.ResetText();
        //    txtPrice.ResetText();
        //    txtUnit.ResetText();
            
        //    txtName.ResetText();
        //    txtQty.Text = "1";
            
           
        //    slkItem.Text = "";
        //    txtCatgoryName.ResetText();
           
        //    slkItem.Focus();

        //}
        //private void slkItem_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(slkItem.Text))
        //    {
        //        try
        //        {
        //            Int64 ItemCode = Convert.ToInt64(slkItem.EditValue);
        //            ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ItemCode == ItemCode);
        //            if (item != null)
        //            {
        //                txtQty.Text = "1";
        //                txtParCode.Text = item.ParCode.ToString();
        //                txtCatgoryName.Text = item.CategoryName;
        //                txtName.Text = item.Name;
        //                txtUnit.Text = item.UnitName;
        //                txtPrice.Text = item.Price.ToString();
                         
        //            }
        //            else
        //            {
        //                Rest();

        //            }

        //        }
        //        catch
        //        {

        //        }
        //        //MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
        //        //return;
        //    }
        //    else
        //    {

        //        Rest();


        //    }


        //}

         
        

       
       
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (
            //        string.IsNullOrWhiteSpace(slkOilItem.Text) ||
            //        string.IsNullOrWhiteSpace(slkOilQty.Text) ||
            //        string.IsNullOrWhiteSpace(slkBottleItem.Text) ||
            //        string.IsNullOrWhiteSpace(txtBottleQty.Text)
            //     )
            //{

            //    MaterialMessageBox.Show("برجاء ادخال جميع الحقول", MessageBoxButtons.OK);

            //    return;
            //}


            if (Application.OpenForms.OfType<frmPerfumSales>().Any())
            {

                frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];
                List<SaleDetailPrfumViewVm> gcData = new List<SaleDetailPrfumViewVm>();

                var glassItemCode = Convert.ToInt64(slkBottleItem.EditValue);
                var AllItemCode = Convert.ToInt64(slkAllItems.EditValue);
                var OilItemCode = Convert.ToInt64(slkOilItem.EditValue);

                double OilQty = 0;
                double glassQty = 0;


                double OilPrice = 0;
                double glassPrice = 0;


                if (!string.IsNullOrWhiteSpace(slkOilItem.Text) && string.IsNullOrWhiteSpace(slkOilQty.Text)) {

                    MaterialMessageBox.Show("برجاء ادخال كمية الزيت ", MessageBoxButtons.OK);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(slkBottleItem.Text) && string.IsNullOrWhiteSpace(txtBottleQty.Text))
                {

                    MaterialMessageBox.Show("برجاء ادخال كمية الزجاج ", MessageBoxButtons.OK);
                    return;
                }

                if (!string.IsNullOrWhiteSpace(slkAllItems.Text) && string.IsNullOrWhiteSpace(txtAllItem.Text))
                {

                    MaterialMessageBox.Show("برجاء ادخال كمية المنتج ", MessageBoxButtons.OK);
                    return;
                }


                SaleDetailPrfumViewVm _SaleDetailPrfumViewVm = new SaleDetailPrfumViewVm();
                var grd = frm.gcPrfumSaleDetail.DataSource as List<SaleDetailPrfumViewVm>;

                if (grd != null)
                {
                    gcData = grd;
                }


                var RowCount = frm.gvPrfumSaleDetail.RowCount;
                if (!string.IsNullOrWhiteSpace(slkAllItems.Text) && slkAllItems.EditValue.ToString() != "0")
                {
                    if (!string.IsNullOrWhiteSpace(txtAllItem.Text))
                    {
                        glassPrice = context.ItemCardViews.Where(x => x.ItemCode == AllItemCode && x.IsDeleted == 0).FirstOrDefault().Price;
                        glassQty = Convert.ToDouble(txtAllItem.Text);

                          _SaleDetailPrfumViewVm = new SaleDetailPrfumViewVm()
                        {
                            GlassIName = slkAllItems.Text ?? "",
                            GlassItemCode = Convert.ToInt64(slkAllItems.EditValue ?? 0),


                            OilItemCode =0,
                            OilIName = "لايوجد",

                            GlassPrice = glassPrice,
                            OilPrice = 0,

                            GlassQty = glassQty,

                            LineSequence = gcData.Count + 1,

                            OilQty = OilQty,

                            Total = (OilQty * OilPrice) + (glassQty * glassPrice)



                        };

                    }
                }
                else {
                    if (!string.IsNullOrWhiteSpace(slkOilQty.Text) && OilItemCode != 0)
                    {

                        if (OilItemCode != 0)
                        {
                            OilPrice = context.ItemCardViews.Where(x => x.ItemCode == OilItemCode && x.IsDeleted == 0).FirstOrDefault().Price;
                            OilQty = Convert.ToDouble(slkOilQty.Text);
                        }
                        else {
                            OilPrice = 0;
                            OilQty = 0;
                        }

                       

                    }

                    if (!string.IsNullOrWhiteSpace(txtBottleQty.Text) )
                    {
                        if (glassItemCode != 0)
                        {
                            glassPrice = context.ItemCardViews.Where(x => x.ItemCode == glassItemCode && x.IsDeleted == 0).FirstOrDefault().Price;
                            glassQty = Convert.ToDouble(txtBottleQty.Text);
                        }
                        else
                        {
                            glassPrice = 0;
                            glassQty = 0;
                        }
                       

                    }


                      _SaleDetailPrfumViewVm = new SaleDetailPrfumViewVm()
                    {
                        GlassIName = slkBottleItem.Text != null && slkBottleItem.Text != ""  ? slkBottleItem.Text : "لايوجد",
                        GlassItemCode = Convert.ToInt64(slkBottleItem.EditValue ?? 0),


                        OilItemCode = Convert.ToInt64(slkOilItem.EditValue ?? 0),
                        OilIName = slkOilItem.Text != null && slkOilItem.Text != "" ? slkOilItem.Text : "لايوجد",

                          GlassPrice = glassPrice,
                        OilPrice = OilPrice,

                        GlassQty = glassQty,

                        LineSequence = gcData.Count + 1,

                        OilQty = OilQty,

                        Total = (OilQty * OilPrice) + (glassQty * glassPrice)



                    };
                }

               



 


                
         
                       


                 

                

                        gcData.Add(_SaleDetailPrfumViewVm);
                        frm.gcPrfumSaleDetail.DataSource = gcData;
                        frm.gcPrfumSaleDetail.RefreshDataSource();
                        double sum = 0;

                        gcData.ForEach(x =>
                        {
                            sum += Convert.ToDouble(x.Total);
                        });
                        frm.lblFinalBeforDesCound.Text = sum.ToString();
                        frm.lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(frm.lblDiscount.Text));
                        frm.lblItemQty.Text = (RowCount + 1).ToString();
                    
                    
 


                }



            slkOilItem.Text = "";
            slkOilItem.EditValue = 0;
            slkOilQty.EditValue = 1;

            slkBottleItem.Text = "";
            slkBottleItem.EditValue = 0;
            txtBottleQty.Text = "1";

            slkAllItems.Text = "";
            slkAllItems.EditValue = 0;
            txtAllItem.Text = "1";




        }

        private void slkAllItems_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(slkAllItems.Text)) {
                slkOilItem.Text = "";
                slkBottleItem.Text = "";
                slkOilItem.EditValue = 0;
                slkBottleItem.EditValue = 0;
                txtBottleQty.Text = "1";
                slkOilQty.EditValue = 1;
            }
        }

        private void slkOilItem_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(slkOilItem.Text))
            {
                slkAllItems.Text = "";

                slkAllItems.EditValue = 0;
               
                txtAllItem.Text = "1";
                
            }
        }

        private void slkBottleItem_EditValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(slkBottleItem.Text))
            {
                slkAllItems.Text = "";

                slkAllItems.EditValue = 0;

                txtAllItem.Text = "1";

            }
        }
    }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    if (
        //            string.IsNullOrWhiteSpace(slkOilItem.Text) ||
        //            string.IsNullOrWhiteSpace(slkOilQty.Text) ||
        //            string.IsNullOrWhiteSpace(slkBottleItem.Text) ||
        //            string.IsNullOrWhiteSpace(txtBottleQty.Text)
        //         )
        //    {

        //        MaterialMessageBox.Show("برجاء ادخال جميع الحقول", MessageBoxButtons.OK);

        //        return;
        //    }


        //    if (Application.OpenForms.OfType<frmPerfumSales>().Any())
        //    {

        //        frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];
        //        List<SaleDetailView> gcData = new List<SaleDetailView>();

        //        var glassItemCode = Convert.ToInt64(slkBottleItem.EditValue) ;
        //        var OilItemCode = Convert.ToInt64(slkOilItem.EditValue) ;

        //        double OilQty = Convert.ToDouble(txtBottleQty.Text) * Convert.ToDouble(slkOilQty.Text); 


        //        for (int i = 0; i < 2; i++)
        //        {
        //            if (i == 1)
        //            {

        //                var grd = frm.gcPrfumSaleDetail.DataSource as List<SaleDetailView>;

        //                if (grd != null)
        //                {
        //                    gcData = grd;
        //                }

                  
        //                var RowCount = frm.gvSaleDetail.RowCount;

        //                ItemCardView itemCard = context.ItemCardViews.Where(x => x.ItemCode == OilItemCode && x.IsDeleted == 0).FirstOrDefault();
        //                if (itemCard == null)
        //                {
        //                    return;
        //                }


        //                SaleDetailView _SaleDetailView = new SaleDetailView()
        //                {
        //                    ItemCode = itemCard.ItemCode,
        //                    EntryDate = DateTime.Now,
        //                    Price = Convert.ToDouble(itemCard.Price),
        //                    Qty = OilQty,
        //                    Total = OilQty * Convert.ToDouble(itemCard.Price),
        //                    Name = itemCard.Name,
        //                    UnitCode = itemCard.UnitCode,
        //                    CategoryCode = itemCard.CategoryCode,
        //                    ParCode = itemCard.ParCode,
        //                    Operation_Type_Id = 2,
        //                    LineSequence = 1,
        //                    isOile = 1
        //                };

        //                gcData.Add(_SaleDetailView);
        //                frm.gcPrfumSaleDetail.DataSource = gcData;
        //                frm.gcPrfumSaleDetail.RefreshDataSource();
        //                double sum = 0;

        //                gcData.ForEach(x =>
        //                {
        //                    sum += Convert.ToDouble(x.Total);
        //                });
        //                frm.lblFinalBeforDesCound.Text = sum.ToString();
        //                frm.lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(frm.lblDiscount.Text));
        //                frm.lblItemQty.Text = (RowCount + 1).ToString();
        //            }
        //            else
        //            {



        //                var grd = frm.gcPrfumSaleDetail.DataSource as List<SaleDetailView>;

        //                if (grd != null)
        //                {
        //                    gcData = grd;
        //                }


        //                var RowCount = frm.gvSaleDetail.RowCount;

        //                ItemCardView itemCard = context.ItemCardViews.Where(x => x.ItemCode == glassItemCode && x.IsDeleted == 0).FirstOrDefault();
        //                if (itemCard == null)
        //                {
        //                    return;
        //                }


        //                SaleDetailView _SaleDetailView = new SaleDetailView()
        //                {
        //                    ItemCode = itemCard.ItemCode,
        //                    EntryDate = DateTime.Now,
        //                    Price = Convert.ToDouble(itemCard.Price),
        //                    Qty = Convert.ToDouble(txtBottleQty.Text),
        //                    Total = Convert.ToDouble(txtBottleQty.Text) * Convert.ToDouble(itemCard.Price),
        //                    Name = itemCard.Name,
        //                    UnitCode = itemCard.UnitCode,
        //                    CategoryCode = itemCard.CategoryCode,
        //                    ParCode = itemCard.ParCode,
        //                    Operation_Type_Id = 2
        //                };

        //                gcData.Add(_SaleDetailView);
        //                frm.gcPrfumSaleDetail.DataSource = gcData;
        //                frm.gcPrfumSaleDetail.RefreshDataSource();
        //                double sum = 0;

        //                gcData.ForEach(x =>
        //                {
        //                    sum += Convert.ToDouble(x.Total);
        //                });
        //                frm.lblFinalBeforDesCound.Text = sum.ToString();
        //                frm.lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(frm.lblDiscount.Text));
        //                frm.lblItemQty.Text = (RowCount + 1).ToString();



        //            }


        //        }



        //        slkOilItem.Text="";
        //        slkOilQty.Text="";
        //        slkBottleItem.Text="";
        //        txtBottleQty.Text="";





        //    }
        //}
   // }
}

public class OilQty
{
    public double Qty { get; set; }
     
}