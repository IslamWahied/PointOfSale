using DataRep;
using FastReport;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.Model;
using System;
using System.Collections.Generic;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;

namespace PointOfSaleSedek._102_Reports
{
    public partial class frmExpensescs : MaterialSkin.Controls.MaterialForm
    {
        SaleEntities context = new SaleEntities();
        public frmExpensescs()
        {
            InitializeComponent();
        }

        private void frmExpensescs_Load(object sender, EventArgs e)


        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
            var ExpenssesList = context.ExpensesViews.Where(a => a.Date >= dtFrom.DateTime && a.Date <= dateTo && a.IsDeleted == 0).ToList();
            double FinalTotal = 0;
            if (ExpenssesList.Count > 0)
            {
                try
                {
                    FinalTotal = ExpenssesList.Sum(x => x.ExpensesQT);
                }
                catch
                {
                    FinalTotal = 0;
                }

                FinalTotal _FinalTotal = new FinalTotal()
                {
                    Total = FinalTotal,
                    TotalDiscount = 0
                };
                List<FinalTotal> _FinalTotalList = new List<FinalTotal>();
                _FinalTotalList.Add(_FinalTotal);



                Report rpt = new Report();
                rpt.Load(@"Reports\frmExpensessReport.frx");
                
                rpt.RegisterData(ExpenssesList, "ExpenssesList");
                rpt.RegisterData(_FinalTotalList, "FinalTotal");

                // rpt.PrintSettings.ShowDialog = false;
                //    rpt.Design();
                rpt.Show();
            }
            else {

                

                    MaterialMessageBox.Show("لا يوجد مصروفات لهذه المده", MessageBoxButtons.OK);
                    return;
                
            }
                

              

            }
        }
    }

