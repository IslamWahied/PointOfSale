using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
 
using System.Windows.Forms;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmPerfumAddItem : MaterialSkin.Controls.MaterialForm
    {
        
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();

        public frmPerfumAddItem()
        {
            InitializeComponent();
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

            using (POSEntity Contexts = new POSEntity())
            {


                bool TestItemName = Contexts.ItemCardViews.Where(Item => Item.IsDeleted==0).Any(xx=> xx.Name == txtName.Text);
            if (TestItemName)
            {
                   
                    MessageBox.Show(" تم  تسجيل الصنف  من قبل");
                
                return;
            }
                if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtPrice.Text))
                {


                    MessageBox.Show("يوجد حقول فارغة");
                    return;


                }

            }




            if (Application.OpenForms.OfType<frmPerfumItemCard>().Any())
            {
                frmPerfumItemCard frm = (frmPerfumItemCard)Application.OpenForms["frmPerfumItemCard"];
              
 
                double price = 0;
                //double priceBuy = 0;

                frm.gvItemCard.AddNewRow();
                
                bool TestUpdate = context.ItemCards.Any(Item =>   Item.Name == txtName.Text);
                Int64? MaxCode = context.ItemCards.Max(u => (Int64?)u.ItemCode);
                if (MaxCode == null)
                {
                    MaxCode = 1;
                }
                else
                {
                    MaxCode += 1;
                }

                //if (string.IsNullOrWhiteSpace(txtPriceBuy.Text))
                //{
                //    priceBuy = 0;

                //}
                //else
                //{

                //    priceBuy = double.Parse(txtPriceBuy.Text);

                //}


                if (string.IsNullOrWhiteSpace(txtPrice.Text))
                {

                    price = 0;
                }
                else
                {

                    price = double.Parse(txtPrice.Text);

                }

                if (string.IsNullOrWhiteSpace(txtRisklimit.Text))
                {
                    txtRisklimit.Text = "0";
                }

                ItemCard _ItemCard = new ItemCard()
                {
                    ItemCode = Convert.ToInt64(MaxCode),
                    CategoryCode = Convert.ToInt64(SlkCatgoryName.EditValue),
                    ParCode = "0",
                    Name = txtName.Text,
                    Price = price,
                    Item_Count_InStoreg = 0,
                    PriceBuy = 0,
                    UnitCode = Convert.ToInt64(SlkUnit.EditValue),
                    //AddItem = chkAddItem.Checked,
                    AddItem =true,
                    Item_Risk_limit = Convert.ToDouble(txtRisklimit.Text)
                };
                context.ItemCards.Add(_ItemCard);
                context.SaveChanges();
                txtName.Text = "";

                //HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
                var result = (from a in context.ItemCardViews where a.IsDeleted==0 select a).ToList();
                frm.gcItemCard.DataSource = result;
                frm.gcItemCard.RefreshDataSource();
              //  SlkCatgoryName.Focus();
                 txtName.Focus();
                if (frm.gvItemCard.RowCount > 0)
                {

                    
                    frm.gcItemCard.Enabled = true;
                }


                frm.CheckGridDataCount();

     

            }
        }






        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void frmAddItem_Load(object sender, EventArgs e)
        {
            GetAllCatgory();
            GetAllUnitCard();
        }

         
    }
}