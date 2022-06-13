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
using EntityData;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmInvoicePurchsSearch : DevExpress.XtraEditors.XtraForm
    {
        PointOfSaleEntities Context = new PointOfSaleEntities();
        public frmInvoicePurchsSearch()
        {
            InitializeComponent();
            FillGrid();
        }
        void FillGrid()
        {
            

            using (PointOfSaleEntities cont = new PointOfSaleEntities())
            {
                gcSaleMaster.DataSource = null;

                gcSaleMaster.DataSource = Context.SaleMasterViews.Where(x => x.Operation_Type_Id == 1 && x.IsDeleted == 0).ToList();
                gcSaleMaster.RefreshDataSource();
            }

            if (gvSaleMaster.RowCount == 0)
            {

                groupControl1.Enabled = false;

            }




        }

        private void groupControl1_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "معاينة")
            {
                if (Application.OpenForms.OfType<frmPurchasescs>().Any())
                {
                    if (gvSaleMaster.RowCount <= 0)
                    {

                        MaterialMessageBox.Show("!لا يوجد اوردرات للمعاينة", MessageBoxButtons.OK);
                        return;


                    }

                    frmPurchasescs frm = (frmPurchasescs)Application.OpenForms["frmPurchasescs"];
                    var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
                    Int64 SaleMasterCode = FocusRow.SaleMasterCode;

                    frm.gcItemCard.DataSource = null;
                    frm.gcItemCard.RefreshDataSource();
                    frm.gcItemCard.DataSource = Context.SaleDetailViews.Where(x => x.SaleMasterCode == SaleMasterCode && x.IsDeleted == 0 && x.Operation_Type_Id == 1).ToList();
                    frm.Status = "Old";
                    frm.groupControl1.CustomHeaderButtons[1].Properties.Caption = SaleMasterCode.ToString();
                    this.Close();
                }

            }
        }

        private void الغاءالفاتورةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FocusRow = gvSaleMaster.GetFocusedRow() as SaleMasterView;
            var Item_History_List = Context.Item_History.Where(x => x.Sale_Master_Code == FocusRow.SaleMasterCode).ToList();
            foreach (var item in Item_History_List)
            {
                if (item.Is_Used == true || item.IsFinshed == true)
                {
                    MaterialMessageBox.Show("لايمكن حذف  الفاتوره بسبب حدوث عمليات بيع عليها", MessageBoxButtons.OK);

                    return;
                }
            }

            SaleMaster _SaleMaster;
            _SaleMaster = Context.SaleMasters.SingleOrDefault(shft => shft.Operation_Type_Id == 1 && shft.ShiftCode == FocusRow.Shift_Code && shft.IsDeleted == 0 && shft.UserCode == FocusRow.UserCode && shft.EntryDate == FocusRow.EntryDate && shft.SaleMasterCode == FocusRow.SaleMasterCode);
            _SaleMaster.Operation_Type_Id = 6;
            _SaleMaster.LastDateModif = DateTime.Now;
            Context.SaveChanges();



            using (PointOfSaleEntities context2 = new PointOfSaleEntities())
            {
                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == FocusRow.SaleMasterCode && w.EntryDate == FocusRow.EntryDate && w.Operation_Type_Id == 1 && w.IsDeleted == 0);
                context2.SaleDetails.RemoveRange(Details);
                context2.SaveChanges();
            }



            FillGrid();






        }
    }
}