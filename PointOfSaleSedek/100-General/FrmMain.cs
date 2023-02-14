using DataRep;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSplashScreen;
using Google.Cloud.Firestore;
using PointOfSaleSedek._101_Adds;
using PointOfSaleSedek._101_Adds._102_Customer;
using PointOfSaleSedek._101_Adds._103_Authentication;
using PointOfSaleSedek._101_Adds._111_Warehouse;
using PointOfSaleSedek._101_Adds._112_Users;
using PointOfSaleSedek._101_Adds._113_BarCode;
using PointOfSaleSedek._101_Adds._114_AddExpenses;
using PointOfSaleSedek._101_Adds.CasherShift;
//using System.Data.OleDb;
//using Newtonsoft.Json;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek._102_Reports;
using PointOfSaleSedek._105_Reports;
using PointOfSaleSedek._114_Adds;
using PointOfSaleSedek.HelperClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace PointOfSaleSedek
{
    public partial class FrmMain : MaterialSkin.Controls.MaterialForm
    {
        POSEntity localContext = new POSEntity();

        readonly POSEntity db = new POSEntity();
        BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities();


        readonly Static st = new Static();
        Timer aTimer = new Timer(60 * 60 * 1000); //one hour in milliseconds

        FirestoreDb fdb;


        void OnTimedEvent(object source, ElapsedEventArgs e)
        {

            if (CheckForInternetConnection())
            {
                try
                {
                    //updateToFireBase();
                    try
                    {
                        Update_From_Back_Office(true);
                    }
                    catch
                    {

                    }
                    //MaterialMessageBox.Show("تم رفع البيانات بنجاح", MessageBoxButtons.OK);
                }
                catch
                {

                    //MaterialMessageBox.Show("لا يوجد اتصال بالانترنت", MessageBoxButtons.OK);   
                }

                try
                {
                    UploadSaleForServer();
                    UploadExpensesForServer();
                }
                catch
                {

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
            // Date Now


            DateTime today = DateTime.Now;


            localContext = new POSEntity();
            ProjectInfo ProjectInfos = localContext.ProjectInfoes.FirstOrDefault();



            // Get Sale Master Data
            localContext = new POSEntity();
            List<SaleMasterView> masterView = localContext.SaleMasterViews.Where(x => x.isUploaded == false && x.IsDeleted == 0).ToList();

            masterView.ForEach(master =>
            {

                FirestoreDb fdb1 = FirestoreDb.Create("pointofsale-d3e8d");

                DocumentReference Doc1 = fdb.Collection("SaleMaster").Document("ProjectCode").Collection(ProjectInfos.ProjectCode.ToString()).Document();

                Dictionary<string, object> data1 = new Dictionary<string, object>() {

                {"SaleMasterCode",master.SaleMasterCode},
                {"TotalBeforDiscount",master.TotalBeforDiscount},
                { "Discount",master.Discount},
                { "QtyTotal",master.QtyTotal},
                { "EntryDate",master.EntryDate.ToString("MM-dd-yyyy hh:mm tt")},
                { "FinalTotal", master.FinalTotal},
                { "UserName",master.UserName},
                { "Emp_Code",master.Emp_Code},
                { "Shift_Code", master.Shift_Code},
                { "PaymentType",master.PaymentType},
                { "OperationTypeId",master.Operation_Type_Id},

                { "LastUpdateDate",today.ToString("MM-dd-yyyy hh:mm tt")},

                };
                Doc1.SetAsync(data1);

                SaleMaster _saleMaster;
                _saleMaster = localContext.SaleMasters.SingleOrDefault(Salmaster => Salmaster.ShiftCode == master.Shift_Code && Salmaster.SaleMasterCode == master.SaleMasterCode && Salmaster.isUploaded == false);
                _saleMaster.isUploaded = true;
                localContext.SaveChanges();

            });









            // Get Shifts  Data
            localContext = new POSEntity();
            List<Shift_View> masterShiftView = localContext.Shift_View.Where(x => x.isUploaded == false && x.Shift_Flag == false && x.IsDeleted == 0).ToList();


            masterShiftView.ForEach(shift =>
            {
                FirestoreDb fdb2 = FirestoreDb.Create("pointofsale-d3e8d");

                DocumentReference Doc2 = fdb.Collection("Shifts").Document("ProjectCode").Collection(ProjectInfos.ProjectCode.ToString()).Document();


                Dictionary<string, object> data2 = new Dictionary<string, object>() {

                {"ShiftCode",shift.Shift_Code},
                {"ShiftStartDate",shift.Shift_Start_Date.ToString("MM-dd-yyyy hh:mm tt")},
                {"ShiftEndDate",shift.Shift_End_Date?.ToString("MM-dd-yyyy hh:mm tt")},

                { "ShiftStartAmount",shift.Shift_Start_Amount},
                { "ShiftEndAmount",shift.Shift_End_Amount},
                { "ShiftIncreasedisability", shift.Shift_Increase_disability},

                { "UserName",shift.UserName},
                { "EmpCode",shift.Emp_Code},
                { "Expenses",shift.Expenses},
                { "TotalSale",shift.TotalSale},
                { "LastUpdateDate",today.ToString("MM-dd-yyyy hh:mm tt")},

                };





                Doc2.SetAsync(data2);
                Shift _ShiftMaster;
                localContext = new POSEntity();
                _ShiftMaster = localContext.Shifts.SingleOrDefault(shft => shft.Shift_Code == shift.Shift_Code && shft.isUploaded == false);
                _ShiftMaster.isUploaded = true;
                localContext.SaveChanges();
            });


            // Get Expenses  Data
            localContext = new POSEntity();
            List<ExpensesView> masterExpensesViews = localContext.ExpensesViews.Where(x => x.isUploaded == false && x.IsDeleted == 0).ToList();


            masterExpensesViews.ForEach(expenses =>
            {
                FirestoreDb fdb3 = FirestoreDb.Create("pointofsale-d3e8d");
                DocumentReference Doc3 = fdb.Collection("Expenses").Document("ProjectCode").Collection(ProjectInfos.ProjectCode.ToString()).Document();

                Dictionary<string, object> data3 = new Dictionary<string, object>() {


                {"ExpensesName",expenses.ExpensesName},
                {"UserName",expenses.UserName},
                {"Date",expenses.Date.ToString("MM-dd-yyyy hh:mm tt")},
                { "ExpensesNotes",expenses.ExpensesNotes},
                { "ExpensesQT",expenses.ExpensesQT},
                { "ExpensesCode",expenses.ExpensesCode},
                { "Shift_Code", expenses.Shift_Code},
                { "Employee_Name",expenses.Employee_Name},
                { "Emp_Code",expenses.Emp_Code},
                { "LastUpdateDate",today.ToString("MM-dd-yyyy hh:mm tt")},


                };




                Doc3.SetAsync(data3);

                ExpensesTransaction _ExpensesTransactionMaster;
                localContext = new POSEntity();
                _ExpensesTransactionMaster = localContext.ExpensesTransactions.Where(Expe => Expe.Shift_Code == expenses.Shift_Code && Expe.isUploaded == false && Expe.Id == expenses.Id).First();
                _ExpensesTransactionMaster.isUploaded = true;
                localContext.SaveChanges();
            });




            // Get Employees  Data
            localContext = new POSEntity();
            List<Employee> masterEmployee = localContext.Employees.Where(x => x.isUploaded == false && x.IsDeleted == 0).ToList();


            masterEmployee.ForEach(Employe =>
            {
                FirestoreDb fdb3 = FirestoreDb.Create("pointofsale-d3e8d");
                DocumentReference Doc4 = fdb.Collection("Employee").Document("ProjectCode").Collection(ProjectInfos.ProjectCode.ToString()).Document();

                Dictionary<string, object> data4 = new Dictionary<string, object>() {


                {"BranchID",Employe.Branch_ID},
                {"EmployeeCode",Employe.Employee_Code},
                {"EmployeeName",Employe.Employee_Name},
                {"EmployeeMobile1",Employe.Employee_Mobile_1},
                {"EmployeeMobile2",Employe.Employee_Mobile_2},
                {"SexTypeCode",Employe.SexTypeCode},
                { "LastUpdateDate",today.ToString("MM-dd-yyyy hh:mm tt")},
                };




                Doc4.SetAsync(data4);

                Employee _Employee;
                localContext = new POSEntity();
                _Employee = localContext.Employees.SingleOrDefault(emp => emp.Employee_Code == Employe.Employee_Code && emp.isUploaded == false);
                _Employee.isUploaded = true;
                localContext.SaveChanges();
            });




            // Upload Project Info
            localContext = new POSEntity();
            List<ProjectMangerInfo> projectMangerInfos = localContext.ProjectMangerInfoes.Where(element => element.isUploaded == false).ToList();

            List<Dictionary<string, object>> listUsers = new List<Dictionary<string, object>>();
            // Upload Ussers


            FirestoreDb fdb5 = FirestoreDb.Create("pointofsale-d3e8d");
            DocumentReference Doc5 = fdb.Collection("UsersInfo").Document();
            projectMangerInfos.ForEach(element =>
            {

                Dictionary<string, object> data5 = new Dictionary<string, object>() {



                {"MangerCode",element.MangerCode},
                {"MangerName",element.MangerName},
                { "ProjectSequence",0},
                { "ProjectId",0},

                { "IsActive",element.IsActive},
                { "MangerMobile",element.MangerMobile},
            };

                listUsers.Add(data5);

                Doc5.SetAsync(data5);

                ProjectMangerInfo _ProjectMangerInfo;
                localContext = new POSEntity();
                _ProjectMangerInfo = localContext.ProjectMangerInfoes.SingleOrDefault(emp => emp.MangerCode == element.MangerCode && emp.isUploaded == false);
                _ProjectMangerInfo.isUploaded = true;
                localContext.SaveChanges();


            });


            FirestoreDb fdb6 = FirestoreDb.Create("pointofsale-d3e8d");
            DocumentReference Doc6 = fdb.Collection("Projects").Document(ProjectInfos.ProjectCode.ToString());
            Dictionary<string, object> data6 = new Dictionary<string, object>() {

                {"ProjectCode",ProjectInfos.ProjectCode},
                {"ProjectName",ProjectInfos.ProjectName},
                {"image",ProjectInfos.ImageUrl??""},

                {"IsActive",ProjectInfos.isActive},
                {"listUsers",listUsers},

            };
            Doc6.SetAsync(data6);





            DocumentReference Doc14 = fdb.Collection("LastUpdateDate").Document("ProjectCode").Collection(ProjectInfos.ProjectCode.ToString()).Document("1");

            Dictionary<string, object> data14 = new Dictionary<string, object>() {
                { "LastUpdateDate",today.ToString("MM-dd-yyyy hh:mm tt")},
                };




            Doc14.SetAsync(data14);






        }

        //void updateToFireBase()
        //{



        //    //// Get Total Sale
        //    DateTime today = DateTime.Now;
        //    var Sales = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.EntryDate.Day == today.Day && x.EntryDate.Month == today.Month && x.EntryDate.Year == today.Year).ToList<SaleMasterView>();
        //    double TotalSales = 0;
        //    Sales.ForEach(x =>
        //    {
        //        TotalSales += x.TotalBeforDiscount;
        //    });




        //    // Get Total Expenses
        //    var Expenses = context.ExpensesViews.Where(x => x.IsDeleted == 0 && x.Date.Day == today.Day && x.Date.Month == today.Month && x.Date.Year == today.Year).ToList();
        //    double TotalExpenses = 0;
        //    Expenses.ForEach(x =>
        //    {
        //        TotalExpenses += x.ExpensesQT;
        //    });



        //    // Get TOtal Discount
        //    var Descount = context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Discount > 0 && x.EntryDate.Day == today.Day && x.EntryDate.Month == today.Month && x.EntryDate.Year == today.Year).ToList<SaleMasterView>();
        //    double TotalDescount = 0;
        //    Descount.ForEach(x =>
        //    {
        //        TotalDescount += x.Discount;
        //    });


        //    String projectID = "1";
        //    String adminId = "1";
        //    DocumentReference Doc = fdb.Collection("Balance").Document(adminId).Collection(projectID).Document();



        //    Dictionary<string, object> data1 = new Dictionary<string, object>()
        //    {
        //        {"AdminId",01115730802 },
        //        {"ProjectId",1 },
        //        {"TotalSales",TotalSales},
        //        {"TotalExpenses",TotalExpenses },
        //        {"TotalDescount",TotalDescount },
        //        {"LastDateUpdate", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")},

        //    };
        //    Doc.SetAsync(data1);

        //}

        // Eslam
        public FrmMain()
        {


            InitializeComponent();
            AppLangu();


            // For FireBase

            //string path = AppDomain.CurrentDomain.BaseDirectory + @"foodapp.json";
            //Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            //fdb = FirestoreDb.Create("pointofsale-d3e8d");

            RibbonControl s = new RibbonControl();
            s.BackColor = Color.Red;

            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

            aTimer.Start();



        }


        public void AppLangu()
        {
            //Int64 User_Code = st.GetUser_Code();
            //User_View item = db.User_View.FirstOrDefault(View => View.Employee_Code == User_Code && View.UserFlag == true && View.IsDeleted == 0 && View.IsDeletedEmployee == 0);


            //barStaticItem12.Caption = item.Employee_Name;
            //barStaticItem12.Appearance.ForeColor = Color.Red;

            //barStaticItem13.Caption = item.Employee_Name;

            //barStaticItem13.Appearance.ForeColor = Color.Red;
            //barStaticItem14.Caption = item.Employee_Name;
            //barStaticItem14.Appearance.ForeColor = Color.Red;
            //barStaticItem15.Caption = item.Employee_Name;
            //barStaticItem15.Appearance.ForeColor = Color.Red;
            //barStaticItem16.Caption = item.Employee_Name;
            //barStaticItem16.Appearance.ForeColor = Color.Red;
            //barStaticItem17.Caption = item.Employee_Name;
            //barStaticItem17.Appearance.ForeColor = Color.Red;
            //barStaticItem18.Caption = item.Employee_Name;
            //barStaticItem18.Appearance.ForeColor = Color.Red;


            //barStaticItem20.Caption = item.Employee_Name;
            //barStaticItem20.Appearance.ForeColor = Color.Red;

            //barStaticItem19.Caption = item.Employee_Name;
            //barStaticItem19.Appearance.ForeColor = Color.Red;


            this.RightToLeft = st.isEnglish() ? System.Windows.Forms.RightToLeft.No : System.Windows.Forms.RightToLeft.Yes;
            barHeaderItem232.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barUserName.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barHeaderItem3.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barHeaderItem2.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barHeaderItem7.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barHeaderItem4.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barHeaderItem232.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            barHeaderItem8.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";


            barButtonItem22.Caption = st.isEnglish() ? "Close" : "اغلاق";
            barButtonItem22.Hint = st.isEnglish() ? "Close" : "اغلاق";

            btnUploadData.Caption = st.isEnglish() ? "Update" : "تحديث";
            btnUploadData.Hint = st.isEnglish() ? "Upload Data" : "رفع البيانات";


            /////////////////////////   Permission Tab ///////////////////////////////////////
            ///
            BrAddauthenticationTab.Text = st.isEnglish() ? "Permissions" : "الصلاحيات";
            BrAddauthentication.Caption = st.isEnglish() ? "Permissions" : "الصلاحيات";
            BrAddauthentication.Hint = st.isEnglish() ? "Add Permissions" : "اضافة صلاحية";
            barButtonItem27.Caption = st.isEnglish() ? "Users" : "المستخدمين";
            barButtonItem27.Hint = st.isEnglish() ? "Add Users" : "اضافة مستخدمين";


            //////////////////////// Coding Tab /////////////////////////////////////////////
            ///
            RbCodeTab.Text = st.isEnglish() ? "Coding" : "التكويد";


            barButtonItem38.Caption = st.isEnglish() ? "Add Warehouse" : "اضافة مخزن";


            btnBranch.Caption = st.isEnglish() ? "Branchs" : "الفروع";
            btnBranch.Hint = st.isEnglish() ? "Add Branch" : "اضافة فرع";

            btnEmployee.Caption = st.isEnglish() ? "Employees" : "الموظفين";
            btnEmployee.Hint = st.isEnglish() ? "Add Employee" : "اضافة موظف";

            btnCode.Caption = st.isEnglish() ? "Categories" : "المجموعات";
            btnCode.Hint = st.isEnglish() ? "Add Category" : "اضافة مجموعة";

            btnItems.Caption = st.isEnglish() ? "Items" : "العناصر";
            btnItems.Hint = st.isEnglish() ? "Add Item" : "اضافة عنصر";

            barButtonItem29.Caption = st.isEnglish() ? "BarCode" : "باركود";
            barButtonItem29.Hint = st.isEnglish() ? "Print BarCode" : "طباعة باركود";

            barButtonItem32.Caption = st.isEnglish() ? "Expenses" : "مصروف";
            barButtonItem32.Hint = st.isEnglish() ? "Add Expenses Name" : "اضافة اسم مصروف";



            //////////////////////// Casher Tab /////////////////////////////////////////////

            RbCasherTab.Text = st.isEnglish() ? "Casher" : "الكاشير";

            barButtonItem8.Caption = st.isEnglish() ? "Casher Screen" : "شاشة الكاشير";


            barButtonItem16.Caption = st.isEnglish() ? "Start Shift" : "فتح وردية جديدة";

            barButtonItem17.Caption = st.isEnglish() ? "End Shift" : "اغلاق وردية";




            //////////////////////// Invoices Tab /////////////////////////////////////////////



            RbInvoicesTab.Text = st.isEnglish() ? "Invoices" : "الفواتير";

            barButtonItem13.Caption = st.isEnglish() ? "Purchases Invoice" : "فاتورة مشتريات";


            barButtonItem33.Caption = st.isEnglish() ? "Expenses" : "مصروف";

            barButtonItem34.Caption = st.isEnglish() ? "Cancel Expense" : "اللغاء مصروف";




            //////////////////////// Reports Tab /////////////////////////////////////////////


            RbReportsTab.Text = st.isEnglish() ? "Reports" : "التقارير";


            barButtonItem50.Caption = st.isEnglish() ? "Product Rate" : "معدل بيع المنتجات";
            barButtonItem18.Caption = st.isEnglish() ? "Store Report" : "تقرير المخزن";
            barButtonItem28.Caption = st.isEnglish() ? "Sales Report" : "تقرير المبيعات";
            barButtonItem26.Caption = st.isEnglish() ? "Purchasing Report" : "تقرير المشتريات";
            barButtonItem36.Caption = st.isEnglish() ? "Expense Report" : "تقرير المصروفات";
            barButtonItem35.Caption = st.isEnglish() ? "Profit && Loss Report" : "تقرير الارباح والخسائر";
            barButtonItem30.Caption = st.isEnglish() ? "Canceled Sales Report" : "تقرير المبيعات الملغاة";
            barButtonItem37.Caption = st.isEnglish() ? "Shifts Report" : "تقرير الورديات";

            ////////////////////// Warehouses /////////////////////////////////////////////////////

            RbStorgeTap.Text = st.isEnglish() ? "Warehouses && Transfer " : "المخازن والتحولات";
            barButtonItem40.Caption = st.isEnglish() ? "Warehouse" : "المخزن";
            barButtonItem41.Caption = st.isEnglish() ? "Transfer" : "التحويلات";

            String Branch_Name = st.GetBranch_Name();
            ribbonPageGroup12.Text = Branch_Name;
            ribbonPageGroup16.Text = Branch_Name;
            ribbonPageGroup13.Text = Branch_Name;
            ribbonPageGroup11.Text = Branch_Name;
            ribbonPageGroup17.Text = Branch_Name;
            ribbonPageGroup21.Text = Branch_Name;
            ribbonPageGroup22.Text = Branch_Name;





        }





        private void BtRefersh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Authentication();



        }


        void HideAllTabs()
        {
            RbAddEmployee.Visible = false;
            RbStorgeTap.Visible = false;
            RbItems.Visible = false;

            RbBranches.Visible = false;
            RbCode.Visible = false;
            RbShiftEnd.Visible = false;
            RbShiftStart.Visible = false;
            RbCasher.Visible = false;
            RbPurches.Visible = false;
            RbStore.Visible = false;
            RbConversionStores.Visible = false;
            RbUser.Visible = false;
            RbAuth.Visible = false;
            RbBarCode.Visible = false;
            RbAddExpenses.Visible = false;
            RbAddExpensesTran.Visible = false;
            RbCancelExpenses.Visible = false;
            RbAddWarehouse.Visible = false;
            rbBackOffice.Visible = false;

            RbShifts.Visible = false;
            btnRefershShiftsData.Visibility = BarItemVisibility.Never;
            btnUploadData.Visibility = BarItemVisibility.Never;
            RbCasherTab.Visible = false;

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
            Int64 Breanch_Code = st.GetBranch_Code();
            Int64 User_Code = st.GetUser_Code();

            User_View item = db.User_View.FirstOrDefault(View => View.Employee_Code == User_Code && View.UserFlag == true && View.IsDeleted == 0 && View.IsDeletedEmployee == 0);


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


            barStaticItem20.Caption = item.Employee_Name;
            barStaticItem20.Appearance.ForeColor = Color.Red;

            barStaticItem19.Caption = item.Employee_Name;
            barStaticItem19.Appearance.ForeColor = Color.Red;





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


                    case "RbAddWarehouse":
                        RbAddWarehouse.Visible = true;
                        break;

                    case "RbConversionStores":
                        RbConversionStores.Visible = true;
                        break;



                    case "btnRefershShiftsData":
                        btnRefershShiftsData.Visibility = BarItemVisibility.Always;
                        break;



                    case "btnUploadData":
                        btnUploadData.Visibility = BarItemVisibility.Always;
                        break;

                    case "rbBackOffice":
                        rbBackOffice.Visible = true;
                        break;

                    case "rbProductRate":
                        rbProductRate.Visible = true;
                        break;

                        

                    default:

                        break;
                }



            }
            if (RbAddEmployee.Visible == false && RbAddWarehouse.Visible == false && RbBarCode.Visible == false && RbCode.Visible == false && RbItems.Visible == false && RbBranches.Visible == false && RbAddExpenses.Visible == false && RbCancelExpenses.Visible == false)
            {

                RbCodeTab.Visible = false;
            }
            else
            {
                RbCodeTab.Visible = true;

            }

            if (RbUser.Visible == false && RbAuth.Visible == false)
            {
                BrAddauthenticationTab.Visible = false;
            }
            else
            {
                BrAddauthenticationTab.Visible = true;
            }



            if (RbAddEmployee.Visible == false && RbAddWarehouse.Visible == false && RbBarCode.Visible == false && RbCode.Visible == false && RbItems.Visible == false && RbBranches.Visible == false && RbAddExpenses.Visible == false && RbCancelExpenses.Visible == false)
            {

                RbCodeTab.Visible = false;
            }
            else
            {
                RbCodeTab.Visible = true;

            }
            if ((RbCasher.Visible == false && RbShiftStart.Visible == false && RbShiftEnd.Visible == false) || Breanch_Code == 0)
            {
                RbCasherTab.Visible = false;
            }
            else
            {
                RbCasherTab.Visible = true;
            }

            if (RbPurches.Visible == false && RbAddExpensesTran.Visible == false && RbCancelExpenses.Visible == false)
            {
                RbInvoicesTab.Visible = false;
            }
            else
            {
                RbInvoicesTab.Visible = true;
            }






            if (RbUser.Visible == false && RbAuth.Visible == false)
            {
                BrAddauthenticationTab.Visible = false;
            }
            else
            {
                BrAddauthenticationTab.Visible = true;
            }






            if (

                 rbProductRate.Visible == false
                &&
                RbSaleReport.Visible == false 
                &&
                RbPurchessReport.Visible == false 
                &&
                RbCancelationInvoiceReport.Visible == false
                &&
                RbProfitAndLossReport.Visible == false
                &&
                RbExpenses.Visible == false
                &&
                RbShifts.Visible == false
                //&&
                //RbStore.Visible == false

                )
            {
                RbReportsTab.Visible = false;
            }
            else
            {
                RbReportsTab.Visible = true;
            }

            if (RbConversionStores.Visible == false && RbStore.Visible == false)
            {
                RbStorgeTap.Visible = false;
            }
            else
            {

                RbStorgeTap.Visible = true;
            }



            if (Breanch_Code != 0)
            {
                rbBackOffice.Visible = false;
            }
            else
            {

                rbBackOffice.Visible = true;
            }




        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            Authentication();
        }




        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (st.Project_Type() == "Cafe")
            {
                frmCafeSales frm = new frmCafeSales();
                frm.ShowDialog();
            }
            else if (st.Project_Type() == "Perfum")
            {
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


        void UploadSaleForServer()
        {


            _server = new BackOfficeEntity.db_a8f74e_posEntities();

            using (POSEntity Contexts5 = new POSEntity())
            {
                var master = Contexts5.SaleMasters.Where(e => e.is_Back_Office_Updated == false).ToList();

                master.ForEach(xx =>
                {


                    BackOfficeEntity.SaleMaster saleMaster = new BackOfficeEntity.SaleMaster()
                    {

                        FinalTotal = xx.FinalTotal,
                        is_Back_Office_Updated = xx.is_Back_Office_Updated,
                        Branch_Id = xx.Branch_Id,
                        Cash = xx.Cash,
                        Customer_Code = xx.Customer_Code,
                        Discount = xx.Discount,
                        EntryDate = xx.EntryDate,
                        IsDeleted = xx.IsDeleted,
                        isUploaded = xx.isUploaded,
                        LastDateModif = xx.LastDateModif,
                        Operation_Type_Id = xx.Operation_Type_Id,
                        Payment_Type = xx.Payment_Type,
                        QtyTotal = xx.QtyTotal,
                        SaleMasterCode = xx.SaleMasterCode,
                        ShiftCode = xx.ShiftCode,
                        TotalBeforDiscount = xx.TotalBeforDiscount,
                        UserCode = xx.UserCode,
                        UserIdTakeOrder = xx.UserIdTakeOrder,
                        Visa = xx.Visa



                    };

                    _server.SaleMasters.Add(saleMaster);

                    _server.SaveChanges();


                    SaleMaster _saleMaster;
                    _saleMaster = Contexts5.SaleMasters.SingleOrDefault(Salmaster => Salmaster.Id == xx.Id);
                    _saleMaster.is_Back_Office_Updated = true;
                    Contexts5.SaveChanges();


                });






            }

            using (POSEntity Contexts5 = new POSEntity())
            {
                var Detail = Contexts5.SaleDetails.Where(e => e.is_Back_Office_Updated == false).ToList();

                Detail.ForEach(xx =>
                {


                    BackOfficeEntity.SaleDetail saleDetail = new BackOfficeEntity.SaleDetail()
                    {
                        Branch_Id = xx.Branch_Id,
                        CustomerCode = xx.CustomerCode,
                        IsDeleted = xx.IsDeleted,
                        shiftCode = xx.shiftCode,
                        is_Back_Office_Updated = xx.is_Back_Office_Updated,
                        EntryDate = xx.EntryDate,
                        isOile = xx.isOile,
                        ItemCode = xx.ItemCode,
                        Item_DisCount = xx.Item_DisCount,
                        LastDateModif = xx.LastDateModif,
                        LineSequence = xx.LineSequence,
                        Operation_Type_Id = xx.Operation_Type_Id,
                        Price = xx.Price,
                        Qty = xx.Qty,
                        SaleDetailCode = xx.SaleDetailCode,
                        SaleMasterCode = xx.SaleMasterCode,
                        Total = xx.Total,
                        UserId = xx.UserId,
                        Warhouse_Code = xx.Warhouse_Code


                    };

                    _server.SaleDetails.Add(saleDetail);

                    _server.SaveChanges();


                    SaleDetail _saleDetail;
                    _saleDetail = Contexts5.SaleDetails.SingleOrDefault(Salmaster => Salmaster.Id == xx.Id);
                    _saleDetail.is_Back_Office_Updated = true;
                    Contexts5.SaveChanges();


                });




            }





        }

        void UploadShiftsForServer()
        {
            _server = new BackOfficeEntity.db_a8f74e_posEntities();

            using (POSEntity Contexts5 = new POSEntity())
            {
                var shifts = Contexts5.Shifts.Where(e => e.is_Back_Office_Updated == false).ToList();

                shifts.ForEach(xx =>
                {


                    BackOfficeEntity.Shift shift = new BackOfficeEntity.Shift()
                    {

                        Branch_Id = xx.Branch_Id,
                        Expenses = xx.Expenses,
                        IsDeleted = xx.IsDeleted,
                        isUploaded = xx.isUploaded,
                        is_Back_Office_Updated = xx.is_Back_Office_Updated,
                        Last_Modified_Date = xx.Last_Modified_Date,
                        Last_Modified_User = xx.Last_Modified_User,
                        Shift_Code = xx.Shift_Code,
                        Shift_End_Amount = xx.Shift_End_Amount,
                        Shift_End_Date = xx.Shift_End_Date,
                        Shift_End_Notes = xx.Shift_End_Notes,
                        Shift_Flag = xx.Shift_Flag,
                        Shift_Increase_disability = xx.Shift_Increase_disability,
                        Shift_Start_Amount = xx.Shift_Start_Amount,
                        Shift_Start_Date = xx.Shift_Start_Date,
                        Shift_Start_Notes = xx.Shift_Start_Notes,
                        TotalSale = xx.TotalSale,
                        User_Id = xx.User_Id


                    };

                    _server.Shifts.Add(shift);

                    _server.SaveChanges();


                    Shift _Shift;
                    _Shift = Contexts5.Shifts.SingleOrDefault(Salmaster => Salmaster.Id == xx.Id);
                    _Shift.is_Back_Office_Updated = true;
                    Contexts5.SaveChanges();


                });






            }

        }
        void UploadExpensesForServer()
        {
            _server = new BackOfficeEntity.db_a8f74e_posEntities();

            using (POSEntity Contexts5 = new POSEntity())
            {
                var exp = Contexts5.ExpensesTransactions.Where(e => e.is_Back_Office_Updated == false).ToList();

                exp.ForEach(xx =>
                {


                    BackOfficeEntity.ExpensesTransaction expenses = new BackOfficeEntity.ExpensesTransaction()
                    {

                        Branch_Id = xx.Branch_Id,
                        ExpensesNotes = xx.ExpensesNotes,
                        is_Back_Office_Updated = xx.is_Back_Office_Updated,
                        Id = xx.Id,
                        Date = xx.Date,
                        Emp_Code = xx.Emp_Code,
                        ExpensesCode = xx.ExpensesCode,
                        ExpensesQT = xx.ExpensesQT,
                        IsDeleted = xx.IsDeleted,
                        isUploaded = xx.isUploaded,
                        Last_Modified_By = xx.Last_Modified_By,
                        Last_Modified_Date = xx.Last_Modified_Date,
                        Shift_Code = xx.Shift_Code


                    };

                    _server.ExpensesTransactions.Add(expenses);

                    _server.SaveChanges();


                    ExpensesTransaction _expensesTransaction;
                    _expensesTransaction = Contexts5.ExpensesTransactions.SingleOrDefault(Salmaster => Salmaster.Id == xx.Id);
                    _expensesTransaction.is_Back_Office_Updated = true;
                    Contexts5.SaveChanges();


                });






            }

        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //Authentication();
            SplashScreenManager.ShowForm(typeof(WaitForm1));


            try
            {
                UploadSaleForServer();
                UploadExpensesForServer();
            }
            catch
            {

            }



            SplashScreenManager.CloseForm();

            //if (CheckForInternetConnection())
            //{

            //    try
            //    {
            //        SplashScreenManager.ShowForm(typeof(WaitForm1));
            //        updateToFireBase();
            //        SplashScreenManager.CloseForm();
            //        MaterialMessageBox.Show("تم رفع البيانات بنجاح", MessageBoxButtons.OK);


            //    }
            //    catch (Exception xx)
            //    {

            //        SplashScreenManager.CloseForm();
            //        MaterialMessageBox.Show(xx.Message, MessageBoxButtons.OK);
            //        return;
            //    }
            //}
            //else {
            //    SplashScreenManager.CloseForm();
            //    MaterialMessageBox.Show("لا يوجد اتصال بالانترنت", MessageBoxButtons.OK);
            //    return;
            //}
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

        void removeEmpityData()
        {

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

                localContext = new POSEntity();
                Int64? MaxCode = localContext.Customer_Info.Max(u => (Int64?)u.Customer_Code + 1);

                if (MaxCode == 0 || MaxCode == null)
                {
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

                localContext.Customer_Info.Add(_customer);


                localContext.SaveChanges();


            }






        }




        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            localContext = new POSEntity();
            List<Shift_View> ressult = localContext.Shift_View.Where(Shift => Shift.IsDeleted == 0 && Shift.Shift_Flag == false).ToList();
            if (ressult.Count > 0)
            {

                ressult.ForEach((Shift) =>
                {

                    // 1 - Get All Seles
                    double totalShiftMaster = 0;
                    double totalShiftExpense = 0;
                    localContext = new POSEntity();
                    bool istotalShiftMaster = localContext.SaleMasters.Any(SaleMasters => SaleMasters.IsDeleted == 0 && SaleMasters.ShiftCode == Shift.Shift_Code && SaleMasters.Operation_Type_Id == 2);


                    if (istotalShiftMaster)
                    {

                        totalShiftMaster = Convert.ToDouble(localContext.SaleMasters.Where(master => master.IsDeleted == 0 && master.ShiftCode == Shift.Shift_Code && master.Operation_Type_Id == 2).Sum(master => master.FinalTotal));

                    }





                    bool isExpenes = localContext.ExpensesTransactions.Any(Expense => Expense.IsDeleted == 0 && Expense.Shift_Code == Shift.Shift_Code);

                    if (isExpenes)
                    {
                        totalShiftExpense = Convert.ToDouble(localContext.ExpensesTransactions.Where(Expense => Expense.IsDeleted == 0 && Expense.Shift_Code == Shift.Shift_Code).Sum(master => master.ExpensesQT));

                    }


                    // 2 - Get All Expenses



                    Shift _Shift;
                    localContext = new POSEntity();
                    _Shift = localContext.Shifts.SingleOrDefault(item => item.Shift_Code == Shift.Shift_Code && item.Shift_Flag == false);
                    _Shift.Expenses = totalShiftExpense;
                    _Shift.TotalSale = totalShiftMaster;
                    _Shift.Shift_Increase_disability = (totalShiftExpense + _Shift.Shift_End_Amount) - (_Shift.Shift_Start_Amount + totalShiftMaster);
                    localContext.SaveChanges();
                    localContext.Dispose();





                });

            }
        }

        private void barButtonItem23_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.MaximizeBox = false;
        }

        private void barButtonItem38_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            frmWarehouse frm = new frmWarehouse();
            frm.ShowDialog();
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
            TransferWarhouse frm = new TransferWarhouse();
            frm.ShowDialog();

        }

        private void barButtonItem40_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmStorge frm = new frmStorge();
            frm.ShowDialog();
        }

        private void barButtonItem42_ItemClick(object sender, ItemClickEventArgs e)
        {
            Category_Back_office frm = new Category_Back_office();
            frm.ShowDialog();

        }

        void updateCategory(Int64 branchCode, BackOfficeEntity.Back_Office_Transaction_Detail detail)
        {

            _server = new BackOfficeEntity.db_a8f74e_posEntities();
            List<BackOfficeEntity.Category_Back_Office> _Category_Back_Office = _server.Category_Back_Office.Where(ff => ff.Branch_Code == branchCode && ff.Back_Office_Master_Code == detail.Back_Office_Master_Code && ff.Back_Office_Detail_Code == detail.Detail_Code && detail.IsDeleted == 0).ToList();
            _Category_Back_Office.ForEach(xc =>
            {

                // check In Local

                localContext = new POSEntity();
                var isFind = localContext.Categories.Any(cat => cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0 && cat.CategoryCode == xc.CategoryCode);

                if (isFind)
                {

                    if (xc.Event_Code == 3)
                    {

                        // Local
                        localContext = new POSEntity();
                        bool CheckRelation = localContext.ItemCardViews.Where(ItemCard => ItemCard.IsDeleted == 0).Any(ItemCard => ItemCard.UnitCode == xc.CategoryCode);



                        if (CheckRelation)
                        {


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Id == detail.Id);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "This Category cannot be deleted because there are products on it" : "لا يمكن حذف هذه المجموعة لوجود منتجات عليها";
                            Detail.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Category_Back_Office Category_Back = _server.Category_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == xc.Back_Office_Detail_Code);

                            Category_Back.IsDeleted = 0;
                            Category_Back.Is_Brach_Take_Update = true;

                            Category_Back.Event_Code = 0;


                            _server.SaveChanges();
                            _server.Dispose();

                        }

                        else
                        {
                            //local

                            Category _Category;
                            localContext = new POSEntity();
                            _Category = localContext.Categories.FirstOrDefault(cat => cat.CategoryCode == xc.CategoryCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);
                            _Category.IsDeleted = 1;
                            localContext.SaveChanges();
                            localContext.Dispose();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Category_Back_Office Category_Back = _server.Category_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == xc.Back_Office_Detail_Code);
                            Category_Back.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail detail1 = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            detail1.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                            detail1.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                        }





                    }

                    else if (xc.Event_Code == 2 || xc.Event_Code == 1)
                    {


                        Category _Category;
                        localContext = new POSEntity();
                        _Category = localContext.Categories.FirstOrDefault(cat => cat.CategoryCode == xc.CategoryCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);
                        _Category.CategoryName = xc.CategoryName;
                        localContext.SaveChanges();
                        localContext.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Category_Back_Office Category_Back = _server.Category_Back_Office.FirstOrDefault(x2 => x2.Back_Office_Detail_Code == detail.Detail_Code);
                        Category_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail Detail4 = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Id == detail.Id);
                        Detail4.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        Detail4.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();



                    }



                }

                else
                {


                    try
                    {
                        Category _Category = new Category()
                        {
                            Branch_Code = branchCode,
                            CategoryCode = xc.CategoryCode,
                            CategoryName = xc.CategoryName,

                            IsDeleted = 0,

                        };
                        localContext = new POSEntity();
                        localContext.Categories.Add(_Category);
                        localContext.SaveChanges();
                        localContext.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Category_Back_Office Category_Back = _server.Category_Back_Office.FirstOrDefault(x2 => x2.Back_Office_Detail_Code == detail.Detail_Code);
                        Category_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail detail10 = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Id == detail.Id);
                        detail10.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        detail10.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {

                            String dd = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name.ToString() + eve.Entry.State.ToString();

                            foreach (var ve in eve.ValidationErrors)
                            {

                                String ddddd = "Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName.ToString() + ve.ErrorMessage.ToString();




                            }
                        }
                        throw;
                    }


                }




            });




        }

        void updateUnit(Int64 branchCode, BackOfficeEntity.Back_Office_Transaction_Detail detail)
        {
            _server = new BackOfficeEntity.db_a8f74e_posEntities();
            List<BackOfficeEntity.UnitCard_Back_Office> _UnitCard_Back_Office = _server.UnitCard_Back_Office.Where(ff => ff.Branch_Code == branchCode && ff.Back_Office_Detail_Code == detail.Detail_Code && ff.Back_Office_Master_Code == detail.Back_Office_Master_Code && detail.IsDeleted == 0).ToList();


            _UnitCard_Back_Office.ForEach(xc =>
            {
                localContext = new POSEntity();
                var isFind = localContext.UnitCards.Any(cat => cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0 && cat.UnitCode == xc.UnitCode);

                if (isFind)
                {

                    if (xc.Event_Code == 3)
                    {
                        localContext = new POSEntity();
                        bool CheckRelation = localContext.ItemCardViews.Where(ItemCard => ItemCard.IsDeleted == 0).Any(ItemCard => ItemCard.UnitCode == xc.UnitCode);



                        if (CheckRelation)
                        {


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "This Unit cannot be deleted because there are products on it" : "لا يمكن حذف هذه الوحدة لوجود منتجات عليها";
                            Detail.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.UnitCard_Back_Office Category_Back = _server.UnitCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);

                            Category_Back.IsDeleted = 0;
                            Category_Back.Is_Brach_Take_Update = true;
                            Category_Back.Event_Code = 0;


                            _server.SaveChanges();
                            _server.Dispose();


                        }

                        else
                        {
                            UnitCard _Category;
                            localContext = new POSEntity();
                            _Category = localContext.UnitCards.FirstOrDefault(cat => cat.UnitCode == xc.UnitCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);
                            _Category.IsDeleted = 1;
                            localContext.SaveChanges();
                            localContext.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.UnitCard_Back_Office Category_Back = _server.UnitCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                            Category_Back.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                            Detail.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                        }





                    }

                    else if (xc.Event_Code == 2 || xc.Event_Code == 1)
                    {


                        UnitCard _Category;
                        localContext = new POSEntity();
                        _Category = localContext.UnitCards.FirstOrDefault(cat => cat.UnitCode == xc.UnitCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);

                        _Category.UnitName = xc.UnitName;
                        localContext.SaveChanges();
                        localContext.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.UnitCard_Back_Office Category_Back = _server.UnitCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                        Category_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Id == detail.Id);
                        Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        Detail.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                    }



                }

                else
                {


                    try
                    {
                        UnitCard _UnitCard = new UnitCard()
                        {
                            Branch_Code = branchCode,
                            UnitCode = xc.UnitCode,
                            UnitName = xc.UnitName,

                            IsDeleted = 0,

                        };
                        localContext = new POSEntity();
                        localContext.UnitCards.Add(_UnitCard);
                        localContext.SaveChanges();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.UnitCard_Back_Office Category_Back = _server.UnitCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == xc.Back_Office_Detail_Code);
                        Category_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                        Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        Detail.Is_Brach_Take_Update = true;

                        _server.SaveChanges();
                        _server.Dispose();


                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {

                            String dd = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name.ToString() + eve.Entry.State.ToString();

                            foreach (var ve in eve.ValidationErrors)
                            {

                                String ddddd = "Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName.ToString() + ve.ErrorMessage.ToString();




                            }
                        }
                        throw;
                    }


                }




            });


        }




        void updateItemCard(Int64 branchCode, BackOfficeEntity.Back_Office_Transaction_Detail detail)
        {

            _server = new BackOfficeEntity.db_a8f74e_posEntities();
            List<BackOfficeEntity.ItemCard_Back_Office> _ItemCard_Back_Office = _server.ItemCard_Back_Office.Where(ff => ff.Branch_Code == branchCode &&
                ff.Is_Brach_Take_Update == false && ff.Back_Office_Detail_Code == detail.Detail_Code && ff.Back_Office_Master_Code == detail.Back_Office_Master_Code && detail.IsDeleted == 0).ToList();
            _ItemCard_Back_Office.ForEach(xc =>
            {

                localContext = new POSEntity();
                var isFind = localContext.ItemCards.Any(cat => cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0 && cat.ItemCode == xc.ItemCode);

                if (isFind)
                {

                    if (xc.Event_Code == 3)
                    {
                        localContext = new POSEntity();
                        bool CheckRelation3 = localContext.SaleDetails.Any(ItemCard => ItemCard.IsDeleted == 0 && ItemCard.ItemCode == xc.ItemCode);



                        if (CheckRelation3)
                        {


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail detail2 = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            detail2.Fail_Take_Update_Reason = st.isEnglish() ? "This item cannot be deleted because there are processes associated with it" : "لا يمكن حذف هذا العنصر لوجود عمليات مرتبطه به";
                            detail2.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.ItemCard_Back_Office itemCard_Back2 = _server.ItemCard_Back_Office.FirstOrDefault(ff => ff.Id == xc.Id);

                            itemCard_Back2.IsDeleted = 0;
                            itemCard_Back2.Is_Brach_Take_Update = true;
                            itemCard_Back2.Event_Code = 0;

                            _server.SaveChanges();
                            _server.Dispose();







                        }

                        else
                        {
                            ItemCard _ItemCard;
                            localContext = new POSEntity();
                            _ItemCard = localContext.ItemCards.FirstOrDefault(cat => cat.ItemCode == xc.ItemCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);
                            _ItemCard.IsDeleted = 1;
                            localContext.SaveChanges();
                            localContext.Dispose();



                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail petail1 = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            petail1.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                            petail1.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.ItemCard_Back_Office _itemCard_Back_Office = _server.ItemCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                            _itemCard_Back_Office.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();




                        }






                    }

                    else if (xc.Event_Code == 2 || xc.Event_Code == 1)
                    {


                        ItemCard _ItemCard;
                        localContext = new POSEntity();
                        _ItemCard = localContext.ItemCards.FirstOrDefault(cat => cat.ItemCode == xc.ItemCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);

                        _ItemCard.ItemCode = xc.ItemCode;
                        _ItemCard.Price = xc.Price;
                        _ItemCard.AddItem = xc.AddItem;
                        _ItemCard.Name = xc.Name;
                        _ItemCard.ParCode = xc.ParCode;
                        _ItemCard.IsDeleted = xc.IsDeleted;
                        _ItemCard.Item_Risk_limit = xc.Item_Risk_limit;
                        _ItemCard.UnitCode = xc.UnitCode;
                        _ItemCard.Branch_Code = branchCode;
                        _ItemCard.Name_En = xc.Name_En;
                        _ItemCard.CategoryCode = xc.CategoryCode;
                        localContext.SaveChanges();
                        localContext.Dispose();



                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail df = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == xc.Back_Office_Detail_Code);

                        df.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        df.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.ItemCard_Back_Office back = _server.ItemCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                        back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();





                    }



                }

                else
                {

                    localContext = new POSEntity();
                    try
                    {
                        ItemCard _ItemCards = new ItemCard()
                        {
                            Branch_Code = branchCode,


                            ItemCode = xc.ItemCode,
                            Price = xc.Price,
                            AddItem = xc.AddItem,
                            Name = xc.Name,
                            ParCode = xc.ParCode,
                            IsDeleted = xc.IsDeleted,
                            Item_Risk_limit = xc.Item_Risk_limit,
                            UnitCode = xc.UnitCode,

                            Name_En = xc.Name_En,
                            CategoryCode = xc.CategoryCode,

                        };
                        localContext.ItemCards.Add(_ItemCards);
                        localContext.SaveChanges();
                        localContext.Dispose();




                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail dtl = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                        dtl.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        dtl.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.ItemCard_Back_Office vategory_Back = _server.ItemCard_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == xc.Back_Office_Detail_Code);
                        vategory_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();



                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {

                            String dd = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name.ToString() + eve.Entry.State.ToString();

                            foreach (var ve in eve.ValidationErrors)
                            {

                                String ddddd = "Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName.ToString() + ve.ErrorMessage.ToString();




                            }
                        }
                        throw;
                    }



                }



            });


        }


        void updateExpens(Int64 branchCode, BackOfficeEntity.Back_Office_Transaction_Detail detail)
        {

            using (POSEntity Contexts5 = new POSEntity())
            {
                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                List<BackOfficeEntity.Expenses_Back_Office> _Expenses_Back_Office = _server.Expenses_Back_Office.Where(ff => ff.Branch_Code == branchCode && ff.Is_Brach_Take_Update == false &&
                ff.Back_Office_Master_Code == detail.Back_Office_Master_Code && ff.Back_Office_Detail_Code == detail.Detail_Code && detail.IsDeleted == 0).ToList();
                _Expenses_Back_Office.ForEach(xc =>
                {
                    localContext = new POSEntity();
                    var isFind = localContext.Expenses.Any(cat => cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0 && cat.ExpensesCode == xc.ExpensesCode);

                    if (isFind)
                    {

                        if (xc.Event_Code == 3)
                        {

                            localContext = new POSEntity();
                            bool CheckRelation = localContext.ExpensesTransactions.Where(ItemCard => ItemCard.IsDeleted == 0).Any(ItemCard => ItemCard.ExpensesCode == xc.ExpensesCode);



                            if (CheckRelation)
                            {


                                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Id == xc.Id_Back_Office);
                                Detail.Fail_Take_Update_Reason = st.isEnglish() ? "This Expenses cannot be deleted because there are products on it" : "لا يمكن حذف هذا المصروف لوجود منتجات عليها";
                                Detail.Is_Brach_Take_Update = true;
                                _server.SaveChanges();
                                _server.Dispose();

                                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                BackOfficeEntity.Expenses_Back_Office Category_Back = _server.Expenses_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);

                                Category_Back.IsDeleted = 0;
                                Category_Back.Is_Brach_Take_Update = true;
                                Category_Back.Event_Code = 0;


                                _server.SaveChanges();
                                _server.Dispose();

                            }

                            else
                            {
                                localContext = new POSEntity();
                                Expens _Expens;
                                _Expens = localContext.Expenses.FirstOrDefault(cat => cat.ExpensesCode == xc.ExpensesCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);
                                _Expens.IsDeleted = 1;
                                localContext.SaveChanges();
                                localContext.Dispose();

                                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == xc.Back_Office_Detail_Code);
                                Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                                Detail.Is_Brach_Take_Update = true;
                                _server.SaveChanges();
                                _server.Dispose();


                                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                BackOfficeEntity.Expenses_Back_Office Category_Back = _server.Expenses_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                                Category_Back.Is_Brach_Take_Update = true;
                                _server.SaveChanges();
                                _server.Dispose();



                            }





                        }

                        else if (xc.Event_Code == 2 || xc.Event_Code == 1)
                        {

                            localContext = new POSEntity();
                            Expens _Expens;
                            _Expens = localContext.Expenses.FirstOrDefault(cat => cat.ExpensesCode == xc.ExpensesCode && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);

                            _Expens.ExpensesName = xc.ExpensesName;
                            localContext.SaveChanges();
                            localContext.Dispose();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                            Detail.Is_Brach_Take_Update = true;
                            _server.SaveChanges();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Expenses_Back_Office Category_Back = _server.Expenses_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                            Category_Back.Is_Brach_Take_Update = true;
                            _server.SaveChanges();


                        }



                    }

                    else
                    {


                        try
                        {
                            Expens _Expens = new Expens()
                            {
                                Branch_Code = branchCode,
                                ExpensesCode = xc.ExpensesCode,
                                ExpensesName = xc.ExpensesName,

                                IsDeleted = 0,

                            };
                            localContext = new POSEntity();
                            localContext.Expenses.Add(_Expens);
                            localContext.SaveChanges();
                            localContext.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                            Detail.Is_Brach_Take_Update = true;

                            _server.SaveChanges();
                            _server.Dispose();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Expenses_Back_Office Category_Back = _server.Expenses_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                            Category_Back.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();


                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {

                                String dd = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name.ToString() + eve.Entry.State.ToString();

                                foreach (var ve in eve.ValidationErrors)
                                {

                                    String ddddd = "Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName.ToString() + ve.ErrorMessage.ToString();




                                }
                            }
                            throw;
                        }


                    }




                });
            }

        }


        void updateUser(Int64 branchCode, BackOfficeEntity.Back_Office_Transaction_Detail detail)
        {
            _server = new BackOfficeEntity.db_a8f74e_posEntities();
            List<BackOfficeEntity.User_Back_Office> _User_Back_Office = _server.User_Back_Office.Where(ff => ff.Branch_Code == branchCode && ff.Is_Brach_Take_Update == false && ff.Back_Office_Detail_Code == detail.Detail_Code && ff.Back_Office_Master_Code == detail.Back_Office_Master_Code && detail.IsDeleted == 0).ToList();
            _User_Back_Office.ForEach(xc =>
            {
                localContext = new POSEntity();
                var isFind = localContext.Users.Any(cat => cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0 && cat.Emp_Code == xc.Emp_Code);

                if (isFind)
                {

                    if (xc.Event_Code == 3)
                    {

                        localContext = new POSEntity();
                        bool CheckRelation1 = localContext.ExpensesTransactions.Any(ItemCard => ItemCard.IsDeleted == 0 && ItemCard.Emp_Code == xc.Emp_Code);

                        bool CheckRelation3 = localContext.SaleMasters.Any(ItemCard => ItemCard.IsDeleted == 0 && ItemCard.UserCode == xc.Emp_Code);







                        if (CheckRelation1 || CheckRelation3)
                        {


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "This user cannot be deleted because there are processes associated with it" : "لا يمكن حذف هذا المستخدم لوجود عمليات مرتبطه به";
                            Detail.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Expenses_Back_Office Category_Back = _server.Expenses_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);

                            Category_Back.IsDeleted = 0;
                            Category_Back.Is_Brach_Take_Update = true;
                            Category_Back.Event_Code = 0;


                            _server.SaveChanges();
                            _server.Dispose();

                        }

                        else
                        {
                            localContext = new POSEntity();
                            User _User;
                            _User = localContext.Users.FirstOrDefault(cat => cat.Emp_Code == xc.Emp_Code && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);
                            _User.IsDeleted = 1;
                            localContext.SaveChanges();
                            localContext.Dispose();

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                            Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                            Detail.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();


                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.User_Back_Office user_Back_Office = _server.User_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                            user_Back_Office.Is_Brach_Take_Update = true;
                            _server.SaveChanges();
                            _server.Dispose();


                            // Back Office
                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            BackOfficeEntity.Employee _BackOfficeEmployee = _server.Employees.FirstOrDefault(x2 => x2.Employee_Code == xc.Emp_Code && x2.IsDeleted == 0);


                            // Local Branch
                            localContext = new POSEntity();
                            bool isEmployFind = localContext.Employees.Any(Employ => Employ.IsDeleted == 0 && Employ.Employee_Code == xc.Emp_Code && Employ.Branch_ID == branchCode);


                            // is True (Update)
                            if (isEmployFind)
                            {
                                localContext = new POSEntity();
                                Employee _LocaleMployee = localContext.Employees.FirstOrDefault(x2 => x2.Employee_Code == xc.Emp_Code && x2.IsDeleted == 0);
                                _LocaleMployee.Employee_Code = _BackOfficeEmployee.Employee_Code;
                                _LocaleMployee.Employee_Name = _BackOfficeEmployee.Employee_Name;
                                _LocaleMployee.IsDeleted = _BackOfficeEmployee.IsDeleted;
                                _LocaleMployee.Employee_National_Id = _BackOfficeEmployee.Employee_National_Id;
                                _LocaleMployee.Employee_Address = _BackOfficeEmployee.Employee_Address;
                                _LocaleMployee.Branch_ID = branchCode;
                                _LocaleMployee.Created_Date = _BackOfficeEmployee.Created_Date;
                                _LocaleMployee.Employee_Email = _BackOfficeEmployee.Employee_Email;
                                _LocaleMployee.Employee_End_Jop = _BackOfficeEmployee.Employee_End_Jop;
                                _LocaleMployee.Employee_Mobile_1 = _BackOfficeEmployee.Employee_Mobile_1;
                                _LocaleMployee.Employee_Mobile_2 = _BackOfficeEmployee.Employee_Mobile_2;
                                _LocaleMployee.Employee_Notes = _BackOfficeEmployee.Employee_Notes;
                                _LocaleMployee.Employee_Start_Jop = _BackOfficeEmployee.Employee_Start_Jop;
                                _LocaleMployee.SexTypeCode = _BackOfficeEmployee.SexTypeCode;
                                _LocaleMployee.img_Url = _BackOfficeEmployee.img_Url;
                                _LocaleMployee.isUploaded = _BackOfficeEmployee.isUploaded;
                                _LocaleMployee.Jop_Code = _BackOfficeEmployee.Jop_Code;
                                _LocaleMployee.Last_Modified_User = _BackOfficeEmployee.Last_Modified_User;
                                _LocaleMployee.Last_Modified_Date = _BackOfficeEmployee.Last_Modified_Date;
                                localContext.SaveChanges();
                                localContext.Dispose();


                            }

                            // is False (New)
                            else
                            {




                                Employee _Employee = new Employee()
                                {
                                    Branch_ID = branchCode,
                                    Created_Date = _BackOfficeEmployee.Created_Date,

                                    Employee_Address = _BackOfficeEmployee.Employee_Address,
                                    Employee_Code = _BackOfficeEmployee.Employee_Code,
                                    Employee_Email = _BackOfficeEmployee.Employee_Email,

                                    Employee_End_Jop = _BackOfficeEmployee.Employee_End_Jop,
                                    Employee_Mobile_1 = _BackOfficeEmployee.Employee_Mobile_1,
                                    Employee_Mobile_2 = _BackOfficeEmployee.Employee_Mobile_2,
                                    Employee_Name = _BackOfficeEmployee.Employee_Name,
                                    Employee_National_Id = _BackOfficeEmployee.Employee_National_Id,
                                    Employee_Notes = _BackOfficeEmployee.Employee_Notes,
                                    Employee_Start_Jop = _BackOfficeEmployee.Employee_Start_Jop,
                                    img_Url = _BackOfficeEmployee.img_Url,
                                    IsDeleted = _BackOfficeEmployee.IsDeleted,
                                    isUploaded = _BackOfficeEmployee.isUploaded,
                                    Jop_Code = _BackOfficeEmployee.Jop_Code,
                                    Last_Modified_Date = _BackOfficeEmployee.Last_Modified_Date,
                                    Last_Modified_User = _BackOfficeEmployee.Last_Modified_User,
                                    SexTypeCode = _BackOfficeEmployee.SexTypeCode


                                };


                                localContext = new POSEntity();

                                localContext.Employees.Add(_Employee);
                                localContext.SaveChanges();
                                localContext.Dispose();


                            }








                        }






                    }

                    else if (xc.Event_Code == 2 || xc.Event_Code == 1)
                    {


                        User _User;
                        localContext = new POSEntity();
                        _User = localContext.Users.FirstOrDefault(cat => cat.Emp_Code == xc.Emp_Code && cat.Branch_Code == branchCode && cat.Branch_Code != 0 && cat.IsDeleted == 0);

                        _User.Password = xc.Password;
                        _User.UserName = xc.UserName;
                        _User.Branch_Code = xc.Branch_Code;
                        _User.Created_Date = xc.Created_Date;
                        _User.Emp_Code = xc.Emp_Code;
                        _User.IsDeleted = xc.IsDeleted;

                        _User.Last_Modified_Date = xc.Last_Modified_Date;
                        _User.Last_Modified_User = xc.Last_Modified_User;
                        _User.UserFlag = xc.UserFlag;



                        localContext.SaveChanges();
                        localContext.Dispose();


                        _server = new BackOfficeEntity.db_a8f74e_posEntities();

                        BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                        Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        Detail.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.User_Back_Office Category_Back = _server.User_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                        Category_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();


                        // Back Office
                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Employee _BackOfficeEmployee = _server.Employees.FirstOrDefault(x2 => x2.Employee_Code == xc.Emp_Code && x2.IsDeleted == 0);


                        // Local Branch
                        localContext = new POSEntity();
                        bool isEmployFind = localContext.Employees.Any(Employ => Employ.IsDeleted == 0 && Employ.Employee_Code == xc.Emp_Code);




                        // is True (Update)
                        if (isEmployFind)
                        {
                            localContext = new POSEntity();
                            Employee _LocaleMployee = localContext.Employees.FirstOrDefault(x2 => x2.Employee_Code == xc.Emp_Code && x2.IsDeleted == 0 && x2.Branch_ID == branchCode);
                            _LocaleMployee.Employee_Code = _BackOfficeEmployee.Employee_Code;
                            _LocaleMployee.Employee_Name = _BackOfficeEmployee.Employee_Name;
                            _LocaleMployee.IsDeleted = _BackOfficeEmployee.IsDeleted;
                            _LocaleMployee.Employee_National_Id = _BackOfficeEmployee.Employee_National_Id;
                            _LocaleMployee.Employee_Address = _BackOfficeEmployee.Employee_Address;
                            _LocaleMployee.Branch_ID = branchCode;
                            _LocaleMployee.Created_Date = _BackOfficeEmployee.Created_Date;
                            _LocaleMployee.Employee_Email = _BackOfficeEmployee.Employee_Email;
                            _LocaleMployee.Employee_End_Jop = _BackOfficeEmployee.Employee_End_Jop;
                            _LocaleMployee.Employee_Mobile_1 = _BackOfficeEmployee.Employee_Mobile_1;
                            _LocaleMployee.Employee_Mobile_2 = _BackOfficeEmployee.Employee_Mobile_2;
                            _LocaleMployee.Employee_Notes = _BackOfficeEmployee.Employee_Notes;
                            _LocaleMployee.Employee_Start_Jop = _BackOfficeEmployee.Employee_Start_Jop;
                            _LocaleMployee.SexTypeCode = _BackOfficeEmployee.SexTypeCode;
                            _LocaleMployee.img_Url = _BackOfficeEmployee.img_Url;
                            _LocaleMployee.isUploaded = _BackOfficeEmployee.isUploaded;
                            _LocaleMployee.Jop_Code = _BackOfficeEmployee.Jop_Code;
                            _LocaleMployee.Last_Modified_User = _BackOfficeEmployee.Last_Modified_User;
                            _LocaleMployee.Last_Modified_Date = _BackOfficeEmployee.Last_Modified_Date;
                            localContext.SaveChanges();
                            localContext.Dispose();


                        }

                        // is False (New)
                        else
                        {




                            Employee _Employee = new Employee()
                            {
                                Branch_ID = branchCode,
                                Created_Date = _BackOfficeEmployee.Created_Date,

                                Employee_Address = _BackOfficeEmployee.Employee_Address,
                                Employee_Code = _BackOfficeEmployee.Employee_Code,
                                Employee_Email = _BackOfficeEmployee.Employee_Email,

                                Employee_End_Jop = _BackOfficeEmployee.Employee_End_Jop,
                                Employee_Mobile_1 = _BackOfficeEmployee.Employee_Mobile_1,
                                Employee_Mobile_2 = _BackOfficeEmployee.Employee_Mobile_2,
                                Employee_Name = _BackOfficeEmployee.Employee_Name,
                                Employee_National_Id = _BackOfficeEmployee.Employee_National_Id,
                                Employee_Notes = _BackOfficeEmployee.Employee_Notes,
                                Employee_Start_Jop = _BackOfficeEmployee.Employee_Start_Jop,
                                img_Url = _BackOfficeEmployee.img_Url,
                                IsDeleted = _BackOfficeEmployee.IsDeleted,
                                isUploaded = _BackOfficeEmployee.isUploaded,
                                Jop_Code = _BackOfficeEmployee.Jop_Code,
                                Last_Modified_Date = _BackOfficeEmployee.Last_Modified_Date,
                                Last_Modified_User = _BackOfficeEmployee.Last_Modified_User,

                                SexTypeCode = _BackOfficeEmployee.SexTypeCode


                            };



                            localContext = new POSEntity();
                            localContext.Employees.Add(_Employee);
                            localContext.SaveChanges();
                            localContext.Dispose();


                        }







                    }






                }

                else
                {


                    try
                    {
                        User _User = new User()
                        {
                            Branch_Code = branchCode,
                            Created_Date = xc.Created_Date,
                            Emp_Code = xc.Emp_Code,
                            Last_Modified_Date = xc.Last_Modified_Date,
                            Last_Modified_User = xc.Last_Modified_User,
                            Password = xc.Password,
                            UserName = xc.UserName,
                            UserFlag = xc.UserFlag,
                            IsDeleted = xc.IsDeleted

                        };
                        localContext = new POSEntity();
                        localContext.Users.Add(_User);
                        localContext.SaveChanges();
                        localContext.Dispose();

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Back_Office_Transaction_Detail Detail = _server.Back_Office_Transaction_Detail.FirstOrDefault(x2 => x2.Detail_Code == detail.Detail_Code);
                        Detail.Fail_Take_Update_Reason = st.isEnglish() ? "Success" : "تم التحديث بنجاح";
                        Detail.Is_Brach_Take_Update = true;


                        _server.SaveChanges();
                        _server.Dispose();


                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.User_Back_Office Category_Back = _server.User_Back_Office.FirstOrDefault(ff => ff.Back_Office_Detail_Code == detail.Detail_Code);
                        Category_Back.Is_Brach_Take_Update = true;
                        _server.SaveChanges();
                        _server.Dispose();



                        // Back Office
                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                        BackOfficeEntity.Employee _BackOfficeEmployee = _server.Employees.FirstOrDefault(x2 => x2.Employee_Code == xc.Emp_Code && x2.IsDeleted == 0);


                        // Local Branch
                        localContext = new POSEntity();
                        bool isEmployFind = localContext.Employees.Any(Employ => Employ.IsDeleted == 0 && Employ.Employee_Code == xc.Emp_Code && Employ.Branch_ID == branchCode);




                        // is True (Update)
                        if (isEmployFind)
                        {
                            localContext = new POSEntity();
                            Employee _LocaleMployee = localContext.Employees.FirstOrDefault(x2 => x2.Employee_Code == xc.Emp_Code && x2.IsDeleted == 0 && x2.Branch_ID == branchCode);
                            _LocaleMployee.Employee_Code = _BackOfficeEmployee.Employee_Code;
                            _LocaleMployee.Employee_Name = _BackOfficeEmployee.Employee_Name;
                            _LocaleMployee.IsDeleted = _BackOfficeEmployee.IsDeleted;
                            _LocaleMployee.Employee_National_Id = _BackOfficeEmployee.Employee_National_Id;
                            _LocaleMployee.Employee_Address = _BackOfficeEmployee.Employee_Address;
                            _LocaleMployee.Branch_ID = branchCode;
                            _LocaleMployee.Created_Date = _BackOfficeEmployee.Created_Date;
                            _LocaleMployee.Employee_Email = _BackOfficeEmployee.Employee_Email;
                            _LocaleMployee.Employee_End_Jop = _BackOfficeEmployee.Employee_End_Jop;
                            _LocaleMployee.Employee_Mobile_1 = _BackOfficeEmployee.Employee_Mobile_1;
                            _LocaleMployee.Employee_Mobile_2 = _BackOfficeEmployee.Employee_Mobile_2;
                            _LocaleMployee.Employee_Notes = _BackOfficeEmployee.Employee_Notes;
                            _LocaleMployee.Employee_Start_Jop = _BackOfficeEmployee.Employee_Start_Jop;
                            _LocaleMployee.SexTypeCode = _BackOfficeEmployee.SexTypeCode;
                            _LocaleMployee.img_Url = _BackOfficeEmployee.img_Url;
                            _LocaleMployee.isUploaded = _BackOfficeEmployee.isUploaded;
                            _LocaleMployee.Jop_Code = _BackOfficeEmployee.Jop_Code;
                            _LocaleMployee.Last_Modified_User = _BackOfficeEmployee.Last_Modified_User;
                            _LocaleMployee.Last_Modified_Date = _BackOfficeEmployee.Last_Modified_Date;
                            localContext.SaveChanges();
                            localContext.Dispose();


                        }

                        // is False (New)
                        else
                        {




                            Employee _Employee = new Employee()
                            {
                                Branch_ID = branchCode,
                                Created_Date = _BackOfficeEmployee.Created_Date,

                                Employee_Address = _BackOfficeEmployee.Employee_Address,
                                Employee_Code = _BackOfficeEmployee.Employee_Code,
                                Employee_Email = _BackOfficeEmployee.Employee_Email,

                                Employee_End_Jop = _BackOfficeEmployee.Employee_End_Jop,
                                Employee_Mobile_1 = _BackOfficeEmployee.Employee_Mobile_1,
                                Employee_Mobile_2 = _BackOfficeEmployee.Employee_Mobile_2,
                                Employee_Name = _BackOfficeEmployee.Employee_Name,
                                Employee_National_Id = _BackOfficeEmployee.Employee_National_Id,
                                Employee_Notes = _BackOfficeEmployee.Employee_Notes,
                                Employee_Start_Jop = _BackOfficeEmployee.Employee_Start_Jop,
                                img_Url = _BackOfficeEmployee.img_Url,
                                IsDeleted = _BackOfficeEmployee.IsDeleted,
                                isUploaded = _BackOfficeEmployee.isUploaded,
                                Jop_Code = _BackOfficeEmployee.Jop_Code,
                                Last_Modified_Date = _BackOfficeEmployee.Last_Modified_Date,
                                Last_Modified_User = _BackOfficeEmployee.Last_Modified_User,

                                SexTypeCode = _BackOfficeEmployee.SexTypeCode


                            };



                            localContext = new POSEntity();
                            localContext.Employees.Add(_Employee);
                            localContext.SaveChanges();
                            localContext.Dispose();


                        }










                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {

                            String dd = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name.ToString() + eve.Entry.State.ToString();

                            foreach (var ve in eve.ValidationErrors)
                            {

                                String ddddd = "Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName.ToString() + ve.ErrorMessage.ToString();




                            }
                        }
                        throw;
                    }



                }



                UpdateUserAutherth(xc.Emp_Code, branchCode);


            });

        }

        void UpdateUserAutherth(Int64 emp_Code, Int64 branch_Code)
        {

            ObjectParameter Message = new ObjectParameter("Message", typeof(string));

            List<User_Auth> _New_List_User_Auth = new List<User_Auth>();


            // BackOffce 
            localContext = new POSEntity();
            _server = new BackOfficeEntity.db_a8f74e_posEntities();
            List<BackOfficeEntity.User_Auth> list_User_Auth = _server.User_Auth.Where(ff => ff.IsDeleted == 0 && ff.User_Code == emp_Code && ff.InActive).ToList();
            localContext.Delete_Trancation_By_User_Code(emp_Code, branch_Code, Message);

            foreach (var row in list_User_Auth)
            {
                localContext = new POSEntity();

                Int64? NewCode = localContext.User_Auth.Max(u => (int?)u.Auth_Code);
                if (string.IsNullOrEmpty(NewCode.ToString()))
                {
                    NewCode = 0;
                }

                User_Auth _User_Auth_Mod = new User_Auth()
                {

                    Tab_Info_Code = row.Tab_Info_Code,
                    User_Code = row.User_Code,
                    IsDeleted = 0,
                    Branch_Code = branch_Code,
                    Last_Modified_Date = DateTime.Now,
                    Created_Date = DateTime.Now,
                    is_Back_Office_Updated = row.is_Back_Office_Updated,
                    Last_Modified_User = row.Last_Modified_User,
                    InActive = true,
                    Auth_Code = Convert.ToInt64(NewCode + 1)

                };
                _New_List_User_Auth.Add(_User_Auth_Mod);


            }
            localContext = new POSEntity();

            localContext.User_Auth.AddRange(_New_List_User_Auth);
            localContext.SaveChanges();
            localContext.Dispose();
            _server.Dispose();
            Authentication();


        }




        bool checkItemQty(Int64 itemCode, Int64 Warehouse_Code, double requerdQty)
        {

            bool isVaild = false;


            using (POSEntity ContVald = new POSEntity())
            {



                //الكميه المطلوبه جديد
                double qty = Convert.ToDouble(requerdQty);
                double item_Qt = 0;
                try
                {
                    // الكمية المتاحة الان في المخزن
                    item_Qt = ContVald.Item_History.Where(w => w.Item_Id == itemCode && w.Warhouse_Code == Warehouse_Code && w.IsFinshed == false).Sum(x => x.Current_Qty_Now);
                }
                catch
                {
                    item_Qt = 0;
                }


                if (item_Qt >= qty)
                {


                    isVaild = true;






                }




            }

            return isVaild;
        }

        public void Update_Item_Qty_Oly(Int64 PoCode, Int64 from_WareHouse, Int64 Sale_Master_Code, double Qty, DateTime OrderDate, Int64 History_Id, Int64 Item_Id,
            Int64 shiftCode, Int64 Warhouse_Transfer_Code, Int64 To_Warhouse_Code)
        {


            using (POSEntity _context2 = new POSEntity())
            {


                //local
                Item_History item_History = _context2.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == from_WareHouse);
                item_History.Current_Qty_Now -= Qty;
                item_History.Is_Used = (bool)true;
                item_History.IsFinshed = (bool)false;
                _context2.SaveChanges();




                //  server
                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                BackOfficeEntity.Item_History_Back_Office item_History_Back_Office = new BackOfficeEntity.Item_History_Back_Office()
                {
                    Is_Branch_Take_Update = false,
                    Main_Item_History_Code = item_History.Id,
                    Price_Buy = item_History.Price_Buy,
                    Qty = Qty,
                    CreatedDate = item_History.CreatedDate,
                    From_Branch_Id = from_WareHouse,
                    From_Warhouse_Code = from_WareHouse,
                    PO_Code = PoCode,
                    IsDeleted = 0,
                    Item_Id = item_History.Item_Id,
                    Main_Master_Code = item_History.Sale_Master_Code,
                    To_Branch_Code = To_Warhouse_Code,
                    To_Warhouse_Code = To_Warhouse_Code,
                    Warhouse_Transfer_Code = Warhouse_Transfer_Code,



                };
                _server.Item_History_Back_Office.Add(item_History_Back_Office);
                _server.SaveChanges();
                _server.Dispose();








                Item_History_transaction _Item_History_transaction = new Item_History_transaction()
                {
                    OrderDate = OrderDate,
                    Item_History_Id = History_Id,
                    CreatedDate = DateTime.Now,
                    Trans_In = 0,
                    Trans_Out = Qty,
                    is_Back_Office_Updated = false,
                    IsDeleted = 0,
                    To_Warhouse_Code = To_Warhouse_Code,
                    Warhouse_Transfer_Code = Warhouse_Transfer_Code,
                    shiftCode = shiftCode,
                    Branch_Id = from_WareHouse,
                    from_Warhouse_Code = from_WareHouse,
                    Item_Id = Item_Id,
                    SaleMasterCode = Sale_Master_Code

                };
                _context2.Item_History_transaction.Add(_Item_History_transaction);
                _context2.SaveChanges();
                _context2.Dispose();


            }


        }

        public void Update_Item_Qty_And_Finshed(Int64 PoCode, Int64 from_WareHouse, Int64 Sale_Master_Code, double Qty, DateTime OrderDate, Int64 History_Id, Int64 Item_Id, Int64 shiftCode,
            Int64 Warhouse_Transfer_Code, Int64 To_Warhouse_Code)
        {

            using (POSEntity _context2 = new POSEntity())
            {

                //local
                Item_History item_History = _context2.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == from_WareHouse);
                item_History.Current_Qty_Now -= Qty;
                item_History.Is_Used = (bool)true;
                item_History.IsFinshed = (bool)true;
                _context2.SaveChanges();




                //  server
                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                BackOfficeEntity.Item_History_Back_Office item_History_Back_Office = new BackOfficeEntity.Item_History_Back_Office()
                {
                    Is_Branch_Take_Update = false,
                    Main_Item_History_Code = item_History.Id,
                    Price_Buy = item_History.Price_Buy,
                    Qty = Qty,
                    CreatedDate = item_History.CreatedDate,
                    From_Branch_Id = from_WareHouse,
                    From_Warhouse_Code = from_WareHouse,
                    PO_Code = PoCode,
                    IsDeleted = 0,
                    Item_Id = item_History.Item_Id,
                    Main_Master_Code = item_History.Sale_Master_Code,
                    To_Branch_Code = To_Warhouse_Code,
                    To_Warhouse_Code = To_Warhouse_Code,
                    Warhouse_Transfer_Code = Warhouse_Transfer_Code,



                };
                _server.Item_History_Back_Office.Add(item_History_Back_Office);
                _server.SaveChanges();









                Item_History_transaction _Item_History_transaction = new Item_History_transaction()
                {
                    OrderDate = OrderDate,
                    Item_History_Id = History_Id,
                    CreatedDate = DateTime.Now,
                    Trans_In = 0,
                    Trans_Out = Qty,
                    IsDeleted = 0,
                    is_Back_Office_Updated = false,
                    To_Warhouse_Code = To_Warhouse_Code,
                    Warhouse_Transfer_Code = Warhouse_Transfer_Code,
                    shiftCode = shiftCode,
                    Branch_Id = from_WareHouse,
                    from_Warhouse_Code = from_WareHouse,
                    Item_Id = Item_Id,
                    SaleMasterCode = Sale_Master_Code

                };
                _context2.Item_History_transaction.Add(_Item_History_transaction);
                _context2.SaveChanges();
                _context2.Dispose();


            }




        }

        List<Item_History> Item_Qty_List = new List<Item_History>();



        void update_Item_card_Balunce(Int64 item_Code, Int64 branchCode, Int64 Ware_House_Code, ItemCard _itemCard)
        {


            _server = new BackOfficeEntity.db_a8f74e_posEntities();
            bool isBalanc = _server.Item_Balance.Any(xx => xx.Item_Code == item_Code && xx.Branch_Code == branchCode && xx.IsDeleted == 0 && xx.WareHouse_Code == Ware_House_Code);

            if (isBalanc)
            {
                var item = _server.Item_Balance.FirstOrDefault(xx => xx.Item_Code == item_Code && xx.Branch_Code == branchCode && xx.IsDeleted == 0 && xx.WareHouse_Code == Ware_House_Code);
                item.Last_Update = DateTime.Now;
                try
                {
                    localContext = new POSEntity();
                    item.Balance = localContext.Item_History.Where(x => !x.IsFinshed && x.Warhouse_Code == Ware_House_Code && x.Item_Id == item_Code).ToList().Sum(xx => xx.Current_Qty_Now);


                }
                catch
                {
                    item.Balance = 0;
                }
                try
                {
                    localContext = new POSEntity();

                    item.Price = _itemCard.Price;
                }
                catch
                {
                    item.Price = 0;
                }
                _server.SaveChanges();
            }

            else
            {
                localContext = new POSEntity();

                double balance = 0;
                try
                {

                    balance = localContext.Item_History.Where(x => !x.IsFinshed && x.Warhouse_Code == Ware_House_Code && x.Item_Id == item_Code).ToList().Sum(xx => xx.Current_Qty_Now);

                }
                catch
                {
                    balance = 0;
                }
                BackOfficeEntity.Item_Balance itmBaln = new BackOfficeEntity.Item_Balance()
                {
                    Price = _itemCard.Price,
                    IsDeleted = 0,
                    Balance = balance,
                    Last_Update = DateTime.Now,
                    Item_Code = _itemCard.ItemCode,
                    Branch_Code = branchCode,
                    WareHouse_Code = Ware_House_Code



                };
                _server.Item_Balance.Add(itmBaln);
                _server.SaveChanges();




            }


        }
        void Update_From_Back_Office(bool isFromTimer)
        {
            Int64 Ware_House_Code = st.Get_Warehouse_Code();
            //Int64 Ware_House_Code =0;
            Int64 branchCode = st.GetBranch_Code();
            // Int64 branchCode = 0;
            Int64 UserCode = st.GetUser_Code();
            Int64 ShiftCode = 0;

            try
            {
                localContext = new POSEntity();
                ShiftCode = localContext.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            }
            catch
            {
                ShiftCode = 0;
            }

            localContext.Dispose();
            if (!isFromTimer)
            {
                SplashScreenManager.ShowForm(typeof(WaitForm1));
            }

            if (CheckForInternetConnection())
            {
                try
                {
                    //updateToFireBase();
                    // Update Base Data
                    using (BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities())
                    {
                        List<BackOfficeEntity.Back_Office_Transaction_Master> MasterUpdate = _server.Back_Office_Transaction_Master.Where(x => x.To_Branch_Code == branchCode &&
                           x.IsDeleted == 0 && !x.Brach_Take_Update).ToList();
                        MasterUpdate.ForEach(Master =>
                        {

                            List<BackOfficeEntity.Back_Office_Transaction_Detail> DetailUpdate = _server.Back_Office_Transaction_Detail.Where(x2 => x2.Back_Office_Master_Code == Master.Master_Code &&
                            x2.Brach_Code == branchCode && !x2.Is_Brach_Take_Update).ToList();


                            DetailUpdate.ForEach(detail =>
                            {

                                // category
                                if (detail.Table_Code == 29)
                                {
                                    updateCategory(branchCode, detail);
                                }

                                // unit
                                else if (detail.Table_Code == 21)
                                {
                                    updateUnit(branchCode, detail);
                                }

                                // Expens
                                else if (detail.Table_Code == 4)
                                {
                                    updateExpens(branchCode, detail);
                                }

                                // User && Employee
                                else if (detail.Table_Code == 22)
                                {
                                    updateUser(branchCode, detail);
                                }

                                else if (detail.Table_Code == 8)
                                {
                                    updateItemCard(branchCode, detail);
                                }



                            });


                            BackOfficeEntity.Back_Office_Transaction_Master Detail = _server.Back_Office_Transaction_Master.FirstOrDefault(x2 => x2.Id == Master.Id);
                            Detail.Brach_Take_Update = true;
                            _server.SaveChanges();




                        });
                    }




                    // Get WareHouse_Tras
                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                    var list_WareHouse_Tras = _server.Warehouse_Transaction.Where(x => (x.To_Warehouse_Code == Ware_House_Code ||
                    x.From_Warehouse_Code == Ware_House_Code) && x.is_Back_Office_Updated == false).ToList();








                    list_WareHouse_Tras.ForEach(trn =>
                    {



                        // Get PO By WareHouse_Tras Id
                        var list_PO = _server.POes.Where(x => x.Warehouse_Transaction_Code == trn.Id && x.IsDeleted == 0).ToList();

                        list_PO.ForEach(po_Model =>
                        {

                            // if Event 2  && To_WareHouseCode = CurntWarhous  ==== >  Aprroved
                            // 1 - get item history Back Office  from server by WareHouse_Tras Id.
                            // 2 - insert into locale Item History.
                            // 3 - update item Back Office history from server set (Branch Take update == true)
                            // 4 - update po requst state to 3
                            // 5 - insert po in local



                            // if Event 2  && To_WareHouseCode = CurntWarhous  ==== >  Aprroved
                            if (po_Model.PO_Request_State == 2 && po_Model.To_WareHouseCode == Ware_House_Code)
                            {

                                // 1 - get item history Back Office  from server by WareHouse_Tras Id.
                                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                List<BackOfficeEntity.Item_History_Back_Office> List_item_History_Back_Offices =
                                    _server.Item_History_Back_Office.Where(xx => xx.Is_Branch_Take_Update == false && xx.PO_Code == po_Model.id &&
                                    xx.Warhouse_Transfer_Code == po_Model.Warehouse_Transaction_Code && xx.To_Warhouse_Code == Ware_House_Code).ToList();


                                // 2 - insert into locale Item History.
                                List_item_History_Back_Offices.ForEach(xv =>
                                {

                                    Item_History item_History = new Item_History()
                                    {
                                        Branch_Id = branchCode,
                                        CreatedDate = xv.CreatedDate,
                                        Current_Qty_Now = xv.Qty,
                                        Qty = xv.Qty,
                                        IsFinshed = false,
                                        is_Back_Office_Updated = false,
                                        Is_Used = false,
                                        Item_Id = xv.Item_Id,
                                        Price_Buy = xv.Price_Buy,
                                        Price_Sale = 0,
                                        Sale_Master_Code = xv.Main_Master_Code,
                                        Warhouse_Code = xv.To_Warhouse_Code,
                                        Warhouse_Transfer_Code = xv.Warhouse_Transfer_Code

                                    };
                                    localContext = new POSEntity();
                                    localContext.Item_History.Add(item_History);
                                    localContext.SaveChanges();

                                    // 3 - update item Back Office history from server set (Branch Take update == true)
                                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                    var item_Back_Offce_histoy = _server.Item_History_Back_Office.FirstOrDefault(bb => bb.Id_Back_Office == xv.Id_Back_Office && bb.PO_Code == po_Model.id);
                                    item_Back_Offce_histoy.Is_Branch_Take_Update = true;

                                    _server.SaveChanges();


                                    // 4 - update po requst state to 3
                                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                    var po_state = _server.POes.FirstOrDefault(bb => bb.id == po_Model.id);
                                    po_state.PO_Request_State = 3;

                                    _server.SaveChanges();


                                    // 5 - insert po in local
                                    localContext = new POSEntity();
                                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                    var po2 = _server.POes.FirstOrDefault(bb => bb.id == po_Model.id);

                                    DataRep.PO locpo = new DataRep.PO()
                                    {
                                        From_WareHouse_Code = po2.From_WareHouse_Code,
                                        PO_Request_State = po2.PO_Request_State,

                                        Created_Date = po2.Created_Date,
                                        Creted_By_User = po2.Creted_By_User,
                                        IsDeleted = po2.IsDeleted,
                                        Item_Code = po2.Item_Code,
                                        PO_Item_Qty = po2.PO_Item_Qty,
                                        Reason = po2.Reason,
                                        To_WareHouseCode = po2.To_WareHouseCode,
                                        Warehouse_Transaction_Code = po2.Warehouse_Transaction_Code

                                    };
                                    localContext.POes.Add(locpo);
                                    localContext.SaveChanges();


                                });






                            }




                            // if Event 1 =======> Waiting Aprove
                            // 1 - get item Back Office history from server by WareHouse_Tras Id.
                            // 2 -  Check if qty is vaild

                            //if (true)=========>
                            // 1 - mins qty from local
                            // 2 - update po requst state to 2

                            // if (false) =======>
                            //1- update item Back Office history from server set (Branch Take update == true)
                            // 2 - update po requst state to 3 (reject)
                            // 5 - insert po in local




                            else if (po_Model.PO_Request_State == 1 && po_Model.From_WareHouse_Code == Ware_House_Code)
                            {

                                // 1 - get item history Back Office  from server by WareHouse_Tras Id.
                                _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                List<BackOfficeEntity.Item_History_Back_Office> List_item_History_Back_Offices =
                                    _server.Item_History_Back_Office.Where(xx => xx.Is_Branch_Take_Update == false &&
                                    xx.Warhouse_Transfer_Code == po_Model.Warehouse_Transaction_Code && xx.To_Warhouse_Code == Ware_House_Code).ToList();


                                // 2 -  Check if qty is vaild
                                bool vaild = checkItemQty(po_Model.Item_Code, Ware_House_Code, po_Model.PO_Item_Qty);

                                //if (true)=========>
                                if (vaild)
                                {

                                    // 1 - mins qty from local
                                    localContext = new POSEntity();
                                    Item_Qty_List = localContext.Item_History.Where(w => w.Item_Id == po_Model.Item_Code && w.IsFinshed == false && w.Warhouse_Code == Ware_House_Code).ToList();


                                    Item_Qty_List.ForEach(x =>
                                    {

                                        double qty = po_Model.PO_Item_Qty;
                                        if (qty > 0)
                                        {
                                                // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
                                                if (qty >= x.Current_Qty_Now)
                                            {

                                                Update_Item_Qty_And_Finshed(po_Model.id, Ware_House_Code, 0, x.Current_Qty_Now, x.CreatedDate, x.Id, x.Item_Id, ShiftCode, trn.Id, po_Model.To_WareHouseCode);
                                                qty = qty - x.Current_Qty_Now;

                                                if (qty < 0)
                                                {
                                                    qty = 0;
                                                }
                                            }

                                            else if (qty < x.Current_Qty_Now)
                                            {


                                                Update_Item_Qty_Oly(po_Model.id, Ware_House_Code, 0, qty, x.CreatedDate, x.Id, x.Item_Id, ShiftCode, trn.Id, po_Model.To_WareHouseCode);
                                                qty = 0;

                                            }



                                        }


                                    });







                                    // 2 - update po requst state to 2
                                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                    var po_state = _server.POes.FirstOrDefault(bb => bb.id == po_Model.id);
                                    po_state.PO_Request_State = 2;
                                    _server.SaveChanges();

                                }
                                // if (false) =======>
                                else
                                {

                                    //1- update item Back Office history from server set (Branch Take update == true)
                                    List_item_History_Back_Offices.ForEach(xv =>
                                    {
                                        _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                        var item_Back_Offce_histoy = _server.Item_History_Back_Office.FirstOrDefault(bb => bb.Id_Back_Office == xv.Id_Back_Office);
                                        item_Back_Offce_histoy.Is_Branch_Take_Update = true;
                                        _server.SaveChanges();
                                    });


                                    // 2 - update po requst state to 4 (reject)
                                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                    var spo = _server.POes.FirstOrDefault(xx => xx.id == po_Model.id);
                                    spo.PO_Request_State = 4;
                                    spo.Reason = "There is not enough quantity";
                                    _server.SaveChanges();



                                    // 5 - insert po in local
                                    localContext = new POSEntity();
                                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                                    var po2 = _server.POes.FirstOrDefault(bb => bb.id == po_Model.id);
                                    DataRep.PO locpo = new DataRep.PO()
                                    {
                                        From_WareHouse_Code = po2.From_WareHouse_Code,
                                        PO_Request_State = po2.PO_Request_State,

                                        Created_Date = po2.Created_Date,
                                        Creted_By_User = po2.Creted_By_User,
                                        IsDeleted = po2.IsDeleted,
                                        Item_Code = po2.Item_Code,
                                        PO_Item_Qty = po2.PO_Item_Qty,
                                        Reason = po2.Reason,
                                        To_WareHouseCode = po2.To_WareHouseCode,
                                        Warehouse_Transaction_Code = po2.Warehouse_Transaction_Code

                                    };
                                    localContext.POes.Add(locpo);
                                    localContext.SaveChanges();

                                }



                            }








                        });



                        localContext = new POSEntity();
                        bool chechk = localContext.Warehouse_Transaction.Any(xx => xx.Code == trn.Code);

                        if (!chechk)
                        {
                            Warehouse_Transaction warehouse_Transaction = new Warehouse_Transaction()
                            {
                                Code = trn.Code,
                                Branch_Code = trn.Branch_Code,
                                Created_By = trn.Created_By,
                                Created_Date = trn.Created_Date,
                                From_Warehouse_Code = trn.From_Warehouse_Code,
                                IsDeleted = trn.IsDeleted,
                                is_Back_Office_Updated = true,
                                To_Warehouse_Code = trn.To_Warehouse_Code
                            };
                            localContext = new POSEntity();
                            localContext.Warehouse_Transaction.Add(warehouse_Transaction);
                            localContext.SaveChanges();

                        }

                        _server = new BackOfficeEntity.db_a8f74e_posEntities();

                        bool anybo = _server.POes.Any(xxx => xxx.From_WareHouse_Code == trn.Code && (xxx.PO_Request_State > 2));

                        if (anybo)
                        {
                            BackOfficeEntity.Warehouse_Transaction warehouse_Transaction1 = _server.Warehouse_Transaction.FirstOrDefault(S => S.Code == trn.Code);
                            warehouse_Transaction1.is_Back_Office_Updated = true;
                            _server.SaveChanges();

                        }


                    });










                    // Update WareHouse_Tras
                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                    var list_WareHouse_Tras2 = _server.Warehouse_Transaction.Where(x => (x.To_Warehouse_Code == Ware_House_Code ||
                    x.From_Warehouse_Code == Ware_House_Code) && x.is_Back_Office_Updated == false).ToList();

                    list_WareHouse_Tras2.ForEach(vv =>
                    {

                        bool anybo = _server.POes.Any(xxx => xxx.From_WareHouse_Code == vv.Code && (xxx.PO_Request_State > 2));

                        if (anybo)
                        {
                            BackOfficeEntity.Warehouse_Transaction warehouse_Transaction1 = _server.Warehouse_Transaction.FirstOrDefault(S => S.Code == vv.Code);
                            warehouse_Transaction1.is_Back_Office_Updated = true;
                            _server.SaveChanges();

                        }


                        else
                        {

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            var list_WareHouse_Tras3 = _server.Warehouse_Transaction.FirstOrDefault(dd => dd.Id == vv.Id);
                            list_WareHouse_Tras3.is_Back_Office_Updated = false;
                            _server.SaveChanges();
                        }

                    });




                    _server = new BackOfficeEntity.db_a8f74e_posEntities();
                    var list_WareHouse_Tras4 = _server.Warehouse_Transaction.Where(x => (x.To_Warehouse_Code == Ware_House_Code || x.From_Warehouse_Code == Ware_House_Code) && x.is_Back_Office_Updated == false).ToList();


                    list_WareHouse_Tras4.ForEach(g =>
                    {


                        bool anybo = _server.POes.Any(xxx => xxx.PO_Request_State <= 2);
                        if (anybo)
                        {

                            _server = new BackOfficeEntity.db_a8f74e_posEntities();
                            var list_WareHouse_Tras5 = _server.Warehouse_Transaction.FirstOrDefault(xxv => xxv.Id == g.Id);
                            list_WareHouse_Tras5.is_Back_Office_Updated = false;
                            _server.SaveChanges();

                        }





                    });




                    localContext = new POSEntity();
                    List<ItemCard> listItemCard = localContext.ItemCards.Where(x => x.IsDeleted == 0 && x.Branch_Code == branchCode).ToList();
                    listItemCard.ForEach(xx =>
                    {

                        update_Item_card_Balunce(xx.ItemCode, branchCode, Ware_House_Code, xx);




                    });



                    if (!isFromTimer)
                    {
                        SplashScreenManager.CloseForm();
                        MaterialMessageBox.Show(st.isEnglish() ? "The data has been updated successfully" : "تم تحديث البيانات بنجاح", MessageBoxButtons.OK);
                    }

                }
                catch
                {

                    if (!isFromTimer)
                    {
                        SplashScreenManager.CloseForm();
                        MaterialMessageBox.Show(st.isEnglish() ? "There is no internet connection" : "لا يوجد اتصال بالانترنت", MessageBoxButtons.OK);
                    }



                }




            }
            else
            {


                if (!isFromTimer)
                {
                    SplashScreenManager.CloseForm();
                    MaterialMessageBox.Show(st.isEnglish() ? "There is no internet connection" : "لا يوجد اتصال بالانترنت", MessageBoxButtons.OK);
                }



            }

        }

        private void barButtonItem43_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Update_From_Back_Office(false);
            }
            catch
            {

            }



        }

        private void barButtonItem44_ItemClick(object sender, ItemClickEventArgs e)
        {

            Unit_Back_office frm = new Unit_Back_office();
            frm.ShowDialog();

        }

        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {

            Expenses_Back_office frm = new Expenses_Back_office();
            frm.ShowDialog();
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {

            User_Back_office frm = new User_Back_office();
            frm.ShowDialog();
        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
            Item_Back_office frm = new Item_Back_office();
            frm.ShowDialog();

        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmBackOfficeDashBord frm = new frmBackOfficeDashBord();
            frm.ShowDialog();
        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
            frm_PO_DashBord frm = new frm_PO_DashBord();
            frm.ShowDialog();

        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {
            frmProductsRate frm = new frmProductsRate();
            frm.ShowDialog();
        }
    }
}
