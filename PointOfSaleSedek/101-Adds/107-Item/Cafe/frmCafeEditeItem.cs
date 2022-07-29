﻿using System;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
 
using DevExpress.Data.ODataLinq.Helpers;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeEditeItem : MaterialSkin.Controls.MaterialForm
    {
         public Int64 ItemCode;
        readonly SaleEntities context = new SaleEntities();
        public frmCafeEditeItem()
        {
            InitializeComponent();
            GetAllCatgory();
            GetAllUnitCard();
        }

        //void GetMaxCatgoryId()
        //{

        //    var FloorCount = context.Categories.Where(x => x.CategoryName == SlkCatgoryName.Text).Select(x => x.Id).Max();


        //}
        void GetAllCatgory()
        {

            var catgorys = context.Categories.Where(x => x.IsDeleted == 0).ToList();



            SlkCatgoryName.Properties.DataSource = catgorys;
            SlkCatgoryName.Properties.ValueMember = "CategoryCode";
            SlkCatgoryName.Properties.DisplayMember = "CategoryName";
        }
        void GetAllUnitCard()
        {

            var Units = context.UnitCards.Where(x => x.IsDeleted == 0).ToList();



            SlkUnit.Properties.DataSource = Units;
            SlkUnit.Properties.ValueMember = "UnitCode";
            SlkUnit.Properties.DisplayMember = "UnitName";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void AddItem()
        {


            if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtParCode.Text))
            {


                //MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
                MessageBox.Show("يوجد حقول فارغة");
                return;


            }


            else
            { 
            
            bool TestItemName = context.ItemCards.Any(Item => Item.Name == txtName.Text||Item.ParCode==txtParCode.Text);
          


            if (Application.OpenForms.OfType<frmCafeItemCard>().Any())
            {
                    frmCafeItemCard frm = (frmCafeItemCard)Application.OpenForms["frmCafeItemCard"];
               
                    ItemCard _ItemCard;
                    _ItemCard = context.ItemCards.SingleOrDefault(item => item.ItemCode == ItemCode);
                    _ItemCard.CategoryCode = Convert.ToInt64(SlkCatgoryName.EditValue);
                _ItemCard.Item_Risk_limit = Convert.ToDouble(txtItemRisklimit.Text);
                _ItemCard.Price = Convert.ToDouble(txtPrice.Text);
                    _ItemCard.PriceBuy =0;
                    _ItemCard.UnitCode = Convert.ToInt64(SlkUnit.EditValue);
            
                    _ItemCard.Name = txtName.Text;
                    _ItemCard.ParCode = txtParCode.Text;
                    _ItemCard.AddItem = (bool)chkAddItem.Checked;
                    context.SaveChanges();
              
                    
                ItemCardView result2 = context.ItemCardViews.Where(x => x.ItemCode == ItemCode && x.IsDeleted == 0).FirstOrDefault();
             frm.gvCafeItemCard.SetFocusedRowCellValue("CategoryName", result2.CategoryName);
                frm.gvCafeItemCard.SetFocusedRowCellValue("Name", result2.Name);
                frm.gvCafeItemCard.SetFocusedRowCellValue("UnitName", result2.UnitName);
                frm.gvCafeItemCard.SetFocusedRowCellValue("AddItem", result2.AddItem);
                    frm.gvCafeItemCard.SetFocusedRowCellValue("CategoryCode", result2.CategoryCode);
                    frm.gvCafeItemCard.SetFocusedRowCellValue("Price", result2.Price);
                frm.gvCafeItemCard.SetFocusedRowCellValue("PriceBuy", 0);
                frm.gvCafeItemCard.SetFocusedRowCellValue("ParCode", result2.ParCode);
                frm.gvCafeItemCard.SetFocusedRowCellValue("Item_Risk_limit", result2.Item_Risk_limit);
                frm.gvCafeItemCard.RefreshData();
                    HelperClass.HelperClass.ClearValues(tableLayoutPanel1);

               
                     this.Close();
            }
            
            }    



        }


     

      

        

      
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            AddItem();
        }

        private void btnCancel_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}