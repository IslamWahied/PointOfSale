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
using PointOfSaleSedek._102_MaterialSkin;
using FastReport;
using DataRep;
using PointOfSaleSedek.Model;

namespace PointOfSaleSedek._105_Reports
{
    public partial class frmSaleReports : DevExpress.XtraEditors.XtraForm
    {
        SaleEntities context = new SaleEntities();
        public frmSaleReports()
        {
            InitializeComponent();
            FillSlkUser();

        }

        public void FillSlkUser()
        {

            //List<User_View> listUserView = new List<User_View>();


            //var resultList = context.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();

            //foreach (var item in resultList)
            //{
            //    bool ressult = context.Shift_View.Any(Shift => Shift.IsDeleted == 0 && Shift.Emp_Code == item.Employee_Code && Shift.Shift_Flag == true);
            //    if (!ressult)
            //    {

            //        listUserView.Add(item);
            //    }

            //}


            //   var result = Context.Shift_View.Where(Shift => Shift.IsDeleted == 0  && Shift.Shift_Flag == false).ToList();
            var resultList = context.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();
            slkUsers.Properties.DataSource = resultList;
            slkUsers.Properties.ValueMember = "Employee_Code";
            slkUsers.Properties.DisplayMember = "UserName";

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            double FinalTotal = 0;
            double TotalDiscount = 0;
            // var Master = (from a in context.SaleMasterViews where a.EntryDate > dtFrom.DateTime && a.EntryDate < dtTo.DateTime && a.Operation_Type_Id == 2 select a).ToList();

            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtFrom.EditValue).AddDays(1));
            List<SaleMasterView> Master = new List<SaleMasterView>();
            List<SaleDetailView> Detail = new List<SaleDetailView>();
            

            if (slkUsers.EditValue != null && !String.IsNullOrWhiteSpace(slkUsers.EditValue.ToString()))
            {

                Master = (from a in context.SaleMasterViews where    a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.UserCode.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();
                 Detail = (from a in context.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Emp_Code.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();

            }
            else {

                 Master = (from a in context.SaleMasterViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Operation_Type_Id == 2 select a).ToList();
                 Detail = (from a in context.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Operation_Type_Id == 2 select a).ToList();
            }


          


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
            //   rpt.Design();
            rpt.Show();

            }

        }

        private void frmSaleReports_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
        }

        private void slkShiftsOpen_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}