
using BackOfficeEntity;
//using DataRep;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PointOfSaleSedek._101_Adds._111_Warehouse
{
    public partial class User_Back_office : DevExpress.XtraEditors.XtraForm
    {

      
        readonly db_a8f74e_posEntities _server = new db_a8f74e_posEntities();
     //  readonly POSEntity context = new POSEntity();

        Static st = new Static();
        public User_Back_office()
        {
            InitializeComponent();
              langu();
            FillFromGride();
           FillslkToWarhouse();

        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;



            gvCategory.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            gvCategory.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";

            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "Save" : "حفظ";


            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";


            materialContextMenuStrip2.Items[0].Text = st.isEnglish() ? "Delete" : "حذف";
            label50.Text = st.isEnglish() ? "Back Office" : "من";
            label2.Text = st.isEnglish() ? "To" : "الي";

            gridColumn28.Caption = st.isEnglish() ? "Name" : "الاسم";

            gridColumn1.Caption = st.isEnglish() ? "User Name" : "اسم المستخدم";
            gridColumn2.Caption = st.isEnglish() ? "User Name" : "اسم المستخدم";

            materialLabel1.Text = st.isEnglish() ? "User Name" : "اسم المستخدم";


        }


        public void FillslkToWarhouse()
        {
            DataRep.POSEntity context = new DataRep.POSEntity();
            DataTable dt = new DataTable();
            var result = context.Branches.Where(Categories => Categories.IsDeleted == 0 && Categories.Branches_Code != 0 && Categories.Is_Back_Office == false).ToList();
            slkToWarhouse.Properties.DataSource = result;
            slkToWarhouse.Properties.ValueMember = "Branches_Code";
            slkToWarhouse.Properties.DisplayMember = "Branches_Name";

        }

      


        

       

      

        List<User_Back_Office> _User_Back_Office = new List<User_Back_Office>();

       

        
        public void FillFromGride()
        {

            Int64 brachCode = st.GetBranch_Code();


            using (DataRep.POSEntity _context23 = new DataRep.POSEntity())
            {
                List<DataRep.User> GetAllCategory = new List<DataRep.User>();


                //if (brachCode == 0) {
                //      GetAllCategory = _context23.Users.Where(x => x.IsDeleted == 0 && x.Branch_Code != 0).ToList();

                //}
                //else {
                      GetAllCategory = _context23.Users.Where(x => x.IsDeleted == 0 &&  x.Emp_Code != 0 &&x.Branch_Code == brachCode).ToList();
                //}

              
              
                gcCategory.DataSource = GetAllCategory;
                gcCategory.RefreshDataSource();
            }
              

        }



   


 
        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
           
            WindowsUIButton btn = e.Button as WindowsUIButton;
             if (btn.Caption == "حفظ" || btn.Caption == "Save")
            {

                SplashScreenManager.ShowForm(typeof(WaitForm1));
                try
                {

                    // Vaidate
                    if (gvToCategory.RowCount == 0)
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "There are no Data" : "لا يوجد ", MessageBoxButtons.OK);
                        SplashScreenManager.CloseForm();
                        return;
                    }
                    else if (String.IsNullOrWhiteSpace(slkToWarhouse.Text))
                    {

                        MaterialMessageBox.Show(st.isEnglish() ? "Please select the Branch you are transferring to" : "برجاء اختيار الفرع المحول اليه", MessageBoxButtons.OK);
                        SplashScreenManager.CloseForm();
                        return;
                    }


                    // Get BranchCode
                    Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);

                    Int64 MaxMasterCode = 0;
                    // Get DetailCode
                    using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                    {
                        MaxMasterCode = Convert.ToInt64(Contexts2.Back_Office_Transaction_Master.Max(u => (Int64?)u.Master_Code + 1));
                        if (MaxMasterCode == 0)
                        {
                            MaxMasterCode = 1;
                        }

                    }


                    // Save trans Back Office Master
                    using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                    {




                        Back_Office_Transaction_Master _Back_Office_Transaction_Master = new Back_Office_Transaction_Master()
                        {
                            Created_By = st.GetUser_Code(),
                            Created_Date = DateTime.Now,
                            IsDeleted = 0,
                            Master_Code = MaxMasterCode,

                            To_Branch_Code = toBranchCode,

                        };
                        Contexts2.Back_Office_Transaction_Master.Add(_Back_Office_Transaction_Master);
                        Contexts2.SaveChanges();

                    }


                    _User_Back_Office.Where(x => x.IsWindow_Transfer).ToList().ForEach(x => {

                        Int64 MaxDetailCode = 0;
                        // Get DetailCode
                        using (db_a8f74e_posEntities Contexts5 = new db_a8f74e_posEntities())
                        {
                            MaxDetailCode = Convert.ToInt64(Contexts5.Back_Office_Transaction_Detail.Max(u => (Int64?)u.Detail_Code + 1));
                            if (MaxDetailCode == 0)
                            {
                                MaxDetailCode = 1;
                            }

                        }
                        using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                        {
                            // 1- Delete By Update Is Deleted 
                            //2- Insert New Line  If Not Event Delete



                            // 1
                            if (x.Event_Code != 1)
                            {

                                User_Back_Office _User_Back_Office = Contexts2.User_Back_Office.FirstOrDefault(cat => cat.Emp_Code == x.Emp_Code && cat.IsDeleted == 0 && cat.Branch_Code == toBranchCode);
                                _User_Back_Office.IsDeleted = 1;
                                _User_Back_Office.IsWindow_Transfer = false;
                                Contexts2.SaveChanges();


                            }
                            //2 



                            Back_Office_Transaction_Detail _Back_Office_Transaction = new Back_Office_Transaction_Detail()
                            {
                                Brach_Code = toBranchCode,
                                Event_Code = x.Event_Code,
                                Fail_Take_Update_Reason = st.isEnglish() ? "Waiting" : "تحت الانتظار",
                                Is_Brach_Take_Update = false,
                                Table_Code = 22,
                                User_Id = st.GetUser_Code(),

                                Created_Date = DateTime.Now,
                                Back_Office_Master_Code = MaxMasterCode,
                                Detail_Code = MaxDetailCode,
                                IsDeleted = 0,

                            };
                            Contexts2.Back_Office_Transaction_Detail.Add(_Back_Office_Transaction);
                            Contexts2.SaveChanges();


                            User_Back_Office _cat = new User_Back_Office()
                            {
                                Branch_Code = toBranchCode,
                                Password = x.Password,
                                Emp_Code = x.Emp_Code,
                                UserName = x.UserName,
                                Created_Date = x.Created_Date,
                                Back_Office_Detail_Code = MaxDetailCode,

                                Is_Brach_Take_Update = false,
                                Last_Modified_Date = x.Last_Modified_Date,
                                Last_Modified_User = x.Last_Modified_User,
                                UserFlag = x.UserFlag,


                                Event_Code = x.Event_Code,
                                IsWindow_Transfer = false,
                                Back_Office_Master_Code = MaxMasterCode,
                                IsDeleted = x.Event_Code == 3 ? 1 : 0
                            };
                            Contexts2.User_Back_Office.Add(_cat);
                            Contexts2.SaveChanges();





                            bool checkEmployeeOnServer = Contexts2.Employee.Any(xx => xx.Employee_Code == x.Emp_Code && xx.IsDeleted == 0);
                            if (!checkEmployeeOnServer)
                            {

                                DataRep.POSEntity local2 = new DataRep.POSEntity();
                                DataRep.Employee employee = local2.Employees.FirstOrDefault(xx => xx.Employee_Code == x.Emp_Code && xx.IsDeleted == 0);
                                Employee emp = new Employee()
                                {
                                    Branch_ID = employee.Branch_ID,
                                    IsDeleted = 0,
                                    Created_Date = employee.Created_Date,
                                    Employee_Address = employee.Employee_Address,
                                    Employee_Code = employee.Employee_Code,
                                    Employee_Email = employee.Employee_Email,
                                    Employee_End_Jop = employee.Employee_End_Jop,
                                    Employee_Mobile_1 = employee.Employee_Mobile_1,
                                    Employee_Mobile_2 = employee.Employee_Mobile_2,
                                    Employee_Name = employee.Employee_Name,
                                    Employee_Notes = employee.Employee_Notes,
                                    Employee_National_Id = employee.Employee_National_Id,
                                    Last_Modified_User = employee.Last_Modified_User,
                                    img_Url = employee.img_Url,
                                    SexTypeCode = employee.SexTypeCode,
                                    isUploaded = employee.isUploaded,
                                    Jop_Code = employee.Jop_Code,

                                    Employee_Start_Jop = employee.Employee_Start_Jop,
                                    Last_Modified_Date = employee.Last_Modified_Date

                                };
                                Contexts2.Employee.Add(emp);
                                Contexts2.SaveChanges();
                                 
                            }


                            // Remove All Auth
                            ObjectParameter message = new ObjectParameter("Message", typeof(string));
                            Contexts2.Delete_Trancation_By_User_Code(x.Emp_Code, toBranchCode, message);

                            DataRep.POSEntity local = new DataRep.POSEntity();
                            List<DataRep.User_Auth> user_Auths = local.User_Auth.Where(xx => xx.User_Code == x.Emp_Code && xx.IsDeleted == 0).ToList();

                            user_Auths.ForEach(c =>
                            {

                                User_Auth mo = new User_Auth()
                                {
                                    User_Code = c.User_Code,
                                    Auth_Code = c.Auth_Code,
                                    Branch_Code = toBranchCode,
                                    Created_Date = c.Created_Date,
                                    InActive = c.InActive,
                                    IsDeleted = c.IsDeleted,

                                    Last_Modified_Date = c.Last_Modified_Date,
                                    Last_Modified_User = c.Last_Modified_User,
                                    Tab_Info_Code = c.Tab_Info_Code,
                                    is_Back_Office_Updated = c.is_Back_Office_Updated,
                                };

                                Contexts2.User_Auth.Add(mo);
                                Contexts2.SaveChanges();






                            });


                        }




                    });




                    fillToBranchGrid();

                    MaterialMessageBox.Show(st.isEnglish() ? "Converted successfully" : "تم التحويل بنجاح", MessageBoxButtons.OK);
                    SplashScreenManager.CloseForm();
                    return;

                }
                catch (Exception)
                {

                   
                }

                SplashScreenManager.CloseForm();


            }

            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {

                this.Close();


            }

        }

        private void gvCategory_RowClick(object sender, RowClickEventArgs e)
        {
            try
            {
                var FocusRow = gvCategory.GetFocusedRow() as DataRep.User;

                labelControl1.Text = FocusRow.UserName;
            }
            catch {
                labelControl1.Text = "";
            }
          
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {





            var FocusRow = gvCategory.GetFocusedRow() as DataRep.User;
            if (String.IsNullOrWhiteSpace(slkToWarhouse.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select the Branch you are transferring to" : "برجاء اختيار الفرع المحول اليه", MessageBoxButtons.OK);
                return;
            }
            if (String.IsNullOrWhiteSpace(labelControl1.Text)) {


                MaterialMessageBox.Show(st.isEnglish() ? "There are no Data To Add" : "لا يوجد بيانات للاضافة ", MessageBoxButtons.OK);
                return;
            }
                if (FocusRow != null)
             {
                Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);

                //if (FocusRow.Branch_Code != toBranchCode)
                //{


                //    MaterialMessageBox.Show(st.isEnglish() ? "It is not possible to add this user to that branch because it is associated with another branch" : "لا بمكن اضافة هذا المستخدم لذلك الفرع لارتباطه بفرع اخر", MessageBoxButtons.OK);
                //    return;
                //}

                _User_Back_Office = _User_Back_Office.Where(x => x.Emp_Code != FocusRow.Emp_Code).ToList();
               
                var isFind = _server.User_Back_Office.Any(xx => xx.Branch_Code == toBranchCode && xx.IsDeleted == 0 && xx.Emp_Code == FocusRow.Emp_Code);



                var model = new User_Back_Office() { 
                
                Branch_Code = toBranchCode,
                Password = FocusRow.Password,
                Emp_Code = FocusRow.Emp_Code,
                UserName  = FocusRow.UserName,
                Created_Date = FocusRow.Created_Date,
                
                 Is_Brach_Take_Update = false,
                Event_Code = isFind ? 2:1,
                 Last_Modified_Date = FocusRow.Last_Modified_Date,
                 Last_Modified_User = FocusRow.Last_Modified_User,
                 UserFlag = FocusRow.UserFlag,
                 IsDeleted = FocusRow.IsDeleted,
                Back_Office_Master_Code = 0,
                IsWindow_Transfer = true
                 
                };


                _User_Back_Office.Add(model);

                gcToCategory.DataSource = _User_Back_Office.ToList();
                gcToCategory.RefreshDataSource();

             
                   labelControl1.Text = "";
             
                 
            }
            else {

               MaterialMessageBox.Show(st.isEnglish() ? "There are no Data" : "لا يوجد ", MessageBoxButtons.OK);
               return;

            }




        }



       void fillToBranchGrid()
        {

            
                if (!String.IsNullOrWhiteSpace(slkToWarhouse.Text))
                {
                    Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);
                using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                {
                    _User_Back_Office = Contexts2.User_Back_Office.Where(x => x.IsDeleted == 0 && x.Branch_Code == toBranchCode).ToList();
                }
                  
                    gcToCategory.DataSource = _User_Back_Office;
                    gcToCategory.RefreshDataSource();
                }
                else
                {
                    labelControl1.Text = "";
                _User_Back_Office = new List<User_Back_Office>();
                    gcToCategory.DataSource = _User_Back_Office;
                    gcToCategory.RefreshDataSource();
                }

        }
        private void slkToWarhouse_EditValueChanged(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            try
            {
                _User_Back_Office = new List<User_Back_Office>();

                gcToCategory.DataSource = _User_Back_Office.Where(x => x.IsDeleted == 0);
                gcToCategory.RefreshDataSource();
                fillToBranchGrid();
            }
            catch (Exception)
            {
               

            }
            SplashScreenManager.CloseForm();

        }

        private void حذفمنالفرعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FocusRow = gvToCategory.GetFocusedRow() as User_Back_Office;



            if (FocusRow != null)
            {
                Int64 toBranchCode = Convert.ToInt64(slkToWarhouse.EditValue);
                var isFind = _server.User_Back_Office.Any(xx => xx.Branch_Code == toBranchCode && xx.IsDeleted == 0 && xx.Emp_Code == FocusRow.Emp_Code);

                if (isFind)
                {
                    _User_Back_Office.FirstOrDefault(x => x.Emp_Code == FocusRow.Emp_Code).IsDeleted = 0;
                    _User_Back_Office.FirstOrDefault(x => x.Emp_Code == FocusRow.Emp_Code).IsWindow_Transfer = true;
                    _User_Back_Office.FirstOrDefault(x => x.Emp_Code == FocusRow.Emp_Code).Event_Code = 3;
                    gcToCategory.DataSource = _User_Back_Office;
                    gcToCategory.RefreshDataSource();
                }
                else
                {

                    MaterialMessageBox.Show(st.isEnglish() ? "This User already exists in this branch" : "هذا المستخدم موجود بالفعل بهذا الفرع", MessageBoxButtons.OK);
                    return;

                }

            }



        }

        private void gvToCategory_RowStyle(object sender, RowStyleEventArgs e)
        {
            int IsDeleted = Convert.ToInt16(gvToCategory.GetRowCellValue(e.RowHandle, "IsDeleted"));
            int Event_Code = Convert.ToInt16(gvToCategory.GetRowCellValue(e.RowHandle, "Event_Code"));
            bool isWndowTransfer = Convert.ToBoolean(gvToCategory.GetRowCellValue(e.RowHandle, "IsWindow_Transfer"));


            if (isWndowTransfer && Event_Code == 3)
            {
                e.Appearance.BackColor = Color.Red;
            }
            else if (isWndowTransfer && Event_Code == 1)
            {
                e.Appearance.BackColor = Color.LightGreen;
            }
            else if (isWndowTransfer && Event_Code ==2)
            {
                e.Appearance.BackColor = Color.Yellow;

            }



                e.HighPriority = true;
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {


            var FocusRow = gvToCategory.GetFocusedRow() as User_Back_Office;

            if (FocusRow != null && FocusRow.IsWindow_Transfer)
            {

                if (FocusRow.Event_Code == 2)
                {
                    _User_Back_Office.FirstOrDefault(x => x.Emp_Code == FocusRow.Emp_Code).IsDeleted = 0;
                    _User_Back_Office.FirstOrDefault(x => x.Emp_Code == FocusRow.Emp_Code).IsWindow_Transfer = false;
                    _User_Back_Office.FirstOrDefault(x => x.Emp_Code == FocusRow.Emp_Code).Event_Code = 0;
                    gcToCategory.DataSource = _User_Back_Office;
                    gcToCategory.RefreshDataSource();
                }
                else {

                    _User_Back_Office = _User_Back_Office.Where(x => x.Emp_Code != FocusRow.Emp_Code).ToList();
                    gvToCategory.DeleteSelectedRows();
                    gcToCategory.RefreshDataSource();
                }
                
            }
          
        }
    }
}