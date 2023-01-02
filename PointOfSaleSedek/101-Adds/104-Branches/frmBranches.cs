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
using DataRep;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmBranches : DevExpress.XtraEditors.XtraForm
    {
        readonly  POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmBranches()
        {
            InitializeComponent();
            AppLangu();
            fillGride();
        }
        public void AppLangu()
        {




            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

            gridColumn12.Caption = st.isEnglish() ? "Branche Code" : "كود الفرع";  
            gridColumn3.Caption = st.isEnglish() ? "Branche Name" : "اسم الفرع";
            gridColumn1.Caption = st.isEnglish() ? "Warehouse" : "المخزن";
            gvEmployeeCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "New" : "جديد";
            windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Delete" : "حذف";
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "New" : "جديد";
            //materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Edite" : "تعديل";
            materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Delete" : "حذف";





        }


       public void fillGride()
        {

            using (POSEntity Contexts2 = new POSEntity())
            {
                var result = Contexts2.warhouse_view.Where(x => x.IsDeleted == 0 && x.isDelete == 0).ToList();
                gcEmployeeCard.DataSource = result;
                 gcEmployeeCard.RefreshDataSource();

            }


          
          

        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد" || btn.Caption == "New")
            {
                frmAddBranches frm = new frmAddBranches();
                frm.ShowDialog();
            }
            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف" || btn.Caption == "Delete")
            {

                Delete();


            }
        }

        private void اضافةصنفجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddBranches frm = new frmAddBranches();
            frm.ShowDialog();
        }


        void Delete() {
            if (gvEmployeeCard.RowCount <= 0)
            {


                return;
            }
            if (MaterialMessageBox.Show(st.isEnglish() ? "Are you sure you want to delete the branch?" : "تاكيد حذف الفرع؟", MessageBoxButtons.YesNo) == DialogResult.OK)
            {


                var x2 = gvEmployeeCard.GetFocusedRow() as warhouse_view;


                using (POSEntity Contexts = new POSEntity())
                {
                    var IfEmployee = Contexts.Employees.Any(x => x.Branch_ID == x2.Branches_Code && x.IsDeleted == 0);


                    if (IfEmployee)
                    {
                        MaterialMessageBox.Show(st.isEnglish() ? "This branch cannot be deleted because there are registered employees on it" : "لا يمكن حذف هذا الفرع بسبب وجود موظفين مسجلين عليه", MessageBoxButtons.OK);

                        return;

                    }

                    else
                    {
                        Branch _Branch;
                        _Branch = Contexts.Branches.SingleOrDefault(Brn => Brn.Branches_Code == x2.Branches_Code && Brn.IsDeleted == 0);
                        _Branch.IsDeleted = 1;
                        Contexts.SaveChanges();
                        fillGride();
                        MaterialMessageBox.Show(st.isEnglish() ? "Deleted Successfully" : "تم الحذف", MessageBoxButtons.OK);


                    }

                }


            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Delete();
        }
    }
}