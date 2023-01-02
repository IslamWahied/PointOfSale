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

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmAdmin : DevExpress.XtraEditors.XtraForm
    {
        public string Status = "New";
        readonly Static st = new Static();

        bool isAdmin = false;
        public string authName = "";

        readonly POSEntity context = new POSEntity();
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            if (st.Project_Type() == "Cafe")
            {
                

                if (Application.OpenForms.OfType<frmCafeSales>().Any())
                {


                    frmCafeSales frm = (frmCafeSales)Application.OpenForms["frmCafeSales"];

                    Int64 User_Code = st.GetUser_Code();

                    var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



                    if (result.Any(xd => xd.Tab_Name == "btnser"))
                    {
                        //frm.btnser.Enabled = true;

                    }
                    else
                    {
                        //frm.btnser.Enabled = false;
                    }


                    if (result.Any(xd => xd.Tab_Name == "btnDiscount"))
                    {
                        frm.btnDiscount.Enabled = true;

                    }
                    else
                    {
                        frm.btnDiscount.Enabled = false;
                    }

                    this.Close();


                }
            }
            else if (st.Project_Type() == "Perfum")
            {


                bool isFind = context.User_View.Any(Users => Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text && Users.IsDeleted == 0 && Users.IsDeletedEmployee == 0);

                if (isFind)
                {


                   var AdminCode = context.User_View.Where(Users => Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text && Users.IsDeleted == 0 && Users.IsDeletedEmployee == 0).First().Employee_Code;

                    Int64 adminCode = Convert.ToInt64(AdminCode);
                    isAdmin = context.Auth_View.Any(View => View.User_Code == adminCode && View.User_IsDeleted == 0 && View.Tab_Name == authName);


                    if (isAdmin)
                    {

                        if (authName == "btnser")
                        {
                            this.Close();

                            frmPerfumInvoiceSearch frm = new frmPerfumInvoiceSearch();
                            frm.ShowDialog();
                        }
                        else if (authName == "btnDiscount")
                        {
                            this.Close();
                            frmPerfumDiscount frm = new frmPerfumDiscount();

                            frm.ShowDialog();

                        }

                        //else if(authName)



                    }
                    else
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "You do not have permission" : "ليس لديك صلاحية", MessageBoxButtons.OK);
                        return;
                    }
                }
                else {
                    MaterialMessageBox.Show(st.isEnglish() ? "You do not have permission" :"ليس لديك صلاحية", MessageBoxButtons.OK);
                    return;

                }
              


                //if (Application.OpenForms.OfType<frmCafeSales>().Any())
                //{


                //    frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];

                //    Int64 User_Code = st.GetUser_Code();

                //    var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



                //    if (result.Any(xd => xd.Tab_Name == "btnser"))
                //    {
                //        //frm.btnser.Enabled = true;

                //    }
                //    else
                //    {
                //        //frm.btnser.Enabled = false;
                //    }


                //    if (result.Any(xd => xd.Tab_Name == "btnDiscount"))
                //    {
                //        frm.btnDiscount.Enabled = true;

                //    }
                //    else
                //    {
                //        frm.btnDiscount.Enabled = false;
                //    }

                //    this.Close();


                //}
            }
            else if (st.Project_Type() == "SuperMarket")
            {
                if (Application.OpenForms.OfType<frmSuperMarketSales>().Any())
                {


                    frmSuperMarketSales frm = (frmSuperMarketSales)Application.OpenForms["frmSuperMarketSales"];

                    Int64 User_Code = st.GetUser_Code();

                    var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



                    if (result.Any(xd => xd.Tab_Name == "btnser"))
                    {
                        //frm.btnser.Enabled = true;

                    }
                    else
                    {
                        //frm.btnser.Enabled = false;
                    }


                    if (result.Any(xd => xd.Tab_Name == "btnDiscount"))
                    {
                        frm.btnDiscount.Enabled = true;

                    }
                    else
                    {
                        frm.btnDiscount.Enabled = false;
                    }

                    this.Close();


                }
            }



           




                //bool CheckUser;
                //try
                //{
                //    CheckUser = context.User_View.Any(Users => Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text);
                //    if (CheckUser)
                //    {
                //        var User = context.User_View.Where(Users => Users.UserName == txtUserName.Text && Users.UserFlag == true && Users.Password == txtPassword.Text).FirstOrDefault();
                //        var result = context.Auth_View.Any(View => View.User_Code == User.Employee_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnDiscount"));
                //        if (result == true)
                //        {
                //            this.Close();
                //            frmDiscount frm = new frmDiscount();
                //            frm.ShowDialog();

                //        }
                //        else
                //        {
                //            MaterialMessageBox.Show(" ليس لديك صلاحية", MessageBoxButtons.OK);
                //            return;

                //        }

                //    }
                //    else
                //    {
                //        MaterialMessageBox.Show(" ليس لديك حساب", MessageBoxButtons.OK);
                //        return;

                //    }
                //}
                //catch 
                //{


                //}

            }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            var UserId = Convert.ToInt64(st.GetUser_Code());
            var Check = context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
            if (!Check)
            {

                if (st.Project_Type() == "Cafe")
                {
                    frmCafeSales obj = (frmCafeSales)Application.OpenForms["frmCafeSales"];
                    obj.Close();
                    
                }
                else if (st.Project_Type() == "Perfum")
                {
                    frmPerfumSales obj = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];
                    obj.Close();
                     
                }
                else if (st.Project_Type() == "SuperMarket")
                {
                    frmSuperMarketSales obj = (frmSuperMarketSales)Application.OpenForms["frmSuperMarketSales"];
                    obj.Close();
                }

               

            }

        }
    }
}