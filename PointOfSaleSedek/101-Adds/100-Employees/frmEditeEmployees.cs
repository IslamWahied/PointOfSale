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

namespace PointOfSaleSedek._101_Adds.Employees
{
    public partial class frmEditeEmployees : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities context = new PointOfSaleEntities();
        readonly Static st = new Static();
        public frmEditeEmployees()
        {
            InitializeComponent();
           
        }
       public void FillJopSlk()
        {

            var result = context.Jops.Where(jop => jop.IsDeleted == 0).ToList();
            slkJop.Properties.DataSource = result;
            slkJop.Properties.ValueMember = "JopCode";
            slkJop.Properties.DisplayMember = "JobName";
             

        }

     public void FillBranchSlk()
        {

            var result = context.Branches.Where(Branch => Branch.IsDeleted == 0).ToList();
            slkBranch.Properties.DataSource = result;
            slkBranch.Properties.ValueMember = "Branches_Code";
            slkBranch.Properties.DisplayMember = "Branches_Name";
             
        }


  public void FillSexSlk()
        {

            var result = context.SexTypes.ToList();
            slkSex.Properties.DataSource = result;
            slkSex.Properties.ValueMember = "SexTypeCode";
            slkSex.Properties.DisplayMember = "SexTypeName";
            
        }
       

       
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditItem();
        }
        void EditItem()
        {
            if (string.IsNullOrWhiteSpace(txtEmpCode.Text))
            {

                MaterialMessageBox.Show("برجاء ادخال كود العميل", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(TxtEmpName.Text))
            {
                MaterialMessageBox.Show("برجاء ادخال اسم العميل", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(slkSex.Text))
            {

                MaterialMessageBox.Show("برجاءاختيار الجنس", MessageBoxButtons.OK);
                return;

            }

            if (string.IsNullOrWhiteSpace(slkJop.Text))
            {

                MaterialMessageBox.Show("برجاءاختيار الوظيفة", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(slkBranch.Text))
            {

                MaterialMessageBox.Show("برجاءاختيار الفرع", MessageBoxButtons.OK);
                return;

            }
          
            if (Application.OpenForms.OfType<frmEmployees>().Any())
            {
                frmEmployees frm = (frmEmployees)Application.OpenForms["frmEmployees"];
                Int64 EmpCode = Convert.ToInt64(txtEmpCode.Text);
                using (PointOfSaleEntities ForCheck = new PointOfSaleEntities())
                {
                    bool TestUserName = ForCheck.Employees.Any(Emp => Emp.Employee_Name == TxtEmpName.Text && Emp.Employee_Code != EmpCode && Emp.IsDeleted == 0);
                    if (TestUserName)
                    {
                        MaterialMessageBox.Show("تم تسجيل هذا الاسم لموظف اخر", MessageBoxButtons.OK);
                        return;

                    }
                    bool TestNatId = ForCheck.Employees.Any(Emp => Emp.Employee_Code != EmpCode && Emp.Employee_National_Id == TxtEmpNataionalId.Text && Emp.IsDeleted == 0);
                    if (TestNatId)
                    {

                        MaterialMessageBox.Show("تم تسجيل رقم البطاقة لموظف اخر", MessageBoxButtons.OK);
                        return;

                    }


                }
                
                Employee _Employee;
                _Employee = context.Employees.SingleOrDefault(Employee => Employee.Employee_Code == EmpCode && Employee.IsDeleted == 0);
                _Employee.Employee_Code = EmpCode;
                _Employee.Employee_Name = TxtEmpName.Text;
                _Employee.Employee_Mobile_1 = txtEmpMob1.Text;
                _Employee.Employee_Mobile_2 = txtEmpMob2.Text;
                _Employee.Employee_National_Id = TxtEmpNataionalId.Text;
                _Employee.Employee_Email = TxtEmpEmail.Text;
                _Employee.Employee_Address = TxtEmpAddress.Text;
                _Employee.Employee_Start_Jop = Convert.ToDateTime(dtEmpStartJop.EditValue);

                if (!string.IsNullOrWhiteSpace(dtEmpEndJop.Text))
                { 
                
                _Employee.Employee_End_Jop = Convert.ToDateTime(dtEmpEndJop.Text);
                
                }

                _Employee.Employee_Notes = TxtEmpNote.Text;
                _Employee.SexTypeCode = Convert.ToInt16(slkSex.EditValue);
                
           

                _Employee.Last_Modified_Date = DateTime.Now;
                _Employee.Last_Modified_User = st.User_Code();
                context.SaveChanges();
                frmEmployees frmEmpMain = new frmEmployees();
                frmEmpMain.gcEmployeeCard.DataSource = context.Employee_View.Where(x => x.IsDeleted == 0).ToList();
                frmEmpMain.gcEmployeeCard.RefreshDataSource();
                using (PointOfSaleEntities NewReco = new PointOfSaleEntities())
                { 
                
                    Employee_View result2 = NewReco.Employee_View.Where(x => x.Employee_Code == EmpCode && x.IsDeleted == 0).FirstOrDefault();
                
                
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Code", result2.Employee_Code);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Name", result2.Employee_Name);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Mobile_1", result2.Employee_Mobile_1);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Mobile_2", result2.Employee_Mobile_2);

                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Start_Jop", result2.Employee_Start_Jop);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_End_Jop", result2.Employee_End_Jop);

                frm.gvEmployeeCard.SetFocusedRowCellValue("SexTypeName", result2.SexTypeName);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Notes", result2.Employee_Notes);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_National_Id", result2.Employee_National_Id);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Branches_Name", result2.Branches_Name);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Email", result2.Employee_Email);
                frm.gvEmployeeCard.SetFocusedRowCellValue("Employee_Address", result2.Employee_Address);
               
            
                frm.gvEmployeeCard.RefreshData();
                MaterialMessageBox.Show("تم التعديل", MessageBoxButtons.OK);
                HelperClass.HelperClass.ClearValues(tableLayoutPanel2);
                }


                this.Close();
            }

        }

        private void btnCancel_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            EditItem();
        }
    }
}