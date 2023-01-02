using DevExpress.XtraBars.Docking2010;
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
using DataRep;

namespace PointOfSaleSedek._101_Adds._112_Users
{
    public partial class frmUser : DevExpress.XtraEditors.XtraForm
    {
       POSEntity  context = new POSEntity();
        Static st = new Static();
        public frmUser()
        {
            InitializeComponent();
            langu();
            FillGride();
            
                
        }

        void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No :RightToLeft.Yes;

            gridColumn12.Caption = st.isEnglish() ? "Employee Code":"كود الموظف";
            gridColumn7.Caption = st.isEnglish() ? "Employee Name":"اسم الموظف";
            gridColumn4.Caption = st.isEnglish() ? "Branche" : "الفرع";
            gridColumn1.Caption = st.isEnglish() ? "UserName" : "كلمة الدخول";
            gridColumn3.Caption = st.isEnglish() ? "Active" : "مفعل";
            gvEmployeeCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "New":"جديد";
            windowsUIButtonPanel.Buttons[1].Properties.Caption = st.isEnglish() ? "Edite" : "تعديل";
            windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Delete" : "حذف";
            windowsUIButtonPanel.Buttons[3].Properties.Caption = st.isEnglish() ? "Refresh" : "تحديث";
            windowsUIButtonPanel.Buttons[5].Properties.Caption = st.isEnglish() ? "Print":"طباعة";
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit":"خروج";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "New" : "جديد";
            materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Edite" : "تعديل";
            materialContextMenuStrip1.Items[2].Text = st.isEnglish() ? "Delete" : "حذف";
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد" || btn.Caption == "New")
            {
                frmAddUser frm = new frmAddUser();
              //  frm.langu();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل" || btn.Caption == "Edite")
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
            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف" || btn.Caption == "Delete")
            {

                if (gvEmployeeCard.RowCount <= 0)
                {


                    return;
                }
                if (MaterialMessageBox.Show( st.isEnglish() ? "Are you sure you want to delete This User?" : "تاكيد حذف الموظف؟", MessageBoxButtons.YesNo) == DialogResult.OK)
                {


                    User_View xx = gvEmployeeCard.GetFocusedRow() as User_View;
                    User _User = new User();
                    _User = context.Users.SingleOrDefault(item => item.Emp_Code == xx.Employee_Code && item.IsDeleted == xx.IsDeleted);
                    _User.IsDeleted = 1;
                    context.SaveChanges();
                    FillGride();


                }

            }

            

            else if (btn.Caption == "طباعة" || btn.Caption == "Print")
            {

                //if (gvEmployeeCard.RowCount <= 0)
                //{


                //    return;
                //}

                //using (POSEntity Contexts = new POSEntity())
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
            //frm.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            //frm.tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

            //frm.Text = st.isEnglish() ? "Add New User" : "اضافة مستخدم جديد";
            //frm.materialLabel11.Text = st.isEnglish() ? "Employee Name" : "اسم الموظف";

            //frm.materialLabel4.Text = st.isEnglish() ? "Branche" : "الفرع";
            //frm.materialLabel1.Text = st.isEnglish() ? "UserName" : "كلمة الدخول";
            //frm.materialLabel13.Text = st.isEnglish() ? "Password" : "كلمة السر";
            //frm.labelControl6.Text = st.isEnglish() ? "Activate as a user of the program" : "تفعيل كمستخدم للبرنامج";

            //frm.btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            //frm.btnCancel.Text = st.isEnglish() ? "Cancel" : "اغلاق";

            //gridColumn1.Caption = st.isEnglish() ? "Employee Name" : "اسم الموظف";
            ////frm.langu();
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
            if (MaterialMessageBox.Show(st.isEnglish() ? "Are you sure you want to delete This User?" : "تاكيد حذف الموظف؟", MessageBoxButtons.YesNo) == DialogResult.OK)
            {

                
                    User_View xx = gvEmployeeCard.GetFocusedRow() as User_View;
                User _User = new User();
                _User  = context.Users.SingleOrDefault(item => item.Emp_Code == xx.Employee_Code && item.IsDeleted == xx.IsDeleted);
                _User.IsDeleted = 1;
                 
                    context.SaveChanges();
                    FillGride();
                 

            }
        }
   
       public void FillGride()
        {
            gcEmployeeCard.DataSource = null;
            var branchCode = st.GetBranch_Code();
            using (POSEntity Contexts = new POSEntity())
            {

                var empData = (from a in Contexts.User_View where  a.IsDeleted==0 &&a.IsDeletedEmployee==0  && a.Employee_Code !=0 && a.Branches_Code == branchCode select a).OrderBy(x => x.Employee_Code).ToList();


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

                        if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "Edite" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "Print" || x.Properties.Caption == "حذف" || x.Properties.Caption == "Delete")
                        {

                            x.Properties.Enabled = false;

                        }
                        if (x.Properties.Caption == "جديد" || x.Properties.Caption == "New")
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
                _User.Last_Modified_User = st.GetUser_Code();
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