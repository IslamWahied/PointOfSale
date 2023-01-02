using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;
using PointOfSaleSedek._102_MaterialSkin;
using DevExpress.XtraBars.Docking2010;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmPurchasescs : DevExpress.XtraEditors.XtraForm
    {
        public string Status = "New";
        Static st = new Static();
        POSEntity Context = new POSEntity();
        public frmPurchasescs()
        {
            InitializeComponent();
            Int64 branchCode = st.GetBranch_Code();
            //HelperClass.HelperClass.DisableControls(tableLayoutPanel1);
            //HelperClass.HelperClass.DisableControls(tableLayoutPanel2);
            //Int64? MaxCode = Context.SaleMasters.Where(x => x.Operation_Type_Id == 1 & x.IsDeleted == 0).Max(u => (Int64?)u.SaleMasterCode + 1);
            //if (MaxCode == null || MaxCode == 0)
            //{
            //    MaxCode = 1;
            //}
            Int64? MaxCode = Context.SaleMasters.Where(x =>  (x.Operation_Type_Id == 1 || x.Operation_Type_Id == 6) && x.Branch_Id == branchCode).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }
            groupControl1.CustomHeaderButtons[1].Properties.Caption = MaxCode.ToString();
            langu();
        }

        void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

            gridColumn1.Caption = st.isEnglish() ? "Code" : "التسلسل";
            gridColumn7.Caption = st.isEnglish() ? "Category" : "المجموعه";
            gridColumn10.Caption = st.isEnglish() ? "BarCode" : "الباركود";
            gridColumn2.Caption = st.isEnglish() ? "Item Name" : "اسم الصنف";
            gridColumn3.Caption = st.isEnglish() ? "Unit" : "وحدة القياس";
            gridColumn4.Caption = st.isEnglish() ? "Selling Price" : "سعر البيع";
            gridColumn12.Caption = st.isEnglish() ? "Quantity" : "الكمية";
            gridColumn13.Caption = st.isEnglish() ? "Price" : "المبلغ";
            gridColumn11.Caption = st.isEnglish() ? "Purchasing price" : "حد الخطر";

            gridColumn14.Caption = st.isEnglish() ? "Warhouse" : "المخزن";
            gridColumn11.Caption = st.isEnglish() ? "Purchasing price" : "حد الخطر";
            this.gvItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            gvItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "New" : "جديد";
            windowsUIButtonPanel.Buttons[1].Properties.Caption = st.isEnglish() ? "Edite" : "تعديل";
            windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Save" : "حفظ";
    
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Search" : "بحث";
            windowsUIButtonPanel.Buttons[7].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";

            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "New" : "جديد";
            materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Edite" : "تعديل";
            materialContextMenuStrip1.Items[2].Text = st.isEnglish() ? "Delete" : "حذف";
            materialContextMenuStrip1.Items[3].Text = st.isEnglish() ? "New Invoice" : "فاتورة جديدة";

            groupControl1.CustomHeaderButtons[0].Properties.Caption = st.isEnglish() ? "Invoice No" : "فاتورة رقم";

            this.gridColumn13.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Total",st.isEnglish() ?"Total =  {0:N}": "الاجمالي =  {0:N}")});
        }




        public void SaveSaleMaster()
        {
            Int64 UserCode = st.GetUser_Code();
            var ShiftCode = Context.Shift_View.Where(x => x.User_Id == UserCode  && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64 SalMasterCode = Convert.ToInt64(groupControl1.CustomHeaderButtons[1].Properties.Caption);
            if (this.Status == "New")
            {
                List<SaleDetailView> DataFromGrid = gvItemCard.DataSource as List<SaleDetailView>;

                if (DataFromGrid != null && DataFromGrid.Count>0)
                {


                    double sum = 0;
                    double Qty = 0;

                    DataFromGrid.ForEach(x =>
                    {
                        sum += x.Total;
                        Qty += x.Qty;

                    });
               
                    SaleMaster _SaleMaster = new SaleMaster()
                    {

                        EntryDate = DateTime.Now,
                        FinalTotal = sum,
                        QtyTotal = Qty,
                        
                        SaleMasterCode = SalMasterCode,
                        Branch_Id = st.GetBranch_Code(),
                        Payment_Type = 1,
                        IsDeleted = 0,
                        UserIdTakeOrder = st.GetUser_Code(),
                        Operation_Type_Id = 1,
                        UserCode = UserCode,
                         ShiftCode = ShiftCode,
                        LastDateModif = DateTime.Now

                    };
                    Context.SaleMasters.Add(_SaleMaster);
                    Context.SaveChanges();
                }



            }
            else
            {
                var DataFromGrid = gvItemCard.DataSource as List<SaleDetailView>;
               

                if (DataFromGrid != null && DataFromGrid.Count > 0)
                {
                    Int64 branchCode = st.GetBranch_Code();

                    Context.SaleMasters.RemoveRange(Context.SaleMasters.Where(x => x.SaleMasterCode == SalMasterCode && x.Branch_Id  == branchCode && x.IsDeleted == 0 && x.Operation_Type_Id == 1));
                    Context.SaveChanges();

                    double sum = 0;
                    double Qty = 0;

                    DataFromGrid.ForEach(x =>
                    {
                        sum += x.Total;
                        Qty += x.Qty;

                    });
                    SaleMaster _SaleMaster = new SaleMaster()
                    {

                        EntryDate = DateTime.Now,
                        FinalTotal = sum,
                        
                        QtyTotal = Qty,
                        Branch_Id = branchCode,
                        SaleMasterCode = SalMasterCode,
                        Operation_Type_Id = 1,
                        Payment_Type = 1,
                        IsDeleted = 0,
                        UserIdTakeOrder = st.GetUser_Code(),
                        UserCode = UserCode,
                        ShiftCode = ShiftCode,
                        LastDateModif = DateTime.Now

                    };
                    Context.SaleMasters.Add(_SaleMaster);
                    Context.SaveChanges();

                }
            }




        }

        public void SaveSaleDetail()
        {
            Int64 SalMasterCode = Convert.ToInt64(groupControl1.CustomHeaderButtons[1].Properties.Caption);
            if (this.Status == "New")
            {
                List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();
                List<Item_History> ArryOfItem_History = new List<Item_History>();
                var GetDataFromGrid = gcItemCard.DataSource as List<SaleDetailView>;
                if (GetDataFromGrid != null && GetDataFromGrid.Count > 0)
                {


                    foreach (var item in GetDataFromGrid)
                    {
                        SaleDetail _SaleDetail = new SaleDetail()
                        {
                            ItemCode = item.ItemCode,
                            Price = item.PriceBuy,
                            Branch_Id = item.Branches_Code,
                            shiftCode = item.shiftCode,
                            Warhouse_Code = item.Warhouse_Code,
                            
                            Qty = item.Qty,
                            Total = item.Total,
                            EntryDate = DateTime.Now,
                            SaleDetailCode = SalMasterCode,
                            SaleMasterCode = SalMasterCode,
                            CustomerCode = 0,
                            LastDateModif = DateTime.Now,
                            IsDeleted = 0,
                         
                            UserId = st.GetUser_Code(),
                            Operation_Type_Id = 1,
                        };

                        Item_History _Item_History = new Item_History()
                        {

                            Item_Id = item.ItemCode,
                            Price_Buy = item.PriceBuy,
                            Price_Sale = item.Price,
                            Branch_Id = st.GetBranch_Code(),
                            Qty = item.Qty,
                            Current_Qty_Now = item.Qty,
                            CreatedDate = DateTime.Now,
                         Warhouse_Code = item.Warhouse_Code,
                            IsFinshed = false,
                            Sale_Master_Code = SalMasterCode

                         
                        };


                        ArryOfSaleDetail.Add(_SaleDetail);
                        ArryOfItem_History.Add(_Item_History);
                        Update_Item_Price_Sale(item.ItemCode, item.PriceBuy, item.Price);
                    }
                    Context.SaleDetails.AddRange(ArryOfSaleDetail);
                    Context.Item_History.AddRange(ArryOfItem_History);
                    Context.SaveChanges();

                }
            }
            else
            {
                // If Old Order
                //   1-From Master And Detail Where SaleMasterId  = SaleMasterId  
                // 2 - Delete All 
                //3  - Insert Lins
                // 4- Insert Header


                Int64 branchCode = st.GetBranch_Code();
                Context.Item_History.RemoveRange(Context.Item_History.Where(x => x.Sale_Master_Code == SalMasterCode && x.Is_Used ==false && x.Branch_Id == branchCode));
                Context.SaleDetails.RemoveRange(Context.SaleDetails.Where(x => x.SaleMasterCode == SalMasterCode && x.IsDeleted == 0 && x.Operation_Type_Id == 1 && x.Branch_Id == branchCode));

                List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();
                List<Item_History> ArryOfItem_History = new List<Item_History>();
                var GetDataFromGrid = gcItemCard.DataSource as List<SaleDetailView>;
                if (GetDataFromGrid != null && GetDataFromGrid.Count>0)
                {
                    Int64 UserCode = st.GetUser_Code();
                   
                    var ShiftCode = Context.Shift_View.Where(x => x.User_Id == UserCode  && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
                    foreach (var item in GetDataFromGrid)
                    {
                        SaleDetail _SaleDetail = new SaleDetail()
                        {
                            ItemCode = item.ItemCode,
                            Price = item.PriceBuy,
                            Qty = item.Qty,
                            Total = item.Total,
                            EntryDate = DateTime.Now,
                            shiftCode = ShiftCode,
                            SaleDetailCode = SalMasterCode,
                            SaleMasterCode = SalMasterCode,
                            UserId = st.GetUser_Code(),
                            CustomerCode = 0,
                            IsDeleted = 0,
                            Warhouse_Code = item.Warhouse_Code,
                            Branch_Id = branchCode,
                            Operation_Type_Id = 1,
                            LastDateModif = DateTime.Now
                        };
                        Item_History _Item_History = new Item_History()
                        {

                            Item_Id = item.ItemCode,
                            Price_Buy = item.PriceBuy,
                            Price_Sale = item.Price,
                            Qty = item.Qty,
                            Current_Qty_Now = item.Qty,
                           Warhouse_Code  = item.Warhouse_Code,
                            Branch_Id = branchCode,
                            CreatedDate = DateTime.Now,
                            IsFinshed = false,
                            Sale_Master_Code = SalMasterCode
                           
                        };
                        ArryOfSaleDetail.Add(_SaleDetail);
                        ArryOfItem_History.Add(_Item_History);
                        Update_Item_Price_Sale(item.ItemCode, item.PriceBuy, item.Price);
                    }
                    Context.SaleDetails.AddRange(ArryOfSaleDetail);
                    Context.Item_History.AddRange(ArryOfItem_History);
                    Context.SaveChanges();

                }

             




            }
 
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var x = gvItemCard.GetFocusedRow() as SaleDetailView;
            if (gvItemCard.RowCount > 0)
            {

                frmEditePurchsItems frm = new frmEditePurchsItems();
                //frm.SlkCatgoryName.EditValue = x.CategoryCode;
                frm.txtUnit.Text = x.UnitName;
                frm.txtName.Text = x.Name;
                frm.txtCatgoryName.Text = x.CategoryName;
                frm.slkWarhouse.EditValue = x.Warhouse_Code;
                frm.txtPrice.Text = x.Price.ToString();
                frm.txtParCode.Text = x.ParCode.ToString();
                frm.txtPriceBuy.Text = x.PriceBuy.ToString();
                frm.txtFinalUnitsNumber.Text = x.Qty.ToString();
                frm.ItemCode = Convert.ToInt64(x.ItemCode);
                // frm.chkAddItem.Checked = (bool)x.AddItem;
                frm.txtParCode.ReadOnly = true;
                frm.txtPrice.Enabled = true;
                frm.txtPriceBuy.Enabled = true;
               

                frm.ShowDialog();

            }

        }

        private void اضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAddPurchsItems frm = new frmAddPurchsItems();
            //frm.ShowDialog();

            gcItemCard.DataSource = null;
            gcItemCard.RefreshDataSource();

            Int64? MaxCode = Context.SaleMasters.Where(x => x.Operation_Type_Id == 1 || x.Operation_Type_Id == 6).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }
            this.Status = "New";
            groupControl1.CustomHeaderButtons[1].Properties.Caption = MaxCode.ToString();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
             
            if (gvItemCard.RowCount > 0)
            {
                var FocusRow = gvItemCard.GetFocusedRow() as SaleDetailView;
                List<SaleDetailView> gcData = gvItemCard.DataSource as List<SaleDetailView>;
                gcData.Remove(FocusRow);
                gcItemCard.DataSource = gcData;
                gcItemCard.RefreshDataSource();
            }
            else
            {

            }
        }

        void Update_Item_Price_Sale(Int64 ItemCode, double PriceBuy, double PriceSale)
        {
            Int64 branchCode = st.GetBranch_Code();
            ItemCard _ItemCard;
            _ItemCard = Context.ItemCards.SingleOrDefault(Item => Item.ItemCode==ItemCode&&Item.IsDeleted==0 && Item.Branch_Code == branchCode );

            if (_ItemCard != null) {
                _ItemCard.Price = PriceSale;

                _ItemCard.PriceBuy = PriceBuy;


                Context.SaveChanges();
                
            }
          


        }

        private void windowsUIButtonPanel_ButtonClick_1(object sender, ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد" || btn.Caption == "New")
            {
                frmAddPurchsItems frm = new frmAddPurchsItems();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل" || btn.Caption == "Edite")
            {

                var x = gvItemCard.GetFocusedRow() as SaleDetailView;
                if (gvItemCard.RowCount > 0)
                {

                    frmEditePurchsItems frm = new frmEditePurchsItems();
                    
                    frm.txtUnit.Text = x.UnitName;
                    frm.txtName.Text = x.Name;
                    frm.txtCatgoryName.Text = x.CategoryName;
                    frm.slkWarhouse.EditValue = x.Warhouse_Code;
                    frm.txtPrice.Text = x.Price.ToString();
                    frm.txtParCode.Text = x.ParCode.ToString();
                    frm.txtPriceBuy.Text = x.PriceBuy.ToString();
                    frm.txtFinalUnitsNumber.Text = x.Qty.ToString();
                    frm.ItemCode = Convert.ToInt64(x.ItemCode);
                    // frm.chkAddItem.Checked = (bool)x.AddItem;
                    frm.txtParCode.ReadOnly = true;
                    //  frm.SlkCatgoryName.Focus();


                    frm.ShowDialog();

                }

            }
            else if (btn.Caption == "حفظ" || btn.Caption == "Save")
            {




                var DataFromGrid = gvItemCard.DataSource as List<SaleDetailView>;


                if (DataFromGrid != null && DataFromGrid.Count > 0)
                {
                    Int64 branchCode = st.GetBranch_Code();
                    Int64 SalMasterCode = Convert.ToInt64(groupControl1.CustomHeaderButtons[1].Properties.Caption);
                    var Item_History_List = Context.Item_History.Where(x => x.Sale_Master_Code == SalMasterCode && x.Branch_Id == branchCode).ToList();
                    //foreach (var item in Item_History_List)
                    //{
                    //    if (item.Is_Used == true || item.IsFinshed == true)
                    //    {
                    //        MaterialMessageBox.Show(st.isEnglish()?"It is not possible to delete or modify the invoice due to sales on it":"لايمكن حذف او التعديل علي الفاتوره بسبب حدوث عمليات بيع عليها", MessageBoxButtons.OK);
                    //    return;
                    //    }
                        
                    //}



                    SaveSaleMaster();
                    SaveSaleDetail();
                    gcItemCard.DataSource = null;
                    gcItemCard.RefreshDataSource();

                    Int64? MaxCode = Context.SaleMasters.Where(x => (x.Operation_Type_Id == 1 || x.Operation_Type_Id == 6) & x.IsDeleted == 0).Max(u => (Int64?)u.SaleMasterCode + 1);
                    if (MaxCode == null || MaxCode == 0)
                    {
                        MaxCode = 1;
                    }
                    groupControl1.CustomHeaderButtons[1].Properties.Caption = MaxCode.ToString();
                    if (this.Status == "New")
                    {

                        MaterialMessageBox.Show(st.isEnglish()? "Invoice No  " +(MaxCode - 1).ToString()+ " has been saved" : "  تم حفظ الفاتوره رقم :" + (MaxCode - 1).ToString() , MessageBoxButtons.OK);
                        this.Status = "New";
                        return;
                    }
                    else
                    {

                        MaterialMessageBox.Show(st.isEnglish()? "Invoice has been modified" : "تم تعديل الفاتوره ", MessageBoxButtons.OK);
                        this.Status = "New";
                        return;
                    }


                }
                else
                {


                    MaterialMessageBox.Show(st.isEnglish()? "There is no data to save" : "لايوجد بيانات لتتم عملية الحفظ", MessageBoxButtons.OK);
                    this.Status = "New";
                    return;


                }

            }
            else if (btn.Caption == "بحث" || btn.Caption == "Search")
            {

                frmInvoicePurchsSearch frm = new frmInvoicePurchsSearch();
                frm.ShowDialog();


            }
            //else if (btn.Caption == "جديد" || btn.Caption == "New")
            //{
            //    Int64? MaxCode = Context.SaleMasters.Where(x => x.Operation_Type_Id == 1 || x.Operation_Type_Id == 6).Max(u => (Int64?)u.SaleMasterCode + 1);
            //    gcItemCard.DataSource = null;
            //    gcItemCard.RefreshDataSource();
            //    if (MaxCode == null || MaxCode == 0)
            //    {
            //        MaxCode = 1;
            //    }
            //    this.Status = "New";
            //    groupControl1.CustomHeaderButtons[1].Properties.Caption = MaxCode.ToString();

            //}
            else if (btn.Caption == "حذف" || btn.Caption == "Delete")
            {

                var x = gvItemCard.GetFocusedRow() as SaleDetailView;
                if (gvItemCard.RowCount > 0)
                {
                    var FocusRow = gvItemCard.GetFocusedRow() as SaleDetailView;
                    List<SaleDetailView> gcData = gvItemCard.DataSource as List<SaleDetailView>;
                    gcData.Remove(FocusRow);
                    gcItemCard.DataSource = gcData;
                    gcItemCard.RefreshDataSource();
                }
                else
                {

                }

            }
            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {


                if (MaterialMessageBox.Show(st.isEnglish()? "Confirm exit" : "تاكيد الاغلاق", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    this.Close();

                }


            }
        }

        private void اضافةجديدةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddPurchsItems frm = new frmAddPurchsItems();
            frm.ShowDialog();
        }

        private void frmPurchasescs_Load(object sender, EventArgs e)
        {
             
            //var UserId = Convert.ToInt64(st.GetUser_Code());
            //var Check = Context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
            //if (!Check)
            //{
            //    if (MaterialMessageBox.Show(st.isEnglish()? "Please Stert Shift for this user":"برجاء اضافة وردية لهذا المستخدم", MessageBoxButtons.OK) == DialogResult.OK)
            //    {
            //        this.Close();
            //    }

            //}
        }
    }
}