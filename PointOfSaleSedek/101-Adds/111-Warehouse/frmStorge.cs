 
using DevExpress.XtraEditors;
using PointOfSaleSedek._102_MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010;
using DataRep;
using PointOfSaleSedek.HelperClass;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmStorge : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        Static st = new Static();
        public frmStorge()
        {
            InitializeComponent();

            langu();

            //FillGride();
            FillslkWarhouse();

            //if (gvItemCard.RowCount <= 0)
            //{

            //    gcItemCard.Enabled = false;
            //    //groupControl1.CustomHeaderButtons[0].Properties.Enabled = true;
            //    //groupControl1.CustomHeaderButtons[1].Properties.Enabled = false;
            //    //groupControl1.CustomHeaderButtons[2].Properties.Enabled = false;
            //}
        }

        public void FillslkWarhouse()
        {
            DataTable dt = new DataTable();
            var result = context.Warehouses.Where(user => user.isDelete == 0).ToList();
            slkWarhouse.Properties.DataSource = result;
            slkWarhouse.Properties.ValueMember = "Warehouse_Code";
            slkWarhouse.Properties.DisplayMember = "Warehouse_Name";

            Int64 branchCode = st.GetBranch_Code();
            if (branchCode != 0) {
                slkWarhouse.EditValue = branchCode;
                slkWarhouse.Enabled = false;
                Fill_BackOffice_Gride(branchCode);
            }



        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            gridColumn7.Caption = st.isEnglish() ? "Category" : "المجموعة";
            gridColumn1.Caption = st.isEnglish() ? "Code" : "الكود";
            gridColumn10.Caption = st.isEnglish() ? "ParCode" : "الباركود";
            gridColumn2.Caption = st.isEnglish() ? "Arabic Name" : "الاسم بالعربية";
            gridColumn14.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn3.Caption = st.isEnglish() ? "Unit" : "وحدة القياس";
            gridColumn11.Caption = st.isEnglish() ? "Purchasing price" : "سعر الشراء";
            labelControl4.Text = st.isEnglish() ? "Warehouse" : "المخزن";
            gridColumn4.Caption = st.isEnglish() ? "Selling Price" : "سعر البيع";
            gridColumn12.Caption = st.isEnglish() ? "Danger Limit" : "حد الخطر";
            gridColumn13.Caption = st.isEnglish() ? "Count" : "العدد";
            windowsUIButtonPanel.Buttons[7].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
            gvItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
        }



        private void windowsUIButtonPanel_ButtonClick(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {


                if (MaterialMessageBox.Show(st.isEnglish() ? "Confirm exit" : "تاكيد الاغلاق", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    this.Close();

                }


            }
        }
          

       

       
      
       
        private void gvItemCard_RowStyle_1(object sender, RowStyleEventArgs e)
        {
            decimal Item_Risk_limit = Convert.ToDecimal(gvItemCard.GetRowCellValue(e.RowHandle, "Item_Risk_limit"));
            decimal Item_Count_InStoreg = Convert.ToDecimal(gvItemCard.GetRowCellValue(e.RowHandle, "Item_Count_InStoreg"));
            string ItemName = Convert.ToString(gvItemCard.GetRowCellValue(e.RowHandle, "Name"));

            if (Item_Risk_limit == Item_Count_InStoreg && !string.IsNullOrWhiteSpace(ItemName))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
            else if (Item_Risk_limit > Item_Count_InStoreg && !string.IsNullOrWhiteSpace(ItemName))
            {
                e.Appearance.BackColor = Color.Red;


            }


            e.HighPriority = true;
        }


        public void Fill_BackOffice_Gride(Int64 branchCode)
        {
            List<ItemCardView> _ItemCardView = new List<ItemCardView>();

            using (POSEntity _context23 = new POSEntity())
            {
             
                var GetAllItems = _context23.ItemCardViews.Where(x => x.IsDeleted == 0 && x.Branch_Code == branchCode).ToList();
                var ItemTransactions = _context23.Item_History.Where(x => !x.IsFinshed && x.Warhouse_Code == branchCode).ToList();

                foreach (var item1 in GetAllItems)
                {


                    GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Item_Count_InStoreg = ItemTransactions.Where(x => x.Item_Id == item1.ItemCode).Sum(xx => xx.Current_Qty_Now);
                    GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Last_Update_Date = DateTime.Now;

                }

                gcItemCard.DataSource = GetAllItems;
                gcItemCard.RefreshDataSource();
            }


        }

        public void Fill_Branch_Gride(Int64 warhousCode)
        {
            Int64 branchCOde = Convert.ToInt64(st.GetBranch_Code());
            List<ItemCardView> _ItemCardView = new List<ItemCardView>();

            using (POSEntity _context23 = new POSEntity())
            {
                var GetAllItems = _context23.ItemCardViews.Where(x => x.IsDeleted == 0 && x.Branch_Code == branchCOde).ToList();
                BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities();
                var item_Balance = _server.Item_Balance.Where(x => x.WareHouse_Code == warhousCode && x.Branch_Code == warhousCode && x.IsDeleted == 0).ToList();

                foreach (var item1 in GetAllItems)
                {
                    try
                    {

                        GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Item_Count_InStoreg = item_Balance.Where(x => x.Item_Code == item1.ItemCode).Sum(xx => xx.Balance);
                    }
                    catch
                    {
                    }
                    try
                    {
                        GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Price = item_Balance.FirstOrDefault(x => x.Item_Code == item1.ItemCode).Price;
                    }
                    catch
                    {
                    }

                   
                    

                    try
                    {

                        DateTime lasstDate = item_Balance.FirstOrDefault(x => x.Item_Code == item1.ItemCode).Last_Update;
                        GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Last_Update_Date = lasstDate;
                    }
                    catch
                    {
                    }


                }

                gcItemCard.DataSource = GetAllItems;
                gcItemCard.RefreshDataSource();
            }


        }

        private void slkWarhouse_EditValueChanged(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));

            Int64 warhouseCode = Convert.ToInt64(slkWarhouse.EditValue);
            try
            {
                if (warhouseCode == 0)
                {
                    Fill_BackOffice_Gride(0);

                }
                else
                {
                    Fill_Branch_Gride(warhouseCode);

                }
            }
            catch
            {
                gcItemCard.DataSource = null;
                gcItemCard.RefreshDataSource();
            }
            SplashScreenManager.CloseForm();
        }

        private void gvItemCard_RowStyle(object sender, RowStyleEventArgs e)
        {
            decimal Item_Risk_limit = Convert.ToDecimal(gvItemCard.GetRowCellValue(e.RowHandle, "Item_Risk_limit"));
            decimal Item_Count_InStoreg = Convert.ToDecimal(gvItemCard.GetRowCellValue(e.RowHandle, "Item_Count_InStoreg"));
            string ItemName = Convert.ToString(gvItemCard.GetRowCellValue(e.RowHandle, "Name"));

            if (Item_Risk_limit == Item_Count_InStoreg && !string.IsNullOrWhiteSpace(ItemName))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
            else if (Item_Risk_limit > Item_Count_InStoreg && !string.IsNullOrWhiteSpace(ItemName))
            {
                e.Appearance.BackColor = Color.Red;


            }


            e.HighPriority = true;
        }

        private void windowsUIButtonPanel_ButtonClick_1(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {


                if (MaterialMessageBox.Show(st.isEnglish() ? "Confirm exit" : "تاكيد الاغلاق", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    this.Close();

                }


            }
        }
    }

}