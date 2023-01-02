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

namespace PointOfSaleSedek._101_Adds.Employees
{
    public partial class frmEditeEmployees : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmEditeEmployees()
        {
            InitializeComponent();
            langu();


        }
        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Edit Employee Data" : "تعديل بيانات الموظف";
            materialLabel10.Text = st.isEnglish() ? "Code" : "كود الموظف";
            materialLabel11.Text = st.isEnglish() ? "Mobile 1" : "موبيل 1";
            materialLabel4.Text = st.isEnglish() ? "Mobile 2" : "موبيل 2";
            materialLabel3.Text = st.isEnglish() ? "Email" : "الايميل";
            materialLabel1.Text = st.isEnglish() ? "Name" : "الاسم";
            materialLabel13.Text = st.isEnglish() ? "Sex" : "الجنس";
            TxtEmpEmail.Text = st.isEnglish() ? "Email" : "الايميل";
            materialLabel2.Text = st.isEnglish() ? "National ID" : "الرقم القومي";
        
            materialLabel12.Text = st.isEnglish() ? "Branch" : "الفرع";
            materialLabel7.Text = st.isEnglish() ? "Jop" : "الوظيفة";
            materialLabel8.Text = st.isEnglish() ? "Date Of Hiring" : "تاريخ التوظيف";
            materialLabel9.Text = st.isEnglish() ? "Resignation Date" : "تاريخ الاستقالة";
            materialLabel5.Text = st.isEnglish() ? "Address" : "العنوان";
            materialLabel6.Text = st.isEnglish() ? "Notes" : "ملاحظات";
            gridColumn3.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn1.Caption = st.isEnglish() ? "Name" : "الاسم";
            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
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

                MaterialMessageBox.Show(st.isEnglish() ? "Please enter the customer code" : "برجاء ادخال كود العميل", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(TxtEmpName.Text))
            {
                MaterialMessageBox.Show(st.isEnglish() ? "Please enter the customer's name" : "برجاء ادخال اسم العميل", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(slkSex.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select a gender" : "برجاءاختيار الجنس", MessageBoxButtons.OK);
                return;

            }

            if (string.IsNullOrWhiteSpace(slkJop.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please choose a job" : "برجاءاختيار الوظيفة", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(slkBranch.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select a branch" : "برجاءاختيار الفرع", MessageBoxButtons.OK);
                return;

            }

            //if (string.IsNullOrWhiteSpace(dtEmpStartJop.Text))
            //{

            //    MaterialMessageBox.Show(st.isEnglish() ? "Please select the employee's employment start date" : "برجاءاختيار تاريخ تعين الموظف", MessageBoxButtons.OK);
            //    return;

            //}





            if (Application.OpenForms.OfType<frmEmployees>().Any())
            {
                frmEmployees frm = (frmEmployees)Application.OpenForms["frmEmployees"];
                Int64 EmpCode = Convert.ToInt64(txtEmpCode.Text);
                using (POSEntity ForCheck = new POSEntity())
                {
                    bool TestUserName = ForCheck.Employees.Any(Emp => Emp.Employee_Name == TxtEmpName.Text && Emp.Employee_Code != EmpCode && Emp.IsDeleted == 0);
                    if (TestUserName)
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "This name has been registered for another employee" : "تم تسجيل هذا الاسم لموظف اخر", MessageBoxButtons.OK);
                        return;

                    }
                    bool TestNatId = ForCheck.Employees.Any(Emp => Emp.Employee_Code != EmpCode && Emp.Employee_National_Id == TxtEmpNataionalId.Text && Emp.IsDeleted == 0);
                    if (TestNatId)
                    {

                        MaterialMessageBox.Show(st.isEnglish() ? "The card number has been registered to another employee" : "تم تسجيل رقم البطاقة لموظف اخر", MessageBoxButtons.OK);
                        return;

                    }


                }
                var barchCode = st.GetBranch_Code();
                Employee _Employee;
                _Employee = context.Employees.SingleOrDefault(Employee => Employee.Employee_Code == EmpCode && Employee.IsDeleted == 0 && Employee.Branch_ID == barchCode);
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
                _Employee.Last_Modified_User = st.GetUser_Code();
                context.SaveChanges();
                //frmEmployees frmEmpMain = new frmEmployees();
                //frmEmpMain.gcEmployeeCard.DataSource = context.Employee_View.Where(x => x.IsDeleted == 0).ToList();
                //frmEmpMain.gcEmployeeCard.RefreshDataSource();
                Employee_View result2;
                using (POSEntity NewReco = new POSEntity())
                { 
                
                      result2 = NewReco.Employee_View.Where(x => x.Employee_Code == EmpCode && x.IsDeleted == 0).FirstOrDefault();
                
                
               
                }

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
                MaterialMessageBox.Show(st.isEnglish() ? "Edited successfully" : "تم التعديل", MessageBoxButtons.OK);
                HelperClass.HelperClass.ClearValues(tableLayoutPanel2);
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