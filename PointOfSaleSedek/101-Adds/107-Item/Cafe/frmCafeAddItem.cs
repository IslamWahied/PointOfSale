using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeAddItem : MaterialSkin.Controls.MaterialForm
    {

        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static(); 

        public frmCafeAddItem()
        {
            InitializeComponent();
            langu();
            GetAllCatgory();
            GetAllUnitCard();

        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.groupControl1.Text =   st.isEnglish() ? "Add Item" : "اضافة صنف";
            tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            groupControl1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;



            labelControl5.Text = st.isEnglish() ? "Category" : "المجموعة";
            labelControl1.Text = st.isEnglish() ? "BarCode" : "الباركود";
       
            labelControl3.Text = st.isEnglish() ? "Unit" : "وحدة القياس";
            lblRisklimit.Text = st.isEnglish() ? "Danger Limit" : "حد الخطر";
            labelControl4.Text = st.isEnglish() ? "Selling Price" : "سعر البيع";
            labelControl6.Text = st.isEnglish() ? "Add the item to the cashier screen" : "اضافة الصنف الي شاشة الكاشير";

            gridColumn1.Caption = st.isEnglish() ? "Code" : "التسلسل";
            gridColumn2.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";
    


            labelControl2.Text = st.isEnglish() ? "Arabic Name" : "الاسم بالعربية";
            labelControl7.Text = st.isEnglish() ? "English Name" : "الاسم بالانجليزية";


            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
        }

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

            if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(txtEngName.Text) || string.IsNullOrWhiteSpace(txtArbName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtPrice.Text))
            {


                //MaterialMessageBox.Show("يوجد حقول فارغة", MessageBoxButtons.OK);
                MessageBox.Show(st.isEnglish() ? "There are empty fields" : "يوجد حقول فارغة");
                return;


            }


            using (POSEntity Contexts = new POSEntity())
            {


                bool TestItemName = Contexts.ItemCardViews.Where(Item => Item.IsDeleted==0 && Item.Branch_Code == brachCode).Any(xx=> xx.Name == txtEngName.Text || xx.ParCode == txtParCode.Text || xx.Name_En == txtArbName.Text);
                 if (TestItemName)
            {
                   
                    MessageBox.Show(st.isEnglish()?"Item has already been registered":"تم  تسجيل الصنف  من قبل");
                
                return;
            }

               else if (string.IsNullOrEmpty(txtParCode.Text)) {
                    try
                    {
                        Int64? MaxCode = context.ItemCardViews.AsEnumerable().Max(u => Convert.ToInt64(u.ParCode) + 1);
                        if (MaxCode == null || MaxCode == 0)
                        {
                            txtParCode.Text = "1";
                        }
                        else
                        {
                            txtParCode.Text = MaxCode.ToString();
                        }
                    }
                    catch {

                        txtParCode.Text = "1";

                    }

                 
                }

                else if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtArbName.Text) || string.IsNullOrEmpty(txtEngName.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtParCode.Text))
                {


                    MessageBox.Show(st.isEnglish() ? "There are empty fields" :"يوجد حقول فارغة");
                    return;


                }

            }




            if (Application.OpenForms.OfType<frmCafeItemCard>().Any())
            {
                frmCafeItemCard frm = (frmCafeItemCard)Application.OpenForms["frmCafeItemCard"];
              
 
                double price = 0;
                //double priceBuy = 0;

                frm.gvCafeItemCard.AddNewRow();
                
                bool TestUpdate = context.ItemCards.Any(Item => Item.ParCode == txtParCode.Text && Item.Branch_Code == brachCode && (Item.Name == txtArbName.Text|| Item.Name_En == txtEngName.Text));
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
                    ParCode = txtParCode.Text,
                    Name = txtArbName.Text,
                    Name_En = txtEngName.Text,
                    Price = price,
                    Item_Count_InStoreg = 0,
                    Branch_Code = brachCode,
                    
                    PriceBuy = 0,
                    UnitCode = Convert.ToInt64(SlkUnit.EditValue),
                    AddItem = chkAddItem.Checked,
                    Item_Risk_limit = Convert.ToDouble(txtRisklimit.Text)
                };
                context.ItemCards.Add(_ItemCard);
                context.SaveChanges();
                HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
                var result = (from a in context.ItemCardViews where a.IsDeleted==0 select a).ToList();
                frm.gcItemCard.DataSource = result;
                frm.gcItemCard.RefreshDataSource();
                SlkCatgoryName.Focus();
                if (frm.gvCafeItemCard.RowCount > 0)
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

        private void labelControl6_Click(object sender, EventArgs e)
        {
            chkAddItem.Checked = !chkAddItem.Checked;
        }
    }
}