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
using EntityData;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmInvoiceSearch : DevExpress.XtraEditors.XtraForm
    {
        PointOfSaleEntities Context = new PointOfSaleEntities();
        public frmInvoiceSearch()
        {
            InitializeComponent();
            FillGrid();
        }
       void FillGrid()
        {
            gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.EntryDate == DateTime.Today&&x.Operation_Type_Id==2).ToList();
 
        }

        

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "معاينة")
            {
                if (Application.OpenForms.OfType<frmSales>().Any())
                {
                    if (gvSaleMaster.RowCount <= 0)
                    {

                        MaterialMessageBox.Show("!لا يوجد اوردرات للمعاينة", MessageBoxButtons.OK);
                        return;


                    }

                    frmSales frm = (frmSales)Application.OpenForms["frmSales"];

                    //var RowCount = gvSaleDetail.RowCount;
                    var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
                    Int64 SaleMasterCode = FocusRow.SaleMasterCode;

                 
                   
                   
                    frm.lblDiscount.Text = Convert.ToString(FocusRow.Discount);
                    frm.lblFinalBeforDesCound.Text = Convert.ToString(FocusRow.TotalBeforDiscount);
                    frm.lblFinalTotal.Text = Convert.ToString(FocusRow.FinalTotal);
                    frm.lblItemQty.Text = Convert.ToString(FocusRow.QtyTotal);
                    frm.lblSaleMasterId.Text = Convert.ToString(FocusRow.SaleMasterCode);
                  
                   
                    frm.btnNew.Enabled = true;
                    frm.btnEdite.Enabled = true;
                    //frm.BtnExit.Enabled = true;
                   frm.btnPrint.Enabled = true;
                    frm.gcSaleDetail.DataSource = null;
                    frm.gcSaleDetail.RefreshDataSource();
                    frm.gcSaleDetail.DataSource  =  Context.SaleDetailViews.Where(x=>x.SaleMasterCode == SaleMasterCode && x.EntryDate==DateTime.Today&&x.Operation_Type_Id==2).ToList();
                    frm.gcSaleDetail.Enabled = false;
                
                   
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
            _SaleMaster = Context.SaleMasters.SingleOrDefault(shft => shft.Operation_Type_Id == 2 && shft.ShiftCode == FocusRow.Shift_Code && shft.IsDeleted == 0 && shft.UserCode==FocusRow.UserCode&& shft.EntryDate == FocusRow.EntryDate&&shft.SaleMasterCode == FocusRow.SaleMasterCode);
            Int64 saleMasterCode = _SaleMaster.SaleMasterCode;
            _SaleMaster.Operation_Type_Id = 3;
            _SaleMaster.LastDateModif = DateTime.Now;
            Context.SaveChanges();
            var DayOfYear = DateTime.Today.Day;
            var Year = DateTime.Today.Year;
            var Month = DateTime.Today.Month;
          //  var item_History =  (from a in Context.Item_History where a.CreatedDate.Day == DayOfYear && a.CreatedDate.Month == Month && a.CreatedDate.Month == Month && a.Sale_Master_Code == _SaleMaster.SaleMasterCode select a).ToList();
            var _item_History_Transactions =  (from a in Context.Item_History_transaction where a.CreatedDate.Day == DayOfYear && a.CreatedDate.Month == Month &&a.CreatedDate.Month == Month && a.SaleMasterCode == saleMasterCode select a).ToList();

          

                _item_History_Transactions.ForEach(x => {

                   
                    using (PointOfSaleEntities cont = new PointOfSaleEntities())
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



           



       



            using (PointOfSaleEntities context2 = new PointOfSaleEntities())
            {
                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == FocusRow.SaleMasterCode && w.EntryDate == FocusRow.EntryDate && w.Operation_Type_Id==2 && w.IsDeleted == 0).ToList();
                if (Details.Count > 0)
                {
                foreach (var item in Details)
                {

                        context2.SaleDetails.Where(shft => shft.Operation_Type_Id == 2 &&  shft.IsDeleted == 0  && shft.EntryDate == FocusRow.EntryDate && shft.SaleMasterCode == FocusRow.SaleMasterCode).ToList().ForEach(x => {
                      x.Operation_Type_Id = 3;
                      x.LastDateModif = DateTime.Now;
                      
                      });

                      
                        context2.SaveChanges();
                }

                }
             
            }

            


            using (PointOfSaleEntities cont  = new PointOfSaleEntities())
            {
                gcSaleMaster.DataSource = null;
               
                gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.EntryDate == DateTime.Today && x.Operation_Type_Id == 2 && x.IsDeleted == 0).ToList();
                gcSaleMaster.RefreshDataSource();
            }

            if (gvSaleMaster.RowCount == 0)
            {

                groupControl1.Enabled = false;
            
            }
        }
    }
}