using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using PointOfSaleSedek._101_Adds.Employees;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.Employees;
using PointOfSaleSedek;
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
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmEmployees : DevExpress.XtraEditors.XtraForm
    {
        POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmEmployees()
        {
            InitializeComponent();
            langu();
            FillGride();
            
        }


        void langu() {
            
                this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

                gridColumn12.Caption = st.isEnglish() ? "Employee Code" : "كود الموظف";
                gridColumn7.Caption = st.isEnglish() ? "Name" : "الاسم";
                gridColumn11.Caption = st.isEnglish() ? "Sex" : "الجنس";
                gridColumn5.Caption = st.isEnglish() ? "National ID" : "الرقم القومي";
                gridColumn10.Caption = st.isEnglish() ? "Address" : "العنوان";
                gridColumn2.Caption = st.isEnglish() ? "Email" : "البريد الالكتروني";
                gridColumn9.Caption = st.isEnglish() ? "Mobile 1 " : "موبيل 1";
                gridColumn1.Caption = st.isEnglish() ? "Mobile 2 " : "موبيل 2";
                gridColumn4.Caption = st.isEnglish() ? "Jop" : "الوظيفة";
                gridColumn6.Caption = st.isEnglish() ? "Date of Hiring" : "ناريخ التعين";
                gridColumn8.Caption = st.isEnglish() ? "Work end Date" : "تاريخ انهاء العمل";
                gridColumn3.Caption = st.isEnglish() ? "Notes" : "الملاحظات";
                gvEmployeeCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";

                windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "New" : "جديد";
                windowsUIButtonPanel.Buttons[1].Properties.Caption = st.isEnglish() ? "Edite" : "تعديل";
                windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Delete" : "حذف";
                windowsUIButtonPanel.Buttons[3].Properties.Caption = st.isEnglish() ? "Refresh" : "تحديث";
                windowsUIButtonPanel.Buttons[5].Properties.Caption = st.isEnglish() ? "Print" : "طباعة";
                windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
                materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "New" : "جديد";
                materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Edite" : "تعديل";
                materialContextMenuStrip1.Items[2].Text = st.isEnglish() ? "Delete" : "حذف";
            
        }

      
        void FillGride()
        {
            gcEmployeeCard.DataSource = null;
            var branchCode = st.GetBranch_Code();
            using (POSEntity Contexts = new POSEntity())
            {

                var empData = (from a in Contexts.Employee_View where a.IsDeleted == 0 && a.Employee_Code != 0 && a.Branch_ID == branchCode select a).OrderBy(x => x.Employee_Code).ToList();
                

                if (empData.Count > 0)
                {

                    gcEmployeeCard.DataSource = empData;
                    gcEmployeeCard.RefreshDataSource();

                }
                else
                {

                    gcEmployeeCard.Enabled = false;
                    حذفToolStripMenuItem.Enabled = false;
                    تعديلToolStripMenuItem.Enabled = false;
                    windowsUIButtonPanel.Buttons.ForEach(x =>
                    {

                        if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "Edite" || x.Properties.Caption == "Print" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف" || x.Properties.Caption == "Delete")
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
        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد" || btn.Caption == "New")
            {
                FrmAddEmployees frm = new FrmAddEmployees();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل" || btn.Caption == "Edite")
            {

                if (gvEmployeeCard.RowCount <= 0)
                {
                    return;
                }

                 frmEditeEmployees frm = new frmEditeEmployees();

               var x = gvEmployeeCard.GetFocusedRow() as Employee_View;
                frm.FillBranchSlk();
                frm.FillJopSlk();
                frm.FillSexSlk();

                frm.txtEmpCode.Text =  x.Employee_Code.ToString();
                frm.TxtEmpName.Text = x.Employee_Name.ToString();
                frm.txtEmpMob1.Text = x.Employee_Mobile_1.ToString();
                frm.txtEmpMob2.Text = x.Employee_Mobile_2.ToString();
                
                frm.TxtEmpEmail.Text = x.Employee_Email.ToString();
                frm.TxtEmpAddress.Text = x.Employee_Address.ToString();
                frm.TxtEmpNote.Text = x.Employee_Notes.ToString();
                frm.slkBranch.EditValue = x.Branch_ID;
                frm.slkJop.EditValue = x.Jop_Code;
                frm.slkSex.EditValue = x.SexTypeCode;
                 
                
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
                if (MaterialMessageBox.Show(st.isEnglish() ? "?Are you sure you want to delete This Employee" : "تاكيد حذف الموظف؟", MessageBoxButtons.YesNo) == DialogResult.OK)
                {
                    var x2 = gvEmployeeCard.GetFocusedRow() as Employee_View;




                    using (POSEntity Contexts = new POSEntity())
                    {
                        var IfUser = Contexts.User_View.Any(x => x.UserFlag == true && x.IsDeleted == 0 && x.Employee_Code == x2.Employee_Code);
                        var IfhaveInvoice = Contexts.SaleMasterViews.Any(x => x.UserCode == x2.Employee_Code && x.IsDeleted == 0 );

                        if (IfUser)
                        {
                            MaterialMessageBox.Show(st.isEnglish() ? "This employee cannot be deleted because there is an account for him in the users screen" : "لا يمكن حذف هذا الموظف بسبب وجود حساب له في شاشة المستخدمين", MessageBoxButtons.OK);

                            return;
                        }

                        else if (IfhaveInvoice)
                        {
                            MaterialMessageBox.Show(st.isEnglish() ? "This employee cannot be deleted because there are invoices attached to his account" : "لا يمكن حذف هذا الموظف بسبب وجود فواتير مربوطه بحسابه", MessageBoxButtons.OK);

                            return;

                        }

                        else
                        {
                            Employee _Employee;
                            _Employee = Contexts.Employees.SingleOrDefault(Emp => Emp.Employee_Code == x2.Employee_Code);
                            _Employee.IsDeleted = 1;
                            Contexts.SaveChanges();
                            MaterialMessageBox.Show(st.isEnglish() ? "Deleted successfully" : "تم الحذف", MessageBoxButtons.OK);
                             
                        }

                    }


                }


            }

            //else if (btn.Caption == "تحديث")
            //{

            //    using (POSEntity Contexts = new POSEntity())
            //    {

            //        var result = (from a in Contexts.User_Detail_View where a.IsDeleted == 0 select a).ToList();
            //        gcEmployeeCard.DataSource = result;
            //        gcEmployeeCard.RefreshDataSource();

            //        if (gvEmployeeCard.RowCount <= 0)
            //        {

            //            gcEmployeeCard.Enabled = false;
            //            windowsUIButtonPanel.Buttons.ForEach(x =>
            //            {

            //                if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
            //                {

            //                    x.Properties.Enabled = false;

            //                }

            //            });



            //        }
            //        else
            //        {

            //            gcEmployeeCard.Enabled = true;

            //        }
            //    }


            //}


            //else if (btn.Caption == "طباعة")
            //{

            //    if (gvEmployeeCard.RowCount <= 0)
            //    {


            //        return;
            //    }

            //    using (POSEntity Contexts = new POSEntity())
            //    {

            //        var result = (from a in Contexts.ItemCardViews where a.IsDeleted == 0 select a).ToList();
            //        gcEmployeeCard.DataSource = result;
            //        gcEmployeeCard.RefreshDataSource();

            //        if (gvEmployeeCard.RowCount <= 0)
            //        {

            //            gcEmployeeCard.Enabled = false;

            //        }
            //        else
            //        {

            //            gcEmployeeCard.Enabled = true;
            //        }
            //    }


            //}
        }

        private void اضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAddEmployees frm = new FrmAddEmployees();
            frm.ShowDialog();
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gvEmployeeCard.RowCount <= 0)
            {
                return;
            }

            frmEditeEmployees frm = new frmEditeEmployees();

            var x = gvEmployeeCard.GetFocusedRow() as Employee_View;
            frm.FillBranchSlk();
            frm.FillJopSlk();
            frm.FillSexSlk();

            frm.txtEmpCode.Text = x.Employee_Code.ToString();
            frm.TxtEmpName.Text = x.Employee_Name.ToString();
            frm.txtEmpMob1.Text = x.Employee_Mobile_1 != null? x.Employee_Mobile_1.ToString():"";
            frm.txtEmpMob2.Text = x.Employee_Mobile_2 != null?  x.Employee_Mobile_2.ToString():"";
            frm.TxtEmpNataionalId.Text = x.Employee_National_Id !=null? x.Employee_National_Id.ToString():"";
            frm.dtEmpStartJop.EditValue = x.Employee_Start_Jop != null? x.Employee_Start_Jop :DateTime.Now ;
            frm.dtEmpEndJop.EditValue = x.Employee_End_Jop != null ? x.Employee_End_Jop : DateTime.Now;

            frm.TxtEmpEmail.Text = x.Employee_Email != null ? x.Employee_Email.ToString() : "";
            frm.TxtEmpAddress.Text = x.Employee_Address != null ?  x.Employee_Address.ToString() :"";
            frm.TxtEmpNote.Text = x.Employee_Notes != null ?     x.Employee_Notes.ToString() :"";
            frm.slkBranch.EditValue = x.Branch_ID ;
            frm.slkJop.EditValue = x.Jop_Code;
            frm.slkSex.EditValue = x.SexTypeCode;
            

            frm.ShowDialog();
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show(st.isEnglish() ? "?Are you sure you want to delete This Employee" : "تاكيد حذف الموظف؟", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                Employee_View xx = gvEmployeeCard.GetFocusedRow() as Employee_View;
                Employee _Employees = new Employee();
                _Employees =  context.Employees.SingleOrDefault(item => item.Employee_Code == xx.Employee_Code &&item.Branch_ID == xx.Branch_ID && item.IsDeleted == 0);
                _Employees.IsDeleted = 1;
                context.SaveChanges();
                FillGride();
            }
        }

        public void CheckGridDataCount()
        {

            if (gvEmployeeCard.RowCount <= 0)
            {

                gcEmployeeCard.Enabled = false;
                windowsUIButtonPanel.Buttons.ForEach(x =>
                {

                    if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "Edite" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                    {

                        x.Properties.Enabled = false;

                    }
                    if (x.Properties.Caption == "جديد")
                    {

                        x.Properties.Enabled = true;

                    }

                });

            }
            else
            {

                gcEmployeeCard.Enabled = true;
                windowsUIButtonPanel.Buttons.ForEach(x =>
                {

                    if (x.Properties.Caption == "تعديل" || x.Properties.Caption == "طباعة" || x.Properties.Caption == "حذف")
                    {

                        x.Properties.Enabled = true;

                    }
                    if (x.Properties.Caption == "جديد")
                    {

                        x.Properties.Enabled = true;

                    }

                });


            }

        }
    }
}