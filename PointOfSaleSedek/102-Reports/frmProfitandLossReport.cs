
using DataRep;
using FastReport;
using PointOfSaleSedek.HelperClass;
using PointOfSaleSedek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSaleSedek._102_Reports
{
    public partial class frmProfitandLossReport : MaterialSkin.Controls.MaterialForm
    {
        POSEntity context = new POSEntity();
        BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities();
        readonly Static st = new Static();
        public frmProfitandLossReport()
        {
            InitializeComponent();
            langu();
            FillslkWarhouse();
        }

        public void FillslkWarhouse()
        {
            DataTable dt = new DataTable();
            var result = context.Branches.ToList();
            slkWarhouse.Properties.DataSource = result;
            slkWarhouse.Properties.ValueMember = "Branches_Code";
            slkWarhouse.Properties.DisplayMember = "Branches_Name";

            Int64 branchCode = st.GetBranch_Code();
            if (branchCode != 0)
            {
                slkWarhouse.EditValue = branchCode;
                slkWarhouse.Enabled = false;
               
            }
        }
        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            //this.RightToLeftLayout = st.isEnglish() ? true : false;
            this.Text = st.isEnglish() ? "Profits and Losses Bills" : "تقرير المصروفات";


            materialLabel1.Text = st.isEnglish() ? "From Date" : "من تاريخ";

          



            materialLabel2.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
          


            simpleButton1.Text = st.isEnglish() ? "View" : "عرض";

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            Int64 selectedBranch = 0;

            if (!String.IsNullOrWhiteSpace(slkWarhouse.Text)) {
                selectedBranch = Convert.ToInt64(slkWarhouse.EditValue);
            }

            double TotalDescount = 0;
            double TotalProductCost = 0;
            double TotalExpenses = 0;
            double TotalSales = 0;

            double TotalPurchess = 0;

            // Back Office Server
            Int64 branchCode = st.GetBranch_Code();
            if (branchCode == 0)
            {

                // All
                if (selectedBranch == 0)
                {
                    // Get Total Sale
                    var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
                    var Sales = _server.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<BackOfficeEntity.SaleMasterView>();
                    TotalSales = 0;
                    Sales.ForEach(x =>
                    {
                        TotalSales += x.TotalBeforDiscount;
                    });





                    // Get Total Expenses
                    var Expenses = _server.ExpensesViews.Where(x => x.IsDeleted == 0 && x.Date >= dtFrom.DateTime && x.Date <= dateTo).ToList();
                    TotalExpenses = 0;
                    Expenses.ForEach(x =>
                    {
                        TotalExpenses += x.ExpensesQT;
                    });


                    // Get TOtal Discount
                    var Descount = _server.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Discount > 0 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<BackOfficeEntity.SaleMasterView>();
                    TotalDescount = 0;
                    Descount.ForEach(x =>
                    {
                        TotalDescount += x.Discount;
                    });


                    // Get Purchess Count
                    var Purchess = _server.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 1 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<BackOfficeEntity.SaleMasterView>();
                    TotalPurchess = 0;
                    Purchess.ForEach(x =>
                    {
                        TotalPurchess += x.FinalTotal;
                    });

                }
                // Only Branch
                else
                {


                    // Get Total Sale
                    var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
                    var Sales = _server.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2  && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo && x.Branches_Code == selectedBranch).ToList<BackOfficeEntity.SaleMasterView>();
                    TotalSales = 0;
                    Sales.ForEach(x =>
                    {
                        TotalSales += x.TotalBeforDiscount;
                    });





                    // Get Total Expenses
                    var Expenses = _server.ExpensesViews.Where(x => x.IsDeleted == 0 && x.Date >= dtFrom.DateTime && x.Date <= dateTo && x.Branches_Code == selectedBranch).ToList();
                    TotalExpenses = 0;
                    Expenses.ForEach(x =>
                    {
                        TotalExpenses += x.ExpensesQT;
                    });


                    // Get TOtal Discount
                    var Descount = _server.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Discount > 0 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo && x.Branches_Code == selectedBranch).ToList<BackOfficeEntity.SaleMasterView>();
                    TotalDescount = 0;
                    Descount.ForEach(x =>
                    {
                        TotalDescount += x.Discount;
                    });


                    // Get Purchess Count
                    var Purchess = _server.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 1 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo && x.Branches_Code == selectedBranch).ToList<BackOfficeEntity.SaleMasterView>();
                    TotalPurchess = 0;
                    Purchess.ForEach(x =>
                    {
                        TotalPurchess += x.FinalTotal;
                    });
                }



            }
            else
            {
                // Branch Local
                // Get Total Sale
                var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
                var Sales = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<SaleMasterView>();
                  TotalSales = 0;
                Sales.ForEach(x =>
                {


                    TotalSales += x.TotalBeforDiscount;


                    // Get TotalProductCost
                    var ItemHisttrans = context.Item_History_transaction.Where(x2 => x2.SaleMasterCode == x.SaleMasterCode && x2.shiftCode == x.Shift_Code).ToList();

                    ItemHisttrans.ForEach(y => {

                        var Item_History = context.Item_History.FirstOrDefault(y2 => y2.Id == y.Item_History_Id);

                        if (Item_History != null) {
                            TotalProductCost += Item_History.Price_Buy * y.Trans_Out;
                        }
                       

                    });

                });





                // Get Total Expenses
                var Expenses = context.ExpensesViews.Where(x => x.IsDeleted == 0 && x.Date >= dtFrom.DateTime && x.Date <= dateTo).ToList();
                  TotalExpenses = 0;
                Expenses.ForEach(x =>
                {
                    TotalExpenses += x.ExpensesQT;
                });


                // Get TOtal Discount
                var Descount = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Discount > 0 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<SaleMasterView>();
                  TotalDescount = 0;
                Descount.ForEach(x =>
                {
                    TotalDescount += x.Discount;
                });


                // Get Purchess Count
                var Purchess = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 1 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<SaleMasterView>();
                  TotalPurchess = 0;
                Purchess.ForEach(x =>
                {
                    TotalPurchess += x.FinalTotal;
                });


               











            }



            List<ProfitAndLosse> _profitAndLosseList = new List<ProfitAndLosse>();


            ProfitAndLosse _profitAndLosse = new ProfitAndLosse() { 
            
            Descount = TotalDescount,
            TotalExpenses =TotalExpenses,
            TotalPurchess = TotalPurchess,
                TotalProductCost = TotalProductCost,
            TotalSales = TotalSales,
           // ProfitOrLosse  = TotalSales - (TotalDescount+ TotalExpenses+ TotalPurchess)
            ProfitOrLosse  = TotalSales - (TotalDescount+ TotalExpenses+ TotalProductCost)

            };

            _profitAndLosseList.Add(_profitAndLosse);
            DataTable dtToDateTime = new DataTable();
            dtToDateTime.Columns.Add("FromDate", typeof(string));
            dtToDateTime.Columns.Add("ToDate", typeof(string));
            DataRow drr = dtToDateTime.NewRow();
            drr[0] = dtFrom.Text;
            drr[1] = dtTo.Text;
            dtToDateTime.Rows.Add(drr);
            Report rpt = new Report();
            rpt.Load(@"Reports\ProfitandLossReport.frx");
            rpt.RegisterData(_profitAndLosseList, "Header");
            rpt.RegisterData(dtToDateTime, "Dates");
            rpt.Show();
            // rpt.PrintSettings.ShowDialog = false;
         //  rpt.Design();



        }

        private void frmProfitandLossReport_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
        }
    }
}
