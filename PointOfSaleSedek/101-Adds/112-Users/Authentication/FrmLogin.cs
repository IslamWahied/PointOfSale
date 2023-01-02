 
using PointOfSaleSedek._101_Adds;
using PointOfSaleSedek._102_MaterialSkin;
using DevExpress.Utils;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;
using DataRep;
using Google.Cloud.Firestore;
using System.Data.Entity.Validation;

namespace PointOfSaleSedek._101_Adds._103_Authentication
{

    public partial class FrmLogin : Form 
    {
     readonly POSEntity Context = new POSEntity();
        public string CPU_Code { get; set; }
      readonly Static st = new Static();
        FirestoreDb db;
        public FrmLogin()
        {
            InitializeComponent();
            radioBtnArbc.Checked = false;
            radioBtnEnglish.Checked = true;
            labelControl1.Text = "        UserName ";
            labelControl2.Text = "Password";
            labelControl3.Text = "Login";
            btnLogin.Text = "Login";
            simpleButton2.Text = "Close";


            st.ChangeLangu(true);

            this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;

            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            string path = AppDomain.CurrentDomain.BaseDirectory + @"foodapp.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
            db = FirestoreDb.Create("pointofsale-d3e8d");
          //  login();


        }

        
        private void TxtuserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }


        void login() {

            bool CheckUser;
            try
            {




                //int ordersNumber = Context.SaleMasterViews.ToList().Count;




                //if (ordersNumber >= 200)
                //{
                //    MaterialMessageBox.Show("تم انتهاء الفتره المسموحه للنسخة النجريبية", MessageBoxButtons.OK);
                //    return;
                //}

                CheckUser = Context.User_View.Any(Users => Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text && Users.IsDeleted == 0 && Users.IsDeletedEmployee == 0);

                
                String branchName = "BackOffice";
                Int64 branchCode = 0;
                Int64 wareHouseCode = 0;

                bool isBackOffice = Context.Branches.Any(x => x.Is_Back_Office && x.IsDeleted == 0);
                if (!isBackOffice)
                {
                    var branch = Context.Branches.Where(x => x.IsDeleted == 0 ).FirstOrDefault();

                    branchCode = branch.Branches_Code;
                    branchName = branch.Branches_Name;
                    wareHouseCode = branch.Warhouse_Code;

                }
                

            
                if (CheckUser)
                {
                    this.Hide();

                   

                    User_View _User;
                    _User = Context.User_View.SingleOrDefault(user => user.Password == txtPassword.Text && user.UserName == txtUserName.Text && user.IsDeleted == 0 && user.Branches_Code == branchCode);

                    st.SetUser_Code(Convert.ToInt64(_User.Employee_Code));
                    st.SetBranch_Code(branchCode);
                    st.SetBranch_Name(branchName);
                    st.Set_Warehouse_Code(wareHouseCode);
                    st.SetUserName(_User.UserName);
                    st.ChangeLangu(this.radioBtnEnglish.Checked);
                    //Int64 Project_Code = Convert.ToInt64(SlkProjectCity.EditValue);
                    // st.Set_Project_Code(Project_Code);


                    //SplashScreenManager.ShowForm(typeof(SplashScreen1));
                    //Timer d = new Timer();
                    //d.Start();

                    //wait(3000);
                    //st.SplashScreen1.Hide();
                    FrmMain frm = new FrmMain();
                    frm.ShowDialog();





                }
                else
                {
                    MaterialMessageBox.Show(radioBtnEnglish.Checked ? "You don't have an account!" : "!ليس لديك حساب", MessageBoxButtons.OK);
                    return;


                }
            }
            catch (DbEntityValidationException es)
            {
                foreach (var eve in es.EntityValidationErrors)
                {

                    String dd = "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" + eve.Entry.Entity.GetType().Name.ToString() + eve.Entry.State.ToString();

                    foreach (var ve in eve.ValidationErrors)
                    {

                        String ddddd = "Property: \"{0}\", Error: \"{1}\"" + ve.PropertyName.ToString() + ve.ErrorMessage.ToString();




                    }
                }
                throw;
            }


        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {


            login();




        }
       
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void slkShiftsOpen_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void radioBtnArbc_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnArbc.Checked) {
                st.ChangeLangu(false);
                this.RightToLeftLayout = false;
                radioBtnEnglish.Checked = false;
                 labelControl1.Text = "اسم المستخدم";
                
                 labelControl2.Text = "كلمة السر";
                 labelControl3.Text = "تسجيل الدخول";
                btnLogin.Text = "دخول";
                simpleButton2.Text = "اغلاق";

                this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;

               

            }
        }

        private void radioBtnEnglish_CheckedChanged(object sender, EventArgs e)
        {
            if (radioBtnEnglish.Checked)
            {
               // this.RightToLeftLayout = true;
           //    this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
                radioBtnArbc.Checked = false;
                labelControl1.Text = "        UserName ";
                 labelControl2.Text = "Password";
                 labelControl3.Text = "Login";
                btnLogin.Text = "Login";
                simpleButton2.Text = "Close";

                st.ChangeLangu(true);

                this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.No;
       
                this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;

            }
        }

       
    }
}
