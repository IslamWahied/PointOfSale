using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using FastReport;
using PointOfSaleSedek._102_MaterialSkin;
using EntityData;
using PointOfSaleSedek.Model;

namespace PointOfSaleSedek._105_Reports
{
    public partial class frmPurchessReport : DevExpress.XtraEditors.XtraForm
    {
        PointOfSaleEntities context = new PointOfSaleEntities();
        public frmPurchessReport()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var Master = (from a in context.SaleMasterViews where a.EntryDate >= dtFrom.DateTime  && a.EntryDate <= dtTo.DateTime && a.Operation_Type_Id == 1 select a).ToList();
            var Detail = (from a in context.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dtTo.DateTime && a.Operation_Type_Id == 1 select a).ToList();

            if (Master.Count == 0 || Detail.Count == 0)

            {
                MaterialMessageBox.Show("لا يوجد فواتير لهذا التاريخ", MessageBoxButtons.OK);
                return;

            }
            else
            {

                List<SaleDetailViewVm> saleDetailViewVmList = new List<SaleDetailViewVm>();

                foreach (var x in Detail)
                {
                    SaleDetailViewVm saleDetailViewVm = new SaleDetailViewVm()
                    {

                        AddItem = x.AddItem,
                        SaleMasterCode = x.SaleMasterCode,
                        CategoryCode = x.CategoryCode,
                        CategoryName = x.CategoryName,
                        Emp_Code = x.Emp_Code,
                        EntryDate = x.EntryDate,
                        Id = x.Id,
                        IsDeleted = x.IsDeleted,
                        ItemCode = x.ItemCode,
                        Item_Count_InStoreg = x.Item_Count_InStoreg,
                        Name = x.Name,
                        Operation_Type_Id = x.Operation_Type_Id,
                        OrederTotal = 0,
                        ParCode = x.ParCode,
                        Price = x.Price,
                        PriceBuy = x.PriceBuy,
                        Qty = x.Qty,
                        Total = x.Total,
                        UnitCode = x.UnitCode,
                        UnitName = x.UnitName,
                        UserName = x.UserName,
                        Discount = 0

                    };
                    saleDetailViewVmList.Add(saleDetailViewVm);

                }

                Master.ForEach(Header =>
                {
                    saleDetailViewVmList.ForEach(Line =>
                    {

                        if (Header.SaleMasterCode == Line.SaleMasterCode)
                        {

                            Line.OrederTotal = Header.FinalTotal;
                            Line.Discount = Header.Discount;

                        }



                    });

                });

                Report rpt = new Report();
               rpt.Load(@"Reports\PurchessRpt.frx");
               rpt.RegisterData(Master, "Header");
               rpt.RegisterData(saleDetailViewVmList, "Lines");
              // rpt.PrintSettings.ShowDialog = false;
         //   rpt.Design();
                rpt.Show();

            }

            ////////////// //rpt.Print();
        }

        private void frmPurchessReport_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
        }
    }
}