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
        POSEntity localContext = new POSEntity();
        BackOfficeEntity.db_a8f74e_posEntities _serverContext = new BackOfficeEntity.db_a8f74e_posEntities();
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

             

           
            var resultList = localContext.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();
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
           
            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));




  



            Int64 branchCode = st.GetBranch_Code();

            if (branchCode != 0)
            {
                List<SaleMasterView> Master = new List<SaleMasterView>();
                List<SaleDetailView> Detail = new List<SaleDetailView>();

                if (slkUsers.EditValue != null && !String.IsNullOrWhiteSpace(slkUsers.EditValue.ToString()))
                {

                    Master = (from a in localContext.SaleMasterViews where (a.EntryDate >= dtFrom.DateTime || a.LastDateModif >= dtFrom.DateTime) && (a.EntryDate <= dateTo || a.LastDateModif <= dateTo) && a.UserCode.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();
                    Detail = (from a in localContext.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Emp_Code.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();

                }
                else
                {

                    Master = (from a in localContext.SaleMasterViews where (a.EntryDate >= dtFrom.DateTime || a.LastDateModif >= dtFrom.DateTime) && (a.EntryDate <= dateTo || a.LastDateModif <= dateTo) && a.Operation_Type_Id == 2 select a).ToList();
                    Detail = (from a in localContext.SaleDetailViews where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Operation_Type_Id == 2 select a).ToList();
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
                                cash = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).Cash,
                                visa = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).Visa,
                                Discount = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).Discount,
                                shiftCode = x.shiftCode,
                                FinalTotal = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.Shift_Code == x.shiftCode && xx.IsDeleted == 0).FinalTotal

                            };
                            saleDetailViewVmList.Add(saleDetailViewVm);
                        }
                        catch
                        {

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

                        FinalTotal += x.Visa + x.Cash;
                        TotalDiscount += x.Discount;
                        TotalVisa += x.Visa;
                        TotalCash += x.Cash;

                    });


                    FinalTotal _FinalTotal = new FinalTotal()
                    {
                        Total = FinalTotal,
                        TotalDiscount = TotalDiscount,
                        TotalCash = TotalCash,
                        TotalVisa = TotalVisa,
                        DecreaseAndIncrease = (TotalCash + TotalVisa) - FinalTotal
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

                }
            }
            else {




                          List<BackOfficeEntity.SaleMasterView > Master = new List<BackOfficeEntity.SaleMasterView>();
            List<BackOfficeEntity.SaleDetailView> Detail = new List<BackOfficeEntity.SaleDetailView>();

                if (slkUsers.EditValue != null && !String.IsNullOrWhiteSpace(slkUsers.EditValue.ToString()))
                {

                    Master = (from a in _serverContext.SaleMasterView where (a.EntryDate >= dtFrom.DateTime || a.LastDateModif >= dtFrom.DateTime) && (a.EntryDate <= dateTo || a.LastDateModif <= dateTo) && a.UserCode.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();
                    Detail = (from a in _serverContext.SaleDetailView where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Emp_Code.ToString() == slkUsers.EditValue.ToString() && a.Operation_Type_Id == 2 select a).ToList();

                }
                else
                {

                    Master = (from a in _serverContext.SaleMasterView where (a.EntryDate >= dtFrom.DateTime || a.LastDateModif >= dtFrom.DateTime) && (a.EntryDate <= dateTo || a.LastDateModif <= dateTo) && a.Operation_Type_Id == 2 select a).ToList();
                    Detail = (from a in _serverContext.SaleDetailView where a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Operation_Type_Id == 2 select a).ToList();
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


                            ItemCardView item = localContext.ItemCardViews.Where(x2 => x2.ItemCode == x2.ItemCode && x2.IsDeleted == 0).FirstOrDefault();
                            SaleDetailViewVm saleDetailViewVm = new SaleDetailViewVm()
                            {

                                AddItem = item.AddItem,
                                SaleMasterCode = x.SaleMasterCode,
                                CategoryCode = item.CategoryCode,
                                CategoryName = item.CategoryName,
                                Emp_Code = x.Emp_Code,
                                EntryDate = x.EntryDate,
                                Id = x.Id,
                                IsDeleted = x.IsDeleted,
                                ItemCode = x.ItemCode,
                                Item_Count_InStoreg = item.Item_Count_InStoreg,
                                Name = item.Name,
                                Operation_Type_Id = x.Operation_Type_Id,
                                OrederTotal = 0,
                                ParCode = item.ParCode,
                                Price = x.Price,
                                PriceBuy = item.PriceBuy,
                                Qty = x.Qty,
                                Total = x.Total,
                                UnitCode = item.UnitCode,
                                UnitName = item.UnitName,
                                UserName = x.UserName,
                                Name_En = item.Name_En,
                                cash = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.ShiftCode == x.shiftCode && xx.IsDeleted == 0).Cash,
                                visa = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.ShiftCode == x.shiftCode && xx.IsDeleted == 0).Visa,
                                Discount = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.ShiftCode == x.shiftCode && xx.IsDeleted == 0).Discount,
                                shiftCode = x.shiftCode,
                                FinalTotal = Master.FirstOrDefault(xx => xx.SaleMasterCode == x.SaleMasterCode && xx.Branches_Code == x.Branches_Code && xx.ShiftCode == x.shiftCode && xx.IsDeleted == 0).FinalTotal

                            };
                            saleDetailViewVmList.Add(saleDetailViewVm);
                        }
                        catch
                        {

                            MaterialMessageBox.Show(st.isEnglish() ? "There are no invoices for this date" : " حدث خطاء لوجود فواتير قديمة  تم التعديل عليها في نفس التاريخ ", MessageBoxButtons.OK);
                            return;

                        }


                    }

                    Master.ForEach(Header =>
                    {
                        saleDetailViewVmList.ForEach(Line =>
                        {

                            if (Header.ShiftCode == Line.shiftCode && Header.SaleMasterCode == Line.SaleMasterCode)
                            {

                                Line.OrederTotal = Header.FinalTotal;
                                Line.Discount = Header.Discount;

                            }



                        });

                    });


                    Master.ForEach(x =>
                    {

                        FinalTotal += x.Visa + x.Cash;
                        TotalDiscount += x.Discount;
                        TotalVisa += x.Visa;
                        TotalCash += x.Cash;

                    });


                    FinalTotal _FinalTotal = new FinalTotal()
                    {
                        Total = FinalTotal,
                        TotalDiscount = TotalDiscount,
                        TotalCash = TotalCash,
                        TotalVisa = TotalVisa,
                        DecreaseAndIncrease = (TotalCash + TotalVisa) - FinalTotal
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

                }


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