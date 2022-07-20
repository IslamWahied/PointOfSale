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

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmAddBranches : DevExpress.XtraEditors.XtraForm
    {

        SaleEntities Context = new SaleEntities(); 
        public frmAddBranches()
        {
            InitializeComponent();
            FormLoad();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void FormLoad()
        { 
        
        var Branches = Context.Branches.Select(x => x.IsDeleted == 0);
            Int64? MaxCode = Context.Branches.Where(x => x.IsDeleted == 0).Max(u => (Int64?)u.Branches_Code + 1);
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
              
                frmBranches frm = (frmBranches)Application.OpenForms["frmBranches"];

                if (string.IsNullOrWhiteSpace(txtName.Text))
                {

                    MaterialMessageBox.Show("برجاء ادخال اسم الفرع", MessageBoxButtons.OK);
                    return;

                }


                Branch _Branch = new Branch()
                {
                    Branches_Code = Convert.ToInt64(txtCode.Text),
                    Branches_Name = txtName.Text


                };
                Context.Branches.Add(_Branch);
                Context.SaveChanges();


                using (SaleEntities NewContext = new SaleEntities())
                {

                    frm.gcEmployeeCard.DataSource = NewContext.Branches.Where(x => x.IsDeleted == 0).ToList();
                    frm.gcEmployeeCard.RefreshDataSource();
                    txtCode.Text = Convert.ToString(Convert.ToInt64(txtCode.Text) + 1);




                }


            



            }

              





        }
    }
}