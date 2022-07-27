﻿using PointOfSaleSedek._101_Adds;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using PointOfSaleSedek._105_Reports;
using PointOfSaleSedek._101_Adds.CasherShift;
using PointOfSaleSedek._101_Adds._112_Users;
using PointOfSaleSedek.HelperClass;
using PointOfSaleSedek._101_Adds._102_Customer;
using PointOfSaleSedek._101_Adds._113_BarCode;
using PointOfSaleSedek._101_Adds._103_Authentication;
using PointOfSaleSedek._102_Reports;
using PointOfSaleSedek._114_Adds;
using PointOfSaleSedek._101_Adds._114_AddExpenses;
using DataRep;
using System.Timers;
using Google.Cloud.Firestore;
using Timer = System.Timers.Timer;
using System.Net;
using DevExpress.XtraBars;
using System.Data.OleDb;
 

namespace PointOfSaleSedek
{
    public partial class FrmMain : MaterialSkin.Controls.MaterialForm
    {
        SaleEntities context = new SaleEntities();
        readonly SaleEntities db = new SaleEntities();
        readonly Static st = new Static();
       Timer aTimer = new Timer(60 * 60 * 1000); //one hour in milliseconds

       FirestoreDb fdb;


          void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            if (CheckForInternetConnection()) { 
               try {
                updateToFireBase();
            }
            catch  {
            }
            }
         
            
        }


        public static bool CheckForInternetConnection(int timeoutMs = 10000, string url = "http://www.google.com/")
        {
            try
            {
               
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = timeoutMs;
                using (var response = (HttpWebResponse)request.GetResponse())
                    return true;
            }
            catch
            {
                return false;
            }
        }





        void updateToFireBase()
        {



            //// Get Total Sale
            DateTime today = DateTime.Now;
            var Sales = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.EntryDate.Day == today.Day && x.EntryDate.Month == today.Month && x.EntryDate.Year == today.Year).ToList<SaleMasterView>();
            double TotalSales = 0;
            Sales.ForEach(x =>
            {
                TotalSales += x.TotalBeforDiscount;
            });




            // Get Total Expenses
            var Expenses = context.ExpensesViews.Where(x => x.IsDeleted == 0 && x.Date.Day == today.Day && x.Date.Month == today.Month && x.Date.Year == today.Year).ToList();
            double TotalExpenses = 0;
            Expenses.ForEach(x =>
            {
                TotalExpenses += x.ExpensesQT;
            });



