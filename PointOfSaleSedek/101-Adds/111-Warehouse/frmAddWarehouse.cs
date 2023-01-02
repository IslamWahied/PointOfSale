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
    public partial class frmAddWarehouse : DevExpress.XtraEditors.XtraForm
    {

        POSEntity Context = new POSEntity();
        readonly Static st = new Static();
        public frmAddWarehouse()
        {
            InitializeComponent();
            AppLangu();
            FormLoad();
        }




        public void AppLangu()
        {




            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? "Add Warehouse" : "اضافة مخزن";

            labelControl7.Text = st.isEnglish() ? "Id" : "النسلسل";
            labelControl4.Text = st.isEnglish() ? "Branche Name" : "اسم المخزن";

            btnAdd.Text = st.isEnglish() ? "Add" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Cancel" : "اغلاق";





        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FormLoad()
        { 
        
        var Branches = Context.Warehouses.Select(x => x.isDelete == 0);
            Int64? MaxCode = Context.Warehouses.Where(x => x.isDelete == 0).Max(u => (Int64?)u.Warehouse_Code + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }


            txtCode.Text = MaxCode.ToString();
            txtName.Focus();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<frmWarehouse>().Any())
            {

                frmWarehouse frm = (frmWarehouse)Application.OpenForms["frmWarehouse"];

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {

                    MaterialMessageBox.Show(st.isEnglish() ? "Please enter the name of the branch" :"برجاء ادخال اسم المخزن", MessageBoxButtons.OK);
                    return;

                }

                var isSameName = Context.Warehouses.Any(x=> x.Warehouse_Name.ToLower() == txtName.Text.ToLower() );

                if (isSameName)
                {
                    MaterialMessageBox.Show(st.isEnglish() ? "This name has been used" : "تم استخدام هذا الاسم من قبل", MessageBoxButtons.OK);
                    return;
                }
                else {

                    Warehouse _Branch = new Warehouse()
                    {
                        Warehouse_Code = Convert.ToInt64(txtCode.Text),
                        Warehouse_Name = txtName.Text


                    };
                    Context.Warehouses.Add(_Branch);
                    Context.SaveChanges();


                    using (POSEntity NewContext = new POSEntity())
                    {

                        frm.gcEmployeeCard.DataSource = NewContext.Warehouses.Where(x => x.isDelete == 0).ToList();
                        frm.gcEmployeeCard.RefreshDataSource();
                        txtCode.Text = Convert.ToString(Convert.ToInt64(txtCode.Text) + 1);
                        txtName.Text = "";
                        txtName.Focus();
                        this.Close();




                    }

                }
              


            



            }

              





        }
    }
}