using System;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
 
using DevExpress.Data.ODataLinq.Helpers;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeEditeItem : MaterialSkin.Controls.MaterialForm
    {
         public Int64 ItemCode;
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmCafeEditeItem()
        {
            InitializeComponent();
            GetAllCatgory();
            GetAllUnitCard();
            langu();
        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.groupControl1.Text = st.isEnglish() ? "Edit Item" : "تعديل الصنف";
            tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            groupControl1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;



            labelControl5.Text = st.isEnglish() ? "Category" : "المجموعة";
            labelControl1.Text = st.isEnglish() ? "BarCode" : "الباركود";
            labelControl2.Text = st.isEnglish() ? "Arabic Name" : "الاسم بالعربية";
            labelControl7.Text = st.isEnglish() ? "English Name" : "الاسم بالانجليزية";
            labelControl3.Text = st.isEnglish() ? "Unit" : "وحدة القياس";
            lblRisklimit.Text = st.isEnglish() ? "Danger Limit" : "حد الخطر";
            labelControl4.Text = st.isEnglish() ? "Selling Price" : "سعر البيع";
            labelControl6.Text = st.isEnglish() ? "Add the item to the cashier screen" : "اضافة الصنف الي شاشة الكاشير";

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
            var brachCode = st.GetBranch_Code();

            var catgorys = context.Categories.Where(x => x.IsDeleted == 0 && x.Branch_Code == brachCode).ToList();



            SlkCatgoryName.Properties.DataSource = catgorys;
            SlkCatgoryName.Properties.ValueMember = "CategoryCode";
            SlkCatgoryName.Properties.DisplayMember = "CategoryName";
        }
        void GetAllUnitCard()
        {
            var brachCode = st.GetBranch_Code();
            var Units = context.UnitCards.Where(x => x.IsDeleted == 0 && x.Branch_Code == brachCode).ToList();



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
            var brachCode = st.GetBranch_Code();

            if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(txtEngName.Text) || string.IsNullOrWhiteSpace(txtArbName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtParCode.Text))
            {


                //MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
                MessageBox.Show(st.isEnglish() ? "There are empty fields" :"يوجد حقول فارغة");
                return;


            }


            else
            { 
            
            bool TestItemName = context.ItemCards.Any(Item => Item.Name_En == txtEngName.Text || Item.Name == txtArbName.Text||Item.ParCode==txtParCode.Text);
          


            if (Application.OpenForms.OfType<frmCafeItemCard>().Any())
            {
                    frmCafeItemCard frm = (frmCafeItemCard)Application.OpenForms["frmCafeItemCard"];
               
                    ItemCard _ItemCard;
                    _ItemCard = context.ItemCards.SingleOrDefault(item => item.ItemCode == ItemCode && item.Branch_Code == brachCode);
                    _ItemCard.CategoryCode = Convert.ToInt64(SlkCatgoryName.EditValue);
                _ItemCard.Item_Risk_limit = Convert.ToDouble(txtItemRisklimit.Text);
                _ItemCard.Price = Convert.ToDouble(txtPrice.Text);
                    _ItemCard.PriceBuy =0;
                    _ItemCard.UnitCode = Convert.ToInt64(SlkUnit.EditValue);
            
                    _ItemCard.Name = txtArbName.Text;
                    _ItemCard.Name_En = txtEngName.Text;
                    _ItemCard.ParCode = txtParCode.Text;
                    _ItemCard.AddItem = (bool)chkAddItem.Checked;
                    context.SaveChanges();
              
                    
                ItemCardView result2 = context.ItemCardViews.Where(x => x.ItemCode == ItemCode && x.IsDeleted == 0).FirstOrDefault();
             frm.gvCafeItemCard.SetFocusedRowCellValue("CategoryName", result2.CategoryName);
                frm.gvCafeItemCard.SetFocusedRowCellValue("Name", result2.Name);
                frm.gvCafeItemCard.SetFocusedRowCellValue("Name_En", result2.Name_En);
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

        private void labelControl6_Click(object sender, EventArgs e)
        {
             chkAddItem.Checked = !chkAddItem.Checked; 
        }
    }
}