using System;
using System.Collections.Generic;

using System.Data;

using System.Linq;

using System.Windows.Forms;

using PointOfSaleSedek._102_MaterialSkin;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeInvoiceSearch : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmCafeInvoiceSearch()
        {
            InitializeComponent();
            FillGrid();
            FillSlkUser();
            langu();
        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.gridColumn1.Caption = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
            this.gridColumn2.Caption = st.isEnglish() ? "Date" : "التاريخ";

            this.gridColumn3.Caption = st.isEnglish() ? "Total" : "الاجمالي";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "Cancel This Invoice" : "اللغاء الفاتورة";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
           new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinalTotal",  st.isEnglish()?"Total = {0:N}": "الاجمالي = {0:N}")});
            this.gridColumn4.Caption = st.isEnglish() ? "Created by" : "البائع";

            this.groupControl1.CustomHeaderButtons[0].Properties.Caption = st.isEnglish() ? "Preview" : "معاينة";
            this.groupControl1.Text = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
            this.groupControl1.Text = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
             


          
        }
        void FillGrid()
        {
            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();
            Int64 UserCode = st.GetUser_Code();

            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();

            using (POSEntity cont = new POSEntity())
            {
                gcCafeSaleMaster.DataSource = null;

                gcCafeSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.Shift_Code == ShiftCode && x.Branches_Code == Branch_Code  &&x.IsDeleted == 0 &&x.UserCode == UserCode && x.Operation_Type_Id == 2).ToList();
                gcCafeSaleMaster.RefreshDataSource();
            }

            if (gvCafeSaleMaster.RowCount == 0)
            {

                groupControl1.Enabled = false;

            }


            // gcSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id==2).ToList();
            
          //  gcSaleMaster.Refresh();
        }

        

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();
            if (e.Button.Properties.Caption == "معاينة" || e.Button.Properties.Caption == "Preview")
            {
                if (Application.OpenForms.OfType<frmCafeSales>().Any())
                {
                    if (gvCafeSaleMaster.RowCount <= 0)
                    {

                        MaterialMessageBox.Show(st.isEnglish() ? "There are no orders for viewing" : "!لا يوجد اوردرات للمعاينة", MessageBoxButtons.OK);
                        return;


                    }

                    frmCafeSales frm = (frmCafeSales)Application.OpenForms["frmCafeSales"];

                    //var RowCount = gvSaleDetail.RowCount;
                    var FocusRow = gvCafeSaleMaster.GetFocusedRow() as SaleMasterView;
                    Int64 SaleMasterCode = FocusRow.SaleMasterCode;


                 


                    frm.lblDiscount.Text = Convert.ToString(FocusRow.Discount);
                    frm.lblFinalBeforDesCound.Text = Convert.ToString(FocusRow.TotalBeforDiscount);
                    frm.lblFinalTotal.Text = Convert.ToString(FocusRow.FinalTotal);
                    frm.lblItemQty.Text = Convert.ToString(FocusRow.QtyTotal);
                    frm.lblSaleMasterId.Text = Convert.ToString(FocusRow.SaleMasterCode);

                    frm.tabItems.Enabled = false;
                    frm.btnNew.Enabled = true;
                  //  frm.btnSave.Enabled = false;
                    frm.btnEdite.Enabled = true;
                    frm.txtParCode.Enabled = false;
                    frm.btnDiscount.Enabled = false;
                    //frm.BtnExit.Enabled = true;
                    frm.btnPrint.Enabled = true;
                    if (FocusRow.Customer_Code > 0) {

                         frm.btnCustomerHistory.Enabled = true;
                    }
                    else{

                         frm.btnCustomerHistory.Enabled = false;
                    }
                  
                    frm.gcCafeSaleDetail.DataSource = null;
                    frm.gcCafeSaleDetail.RefreshDataSource();
                   
                    var ccc = context.SaleDetailViews.Where(x => x.SaleMasterCode == SaleMasterCode && x.Warhouse_Code == Warehouse_Code  &&x.shiftCode == FocusRow.Shift_Code && x.Branches_Code == Branch_Code && x.Operation_Type_Id == 2).ToList();
                    frm.gcCafeSaleDetail.DataSource = ccc;
                   frm.gcCafeSaleDetail.Enabled = false;
                    frm.slkCustomers.EditValue = FocusRow.Customer_Code;
                    frm.slkCustomers.Enabled = false;
                    frm.btnDiscount.Enabled = false;
                    frm.btnser.Enabled = false;

                    Int64 User_Code = st.GetUser_Code();

                    var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



                    if (result.Any(xd => xd.Tab_Name == "btnser"))
                    {
                        frm.btnser.Enabled = true;

                    }
                    else
                    {
                     //   frm.btnser.Enabled = false;
                    }


                    if (result.Any(xd => xd.Tab_Name == "btnDiscount"))
                    {
                       frm.btnDiscount.Enabled = true;

                    }
                    else
                    {
                        frm.btnDiscount.Enabled = false;
                    }



                    frm.Status = "Old";
                    this.Close();
                }
            
            }
          

            
        }

        private void frmInvoiceSearch_Load(object sender, EventArgs e)
        {
            if (gvCafeSaleMaster.RowCount <= 0)
            {

                groupControl1.Enabled = false;


            }
        }

        private void الغاءالفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var FocusRow = gvCafeSaleMaster.GetFocusedRow() as SaleMasterView;
            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();

            SaleMaster _SaleMaster = context.SaleMasters.SingleOrDefault(shft =>
            shft.Operation_Type_Id == 2 && shft.ShiftCode == FocusRow.Shift_Code && shft.IsDeleted == 0 &&
            shft.UserCode==FocusRow.UserCode &&shft.SaleMasterCode == FocusRow.SaleMasterCode && shft.Branch_Id == Branch_Code);


            Int64 saleMasterCode = _SaleMaster.SaleMasterCode;
            _SaleMaster.Operation_Type_Id = 3;
            _SaleMaster.LastDateModif = DateTime.Now;
            context.SaveChanges();
           
        
            var _item_History_Transactions =  (from a in context.Item_History_transaction where
                                               a.shiftCode == FocusRow.Shift_Code &&a.Branch_Id == FocusRow.Branches_Code &&
                                               a.from_Warhouse_Code == Warehouse_Code &&
                                               a.SaleMasterCode == saleMasterCode && a.IsDeleted == 0 select a).ToList();

          

                _item_History_Transactions.ForEach(x => {

                   
                    using (POSEntity cont = new POSEntity())
                    {
                        Item_History _item_History; 

                        _item_History = cont.Item_History.SingleOrDefault(History => History.Id == x.Item_History_Id && History.Warhouse_Code == Warehouse_Code);
                        _item_History.Current_Qty_Now += x.Trans_Out;
                        _item_History.IsFinshed = (bool)false;
                     
                        cont.SaveChanges();




                        List<Item_History_transaction> _Item_History_transaction = new List<Item_History_transaction>();
                        _Item_History_transaction = cont.Item_History_transaction.Where(History_tr => History_tr.shiftCode == FocusRow.Shift_Code &&
                        History_tr.Item_History_Id == x.Item_History_Id && History_tr.SaleMasterCode == saleMasterCode
                        && History_tr.IsDeleted == 0 && History_tr.Branch_Id == Branch_Code).ToList();

                        _Item_History_transaction.ForEach(History_transaction => {
                            Item_History_transaction  _Item_Historytransaction;
                            _Item_Historytransaction = cont.Item_History_transaction.SingleOrDefault(History_tr => History_tr.Id == History_transaction.Id&&
                            History_tr.IsDeleted == 0 &&History_tr.Branch_Id == Branch_Code  && History_tr.SaleMasterCode== saleMasterCode && History_tr.shiftCode == FocusRow.Shift_Code);
                            _Item_Historytransaction.IsDeleted = 1;
                            cont.SaveChanges();
                        });



                        
                    }
                    
                    

                });



           



       



            using (POSEntity context2 = new POSEntity())
            {
                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == FocusRow.SaleMasterCode && w.Warhouse_Code == Warehouse_Code && w.Branch_Id == FocusRow.Branches_Code
                &&w.shiftCode == FocusRow.Shift_Code && w.Operation_Type_Id==2 && w.IsDeleted == 0).ToList();
                if (Details.Count > 0)
                {
                foreach (var item in Details)
                {

                        context2.SaleDetails.Where(shft => shft.Operation_Type_Id == 2 &&  shft.Branch_Id == Branch_Code && shft.Warhouse_Code == Warehouse_Code &&shft.IsDeleted == 0  &&shft.shiftCode == FocusRow.Shift_Code&& shft.SaleMasterCode == FocusRow.SaleMasterCode).ToList().ForEach(x => {
                      x.Operation_Type_Id = 3;
                      x.LastDateModif = DateTime.Now;
                      
                      });

                      
                        context2.SaveChanges();
                }

                }
             
            }

            


            using (POSEntity cont  = new POSEntity())
            {
                gcCafeSaleMaster.DataSource = null;
               
                gcCafeSaleMaster.DataSource = context.SaleMasterViews.Where(x =>x.Shift_Code == FocusRow.Shift_Code && x.Branches_Code ==Branch_Code && x.Operation_Type_Id == 2 && x.IsDeleted == 0).ToList();
                gcCafeSaleMaster.RefreshDataSource();
            }

            if (gvCafeSaleMaster.RowCount == 0)
            {

                groupControl1.Enabled = false;
            
            }
        }

        public void FillSlkUser()
        {

            //List<User_View> listUserView = new List<User_View>();


            //var resultList = context.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();

            //foreach (var item in resultList)
            //{
            //    bool ressult = context.Shift_View.Any(Shift => Shift.IsDeleted == 0 && Shift.Emp_Code == item.Employee_Code && Shift.Shift_Flag == true);
            //    if (!ressult)
            //    {

            //        listUserView.Add(item);
            //    }

            //}


            //   var result = Context.Shift_View.Where(Shift => Shift.IsDeleted == 0  && Shift.Shift_Flag == false).ToList();
            var resultList = context.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();
            slkUsers.Properties.DataSource = resultList;
            slkUsers.Properties.ValueMember = "Employee_Code";
            slkUsers.Properties.DisplayMember = "UserName";

        }

        private void slkShiftsOpen_Properties_EditValueChanged(object sender, EventArgs e)
        {
            Int64 UserCode = st.GetUser_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            try
            {
                if (slkUsers.EditValue != null &&   !String.IsNullOrWhiteSpace(slkUsers.EditValue.ToString()))
                {
                    string userCode = slkUsers.EditValue.ToString();

                    gcCafeSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.UserCode.ToString() == userCode && x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2).ToList();
                }
                else
                {
                   
                    gcCafeSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2 && x.Shift_Code == ShiftCode).ToList();
                }

                gcCafeSaleMaster.Refresh();
            }
            catch (Exception)
            {

                gcCafeSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2 && x.Shift_Code == ShiftCode).ToList();
                gcCafeSaleMaster.Refresh();
            }
          

        }
    }
}