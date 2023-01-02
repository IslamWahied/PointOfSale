using System; 
using System.Linq;
using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;
using PointOfSaleSedek._102_MaterialSkin;
using DataRep;

namespace PointOfSaleSedek._101_Adds._102_Customer
{
    public partial class frmAddCustomer : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmAddCustomer()
        {
            InitializeComponent();

            langu();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Add Customer" : "اضافة عميل";
            materialLabel1.Text = st.isEnglish() ? "Name" : "الاسم";
            materialLabel11.Text = st.isEnglish() ? "Mobile" : "الموبيل";
            materialLabel13.Text = st.isEnglish() ? "Gender" : "النوع";

            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn4.FieldName = st.isEnglish() ? "SexTypeName_En" : "SexTypeName";
            btnAdd.Text = st.isEnglish() ? "Save" : "حفظ";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";

        }

        void FillSexSlk()
        {

           
                var result = context.SexTypes.ToList();
                slkSex.Properties.DataSource = result;
            slkSex.Properties.ValueMember = "SexTypeCode";
            slkSex.Properties.DisplayMember = st.isEnglish() ? "SexTypeName_En" : "SexTypeName";

             
            slkSex.EditValue = result[0].SexTypeCode;
               

            
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            Save();
        }
        public void Save()
        {
            
            if (String.IsNullOrWhiteSpace(slkSex.Text) || String.IsNullOrWhiteSpace(TxtEmpName.Text) || String.IsNullOrWhiteSpace(txtEmpMob1.Text))
            {
                MaterialMessageBox.Show(st.isEnglish() ? "Please enter all fields" :"برجاء ادخال جميع الحقول", MessageBoxButtons.OK);
                return;
            }

            var Phone = txtEmpMob1.Text;
            Int64? MaxCode = context.Customer_Info.Max(u => (Int64?)u.Customer_Code + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }

            bool isOldCustomer = context.Customer_Info.Any(x => x.Customer_Phone == Phone);

            if (isOldCustomer)
            {
                MaterialMessageBox.Show(st.isEnglish() ? "This customer has already been registered" : "تم تسجل هذا العميل من قبل", MessageBoxButtons.OK);
                return;
            }
            else
            {

                Customer_Info _customer = new Customer_Info()
                {

                    Customer_Phone = Phone,
                    Created_Date = DateTime.Now,
                    Customer_Code = Convert.ToInt64(MaxCode),
                    Customer_Name = TxtEmpName.Text,
                    Branch_Code = st.GetBranch_Code(),
                    SexTypeCode = Convert.ToInt32(slkSex.EditValue),
                    Last_Modified_User = st.GetUser_Code(),
            };
                context.Customer_Info.Add(_customer);
                context.SaveChanges();

                //MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);


                if (st.Project_Type() == "Cafe")
                {

                    if (Application.OpenForms.OfType<frmCafeSales>().Any())
                    {


                        frmCafeSales frm = (frmCafeSales)Application.OpenForms["frmCafeSales"];

                        frm.FillSlkCustomers();

                        frm.slkCustomers.EditValue = context.Customer_View.FirstOrDefault(Customer => Customer.Customer_Phone == Phone).Customer_Code;
                        frm.slkCustomers.Enabled = true;
                        frm.btnCustomerHistory.Enabled = true;
                        frm.txtParCode.Focus();
                        this.Close();



                    }
                }
                else if (st.Project_Type() == "Perfum")
                {
                    if (Application.OpenForms.OfType<frmPerfumSales>().Any())
                    {


                        frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];

                        Int64 User_Code = st.GetUser_Code();


                        frm.FillSlkCustomers();

                        frm.slkCustomers.EditValue = context.Customer_View.FirstOrDefault(Customer => Customer.Customer_Phone == Phone).Customer_Code;
                        frm.slkCustomers.Enabled = true;
                        frm.btnCustomerHistory.Enabled = true;
                        //frm.txtParCode.Focus();
                        this.Close();


                    }
                }
                else if (st.Project_Type() == "SuperMarket")
                {
                    if (Application.OpenForms.OfType<frmSuperMarketSales>().Any())
                    {


                        frmSuperMarketSales frm = (frmSuperMarketSales)Application.OpenForms["frmSuperMarketSales"];

                       
                      

                        Int64 User_Code = st.GetUser_Code();


                        frm.FillSlkCustomer();

                        frm.slkCustomers.EditValue = context.Customer_View.FirstOrDefault(Customer => Customer.Customer_Phone == Phone).Customer_Code;
                        frm.slkCustomers.Enabled = true;
                        frm.btnCustomerHistory.Enabled = true;
                        //frm.txtParCode.Focus();
                        this.Close();


                    }
                }







            }



        }

        private void frmAddCustomer_Load(object sender, EventArgs e)
        {
            FillSexSlk();
        }
    }
}