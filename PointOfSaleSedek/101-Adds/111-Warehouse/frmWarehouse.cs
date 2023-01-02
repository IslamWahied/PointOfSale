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
    public partial class frmWarehouse : DevExpress.XtraEditors.XtraForm
    {
        readonly  POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmWarehouse()
        {
            InitializeComponent();
            AppLangu();
            fillGride();
        }
        public void AppLangu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            gridColumn12.Caption = st.isEnglish() ? "Branche Code" : "كود المخزن";  
            gridColumn3.Caption = st.isEnglish() ? "Branche Name" : "اسم المخزن";
            gvEmployeeCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "New" : "جديد";
            windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Delete" : "حذف";
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "New" : "جديد";
            materialContextMenuStrip1.Items[1].Text = st.isEnglish() ? "Edite" : "تعديل";
            materialContextMenuStrip1.Items[2].Text = st.isEnglish() ? "Delete" : "حذف";
        }


        void fillGride()
        {

            var result = context.Warehouses.Where(x => x.isDelete == 0).ToList();
           gcEmployeeCard.DataSource = result;

        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "جديد" || btn.Caption == "New")
            {
                frmAddWarehouse frm = new frmAddWarehouse();
                frm.ShowDialog();
            }
            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {

                this.Close();


            }
            else if (btn.Caption == "حذف" || btn.Caption == "Delete")
            {

                if (gvEmployeeCard.RowCount <= 0)
                {


                    return;
                }
                if (MaterialMessageBox.Show(st.isEnglish() ? "?Are you sure you want to delete the branch" : "تاكيد حذف المخزن؟", MessageBoxButtons.YesNo) == DialogResult.OK)
                {


                    var x2 = gvEmployeeCard.GetFocusedRow() as Warehouse;


                    using (POSEntity Contexts = new POSEntity())
                    {
                         var IfItem = Contexts.Item_History_transaction.Any(x => x.from_Warhouse_Code == x2.Warehouse_Code && x.IsDeleted == 0);


                        if (IfItem)
                        {

                            MaterialMessageBox.Show(st.isEnglish() ? "This Warehouse cannot be deleted because there are registered Item on it" : "لا يمكن حذف هذا المخزن بسبب وجود منتجات مسجله عليه", MessageBoxButtons.OK);
                            return;

                        }

                        else
                        {
                            Warehouse _Warehouse;
                            _Warehouse = Contexts.Warehouses.SingleOrDefault(Brn => Brn.Warehouse_Code == x2.Warehouse_Code && Brn.isDelete == 0);
                            _Warehouse.isDelete = 1;
                            Contexts.SaveChanges();
                            using (POSEntity Contexts2 = new POSEntity())
                            {
                                var result = Contexts2.Warehouses.Where(x => x.isDelete == 0).ToList();
                                gcEmployeeCard.DataSource = result;

                            }
                            MaterialMessageBox.Show(st.isEnglish() ? "Deleted Successfully" : "تم الحذف", MessageBoxButtons.OK);


                        }

                    }


                }


            }
        }

      
    }
}