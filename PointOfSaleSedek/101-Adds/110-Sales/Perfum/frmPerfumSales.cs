using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Extensions;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using PointOfSaleSedek._101_Adds._102_Customer;
using PointOfSaleSedek._101_Adds._110_Sales;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmPerfumSales : Form
    {
        SaleEntities context = new SaleEntities();
        public String Status = "New";
        public bool SearchStatus = false;
        Static st = new Static();


        public frmPerfumSales()
        {
            InitializeComponent();
           


        }

        

        private void btnEdite_Click(object sender, EventArgs e)
        {
 
            gcSaleDetail.Enabled = true;
            
            btnNew.Enabled = true;
            btnSave.Enabled = true;
            btnDiscount.Enabled = true;
            btnEdite.Enabled = false;


            
            btnAddItems.Enabled = true;
            slkCustomers.Enabled = false;
            SlkPaymentsType.Enabled = false;

            HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
        }

     
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void getallpaymentstypes()
        {

            var Payment_Type = context.Payment_Type.Where(x => x.IsDeleted == 0).ToList();



            SlkPaymentsType.Properties.DataSource = Payment_Type;
            SlkPaymentsType.Properties.ValueMember = "Code";
            SlkPaymentsType.Properties.DisplayMember = "PaymentType";
            SlkPaymentsType.EditValue = 1;


        }
       
        private void frmSales_Load(object sender, EventArgs e)
        {
            
            //timer1.Start();
                 this.Status = "New";
                
                 btnEdite.Enabled = false;

            var UserId = Convert.ToInt64(st.User_Code());
            var Check = context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
            if (!Check)
            {
                if (MaterialMessageBox.Show("برجاء اضافة وردية لهذا المستخدم", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }

            }
            NewForLoad();
            
        }

    

         void NewForLoad()
         {

            this.Status = "New";
            HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64? MaxCode = context.SaleMasters.Where(x => x.ShiftCode == ShiftCode && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }
            lblSaleMasterId.Text = MaxCode.ToString();
            // lblCurrency.Text = "جنية مصري";
            lblFinalTotal.Text = "0";
             lblFinalBeforDesCound.Text = "0";
             lblItemQty.Text = "0";
             lblDiscount.Text = "0";
             lblUserName.Text = "المدير";
          //dtEntryDate.DateTime = DateTime.Now;
             while (gvSaleDetail.RowCount > 0)
             {
                 gvSaleDetail.SelectAll();
                 gvSaleDetail.DeleteSelectedRows();
             }

             gcSaleDetail.DataSource = null;
             gcSaleDetail.RefreshDataSource();
             btnSave.Enabled = true;
            getallpaymentstypes();


            FillSlkEmployees();
            FillSlkCustomers();



        }

      
        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
        {
            var RowCount = gvSaleDetail.RowCount;
                 var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

                 List<SaleDetailView> gcData = gcSaleDetail.DataSource as List<SaleDetailView>;
                 gcData.Remove(FocusRow);
                
            double sum = 0;
            gcData.ForEach(x =>
            {
                 
                    x.Total = x.Qty * x.Price;
                sum += Convert.ToDouble(x.Total);



            });


            gcSaleDetail.DataSource = null;
            gcSaleDetail.DataSource = gcData;
            gcSaleDetail.RefreshDataSource();

                 



                 lblFinalBeforDesCound.Text = sum.ToString();

            lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                 lblItemQty.Text = (RowCount - 1).ToString();
                 
                  
             


        }

        private void gvSaleDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            List<SaleDetailView> gcData = gcSaleDetail.DataSource as List<SaleDetailView>;
            var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

            gcData.Where(x => x.ItemCode == FocusRow.ItemCode).Select(x => x.Qty == (Double) e.Value);
            
         
           
            double sum = 0;
            gcData.ForEach(x =>
            {
                x.Total = x.Qty * x.Price;
                sum += Convert.ToDouble(x.Total);
            });
            gcSaleDetail.DataSource = null;
            gcSaleDetail.DataSource = gcData;
            gcSaleDetail.RefreshDataSource();

            lblItemQty.Text = gvSaleDetail.RowCount.ToString();
            lblFinalBeforDesCound.Text = sum.ToString();

            lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
           


           

        }

        // //Save SaleMaster Date
        //public void SaveSaleMaster()

        //{


        //    Int64 UserCode = st.User_Code();
        //    var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx=>xx.Shift_Code).SingleOrDefault();
        //   Int64 CustomerCode = Convert.ToInt64(slkCustomers.EditValue);
        //    if (this.Status == "Old")
        //    {

        //        Int64 SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);
        //        bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.EntryDate == DateTime.Today && SaleMaster.Operation_Type_Id==2);

        //        if (TestUpdate)
        //        {
        //            SaleMaster _SaleMasters;
        //            _SaleMasters = context.SaleMasters.SingleOrDefault(Master => Master.SaleMasterCode == SaleMasterCode && Master.EntryDate == DateTime.Today&& Master.Operation_Type_Id==2);
        //            _SaleMasters.LastDateModif = DateTime.Now;
        //            _SaleMasters.TotalBeforDiscount = double.Parse(lblFinalBeforDesCound.Text);
        //            _SaleMasters.Discount = double.Parse(lblDiscount.Text);
        //            _SaleMasters.FinalTotal = double.Parse(lblFinalTotal.Text);
        //            _SaleMasters.ShiftCode = ShiftCode;
        //            _SaleMasters.QtyTotal = double.Parse(lblItemQty.Text);
        //            _SaleMasters.Operation_Type_Id = 2;
        //            _SaleMasters.Payment_Type = Convert.ToInt64(SlkPaymentsType.EditValue);
        //            _SaleMasters.UserIdTakeOrder = Convert.ToInt64(slkEmployees.EditValue);

        //            SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);


        //            _SaleMasters.UserCode = st.User_Code();
        //            context.SaveChanges();



        //        }


        //    }
        //    else
        //    {

        //       SaleMaster _SaleMaster = new SaleMaster()
        //       {

        //           EntryDate = DateTime.Now,
        //           Payment_Type = Convert.ToInt64(SlkPaymentsType.EditValue),
        //           UserIdTakeOrder = Convert.ToInt64(slkEmployees.EditValue),
        //           Discount = double.Parse(lblDiscount.Text),
        //           TotalBeforDiscount = double.Parse(lblFinalBeforDesCound.Text),
        //           FinalTotal = double.Parse(lblFinalTotal.Text),
        //           ShiftCode = ShiftCode,

        //           QtyTotal = double.Parse(lblItemQty.Text),
        //           SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
        //           Operation_Type_Id = 2,
        //           Customer_Code = Convert.ToInt64(slkCustomers.EditValue),
        //           UserCode = UserCode,



        //        };
        //        context.SaleMasters.Add(_SaleMaster);
        //        context.SaveChanges();

        //    }




        //}



        public void SaveSaleMaster(Int64 ShiftCode, Int64 UserCode, Double Discount, Double TotalBeforDiscount, Double FinalTotal, Double QtyTotal, Int64 SaleMasterCode)

        {
            //if (slkCustomers.EditValue == null)
            //{
            //    slkCustomers.EditValue = 0;
            //}


            //if (slkCustomers.EditValue == null)
            //{
            //    slkCustomers.EditValue = 0;
            //}

            if (this.Status == "Old")
            {


                bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.EntryDate.Day == DateTime.Today.Day && SaleMaster.EntryDate.Month == DateTime.Today.Month && SaleMaster.EntryDate.Year == DateTime.Today.Year && SaleMaster.Operation_Type_Id == 2);

                if (TestUpdate)
                {
                    SaleMaster _SaleMasters;
                    _SaleMasters = context.SaleMasters.SingleOrDefault(Master => Master.SaleMasterCode == SaleMasterCode && Master.EntryDate.Day == DateTime.Today.Day && Master.EntryDate.Month == DateTime.Today.Month && Master.EntryDate.Year == DateTime.Today.Year && Master.Operation_Type_Id == 2);
                    _SaleMasters.LastDateModif = DateTime.Now;
                    _SaleMasters.TotalBeforDiscount = double.Parse(lblFinalBeforDesCound.Text);
                    _SaleMasters.Discount = double.Parse(lblDiscount.Text);
                    _SaleMasters.FinalTotal = double.Parse(lblFinalTotal.Text);
                    _SaleMasters.ShiftCode = ShiftCode;
                    _SaleMasters.QtyTotal = double.Parse(lblItemQty.Text);
                    _SaleMasters.Customer_Code = Int64.Parse(slkCustomers.EditValue.ToString());
                    _SaleMasters.Operation_Type_Id = 2;
                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);
                    _SaleMasters.Payment_Type = Int64.Parse(SlkPaymentsType.EditValue.ToString());

                    _SaleMasters.UserCode = st.User_Code();
                    context.SaveChanges();



                }


            }
            else
            {

                SaleMaster _SaleMaster = new SaleMaster()
                {

                    EntryDate = DateTime.Now,


                    Discount = double.Parse(lblDiscount.Text),
                    TotalBeforDiscount = double.Parse(lblFinalBeforDesCound.Text),
                    FinalTotal = double.Parse(lblFinalTotal.Text),
                    ShiftCode = ShiftCode,
                     Payment_Type = Int64.Parse(SlkPaymentsType.EditValue.ToString()),
                    
                    UserIdTakeOrder = UserCode,
                    Customer_Code = Int64.Parse(slkCustomers.EditValue.ToString()),
                 
                    QtyTotal = double.Parse(lblItemQty.Text),
                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
                    Operation_Type_Id = 2,

                    UserCode = UserCode,

                };
                context.SaleMasters.Add(_SaleMaster);
                context.SaveChanges();

            }




        }

        //public void SaveSaleDetail()
        //{
        //    List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();
        //    var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
        //    Int64 UserCode = st.User_Code();
        //    var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();

        //        Int64 SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);
        //    if (this.Status == "Old")
        //    {
        //        bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Operation_Type_Id==2 && SaleMaster.EntryDate == DateTime.Today);

        //        if (TestUpdate)
        //        {

        //            var SaleDetails = context.SaleDetails.Where(Masters => Masters.SaleMasterCode == SaleMasterCode && Masters.Operation_Type_Id ==2 && Masters.EntryDate == DateTime.Today);


        //           // Vildate QTy
        //           using (PointOfSaleEntities ContVald = new PointOfSaleEntities())
        //           {
        //               GetDataFromGrid.ForEach(item =>
        //               {

        //                   var qty = item.Qty;

        //                   var DayOfYears = item.EntryDate.Day;
        //                   var Years = item.EntryDate.Year;
        //                   var Months = item.EntryDate.Month;

        //                   var Item_Qty_List_Tran_History = ContVald.Item_History_transaction.Where(w => w.Item_Id == item.ItemCode && w.IsDeleted == 0 && w.SaleMasterCode == item.SaleMasterCode &&w.OrderDate.Day== DayOfYears&& w.OrderDate.Month == Months && w.OrderDate.Year == Years).ToList();
        //                   var item_Qt_Tran_History = Item_Qty_List_Tran_History.Where(x => x.Item_Id == item.ItemCode && x.SaleMasterCode == item.SaleMasterCode).Sum(x => x.Trans_Out);


        //                   var Item_Qty_List = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();
        //                   var item_Qt = Item_Qty_List.Where(x => x.Item_Id == item.ItemCode && x.IsFinshed == false).Sum(x => x.Current_Qty_Now);
        //                   var Total = item_Qt_Tran_History + item_Qt;

        //                   //if (Total < qty)
        //                   //{
        //                   //    MaterialMessageBox.Show("كمية" + item.Name + "غير كافية في المخزن حيث يوجد " + item_Qt + "فقط ", MessageBoxButtons.OK);
        //                   //    return;
        //                   //}
        //               });

        //           }

        //           using (PointOfSaleEntities context2 = new PointOfSaleEntities())
        //            {
        //                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == SaleMasterCode && w.EntryDate == DateTime.Today);
        //                context2.SaleDetails.RemoveRange(Details);
        //                context2.SaveChanges();



        //           }




        //           List<Item_History_transaction> _Item_History_transaction  = new List<Item_History_transaction>();

        //           var DayOfYear = DateTime.Today.Day;
        //           var Year = DateTime.Today.Year;
        //           var Month = DateTime.Today.Month;
        //           _Item_History_transaction = context.Item_History_transaction.Where(w => w.SaleMasterCode == SaleMasterCode &&w.OrderDate.Day == DayOfYear && w.OrderDate.Month == Month && w.OrderDate.Year == Year && w.IsDeleted==0).ToList();

        //           _Item_History_transaction.ForEach(x => {

        //               Item_History item_History;
        //               item_History = context.Item_History.SingleOrDefault(Item => Item.Item_Id==x.Item_Id&&Item.Id==x.Item_History_Id);
        //               item_History.Current_Qty_Now -=   x.Trans_Out;

        //               item_History.IsFinshed = false;
        //               context.SaveChanges();




        //             List<Item_History_transaction> __Item_History_transaction = new List<Item_History_transaction>();
        //               __Item_History_transaction = context.Item_History_transaction.Where(Item => Item.SaleMasterCode == x.SaleMasterCode && Item.Item_Id == x.Item_Id && Item.IsDeleted ==0).ToList();
        //               __Item_History_transaction.ForEach(xx => { 

        //               Item_History_transaction Item_History_transaction;
        //                   Item_History_transaction = context.Item_History_transaction.SingleOrDefault(Item => Item.Id == xx.Id);

        //                   Item_History_transaction.IsDeleted = 1;


        //                   context.SaveChanges();
        //               });








        //           });


        //           foreach (var item in GetDataFromGrid)
        //            {

        //               var qty = item.Qty;


        //               var Item_Qty_List = context.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();
        //             var item_Qt =   Item_Qty_List.Where(x => x.Item_Id == item.ItemCode && x.IsFinshed == true).Sum(x => x.Current_Qty_Now);


        //               Item_Qty_List.ForEach(x =>
        //               {
        //                   if (qty > 0)
        //                   {
        //                       // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
        //                       if (qty > x.Current_Qty_Now)
        //                       {
        //                           qty = qty - x.Current_Qty_Now;

        //                           Update_Item_Qty_And_Finshed(x.Sale_Master_Code, x.Current_Qty_Now, x.CreatedDate, x.Id, x.Item_Id);
        //                       }
        //                       else if (qty < x.Current_Qty_Now)
        //                       {


        //                           Update_Item_Qty_Oly(x.Sale_Master_Code, qty, x.CreatedDate,x.Id,x.Item_Id);
        //                           qty = 0;
        //                       }
        //                       else if (qty == x.Current_Qty_Now)
        //                       {

        //                           Update_Item_Qty_And_Finshed(x.Sale_Master_Code, qty, x.CreatedDate, x.Id, x.Item_Id);
        //                           qty = 0;
        //                       }


        //                   }


        //               });


        //               SaleDetail _SaleDetail = new SaleDetail()
        //                {
        //                    ItemCode = item.ItemCode,
        //                    Price = item.Price,
        //                    Qty = item.Qty,
        //                    Total = item.Total,
        //                    EntryDate = DateTime.Now,
        //                    SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
        //                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
        //                   CustomerCode = Convert.ToInt64(slkCustomers.EditValue),



        //                    Operation_Type_Id = 2,
        //                     UserId =  st.User_Code()



        //            };

        //                ArryOfSaleDetail.Add(_SaleDetail);

        //            }


        //            context.SaleDetails.AddRange(ArryOfSaleDetail);
        //            context.SaveChanges();
        //           SaveSaleMaster();



        //           var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
        //           var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
        //           New();
        //           //Report rpt = new Report();
        //           //rpt.Load(@"Reports\SaleInvoice.frx");
        //           //rpt.RegisterData(Master, "Header");
        //           //rpt.RegisterData(Detail, "Lines");
        //           //rpt.PrintSettings.ShowDialog = false;
        //           ////rpt.Design();
        //           //rpt.Print();



        //       }





        //    }
        //    else
        //    {

        //       using (PointOfSaleEntities ContVald = new PointOfSaleEntities ())
        //       {
        //       foreach (var item in GetDataFromGrid)
        //       {


        //           var qty = item.Qty;

        //           var Item_Qty_List = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();
        //           var item_Qt = Item_Qty_List.Where(x => x.Item_Id == item.ItemCode && x.IsFinshed == false).Sum(x => x.Current_Qty_Now);


        //           //if (item_Qt < qty)
        //           //{
        //           //    MaterialMessageBox.Show("كمية   " + item.Name + " غير كافية في المخزن حيث يوجد  " + item_Qt + "  فقط  ", MessageBoxButtons.OK);
        //           //    return;

        //           //}


        //       }

        //       }
        //       // Vildate QTy


        //       foreach (var item in GetDataFromGrid)
        //        {

        //           var qty = item.Qty;


        //           var Item_Qty_List = context.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();



        //           Item_Qty_List.ForEach(x =>
        //           {
        //               if (qty > 0)
        //               {
        //                   // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
        //                   if (qty > x.Current_Qty_Now)
        //                   {
        //                       qty = qty - x.Current_Qty_Now;

        //                       Update_Item_Qty_And_Finshed(SaleMasterCode, x.Current_Qty_Now, x.CreatedDate, x.Id, x.Item_Id);
        //                   }
        //                   else if (qty < x.Current_Qty_Now)
        //                   {


        //                       Update_Item_Qty_Oly(SaleMasterCode, qty, x.CreatedDate, x.Id, x.Item_Id);
        //                       qty = 0;
        //                   }
        //                   else if (qty == x.Current_Qty_Now)
        //                   {

        //                       Update_Item_Qty_And_Finshed(SaleMasterCode, qty, x.CreatedDate, x.Id, x.Item_Id);
        //                       qty = 0;
        //                   }


        //               }


        //           });





        //           SaleDetail _SaleDetail = new SaleDetail()
        //            {
        //                ItemCode = item.ItemCode,
        //                Price = item.Price,
        //                Qty = item.Qty,
        //                Total = item.Total,
        //                EntryDate = DateTime.Now,
        //                SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
        //                SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
        //                UserId = st.User_Code(),
        //                Operation_Type_Id = 2,
        //                CustomerCode = Convert.ToInt64(slkCustomers.EditValue)

        //            };

        //            ArryOfSaleDetail.Add(_SaleDetail);

        //        }


        //        context.SaleDetails.AddRange(ArryOfSaleDetail);
        //        context.SaveChanges();
        //        SaveSaleMaster();


        //       var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year select a).ToList();
        //       var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year select a).ToList();
        //       New();
        //       //Report rpt = new Report();
        //       //rpt.Load(@"Reports\SaleInvoice.frx");
        //       //rpt.RegisterData(Master, "Header");
        //       //rpt.RegisterData(Detail, "Lines");
        //       //rpt.PrintSettings.ShowDialog = false;
        //       ////rpt.Design();
        //       //rpt.Print();


        //   }




        //}


        public void SaveSaleDetail()
        {


            //if (slkPaymentType.EditValue == null || slkPaymentType.EditValue.ToString() == "0" || slkPaymentType.Text.Trim() == "")
            //{
            //    MaterialMessageBox.Show("برجاء اختيار طريقة الدفع", MessageBoxButtons.OK);
            //    return;
            //}



            var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;

            decimal finaltotal = Convert.ToInt64(lblFinalTotal.Text);

            if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
            {


                MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                return;


            }

            if (finaltotal < 0)
            {

                MaterialMessageBox.Show("لا يمكن قبول قيمة فاتورة اقل من الصفر", MessageBoxButtons.OK);
                return;
            }




            List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();

            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64 SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);


            if (this.Status == "Old")
            {
                bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Operation_Type_Id == 2 && SaleMaster.ShiftCode == ShiftCode);

                if (TestUpdate)
                {

                    var SaleDetails = context.SaleDetails.Where(Masters => Masters.SaleMasterCode == SaleMasterCode && Masters.Operation_Type_Id == 2 && Masters.shiftCode == ShiftCode);


                    // Vildate QTy
                    //using (PointOfSaleEntities ContVald = new PointOfSaleEntities())
                    //{
                    //    GetDataFromGrid.ForEach(item =>
                    //    {

                    //        var qty = item.Qty;

                    //        var DayOfYears = item.EntryDate.Day;
                    //        var Years = item.EntryDate.Year;
                    //        var Months = item.EntryDate.Month;

                    //        var Item_Qty_List_Tran_History = ContVald.Item_History_transaction.Where(w => w.Item_Id == item.ItemCode && w.IsDeleted == 0 && w.SaleMasterCode == item.SaleMasterCode && w.OrderDate.Day == DayOfYears && w.OrderDate.Month == Months && w.OrderDate.Year == Years).ToList();
                    //        var item_Qt_Tran_History = Item_Qty_List_Tran_History.Where(x => x.Item_Id == item.ItemCode && x.SaleMasterCode == item.SaleMasterCode).Sum(x => x.Trans_Out);


                    //        var Item_Qty_List = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();
                    //        var item_Qt = Item_Qty_List.Where(x => x.Item_Id == item.ItemCode && x.IsFinshed == false).Sum(x => x.Current_Qty_Now);
                    //        var Total = item_Qt_Tran_History + item_Qt;

                    //        if (Total < qty)
                    //        {
                    //            MaterialMessageBox.Show("كمية" + item.Name + "غير كافية في المخزن حيث يوجد " + item_Qt + "فقط هل تريد اكمال العملية ", MessageBoxButtons.OKCancel);
                    //            return;
                    //        }
                    //    });

                    //}

                    using (SaleEntities context2 = new SaleEntities())
                    {
                        var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == SaleMasterCode && w.shiftCode == ShiftCode);
                        context2.SaleDetails.RemoveRange(Details);
                        context2.SaveChanges();



                    }




                    List<Item_History_transaction> _Item_History_transaction = new List<Item_History_transaction>();

                    var DayOfYear = DateTime.Today.Day;
                    var Year = DateTime.Today.Year;
                    var Month = DateTime.Today.Month;
                    _Item_History_transaction = context.Item_History_transaction.Where(w => w.SaleMasterCode == SaleMasterCode && w.shiftCode == ShiftCode && w.IsDeleted == 0).ToList();

                    _Item_History_transaction.ForEach(x => {

                        Item_History item_History;
                        item_History = context.Item_History.SingleOrDefault(Item => Item.Item_Id == x.Item_Id && Item.Id == x.Item_History_Id);
                        item_History.Current_Qty_Now -= x.Trans_Out;

                        item_History.IsFinshed = false;
                        context.SaveChanges();




                        List<Item_History_transaction> __Item_History_transaction = new List<Item_History_transaction>();
                        __Item_History_transaction = context.Item_History_transaction.Where(Item => Item.shiftCode == ShiftCode && Item.SaleMasterCode == x.SaleMasterCode && Item.Item_Id == x.Item_Id && Item.IsDeleted == 0).ToList();
                        __Item_History_transaction.ForEach(xx => {

                            Item_History_transaction Item_History_transaction;
                            Item_History_transaction = context.Item_History_transaction.SingleOrDefault(Item => Item.Id == xx.Id);

                            Item_History_transaction.IsDeleted = 1;


                            context.SaveChanges();
                        });








                    });


                    foreach (var item in GetDataFromGrid)
                    {

                        var qty = item.Qty;

                        #region
                        //  var Item_Qty_List = context.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();
                        //   var item_Qt =   Item_Qty_List.Where(x => x.Item_Id == item.ItemCode && x.IsFinshed == true).Sum(x => x.Current_Qty_Now);


                        //Item_Qty_List.ForEach(x =>
                        //{
                        //    if (qty > 0)
                        //    {
                        //        // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
                        //        if (qty > x.Current_Qty_Now)
                        //        {
                        //            qty = qty - x.Current_Qty_Now;

                        //            Update_Item_Qty_And_Finshed(x.Sale_Master_Code, x.Current_Qty_Now, x.CreatedDate, x.Id, x.Item_Id);
                        //        }
                        //        else if (qty < x.Current_Qty_Now)
                        //        {


                        //            Update_Item_Qty_Oly(x.Sale_Master_Code, qty, x.CreatedDate,x.Id,x.Item_Id);
                        //            qty = 0;
                        //        }
                        //        else if (qty == x.Current_Qty_Now)
                        //        {

                        //            Update_Item_Qty_And_Finshed(x.Sale_Master_Code, qty, x.CreatedDate, x.Id, x.Item_Id);
                        //            qty = 0;
                        //        }


                        //    }


                        //});
                        #endregion

                        SaleDetail _SaleDetail = new SaleDetail()
                        {
                            ItemCode = item.ItemCode,
                            Price = item.Price,
                            Qty = item.Qty,
                            Total = item.Total,
                            EntryDate = DateTime.Now,
                            CustomerCode = Convert.ToInt64(slkCustomers.EditValue),
                            shiftCode = ShiftCode,
                            SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
                            SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
                            LastDateModif = DateTime.Now,
                            Operation_Type_Id = 2,
                            UserId = st.User_Code()



                        };

                        ArryOfSaleDetail.Add(_SaleDetail);

                    }


                    context.SaleDetails.AddRange(ArryOfSaleDetail);
                    context.SaveChanges();

                    SaveSaleMaster(
                        
                         ShiftCode: ShiftCode,
                         
                         UserCode: st.User_Code(),
                         Discount: double.Parse(lblDiscount.Text),
                         TotalBeforDiscount: double.Parse(lblFinalBeforDesCound.Text),
                         FinalTotal: double.Parse(lblFinalTotal.Text),
                         QtyTotal: double.Parse(lblItemQty.Text),
                         SaleMasterCode: Int64.Parse(lblSaleMasterId.Text)
                        );



                    var Master = (from a in context.SaleMasterViews where a.Shift_Code == ShiftCode select a).ToList();
                    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == ShiftCode select a).ToList();

                    New();
                    //Report rpt = new Report();
                    //rpt.Load(@"Reports\SaleInvoice.frx");
                    //rpt.RegisterData(Master, "Header");
                    //rpt.RegisterData(Detail, "Lines");
                    //rpt.PrintSettings.ShowDialog = false;
                    ////rpt.Design();
                    //rpt.Print();



                }





            }
            else
            {
                #region
                //using (PointOfSaleEntities ContVald = new PointOfSaleEntities())
                //{
                //    foreach (var item in GetDataFromGrid)
                //    {




                //        var Item_Qty_List = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList().Sum(x => x.Current_Qty_Now);



                //        if (Item_Qty_List < item.Qty)
                //        {

                //            if (MaterialMessageBox.Show("كمية   " + item.Name + " غير كافية في المخزن حيث يوجد  " + Item_Qty_List + "  فقط  ", MessageBoxButtons.YesNo) != DialogResult.OK)
                //            {


                //                return;
                //            }
                //        }


                //    }

                //}
                // Vildate QTy
                #endregion

                foreach (var item in GetDataFromGrid)
                {

                    var qty = item.Qty;


                    var Item_Qty_List = context.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();


                    #region
                    //item_qty_list.foreach(x =>
                    //{
                    //    if (qty > 0)
                    //    {
                    //        // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
                    //        if (qty > x.current_qty_now)
                    //        {
                    //            qty = qty - x.current_qty_now;

                    //            update_item_qty_and_finshed(salemastercode, x.current_qty_now, x.createddate, x.id, x.item_id);
                    //        }
                    //        else if (qty < x.current_qty_now)
                    //        {


                    //            update_item_qty_oly(salemastercode, qty, x.createddate, x.id, x.item_id);
                    //            qty = 0;
                    //        }
                    //        else if (qty == x.current_qty_now)
                    //        {

                    //            update_item_qty_and_finshed(salemastercode, qty, x.createddate, x.id, x.item_id);
                    //            qty = 0;
                    //        }


                    //    }


                    //});

                    #endregion



                    SaleDetail _SaleDetail = new SaleDetail()
                    {
                        ItemCode = item.ItemCode,
                        Price = item.Price,
                        Qty = item.Qty,
                        shiftCode = ShiftCode,
                        Total = item.Total,
                        EntryDate = DateTime.Now,
                        CustomerCode = Convert.ToInt64(slkCustomers.EditValue),
                        SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
                        SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
                        UserId = st.User_Code(),
                        Operation_Type_Id = 2,

                    };

                    ArryOfSaleDetail.Add(_SaleDetail);

                }


                context.SaleDetails.AddRange(ArryOfSaleDetail);
                context.SaveChanges();
                while (gvSaleDetail.RowCount > 0)
                {
                    gvSaleDetail.SelectAll();
                    gvSaleDetail.DeleteSelectedRows();
                    gcSaleDetail.RefreshDataSource();

                }
                SaveSaleMaster(
                         ShiftCode: ShiftCode,
                         UserCode: st.User_Code(),
                         
                         Discount: double.Parse(lblDiscount.Text),
                         TotalBeforDiscount: double.Parse(lblFinalBeforDesCound.Text),
                         FinalTotal: double.Parse(lblFinalTotal.Text),
                         QtyTotal: double.Parse(lblItemQty.Text),
                         SaleMasterCode: Int64.Parse(lblSaleMasterId.Text)
                        );


                New();
                var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.Shift_Code == ShiftCode select a).ToList();
                var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == ShiftCode select a).ToList();

                //Report rpt = new Report();
                //rpt.Load(@"Reports\SaleInvoice.frx");
                //rpt.RegisterData(Master, "Header");
                //rpt.RegisterData(Detail, "Lines");
                //rpt.PrintSettings.ShowDialog = false;
                ////rpt.Design();
                //rpt.Print();


            }




        }


        void Update_Item_Qty_Oly(Int64 Sale_Master_Code, double Qty,DateTime OrderDate, Int64 History_Id, Int64 Item_Id)
        {

            using (SaleEntities _context2 = new SaleEntities())
            {
                Item_History item_History;
                item_History = _context2.Item_History.SingleOrDefault(Item => Item.Id == History_Id);
                item_History.Current_Qty_Now -= Qty;
                item_History.Is_Used = (bool)true;
                _context2.SaveChanges();
            }
            using (SaleEntities _context = new SaleEntities())
            {
                Item_History_transaction _Item_History_transaction = new Item_History_transaction()
                {
                    OrderDate = OrderDate,
                    Item_History_Id = History_Id,
                    CreatedDate = DateTime.Now,
                    Trans_In = 0,
                    Trans_Out = Qty,
                    IsDeleted = 0,
                    Item_Id = Item_Id,
                    SaleMasterCode = Sale_Master_Code

                };
                _context.Item_History_transaction.Add(_Item_History_transaction);
                _context.SaveChanges();
            }
        }

        void Update_Item_Qty_And_Finshed(Int64 Sale_Master_Code, double Qty, DateTime OrderDate, Int64 History_Id, Int64 Item_Id)
        {
            Item_History item_History;
            item_History = context.Item_History.SingleOrDefault(Item => Item.Id == History_Id);
            item_History.Current_Qty_Now -=  Qty;
            item_History.Is_Used = (bool)true;
            item_History.IsFinshed = (bool)true;
            context.SaveChanges();


            Item_History_transaction _Item_History_transaction = new Item_History_transaction()
            {
                OrderDate = OrderDate,
                Item_History_Id = History_Id,
                CreatedDate = DateTime.Now,
                Trans_In = 0,
                Trans_Out = Qty,
                IsDeleted = 0,
                Item_Id = Item_Id,
                SaleMasterCode = Sale_Master_Code

            };
            context.Item_History_transaction.Add(_Item_History_transaction);
            context.SaveChanges();



        }


        private void timer1_Tick_1(object sender, EventArgs e)
        {
            //var x = DateTime.Now.ToString("hh:mm:ss");
            //var xx = DateTime.Now.ToString("MM/dd/yyyy");
            //dtEntryDate.Text = x + " " + xx;
        }

    
        void New()
        {

            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64? MaxCode = context.SaleMasters.Where(x => x.ShiftCode == ShiftCode && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }
            lblSaleMasterId.Text = MaxCode.ToString();
            gcSaleDetail.Enabled = true;
            


            btnEdite.Enabled = false;
            btnSave.Enabled = true;
            

      

            lblFinalTotal.Text = "0";
            lblFinalBeforDesCound.Text = "0";
            lblItemQty.Text = "0";
      
            lblDiscount.Text = "0";


            slkCustomers.Text = "";
            SlkPaymentsType.EditValue = 1;
            var usercode = st.User_Code();
            slkEmployees.EditValue = context.Employee_View.Where(user => user.IsDeleted == 0 && user.Employee_Code == usercode).ToList().FirstOrDefault().Employee_Code;



            slkCustomers.Enabled = true;
           SlkPaymentsType.Enabled = true;
           slkEmployees.Enabled = true;
            btnser.Enabled = true;
            btnAddItems.Enabled = true;
            //dtEntryDate.DateTime = DateTime.Now;
            while (gvSaleDetail.RowCount > 0)
            {
                gvSaleDetail.SelectAll();
                gvSaleDetail.DeleteSelectedRows();
                gcSaleDetail.RefreshDataSource() ;
           
            }

            this.Status = "New";

        }
        private void gcSaleDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
              {

                     if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                     {
                    frmPerfumSearchItems f3 = new frmPerfumSearchItems();
                         f3.ShowDialog();
                     }
                     else if (e.KeyCode == Keys.Escape)
                     {

                         Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                         var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
                         if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                         {


                             MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                             return;


                         }


                         else if (this.Status == "New")
                         {

                
                             SaveSaleDetail();
                            
                            






                         }
                         else if (this.Status == "Old")
                         {

                             
                             SaveSaleDetail();
                        
                            

                         }

                     }
                 }
                 catch(Exception ex)
                 {
                     var x = ex.Message;
                 }
            }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Int64 User_Code = st.User_Code();

            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnser"));
            if (result)
            {
                frmPerfumInvoiceSearch frm = new frmPerfumInvoiceSearch();
                frm.Show();

            }
            else
            {
                frmAdmin frm = new frmAdmin();
                frm.Show();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {

               
                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;

                    decimal finaltotal = Convert.ToInt64(lblFinalTotal.Text);

                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                        return;


                    }

                if (String.IsNullOrWhiteSpace(slkCustomers.Text))
                {
                    MaterialMessageBox.Show("برجاء اختيار العميل", MessageBoxButtons.OK);
                    return;

                }



                if (String.IsNullOrWhiteSpace(slkEmployees.Text))
                {


                    MaterialMessageBox.Show("برجاء اختيار اسم البائع", MessageBoxButtons.OK);
              
                    return;


                }


                else if (finaltotal < 0)
                    {

                        MaterialMessageBox.Show("لا يمكن قبول قيمة فاتورة اقل من الصفر", MessageBoxButtons.OK);
                        return;
                    }

                 
                        SaveSaleDetail();


                     

                
            }
            catch (Exception ex)
            {
                var x = ex.Message;

                MaterialMessageBox.Show(ex.Message.ToString(), MessageBoxButtons.OK);
            }
            //Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
            //var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
            //if (GetDataFromGrid.Count <= 0 || GetDataFromGrid == null)
            //{


            //    MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
            //    return;


            //}

            //else
            //{

            //    var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
            //    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
            //    if (Master.Count == 0 || Detail.Count == 0)
            //    {

            //        MaterialMessageBox.Show("!برجاء اختيار فاتوره للطباعة", MessageBoxButtons.OK);
            //        return;
            //    }
            //    else
            //    {

            //        Report rpt = new Report();
            //        //rpt.Load(@"Reports\SaleInvoice.frx");
            //        rpt.RegisterData(Master, "Header");
            //        rpt.RegisterData(Detail, "Lines");
            //        //  rpt.PrintSettings.ShowDialog = false;
            //        // rpt.Design();
            //       rpt.Print();

            //    }

            //}


            //btnEdite.Enabled = false;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Int64 User_Code = st.User_Code();

            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnDiscount"));
            if (result)
            {
                frmPerfumDiscount frm = new frmPerfumDiscount();
                frm.lblFinalAmount = Convert.ToDecimal(lblFinalBeforDesCound.Text);
                frm.ShowDialog();

            }
            else
            {
                frmAdmin frm = new frmAdmin();
                frm.ShowDialog();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmPerfumSearchItems frm = new frmPerfumSearchItems();
            frm.ShowDialog();
        }




        private void txtParCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmPerfumSearchItems f3 = new frmPerfumSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    try
                    {


                        Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                        var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;

                        decimal finaltotal = Convert.ToInt64(lblFinalTotal.Text);

                        if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                        {


                            MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                            return;


                        }

                        if (String.IsNullOrWhiteSpace(slkCustomers.Text))
                        {
                            MaterialMessageBox.Show("برجاء اختيار العميل", MessageBoxButtons.OK);
                            return;

                        }



                        if (String.IsNullOrWhiteSpace(slkEmployees.Text))
                        {


                            MaterialMessageBox.Show("برجاء اختيار اسم البائع", MessageBoxButtons.OK);

                            return;


                        }


                        else if (finaltotal < 0)
                        {

                            MaterialMessageBox.Show("لا يمكن قبول قيمة فاتورة اقل من الصفر", MessageBoxButtons.OK);
                            return;
                        }


                        else if (this.Status == "New")
                        {



                            SaveSaleDetail();


                        }
                        else if (this.Status == "Old")
                        {


                            SaveSaleDetail();


                        }



                    }
                    catch (Exception ex)
                    {
                        var x = ex.Message;

                        MaterialMessageBox.Show(ex.Message.ToString(), MessageBoxButtons.OK);
                    }


                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        //private void txtParCode_KeyUp_1(object sender, KeyEventArgs e)
        //{
        //    // Get GrideData
        //    var RowCount = gvSaleDetail.RowCount;
        //    if (RowCount > 0)
        //    {
        //        try
        //        {
        //            List<SaleDetailView> gcData = gcSaleDetail.DataSource as List<SaleDetailView>;

        //            var item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0);

        //            bool TestUpdate = gcData.Any(User => User.ParCode == txtParCode.Text);

        //            double Qty = 1;

        //            if (TestUpdate)
        //            {
                        
        //                double sum = 0;
        //                gcData.ForEach(xx =>
        //                {
        //                    sum += Convert.ToDouble(xx.Total);
        //                });

        //                gcSaleDetail.DataSource = gcData;
        //                gcSaleDetail.RefreshDataSource();

        //                lblItemQty.Text = gvSaleDetail.RowCount.ToString();
        //                lblFinalBeforDesCound.Text = sum.ToString();

        //                lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                       
        //            }


        //            else
        //            {



        //                ItemCardView ii = context.ItemCardViews.Where(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0).FirstOrDefault();
        //                if (ii == null)
        //                {
        //                    return;

        //                }

        //                SaleDetailView _SaleDetailView = new SaleDetailView()
        //                {
        //                    ItemCode = ii.ItemCode,
        //                    EntryDate = DateTime.Now,
        //                    Price = Convert.ToDouble(ii.Price),
        //                    Qty = 1,
        //                    Total = Convert.ToDouble(1) * Convert.ToDouble(ii.Price),
        //                    Name = ii.Name,
        //                    UnitCode = ii.UnitCode,
        //                    Operation_Type_Id = 2,
        //                    CategoryCode = ii.CategoryCode,
        //                    ParCode = ii.ParCode



        //                };

        //                gcData.Add(_SaleDetailView);
        //                gcSaleDetail.DataSource = gcData;
        //                double sum = 0;
        //                gcData.ForEach(x =>
        //                {
        //                    sum += Convert.ToDouble(x.Total);
        //                });
        //                gcSaleDetail.RefreshDataSource();

        //                lblItemQty.Text = gvSaleDetail.RowCount.ToString();
        //                lblFinalBeforDesCound.Text = sum.ToString();

        //                lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
        //                txtParCode.ResetText();


        //                txtParCode.Focus();

        //            }


        //        }
        //        catch
        //        {

        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            List<SaleDetailView> gcdata = new List<SaleDetailView>();


        //            ItemCardView ii = context.ItemCardViews.Where(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0).FirstOrDefault();
        //            if (ii == null)
        //            {

        //                return;

        //            }



        //            SaleDetailView _SaleDetailView = new SaleDetailView()
        //            {
        //                ItemCode = ii.ItemCode,
        //                EntryDate = DateTime.Now,
        //                Price = Convert.ToDouble(ii.Price),
        //                Qty = 1,
        //                Total = Convert.ToDouble(1) * Convert.ToDouble(ii.Price),
        //                Name = ii.Name,
        //                UnitCode = ii.UnitCode,
        //                CategoryCode = ii.CategoryCode,
        //                ParCode = ii.ParCode,
        //                Operation_Type_Id = 2



        //            };

        //            gcdata.Add(_SaleDetailView);
        //            gcSaleDetail.DataSource = gcdata;
        //            double sum = 0;
        //            gcdata.ForEach(x =>
        //            {
        //                sum += Convert.ToDouble(x.Total);
        //            });


        //            lblFinalBeforDesCound.Text = sum.ToString();

        //            lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
        //            lblItemQty.Text = (RowCount + 1).ToString();
        //            txtParCode.ResetText();
        //            //  txtQty.ResetText();
        //            txtParCode.Focus();
        //            //  btnAdd.Enabled = true;
        //        }
        //        catch
        //        {
        //            if (gvSaleDetail.RowCount > 0)
        //            {
        //                //  btnAdd.Enabled = true;

        //            }
        //            else
        //            {

        //                //    btnAdd.Enabled = false;

        //            }
        //        }
        //    }
        //    //txtParCode.Focus();
        //}

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var RowCount = gvSaleDetail.RowCount;
            var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

            List<SaleDetailView> gcData = gcSaleDetail.DataSource as List<SaleDetailView>;
            gcData.Remove(FocusRow);
            gcSaleDetail.DataSource = gcData;
            double sum = 0;
            gcData.ForEach(x =>
            {
                sum += Convert.ToDouble(x.Total);
            });



            gcSaleDetail.RefreshDataSource();
 



            lblFinalBeforDesCound.Text = sum.ToString();

            lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
            lblItemQty.Text = (RowCount - 1).ToString();
          
        }

        private void btnEdite_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmPerfumSearchItems f3 = new frmPerfumSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    try
                    {


                        Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                        var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;

                        decimal finaltotal = Convert.ToInt64(lblFinalTotal.Text);

                        if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                        {


                            MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                            return;


                        }

                        if (String.IsNullOrWhiteSpace(slkCustomers.Text))
                        {
                            MaterialMessageBox.Show("برجاء اختيار العميل", MessageBoxButtons.OK);
                            return;

                        }



                        if (String.IsNullOrWhiteSpace(slkEmployees.Text))
                        {


                            MaterialMessageBox.Show("برجاء اختيار اسم البائع", MessageBoxButtons.OK);

                            return;


                        }


                        else if (finaltotal < 0)
                        {

                            MaterialMessageBox.Show("لا يمكن قبول قيمة فاتورة اقل من الصفر", MessageBoxButtons.OK);
                            return;
                        }


                        else if (this.Status == "New")
                        {



                            SaveSaleDetail();


                        }
                        else if (this.Status == "Old")
                        {


                            SaveSaleDetail();


                        }



                    }
                    catch (Exception ex)
                    {
                        var x = ex.Message;

                        MaterialMessageBox.Show(ex.Message.ToString(), MessageBoxButtons.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        
        //private void slkItem_EditValueChanged_1(object sender, EventArgs e)
        //{
        //    if (slkItem.EditValue != null)
        //    {
        //        List<SaleDetailView> gcData = new List<SaleDetailView>();


        //        var grd = gcSaleDetail.DataSource as List<SaleDetailView>;

        //        if (grd != null)
        //        {
        //            gcData = grd;

        //        }



        //        var itemcode = Convert.ToInt64(slkItem.EditValue);

        //        var RowCount = gvSaleDetail.RowCount;


        //        ItemCardView ii = context.ItemCardViews.Where(x => x.ItemCode == itemcode && x.IsDeleted == 0).FirstOrDefault();
        //        if (ii == null)
        //        {

        //            return;

        //        }



        //        SaleDetailView _SaleDetailView = new SaleDetailView()
        //        {
        //            ItemCode = ii.ItemCode,
        //            EntryDate = DateTime.Now,
        //            Price = Convert.ToDouble(ii.Price),
        //            Qty = 1,
        //            Total = Convert.ToDouble(1) * Convert.ToDouble(ii.Price),
        //            Name = ii.Name,
        //            UnitCode = ii.UnitCode,
        //            CategoryCode = ii.CategoryCode,
        //            ParCode = ii.ParCode,
        //            Operation_Type_Id = 2




        //        };


        //        gcData.Add(_SaleDetailView);




        //        gcSaleDetail.DataSource = gcData;
        //        gcSaleDetail.RefreshDataSource();
        //        double sum = 0;
        //        gcData.ForEach(x =>
        //        {
        //            sum += Convert.ToDouble(x.Total);
        //        });


        //        lblFinalBeforDesCound.Text = sum.ToString();

        //        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
        //        lblItemQty.Text = (RowCount + 1).ToString();
        //        slkItem.EditValue = null;

        //    }

        //}

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            frmPerfumSearchItems frm = new frmPerfumSearchItems();
            frm.ShowDialog();
        }



        public void FillSlkEmployees()
        {
            var result = context.Employee_View.Where(user => user.IsDeleted == 0).ToList();
            slkEmployees.Properties.DataSource = result;
            slkEmployees.Properties.ValueMember = "Employee_Code";
            slkEmployees.Properties.DisplayMember = "Employee_Name";

            var usercode = st.User_Code();
            slkEmployees.EditValue = context.Employee_View.Where(user => user.IsDeleted == 0&&user.Employee_Code == usercode).ToList().FirstOrDefault().Employee_Code;
        //    slkEmployees.Text = context.Employee_View.Where(user => user.IsDeleted == 0).ToList().FirstOrDefault().;

        }


        public void FillSlkCustomers()
        {
            var result = context.Customer_View.Where(Customer => Customer.IsDeleted == 0).ToList();
            slkCustomers.Properties.DataSource = result;
            slkCustomers.Properties.ValueMember = "Customer_Code";
            slkCustomers.Properties.DisplayMember = "Customer_Name";
        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            frmAddCustomer frm = new frmAddCustomer();
            frm.ShowDialog();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            List<string> textlist = new List<string>();
           string text = "";

            var customercode = Convert.ToInt64(slkCustomers.EditValue);
            context.SaleDetailViews.Where(x => x.CustomerCode == customercode && x.CategoryCode == 1).Distinct().ForEach(x=> {


                textlist.Add(x.Name.ToString());
               
            });

            textlist.Distinct().ForEach(x => {
                text +=  x + " / ";
            });





            frmPerfumCustomerHistory frm = new frmPerfumCustomerHistory();

            frm.txtHistory.Text = text;

            frm.ShowDialog();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            frmAddCustomer frm = new frmAddCustomer();
            frm.ShowDialog();
        }

        private void slkCustomers_EditValueChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(slkCustomers.Text))
            {

                btnCustomerHistory.Enabled = false;


            }
            else
            {
                btnCustomerHistory.Enabled = true;
            }
        }

     
    }


}
