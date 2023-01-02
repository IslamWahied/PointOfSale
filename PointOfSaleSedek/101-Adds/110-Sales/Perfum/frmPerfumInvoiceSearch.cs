using System;
using System.Collections.Generic;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
using DataRep;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using PointOfSaleSedek.Model;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmPerfumInvoiceSearch : DevExpress.XtraEditors.XtraForm
    {
        POSEntity Context = new POSEntity();
        readonly Static st = new Static();
        public frmPerfumInvoiceSearch()
        {
            InitializeComponent();
            FillGrid();
            langu();
        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.gridColumn1.Caption = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
            this.gridColumn2.Caption = st.isEnglish() ? "Date" : "التاريخ";

            this.gridColumn3.Caption = st.isEnglish() ? "Total" : "الاجمالي";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
           new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinalTotal",  st.isEnglish()?"Total = {0:N}": "الاجمالي = {0:N}")});
            this.gridColumn4.Caption = st.isEnglish() ? "Created by" : "البائع";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "Cancel This Invoice" : "اللغاء الفاتورة";
            this.groupControl1.CustomHeaderButtons[0].Properties.Caption = st.isEnglish() ? "Preview" : "معاينة";
            this.groupControl1.Text = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
            this.groupControl1.Text = st.isEnglish() ? "invoice number" : "رقم الفاتورة";




        }
        void FillGrid()
        {
            Int64 UserCode = st.GetUser_Code();
            var ShiftCode = Context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();

            using (POSEntity cont = new POSEntity())
             
                gcSaleMaster.DataSource = null;

                gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.Shift_Code == ShiftCode && x.UserCode == UserCode && x.Operation_Type_Id == 2).ToList();
                gcSaleMaster.RefreshDataSource();
            }

           


        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (Application.OpenForms.OfType<frmPerfumSales>().Any())
            {
                if (gvSaleMaster.RowCount <= 0)
                {

                    MaterialMessageBox.Show(st.isEnglish() ? "There are no orders for viewing" : "!لا يوجد اوردرات للمعاينة", MessageBoxButtons.OK);
                    return;


                }
                List<SaleDetailPrfumViewVm> listSaleDetailPrfumViewVm = new List<SaleDetailPrfumViewVm>();

                frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];

                //var RowCount = gvSaleDetail.RowCount;
                var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
                Int64 SaleMasterCode = FocusRow.SaleMasterCode;


                frm.SlkPaymentsType.EditValue = FocusRow.Payment_Type;

                frm.lblDiscount.Text = Convert.ToString(FocusRow.Discount);
                frm.lblFinalBeforDesCound.Text = Convert.ToString(FocusRow.TotalBeforDiscount);
                frm.lblFinalTotal.Text = Convert.ToString(FocusRow.FinalTotal);
                frm.lblItemQty.Text = Convert.ToString(FocusRow.QtyTotal);
                frm.lblSaleMasterId.Text = Convert.ToString(FocusRow.SaleMasterCode);

                
                frm.btnNew.Enabled = true;
                   frm.btnSave.Enabled = false;
                frm.btnAddItems.Enabled = false;
                frm.slkCustomers.Enabled = false;
                frm.SlkPaymentsType.Enabled = false;
                frm.btnEdite.Enabled = true;
                
               
                if (FocusRow.Customer_Code > 0)
                {

                       frm.btnCustomerHistory.Enabled = true;
                    frm.FillSlkCustomers();
                    frm.slkCustomers.EditValue = FocusRow.Customer_Code;
                }
                else
                {

                     frm.btnCustomerHistory.Enabled = false;
                }

                frm.gcPrfumSaleDetail.DataSource = null;
                frm.gcPrfumSaleDetail.RefreshDataSource();
               var  detail = Context.SaleDetailViews.Where(x => x.SaleMasterCode == SaleMasterCode && x.shiftCode == FocusRow.Shift_Code && x.Operation_Type_Id == 2).ToList();

                if (detail.Count > 0)
                {
                   


                    List<Int64> seq = new List<Int64>();

                    detail.ForEach((xx) => {

                        if (!seq.Any( u => u == xx.LineSequence)) {
                            seq.Add(xx.LineSequence);
                        }
                    });



                    seq.ForEach((x) => {
                        SaleDetailPrfumViewVm SaleDetailPrfumViewVm = new SaleDetailPrfumViewVm();

                        SaleDetailPrfumViewVm.Total = 0;

                        detail.Where(vv => vv.LineSequence == x).ToList().ForEach(dd => {


                            if (dd.isOile)
                            {
                                SaleDetailPrfumViewVm.OilIName = dd.Name;
                                SaleDetailPrfumViewVm.OilItemCode = dd.ItemCode;
                                SaleDetailPrfumViewVm.OilQty = dd.Qty;
                                SaleDetailPrfumViewVm.OilPrice = dd.Price;
                                SaleDetailPrfumViewVm.LineSequence = x;
                                SaleDetailPrfumViewVm.Total += dd.Total;
                               

                            }
                            else {

                                SaleDetailPrfumViewVm.GlassIName = dd.Name;
                                SaleDetailPrfumViewVm.GlassItemCode = dd.ItemCode;
                                SaleDetailPrfumViewVm.GlassQty = dd.Qty;
                                SaleDetailPrfumViewVm.GlassPrice = dd.Price;
                                SaleDetailPrfumViewVm.LineSequence = x;
                                SaleDetailPrfumViewVm.Total += dd.Total;

                            }









                        });


                        listSaleDetailPrfumViewVm.Add(SaleDetailPrfumViewVm);


                    });


                    


                }


 
                frm.gcPrfumSaleDetail.DataSource = listSaleDetailPrfumViewVm;
                frm.gcPrfumSaleDetail.RefreshDataSource();
                frm.slkEmployees.EditValue = detail.FirstOrDefault().Emp_Code;

                frm.gcPrfumSaleDetail.Enabled = false;
                frm.slkCustomers.EditValue = FocusRow.Customer_Code;
                frm.slkCustomers.Enabled = false;
                frm.btnDiscount.Enabled = false;
                frm.btnser.Enabled = false;
                frm.btnAddItems.Enabled = false;
                frm.btnser.Enabled = false;


                Int64 User_Code = st.GetUser_Code();

                var result = Context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



                //if (result.Any(xd => xd.Tab_Name == "btnser"))
                //{
                //       frm.btnAddCustomer.Enabled = true;

                //}
                //else
                //{
                //        frm.btnAddCustomer.Enabled = false;
                //}


                if (result.Any(xd => xd.Tab_Name == "btnEdite"))
                {
                      frm.btnEdite.Enabled = true;
                }
                else
                {
                     frm.btnEdite.Enabled = false;
                }



                frm.Status = "Old";
                this.Close();
            }



        }

        private void frmInvoiceSearch_Load(object sender, EventArgs e)
        {
            if (gvSaleMaster.RowCount <= 0)
            {

                groupControl1.Enabled = false;


            }
        }

        private void الغاءالفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {







            var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
            SaleMaster _SaleMaster;
            _SaleMaster = Context.SaleMasters.SingleOrDefault(shft => shft.Operation_Type_Id == 2 && shft.ShiftCode == FocusRow.Shift_Code && shft.IsDeleted == 0 && shft.UserCode == FocusRow.UserCode && shft.SaleMasterCode == FocusRow.SaleMasterCode);
            Int64 saleMasterCode = _SaleMaster.SaleMasterCode;
            _SaleMaster.Operation_Type_Id = 3;
            _SaleMaster.LastDateModif = DateTime.Now;
            Context.SaveChanges();
            var DayOfYear = DateTime.Today.Day;
            var Year = DateTime.Today.Year;
            var Month = DateTime.Today.Month;
            //  var item_History =  (from a in Context.Item_History where a.CreatedDate.Day == DayOfYear && a.CreatedDate.Month == Month && a.CreatedDate.Month == Month && a.Sale_Master_Code == _SaleMaster.SaleMasterCode select a).ToList();
            var _item_History_Transactions = (from a in Context.Item_History_transaction where a.shiftCode == FocusRow.Shift_Code && a.SaleMasterCode == saleMasterCode select a).ToList();



            _item_History_Transactions.ForEach(x => {


                using (POSEntity cont = new POSEntity())
                {
                    Item_History _item_History;

                    _item_History = cont.Item_History.SingleOrDefault(History => History.Id == x.Item_History_Id);
                    _item_History.Current_Qty_Now += x.Trans_Out;
                    _item_History.IsFinshed = (bool)false;
                    _item_History.IsFinshed = (bool)false;
                    cont.SaveChanges();




                    List<Item_History_transaction> _Item_History_transaction = new List<Item_History_transaction>();
                    _Item_History_transaction = cont.Item_History_transaction.Where(History_tr => History_tr.shiftCode == FocusRow.Shift_Code && History_tr.Item_History_Id == x.Item_History_Id && History_tr.SaleMasterCode == saleMasterCode).ToList();
                    _Item_History_transaction.ForEach(History_transaction => {
                        Item_History_transaction _Item_Historytransaction;
                        _Item_Historytransaction = cont.Item_History_transaction.SingleOrDefault(History_tr => History_tr.Id == History_transaction.Id && History_tr.SaleMasterCode == saleMasterCode && History_tr.shiftCode == FocusRow.Shift_Code);
                        _Item_Historytransaction.IsDeleted = 1;
                        cont.SaveChanges();
                    });




                }



            });











            using (POSEntity context2 = new POSEntity())
            {
                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == FocusRow.SaleMasterCode && w.shiftCode == FocusRow.Shift_Code && w.Operation_Type_Id == 2 && w.IsDeleted == 0).ToList();
                if (Details.Count > 0)
                {
                    foreach (var item in Details)
                    {

                        context2.SaleDetails.Where(shft => shft.Operation_Type_Id == 2 && shft.IsDeleted == 0 && shft.shiftCode == FocusRow.Shift_Code && shft.SaleMasterCode == FocusRow.SaleMasterCode).ToList().ForEach(x => {
                            x.Operation_Type_Id = 3;
                            x.LastDateModif = DateTime.Now;

                        });


                        context2.SaveChanges();
                    }

                }

            }




            using (POSEntity cont = new POSEntity())
            {
                gcSaleMaster.DataSource = null;

                gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.Shift_Code == FocusRow.Shift_Code && x.Operation_Type_Id == 2 && x.IsDeleted == 0).ToList();
                gcSaleMaster.RefreshDataSource();
            }

            if (gvSaleMaster.RowCount == 0)
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
            //var resultList = context.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();
            //slkUsers.Properties.DataSource = resultList;
            //slkUsers.Properties.ValueMember = "Employee_Code";
            //slkUsers.Properties.DisplayMember = "UserName";

        }

        private void slkShiftsOpen_Properties_EditValueChanged(object sender, EventArgs e)
        {
            Int64 UserCode = st.GetUser_Code();
            var ShiftCode = Context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            try
            {
                //if (slkUsers.EditValue != null && !String.IsNullOrWhiteSpace(slkUsers.EditValue.ToString()))
                //{
                //    string userCode = slkUsers.EditValue.ToString();

                //    gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.UserCode.ToString() == userCode && x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2).ToList();
                //}
                //else
                //{

                //    gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2 && x.Shift_Code == ShiftCode).ToList();
                //}

                gcSaleMaster.Refresh();
            }
            catch (Exception)
            {

                gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2 && x.Shift_Code == ShiftCode).ToList();
                gcSaleMaster.Refresh();
            }


        
    }

        private void gcSaleMaster_Click(object sender, EventArgs e)
        {

        }
    }
}