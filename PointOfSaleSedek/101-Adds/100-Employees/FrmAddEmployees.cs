using PointOfSaleSedek._102_MaterialSkin;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSaleSedek._101_Adds;
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek.Employees
{
    public partial class FrmAddEmployees : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        //readonly MaterialSkin.MaterialSkinManager skinManager;
        public FrmAddEmployees()
        {
            InitializeComponent();
            langu();
            Int64? MaxCode = context.Employees.Where(x => x.IsDeleted == 0).Max(u => (Int64?)u.Employee_Code + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }


            txtEmpCode.Text = MaxCode.ToString();
            FillJopSlk();
            FillBranchSlk();
            FillSexSlk();


        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Add Employee" : "اضافة موظف";

            materialLabel10.Text = st.isEnglish() ? "Code":"كود الموظف";
            TxtEmpEmail.Text = st.isEnglish() ? "Email" : "الايميل";
            materialLabel11.Text = st.isEnglish() ? "Mobile 1":"موبيل 1";
            materialLabel4.Text = st.isEnglish() ? "Mobile 2":"موبيل 2";
            gridColumn3.Caption = st.isEnglish() ? "Name":"الاسم";
            gridColumn4.Caption = st.isEnglish() ? "Name":"الاسم";
            gridColumn1.Caption = st.isEnglish() ? "Name":"الاسم";
            materialLabel1.Text = st.isEnglish() ? "Name":"الاسم";
            materialLabel3.Text = st.isEnglish() ? "Email" : "الايميل";
            materialLabel13.Text = st.isEnglish() ? "Sex":"الجنس";
            materialLabel2.Text = st.isEnglish() ? "National ID" : "الرقم القومي";
            materialLabel12.Text = st.isEnglish() ? "Branch" : "الفرع";
            materialLabel7.Text = st.isEnglish() ? "Jop" : "الوظيفة";
            materialLabel8.Text = st.isEnglish() ? "Date Of Hiring" : "تاريخ التوظيف";
            materialLabel9.Text = st.isEnglish() ? "Resignation Date" : "تاريخ الاستقالة";
            materialLabel5.Text = st.isEnglish() ? "Address" : "العنوان";
            materialLabel6.Text = st.isEnglish() ? "Notes" : "ملاحظات";
            btnAdd.Text = st.isEnglish() ? "Add": "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close": "اغلاق";
        }

        void FillJopSlk()
        {

            var result = context.Jops.Where(jop => jop.IsDeleted == 0).ToList();
            slkJop.Properties.DataSource = result;
            slkJop.Properties.ValueMember = "JopCode";
            slkJop.Properties.DisplayMember = "JobName";
            slkJop.EditValue = result[0].JopCode;

        }

        void FillBranchSlk()
        {

            var result = context.Branches.Where(Branch => Branch.IsDeleted == 0).ToList();
            slkBranch.Properties.DataSource = result;
            slkBranch.Properties.ValueMember = "Branches_Code";
            slkBranch.Properties.DisplayMember = "Branches_Name";
            slkBranch.EditValue = result[0].Branches_Code;
        }


        void FillSexSlk()
        {

            var result = context.SexTypes.ToList();
            slkSex.Properties.DataSource = result;
            slkSex.Properties.ValueMember = "SexTypeCode";
            slkSex.Properties.DisplayMember = "SexTypeName";
            slkSex.EditValue = result[0].SexTypeCode;
        }


        public void Save()
        {
            if (string.IsNullOrWhiteSpace(txtEmpCode.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please enter the customer code" :"برجاء ادخال كود العميل", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(TxtEmpName.Text))
            {
                MaterialMessageBox.Show(st.isEnglish() ? "Please enter the customer's name" :"برجاء ادخال اسم العميل", MessageBoxButtons.OK);
                return;
            }
            if (string.IsNullOrWhiteSpace(slkSex.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select a gender" : "برجاءاختيار الجنس", MessageBoxButtons.OK);
                return;

            }

            if (string.IsNullOrWhiteSpace(slkJop.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please choose a job" :"برجاءاختيار الوظيفة", MessageBoxButtons.OK);
                return;

            }
            if (string.IsNullOrWhiteSpace(slkBranch.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select a branch" :"برجاءاختيار الفرع", MessageBoxButtons.OK);
                return;

            }

            if (string.IsNullOrWhiteSpace(dtEmpStartJop.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select the employee's employment start date" :"برجاءاختيار تاريخ تعين الموظف", MessageBoxButtons.OK);
                return;

            }


            if (Application.OpenForms.OfType<frmEmployees>().Any())
            {
                Int64 EmpCode = Convert.ToInt64(txtEmpCode.Text);

                bool TestUpdate = context.Employees.Any(customer => customer.Employee_Code == EmpCode && customer.IsDeleted == 0);
                frmEmployees frm = (frmEmployees)Application.OpenForms["frmEmployees"];
                if (TestUpdate)
                {

                    using (POSEntity ForCheck = new POSEntity())
                    {

                        bool TestUserName = ForCheck.Employees.Any(Emp => Emp.Employee_Name == TxtEmpName.Text && Emp.Employee_Code !=EmpCode &&  Emp.IsDeleted == 0);
                        if (TestUserName)
                        {
                        MaterialMessageBox.Show(st.isEnglish() ? "This name has been registered for another employee" :"تم تسجيل هذا الاسم لموظف اخر", MessageBoxButtons.OK);
                        return;

                        }
                        bool TestNatId = ForCheck.Employees.Any(Emp => Emp.Employee_National_Id == TxtEmpNataionalId.Text && Emp.IsDeleted == 0);
                        if (TestUserName)
                        {

                            MaterialMessageBox.Show(st.isEnglish() ? "The card number has been registered to another employee" :"تم تسجيل رقم البطاقة لموظف اخر", MessageBoxButtons.OK);
                            return;

                        }


                    }
                    Employee _Employee;
                    _Employee = context.Employees.SingleOrDefault(Employee => Employee.Employee_Code == EmpCode);
                    _Employee.Employee_Code = EmpCode;
                    _Employee.Employee_Name = TxtEmpName.Text;
                    _Employee.Employee_Mobile_1 = txtEmpMob1.Text;
                    _Employee.Employee_Mobile_2 = txtEmpMob2.Text;
                    _Employee.Employee_National_Id = TxtEmpNataionalId.Text;
                    _Employee.Employee_Email = TxtEmpEmail.Text;
                    _Employee.Branch_ID = Convert.ToInt64(slkBranch.EditValue);
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

                    using (POSEntity Contx2 = new POSEntity())
                    {
                    frm.gcEmployeeCard.DataSource = Contx2.Employee_View.Where(x => x.IsDeleted == 0).ToList();
                    frm.gcEmployeeCard.RefreshDataSource();
                    MaterialMessageBox.Show(st.isEnglish() ? "Modified" :"تم التعديل", MessageBoxButtons.OK);

                    }

                }
                else
                {
                    using (POSEntity ForCheck = new POSEntity())
                    {

                        bool TestUserName = ForCheck.Employees.Any(Emp => Emp.Employee_Name == TxtEmpName.Text && Emp.IsDeleted == 0);
                        if (TestUserName)
                        { 
                        
                        MaterialMessageBox.Show(st.isEnglish() ? "This name has been registered for another employee" :"تم تسجيل هذا الاسم لموظف اخر", MessageBoxButtons.OK);
                        return;
                        
                        }
                        bool TestNatId = ForCheck.Employees.Any(Emp => Emp.Employee_National_Id == TxtEmpNataionalId.Text && Emp.IsDeleted == 0);
                        if (TestNatId)
                        {

                            MaterialMessageBox.Show(st.isEnglish() ? "The card number has been registered to another employee" :"تم تسجيل رقم البطاقة لموظف اخر", MessageBoxButtons.OK);
                            return;

                        }
                    };

                    if (!string.IsNullOrWhiteSpace(dtEmpEndJop.Text))
                    {

                        Employee _Employee = new Employee()
                        {
                            Employee_Code = EmpCode,
                            Employee_Name = TxtEmpName.Text,
                            Employee_Mobile_1 = txtEmpMob1.Text,
                            Employee_Mobile_2 = txtEmpMob2.Text,
                            Employee_National_Id = TxtEmpNataionalId.Text,
                            Employee_Email = TxtEmpEmail.Text,
                            Employee_Address = TxtEmpAddress.Text,
                            Employee_Start_Jop = Convert.ToDateTime(dtEmpStartJop.EditValue),
                            Employee_End_Jop = Convert.ToDateTime(dtEmpEndJop.Text),
                            Employee_Notes = TxtEmpNote.Text,
                            Created_Date = DateTime.Now,
                            
                            SexTypeCode = Convert.ToInt16(slkSex.EditValue),
                            Branch_ID =0,
                            Last_Modified_User = st.GetUser_Code(),
                            Jop_Code = Convert.ToInt64(slkJop.EditValue),
                            img_Url = "",


                        };
                        context.Employees.Add(_Employee);
                        context.SaveChanges();
                    }
                    else
                    {
                        Employee _Employee = new Employee()
                        {
                            Employee_Code = EmpCode,
                            Employee_Name = TxtEmpName.Text,
                            Employee_Mobile_1 = txtEmpMob1.Text,
                            Employee_Mobile_2 = txtEmpMob2.Text,
                            Employee_National_Id = TxtEmpNataionalId.Text,
                            Employee_Email = TxtEmpEmail.Text,
                            Employee_Address = TxtEmpAddress.Text,
                            Employee_Start_Jop = Convert.ToDateTime(dtEmpStartJop.EditValue),
                            
                    
                            Employee_Notes = TxtEmpNote.Text,
                            Created_Date = DateTime.Now,
                            SexTypeCode = Convert.ToInt16(slkSex.EditValue),
                            Branch_ID = 0,
                            Last_Modified_User = st.GetUser_Code(),
                           
                            Jop_Code = Convert.ToInt64(slkJop.EditValue),
                            img_Url = "",


                        };
                        context.Employees.Add(_Employee);
                    context.SaveChanges();
                    }


                
                   
                    using (POSEntity NewContext = new POSEntity())
                    { 
                    
                        frm.gcEmployeeCard.DataSource = NewContext.Employee_View.Where(x => x.IsDeleted == 0 && x.Employee_Code != 0).ToList();
                        frm.gcEmployeeCard.Enabled = true;

                    frm.gcEmployeeCard.RefreshDataSource();

                        MaterialMessageBox.Show(st.isEnglish() ? "Customer has been registered successfully" :"تم تسجيل العميل بنجاح", MessageBoxButtons.OK);
                        this.Close();




                    }

                   
                }

            }
        }

    

        public void Delete()
        {
            //Customer_Info _Customer;
            //_Customer = db.Customer_Info.SingleOrDefault(customer => customer.Customer_Phone == TxtMob.Text);
            //_Customer.IsDeleted = 1;
            //db.SaveChanges();
            //MaterialMessageBox.Show("تم الحذف", MessageBoxButtons.OK);
        }

      
        public void GetById()
        {
            throw new NotImplementedException();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //#region Vaildate
            //if (string.IsNullOrWhiteSpace(txtEmpCode.Text))
            //{

            //    MaterialMessageBox.Show(st.isEnglish() ? "Please enter the customer code" :"برجاء ادخال كود للعميل", MessageBoxButtons.OK);
            //    return;


            //}
            //if (string.IsNullOrEmpty(txtEmpMob2.Text))
            //{

            //    MaterialMessageBox.Show(st.isEnglish() ? "Please enter the mobile number" :"برجاء ادخال رقم الموبيل", MessageBoxButtons.OK);
            //    return;


            //}
            //if (string.IsNullOrEmpty(TxtEmpName.Text))
            //{
            //    MaterialMessageBox.Show(st.isEnglish() ? "Please enter the customer's name" :"برجاء ادخال اسم العميل", MessageBoxButtons.OK);

            //    return;
            //}
            //#endregion
           
            Save();
          
            Rest();
            
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpMob2.Text))
            {
                MaterialMessageBox.Show(st.isEnglish() ? "Please enter the mobile number" :"برجاء ادخال رقم الموبيل", MessageBoxButtons.OK);
               
                return;
            }
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            Delete();
            Rest();
            SplashScreenManager.CloseForm();
        }



        void Rest()
        {
            txtEmpMob2.Text = "";
            TxtEmpName.Text = "";
            TxtEmpEmail.Text = "";
            TxtEmpNataionalId.Text = "";
            TxtEmpAddress.Text = "";
            txtEmpMob1.Text = "";
            txtEmpMob2.Text = "";
            TxtEmpNote.Text = "";
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                Rest();
            }
            catch { 
            }

          
        }
    }
}
