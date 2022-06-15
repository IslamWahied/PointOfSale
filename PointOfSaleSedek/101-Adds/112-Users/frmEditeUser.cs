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
using EntityData;


namespace PointOfSaleSedek._101_Adds._112_Users
{
    public partial class frmEditeUser : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities2 context = new PointOfSaleEntities2();
        readonly Static st = new Static();
        public frmEditeUser()
        {
            InitializeComponent();
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
            
           
            if (string.IsNullOrWhiteSpace(txtUserName.Text) || txtUserName.Text.Length<=1)
            {
                MaterialMessageBox.Show("يجب ان تكون كلمة الدخول اكثر من حرف", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text.Length <= 1)
            {
                MaterialMessageBox.Show("يجب ان يكون الباسورد اكثر من حرف", MessageBoxButtons.OK);
                return;
            }

            if (Application.OpenForms.OfType<frmUser>().Any())
            {
                frmUser frm = (frmUser)Application.OpenForms["frmUser"];
                Int64 EmpCode = Convert.ToInt64(txtEmpCode.Text);
                using (PointOfSaleEntities2 ForCheck = new PointOfSaleEntities2())
                {
                    bool TestUserName = ForCheck.Users.Any(Emp => Emp.Emp_Code != EmpCode && Emp.IsDeleted == 0 && Emp.UserName == txtUserName.Text);
                    if (TestUserName)
                    {
                        MaterialMessageBox.Show("تم تسجيل هذا الاسم لموظف اخر", MessageBoxButtons.OK);
                        return;
                    }
                   
                    if (TestUserName)
                    {

                        MaterialMessageBox.Show("تم تسجيل رقم البطاقة لموظف اخر", MessageBoxButtons.OK);
                        return;

                    }


                }

                User _User;
                _User = context.Users.SingleOrDefault(Employee => Employee.Emp_Code == EmpCode&&Employee.IsDeleted==0);
                _User.Last_Modified_Date = DateTime.Now;
                _User.Last_Modified_User = st.User_Code();
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text;
                _User.UserFlag =  (bool)chkAddUser.Checked;
                context.SaveChanges();
                

                frmUser frmUserMain = new frmUser();
                frmUserMain.gcEmployeeCard.DataSource = context.User_View.Where(x => x.IsDeleted == 0&&x.IsDeletedEmployee==0).OrderBy(x=>x.Employee_Code).ToList();
                frmUserMain.gcEmployeeCard.RefreshDataSource();
                using (PointOfSaleEntities2 NewReco = new PointOfSaleEntities2())
                {

                    User_View result2 = NewReco.User_View.Where(x => x.Employee_Code == EmpCode && x.IsDeleted == 0&&x.IsDeletedEmployee==0).FirstOrDefault();


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
    }
}