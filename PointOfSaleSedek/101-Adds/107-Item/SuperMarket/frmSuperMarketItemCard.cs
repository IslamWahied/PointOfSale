using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
 
using System.Windows.Forms;
 
using PointOfSaleSedek._102_MaterialSkin;

using DevExpress.XtraBars.Docking2010;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmSuperMarketItemCard : DevExpress.XtraEditors.XtraForm
    {
        readonly SaleEntities context = new SaleEntities();
        public frmSuperMarketItemCard()
        {
            InitializeComponent();

           

        }

         
        private void اضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSuperMarketAddItem frm = new frmSuperMarketAddItem();
            frm.ShowDialog();
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSuperMarketEditeItem frm = new frmSuperMarketEditeItem();

            var x = gvItemCard.GetFocusedRow() as ItemCardView;


            frm.SlkCatgoryName.EditValue = x.CategoryCode;
            frm.SlkUnit.EditValue = x.UnitCode;
            frm.txtName.Text = x.Name;
            frm.txtPrice.Text = x.Price.ToString();
            frm.ItemCode = Convert.ToInt64(x.ItemCode);
            frm.txtParCode.Text = x.ParCode.ToString();
         //   frm.txtPriceBuy.Text = x.PriceBuy.ToString();
            frm.txtItemRisklimit.Text = x.Item_Risk_limit.ToString();
            frm.chkAddItem.Checked = (bool)x.AddItem;
            frm.txtParCode.ReadOnly = true;
            frm.SlkCatgoryName.Focus();


            frm.ShowDialog();

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
            {

                ItemCardView xx = gvItemCard.GetFocusedRow() as ItemCardView;


                    ItemCard _ItemCard = new ItemCard();
                _ItemCard = context.ItemCards.SingleOrDefault(item => item.ItemCode == xx.ItemCode);
                _ItemCard.IsDeleted = 1;
                    
                    context.SaveChanges();
                using (SaleEntities Contexts = new SaleEntities())
                {

                    var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 select a).ToList();
                    gcItemCard.DataSource = result;
                    gcItemCard.RefreshDataSource();
                    MaterialMessageBox.Show("تم الحذف", MessageBoxButtons.OK);
                    CheckGridDataCount();
                }

            }

        }

        private void frmItemCard_Load(object sender, EventArgs e)
        {
            var result = (from a in context.ItemCardViews where a.IsDeleted==0 select a).ToList();
            gcItemCard.DataSource = result;
            CheckGridDataCount();




        }

        
        private void groupControl1_CustomButtonClick_1(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "جديد")
            {

                frmSuperMarketAddItem frm = new frmSuperMarketAddItem();
                frm.Show();


            }

            if (e.Button.Properties.Caption == "تعديل")
            {

                if (gvItemCard.RowCount <= 0)
                {


                    return;
                }
                frmSuperMarketEditeItem frm = new frmSuperMarketEditeItem();

                var x = gcItemCard.DataSource as ItemCardView;

                frm.SlkCatgoryName.EditValue = x.CategoryCode;
                frm.SlkUnit.EditValue = x.UnitCode;
                frm.txtName.Text = x.Name;
                frm.txtItemRisklimit.Text = x.Item_Risk_limit.ToString();
                frm.txtPrice.Text = x.Price.ToString();
            //    frm.txtPriceBuy.Text = x.PriceBuy.ToString();
                frm.txtParCode.Text = x.ParCode;
                frm.chkAddItem.Checked = (bool)x.AddItem;


                frm.Show();


            }

            if (e.Button.Properties.Caption == "حذف")
            {
                if (gvItemCard.RowCount <= 0)
                {


                    return;
                }
                if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    ItemCardView xx = gvItemCard.GetFocusedRow() as ItemCardView;


                    using (SaleEntities Contexts = new SaleEntities())
                    {
                        ItemCard _ItemCard = new ItemCard();
                        _ItemCard = Contexts.ItemCards.SingleOrDefault(item => item.ItemCode == xx.ItemCode);
                        Contexts.ItemCards.Remove(_ItemCard);
                        Contexts.SaveChanges();
                        var result = (from a in Contexts.ItemCards where a.IsDeleted==0 select a).ToList();
                        gcItemCard.DataSource = result;
                        gcItemCard.RefreshDataSource();
                        MaterialMessageBox.Show("تم الحذف", MessageBoxButtons.OK);
                        if (gvItemCard.RowCount <= 0)
                        {

                            gcItemCard.Enabled = false;
                            //groupControl1.CustomHeaderButtons[0].Properties.Enabled = true;
                            //groupControl1.CustomHeaderButtons[1].Properties.Enabled = false;
                            //groupControl1.CustomHeaderButtons[2].Properties.Enabled = false;
                        }
                    }

                }
            }
        }

      

        private void windowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد")
            {
                frmSuperMarketAddItem frm = new frmSuperMarketAddItem();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل")
            {

                if (gvItemCard.RowCount <= 0)
                {
                    return;
                }

                frmSuperMarketEditeItem frm = new frmSuperMarketEditeItem();

                var x = gvItemCard.GetFocusedRow() as ItemCardView;


                frm.SlkCatgoryName.EditValue = x.CategoryCode;
                frm.SlkUnit.EditValue = x.UnitCode;
                frm.txtName.Text = x.Name;
                frm.txtPrice.Text = x.Price.ToString();
                frm.ItemCode = Convert.ToInt64(x.ItemCode);
                frm.txtParCode.Text = x.ParCode.ToString();
              //  frm.txtPriceBuy.Text = x.PriceBuy.ToString();
                frm.txtItemRisklimit.Text = x.Item_Risk_limit.ToString();
                frm.chkAddItem.Checked = (bool)x.AddItem;
                frm.txtParCode.ReadOnly = true;
                frm.SlkCatgoryName.Focus();


                frm.ShowDialog();

            }
            else if (btn.Caption == "خروج")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف")
            {

                if (gvItemCard.RowCount <= 0)
                {
                     

                    return;
                }
                if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    ItemCardView xx = gvItemCard.GetFocusedRow() as ItemCardView;


                    using (SaleEntities Contexts = new SaleEntities())
                    {
                        ItemCard _ItemCard = new ItemCard();
                        _ItemCard = Contexts.ItemCards.SingleOrDefault(item => item.ItemCode == xx.ItemCode);
                        Contexts.ItemCards.Remove(_ItemCard);
                        Contexts.SaveChanges();
                        var result = (from a in Contexts.ItemCardViews where a.IsDeleted==0 select a).ToList();
                        gcItemCard.DataSource = result;
                        gcItemCard.RefreshDataSource();
                        MaterialMessageBox.Show("تم الحذف", MessageBoxButtons.OK);
                        if (gvItemCard.RowCount <= 0)
                        {
                            gcItemCard.Enabled = false;
                            windowsUIButtonPanel.Buttons.ForEach(x =>
                            {

                                if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                                {

                                    x.Properties.Enabled = false;

                                }
                                if (x.Properties.Caption == "جديد")
                                {

                                    x.Properties.Enabled = true;

                                }

                            });

                            return;
                        }
                    }

                }


            }
          
            else if (btn.Caption == "تحديث")
            {

                using (SaleEntities Contexts = new SaleEntities())
                {
                    
                    var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 select a).ToList();
                    gcItemCard.DataSource = result;
                    gcItemCard.RefreshDataSource();

                    if (gvItemCard.RowCount <= 0)
                    {

                        gcItemCard.Enabled = false;
                        windowsUIButtonPanel.Buttons.ForEach(x =>
                        {

                            if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                            {

                                x.Properties.Enabled = false;

                            }

                        });

                        

                    }
                    else
                    {

                        gcItemCard.Enabled = true;

                    }
                }


            }


            else if (btn.Caption == "طباعة")
            {

                if (gvItemCard.RowCount <= 0)
                {


                    return;
                }

                using (SaleEntities Contexts = new SaleEntities())
                {

                    var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 select a).ToList();
                    gcItemCard.DataSource = result;
                    gcItemCard.RefreshDataSource();

                    if (gvItemCard.RowCount <= 0)
                    {

                        gcItemCard.Enabled = false;

                    }
                    else
                    {

                        gcItemCard.Enabled = true;
                    }
                }


            }
        }

      public  void CheckGridDataCount()
        {

            if (gvItemCard.RowCount <= 0)
            {

                gcItemCard.Enabled = false;
                windowsUIButtonPanel.Buttons.ForEach(x =>
                {

                    if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                    {

                        x.Properties.Enabled = false;

                    }
                    if (x.Properties.Caption == "جديد")
                    {

                        x.Properties.Enabled = true;

                    }

                });

            }
            else
            {

                gcItemCard.Enabled = true;
                windowsUIButtonPanel.Buttons.ForEach(x =>
                {

                    if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                    {

                        x.Properties.Enabled = true;

                    }
                    if (x.Properties.Caption == "جديد")
                    {

                        x.Properties.Enabled = true;

                    }

                });


            }

        }
    }
}