            // Get TOtal Discount
            var Descount = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Discount > 0 && x.EntryDate.Day == today.Day && x.EntryDate.Month == today.Month && x.EntryDate.Year == today.Year).ToList<SaleMasterView>();
            double TotalDescount = 0;
            Descount.ForEach(x =>
            {
                TotalDescount += x.Discount;
            });


            String projectID = "1";
            String adminId = "1";
            DocumentReference Doc = fdb.Collection("Balance").Document(adminId).Collection(projectID).Document();



            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"AdminId",01115730802 },
                {"ProjectId",1 },
                {"TotalSales",TotalSales},
                {"TotalExpenses",TotalExpenses },
                {"TotalDescount",TotalDescount },
                {"LastDateUpdate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")},

            };
            Doc.SetAsync(data1);
           
        }

        public FrmMain()
        {
            InitializeComponent();


            string path = AppDomain.CurrentDomain.BaseDirectory + @"foodapp.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            fdb = FirestoreDb.Create("pointofsale-d3e8d");

            RibbonControl s = new RibbonControl();
            s.BackColor = Color.Red;

            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Start();



            // SplashScreenManager.CloseForm();

        }





        private void BtRefersh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Authentication();



        }


        void HideAllTabs()
        {
            RbAddEmployee.Visible = false;
            RbItems.Visible = false;
            RbBranches.Visible = false;
            RbCode.Visible = false;
            RbShiftEnd.Visible = false;
            RbShiftStart.Visible = false;
            RbCasher.Visible = false;
            RbPurches.Visible = false;
            RbStore.Visible = false;
            RbUser.Visible = false;
            RbAuth.Visible = false;
            RbBarCode.Visible = false;
            RbAddExpenses.Visible = false;
            RbAddExpensesTran.Visible = false;
            RbCancelExpenses.Visible = false;

            RbShifts.Visible = false;
            btnRefershShiftsData.Visibility = BarItemVisibility.Never;
            RbCasherTab.Visible = false;
            RbStockTab.Visible = false;
            BrAddauthenticationTab.Visible = false;
            RbInvoicesTab.Visible = false;
            RbReportsTab.Visible = false;
            RbCodeTab.Visible = false;
            RbPurchessReport.Visible = false;
            RbSaleReport.Visible = false;

            RbCancelationInvoiceReport.Visible = false;
            RbProfitAndLossReport.Visible = false;
            RbExpenses.Visible = false;

        }


         


        void Authentication()
        {
            HideAllTabs();
            Int64 User_Code = st.User_Code();
            List<User_View> User;
            User = db.User_View.Where(View => View.Employee_Code == User_Code && (View.UserFlag == true) && View.IsDeleted == 0 && View.IsDeletedEmployee == 0).ToList();
            foreach (var item in User)
            {

                barStaticItem12.Caption = item.Employee_Name;
                barStaticItem12.Appearance.ForeColor = Color.Red;

                barStaticItem13.Caption = item.Employee_Name;

                barStaticItem13.Appearance.ForeColor = Color.Red;
                barStaticItem14.Caption = item.Employee_Name;
                barStaticItem14.Appearance.ForeColor = Color.Red;
                barStaticItem15.Caption = item.Employee_Name;
                barStaticItem15.Appearance.ForeColor = Color.Red;
                barStaticItem16.Caption = item.Employee_Name;
                barStaticItem16.Appearance.ForeColor = Color.Red;
                barStaticItem17.Caption = item.Employee_Name;
                barStaticItem17.Appearance.ForeColor = Color.Red;
                barStaticItem18.Caption = item.Employee_Name;
                barStaticItem18.Appearance.ForeColor = Color.Red;


            }
            var result = db.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();
            for (var i = 0; i < result.Count; i++)
            {
                string _TabName = result[i].Tab_Name;


                switch (_TabName)
                {
                    case "RbAuth":
                        RbAuth.Visible = true;
                        break;
                    case "RbUser":
                        RbUser.Visible = true;
                        break;
                    case "RbStore":
                        RbStore.Visible = true;
                        break;
                    case "RbPurches":
                        RbPurches.Visible = true;
                        break;
                    case "RbCasher":
                        RbCasher.Visible = true;
                        break;
                    case "RbShiftStart":
                        RbShiftStart.Visible = true;
                        break;
                    case "RbShiftEnd":
                        RbShiftEnd.Visible = true;
                        break;
                    case "RbCode":
                        RbCode.Visible = true;
                        break;
                    case "RbBranches":
                        RbBranches.Visible = true;
                        break;
                    case "RbItems":
                        RbItems.Visible = true;
                        break;
                    case "RbAddEmployee":
                        RbAddEmployee.Visible = true;
                        break;


                    case "RbPurchessReport":
                        RbPurchessReport.Visible = true;
                        break;

                    case "RbSaleReport":
                        RbSaleReport.Visible = true;
                        break;

                    case "RbBarCode":
                        RbBarCode.Visible = true;
                        break;

                    case "RbAddExpenses":
                        RbAddExpenses.Visible = true;
                        break;
                    case "RbAddExpensesTran":
                        RbAddExpensesTran.Visible = true;
                        break;
                    case "RbCancelExpenses":
                        RbCancelExpenses.Visible = true;
                        break;

                    case "RbProfitAndLossReport":
                        RbProfitAndLossReport.Visible = true;
                        break;

                    case "RbExpenses":
                        RbExpenses.Visible = true;
                        break;


                    case "RbCancelationInvoiceReport":
                        RbCancelationInvoiceReport.Visible = true;
                        break;


                    case "RbShifts":
                        RbShifts.Visible = true;
                        break;



                    case "btnRefershShiftsData":
                        btnRefershShiftsData.Visibility = BarItemVisibility.Always;
                        break;

                        




                    default:
                    
                        break;
                }
              


            }

            //RbReportsTab.Visible = true;
            if (RbAddEmployee.Visible == false && RbBarCode.Visible == false && RbCode.Visible == false && RbItems.Visible == false && RbBranches.Visible == false && RbAddExpenses.Visible == false && RbCancelExpenses.Visible == false)
            {

                RbCodeTab.Visible = false;
            }
            else
            {
                RbCodeTab.Visible = true;

            }
            if (RbCasher.Visible == false && RbShiftStart.Visible == false && RbShiftEnd.Visible == false)
            {
                RbCasherTab.Visible = false;
            }
            else
            {
                RbCasherTab.Visible = true;
            }

            if (RbPurches.Visible == false  && RbAddExpensesTran.Visible == false && RbCancelExpenses.Visible == false)
            {
                RbInvoicesTab.Visible = false;
            }
            else
            {
                RbInvoicesTab.Visible = true;
            }



            if (RbStore.Visible == false)
            {
                RbStockTab.Visible = false;
            }
            else
            {
                RbStockTab.Visible = true;
            }



            if (RbUser.Visible == false && RbAuth.Visible == false)
            {
                BrAddauthenticationTab.Visible = false;
            }
            else
            {
                BrAddauthenticationTab.Visible = true;
            }



            if (RbUser.Visible == false && RbAuth.Visible == false)
            {
                BrAddauthenticationTab.Visible = false;
            }
            else
            {
                BrAddauthenticationTab.Visible = true;
            }



            if (RbSaleReport.Visible == false &&
                RbPurchessReport.Visible == false &&
                RbCancelationInvoiceReport.Visible == false
                &&
                RbProfitAndLossReport.Visible == false
                &&
                RbExpenses.Visible == false
                &&
                RbShifts.Visible == false

                )
            {
                RbReportsTab.Visible = false;
            }
            else
            {
                RbReportsTab.Visible = true;
            }



        }
        private void FrmMain_Load(object sender, EventArgs e)
        {



            Authentication();


        }





        private void BrCustomerReport_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //SplashScreenManager.ShowForm(typeof(WaitForm1));
            //var result = (from a in db.Customer_Info where a.IsDeleted == 0 select a).ToList();

            //Report rpt = new Report();
            //rpt.Load(@"Reports\CustomerReport.frx");
            //rpt.RegisterData(result, "Lines");
            //rpt.Show();
            //SplashScreenManager.CloseForm();

        }





        private void BrUserReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //SplashScreenManager.ShowForm(typeof(WaitForm1));
            //var result = (from a in db.User_Detail_View where a.IsDeleted == 0 select a).ToList();
            //Report rpt = new Report();
            //rpt.Load(@"Reports\UsersReport.frx");
            //rpt.RegisterData(result, "Lines");
            //rpt.Show();
            //SplashScreenManager.CloseForm();

        }

        private void BtnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (st.Project_Type() == "Cafe") {
                frmCafeSales frm = new frmCafeSales();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "Perfum") {
                frmPerfumSales frm = new frmPerfumSales();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "SuperMarket")
            {
                frmSuperMarketSales frm = new frmSuperMarketSales();
                frm.ShowDialog();
            }
           
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCodes frm = new frmCodes();
            frm.ShowDialog();
        }

        private void BarAddUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

           
             frmUser frm = new frmUser();
             frm.ShowDialog();

            

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (st.Project_Type() == "Cafe")
            {
                frmCafeItemCard frm = new frmCafeItemCard();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "Perfum")
            {
                frmPerfumItemCard frm = new frmPerfumItemCard();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "SuperMarket")
            {
                frmSuperMarketItemCard frm = new frmSuperMarketItemCard();
                frm.ShowDialog();
            }
 
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPurchasescs frm = new frmPurchasescs();
            frm.ShowDialog();
        }

        private void BrAddauthentication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAuthentication frm = new FrmAuthentication();
            frm.ShowDialog();



            Authentication();



        }

        private void barButtonItem15_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPurchessReport frm = new frmPurchessReport();
            frm.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //frmSaleReports frm = new frmSaleReports();
            //frm.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmShiftStart frm = new frmShiftStart();
            frm.ShowDialog();
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmShiftEnd frm = new frmShiftEnd();
            frm.ShowDialog();
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmStorge frm = new frmStorge();
            frm.ShowDialog();
        }

        private void barButtonItem19_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBranches frm = new frmBranches();
            frm.ShowDialog();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEmployees frm = new frmEmployees();
            frm.ShowDialog();
        }

        private void barStaticItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             
              Authentication();
            if (CheckForInternetConnection())
            {
                try
                {
                    updateToFireBase();
                }
                catch
                {
                }
            }
        }

        private void BrAddCustomer_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCustomer frm = new frmCustomer();
            frm.ShowDialog();
        }

        private void btnEmployee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmEmployees frm = new frmEmployees();
            frm.ShowDialog();
        }

        private void btnItems_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (st.Project_Type() == "Cafe")
            {
                frmCafeItemCard frm = new frmCafeItemCard();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "Perfum")
            {
                frmPerfumItemCard frm = new frmPerfumItemCard();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "SuperMarket")
            {
                frmSuperMarketItemCard frm = new frmSuperMarketItemCard();
                frm.ShowDialog();
            }
          
        }

        private void btnBranch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmBranches frm = new frmBranches();
            frm.ShowDialog();
        }

        private void btnCode_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCodes frm = new frmCodes();
            frm.ShowDialog();
        }

        private void barHeaderItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmUser frm = new frmUser();
            frm.ShowDialog();
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmSaleReports frm = new frmSaleReports();
            frm.ShowDialog();
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPurchessReport frm = new frmPurchessReport();
            frm.ShowDialog();
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //frmBarCode frm = new frmBarCode();
            //frm.ShowDialog();
            frmBarCodes frm = new frmBarCodes();
            frm.ShowDialog();

        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCanelationInvoice frm = new frmCanelationInvoice();
            frm.ShowDialog();
        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmExpenses frm = new frmExpenses();
            frm.ShowDialog();
        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmExpensesTransaction frm = new frmExpensesTransaction();
            frm.ShowDialog();
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmCancelExpenses frm = new FrmCancelExpenses();
            frm.ShowDialog();
        }

        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmProfitandLossReport frm = new frmProfitandLossReport();
            frm.ShowDialog();
        }

        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmExpensescs frm = new frmExpensescs();
            frm.ShowDialog();
        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmShiftsReport frm = new frmShiftsReport();
            frm.ShowDialog();
        }

        DataTable dt = new DataTable();

        void removeEmpityData() {

            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                if (dt.Rows[i][1] == DBNull.Value)
                {
                    dt.Rows[i].Delete();
                }
            }
            dt.AcceptChanges();


            List<Customer_Info> _listcustomer = new List<Customer_Info>();


            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {

                Int64? MaxCode = context.Customer_Info.Max(u => (Int64?)u.Customer_Code + 1);

                if (MaxCode == 0 || MaxCode == null) {
                    MaxCode = 1;
                }

                Customer_Info _customer = new Customer_Info()
                {

                    Customer_Phone = dt.Rows[i]["رقم الموبيل"].ToString(),
                    Customer_Name = dt.Rows[i]["اسم العميل"].ToString(),
                    CustomerFavourit = dt.Rows[i]["البرفن الخاص به"].ToString(),
                    Created_Date = DateTime.Now,
                    Customer_Code = Convert.ToInt64(MaxCode),
                   
                    SexTypeCode = Convert.ToInt32(1),
                    Last_Modified_User = 0,
                    
                };

                context.Customer_Info.Add(_customer);
 

                context.SaveChanges();


            }

      
         
             


        }

          void dd()
        {
 
                    string d  = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + @"C:\Users\Eslam\Desktop\Autosaveds.xlsx" + ";Extended Properties=Excel 12.0;";
            using (OleDbConnection connection = new OleDbConnection(d))
            {
               
                OleDbCommand command = new OleDbCommand("Select * FROM [Sheet1$]", connection);
                connection.Open();
                // Create DbDataReader to Data Worksheet
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    dt.Load(dr);

                    removeEmpityData();

                   


                }
            }
            Console.ReadKey();
        }
     

        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {



            //dd();



            List<Shift_View> ressult = context.Shift_View.Where(Shift => Shift.IsDeleted == 0 && Shift.Shift_Flag == false).ToList();
            if (ressult.Count > 0)
            {

                ressult.ForEach((Shift) =>
                {

                    // 1 - Get All Seles
                    double totalShiftMaster = 0;
                    double totalShiftExpense = 0;
                    bool istotalShiftMaster = context.SaleMasters.Any(SaleMasters => SaleMasters.IsDeleted == 0 && SaleMasters.ShiftCode == Shift.Shift_Code && SaleMasters.Operation_Type_Id == 2);


                    if (istotalShiftMaster)
                    {
                        totalShiftMaster = Convert.ToDouble(context.SaleMasters.Where(master => master.IsDeleted == 0 && master.ShiftCode == Shift.Shift_Code && master.Operation_Type_Id == 2).Sum(master => master.FinalTotal));

                    }





                    bool isExpenes = context.ExpensesTransactions.Any(Expense => Expense.IsDeleted == 0 && Expense.Shift_Code == Shift.Shift_Code);

                    if (isExpenes)
                    {
                        totalShiftExpense = Convert.ToDouble(context.ExpensesTransactions.Where(Expense => Expense.IsDeleted == 0 && Expense.Shift_Code == Shift.Shift_Code).Sum(master => master.ExpensesQT));

                    }


                    // 2 - Get All Expenses



                    Shift _Shift;
                    _Shift = context.Shifts.SingleOrDefault(item => item.Shift_Code == Shift.Shift_Code && item.Shift_Flag == false);
                    _Shift.Expenses = totalShiftExpense;
                    _Shift.TotalSale = totalShiftMaster;
                    _Shift.Shift_Increase_disability = (totalShiftExpense + _Shift.Shift_End_Amount) - (_Shift.Shift_Start_Amount + totalShiftMaster);
                    context.SaveChanges();





                });

            }
        }
    }
}
