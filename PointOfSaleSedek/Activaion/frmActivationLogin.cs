
 
 
using DataRep;
using MaterialSkin;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek._Activaion._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
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

namespace PointOfSaleSedek._Activaion
{

    public partial class frmActivationLogin : MaterialSkin.Controls.MaterialForm, IBase
    {
        readonly POSEntity Context = new POSEntity();
        Static st = new Static();
        public frmActivationLogin()
        {
            InitializeComponent();
            txtuserName.Properties.UseSystemPasswordChar = true;


        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Fillgrid()
        {
            throw new NotImplementedException();
        }

        public void GetById()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {


             
            bool CheckUser = Context.AppActivations.Any(Users => Users.AdminUserName == txtuserName.Text && Users.IsDeleted == 0 && Users.AdminPassowrd == txtPassword.Text);
            if (CheckUser)
            {
                 
                frmActivate frm = new frmActivate();
                frm.ShowDialog();
            }
            else
            {
                MaterialMessageBox.Show("خطاء في اسم المستخدم او كلمة المرور", MessageBoxButtons.OK);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtuserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }
        }



        private void txtuserName_KeyDown(object sender, KeyEventArgs e)
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
    }
}
