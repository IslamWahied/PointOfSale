
using PointOfSaleSedek._102_MaterialSkin;
using System;
using System.Data;

using System.Linq;

using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;
using DataRep;

namespace PointOfSaleSedek._101_Adds._112_Users
{
    public partial class frmAddUser : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmAddUser()
        {
             
            InitializeComponent();
            langu();
            FillEmpSlk();
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
            materialLabel1.Text = st.isEnglish() ? "UserName" : "كلمة الدخول";
            materialLabel13.Text = st.isEnglish() ? "Password" : "كلمة السر";
            labelControl6.Text = st.isEnglish() ? "Activate as a user of the program" : "تفعيل كمستخدم للبرنامج";

            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Cancel" : "اغلاق";

            gridColumn1.Caption = st.isEnglish() ? "Employee Name" : "اسم الموظف";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        void FillEmpSlk()
        {
            var result = context.Employee_View.Where(Emp => Emp.IsDeleted == 0 && Emp.Employee_Code != 0).ToList();
            slkEmp.Properties.DataSource = result;
            slkEmp.Properties.ValueMember = "Employee_Code";
            slkEmp.Properties.DisplayMember = "Employee_Name";
           
        }

        private void slkEmp_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(slkEmp.Text))
            {
                txtBranch.ResetText();
            }
            else
            {
                var UserCode = Int64.Parse(slkEmp.EditValue.ToString());
                txtBranch.Text = context.Employee_View.Where(x => x.IsDeleted == 0 && x.Employee_Code == UserCode).Select(x => x.Branches_Name).Single();
               
            }
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(slkEmp.Text))
            {

                MaterialMessageBox.Show(st.isEnglish()?"Please Select Employee":"برجاء اختيار الموظف", MessageBoxButtons.OK);
                return;

            }

        
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || txtUserName.Text.Length <= 1)
            {
                MaterialMessageBox.Show(st.isEnglish()?"The UserName must be more than one character":"يجب ان تكون كلمة الدخول اكثر من حرف", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length <= 1)
            {
                MaterialMessageBox.Show(st.isEnglish()?"The password must be more than one character":"يجب ان يكون الباسورد اكثر من حرف", MessageBoxButtons.OK);
                return;
            }

            Int64 EmpCode = Convert.ToInt64(slkEmp.EditValue);
            if (Application.OpenForms.OfType<frmUser>().Any())
            {
                bool TestUpdate = context.Users.Any(User => User.Emp_Code == EmpCode && User.IsDeleted == 0);
                frmUser frm = (frmUser)Application.OpenForms["frmUser"];
                if (TestUpdate)
                {

                    using (POSEntity ForCheck = new POSEntity())
                    {

                        bool TestUserName = ForCheck.User_View.Any(User => User.UserName == txtUserName.Text && User.Employee_Code != EmpCode);
                        if (TestUserName)
                        {
                            MaterialMessageBox.Show(st.isEnglish()? "This name has been registered for another user":"تم تسجيل هذا الاسم لمستخدم اخر", MessageBoxButtons.OK);
                            return;

                        }


                    }
                    User _User;
                    _User = context.Users.SingleOrDefault(User => User.Emp_Code == EmpCode &&  User.IsDeleted == 0  );
                    _User.Emp_Code = EmpCode;
                    _User.UserName = txtUserName.Text;
                    
                    _User.Password = txtPassword.Text;
                    _User.UserFlag = (bool)chkAddUser.Checked;
                    _User.Last_Modified_Date = DateTime.Now;
                    _User.Last_Modified_User = st.GetUser_Code();
                    context.SaveChanges();
                    frm.FillGride();
                    frm.gcEmployeeCard.RefreshDataSource();
                    frm.gcEmployeeCard.Enabled = true;
                  //  MaterialMessageBox.Show(st.isEnglish()?"Edited successfully":"تم التعديل بنجاح", MessageBoxButtons.OK);
                    this.Close();

                }
                else
                {
                    using (POSEntity ForCheck = new POSEntity())
                    {

                        bool TestUserName = ForCheck.User_View.Any(User => User.UserName == txtUserName.Text && User.IsDeleted== 0 && User.IsDeletedEmployee == 0 );
                        if (TestUserName)
                        {
                            MaterialMessageBox.Show(st.isEnglish() ? "This name has been registered for another user" : "تم تسجيل هذا الاسم لمستخدم اخر", MessageBoxButtons.OK);
                            return;

                        }


                    }



                    Int64 BranchCode = context.Branches.FirstOrDefault(x=>x.Branches_Name.ToLower() == txtBranch.EditValue.ToString().ToLower()).Branches_Code;

                    User _User = new User()
                    {
                        Emp_Code = EmpCode,
                        UserName = txtUserName.Text,
                        Password = txtPassword.Text,
                        Branch_Code = true? 0:st.GetBranch_Code(),
                        UserFlag = (bool)chkAddUser.Checked,
                        IsDeleted = 0,
                        Last_Modified_User=st.GetUser_Code(),
                        Created_Date = DateTime.Now
                    };
                    context.Users.Add(_User);
                    context.SaveChanges();
                       frm.FillGride();

                    frm.gcEmployeeCard.Enabled = true;
               //    MaterialMessageBox.Show(st.isEnglish() ? "Saved successfully" : "تم التعديل بنجاح", MessageBoxButtons.OK);
                    this.Close();
                }

                
            }
        }

        private void frmAddUser_Load(object sender, EventArgs e)
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.RightToLeftLayout = st.isEnglish() ? false : true;

            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            tableLayoutPanel3.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
        }

        private void labelControl6_Click(object sender, EventArgs e)
        {
    
                chkAddUser.Checked = !chkAddUser.Checked;
            
        }
    }
}