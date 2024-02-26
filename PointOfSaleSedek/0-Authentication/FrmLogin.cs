 
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
using PointOfSaleSedek._Activaion;


namespace PointOfSaleSedek._0_Authentication
{

    public partial class FrmLogin : Form 
    {
     readonly POSEntity Context = new POSEntity();
        public string CPU_Code { get; set; }
      readonly Static st = new Static();
        //FirestoreDb db;
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


            AppActivation appActivation = Context.AppActivations.FirstOrDefault();
            labelControl5.Text = appActivation.AppVersion;
            if (appActivation.ActivaionState && appActivation.Activation_Date.Value > DateTime.Now)
            {
                
                labelControl6.Text = "Active";
                labelControl6.BackColor = Color.Green;
            }

            else {
                labelControl6.Text = "Inactive";
                labelControl6.BackColor = Color.Red;
            }

           
    
            
        

            
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
        public static DateTime? GetDateTime()
        {
            DateTime dateTime = DateTime.MinValue;
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            try
            {
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string todaysDates = response.Headers["date"];

                    dateTime = DateTime.ParseExact(todaysDates, "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                        System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat, System.Globalization.DateTimeStyles.AssumeUniversal);
                }
                SplashScreenManager.CloseForm();
            }
            catch (Exception)
            {
                SplashScreenManager.CloseForm();
                return null;
            }        
            return dateTime;
        }

        void login() {

            bool CheckUser;
            try
            {
                var interNetDate = GetDateTime();
                DateTime? dateTimeNow;

                if (interNetDate == null) {
                    dateTimeNow = DateTime.Now;

                } else
                {

                    dateTimeNow = interNetDate;
                }


                using (POSEntity Context1 = new POSEntity())
                {
                    AppActivation appActivationT = Context1.AppActivations.FirstOrDefault();
                    if (interNetDate == null && appActivationT.Login_Offline_Count <= 29)
                    {

                        appActivationT.Login_Offline_Count = appActivationT.Login_Offline_Count + 1;
                        Context1.SaveChanges();
                    }
                    else if (interNetDate != null)
                    {

                        appActivationT.Login_Offline_Count = 0;
                        Context1.SaveChanges();
                    }
                }

                AppActivation appActivation;
                using (POSEntity Context  = new POSEntity())
                {
                      appActivation = Context.AppActivations.FirstOrDefault();
                }
        
                var mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
                var mbsList = mbs.Get();

                foreach (ManagementObject mo in mbsList)
                {
                    CPU_Code = mo["ProcessorID"].ToString();
                }

                if (appActivation.CPU_Code != CPU_Code)
                {
                    MaterialMessageBox.Show(radioBtnEnglish.Checked ? "The point has not been activated before, please refer to technical support" : "لم يتم تفعيل النقطه من قبل برجاء الرجوع الي الدعم الفني", MessageBoxButtons.OK);
                    return;
                }

                if (appActivation.Login_Offline_Count >= 30) {
                    MaterialMessageBox.Show(radioBtnEnglish.Checked ? "Please connect to the internet" : "يرجي الاتصال بالانترنت", MessageBoxButtons.OK);
                    return;
                }

                if (!appActivation.ActivaionState || appActivation.Activation_Date.Value < dateTimeNow)
                {

                    using (POSEntity Context3 = new POSEntity())
                    {

                        AppActivation appActivationL = Context3.AppActivations.FirstOrDefault();
                        appActivationL.ActivaionState = false;
                        Context3.SaveChanges();
                        labelControl6.Text = "Inactive";
                        labelControl6.BackColor = Color.Red;
                       
                    }

                    MaterialMessageBox.Show(radioBtnEnglish.Checked ? "The period allowed for the copy has expired, please refer to technical support" : "تم انتهاء الفتره المسموحه للنسخة برجاء الرجوع الي الدعم الفني", MessageBoxButtons.OK);
                    return;
                }

                
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


                CheckUser = Context.User_View.Any(Users => Users.Branches_Code == branchCode && Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text && Users.IsDeleted == 0 && Users.IsDeletedEmployee == 0);
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
                    var _printCopyNumber = Context.PrintCopyNumbers.SingleOrDefault();
                    st.SetPrintCopyNumber(_printCopyNumber.PrintCopyNumber1);
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

        private void radioBtnEnglish_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.A)
            {
                frmActivationLogin frm = new frmActivationLogin();
                frm.ShowDialog();
            }
        }

        private void txtUserName_Click(object sender, EventArgs e)
        {

        }
    }
}
