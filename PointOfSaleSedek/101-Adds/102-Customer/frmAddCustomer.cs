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
        readonly SaleEntities context = new SaleEntities();
        readonly Static st = new Static();
        public frmAddCustomer()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FillSexSlk()
        {

           
                var result = context.SexTypes.ToList();
                slkSex.Properties.DataSource = result;
            slkSex.Properties.ValueMember = "SexTypeCode";
            slkSex.Properties.DisplayMember = "SexTypeName";

             
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
                MaterialMessageBox.Show("برجاء ادخال جميع الحقول", MessageBoxButtons.OK);
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
                MaterialMessageBox.Show("تم تسجل هذا العميل من قبل", MessageBoxButtons.OK);
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
                    SexTypeCode = Convert.ToInt32(slkSex.EditValue),
                    Last_Modified_User = st.User_Code(),
            };
                context.Customer_Info.Add(_customer);
                context.SaveChanges();

                //MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);


                if (st.Project_Type() == "Cafe")
                {

                    if (Application.OpenForms.OfType<frmCafeSales>().Any())
                    {


                        frmCafeSales frm = (frmCafeSales)Application.OpenForms["frmCafeSales"];

                        //frm.FillSlkCustomers();

                        //frm.slkCustomers.EditValue = context.Customer_View.FirstOrDefault(Customer => Customer.Customer_Phone == Phone).Customer_Code;
                        //frm.slkCustomers.Enabled = true;
                        //frm.btnCustomerHistory.Enabled = true;
                        //frm.txtParCode.Focus();
                        this.Close();



                    }
                }
                else if (st.Project_Type() == "Perfum")
                {
                    if (Application.OpenForms.OfType<frmPerfumSales>().Any())
                    {


                        frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];

                        Int64 User_Code = st.User_Code();


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

                       
                      

                        Int64 User_Code = st.User_Code();


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