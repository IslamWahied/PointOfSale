 
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
using EntityData;

namespace PointOfSaleSedek._101_Adds._103_Authentication
{

    public partial class FrmLogin : Form 
    {
     readonly PointOfSaleEntities2 Context = new PointOfSaleEntities2();
        public string CPU_Code { get; set; }
      readonly Static st = new Static();
        public FrmLogin()
        {
            InitializeComponent();
     

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


        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            bool CheckUser;
            try
            {
                CheckUser = Context.User_View.Any(Users => Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text&&Users.IsDeleted == 0&& Users.IsDeletedEmployee == 0);
                if (CheckUser)
                {

                    User _User;
                    _User = Context.Users.SingleOrDefault(user => user.Password == txtPassword.Text && user.UserName == txtUserName.Text && user.IsDeleted == 0);
                    this.Hide();
                    st.UserName(_User.Emp_Code);
                    //Int64 Project_Code = Convert.ToInt64(SlkProjectCity.EditValue);
                    // st.Set_Project_Code(Project_Code);
                    this.Hide();
                    // SplashScreenManager.ShowForm(typeof(SplashScreen1));
                    //Timer d = new Timer();
                    //d.Start();
                    FrmMain frm = new FrmMain();
                    frm.ShowDialog();



                }
                else
                {
                    MaterialMessageBox.Show("!ليس لديك حساب", MessageBoxButtons.OK);
                    return;


                }
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
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

        
    }
}
