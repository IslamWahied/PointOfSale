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
using EntityData;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmSearchItems : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities2 context = new PointOfSaleEntities2();
        public frmSearchItems()
        {
            InitializeComponent();
            FillSlkItems();
            slkItem.Focus();
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
           if (Application.OpenForms.OfType<frmSales>().Any())
           {
                

               frmSales frm = (frmSales)Application.OpenForms["frmSales"];

                List<SaleDetailView> gcData = new  List<SaleDetailView>();
                
               
                var grd = frm.gcSaleDetail.DataSource as List<SaleDetailView>;

                if (grd != null)
                { 
                gcData = grd;
                
                }



                var itemcode = Convert.ToInt64(slkItem.EditValue);

                var RowCount = frm.gvSaleDetail.RowCount;


                ItemCardView ii = context.ItemCardViews.Where(x => x.ItemCode == itemcode && x.IsDeleted == 0).FirstOrDefault();
                if (ii == null)
                {

                    return;

                }



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
                txtParCode.ResetText();
                
                txtParCode.Focus();
                 

                this.Close();
           }
        }
    }
}