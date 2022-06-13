using DevExpress.XtraEditors;
using PointOfSaleSedek._102_MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;
using EntityData;

namespace PointOfSaleSedek._101_Adds._112_Users
{
    public partial class frmAddUser : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities context = new PointOfSaleEntities();
        readonly Static st = new Static();
        public frmAddUser()
        {
            InitializeComponent();
            FillEmpSlk();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        void FillEmpSlk()
        {
            var result = context.Employee_View.Where(Branch => Branch.IsDeleted == 0).ToList();
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

                MaterialMessageBox.Show("برجاء اختيار الموظف", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || txtUserName.Text.Length <= 1)
            {
                MaterialMessageBox.Show("يجب ان تكون كلمة الدخول اكثر من حرف", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length <= 1)
            {
                MaterialMessageBox.Show("يجب ان يكون الباسورد اكثر من حرف", MessageBoxButtons.OK);
                return;
            }

            Int64 EmpCode = Convert.ToInt64(slkEmp.EditValue);
            if (Application.OpenForms.OfType<frmUser>().Any())
            {
                bool TestUpdate = context.Users.Any(User => User.Emp_Code == EmpCode && User.IsDeleted == 0);
                frmUser frm = (frmUser)Application.OpenForms["frmUser"];
                if (TestUpdate)
                {

                    using (PointOfSaleEntities ForCheck = new PointOfSaleEntities())
                    {

                        bool TestUserName = ForCheck.User_View.Any(User => User.UserName == txtUserName.Text && User.Employee_Code != EmpCode);
                        if (TestUserName)
                        {
                            MaterialMessageBox.Show("تم تسجيل هذا الاسم لمستخدم اخر", MessageBoxButtons.OK);
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
                    _User.Last_Modified_User = st.User_Code();
                    context.SaveChanges();
                    frm.gcEmployeeCard.DataSource = context.User_View.Where(x => x.IsDeleted == 0&&x.IsDeletedEmployee==0).OrderBy(x=>x.Employee_Code).ToList();
                    frm.gcEmployeeCard.RefreshDataSource();
                    MaterialMessageBox.Show("تم التعديل بنجاح", MessageBoxButtons.OK);

                }
                else
                {
                    using (PointOfSaleEntities ForCheck = new PointOfSaleEntities())
                    {

                        bool TestUserName = ForCheck.User_View.Any(User => User.UserName == txtUserName.Text && User.IsDeleted== 0 && User.IsDeletedEmployee == 0 );
                        if (TestUserName)
                        {
                            MaterialMessageBox.Show("تم تسجيل هذا الاسم لمستخدم اخر", MessageBoxButtons.OK);
                            return;

                        }


                    }



                    User _User = new User()
                    {
                        Emp_Code = EmpCode,
                        UserName = txtUserName.Text,
                        Password = txtPassword.Text,
                        UserFlag = (bool)chkAddUser.Checked,
                        IsDeleted = 0,
                        Last_Modified_User=st.User_Code(),
                        Created_Date = DateTime.Now
                    };
                    context.Users.Add(_User);
                    context.SaveChanges();
                    using (PointOfSaleEntities NewContext = new PointOfSaleEntities())
                    {
                        frm.gcEmployeeCard.DataSource = NewContext.User_View.Where(x => x.IsDeleted==0&&x.IsDeletedEmployee==0).ToList();
                        frm.gcEmployeeCard.RefreshDataSource();
                    }
                    MaterialMessageBox.Show("تم الحفظ بنجاح", MessageBoxButtons.OK);
                }
            }
        }
    }
}