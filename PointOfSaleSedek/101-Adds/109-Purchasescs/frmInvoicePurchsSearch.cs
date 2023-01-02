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
using DataRep;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmInvoicePurchsSearch : DevExpress.XtraEditors.XtraForm
    {
        POSEntity Context = new POSEntity();
        Static st = new Static();
        public frmInvoicePurchsSearch()
        {
            InitializeComponent();
            langu();
            FillGrid();
        }

        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.gridColumn1.Caption = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
            this.gridColumn2.Caption = st.isEnglish() ? "Date" : "التاريخ";

            this.gridColumn3.Caption = st.isEnglish() ? "Total" : "الاجمالي";
            this.gridColumn3.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
           new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "FinalTotal",  st.isEnglish()?"Total = {0:N}": "الاجمالي = {0:N}")});
            this.gridColumn4.Caption = st.isEnglish() ? "Created by" : "البائع";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "Cancel This Invoice" : "اللغاء الفاتورة";
            this.groupControl1.CustomHeaderButtons[0].Properties.Caption = st.isEnglish() ? "Preview" : "معاينة";
            this.groupControl1.Text = st.isEnglish() ? "invoice number" : "رقم الفاتورة";
    




        }
        void FillGrid()
        {
            

            using (POSEntity cont = new POSEntity())
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
            if (e.Button.Properties.Caption == "معاينة" || e.Button.Properties.Caption == "Preview")
            {
                if (Application.OpenForms.OfType<frmPurchasescs>().Any())
                {
                    if (gvSaleMaster.RowCount <= 0)
                    {

                        MaterialMessageBox.Show(st.isEnglish()?"There are no orders for viewing !":"!لا يوجد اوردرات للمعاينة", MessageBoxButtons.OK);
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
                bool isTran =   Context.Item_History_transaction.Any(x => x.SaleMasterCode == FocusRow.SaleMasterCode && x.IsDeleted == 0 &&x.Item_History_Id == item.Id && (x.from_Warhouse_Code == item.Warhouse_Code || x.Warhouse_Transfer_Code == item.Warhouse_Code));
                if (item.Is_Used == true || item.IsFinshed == true || isTran)
                {
                    MaterialMessageBox.Show(st.isEnglish()?"The invoice cannot be deleted due to the occurrence of sales on it":"لايمكن حذف  الفاتوره بسبب حدوث عمليات بيع عليها", MessageBoxButtons.OK);

                    return;
                }

                


            }



            using (POSEntity context3 = new POSEntity())
            {
                SaleMaster _SaleMaster;
                _SaleMaster = context3.SaleMasters.SingleOrDefault(shft => shft.Operation_Type_Id == 1 && shft.ShiftCode == FocusRow.Shift_Code && shft.IsDeleted == 0 && shft.UserCode == FocusRow.UserCode &&
                shft.EntryDate == FocusRow.EntryDate && shft.SaleMasterCode == FocusRow.SaleMasterCode);
                _SaleMaster.Operation_Type_Id = 6;
                _SaleMaster.LastDateModif = DateTime.Now;
                context3.SaveChanges();

            }
            
            using (POSEntity context2 = new POSEntity())
            {
                Int64 cn = FocusRow.SaleMasterCode;
                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == cn && w.Operation_Type_Id == 1 && w.IsDeleted == 0 ).ToList();
                context2.SaleDetails.RemoveRange(Details);
                context2.SaveChanges();
            }


            using (POSEntity context4 = new POSEntity())
            {
                Int64 cn = FocusRow.SaleMasterCode;
                var Details = context4.Item_History.Where(w => w.Sale_Master_Code == cn ).ToList();
                context4.Item_History.RemoveRange(Details);
                context4.SaveChanges();
            }



            FillGrid();






        }
    }
}