
using DataRep;
using FastReport;
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
        SaleEntities context = new SaleEntities();
        public frmProfitandLossReport()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {



            // Get Total Sale
            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
            var Sales = context.SaleMasterViews.Where(x=>x.IsDeleted==0 && x.Operation_Type_Id==2 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<SaleMasterView>();
            double TotalSales = 0;
            Sales.ForEach(x =>
            {
                TotalSales += x.TotalBeforDiscount;
            });





            // Get Total Expenses
            var Expenses = context.ExpensesViews.Where(x => x.IsDeleted == 0 && x.Date  >= dtFrom.DateTime &&  x.Date  <= dateTo).ToList();
            double TotalExpenses = 0;
            Expenses.ForEach(x =>
            {
                TotalExpenses += x.ExpensesQT;
            });


            // Get TOtal Discount
            var Descount = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Discount>0 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<SaleMasterView>();
            double TotalDescount = 0;
            Descount.ForEach(x =>
            {
                TotalDescount += x.Discount;
            });


            // Get Purchess Count
            var Purchess = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 1 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo).ToList<SaleMasterView>();
            double TotalPurchess = 0;
            Purchess.ForEach(x =>
            {
                TotalPurchess += x.FinalTotal;
            });


            List<ProfitAndLosse> _profitAndLosseList = new List<ProfitAndLosse>();
            ProfitAndLosse _profitAndLosse = new ProfitAndLosse() { 
            
            Descount = TotalDescount,
            TotalExpenses =TotalExpenses,
            TotalPurchess = TotalPurchess,
            TotalSales = TotalSales,
            ProfitOrLosse  = TotalSales - (TotalDescount+ TotalExpenses+ TotalPurchess)

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
            // rpt.Design();



        }

        private void frmProfitandLossReport_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
        }
    }
}
