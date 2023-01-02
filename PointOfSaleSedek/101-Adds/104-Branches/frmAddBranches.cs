using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataRep;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmAddBranches : DevExpress.XtraEditors.XtraForm
    {

        POSEntity Context = new POSEntity();
        readonly Static st = new Static();
        public frmAddBranches()
        {
            InitializeComponent();
            AppLangu();
         
            FormLoad();
            FillslkWarhouse();
        }



        public void FillslkWarhouse()
        {
            DataTable dt = new DataTable();
            var result = Context.Warehouses.Where(user => user.isDelete == 0).ToList();
            slkWarhouse.Properties.DataSource = result;
            slkWarhouse.Properties.ValueMember = "Warehouse_Code";
            slkWarhouse.Properties.DisplayMember = "Warehouse_Name";

        }
        public void AppLangu()
        {




            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Add Branche" : "اضافة فرع";

            labelControl7.Text = st.isEnglish() ? "Id" : "النسلسل";
            labelControl1.Text = st.isEnglish() ? "Branche Name" : "اسم الفرع";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";



            labelControl4.Text = st.isEnglish() ? "Warhouse" : "المخزن";

            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Cancel" : "اغلاق";





        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FormLoad()
        { 
        
        var Branches = Context.Branches.Select(x => x.IsDeleted == 0);
            Int64? MaxCode = Context.Branches.Max(u => (Int64?)u.Branches_Code + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }


            txtCode.Text = MaxCode.ToString();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmBranches>().Any())
            {

                bool isSameName = false;
                try
                {
                    isSameName = Context.Branches.Any(x => x.Branches_Name.Trim().ToLower() == txtName.Text.Trim().ToLower());
                }
                catch {

                    isSameName = false;
                }
             

                frmBranches frm = (frmBranches)Application.OpenForms["frmBranches"];

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {

                    MaterialMessageBox.Show(st.isEnglish() ? "Please enter the name of the branch" : "برجاء ادخال اسم الفرع", MessageBoxButtons.OK);
                    return;

                }
                else if (string.IsNullOrWhiteSpace(slkWarhouse.Text)) {

                    MaterialMessageBox.Show(st.isEnglish() ? "Please enter the name of the Warhouse" : "برجاء ادخال اسم المخزن", MessageBoxButtons.OK);
                    return;
                }


               

                else if (isSameName) {
                    MaterialMessageBox.Show(st.isEnglish() ? "The name of the branch has already been registered" : "تم تسجيل اسم الفرع سابقا", MessageBoxButtons.OK);
                    return;
                }


                Branch _Branch = new Branch()
                {
                    Branches_Code = Convert.ToInt64(txtCode.Text),
                    Warhouse_Code= Convert.ToInt64(slkWarhouse.EditValue),
                    
                    IsDeleted = 0,
         
                    Branches_Name = txtName.Text
                    


                };
                Context.Branches.Add(_Branch);
                Context.SaveChanges();


                using (POSEntity NewContext = new POSEntity())
                {

                    frm.fillGride();
                   
                    txtCode.Text = Convert.ToString(Convert.ToInt64(txtCode.Text) + 1);




                }

                this.Close();
            



            }

              





        }
    }
}