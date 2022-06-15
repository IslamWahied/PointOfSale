 
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
using DevExpress.XtraBars.Docking2010;
using EntityData;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmStorge : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities2 context = new PointOfSaleEntities2();
        public frmStorge()
        {
            InitializeComponent();
          
           

            FillGride();

            //if (gvItemCard.RowCount <= 0)
            //{

            //    gcItemCard.Enabled = false;
            //    //groupControl1.CustomHeaderButtons[0].Properties.Enabled = true;
            //    //groupControl1.CustomHeaderButtons[1].Properties.Enabled = false;
            //    //groupControl1.CustomHeaderButtons[2].Properties.Enabled = false;
            //}
        }

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "خروج")
            {


                if (MaterialMessageBox.Show("تاكيد الاغلاق", MessageBoxButtons.YesNo) == DialogResult.OK)
                {

                    this.Close();

                }


            }
        }
    public void FillGride()
    {
            List<ItemCardView> _ItemCardView = new List<ItemCardView>();
            var GetAllDetail = context.SaleDetailViews.Where(x => x.IsDeleted == 0).ToList();
            var GetAllItems = context.ItemCardViews.Where(x => x.IsDeleted == 0).ToList();

                var ItemTransactions = context.SaleDetailViews.Where(xxxx => xxxx.ItemCode == xxxx.ItemCode).ToList();
            foreach (var item1 in GetAllItems)
            {
                //Double LoseCount = 0, AddCount = 0, TotalCount = 0;
                var cc = ItemTransactions.Where(x => x.ItemCode == item1.ItemCode);

                foreach (var item2 in cc)
                {
                    if (item2.Operation_Type_Id == 1|| item2.Operation_Type_Id == 3 || item2.Operation_Type_Id == 4 || item2.Operation_Type_Id == 5&& item2.ItemCode== item1.ItemCode )
                    {
                        item1.Item_Count_InStoreg += Convert.ToDouble(item2.Qty);
                    }
                    if (item2.Operation_Type_Id == 2 && item2.ItemCode == item1.ItemCode)
                    {
                        item1.Item_Count_InStoreg -= Convert.ToDouble(item2.Qty);
                    }

                }
            
                    //TotalCount = AddCount - LoseCount;
                    //Count.Item_Count_InStoreg = TotalCount;
                 
            }

            gcItemCard.DataSource = GetAllItems;
    }

        private void gvItemCard_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            decimal Item_Risk_limit = Convert.ToDecimal(gvItemCard.GetRowCellValue(e.RowHandle, "Item_Risk_limit"));
            decimal Item_Count_InStoreg = Convert.ToDecimal(gvItemCard.GetRowCellValue(e.RowHandle, "Item_Count_InStoreg"));
            string ItemName = Convert.ToString(gvItemCard.GetRowCellValue(e.RowHandle, "Name"));

            if (Item_Risk_limit == Item_Count_InStoreg && !string.IsNullOrWhiteSpace(ItemName))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
            else if (Item_Risk_limit > Item_Count_InStoreg && !string.IsNullOrWhiteSpace(ItemName))
            {
                e.Appearance.BackColor = Color.Red;


            }
           

            e.HighPriority = true;
        }
    }

}