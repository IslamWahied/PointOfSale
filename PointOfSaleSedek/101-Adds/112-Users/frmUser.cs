using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using PointOfSaleSedek._101_Adds._112_Users.Auth;
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
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
       PointOfSaleEntities2  context = new PointOfSaleEntities2();
        Static st = new Static();
        public frmUser()
        {
            InitializeComponent();
            FillGride();
            
                
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد")
            {
                frmAddUser frm = new frmAddUser();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل")
            {

                if (gvEmployeeCard.RowCount <= 0)
                {
                    return;
                }

                frmEditeUser frm = new frmEditeUser();

                var User = gvEmployeeCard.GetFocusedRow() as User_View;


                frm.txtEmpCode.Text = User.Employee_Code.ToString();
                frm.txtEmpName.Text = User.Employee_Name.ToString();
                frm.txtUserName.EditValue = User.UserName;
                frm.txtBranch.EditValue = User.Branches_Name;
                frm.txtPassword.EditValue = User.Password;

                frm.chkAddUser.Checked = (bool)User.UserFlag;

                frm.ShowDialog();

            }
            else if (btn.Caption == "خروج")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف")
            {

                if (gvEmployeeCard.RowCount <= 0)
                {


                    return;
                }
                if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
                {


                    User_View xx = gvEmployeeCard.GetFocusedRow() as User_View;
                    User _User = new User();
                    _User = context.Users.SingleOrDefault(item => item.Emp_Code == xx.Employee_Code && item.IsDeleted == xx.IsDeleted);
                    _User.IsDeleted = 1;
                    context.SaveChanges();
                    FillGride();


                }

            }

            

            else if (btn.Caption == "طباعة")
            {

                //if (gvEmployeeCard.RowCount <= 0)
                //{


                //    return;
                //}

                //using (PointOfSaleEntities2 Contexts = new PointOfSaleEntities2())
                //{

                //    var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 select a).ToList();
                //    gcEmployeeCard.DataSource = result;
                //    gcEmployeeCard.RefreshDataSource();

                //    if (gvEmployeeCard.RowCount <= 0)
                //    {

                //        gcEmployeeCard.Enabled = false;

                //    }
                //    else
                //    {

                //        gcEmployeeCard.Enabled = true;
                //    }
                //}


            }
        }

        private void اضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAddUser frm = new frmAddUser();
            frm.ShowDialog();
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvEmployeeCard.RowCount <= 0)
            {
                return;
            }

            frmEditeUser frm = new frmEditeUser();

            var User = gvEmployeeCard.GetFocusedRow() as User_View;
            
             
             frm.txtEmpCode.Text = User.Employee_Code.ToString();
            frm.txtEmpName.Text = User.Employee_Name.ToString();
            frm.txtUserName.EditValue = User.UserName;
            frm.txtBranch.EditValue = User.Branches_Name;
            frm.txtPassword.EditValue = User.Password;
            
            frm.chkAddUser.Checked = (bool)User.UserFlag;

            frm.ShowDialog();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvEmployeeCard.RowCount <= 0)
            {


                return;
            }
            if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
            {

                
                    User_View xx = gvEmployeeCard.GetFocusedRow() as User_View;
                User _User = new User();
                _User  = context.Users.SingleOrDefault(item => item.Emp_Code == xx.Employee_Code && item.IsDeleted == xx.IsDeleted);
                _User.IsDeleted = 1;
                 
                    context.SaveChanges();
                    FillGride();
                 

            }
        }
        void Rest()
        { 
        
        
        
        
        }
        void FillGride()
        {
            gcEmployeeCard.DataSource = null;
            using (PointOfSaleEntities2 Contexts = new PointOfSaleEntities2())
            {

                var empData = (from a in Contexts.User_View where  a.IsDeleted==0 &&a.IsDeletedEmployee==0 select a).OrderBy(x => x.Employee_Code).ToList();


                if (empData.Count > 0)
                {

                    gcEmployeeCard.DataSource = empData;
                    gcEmployeeCard.RefreshDataSource();

                }
                else
                {

                    gcEmployeeCard.Enabled = false;
                    windowsUIButtonPanel.Buttons.ForEach(x =>
                    {

                        if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                        {

                            x.Properties.Enabled = false;

                        }
                        if (x.Properties.Caption == "جديد")
                        {

                            x.Properties.Enabled = true;

                        }

                    });




                }
            }


        }

        private void repositoryItemCheckEdit1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (gvEmployeeCard.RowCount <= 0)
                {
                    return;
                }

                User_View User = gvEmployeeCard.GetFocusedRow() as User_View;

                User _User;
                _User = context.Users.SingleOrDefault(Employee => Employee.Emp_Code == User.Employee_Code && Employee.IsDeleted == 0);
                _User.Last_Modified_Date = DateTime.Now;
                _User.Last_Modified_User = st.User_Code();
                _User.UserFlag = (bool)repositoryItemCheckEdit1.ValueChecked;
                context.SaveChanges();
                //gvEmployeeCard.SetFocusedRowCellValue("UserFlag", (bool)repositoryItemCheckEdit1.ValueChecked);
            }
            catch
            { 
            
            
            }
           







        }

        private void الصلاحياتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmAuthcs frm = new frmAuthcs();
            
            //frm.ShowDialog();
        }
    }
}