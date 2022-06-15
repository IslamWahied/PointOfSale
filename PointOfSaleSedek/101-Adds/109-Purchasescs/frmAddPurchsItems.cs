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

using PointOfSaleSedek._102_MaterialSkin;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmAddPurchsItems : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities2 context = new PointOfSaleEntities2();
        public frmAddPurchsItems()
        {
            InitializeComponent();
            txtBoxNumber.Enabled = false;
            txtUnitForBoxNumber.Enabled = false;
            FillSlkItems();
            txtFinalUnitsNumber.Text = "1";


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    ItemCardView item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text);
            //    if (item != null)
            //    {
            //        txtName.Text = item.Name;
            //        txtUnit.Text = item.UnitName;
            //        txtPrice.Text = item.Price.ToString();
            //        txtCatgoryName.Text = item.CategoryName;
            //        txtPriceBuy.Text = item.PriceBuy.ToString();
            //    }
            //    else
            //    {
            //        Rest();

            //    }

            //}
            //catch
            //{

            //}
        }
        public void FillSlkItems()
        {
            DataTable dt = new DataTable();
            var result = context.ItemCardViews.Where(user => user.IsDeleted == 0).ToList();
            slkItem.Properties.DataSource = result;
            slkItem.Properties.ValueMember = "ItemCode";
            slkItem.Properties.DisplayMember = "Name";

        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBox.Checked == true)
            {

                chkUnits.Checked = false;
                txtBoxNumber.Enabled = true;
                txtUnitForBoxNumber.Enabled = true;
                txtFinalUnitsNumber.Enabled = false;
                txtFinalUnitsNumber.ResetText();
            }
            else
            {
                chkUnits.Checked = true;

            }
        }

        private void chkUnits_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUnits.Checked == true)
            {

                chkBox.Checked = false;
                txtBoxNumber.ResetText();
                txtUnitForBoxNumber.ResetText();
                txtBoxNumber.Enabled = false;
                txtUnitForBoxNumber.Enabled = false;

                txtFinalUnitsNumber.Enabled = true;
                txtFinalUnitsNumber.Text = Convert.ToString(1);
            }
            else if (chkUnits.Checked == false)
            {

                chkBox.Checked = true;


            }
        }

        private void txtBoxNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBox.Checked == true)
                {
                    if (string.IsNullOrWhiteSpace(txtUnitForBoxNumber.Text))
                    {

                        txtUnitForBoxNumber.Text = Convert.ToString(1);


                    }
                    if (string.IsNullOrWhiteSpace(txtBoxNumber.Text))
                    {

                        txtBoxNumber.Text = Convert.ToString(1);


                    }
                    txtFinalUnitsNumber.Text = Convert.ToString((Convert.ToDouble(txtBoxNumber.Text)) * (Convert.ToDouble(txtUnitForBoxNumber.Text)));
                }

            }
            catch
            {
                txtBoxNumber.Text = 1.ToString();
                txtUnitForBoxNumber.Text = 1.ToString();
                txtFinalUnitsNumber.Text = 1.ToString();

            }
        }

        private void txtUnitForBoxNumber_EditValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (chkBox.Checked == true)
                {
                    if (string.IsNullOrWhiteSpace(txtUnitForBoxNumber.Text))
                    {

                        txtUnitForBoxNumber.Text = Convert.ToString(1);


                    }
                    if (string.IsNullOrWhiteSpace(txtBoxNumber.Text))
                    {

                        txtBoxNumber.Text = Convert.ToString(1);


                    }
                    txtFinalUnitsNumber.Text = Convert.ToString((Convert.ToDouble(txtBoxNumber.Text)) * (Convert.ToDouble(txtUnitForBoxNumber.Text)));

                }
            }
            catch
            {
                txtBoxNumber.Text = 1.ToString();
                txtUnitForBoxNumber.Text = 1.ToString();
                txtFinalUnitsNumber.Text = 1.ToString();
            }
        }

        private void txtFinalUnitsNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (chkUnits.Checked == true)
            {

                if (string.IsNullOrWhiteSpace(txtFinalUnitsNumber.Text))
                {


                    txtFinalUnitsNumber.Text = Convert.ToString(1);

                }


            }
        }


        private void frmAddPurchsItems_Load(object sender, EventArgs e)
        {
            slkItem.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrWhiteSpace(slkItem.Text))
                {
                    Int64 ItemCode = Convert.ToInt64(slkItem.EditValue);
                    ItemCardView item1 = context.ItemCardViews.FirstOrDefault(x => x.ItemCode == ItemCode);

                    if (item1 != null)
                    {


                        List<SaleDetailView> DetailList = new List<SaleDetailView>();

                        if (Application.OpenForms.OfType<frmPurchasescs>().Any())
                        {

                            frmPurchasescs frm = (frmPurchasescs)Application.OpenForms["frmPurchasescs"];
                            if (frm.gvItemCard.RowCount > 0)
                            {
                                var DetailListV = frm.gcItemCard.DataSource as List<SaleDetailView>;

                                var x = DetailListV.Any(xx => xx.ItemCode == item1.ItemCode);

                                if (x)
                                {
                                    DetailListV.ForEach(xx =>
                                    {
                                        if (xx.ItemCode == item1.ItemCode)
                                        {

                                            xx.Qty += double.Parse(txtFinalUnitsNumber.Text);
                                            xx.Total = Convert.ToDouble(xx.Qty) * Convert.ToDouble(xx.PriceBuy);

                                        }


                                    });


                                }
                                else
                                {


                                    SaleDetailView Detail = new SaleDetailView
                                    {

                                        ParCode = item1.ParCode,
                                        Name = item1.Name,
                                        UnitName = item1.UnitName,
                                        Price = Convert.ToDouble(txtPrice.Text),
                                        PriceBuy = Convert.ToDouble(txtPriceBuy.Text),
                                        Qty = Convert.ToDouble(txtFinalUnitsNumber.Text),
                                        Total = Convert.ToDouble(txtFinalUnitsNumber.Text) * Convert.ToDouble(txtPriceBuy.Text),

                                        CategoryCode = item1.CategoryCode,
                                        ItemCode = item1.ItemCode,
                                        Operation_Type_Id = 2,
                                        EntryDate = DateTime.Now,
                                        UnitCode = item1.UnitCode,
                                        CategoryName = item1.CategoryName,


                                    };
                                    DetailListV.Add(Detail);

                                }

                                frm.gcItemCard.DataSource = DetailListV;
                                frm.gcItemCard.RefreshDataSource();
                                Rest();

                            }

                            else
                            {

                                try
                                {
                                    List<SaleDetailView> DetailListV2 = new List<SaleDetailView>();
                                    if (item1 != null)
                                    {

                                        SaleDetailView Detail = new SaleDetailView
                                        {

                                            ParCode = item1.ParCode,
                                            Name = item1.Name,
                                            UnitName = item1.UnitName,
                                            Price = Convert.ToDouble(txtPrice.Text),
                                            PriceBuy = Convert.ToDouble(txtPriceBuy.Text),
                                            Qty = Convert.ToDouble(txtFinalUnitsNumber.Text),
                                            Total = Convert.ToDouble(txtFinalUnitsNumber.Text) * Convert.ToDouble(txtPriceBuy.Text),

                                            CategoryCode = item1.CategoryCode,
                                            ItemCode = item1.ItemCode,
                                            Operation_Type_Id = 2,
                                            EntryDate = DateTime.Now,
                                            UnitCode = item1.UnitCode,
                                            CategoryName = item1.CategoryName,


                                        };
                                        DetailListV2.Add(Detail);
                                        frm.gcItemCard.DataSource = DetailListV2;
                                        frm.gcItemCard.RefreshDataSource();
                                        Rest();

                                    }


                                }
                                catch
                                {


                                }


                            }


                        }

                    }
                }
                else
                {
                    MaterialMessageBox.Show("برجاء اختيار منتج", MessageBoxButtons.OK);
                    return;

                }


            }
            catch
            {

            }


            Rest();

        }
        void Rest()
        {


            //txtParCode.ResetText();
            txtPrice.ResetText();
            txtUnit.ResetText();
            txtPriceBuy.ResetText();
            txtName.ResetText();
            txtFinalUnitsNumber.Text = "1";
            //txtFinalUnitsNumber.ResetText();
            txtUnitForBoxNumber.ResetText();
            slkItem.Text="";
            txtCatgoryName.ResetText();
            chkBox.Checked = false;
            chkUnits.Checked = true;
            slkItem.Focus();

        }

        private void جديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rest();
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
                        txtName.Text = item.Name;
                        txtUnit.Text = item.UnitName;
                        txtPrice.Text = item.Price.ToString();
                        txtCatgoryName.Text = item.CategoryName;
                        txtPriceBuy.Text = item.PriceBuy.ToString();
                        txtFinalUnitsNumber.Text = "1";
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
    }
}