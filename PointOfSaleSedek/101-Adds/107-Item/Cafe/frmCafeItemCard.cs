using System;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
 
using PointOfSaleSedek._102_MaterialSkin;
 
using DevExpress.XtraBars.Docking2010;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeItemCard : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmCafeItemCard()
        {
            InitializeComponent();
            langu();



        }


        void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

            gridColumn1.Caption = st.isEnglish() ? "Code" : "التسلسل";
            gridColumn7.Caption = st.isEnglish() ? "Category" : "المجموعه";
            gridColumn10.Caption = st.isEnglish() ? "BarCode" : "الباركود";
            gridColumn2.Caption = st.isEnglish() ? "Arabic Name" : "الاسم بالعربية";
            gridColumn13.Caption = st.isEnglish() ? "English Name" : "الاسم بالانجليزية";
            gridColumn3.Caption = st.isEnglish() ? "Unit" : "وحدة القياس";
            gridColumn4.Caption = st.isEnglish() ? "Selling Price" : "سعر البيع";
            gridColumn12.Caption = st.isEnglish() ? "Danger Limit" : "حد الخطر";
            gvCafeItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
           
            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "New" : "جديد";
            windowsUIButtonPanel.Buttons[1].Properties.Caption = st.isEnglish() ? "Edite" : "تعديل";
            windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Delete" : "حذف";
            windowsUIButtonPanel.Buttons[3].Properties.Caption = st.isEnglish() ? "Deleted History" : "منتجات تم حذفها";
            windowsUIButtonPanel.Buttons[4].Properties.Caption = st.isEnglish() ? "Refresh" : "تحديث";
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Print" : "طباعة";
            windowsUIButtonPanel.Buttons[7].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
 
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "New" : "جديد";
            materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Edite" : "تعديل";
            materialContextMenuStrip1.Items[2].Text = st.isEnglish() ? "Delete" : "حذف";
        }

        private void اضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCafeAddItem frm = new frmCafeAddItem();
            frm.ShowDialog();
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCafeEditeItem frm = new frmCafeEditeItem();

            var x = gvCafeItemCard.GetFocusedRow() as ItemCardView;


            frm.SlkCatgoryName.EditValue = x.CategoryCode;
            frm.SlkUnit.EditValue = x.UnitCode;
            frm.txtArbName.Text = x.Name;
            frm.txtEngName.Text = x.Name_En;
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


        void deleteItemAction() {
            if (gvCafeItemCard.RowCount <= 0)
            {


                return;
            }
            
                var brachCode = st.GetBranch_Code();
                if (MaterialMessageBox.Show(st.isEnglish() ? "Are you sure to delete this Item?" : "تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    ItemCardView xx = gvCafeItemCard.GetFocusedRow() as ItemCardView;
                    bool checkIfItemUsed = context.SaleDetails.Any(x => x.IsDeleted == 0 && x.ItemCode == xx.ItemCode);


                    if (checkIfItemUsed)
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "The item cannot be deleted because there are invoices associated with it" : "لا يمكن حذف العنصر بسبب وجود فواتير مربوطه به", MessageBoxButtons.OK);
                    }
                    else
                    {
                        ItemCard _ItemCard = new ItemCard();
                        _ItemCard = context.ItemCards.SingleOrDefault(item => item.ItemCode == xx.ItemCode);
                        _ItemCard.IsDeleted = 1;

                        context.SaveChanges();
                        using (POSEntity Contexts = new POSEntity())
                        {

                            var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 && a.Branch_Code == brachCode select a).ToList();
                            gcItemCard.DataSource = result;
                            gcItemCard.RefreshDataSource();
                            MaterialMessageBox.Show(st.isEnglish() ? "Deleted Successfully" : "تم الحذف", MessageBoxButtons.OK);
                            CheckGridDataCount();
                        }
                    }


                }
            
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            deleteItemAction();
        }


      public  void fillgrid()
        {
            var brachCode = st.GetBranch_Code();
            var result = (from a in context.ItemCardViews where a.IsDeleted == 0 && a.Branch_Code == brachCode select a).ToList();
            gcItemCard.DataSource = result;
            CheckGridDataCount();

        }

        private void frmItemCard_Load(object sender, EventArgs e)
        {
            fillgrid();



        }

        
        private void groupControl1_CustomButtonClick_1(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var brachCode = st.GetBranch_Code();
            if (e.Button.Properties.Caption == "جديد" || e.Button.Properties.Caption == "New")
            {

                frmCafeAddItem frm = new frmCafeAddItem();
                frm.Show();


            }

            if (e.Button.Properties.Caption == "تعديل" || e.Button.Properties.Caption == "Edite")
            {

                if (gvCafeItemCard.RowCount <= 0)
                {


                    return;
                }
                frmCafeEditeItem frm = new frmCafeEditeItem();

                var x = gcItemCard.DataSource as ItemCardView;

                frm.SlkCatgoryName.EditValue = x.CategoryCode;
                frm.SlkUnit.EditValue = x.UnitCode;
                frm.txtArbName.Text = x.Name;
                frm.txtEngName.Text = x.Name_En;
                frm.txtItemRisklimit.Text = x.Item_Risk_limit.ToString();
                frm.txtPrice.Text = x.Price.ToString();
            //    frm.txtPriceBuy.Text = x.PriceBuy.ToString();
                frm.txtParCode.Text = x.ParCode;
                frm.chkAddItem.Checked = (bool)x.AddItem;


                frm.Show();


            }

            if (e.Button.Properties.Caption == "حذف" || e.Button.Properties.Caption == "Delete")
            {
                deleteItemAction();
            }
        }

      

        private void windowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            var brachCode = st.GetBranch_Code();
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد" || btn.Caption == "New")
            {
                frmCafeAddItem frm = new frmCafeAddItem();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل" || btn.Caption == "Edite")
            {

                if (gvCafeItemCard.RowCount <= 0)
                {
                    return;
                }

                frmCafeEditeItem frm = new frmCafeEditeItem();

                var x = gvCafeItemCard.GetFocusedRow() as ItemCardView;


                frm.SlkCatgoryName.EditValue = x.CategoryCode;
                frm.SlkUnit.EditValue = x.UnitCode;
                frm.txtArbName.Text = x.Name;
                frm.txtEngName.Text = x.Name_En;
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

            if (btn.Caption == "منتجات تم حذفها" || btn.Caption == "Deleted History")
            {
                frmItemDeletedHistory frm = new frmItemDeletedHistory();
                frm.ShowDialog();
            }
            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف" || btn.Caption == "Delete")
            {

                if (gvCafeItemCard.RowCount <= 0)
                {
                     

                    return;
                }
                if (MaterialMessageBox.Show(st.isEnglish() ? "Are you sure to delete this Item?" : "تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    ItemCardView xx = gvCafeItemCard.GetFocusedRow() as ItemCardView;


                    using (POSEntity Contexts = new POSEntity())
                    {
                        ItemCard _ItemCard = new ItemCard();
                        _ItemCard = Contexts.ItemCards.SingleOrDefault(item => item.ItemCode == xx.ItemCode);
                        Contexts.ItemCards.Remove(_ItemCard);
                        Contexts.SaveChanges();
                        var result = (from a in Contexts.ItemCardViews where a.IsDeleted==0 && a.Branch_Code == brachCode select a).ToList();
                        gcItemCard.DataSource = result;
                        gcItemCard.RefreshDataSource();
                        MaterialMessageBox.Show(st.isEnglish() ? "Are you sure to delete this Item?" : "تم الحذف", MessageBoxButtons.OK);
                        if (gvCafeItemCard.RowCount <= 0)
                        {
                            gcItemCard.Enabled = false;
                            windowsUIButtonPanel.Buttons.ForEach(x =>
                            {

                                if (x.Properties.Caption == "تعديل" || btn.Caption == "Edite" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "Print" || x.Properties.Caption == "حذف" || e.Button.Properties.Caption == "Delete")
                                {

                                    x.Properties.Enabled = false;

                                }
                                if (x.Properties.Caption == "جديد" || x.Properties.Caption == "New")
                                {

                                    x.Properties.Enabled = true;

                                }

                            });

                            return;
                        }
                    }

                }


            }
          
            else if (btn.Caption == "تحديث" || btn.Caption == "Update")
            {

                using (POSEntity Contexts = new POSEntity())
                {
                    
                    var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 && a.Branch_Code == brachCode select a).ToList();
                    gcItemCard.DataSource = result;
                    gcItemCard.RefreshDataSource();

                    if (gvCafeItemCard.RowCount <= 0)
                    {

                        gcItemCard.Enabled = false;
                        windowsUIButtonPanel.Buttons.ForEach(x =>
                        {

                            if (x.Properties.Caption == "تعديل" || btn.Caption == "Edite" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "Print" || x.Properties.Caption == "حذف" || e.Button.Properties.Caption == "Delete")
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


            else if (btn.Caption == "طباعة" || btn.Caption == "Print")
            {

                if (gvCafeItemCard.RowCount <= 0)
                {


                    return;
                }

                using (POSEntity Contexts = new POSEntity())
                {

                    var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 && a.Branch_Code == brachCode select a).ToList();
                    gcItemCard.DataSource = result;
                    gcItemCard.RefreshDataSource();

                    if (gvCafeItemCard.RowCount <= 0)
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

            if (gvCafeItemCard.RowCount <= 0)
            {

                gcItemCard.Enabled = false;
                windowsUIButtonPanel.Buttons.ForEach(x =>
                {

                    if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "Edite" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "Print" || x.Properties.Caption == "حذف" || x.Properties.Caption == "Delete")
                    {

                        x.Properties.Enabled = false;

                    }
                    if (x.Properties.Caption == "جديد" || x.Properties.Caption == "New")
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

                    if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "Edite" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "Print" || x.Properties.Caption == "حذف" || x.Properties.Caption == "Delete")
                    {

                        x.Properties.Enabled = true;

                    }
                    if (x.Properties.Caption == "جديد" || x.Properties.Caption == "New")
                    {

                        x.Properties.Enabled = true;

                    }

                });


            }

        }
    }
}