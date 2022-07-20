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
 
using PointOfSaleSedek.HelperClass;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmSuperMarketInvoiceSearch : DevExpress.XtraEditors.XtraForm
    {
        readonly SaleEntities context = new SaleEntities();
        readonly Static st = new Static();
        public frmSuperMarketInvoiceSearch()
        {
            InitializeComponent();
            FillGrid();
        }
       void FillGrid()
        {
             
            gcSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id==2).ToList();
 
        }

        

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "معاينة")
            {
                if (Application.OpenForms.OfType<frmSuperMarketSales>().Any())
                {
                    if (gvSaleMaster.RowCount <= 0)
                    {

                        MaterialMessageBox.Show("!لا يوجد اوردرات للمعاينة", MessageBoxButtons.OK);
                        return;


                    }

                    frmSuperMarketSales frm = (frmSuperMarketSales)Application.OpenForms["frmSuperMarketSales"];

                    //var RowCount = gvSaleDetail.RowCount;
                    var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
                    Int64 SaleMasterCode = FocusRow.SaleMasterCode;

                 
                   
                   
                    frm.lblDiscount.Text = Convert.ToString(FocusRow.Discount);
                    frm.lblFinalBeforDesCound.Text = Convert.ToString(FocusRow.TotalBeforDiscount);
                    frm.lblFinalTotal.Text = Convert.ToString(FocusRow.FinalTotal);
                    frm.lblItemQty.Text = Convert.ToString(FocusRow.QtyTotal);
                    frm.lblSaleMasterId.Text = Convert.ToString(FocusRow.SaleMasterCode);
                  
                   
                    frm.btnNew.Enabled = true;
                    frm.btnSave.Enabled = false;
                    frm.btnEdite.Enabled = true;
                    frm.txtParCode.Enabled = false;
                    //frm.BtnExit.Enabled = true;
                    frm.btnPrint.Enabled = true;
                    if (FocusRow.Customer_Code > 0) {

                        frm.btnCustomerHistory.Enabled = true;
                    }
                    else{

                        frm.btnCustomerHistory.Enabled = false;
                    }
                  
                    frm.gcSaleDetail.DataSource = null;
                    frm.gcSaleDetail.RefreshDataSource();
                    frm.gcSaleDetail.DataSource  = context.SaleDetailViews.Where(x=>x.SaleMasterCode == SaleMasterCode && x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year &&x.Operation_Type_Id==2).ToList();
                    frm.gcSaleDetail.Enabled = false;
                    frm.slkCustomers.EditValue = FocusRow.Customer_Code;
                    frm.slkCustomers.Enabled = false;
                    frm.btnDiscount.Enabled = false;
                    frm.btnAddCustomer.Enabled = false;
                  
                    Int64 User_Code = st.User_Code();

                    var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



                    if (result.Any(xd => xd.Tab_Name == "btnser"))
                    {
                       frm.btnser.Enabled = true;

                    }
                    else
                    {
                        frm.btnser.Enabled = false;
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
            if (gvSaleMaster.RowCount <= 0)
            {

                groupControl1.Enabled = false;


            }
        }

        private void الغاءالفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
            SaleMaster _SaleMaster;
            _SaleMaster = context.SaleMasters.SingleOrDefault(shft => shft.Operation_Type_Id == 2 && shft.ShiftCode == FocusRow.Shift_Code && shft.IsDeleted == 0 && shft.UserCode==FocusRow.UserCode&& shft.EntryDate == FocusRow.EntryDate&&shft.SaleMasterCode == FocusRow.SaleMasterCode);
            Int64 saleMasterCode = _SaleMaster.SaleMasterCode;
            _SaleMaster.Operation_Type_Id = 3;
            _SaleMaster.LastDateModif = DateTime.Now;
            context.SaveChanges();
            var DayOfYear = DateTime.Today.Day;
            var Year = DateTime.Today.Year;
            var Month = DateTime.Today.Month;
          //  var item_History =  (from a in Context.Item_History where a.CreatedDate.Day == DayOfYear && a.CreatedDate.Month == Month && a.CreatedDate.Month == Month && a.Sale_Master_Code == _SaleMaster.SaleMasterCode select a).ToList();
            var _item_History_Transactions =  (from a in context.Item_History_transaction where a.CreatedDate.Day == DayOfYear && a.CreatedDate.Month == Month &&a.CreatedDate.Month == Month && a.SaleMasterCode == saleMasterCode select a).ToList();

          

                _item_History_Transactions.ForEach(x => {

                   
                    using (SaleEntities cont = new SaleEntities())
                    {
                        Item_History _item_History;

                        _item_History = cont.Item_History.SingleOrDefault(History => History.Id == x.Item_History_Id);
                        _item_History.Current_Qty_Now += x.Trans_Out;
                        _item_History.IsFinshed = (bool)false;
                        cont.SaveChanges();




                        List<Item_History_transaction> _Item_History_transaction = new List<Item_History_transaction>();
                        _Item_History_transaction = cont.Item_History_transaction.Where(History_tr => History_tr.Item_History_Id == x.Item_History_Id&&History_tr.SaleMasterCode == saleMasterCode).ToList();
                        _Item_History_transaction.ForEach(History_transaction => {
                            Item_History_transaction  _Item_Historytransaction;
                            _Item_Historytransaction = cont.Item_History_transaction.SingleOrDefault(History_tr => History_tr.Id == History_transaction.Id&& History_tr.SaleMasterCode== saleMasterCode);
                            _Item_Historytransaction.IsDeleted = 1;
                            cont.SaveChanges();
                        });



                        
                    }
                    
                    

                });



           



       



            using (SaleEntities context2 = new SaleEntities())
            {
                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == FocusRow.SaleMasterCode && w.EntryDate.Day == FocusRow.EntryDate.Day && w.EntryDate.Month == FocusRow.EntryDate.Month && w.EntryDate.Year == FocusRow.EntryDate.Year && w.Operation_Type_Id==2 && w.IsDeleted == 0).ToList();
                if (Details.Count > 0)
                {
                foreach (var item in Details)
                {

                        context2.SaleDetails.Where(shft => shft.Operation_Type_Id == 2 &&  shft.IsDeleted == 0  && shft.EntryDate.Day == FocusRow.EntryDate.Day && shft.EntryDate.Month == FocusRow.EntryDate.Month && shft.EntryDate.Year == FocusRow.EntryDate.Year   && shft.SaleMasterCode == FocusRow.SaleMasterCode).ToList().ForEach(x => {
                      x.Operation_Type_Id = 3;
                      x.LastDateModif = DateTime.Now;
                      
                      });

                      
                        context2.SaveChanges();
                }

                }
             
            }

            


            using (SaleEntities cont  = new SaleEntities())
            {
                gcSaleMaster.DataSource = null;
               
                gcSaleMaster.DataSource = context.SaleMasterViews.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && x.Operation_Type_Id == 2 && x.IsDeleted == 0).ToList();
                gcSaleMaster.RefreshDataSource();
            }

            if (gvSaleMaster.RowCount == 0)
            {

                groupControl1.Enabled = false;
            
            }
        }
    }
}