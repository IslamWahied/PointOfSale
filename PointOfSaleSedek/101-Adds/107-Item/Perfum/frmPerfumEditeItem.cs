using System;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
using DataRep;
using DevExpress.Data.ODataLinq.Helpers;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmPerfumEditeItem : MaterialSkin.Controls.MaterialForm
    {
         public Int64 ItemCode;
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmPerfumEditeItem()
        {
            InitializeComponent();
            GetAllCatgory();
            GetAllUnitCard();
            langu();
        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.groupControl1.Text = st.isEnglish() ? "Add Item" : "اضافة صنف";
            tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            groupControl1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;



            labelControl5.Text = st.isEnglish() ? "Category" : "المجموعة";

            labelControl2.Text = st.isEnglish() ? "Item Name" : "اسم الصنف";
            labelControl3.Text = st.isEnglish() ? "Unit" : "وحدة القياس";
            lblRisklimit.Text = st.isEnglish() ? "Danger Limit" : "حد الخطر";
            labelControl4.Text = st.isEnglish() ? "Selling Price" : "سعر البيع";


            gridColumn1.Caption = st.isEnglish() ? "Code" : "التسلسل";
            gridColumn2.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";

            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
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


            if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtPrice.Text))
            {


                //MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
                MessageBox.Show("يوجد حقول فارغة");
                return;


            }


            else
            { 
            
            bool TestItemName = context.ItemCards.Any(Item => Item.Name == txtName.Text);
          


            if (Application.OpenForms.OfType<frmPerfumItemCard>().Any())
            {
                    frmPerfumItemCard frm = (frmPerfumItemCard)Application.OpenForms["frmPerfumItemCard"];
               
                    ItemCard _ItemCard;
                    _ItemCard = context.ItemCards.SingleOrDefault(item => item.ItemCode == ItemCode);
                    _ItemCard.CategoryCode = Convert.ToInt64(SlkCatgoryName.EditValue);
                _ItemCard.Item_Risk_limit = Convert.ToDouble(txtItemRisklimit.Text);
                _ItemCard.Price = Convert.ToDouble(txtPrice.Text);
                    _ItemCard.PriceBuy =0;
                    _ItemCard.UnitCode = Convert.ToInt64(SlkUnit.EditValue);
            
                    _ItemCard.Name = txtName.Text;
                    _ItemCard.ParCode ="0";
                  // _ItemCard.AddItem = (bool)chkAddItem.Checked;
                    _ItemCard.AddItem = true;
                    context.SaveChanges();
              
                    
                ItemCardView result2 = context.ItemCardViews.Where(x => x.ItemCode == ItemCode && x.IsDeleted == 0).FirstOrDefault();
             frm.gvItemCard.SetFocusedRowCellValue("CategoryName", result2.CategoryName);
                frm.gvItemCard.SetFocusedRowCellValue("Name", result2.Name);
                frm.gvItemCard.SetFocusedRowCellValue("UnitName", result2.UnitName);
                frm.gvItemCard.SetFocusedRowCellValue("AddItem", result2.AddItem);
                frm.gvItemCard.SetFocusedRowCellValue("CategoryCode", result2.CategoryCode);
                     
                frm.gvItemCard.SetFocusedRowCellValue("Price", result2.Price);
                frm.gvItemCard.SetFocusedRowCellValue("PriceBuy", 0);
                frm.gvItemCard.SetFocusedRowCellValue("ParCode", result2.ParCode);
                frm.gvItemCard.SetFocusedRowCellValue("Item_Risk_limit", result2.Item_Risk_limit);
                frm.gvItemCard.RefreshData();
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