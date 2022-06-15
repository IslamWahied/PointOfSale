using EntityData;
using FastReport;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSaleSedek._102_Reports
{
    public partial class frmCanelationInvoice : MaterialSkin.Controls.MaterialForm
    {
        PointOfSaleEntities2 context = new PointOfSaleEntities2();

        public frmCanelationInvoice()
        {
            InitializeComponent();
        }

        private void frmCanelationInvoice_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // var Master = (from a in context.SaleMasterViews where a.EntryDate > dtFrom.DateTime && a.EntryDate < dtTo.DateTime && a.Operation_Type_Id == 2 select a).ToList();
            var Master = context.SaleMasterViews.Where(a => a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dtTo.DateTime && a.Operation_Type_Id == 3).ToList();
            var Detail = (from a in context.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dtTo.DateTime && a.Operation_Type_Id == 3 select a).ToList();

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


                double FinalTotal = 0;
                double TotalDiscount = 0;


                Master.ForEach(x =>
                {

                    FinalTotal += x.FinalTotal;
                    TotalDiscount += x.Discount;

                });



                FinalTotal _FinalTotal = new FinalTotal()
                {
                    Total = FinalTotal,
                    TotalDiscount = TotalDiscount



                };
                List<FinalTotal> _FinalTotalList = new List<FinalTotal>();
                _FinalTotalList.Add(_FinalTotal);

                Report rpt = new Report();
                rpt.Load(@"Reports\SalesReport.frx");
                rpt.RegisterData(Master, "Header");
                rpt.RegisterData(_FinalTotalList, "FinalTotal");
                rpt.RegisterData(saleDetailViewVmList, "Lines");
                // rpt.PrintSettings.ShowDialog = false;
             //       rpt.Design();
               rpt.Show();

            }
        }
    }
}
