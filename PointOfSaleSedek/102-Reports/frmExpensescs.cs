﻿using DataRep;
using FastReport;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
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
        POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmExpensescs()
        {
            InitializeComponent();
            langu();
        }

        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            //this.RightToLeftLayout = st.isEnglish() ? true : false;
            this.Text = st.isEnglish() ? "Expensescs Bills" : "تقرير المصروفات";


            materialLabel1.Text = st.isEnglish() ? "From Date" : "من تاريخ";

            gridColumn7.Caption = st.isEnglish() ? "Name" : "الاسم";



            materialLabel2.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
            this.materialLabel3.Text = st.isEnglish() ? "Employee (optional)" : "الموظف(اختياري)";


            simpleButton1.Text = st.isEnglish() ? "View" : "عرض";

        }

        private void frmExpensescs_Load(object sender, EventArgs e)


        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
            FillSlkEmployees();
        }


        public void FillSlkEmployees()
        {
            var result = context.Employee_View.Where(user => user.IsDeleted == 0 ).ToList();
            slkUsers.Properties.DataSource = result;
            slkUsers.Properties.ValueMember = "Employee_Code";
            slkUsers.Properties.DisplayMember = "Employee_Name";

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
            List<ExpensesView> ExpenssesList = new List<ExpensesView>();

            if (string.IsNullOrWhiteSpace(slkUsers.Text))
            {
                ExpenssesList  = context.ExpensesViews.Where(a => a.Date >= dtFrom.DateTime   && a.Date <= dateTo && a.IsDeleted == 0).ToList();
            }
            else
            {
                Int64 empCode = Convert.ToInt64(slkUsers.EditValue);
                ExpenssesList = context.ExpensesViews.Where(a => a.Date >= dtFrom.DateTime && a.Emp_Code == empCode && a.Date <= dateTo && a.IsDeleted == 0).ToList();
            }

            
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
               // rpt.Design();
                 rpt.Show();
            }
            else {


                MaterialMessageBox.Show(st.isEnglish() ? "There are no charges for this date" : "لا يوجد مصروفات لهذا التاريخ", MessageBoxButtons.OK);
     
                    return;
                
            }
                

              

            }

       

        
    }
    }

