using System;
using System.Collections.Generic;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
 
using PointOfSaleSedek._102_MaterialSkin;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{
    
    public partial class frmPerfumSearchItems : DevExpress.XtraEditors.XtraForm
    {

       

        readonly SaleEntities context = new SaleEntities();
        public frmPerfumSearchItems()
        {
            InitializeComponent();
            FillOliSlkItems();

            FillSlkOilQty();

            FillGlassSlkItems();
 
        }

        public void FillOliSlkItems()
        {
            DataTable dt = new DataTable();
            var result = context.ItemCardViews.Where(Item => Item.IsDeleted == 0 && Item.CategoryCode == 1).ToList();
            slkOilItem.Properties.DataSource = result;
            slkOilItem.Properties.ValueMember = "ItemCode";
            slkOilItem.Properties.DisplayMember = "Name";

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
            if (
    String.IsNullOrWhiteSpace(slkOilItem.Text) ||
    String.IsNullOrWhiteSpace(slkOilQty.Text) ||
    String.IsNullOrWhiteSpace(slkBottleItem.Text) ||
    String.IsNullOrWhiteSpace(txtBottleQty.Text)
    )
            {

                MaterialMessageBox.Show("برجاء ادخال جميع الحقول", MessageBoxButtons.OK);

                return;
            }


            if (Application.OpenForms.OfType<frmPerfumSales>().Any())
            {

                frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];
                List<SaleDetailView> gcData = new List<SaleDetailView>();

                var glassItemCode = Convert.ToInt64(slkBottleItem.EditValue) ;
                var OilItemCode = Convert.ToInt64(slkOilItem.EditValue) ;

                double OilQty = Convert.ToDouble(txtBottleQty.Text) * Convert.ToDouble(slkOilQty.Text); 


                for (int i = 0; i < 2; i++)
                {
                    if (i == 1)
                    {

                        var grd = frm.gcSaleDetail.DataSource as List<SaleDetailView>;

                        if (grd != null)
                        {
                            gcData = grd;
                        }

                  
                        var RowCount = frm.gvSaleDetail.RowCount;

                        ItemCardView itemCard = context.ItemCardViews.Where(x => x.ItemCode == OilItemCode && x.IsDeleted == 0).FirstOrDefault();
                        if (itemCard == null)
                        {
                            return;
                        }


                        SaleDetailView _SaleDetailView = new SaleDetailView()
                        {
                            ItemCode = itemCard.ItemCode,
                            EntryDate = DateTime.Now,
                            Price = Convert.ToDouble(itemCard.Price),
                            Qty = OilQty,
                            Total = OilQty * Convert.ToDouble(itemCard.Price),
                            Name = itemCard.Name,
                            UnitCode = itemCard.UnitCode,
                            CategoryCode = itemCard.CategoryCode,
                            ParCode = itemCard.ParCode,
                            Operation_Type_Id = 2
                        };

                        gcData.Add(_SaleDetailView);
                        frm.gcSaleDetail.DataSource = gcData;
                        frm.gcSaleDetail.RefreshDataSource();
                        double sum = 0;

                        gcData.ForEach(x =>
                        {
                            sum += Convert.ToDouble(x.Total);
                        });
                        frm.lblFinalBeforDesCound.Text = sum.ToString();
                        frm.lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(frm.lblDiscount.Text));
                        frm.lblItemQty.Text = (RowCount + 1).ToString();
                    }
                    else
                    {



                        var grd = frm.gcSaleDetail.DataSource as List<SaleDetailView>;

                        if (grd != null)
                        {
                            gcData = grd;
                        }


                        var RowCount = frm.gvSaleDetail.RowCount;

                        ItemCardView itemCard = context.ItemCardViews.Where(x => x.ItemCode == glassItemCode && x.IsDeleted == 0).FirstOrDefault();
                        if (itemCard == null)
                        {
                            return;
                        }


                        SaleDetailView _SaleDetailView = new SaleDetailView()
                        {
                            ItemCode = itemCard.ItemCode,
                            EntryDate = DateTime.Now,
                            Price = Convert.ToDouble(itemCard.Price),
                            Qty = Convert.ToDouble(txtBottleQty.Text),
                            Total = Convert.ToDouble(txtBottleQty.Text) * Convert.ToDouble(itemCard.Price),
                            Name = itemCard.Name,
                            UnitCode = itemCard.UnitCode,
                            CategoryCode = itemCard.CategoryCode,
                            ParCode = itemCard.ParCode,
                            Operation_Type_Id = 2
                        };

                        gcData.Add(_SaleDetailView);
                        frm.gcSaleDetail.DataSource = gcData;
                        frm.gcSaleDetail.RefreshDataSource();
                        double sum = 0;

                        gcData.ForEach(x =>
                        {
                            sum += Convert.ToDouble(x.Total);
                        });
                        frm.lblFinalBeforDesCound.Text = sum.ToString();
                        frm.lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(frm.lblDiscount.Text));
                        frm.lblItemQty.Text = (RowCount + 1).ToString();



                    }


                }



                slkOilItem.Text="";
                slkOilQty.Text="";
                slkBottleItem.Text="";
                txtBottleQty.Text="";





            }
        }
    }
}

public class OilQty
{
    public double Qty { get; set; }
     
}