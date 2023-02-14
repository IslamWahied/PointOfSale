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
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._105_Reports
{
    public partial class frmSaleReports : DevExpress.XtraEditors.XtraForm
    {
        POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmSaleReports()
        {
            InitializeComponent();
            langu();
            FillSlkUser();

        }
        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.Text = st.isEnglish() ? "Sales Bill" : "فاتورة مبيعات";

            materialLabel3.Text = st.isEnglish() ? "UserName" : "اسم المستخدم";
            materialLabel1.Text = st.isEnglish() ? "From Date" : "من تاريخ";
            materialLabel2.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
            simpleButton1.Text = st.isEnglish() ? "View" : "عرض";
           
 
          
             
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
            double TotalVisa = 0;
            double TotalCash = 0;
            // var Master = (from a in context.SaleMasterViews where a.EntryDate > dtFrom.DateTime && a.EntryDate < dtTo.DateTime && a.Operation_Type_Id == 2 select a).ToList();

            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
            List<SaleMasterView> Master = new List<SaleMasterView>();
            List<SaleDetailView> Detail = new List<SaleDetailView>();
            

            if (slkUsers.EditValue != null && !String.IsNullOrWhiteSpace(slkUsers.EditValue.ToString()))
            {

                Master = (from a in context.SaleMasterViews where (a.EntryDate >= dtFrom.DateTime || a.LastDateModif >= dtFrom.DateTime) && (a.EntryDate <= dateTo || a.LastDateModif <= dateTo) && a.UserCode.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();
                 Detail = (from a in context.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Emp_Code.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();

            }
            else {

                 Master = (from a in context.SaleMasterViews where (a.EntryDate >= dtFrom.DateTime || a.LastDateModif >= dtFrom.DateTime) && (a.EntryDate <= dateTo || a.LastDateModif <= dateTo) && a.Operation_Type_Id == 2 select a).ToList();
                 Detail = (from a in context.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Operation_Type_Id == 2 select a).ToList();
            }


          


            if (Master.Count == 0 || Detail.Count == 0)

            {
                MaterialMessageBox.Show(st.isEnglish() ? "There are no invoices for this date" : "لا يوجد فواتير لهذا التاريخ", MessageBoxButtons.OK);
                return;

            }
            else
            {

            List<SaleDetailViewVm> saleDetailViewVmList = new List<SaleDetailViewVm>();

                foreach (var x in Detail)
                {

                    try
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
                            Name_En = x.Name_En,
                            cash = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).Cash,
                            visa = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).Visa,
                            Discount = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).Discount,
                            shiftCode = x.shiftCode,
                            FinalTotal = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).FinalTotal

                        };
                        saleDetailViewVmList.Add(saleDetailViewVm);
                    }
                    catch  {

                        MaterialMessageBox.Show(st.isEnglish() ? "There are no invoices for this date" : " حدث خطاء لوجود فواتير قديمة  تم التعديل عليها في نفس التاريخ ", MessageBoxButtons.OK);
                        return;

                    }
                  

                }

                Master.ForEach(Header =>
                {
                    saleDetailViewVmList.ForEach(Line =>
                    {

                        if (Header.Shift_Code == Line.shiftCode && Header.SaleMasterCode == Line.SaleMasterCode)
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
                    TotalVisa += x.Visa;
                    TotalCash += x.Cash;

                });




                FinalTotal _FinalTotal = new FinalTotal()
                {
                    Total = FinalTotal,
                    TotalDiscount = TotalDiscount,
                    TotalCash = TotalCash,
                    TotalVisa =  TotalVisa,
                    DecreaseAndIncrease = (TotalCash + TotalVisa) -  FinalTotal  
                };
                List<FinalTotal> _FinalTotalList = new List<FinalTotal>();
                _FinalTotalList.Add(_FinalTotal);


                Report rpt = new Report();
                rpt.Load(@"Reports\SalesReport.frx");
                rpt.RegisterData(Master, "Header");
                rpt.RegisterData(_FinalTotalList, "FinalTotal");
                rpt.RegisterData(saleDetailViewVmList, "Lines");
               rpt.PrintSettings.ShowDialog = false;
          //  rpt.Design();
            rpt.Show();
//
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