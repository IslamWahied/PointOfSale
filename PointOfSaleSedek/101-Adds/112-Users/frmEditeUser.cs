using DevExpress.XtraEditors;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataRep;


namespace PointOfSaleSedek._101_Adds._112_Users
{
    public partial class frmEditeUser : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmEditeUser()
        {
            InitializeComponent();
            langu();
        }

        public void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.RightToLeftLayout = st.isEnglish() ? false : true;

            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            tableLayoutPanel3.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;

            this.Text = st.isEnglish() ? "Add New User" : "اضافة مستخدم جديد";
            materialLabel11.Text = st.isEnglish() ? "Employee Name" : "اسم الموظف";

            materialLabel4.Text = st.isEnglish() ? "Branche" : "الفرع";
            materialLabel10.Text = st.isEnglish() ? "Code" : "كود الموظف";
            materialLabel1.Text = st.isEnglish() ? "UserName" : "كلمة الدخول";
            materialLabel13.Text = st.isEnglish() ? "Password" : "كلمة السر";
            labelControl6.Text = st.isEnglish() ? "Activate as a user of the program" : "تفعيل كمستخدم للبرنامج";

            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Cancel" : "اغلاق";

            txtEmpName.Text = st.isEnglish() ? "Employee Name" : "اسم الموظف";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditUser();
        }
        void EditUser()
        {
           
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || txtUserName.Text.Length <= 1)
            {
                MaterialMessageBox.Show(st.isEnglish() ? "The UserName must be more than one character" : "يجب ان تكون كلمة الدخول اكثر من حرف", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length <= 1)
            {
                MaterialMessageBox.Show(st.isEnglish() ? "The password must be more than one character" : "يجب ان يكون الباسورد اكثر من حرف", MessageBoxButtons.OK);
                return;
            }


          

            if (Application.OpenForms.OfType<frmUser>().Any())
            {
                frmUser frm = (frmUser)Application.OpenForms["frmUser"];
                Int64 EmpCode = Convert.ToInt64(txtEmpCode.Text);
                using (POSEntity ForCheck = new POSEntity())
                {
                    bool TestUserName = ForCheck.Users.Any(Emp => Emp.Emp_Code != EmpCode && Emp.IsDeleted == 0 && Emp.UserName == txtUserName.Text);
                    if (TestUserName)
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "This name has been registered for another user" : "تم تسجيل هذا الاسم لمستخدم اخر", MessageBoxButtons.OK);
                        return;
                    }
                   
                    


                }

                User _User;
                _User = context.Users.SingleOrDefault(Employee => Employee.Emp_Code == EmpCode&&Employee.IsDeleted==0);
                _User.Last_Modified_Date = DateTime.Now;
                _User.Last_Modified_User = st.GetUser_Code();
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text;
                _User.UserFlag =  (bool)chkAddUser.Checked;
                context.SaveChanges();
                
 
                using (POSEntity NewReco = new POSEntity())
                {
                    var branchCode = st.GetBranch_Code();
                    User_View result2 = NewReco.User_View.Where(x => x.Employee_Code == EmpCode && x.IsDeleted == 0&&x.IsDeletedEmployee==0 && x.Branches_Code == branchCode).FirstOrDefault();


                    frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Code", result2.Employee_Code);
                    frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Name", result2.Employee_Name);
                  
                    frm.gvEmployeeCard.SetFocusedRowCellValue("Branches_Name", result2.Branches_Name);
                    frm.gvEmployeeCard.SetFocusedRowCellValue("UserName", result2.UserName);
                    frm.gvEmployeeCard.SetFocusedRowCellValue("UserFlag", result2.UserFlag);


                    frm.gvEmployeeCard.RefreshData();
                   
                    HelperClass.HelperClass.ClearValues(tableLayoutPanel2);
                }


                this.Close();
            }

        }

        private void labelControl6_Click(object sender, EventArgs e)
        {
            chkAddUser.Checked = !chkAddUser.Checked;
        }
    }
}