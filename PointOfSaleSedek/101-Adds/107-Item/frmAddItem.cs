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
using DevExpress.Utils.Serializing;
using DevExpress.XtraSplashScreen;
using EntityData;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmAddItem : MaterialSkin.Controls.MaterialForm
    {

        readonly PointOfSaleEntities context = new PointOfSaleEntities();
        public frmAddItem()
        {
            InitializeComponent();

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

            using (PointOfSaleEntities Contexts = new PointOfSaleEntities())
            {


                bool TestItemName = Contexts.ItemCardViews.Where(Item => Item.IsDeleted==0).Any(xx=> xx.Name == txtName.Text || xx.ParCode == txtParCode.Text);
            if (TestItemName)
            {
                   
                    MessageBox.Show(" تم  تسجيل الصنف  من قبل");
                
                return;
            }
                if (string.IsNullOrWhiteSpace(SlkCatgoryName.Text) || string.IsNullOrWhiteSpace(SlkUnit.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtParCode.Text))
                {


                    MessageBox.Show("يوجد حقول فارغة");
                    return;


                }

            }




            if (Application.OpenForms.OfType<frmItemCard>().Any())
            {
                frmItemCard frm = (frmItemCard)Application.OpenForms["frmItemCard"];
              
 
                double price = 0;
                //double priceBuy = 0;

                frm.gvItemCard.AddNewRow();
                
                bool TestUpdate = context.ItemCards.Any(Item => Item.ParCode == txtParCode.Text && Item.Name == txtName.Text);
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
                    Name = txtName.Text,
                    Price = price,
                    Item_Count_InStoreg = 0,
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