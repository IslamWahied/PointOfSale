 
using PointOfSaleSedek._102_MaterialSkin;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityData;

namespace PointOfSaleSedek._101_Adds
{
    public partial class FrmAuthentication : MaterialSkin.Controls.MaterialForm 
    {
      private readonly PointOfSaleEntities2 Db = new PointOfSaleEntities2();
        //private readonly Static St = new Static();

        public FrmAuthentication()
        {
            InitializeComponent();
            FillSlkUser();
        } 
        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show("تاكيد الاغلاق", MessageBoxButtons.YesNo) == DialogResult.OK)
            {

                this.Close();

            }

        }
        public void FillSlkUser()
        {
            //DataTable dt = new DataTable();
            var result = Db.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();
            slkUser.Properties.DataSource = result;
            slkUser.Properties.ValueMember = "Employee_Code";
            slkUser.Properties.DisplayMember = "Employee_Name";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           try
         {
           SplashScreenManager.ShowForm(typeof(WaitForm1));
             var xxx = slkUser.Text;


             if (xxx.Length < 1)
             {
                 MaterialMessageBox.Show("برجاء اختيار مستخدم", MessageBoxButtons.OK);

                 return;
             }

             var _USer_Code = Convert.ToInt64(slkUser.EditValue);
             bool TestUpdate = Db.User_Auth.Any(User_Auth => User_Auth.User_Code == _USer_Code);
             if (TestUpdate)
             {
                 ObjectParameter Message = new ObjectParameter("Message", typeof(string));
                 Db.Delete_Trancation_By_User_Code(_USer_Code, Message);
                 List<Tab_Info> GridData = GridControl1.DataSource as List<Tab_Info>;
                 List<User_Auth> List_User_Auth = new List<User_Auth>();
                 foreach (var Row in GridData.Where(x => x.InActive == true))
                 {


                     Int64? NewCode = Db.User_Auth.Max(u => (int?)u.Auth_Code);
                     if (string.IsNullOrEmpty(NewCode.ToString()))
                     {
                         NewCode = 0;
                     }

                     User_Auth _User_Auth = new User_Auth()
                     {

                         Tab_Info_Code = Row.Tab_Code,
                         User_Code = _USer_Code,
                         IsDeleted = 0,
                         InActive = true,
                         Auth_Code = Convert.ToInt64(NewCode + 1)

                     };
                     List_User_Auth.Add(_User_Auth);


                 }
                 Db.User_Auth.AddRange(List_User_Auth);
                 Db.SaveChanges();
                 MaterialMessageBox.Show("تم التعديل", MessageBoxButtons.OK);
             }
             else
             {


                 List<Tab_Info> GridData = new List<Tab_Info>();
                 GridData = GridControl1.DataSource as List<Tab_Info>;

                 foreach (var Row in GridData)
                 {
                     if (Row.InActive == true)
                     {

                         Int64? NewCode = Db.User_Auth.Max(u => (int?)u.Auth_Code);
                         if (string.IsNullOrEmpty(NewCode.ToString()))
                         {
                             NewCode = 0;
                         }

                         User_Auth _User_Auth = new User_Auth()
                         {

                             Tab_Info_Code = Row.Tab_Code,
                             User_Code = _USer_Code,
                             IsDeleted = 0,
                             InActive = true,
                             Auth_Code = Convert.ToInt64(NewCode + 1)

                         };


                         Db.User_Auth.Add(_User_Auth);
                         Db.SaveChanges();

                     }
                 }
                 MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);
             }

             SplashScreenManager.CloseForm();

         }
         catch
         {

                SplashScreenManager.CloseForm();

            }
        }

        private void SlkUser_EditValueChanged(object sender, EventArgs e)
        {
            Int64 UserId = Convert.ToInt64(slkUser.EditValue);
            var AllTabs = (from a in Db.Tab_Info where a.IsDeleted == 0 select a).ToList();
            var UserTabs = (from a in Db.User_Auth where a.IsDeleted == 0    && (a.User_Code == UserId) select a).ToList();
            List<Tab_Info> x = new List<Tab_Info>();
            foreach (var altab in AllTabs)
            {
                foreach (var Usertab in UserTabs)
                {
                    if (Usertab.Tab_Info_Code == altab.Tab_Code && Usertab.User_Code == UserId)
                    {
                        altab.InActive = true;
                        x.Add(altab);

                    }
                }
            }
            foreach (var h in AllTabs)
            {
                bool TestUpdate = x.Any(sales => sales.Tab_Code == h.Tab_Code);

                if (!TestUpdate)
                {
                    h.InActive = false;
                    x.Add(h);
                }
            }
            GridControl1.DataSource = x;
            GridControl1.RefreshDataSource();
        }

        
    }
}
