 
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
using DataRep;
using PointOfSaleSedek.HelperClass;
using System.Data.Entity.Validation;

namespace PointOfSaleSedek._0_Authentication
{
    public partial class FrmAuthentication : DevExpress.XtraEditors.XtraForm
    {
      private readonly POSEntity Db = new POSEntity();
        private readonly Static st = new Static();

        public FrmAuthentication()
        {
            InitializeComponent();
            langu();
            FillSlkUser();
        }


        void langu()
        {
          this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
     
            this.Text   = st.isEnglish() ? "Users Permations" : "صلاحيات المستخدمين";

            gridColumn3.Caption = st.isEnglish() ? "Permations" : "الصلاحيات";
            gridColumn2.Caption = st.isEnglish() ? "Name" : "الاسم";
            this.gridColumn3.FieldName = st.isEnglish() ? "Tab_Description_Eng" : "Tab_Description";
            BtnSave.Text = st.isEnglish() ? "Save" : "حفظ";
             BtnClose.Text = st.isEnglish() ? "Close" : "اغلاق";
             materialLabel4.Text = st.isEnglish() ? "UserName" : "اسم المستخدم";
 
          
        }



        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show(st.isEnglish()?"Are You Sure To Exit?":"تاكيد الاغلاق", MessageBoxButtons.YesNo) == DialogResult.OK)
            {

                this.Close();

            }

        }
        public void FillSlkUser()
        {
            var barchCode = st.GetBranch_Code();

            //DataTable dt = new DataTable();
            var result = Db.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0 && user.Branches_Code == barchCode).ToList();
            slkUser.Properties.DataSource = result;
            slkUser.Properties.ValueMember = "Employee_Code";
            slkUser.Properties.DisplayMember = "Employee_Name";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var branchCode = st.GetBranch_Code();
            try
         {
           SplashScreenManager.ShowForm(typeof(WaitForm1));
             var xxx = slkUser.Text;


             if (xxx.Length < 1)
             {
                 MaterialMessageBox.Show(st.isEnglish() ? "Please select a user" : "برجاء اختيار مستخدم", MessageBoxButtons.OK);

                 return;
             }

             var _USer_Code = Convert.ToInt64(slkUser.EditValue);
             
             bool TestUpdate = Db.User_Auth.Any(User_Auth => User_Auth.User_Code == _USer_Code && User_Auth.Branch_Code == branchCode && User_Auth.IsDeleted == 0);
                Db.SaveChanges();
                using (POSEntity Contexts = new POSEntity())
                {
                    if (TestUpdate)
                    {
                        ObjectParameter Message = new ObjectParameter("Message", typeof(string));
                      
                        Contexts.Delete_Trancation_By_User_Code(_USer_Code, branchCode, Message);
                        List<Tab_Info> GridData = GridControl1.DataSource as List<Tab_Info>;
                        List<User_Auth> List_User_Auth = new List<User_Auth>();
                        foreach (var Row in GridData.Where(x => x.InActive == true))
                        {


                            Int64? NewCode = Contexts.User_Auth.Max(u => (int?)u.Auth_Code);
                            if (string.IsNullOrEmpty(NewCode.ToString()))
                            {
                                NewCode = 0;
                            }

                            User_Auth _User_Auth = new User_Auth()
                            {

                                Tab_Info_Code = Row.Tab_Code,
                                User_Code = _USer_Code,
                                IsDeleted = 0,
                                Branch_Code = branchCode,

                                InActive = true,
                                Auth_Code = Convert.ToInt64(NewCode + 1)

                            };
                            List_User_Auth.Add(_User_Auth);


                        }
                        Contexts.User_Auth.AddRange(List_User_Auth);
                        Contexts.SaveChanges();
                        MaterialMessageBox.Show(st.isEnglish() ? "Modified Successfully" : "تم التعديل", MessageBoxButtons.OK);
                    }
                    else
                    {


                        List<Tab_Info> GridData = new List<Tab_Info>();
                        GridData = GridControl1.DataSource as List<Tab_Info>;

                        foreach (var Row in GridData)
                        {
                            if (Row.InActive == true)
                            {

                                Int64? NewCode = Contexts.User_Auth.Max(u => (int?)u.Auth_Code);
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
                                    Branch_Code = branchCode,




                                    Auth_Code = Convert.ToInt64(NewCode + 1)

                                };


                                Contexts.User_Auth.Add(_User_Auth);
                                Contexts.SaveChanges();

                            }
                        }
                        MaterialMessageBox.Show(st.isEnglish() ? "Saved Successfully" : "تم الحفظ", MessageBoxButtons.OK);
                    }

                    SplashScreenManager.CloseForm();
                }

                

         }
            //catch (Exception ee)
            //{

            //    SplashScreenManager.CloseForm();
            //}
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
                SplashScreenManager.CloseForm();
                throw;
            }

        }


        private void SlkUser_EditValueChanged(object sender, EventArgs e)
        {
            var branchCode = st.GetBranch_Code();
            Int64 UserId = Convert.ToInt64(slkUser.EditValue);
            var AllTabs = (from a in Db.Tab_Info where a.IsDeleted == 0 select a).ToList();
            var UserTabs = (from a in Db.User_Auth where a.IsDeleted == 0 && a.Branch_Code == branchCode && (a.User_Code == UserId) select a).ToList();
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
