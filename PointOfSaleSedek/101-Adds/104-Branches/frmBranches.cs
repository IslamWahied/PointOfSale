using DevExpress.XtraBars.Docking2010;
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
using EntityData;
using PointOfSaleSedek._102_MaterialSkin;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmBranches : DevExpress.XtraEditors.XtraForm
    {
        readonly  PointOfSaleEntities2 context = new PointOfSaleEntities2();
        public frmBranches()
        {
            InitializeComponent();
            fillGride();
        }


        void fillGride()
        {

            var result = context.Branches.Where(x => x.IsDeleted == 0).ToList();
           gcEmployeeCard.DataSource = result;

        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد")
            {
                frmAddBranches frm = new frmAddBranches();
                frm.ShowDialog();
            }
            else if (btn.Caption == "خروج")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف")
            {

                if (gvEmployeeCard.RowCount <= 0)
                {


                    return;
                }
                if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
                {


                    var x2 = gvEmployeeCard.GetFocusedRow() as Branch;


                    using (PointOfSaleEntities2 Contexts = new PointOfSaleEntities2())
                    {
                        var IfEmployee = Contexts.Employees.Any(x => x.Branch_ID == x2.Branches_Code && x.IsDeleted == 0);


                       if (IfEmployee)
                        {
                            MaterialMessageBox.Show("لا يمكن حذف هذا الفرع بسبب وجود موظفين مسجلين عليه", MessageBoxButtons.OK);

                            return;

                        }

                        else
                        {
                            Branch _Branch;
                            _Branch = Contexts.Branches.SingleOrDefault(Brn => Brn.Branches_Code == x2.Branches_Code && Brn.IsDeleted == 0);
                            _Branch.IsDeleted = 1;
                            Contexts.SaveChanges();
                            using (PointOfSaleEntities2 Contexts2 = new PointOfSaleEntities2())
                            {
                            var result = Contexts2.Branches.Where(x => x.IsDeleted == 0).ToList();
                            gcEmployeeCard.DataSource = result;

                            }
                            MaterialMessageBox.Show("تم الحذف", MessageBoxButtons.OK);


                        }

                    }


                }


            }
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}