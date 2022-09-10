using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;

using System.Linq;

using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Extensions;
using DevExpress.XtraTab;

using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;

using DataRep;
using FastReport;
using PointOfSaleSedek.Model;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmCafeSales : Form
    {
        SaleEntities context = new SaleEntities();
        public String Status = "New";
        public bool SearchStatus = false;
        Static st = new Static();



        public frmCafeSales()
        {
            InitializeComponent();
            NewForLoad();
            txtParCode.Focus();

           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQty.ResetText();
            txtParCode.Focus();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {

            this.Close();
        }
        //New Data
        //void New()
        //{
        //    HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
        //    HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
        //    Int64? MaxCode = context.SaleMasters.Where(x => x.EntryDate == DateTime.Today && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
        //    if (MaxCode == null || MaxCode == 0)
        //    {
        //        MaxCode = 1;
        //    }
        //    //  HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
        //    gcSaleDetail.Enabled = true;
        //    tabItems.Enabled = true;


        //    btnEdite.Enabled = false;
        //    btnPrint.Enabled = false;
        //    btnAdd.Enabled = false;

        //    lblSaleMasterId.Text = MaxCode.ToString();
        //    lblCurrency.Text = "جنية مصري";
        //    lblFinalTotal.Text = "0";
        //    lblFinalBeforDesCound.Text = "0";
        //    lblItemQty.Text = "0";
        //    lblUserName.Text = "المدير";
        //    lblDiscount.Text = "0";
        //    dtEntryDate.DateTime = DateTime.Now;
        //    while (gvSaleDetail.RowCount > 0)
        //    {
        //        gvSaleDetail.SelectAll();
        //        gvSaleDetail.DeleteSelectedRows();
        //    }
        //    txtParCode.ResetText();
        //    txtParCode.Focus();
        //    this.Status = "New";
        //    //gcSaleDetail.DataSource = null;
        //    //gcSaleDetail.RefreshDataSource() ;

        //}


        void New()
        {

                HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
             HelperClass.HelperClass.EnableControls(tableLayoutPanel1);


            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64? MaxCode = context.SaleMasters.Where(x => x.ShiftCode == ShiftCode && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }
            HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
            gcCafeSaleDetail.Enabled = true;
            txtParCode.Enabled = true;
              tabItems.Enabled = true;

            btnEdite.Enabled = false;
            btnPrint.Enabled = false;
            btnDiscount.Enabled = true;
            btnSave.Enabled = true;

            lblSaleMasterId.Text = MaxCode.ToString();

            lblFinalTotal.Text = "0";
            lblFinalBeforDesCound.Text = "0";
            lblItemQty.Text = "0";

            lblDiscount.Text = "0";
            //dtEntryDate.DateTime = DateTime.Now;
            while (gvSaleDetail.RowCount > 0)
            {
                gvSaleDetail.SelectAll();
                gvSaleDetail.DeleteSelectedRows();
                gcCafeSaleDetail.RefreshDataSource();

            }


            //slkCustomers.Text = "";
            //slkCustomers.SelectedText = "";
            //slkCustomers.Enabled = true;
            //slkCustomers.EditValue = 0;
            //slkCustomers.Reset();
            //slkCustomers.Refresh();
            txtParCode.ResetText();
            Int64 User_Code = st.User_Code();

            var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



            if (result.Any(xd => xd.Tab_Name == "btnser"))
            {
               // btnser.Enabled = true;

            }
            else
            {
             //   btnser.Enabled = false;
            }


            if (result.Any(xd => xd.Tab_Name == "btnDiscount"))
            {
                btnDiscount.Enabled = true;

            }
            else
            {
                btnDiscount.Enabled = false;
            }


            AddButtons();
            txtParCode.Focus();
            this.Status = "New";


        }

        //void NewForLoad()
        //{
        //    HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
        //    Int64? MaxCode = context.SaleMasters.Where(x => x.EntryDate == DateTime.Today && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
        //    if (MaxCode == null || MaxCode == 0)
        //    {
        //        MaxCode = 1;
        //    }
        //    lblSaleMasterId.Text = MaxCode.ToString();
        //    lblCurrency.Text = "جنية مصري";
        //    lblFinalTotal.Text = "0";
        //    lblFinalBeforDesCound.Text = "0";
        //    lblItemQty.Text = "0";
        //    lblDiscount.Text = "0";
        //    lblUserName.Text = "المدير";
        //    dtEntryDate.DateTime = DateTime.Now;
        //    while (gvSaleDetail.RowCount > 0)
        //    {
        //        gvSaleDetail.SelectAll();
        //        gvSaleDetail.DeleteSelectedRows();
        //    }

        //    gcSaleDetail.DataSource = null;
        //    gcSaleDetail.RefreshDataSource();
        //    btnPrint.Enabled = false;
        //    btnSave.Enabled = false;

        //    context.Categories.ForEach(x => tabItems.TabPages.Add(x.CategoryName));
        //    AddButtons();
        //    txtParCode.Focus();
        //}


        void NewForLoad()
        {
            HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
            //FillSlkPaymentType();
            //FillSlkCustomers();


            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64? MaxCode = context.SaleMasters.Where(x => x.ShiftCode == ShiftCode && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }

            while (gvSaleDetail.RowCount > 0)
            {
                gvSaleDetail.SelectAll();
                gvSaleDetail.DeleteSelectedRows();
            }

            lblSaleMasterId.Text = MaxCode.ToString();
             lblCurrency.Text = "جنية مصري";
            lblFinalTotal.Text = "0";
            lblFinalBeforDesCound.Text = "0";
            lblItemQty.Text = "0";
            lblDiscount.Text = "0";
            lblUserName.Text = "المدير";
            dtEntryDate.Text = context.Employee_View.Where(x => x.Employee_Code == UserCode).First().Employee_Name.ToString() ?? "";

            while (gvSaleDetail.RowCount > 0)
            {
                gvSaleDetail.SelectAll();
                gvSaleDetail.DeleteSelectedRows();
            }

            gcCafeSaleDetail.DataSource = null;
            gcCafeSaleDetail.RefreshDataSource();
            btnPrint.Enabled = false;

            Int64 User_Code = st.User_Code();

            var result = context.Auth_View.Where(View => View.User_Code == User_Code && (View.User_IsDeleted == 0)).ToList();



            //if (result.Any(xd => xd.Tab_Name == "btnser"))
            //{
            //    btnser.Enabled = true;

            //}
            //else
            //{
            //    btnser.Enabled = false;
            //}


            if (result.Any(xd => xd.Tab_Name == "btnDiscount"))
            {
                btnDiscount.Enabled = true;

            }
            else
            {
                btnDiscount.Enabled = false;
            }
            context.Categories.ForEach(x => tabItems.TabPages.Add(x.CategoryName));
            AddButtons();
            txtParCode.Focus();



        }


        //Save SaleMaster Date
        //public void SaveSaleMaster()

        //{

        //    Int64 UserCode = st.User_Code();
        //    var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx=>xx.Shift_Code).SingleOrDefault();

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
        //            SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);

        //            _SaleMasters.UserCode = st.User_Code();
        //            context.SaveChanges();



        //        }


        //    }
        //    else
        //    {

        //        SaleMaster _SaleMaster = new SaleMaster()
        //        {

        //            EntryDate = DateTime.Now,


        //            Discount = double.Parse(lblDiscount.Text),
        //            TotalBeforDiscount = double.Parse(lblFinalBeforDesCound.Text),
        //            FinalTotal = double.Parse(lblFinalTotal.Text),
        //             ShiftCode = ShiftCode,

        //        QtyTotal = double.Parse(lblItemQty.Text),
        //            SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
        //            Operation_Type_Id = 2,

        //            UserCode = UserCode,

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
                    _SaleMasters.Operation_Type_Id = 2;
                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);

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
                  //  Payment_Type = Int64.Parse(slkPaymentType.EditValue.ToString()),
                     Payment_Type = 1,
                    UserIdTakeOrder = UserCode,
                    //Customer_Code = Int64.Parse(slkCustomers.EditValue.ToString()),
                    Customer_Code = 0,
                    QtyTotal = double.Parse(lblItemQty.Text),
                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
                    Operation_Type_Id = 2,

                    UserCode = UserCode,

                };
                context.SaleMasters.Add(_SaleMaster);
                context.SaveChanges();

            }




        }


        public void SaveSaleDetail()
        {


            //if (slkPaymentType.EditValue == null || slkPaymentType.EditValue.ToString() == "0" || slkPaymentType.Text.Trim() == "")
            //{
            //    MaterialMessageBox.Show("برجاء اختيار طريقة الدفع", MessageBoxButtons.OK);
            //    return;
            //}



            var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;

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
                    //using (SaleEntities ContVald = new SaleEntities())
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
                        __Item_History_transaction = context.Item_History_transaction.Where(Item =>Item.shiftCode == ShiftCode  && Item.SaleMasterCode == x.SaleMasterCode && Item.Item_Id == x.Item_Id && Item.IsDeleted == 0).ToList();
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
                            shiftCode =  ShiftCode,
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
                    Report rpt = new Report();
                    rpt.Load(@"Reports\SaleInvoice.frx");
                    rpt.RegisterData(Master, "Header");
                    rpt.RegisterData(Detail, "Lines");
                   rpt.PrintSettings.ShowDialog = false;
                    //rpt.Design();
                    rpt.Print();



                }





            }
            else
            {
                #region
                //using (SaleEntities ContVald = new SaleEntities())
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
                    gcCafeSaleDetail.RefreshDataSource();

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
                var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.Shift_Code == ShiftCode  select a).ToList();
                var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == ShiftCode select a).ToList();

                Report rpt = new Report();
                rpt.Load(@"Reports\SaleInvoice.frx");
                rpt.RegisterData(Master, "Header");
                rpt.RegisterData(Detail, "Lines");
                rpt.PrintSettings.ShowDialog = false;
                //rpt.Design();
                rpt.Print();


            }




        }

        //public void SaveSaleDetail()
        //{
        //    List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();
        //    var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
        //    Int64 UserCode = st.User_Code();
        //    var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();

        //    if (this.Status == "Old")
        //    {
        //        Int64 SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);
        //        bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Operation_Type_Id==2 && SaleMaster.EntryDate == DateTime.Today);

        //        if (TestUpdate)
        //        {

        //            //SaleDetail _SaleDetails;
        //            var SaleDetails = context.SaleDetails.Where(Master => Master.SaleMasterCode == SaleMasterCode && Master.Operation_Type_Id ==2 && Master.EntryDate == DateTime.Today);




        //            using (SaleEntities context2 = new SaleEntities())
        //            {
        //                var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == SaleMasterCode && w.EntryDate == DateTime.Today);
        //                context2.SaleDetails.RemoveRange(Details);
        //                context2.SaveChanges();
        //            }


        //            foreach (var item in GetDataFromGrid)
        //            {
        //                SaleDetail _SaleDetail = new SaleDetail()
        //                {
        //                    ItemCode = item.ItemCode,
        //                    Price = item.Price,
        //                    Qty = item.Qty,
        //                    Total = item.Total,
        //                    EntryDate = DateTime.Now,
        //                    SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
        //                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),

        //                    Operation_Type_Id = 2,
        //                     UserId =  st.User_Code()



        //            };

        //                ArryOfSaleDetail.Add(_SaleDetail);

        //            }


        //            context.SaleDetails.AddRange(ArryOfSaleDetail);
        //            context.SaveChanges();
        //          //  MaterialMessageBox.Show("تم التعديل", MessageBoxButtons.OK);




        //        }





        //    }
        //    else
        //    {



        //        foreach (var item in GetDataFromGrid)
        //        {
        //            SaleDetail _SaleDetail = new SaleDetail()
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



        //            };

        //            ArryOfSaleDetail.Add(_SaleDetail);

        //        }


        //        context.SaleDetails.AddRange(ArryOfSaleDetail);
        //        context.SaveChanges();
        //      //  MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);

        //    }




        //}

        public void AddButtons()
        {
            int i = 1;
            foreach (XtraTabPage tb in tabItems.TabPages)
            {
                var item = context.ItemCardViews.Where(x => x.CategoryCode == i && x.IsDeleted == 0 && x.AddItem == true).ToList<ItemCardView>();
                tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;
       
                item.ForEach(c =>
                {
                    //var itemCountDetailIn = context.SaleDetailViews.Where(x => x.ItemCode == c.ItemCode && x.Operation_Type_Id == 1 && x.IsDeleted == 0).Sum(u => (double?)u.Total);
                    //var itemCountDetailOut = context.SaleDetailViews.Where(x => x.ItemCode == c.ItemCode && x.Operation_Type_Id == 2 && x.IsDeleted == 0).Sum(u => (double?)u.Total);
                    //var FinalTotal = itemCountDetailIn - itemCountDetailOut;
                    SimpleButton b = new SimpleButton();
                    b.Size = new Size(129, 120);
                    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                    b.ImageOptions.Image = imageCollection1.Images[0];
                    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                    b.Appearance.BackColor = Color.SteelBlue;
                    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                    b.Appearance.BorderColor = Color.Firebrick;
                    b.Text = c.Name;
                    b.Tag = item;
                    b.Click += B_Click;
                    flp.Controls.Add(b);
                     
                });
                flp.AutoScroll = true;
                tb.Controls.Add(flp);
                i++;
            }
            txtParCode.Focus();
        }
        public void AddButtonsForSearch()
        {
            int i = 1;
            foreach (XtraTabPage tb in tabItems.TabPages)
            {
                var item = context.ItemCardViews.Where(x => x.CategoryCode == i && x.IsDeleted == 0 && x.AddItem == true).ToList<ItemCardView>();
                tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;
             
                item.ForEach(c =>
                {
                    //var itemCountDetailIn = context.SaleDetailViews.Where(x => x.ItemCode == c.ItemCode && x.Operation_Type_Id == 1 && x.IsDeleted == 0).Sum(u => (double?)u.Total);
                    //var itemCountDetailOut = context.SaleDetailViews.Where(x => x.ItemCode == c.ItemCode && x.Operation_Type_Id == 2 && x.IsDeleted == 0).Sum(u => (double?)u.Total);
                    //var FinalTotal = itemCountDetailIn - itemCountDetailOut;
                    SimpleButton b = new SimpleButton();
                    b.Size = new Size(120, 120);
                    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                    b.ImageOptions.Image = imageCollection1.Images[0];
                    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                    b.Appearance.BackColor = Color.SteelBlue;
                    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                    b.Appearance.BorderColor = Color.Firebrick;
                    b.Text = c.Name;
                    b.Tag = item;
                    b.Click += B_Click;
                    flp.Controls.Add(b);

                });
                flp.AutoScroll = true;
                tb.Controls.Add(flp);
                i++;
            }
            txtParCode.Focus();
        }

        private void B_Click(object sender, EventArgs e)
        {
            // Get GrideData
            var RowCount = gvSaleDetail.RowCount;
            if (RowCount > 0)
            {
                try
                {
                    List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;

                    SimpleButton b = (SimpleButton)sender;

                    bool TestUpdate = gcData.Any(User => User.Name == b.Text);

                    double Qty = 0;
                    if (string.IsNullOrWhiteSpace(txtQty.Text))
                    {
                        Qty = 1;
                    }
                    else
                    {
                        Qty = double.Parse(txtQty.Text);

                    }

                    if (TestUpdate)
                    {
                        gcData.ForEach(x =>
                        {
                            if (x.Name == b.Text)
                            {
                                x.Qty += Qty;
                                x.Total = x.Qty * x.Price;

                            }
                        });
                        double sum = 0;
                        gcData.ForEach(xx =>
                        {
                            sum += Convert.ToDouble(xx.Total);
                        });

                        gcCafeSaleDetail.DataSource = gcData;
                        gcCafeSaleDetail.RefreshDataSource();

                        lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                        lblFinalBeforDesCound.Text = sum.ToString();

                        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                        txtParCode.ResetText();
                        txtQty.ResetText();
                        txtParCode.Focus();
                        btnSave.Enabled = true;
                    }


                    else
                    {



                        ItemCard ii = context.ItemCards.Where(x => x.Name == b.Text && x.IsDeleted == 0).FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(txtQty.Text))
                        {

                            txtQty.Text = "1";

                        }

                        SaleDetailView _SaleDetailView = new SaleDetailView()
                        {
                            ItemCode = ii.ItemCode,
                            EntryDate = DateTime.Now,
                            Price = Convert.ToDouble(ii.Price),
                            Qty = double.Parse(txtQty.Text),
                            Total = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(ii.Price),
                            Name = ii.Name,
                            Operation_Type_Id = 2,
                            UnitCode = ii.UnitCode,
                            CategoryCode = ii.CategoryCode,
                            ParCode = ii.ParCode,




                        };

                        gcData.Add(_SaleDetailView);
                        gcCafeSaleDetail.DataSource = gcData;
                        double sum = 0;
                        gcData.ForEach(x =>
                        {
                            sum += Convert.ToDouble(x.Total);
                        });
                        gcCafeSaleDetail.RefreshDataSource();

                        lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                        lblFinalBeforDesCound.Text = sum.ToString();

                        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                        txtParCode.ResetText();
                        txtQty.ResetText();
                        txtParCode.Focus();
                        btnSave.Enabled = true;

                    }


                }
                catch
                {
                    if (gvSaleDetail.RowCount > 0)
                    {
                        btnSave.Enabled = true;

                    }
                    else
                    {

                        btnSave.Enabled = false;

                    }
                }
            }
            else
            {



                try
                {
                    List<SaleDetailView> gcdata = new List<SaleDetailView>();
                    SimpleButton b = (SimpleButton)sender;
                    var ccc = b.Tag;

                    ItemCard ii = context.ItemCards.Where(x => x.Name == b.Text && x.IsDeleted == 0).FirstOrDefault();



                    if (string.IsNullOrWhiteSpace(txtQty.Text))
                    {

                        txtQty.Text = "1";

                    }

                    SaleDetailView _SaleDetailView = new SaleDetailView()
                    {
                        ItemCode = ii.ItemCode,
                        EntryDate = DateTime.Now,
                        Price = Convert.ToDouble(ii.Price),
                        Qty = double.Parse(txtQty.Text),
                        Total = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(ii.Price),
                        Name = ii.Name,
                        Operation_Type_Id = 2,
                        UnitCode = ii.UnitCode,
                        CategoryCode = ii.CategoryCode,
                        ParCode = ii.ParCode,
                        SaleMasterCode = 1,



                    };

                    gcdata.Add(_SaleDetailView);
                    gcCafeSaleDetail.DataSource = gcdata;
                    double sum = 0;
                    gcdata.ForEach(x =>
                    {
                        sum += Convert.ToDouble(x.Total);
                    });


                    lblFinalBeforDesCound.Text = sum.ToString();

                    lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                    lblItemQty.Text = (RowCount + 1).ToString();
                    txtParCode.ResetText();
                    txtQty.ResetText();
                    txtParCode.Focus();
                    btnSave.Enabled = true;
                }
                catch
                {
                    if (gvSaleDetail.RowCount > 0)
                    {
                        btnSave.Enabled = true;

                    }
                    else
                    {

                        btnSave.Enabled = false;

                    }
                }





            }


            txtParCode.Focus();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn1.Text;
            txtParCode.Focus();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn2.Text;
            txtParCode.Focus();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn3.Text;
            txtParCode.Focus();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn4.Text;
            txtParCode.Focus();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn5.Text;
            txtParCode.Focus();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn6.Text;
            txtParCode.Focus();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn7.Text;
            txtParCode.Focus();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn8.Text;
            txtParCode.Focus();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn9.Text;
            txtParCode.Focus();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtQty.Text += btn0.Text;
            txtParCode.Focus();
        }



        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
            //txtParCode.Focus();

        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            txtQty.Text += btnComma.Text;
            txtParCode.Focus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtQty.Text))
            {

                txtQty.Text = txtQty.Text.Remove(txtQty.Text.Length - 1, 1);
                txtParCode.Focus();
            }
        }



        private void txtParCode_KeyUp(object sender, KeyEventArgs e)
        {
            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            // Get GrideData
            var RowCount = gvSaleDetail.RowCount;
            if (RowCount > 0)
            {
                try
                {
                  
                    List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;

                    var item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0);

                    bool TestUpdate = gcData.Any(User => User.ParCode == txtParCode.Text);

                    double Qty = 0;
                    if (string.IsNullOrWhiteSpace(txtQty.Text))
                    {
                        Qty = 1;
                    }
                    else
                    {
                        Qty = double.Parse(txtQty.Text);

                    }

                    if (TestUpdate)
                    {
                        gcData.ForEach(x =>
                        {
                            if (x.ParCode == txtParCode.Text)
                            {
                                x.Qty += Qty;
                                x.Total = x.Qty * x.Price;

                            }
                        });
                        double sum = 0;
                        gcData.ForEach(xx =>
                        {
                            sum += Convert.ToDouble(xx.Total);
                        });

                        gcCafeSaleDetail.DataSource = gcData;
                        gcCafeSaleDetail.RefreshDataSource();

                        lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                        lblFinalBeforDesCound.Text = sum.ToString();

                        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                        txtParCode.ResetText();
                        txtQty.ResetText();
                        txtParCode.Focus();
                        btnSave.Enabled = true;
                    }


                    else
                    {

                        //var item = context.ItemCardViews.Where(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0);

                        ItemCardView ii = context.ItemCardViews.Where(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0).FirstOrDefault();
                        if (ii == null)
                        {
                            return;

                        }
                        if (string.IsNullOrWhiteSpace(txtQty.Text))
                        {

                            txtQty.Text = "1";

                        }

                        SaleDetailView _SaleDetailView = new SaleDetailView()
                        {
                            ItemCode = ii.ItemCode,
                            EntryDate = DateTime.Now,
                            Price = Convert.ToDouble(ii.Price),
                            Qty = double.Parse(txtQty.Text),
                            Total = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(ii.Price),
                            Name = ii.Name,
                            shiftCode = ShiftCode,
                            UnitCode = ii.UnitCode,
                            Operation_Type_Id = 2,
                            CategoryCode = ii.CategoryCode,
                            ParCode = ii.ParCode



                        };

                        gcData.Add(_SaleDetailView);
                        gcCafeSaleDetail.DataSource = gcData;
                        double sum = 0;
                        gcData.ForEach(x =>
                        {
                            sum += Convert.ToDouble(x.Total);
                        });
                        gcCafeSaleDetail.RefreshDataSource();

                        lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                        lblFinalBeforDesCound.Text = sum.ToString();

                        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                        txtParCode.ResetText();
                        txtQty.ResetText();

                        txtParCode.Focus();
                        btnSave.Enabled = true;

                    }


                }
                catch
                {
                    if (gvSaleDetail.RowCount > 0)
                    {
                        btnSave.Enabled = true;

                    }
                    else
                    {

                        btnSave.Enabled = false;

                    }
                }
            }
            else
            {
                try
                {
                    List<SaleDetailView> gcdata = new List<SaleDetailView>();


                    ItemCardView ii = context.ItemCardViews.Where(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0).FirstOrDefault();
                    if (ii == null)
                    {

                        return;

                    }


                    if (string.IsNullOrWhiteSpace(txtQty.Text))
                    {

                        txtQty.Text = "1";

                    }

                    SaleDetailView _SaleDetailView = new SaleDetailView()
                    {
                        ItemCode = ii.ItemCode,
                        EntryDate = DateTime.Now,
                        Price = Convert.ToDouble(ii.Price),
                        Qty = double.Parse(txtQty.Text),
                        Total = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(ii.Price),
                        Name = ii.Name,
                        shiftCode = ShiftCode,
                        UnitCode = ii.UnitCode,
                        CategoryCode = ii.CategoryCode,
                        ParCode = ii.ParCode,
                        Operation_Type_Id = 2



                    };

                    gcdata.Add(_SaleDetailView);
                    gcCafeSaleDetail.DataSource = gcdata;
                    double sum = 0;
                    gcdata.ForEach(x =>
                    {
                        sum += Convert.ToDouble(x.Total);
                    });


                    lblFinalBeforDesCound.Text = sum.ToString();

                    lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                    lblItemQty.Text = (RowCount + 1).ToString();
                    txtParCode.ResetText();
                    txtQty.ResetText();
                    txtParCode.Focus();
                    btnSave.Enabled = true;
                }
                catch
                {
                    if (gvSaleDetail.RowCount > 0)
                    {
                        btnSave.Enabled = true;

                    }
                    else
                    {

                        btnSave.Enabled = false;

                    }
                }
            }
            //txtParCode.Focus();
        }

       

        public void LoadCatagory()
        {

            var categories = context.Categories.Where(x => x.IsDeleted == 0);

            categories.ForEach(c =>
            {

                CmdCatagory.Properties.Items.Add(c.CategoryName);

            });
        }


        public void LoadItems()
        {

            var Items = context.ItemCardViews.Where(x => x.IsDeleted == 0 && x.AddItem == true);

            Items.ForEach(c =>
            {

                cmdItems.Properties.Items.Add(c.Name);

            });
        }
        private void frmSales_Load(object sender, EventArgs e)
        {
            timer1.Start();
            this.Status = "New";
            txtParCode.Focus();
            btnEdite.Enabled = false;
            btnSave.Enabled = false;
            LoadCatagory();
            LoadItems();
            var UserId = Convert.ToInt64(st.User_Code());
            var Check = context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
            if (!Check)
            {
                if (MaterialMessageBox.Show("برجاء اضافة وردية لهذا المستخدم", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }

            }

        }

        private void btnEdite_Click(object sender, EventArgs e)
        {
            this.Status = "Old";
            btnEdite.Enabled = false;
            btnSave.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            SaveSaleDetail();
            txtParCode.Focus();

            ////Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
            //var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
            //if (GetDataFromGrid.Count <= 0 || GetDataFromGrid == null)
            //{


            //    MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
            //    return;


            //}


            //else if (this.Status == "New")
            //{

            //    SplashScreenManager.ShowForm(typeof(WaitForm1));
            //    SaveSaleMaster();
            //    SaveSaleDetail();
            //    SplashScreenManager.CloseForm();

            //    //var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
            //    //var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

            //    //Report rpt = new Report();
            //    //rpt.Load(@"Reports\SaleInvoice.frx");
            //    //rpt.RegisterData(Master, "Header");
            //    //rpt.RegisterData(Detail, "Lines");
            //    ////  rpt.PrintSettings.ShowDialog = false;
            //    //// rpt.Design();
            //    //rpt.Print();






            //}
            //else if (this.Status == "Old")
            //{

            //    SaveSaleMaster();
            //    SaveSaleDetail();
            //    //var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
            //    //var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

            //    //Report rpt = new Report();
            //    //rpt.Load(@"Reports\SaleInvoice.frx");
            //    //rpt.RegisterData(Master, "Header");
            //    //rpt.RegisterData(Detail, "Lines");
            //    //// rpt.PrintSettings.ShowDialog = false;
            //    ////rpt.Design();
            //    //rpt.Print();

            //}

            //New();
        }

        private void btnEdite_Click_1(object sender, EventArgs e)
        {
            //frmInvoiceSearch frm = new frmInvoiceSearch();
            //frm.ShowDialog();
            gcCafeSaleDetail.Enabled = true;
            tabItems.Enabled = true;
            btnNew.Enabled = true;
            btnEdite.Enabled = false;
            BtnExit.Enabled = true;
            btnDiscount.Enabled = true;
            txtParCode.Focus();
            HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Int64 User_Code = st.User_Code();
            
            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnDiscount"));
            if (result)
            {
                frmCafeDiscount frm = new frmCafeDiscount();
                frm.lblFinalAmount = Convert.ToDecimal(lblFinalBeforDesCound.Text);
                frm.ShowDialog();

            }
            else
            {
                frmAdmin frm = new frmAdmin();
                frm.ShowDialog();
            }

 

 

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {


            Int64 UserCode = st.User_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            double FinalTotal = 0;
            double TotalDiscount = 0;



            Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
            var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
            var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.Shift_Code == ShiftCode  select a).ToList();
            var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == ShiftCode select a).ToList();


            if (GetDataFromGrid.Count <= 0 || GetDataFromGrid == null)
            {


                MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
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
                rpt.Load(@"Reports\SaleInvoice.frx");
                rpt.RegisterData(Master, "Header");
                rpt.RegisterData(_FinalTotalList, "FinalTotal");
                rpt.RegisterData(saleDetailViewVmList, "Lines");
                 rpt.PrintSettings.ShowDialog = false;
                //  rpt.Design();
                rpt.Print();

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
            //        //rpt.Print();

            //    }

            //}


            //btnEdite.Enabled = false;
            //btnSave.Enabled = true;
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var RowCount = gvSaleDetail.RowCount;
            var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

            List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
            gcData.Remove(FocusRow);
            gcCafeSaleDetail.DataSource = gcData;
            double sum = 0;
            gcData.ForEach(x =>
            {
                sum += Convert.ToDouble(x.Total);
            });



            gcCafeSaleDetail.RefreshDataSource();

            if (gvSaleDetail.RowCount <= 0)
            {
                btnSave.Enabled = false;

            }
            else
            {

                btnSave.Enabled = true;


            }



            lblFinalBeforDesCound.Text = sum.ToString();

            lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
            lblItemQty.Text = (RowCount - 1).ToString();
            txtParCode.ResetText();
            txtQty.ResetText();
            txtParCode.Focus();


        }



        private void gcSaleDetail_DataSourceChanged(object sender, EventArgs e)
        {
            txtParCode.ResetText();
        }

       

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //Int64 User_Code = st.User_Code();

            //var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnser"));
            //if (result)
            //{
            //    frmInvoiceSearch frm = new frmInvoiceSearch();
            //    frm.ShowDialog();

            //}
            //else
            //{
            //    frmAdmin frm = new frmAdmin();
            //    frm.ShowDialog();
            //}
            Int64 User_Code = st.User_Code();

            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnser"));
            if (result)
            {
                frmCafeInvoiceSearch frm = new frmCafeInvoiceSearch();
                frm.Show();

            }
            else
            {
                frmAdmin frm = new frmAdmin();
                frm.Show();
            }

        }

        private void repositoryItemButtonEdit2_Click(object sender, EventArgs e)
        {
            var RowCount = gvSaleDetail.RowCount;
            var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

            List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
            gcData.Remove(FocusRow);
            gcCafeSaleDetail.DataSource = gcData;
            double sum = 0;
            gcData.ForEach(x =>
            {
                sum += Convert.ToDouble(x.Total);
            });



            gcCafeSaleDetail.RefreshDataSource();

            if (gvSaleDetail.RowCount <= 0)
            {
                btnSave.Enabled = false;

            }
            else
            {

                btnSave.Enabled = true;


            }



            lblFinalBeforDesCound.Text = sum.ToString();

            lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
            lblItemQty.Text = (RowCount - 1).ToString();
            txtParCode.ResetText();
            txtQty.ResetText();
            txtParCode.Focus();



        }

        private void lblDiscount_TextChanged(object sender, EventArgs e)
        {
            lblFinalTotal.Text = (Convert.ToDecimal(lblFinalBeforDesCound.Text) - Convert.ToDecimal(lblDiscount.Text)).ToString();
        }

        private void lblFinalBeforDesCound_TextChanged(object sender, EventArgs e)
        {
            lblFinalTotal.Text = (Convert.ToDecimal(lblFinalBeforDesCound.Text) - Convert.ToDecimal(lblDiscount.Text)).ToString();
        }

       
        private void comboBoxEdit2_Properties_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(cmdItems.Text))
                {
                    
                    //foreach (XtraTabPage tb in tabItems.TabPages)
                    //{

                    //    ItemCardView item = context.ItemCardViews.Where(x =>  x.AddItem == true).FirstOrDefault();


                    //    tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                    //    FlowLayoutPanel flp = new FlowLayoutPanel();
                    //    flp.Dock = DockStyle.Fill;



                    //    SimpleButton b = new SimpleButton();
                    //    b.Size = new Size(120, 120);
                    //    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                    //    b.ImageOptions.Image = imageCollection1.Images[0];
                    //    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                    //    b.Appearance.BackColor = Color.SteelBlue;
                    //    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                    //    b.Appearance.BorderColor = Color.Firebrick;
                    //    b.Text = item.Name;
                    //    b.Tag = item;
                    //    b.Click += B_Click;
                    //    flp.Controls.Add(b);


                    //    flp.AutoScroll = true;

                    //    for (int i = 0; i < tb.TabControl.TabPages.Count; i++)
                    //        if (tb.TabControl.TabPages[i].Text == item.CategoryName)
                    //        {
                    //            tb.TabControl.SelectedTabPage = tb.TabControl.TabPages[i];
                    //            break;
                    //        }

                    //    //tabItems.TabPages.Where(x => x.Text== item.Name);
                    //    tb.Controls.Clear();
                    //    tb.Controls.Add(flp);

                    //}
                    //txtParCode.Focus();
                    //cmdItems.Text = null;
                    //cmdItems.Properties.NullValuePrompt = "البحث عن منتج";
                    //this.SearchStatus = true;
                    //AddButtons();
                }
                else
                { 
                
                
                foreach (XtraTabPage tb in tabItems.TabPages)
                {

                    ItemCardView item = context.ItemCardViews.Where(x => x.Name == cmdItems.Text && x.AddItem == true).FirstOrDefault() ;


                    tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.Dock = DockStyle.Fill;

                 

                    SimpleButton b = new SimpleButton();
                    b.Size = new Size(120, 120);
                    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                    b.ImageOptions.Image = imageCollection1.Images[0];
                    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                    b.Appearance.BackColor = Color.SteelBlue;
                    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                    b.Appearance.BorderColor = Color.Firebrick;
                    b.Text = item.Name;
                    b.Tag = item;
                    b.Click += B_Click;
                    flp.Controls.Add(b);


                    flp.AutoScroll = true;

                    for (int i = 0; i < tb.TabControl.TabPages.Count; i++)
                        if (tb.TabControl.TabPages[i].Text == item.CategoryName)
                        {
                            tb.TabControl.SelectedTabPage = tb.TabControl.TabPages[i];
                            break;
                        }
                    
                    tb.Controls.Clear();
                    tb.Controls.Add(flp);
                   tabItems.TabPages.Where(x => x.Text== item.Name);
                   
                }
                txtParCode.Focus();
                cmdItems.Text = null;
                cmdItems.Properties.NullValuePrompt = "البحث عن منتج";
                this.SearchStatus = true;
                
               
                }


             

            }
        }

     
        private void tabItems_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
           
            if (this.SearchStatus == true)
            {

            int i = 1;
            foreach (XtraTabPage tb in tabItems.TabPages)
            {
                var item = context.ItemCardViews.Where(x => x.CategoryCode == i && x.AddItem == true);
                tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                FlowLayoutPanel flp = new FlowLayoutPanel();
                flp.Dock = DockStyle.Fill;

                item.ForEach(c =>
                {
                    var itemCountDetailIn = context.SaleDetailViews.Where(x => x.ItemCode == c.ItemCode && x.Operation_Type_Id == 1 && x.IsDeleted == 0).Sum(u => (double?)u.Total);
                    var itemCountDetailOut = context.SaleDetailViews.Where(x => x.ItemCode == c.ItemCode && x.Operation_Type_Id == 2 && x.IsDeleted == 0).Sum(u => (double?)u.Total);
                    var FinalTotal = itemCountDetailIn - itemCountDetailOut;
                    SimpleButton b = new SimpleButton();
                    b.Size = new Size(120, 120);
                    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                    b.ImageOptions.Image = imageCollection1.Images[0];
                    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                    b.Appearance.BackColor = Color.SteelBlue;
                    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                    b.Appearance.BorderColor = Color.Firebrick;
                    b.Text = c.Name;
                    b.Tag = item;
                    b.Click += B_Click;
                    flp.Controls.Add(b);

                });
                flp.AutoScroll = true;
          
                tb.Controls.Add(flp);
                i++;
            }
            txtParCode.Focus();
                this.SearchStatus = false;
            }
         
        }

         
        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //     var x = DateTime.Now.ToString("hh:mm:ss");
        //     var xx = DateTime.Now.ToString("MM/dd/yyyy");
        //    dtEntryDate.Text = x + " " + xx;

        //}

        private void btnRefershItems_Click(object sender, EventArgs e)
        {
            tabItems.TabPages.Clear();
            context.Categories.ForEach(x => tabItems.TabPages.Add(x.CategoryName));
            AddButtons();
        }


        private void tableLayoutPanel1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmCafeSearchItems f3 = new frmCafeSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {


                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                        return;


                    }


                    //else if (this.Status == "New")
                    //{


                    //    SaveSaleDetail();








                    //}
                    //else if (this.Status == "Old")
                    //{


                        SaveSaleDetail();



                   // }

                    //Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    //var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
                    //if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    //{


                    //    MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                    //    return;


                    //}


                    //else if (this.Status == "New")
                    //{

                    //    SplashScreenManager.ShowForm(typeof(WaitForm1));
                    //    SaveSaleMaster();
                    //    SaveSaleDetail();
                    //    SplashScreenManager.CloseForm();

                    //    var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
                    //    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

                    //    Report rpt = new Report();
                    //    rpt.Load(@"Reports\SaleInvoice.frx");
                    //    rpt.RegisterData(Master, "Header");
                    //    rpt.RegisterData(Detail, "Lines");
                    //    rpt.PrintSettings.ShowDialog = false;
                    //    //rpt.Design();
                    //    rpt.Print();






                    //}
                    //else if (this.Status == "Old")
                    //{

                    //    SaveSaleMaster();
                    //    SaveSaleDetail();
                    //    var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
                    //    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

                    //    Report rpt = new Report();
                    //    rpt.Load(@"Reports\SaleInvoice.frx");
                    //    rpt.RegisterData(Master, "Header");
                    //    rpt.RegisterData(Detail, "Lines");
                    //    rpt.PrintSettings.ShowDialog = false;
                    //    //rpt.Design();
                    //    rpt.Print();

                    //}

                    //New();
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        private void txtParCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmCafeSearchItems f3 = new frmCafeSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                        return;


                    }


                    //else if (this.Status == "New")
                    //{


                        SaveSaleDetail();








                    //}
                    //else if (this.Status == "Old")
                    //{


                    //    SaveSaleDetail();



                    //}

                    //Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    //var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
                    //if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    //{


                    //    MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                    //    return;


                    //}


                    //else if (this.Status == "New")
                    //{

                    //    SplashScreenManager.ShowForm(typeof(WaitForm1));
                    //    SaveSaleMaster();
                    //    SaveSaleDetail();
                    //    SplashScreenManager.CloseForm();

                    //    var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
                    //    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

                    //    Report rpt = new Report();
                    //    rpt.Load(@"Reports\SaleInvoice.frx");
                    //    rpt.RegisterData(Master, "Header");
                    //    rpt.RegisterData(Detail, "Lines");
                    //      rpt.PrintSettings.ShowDialog = false;
                    //    //rpt.Design();
                    //   rpt.Print();






                    //}
                    //else if (this.Status == "Old")
                    //{

                    //    SaveSaleMaster();
                    //    SaveSaleDetail();
                    //    var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
                    //    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

                    //    Report rpt = new Report();
                    //    rpt.Load(@"Reports\SaleInvoice.frx");
                    //    rpt.RegisterData(Master, "Header");
                    //    rpt.RegisterData(Detail, "Lines");
                    //    rpt.PrintSettings.ShowDialog = false;
                    //    //rpt.Design();
                    //    rpt.Print();

                    //}

                    //New();
                }
            }
            catch(Exception ex)
            {
                var x = ex.Message;
            }
        }

        private void gcSaleDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmCafeSearchItems f3 = new frmCafeSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    //Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    // var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
                    // if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    // {


                    //     MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                    //     return;


                    // }


                    // else if (this.Status == "New")
                    // {

                    //     SplashScreenManager.ShowForm(typeof(WaitForm1));
                    //     SaveSaleMaster();
                    //     SaveSaleDetail();
                    //     SplashScreenManager.CloseForm();

                    //     var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
                    //     var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

                    //     Report rpt = new Report();
                    //     rpt.Load(@"c:\Users\islam\Desktop\Untitled.frx");
                    //     rpt.RegisterData(Master, "Header");
                    //     rpt.RegisterData(Detail, "Lines");
                    //       //rpt.PrintSettings.ShowDialog = false;
                    //      //rpt.Design();
                    //     rpt.Design();






                    // }
                    // else if (this.Status == "Old")
                    // {

                    //     SaveSaleMaster();
                    //     SaveSaleDetail();
                    //     var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();
                    //     var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate == DateTime.Today select a).ToList();

                    //     Report rpt = new Report();
                    //     rpt.Load(@"Reports\Untitled.frx");
                    //     rpt.RegisterData(Master, "Header");
                    //     rpt.RegisterData(Detail, "Lines");
                    //     // rpt.PrintSettings.ShowDialog = false;
                    //     //rpt.Design();
                    //     //rpt.Show();

                    // }

                    // New();


                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                        return;


                    }


                    //else if (this.Status == "New")
                    //{


                        SaveSaleDetail();








                    //}
                    //else if (this.Status == "Old")
                    //{


                     //   SaveSaleDetail();



                    //}
                }
            }
            catch
            {

            }
        }

        private void CmdCatagory_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                for (int i = 0; i < tabItems.TabPages.Count; i++)
                {
                    if (tabItems.TabPages[i].Text == CmdCatagory.Text)
                    {
                        tabItems.SelectedTabPage = tabItems.TabPages[i];
                    }
                }
                CmdCatagory.Text = null;
                CmdCatagory.Properties.NullValuePrompt = "البحث عن مجموعة";


            }
        }

        private void CmdCatagory_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < tabItems.TabPages.Count; i++)
            {
                if (tabItems.TabPages[i].Text == CmdCatagory.Text)
                {
                    tabItems.SelectedTabPage = tabItems.TabPages[i];
                }
            }
            CmdCatagory.Text = null;
            CmdCatagory.Properties.NullValuePrompt = "البحث عن مجموعة";
        }

        private void cmdItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmdItems.Text))
            {

                //foreach (XtraTabPage tb in tabItems.TabPages)
                //{

                //    ItemCardView item = context.ItemCardViews.Where(x =>  x.AddItem == true).FirstOrDefault();


                //    tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                //    FlowLayoutPanel flp = new FlowLayoutPanel();
                //    flp.Dock = DockStyle.Fill;



                //    SimpleButton b = new SimpleButton();
                //    b.Size = new Size(120, 120);
                //    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                //    b.ImageOptions.Image = imageCollection1.Images[0];
                //    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                //    b.Appearance.BackColor = Color.SteelBlue;
                //    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                //    b.Appearance.BorderColor = Color.Firebrick;
                //    b.Text = item.Name;
                //    b.Tag = item;
                //    b.Click += B_Click;
                //    flp.Controls.Add(b);


                //    flp.AutoScroll = true;

                //    for (int i = 0; i < tb.TabControl.TabPages.Count; i++)
                //        if (tb.TabControl.TabPages[i].Text == item.CategoryName)
                //        {
                //            tb.TabControl.SelectedTabPage = tb.TabControl.TabPages[i];
                //            break;
                //        }

                //    //tabItems.TabPages.Where(x => x.Text== item.Name);
                //    tb.Controls.Clear();
                //    tb.Controls.Add(flp);

                //}
                //txtParCode.Focus();
                //cmdItems.Text = null;
                //cmdItems.Properties.NullValuePrompt = "البحث عن منتج";
                //this.SearchStatus = true;
                //AddButtons();
            }
            else
            {


                foreach (XtraTabPage tb in tabItems.TabPages)
                {

                    ItemCardView item = context.ItemCardViews.Where(x => x.Name == cmdItems.Text && x.AddItem == true).FirstOrDefault();


                    tb.Appearance.Header.Font = new Font("Calibri", 13, FontStyle.Bold);
                    FlowLayoutPanel flp = new FlowLayoutPanel();
                    flp.Dock = DockStyle.Fill;



                    SimpleButton b = new SimpleButton();
                    b.Size = new Size(120, 120);
                    b.Font = new Font("Calibri", 11, FontStyle.Bold);
                    b.ImageOptions.Image = imageCollection1.Images[0];
                    b.ImageOptions.ImageToTextAlignment = ImageAlignToText.BottomCenter;
                    b.Appearance.BackColor = Color.SteelBlue;
                    b.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
                    b.Appearance.BorderColor = Color.Firebrick;
                    b.Text = item.Name;
                    b.Tag = item;
                    b.Click += B_Click;
                    flp.Controls.Add(b);


                    flp.AutoScroll = true;

                    for (int i = 0; i < tb.TabControl.TabPages.Count; i++)
                        if (tb.TabControl.TabPages[i].Text == item.CategoryName)
                        {
                            tb.TabControl.SelectedTabPage = tb.TabControl.TabPages[i];
                            break;
                        }

                    tb.Controls.Clear();
                    tb.Controls.Add(flp);
                    tabItems.TabPages.Where(x => x.Text == item.Name);

                }
                txtParCode.Focus();
                cmdItems.Text = null;
                cmdItems.Properties.NullValuePrompt = "البحث عن منتج";
                this.SearchStatus = true;


            }
        }
    }


}
