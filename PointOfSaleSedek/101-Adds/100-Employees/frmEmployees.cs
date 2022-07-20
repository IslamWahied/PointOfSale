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

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmEmployees : DevExpress.XtraEditors.XtraForm
    {
        SaleEntities context = new SaleEntities();
        public frmEmployees()
        {
            InitializeComponent();
            FillGride();
        }

        private void frmCustomers_Load(object sender, EventArgs e)
        {

        }

        private void gcItemCard_Click(object sender, EventArgs e)
        {

        }

        void FillGride()
        {
            gcEmployeeCard.DataSource = null;
            using (SaleEntities Contexts = new SaleEntities())
            {

                var empData = (from a in Contexts.Employee_View where a.IsDeleted == 0 select a).OrderBy(x => x.Employee_Code).ToList();
                

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
        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد")
            {
                FrmAddEmployees frm = new FrmAddEmployees();
                frm.ShowDialog();
            }
            else if (btn.Caption == "تعديل")
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
                    var x2 = gvEmployeeCard.GetFocusedRow() as Employee_View;




                    using (SaleEntities Contexts = new SaleEntities())
                    {
                        var IfUser = Contexts.User_View.Any(x => x.UserFlag == true && x.IsDeleted == 0 && x.Employee_Code == x2.Employee_Code);
                        var IfhaveInvoice = Contexts.SaleMasterViews.Any(x => x.UserCode == x2.Employee_Code && x.IsDeleted == 0 );

                        if (IfUser)
                        {
                            MaterialMessageBox.Show("لا يمكن حذف هذا الموظف بسبب وجود حساب له في شاشة المستخدمين", MessageBoxButtons.OK);

                            return;
                        }

                        else if (IfhaveInvoice)
                        {
                            MaterialMessageBox.Show("لا يمكن حذف هذا الموظف بسبب وجود فواتير مربوطه بحسابه", MessageBoxButtons.OK);

                            return;

                        }

                        else
                        {
                            Employee _Employee;
                            _Employee = Contexts.Employees.SingleOrDefault(Emp => Emp.Employee_Code == x2.Employee_Code);
                            _Employee.IsDeleted = 1;
                            Contexts.SaveChanges();
                            MaterialMessageBox.Show("تم الحذف", MessageBoxButtons.OK);
                             
                        }

                    }


                }


            }

            //else if (btn.Caption == "تحديث")
            //{

            //    using (SaleEntities Contexts = new SaleEntities())
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

            //    using (SaleEntities Contexts = new SaleEntities())
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
            if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
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