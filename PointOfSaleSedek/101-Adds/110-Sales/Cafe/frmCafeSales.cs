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
using PointOfSaleSedek._101_Adds._102_Customer;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmCafeSales : Form
    {
        POSEntity context = new POSEntity();
        public String Status = "New";
        public bool SearchStatus = false;
        Static st = new Static();



        public frmCafeSales()
        {
            InitializeComponent();
            langu();
            NewForLoad();
            txtParCode.Focus();

           
        }
        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            //tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            
            simpleButton1.Text = st.isEnglish() ? "Invoices" : "الفواتير";
            btnNew.Text = st.isEnglish() ? "New" : "جديد";
            btnEdite.Text = st.isEnglish() ? "Edite" : "تعديل";
            btnSave.Text = st.isEnglish() ? "Pay" : "دفع";
            btnser.Text = st.isEnglish() ? "New" : "جديد";
            btnCustomerHistory.Text = st.isEnglish() ? "invoices" : "فواتيره";
            gridColumn5.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn4.Caption = st.isEnglish() ? "Mobile" : "الموبيل";
            paymentType.Text = st.isEnglish() ? "Payment" : "طريقة الدفع";
            gridColumn3.Caption = st.isEnglish() ? "Payment Type" : "طريقة الدفع";
            btnDiscount.Text = st.isEnglish() ? "Discount" : "خصم";
            btnPrint.Text = st.isEnglish() ? "Print" : "طباعة";
            BtnExit.Text = st.isEnglish() ? "Exit" : "خروج";
            labelControl1.Text = st.isEnglish() ? "User" : "المستخدم";
            labelControl3.Text = st.isEnglish() ? "Items" : "الاصناف";
            labelControl4.Text = st.isEnglish() ? "Invoice" : "الفاتورة";
            labelControl5.Text = st.isEnglish() ? "Total Before Discount" : "الاجمالي قبل الخصم";
            labelControl7.Text = st.isEnglish() ? "Customers" : "العملاء";
            colTotal.Caption = st.isEnglish() ? "Total" : "الاجمالي";
            labelControl2.Text = st.isEnglish() ? "Discount Value" : "قيمة الخصم ";
            lblCurrency.Text = st.isEnglish() ? "Qatari Riyal" : "ريال قطري";
            colName.Caption = st.isEnglish() ? "Name" : "الاسم";
            colName.FieldName = st.isEnglish() ? "Name_En" : "Name";
            colPrice.Caption = st.isEnglish() ? "Price" : "السعر";
            colQty.Caption = st.isEnglish() ? "Qty" : "الكمية";
            materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "Remove" : "حذف";



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


            Int64 UserCode = st.GetUser_Code();
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


            slkCustomers.Text = "";
            slkCustomers.SelectedText = "";
            slkCustomers.Enabled = true;
            slkCustomers.EditValue = 0;
            slkCustomers.Reset();
            slkCustomers.Refresh();
            txtParCode.ResetText();
            Int64 User_Code = st.GetUser_Code();

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
             FillSlkPaymentType();
            FillSlkCustomers();


            Int64 UserCode = st.GetUser_Code();
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
            lblCurrency.Text = st.isEnglish() ? "Ryal" : "ريال"; ;
            lblFinalTotal.Text = "0";
            lblFinalBeforDesCound.Text = "0";
            lblItemQty.Text = "0";
            lblDiscount.Text = "0";
            lblUserName.Text = st.GetUserName();
           // dtEntryDate.Text = context.Employee_View.Where(x => x.Employee_Code == UserCode).First().Employee_Name.ToString() ?? "";

            while (gvSaleDetail.RowCount > 0)
            {
                gvSaleDetail.SelectAll();
                gvSaleDetail.DeleteSelectedRows();
            }

            gcCafeSaleDetail.DataSource = null;
            gcCafeSaleDetail.RefreshDataSource();
            btnPrint.Enabled = false;

            Int64 User_Code = st.GetUser_Code();

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


        public void FillSlkCustomers()
        {
            var result = context.Customer_View.Where(Customer => Customer.IsDeleted == 0).ToList();
            slkCustomers.Properties.DataSource = result;
            slkCustomers.Properties.ValueMember = "Customer_Code";
            slkCustomers.Properties.DisplayMember = "Customer_Name";
        }


        //Save SaleMaster Date
        //public void SaveSaleMaster()

        //{

        //    Int64 UserCode = st.GetUser_Code();
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

        //            _SaleMasters.UserCode = st.GetUser_Code();
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

        public void FillSlkPaymentType()
        {
            DataTable dt = new DataTable();
            var result = context.Payment_Type.Where(user => user.IsDeleted == 0).ToList();
            slkPaymentType.Properties.DataSource = result;
            slkPaymentType.Properties.ValueMember = "Code";
            slkPaymentType.Properties.DisplayMember = "PaymentType";

            slkPaymentType.EditValue = result[0].Code;

        }

        public void SaveSaleMaster(Int64 ShiftCode, Int64 UserCode, Double Discount, Double TotalBeforDiscount, Double FinalTotal, Double QtyTotal, Int64 SaleMasterCode, Double cash, Double visa)

        {
            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();
            if (slkCustomers.EditValue == null)
            {
                slkCustomers.EditValue = 0;
            }


            if (slkCustomers.EditValue == null)
            {
                slkCustomers.EditValue = 0;
            }

            if (this.Status == "Old")
            {


                bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Branch_Id == Branch_Code && SaleMaster.ShiftCode == ShiftCode  && SaleMaster.Operation_Type_Id == 2);

                if (TestUpdate)
                {
                    SaleMaster _SaleMasters;
                    _SaleMasters = context.SaleMasters.SingleOrDefault(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Branch_Id == Branch_Code && SaleMaster.ShiftCode == ShiftCode && SaleMaster.Operation_Type_Id == 2);
                    _SaleMasters.LastDateModif = DateTime.Now;
                    _SaleMasters.TotalBeforDiscount = double.Parse(lblFinalBeforDesCound.Text);
                    _SaleMasters.Discount = double.Parse(lblDiscount.Text);
                    _SaleMasters.FinalTotal = double.Parse(lblFinalTotal.Text);
                    _SaleMasters.ShiftCode = ShiftCode;
                    
                    _SaleMasters.Visa = visa;
                    _SaleMasters.Cash = cash;
                    _SaleMasters.QtyTotal = double.Parse(lblItemQty.Text);
                    _SaleMasters.Operation_Type_Id = 2;
                    _SaleMasters.Payment_Type = Int64.Parse(slkPaymentType.EditValue.ToString());
                    
                    //_SaleMasters.Customer_Code = Int64.Parse(slkCustomers.EditValue.ToString());


                    SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);

                    _SaleMasters.UserCode = st.GetUser_Code();
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
                    Payment_Type = Int64.Parse(slkPaymentType.EditValue.ToString()),
                     Branch_Id = Branch_Code,
                     Cash = cash,
                     Visa =visa,
                     
                    UserIdTakeOrder = UserCode,
                     Customer_Code = Int64.Parse(slkCustomers.EditValue.ToString()),
                    //Customer_Code = 0,
                    QtyTotal = double.Parse(lblItemQty.Text),
                    SaleMasterCode = SaleMasterCode,
                    Operation_Type_Id = 2,

                    UserCode = UserCode,

                };
                context.SaleMasters.Add(_SaleMaster);
                context.SaveChanges();

            }




        }


        /// <summary>
        /// ///////////////////////////// Save Detail /////////////////
        /// 1 - Check Old OR New
        /// 2- Validate
        /// 
        /// 
        /// 3- Check Item Qty With WareHouse 
        /// 4- Update WareHouse
        /// 5- Save Detial 
        /// 6-  Save Master 
        /// 
        /// 
        /// 
        /// </summary>
        /// 

        public void printSaleInvoice2(Int64 shiftCode, Int64 SaleMasterCode)
        {
            New();

            var Master = (from a in context.SaleMasterViews where a.Shift_Code == shiftCode && a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 select a).ToList();
            var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == shiftCode select a).ToList();


            Report rpt = new Report();
            rpt.Load(@"Reports\SaleInvoice.frx");
            rpt.RegisterData(Master, "Header");
            rpt.RegisterData(Detail, "Lines");
            rpt.PrintSettings.ShowDialog = false;
            rpt.PrintSettings.Copies = 2;
            //rpt.Design();
            rpt.Print();

        }

        public  void printSaleInvoice(Int64 shiftCode, Int64 SaleMasterCode)
        {

            New();
            var Master = (from a in context.SaleMasterViews where a.Shift_Code == shiftCode && a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 select a).ToList();
            var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == shiftCode select a).ToList();


            Report rpt = new Report();
            rpt.Load(@"Reports\SaleInvoice.frx");
            rpt.RegisterData(Master, "Header");
            rpt.RegisterData(Detail, "Lines");
            rpt.PrintSettings.ShowDialog = false;
            //rpt.Design();
            rpt.Print();

        }

        public void Update_Item_Qty_Oly(Int64 Sale_Master_Code, double Qty,DateTime OrderDate, Int64 History_Id, Int64 Item_Id,Int64 shiftCode)
        {

            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();
            using (POSEntity _context2 = new POSEntity())
            {
               
                Item_History item_History = _context2.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == Warehouse_Code);
                item_History.Current_Qty_Now -= Qty;
                
                item_History.Is_Used = (bool)true;
                item_History.IsFinshed = (bool)false;
               
                Item_History_transaction _Item_History_transaction = new Item_History_transaction()
                {
                    OrderDate = OrderDate,
                    Item_History_Id = History_Id,
                    CreatedDate = DateTime.Now,
                    Trans_In = 0,
                    Trans_Out = Qty,
                    IsDeleted = 0,
                    shiftCode = shiftCode,
                    Branch_Id = Branch_Code,
                    from_Warhouse_Code = Warehouse_Code,
                    Item_Id = Item_Id,
                    SaleMasterCode = Sale_Master_Code

                };
                _context2.Item_History_transaction.Add(_Item_History_transaction);

                _context2.SaveChanges();

            
            }
           
        }

      public  void Update_Item_Qty_And_Finshed(Int64 Sale_Master_Code, double Qty, DateTime OrderDate, Int64 History_Id, Int64 Item_Id, Int64 shiftCode)
        {

            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();


            using (POSEntity context100 = new POSEntity())
            {
                Item_History item_History = context100.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == Warehouse_Code);
                item_History.Current_Qty_Now -= Qty;
                item_History.Is_Used = (bool)true;
                item_History.IsFinshed = (bool)true;

                Item_History_transaction _Item_History_transaction = new Item_History_transaction()
                {
                    OrderDate = OrderDate,
                    Item_History_Id = History_Id,
                    CreatedDate = DateTime.Now,
                    Trans_In = 0,
                    Trans_Out = Qty,
                    shiftCode = shiftCode,
                    IsDeleted = 0,
                    Item_Id = Item_Id,
                    Branch_Id = Branch_Code,
                    from_Warhouse_Code = Warehouse_Code,
                    SaleMasterCode = Sale_Master_Code

                };
                context100.Item_History_transaction.Add(_Item_History_transaction);


                context100.SaveChanges();
            }


        }

        public bool vaildate(Int64 shiftCode, List<SaleDetailView> GetDataFromGrid, Int64 SaleMasterCode)
        {

            bool isVaild = true;
            var Warehouse_Code = st.Get_Warehouse_Code();
            var Branch_Code = st.GetBranch_Code();
            decimal finaltotal = Convert.ToDecimal(lblFinalTotal.Text);





            if (slkPaymentType.EditValue == null || slkPaymentType.EditValue.ToString() == "0" || slkPaymentType.Text.Trim() == "")
            {
                MaterialMessageBox.Show("برجاء اختيار طريقة الدفع", MessageBoxButtons.OK);
                isVaild = false;
            }





            if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
            {


                MaterialMessageBox.Show(st.isEnglish() ? "There are no items" : "لا يوجد اصناف", MessageBoxButtons.OK);

                isVaild = false;

            }

            if (finaltotal < 0)
            {

                MaterialMessageBox.Show(st.isEnglish() ? "An invoice value less than zero cannot be accepted" : "لا يمكن قبول قيمة فاتورة اقل من الصفر", MessageBoxButtons.OK);
                isVaild = false;
            }


            if (shiftCode == 0)
            {


                if (MaterialMessageBox.Show(st.isEnglish() ? "Please Start New Shift for this user" : "برجاء اضافة وردية لهذا المستخدم", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    isVaild = false;
                    this.Close();
                }



            }


            if (this.Status == "Old")
            {



                // Vildate QTy
                using (POSEntity ContVald = new POSEntity())
                {

                    bool TestUpdate = ContVald.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Branch_Id == Branch_Code && SaleMaster.Operation_Type_Id == 2 && SaleMaster.ShiftCode == shiftCode);

                    if (!TestUpdate)
                    {
                        if (MaterialMessageBox.Show(st.isEnglish() ? "There is no data for this invoice" : "لا يوجد بيانات لتلك الفاتورة", MessageBoxButtons.OK) == DialogResult.OK)
                        {
                            
                            return false;
                        }


                    }
                    GetDataFromGrid.ForEach(item =>
                    {

                        //الكميه المطلوبه جديد
                        double qty = item.Qty;

                        double item_Qt_Tran_History = 0;
                        try
                        {
                           
                            //الكمية القديمه في الفاتورة
                              item_Qt_Tran_History = ContVald.Item_History_transaction.Where(w => w.Item_Id == item.ItemCode && w.from_Warhouse_Code == Warehouse_Code && w.IsDeleted == 0 && w.SaleMasterCode == SaleMasterCode && w.shiftCode == shiftCode).Sum(x => x.Trans_Out);
                        }
                        catch {

                            item_Qt_Tran_History = 0;
                        }
                      


                        double item_Qt = 0;
                        try
                        {
                            // الكمية المتاحة الان في المخزن
                            item_Qt = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.Warhouse_Code == Warehouse_Code && w.IsFinshed == false).Sum(x => x.Current_Qty_Now);
                        }
                        catch
                        {
                            item_Qt = 0;
                        }


                        // جمع الكمية القديمه + الكمية المتاحة
                        double Total = item_Qt_Tran_History + item_Qt;




                        if (Total < qty)
                        {

                            if (MaterialMessageBox.Show(st.isEnglish() ? "Quantity " + item.Name_En + " is not enough, as only " + item_Qt + " are available. Do you want to confirm the sale?" : "كمية" + item.Name + "غير كافية في المخزن حيث يوجد " + item_Qt + "فقط هل تريد اكمال العملية ", MessageBoxButtons.YesNo) == DialogResult.Cancel)
                            {
                                isVaild = false;
                                return;


                            }



                        }
                    });

                 

                }





             

            }

            if (this.Status == "New") {
                // Vildate QTy
                using (POSEntity ContVald = new POSEntity())
                {

                   
                    GetDataFromGrid.ForEach(item =>
                    {

                        //الكميه المطلوبه جديد
                        double qty = Convert.ToDouble(item.Qty);
                        double item_Qt = 0;
                        try
                        {
                            // الكمية المتاحة الان في المخزن
                            item_Qt =  ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.Warhouse_Code == Warehouse_Code && w.IsFinshed == false).Sum(x => x.Current_Qty_Now);
                        }
                        catch {
                            item_Qt = 0;
                        }
                       

                        if (item_Qt < qty)
                        {

                            if (MaterialMessageBox.Show(st.isEnglish() ? "Quantity " + item.Name + " is not enough, as only " + item_Qt.ToString() + " are available. Do you want to confirm the sale?" : "كمية" + item.Name + "غير كافية في المخزن حيث يوجد " + item_Qt.ToString() + "فقط هل تريد اكمال العملية ", MessageBoxButtons.YesNo) == DialogResult.Cancel)
                            {
                                isVaild = false;
                               


                            }



                        }
                    });



                }
            }

            return isVaild;

        }




      public  Int64 SaleMasterCode;
      public  Int64 ShiftCode;
      public  Int64 UserCode;
      public  Int64 Branch_Code;
      public  Int64 Warehouse_Code;
        public Item_History item_History;
        public Item_History_transaction _Item_History_transaction = new Item_History_transaction();
        public List<SaleDetailView> GetDataFromGrid = new List<SaleDetailView>();
        public List<Item_History> Item_Qty_List = new List<Item_History>();
        public List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();
        public void SaveSaleDetail()
        {

            SaleMasterCode =  Int64.Parse(lblSaleMasterId.Text);
                  
              Warehouse_Code = st.Get_Warehouse_Code();
              Branch_Code = st.GetBranch_Code();
              UserCode = st.GetUser_Code();
                      ShiftCode = 0;
                    try
                            {
                                ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
                            }
                            catch
                            {
                                ShiftCode = 0;
                            }
                   
                  
                   



            GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;





            bool isVald = vaildate(shiftCode: ShiftCode, GetDataFromGrid: GetDataFromGrid, SaleMasterCode: SaleMasterCode);

            // Vaidate
            if (!isVald)
            {

                return;
            }

            else {

                frmCafeCash frm = new frmCafeCash();
                double x = Convert.ToDouble(lblFinalTotal.Text);

                frm.total = x * -1;
                frm.lblTotal.Text = (x * -1).ToString();
                frm.ShowDialog();


              

            }
  
        }

        //public void SaveSaleDetail()
        //{
        //    List<SaleDetail> ArryOfSaleDetail = new List<SaleDetail>();
        //    var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
        //    Int64 UserCode = st.GetUser_Code();
        //    var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();

        //    if (this.Status == "Old")
        //    {
        //        Int64 SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);
        //        bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Operation_Type_Id==2 && SaleMaster.EntryDate == DateTime.Today);

        //        if (TestUpdate)
        //        {

        //            //SaleDetail _SaleDetails;
        //            var SaleDetails = context.SaleDetails.Where(Master => Master.SaleMasterCode == SaleMasterCode && Master.Operation_Type_Id ==2 && Master.EntryDate == DateTime.Today);




        //            using (POSEntity context2 = new POSEntity())
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
        //                     UserId =  st.GetUser_Code()



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
        //                UserId = st.GetUser_Code(),
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
                    b.Text = c.Name_En + Environment.NewLine +  c.Name + Environment.NewLine;
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

            List<SaleDetailView> grdData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
            // Get GrideData
            var RowCount = gvSaleDetail.RowCount;
            if (RowCount > 0)
            {
                try
                {
                    List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;

                    SimpleButton b = (SimpleButton)sender;

                    bool TestUpdate = gcData.Any(User => User.Name_En + Environment.NewLine + User.Name + Environment.NewLine == b.Text);

                    double Qty = 0;
                    if (string.IsNullOrWhiteSpace(txtQty.Text) || txtQty.Text.Trim() == "0" || txtQty.Text.Trim() == "")
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
                            if (x.Name_En + Environment.NewLine + x.Name + Environment.NewLine == b.Text)
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



                        ItemCard ii = context.ItemCards.Where(x => x.Name_En + Environment.NewLine + x.Name + Environment.NewLine == b.Text && x.IsDeleted == 0).FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(txtQty.Text) || txtQty.Text.Trim() == "0" || txtQty.Text.Trim() == "")
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
                            LineSequence = gcData.Count == 0? 1 : gcData.Count + 1,
                            Name_En = ii.Name_En,
                            


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

                    ItemCard ii = context.ItemCards.Where(x => x.Name_En + Environment.NewLine + x.Name + Environment.NewLine == b.Text && x.IsDeleted == 0).FirstOrDefault();



                    if (string.IsNullOrWhiteSpace(txtQty.Text) || txtQty.Text.Trim() == "0" || txtQty.Text.Trim() == "")
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
                        Name_En = ii.Name_En,
                        CustomerCode = 0,
                        LineSequence = grdData == null || grdData.Count == 0 ? 1 : grdData.Count + 1,
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
            Int64 UserCode = st.GetUser_Code();
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
                            Name_En = ii.Name_En,
                            LineSequence = gcData.Count == 0 ? 1 : gcData.Count + 1,
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

                    List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;

                    SaleDetailView _SaleDetailView = new SaleDetailView()
                    {
                        ItemCode = ii.ItemCode,
                        EntryDate = DateTime.Now,
                        Price = Convert.ToDouble(ii.Price),
                        Qty = double.Parse(txtQty.Text),
                        Total = Convert.ToDouble(txtQty.Text) * Convert.ToDouble(ii.Price),
                        Name = ii.Name,
                        Name_En = ii.Name_En,
                        
                        LineSequence = gcData ==null || gcData.Count == 0 ? 1 : gcData.Count + 1,

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
            var UserId = Convert.ToInt64(st.GetUser_Code());
            var Check = context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
            if (!Check)
            {
                if (MaterialMessageBox.Show(st.isEnglish() ? "Please Start New Shift for this user" :"برجاء اضافة وردية لهذا المستخدم", MessageBoxButtons.OK) == DialogResult.OK)
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
            if (gvSaleDetail.RowCount > 0) {

                btnSave.Enabled  = true;
            }

            txtParCode.Focus();
            HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Int64 User_Code = st.GetUser_Code();
            
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


            Int64 UserCode = st.GetUser_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            //double FinalTotal = 0;
            //double TotalDiscount = 0;



            Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);



            var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
           

            if (GetDataFromGrid.Count <= 0 || GetDataFromGrid == null)
            {


                MaterialMessageBox.Show(st.isEnglish() ? "There are no items" : "لا يوجد اصناف", MessageBoxButtons.OK);
                return;


            }
            else
            {

                printSaleInvoice(ShiftCode, SaleMasterCode);

                //List<SaleDetailViewVm> saleDetailViewVmList = new List<SaleDetailViewVm>();

                //foreach (var x in Detail)
                //{
                //    SaleDetailViewVm saleDetailViewVm = new SaleDetailViewVm()
                //    {

                //        AddItem = x.AddItem,
                //        SaleMasterCode = x.SaleMasterCode,
                //        CategoryCode = x.CategoryCode,
                //        CategoryName = x.CategoryName,
                //        Emp_Code = x.Emp_Code,
                //        EntryDate = x.EntryDate,
                //        Id = x.Id,
                //        IsDeleted = x.IsDeleted,
                //        ItemCode = x.ItemCode,
                //        Item_Count_InStoreg = x.Item_Count_InStoreg,
                //        Name = x.Name,
                //        Name_En = x.Name_En,
                //        Operation_Type_Id = x.Operation_Type_Id,
                //        OrederTotal = 0,

                //        ParCode = x.ParCode,
                //        Price = x.Price,
                //        PriceBuy = x.PriceBuy,
                //        Qty = x.Qty,
                //        Total = x.Total,
                //        UnitCode = x.UnitCode,
                //        UnitName = x.UnitName,
                //        UserName = x.UserName,
                //        Discount = 0

                //    };
                //    saleDetailViewVmList.Add(saleDetailViewVm);

                //}

                //Master.ForEach(Header =>
                //{
                //    saleDetailViewVmList.ForEach(Line =>
                //    {

                //        if (Header.SaleMasterCode == Line.SaleMasterCode)
                //        {

                //            Line.OrederTotal = Header.FinalTotal;
                //            Line.Discount = Header.Discount;

                //        }



                //    });

                //});


                //Master.ForEach(x =>
                //{

                //    FinalTotal += x.FinalTotal;
                //    TotalDiscount += x.Discount;

                //});




                //FinalTotal _FinalTotal = new FinalTotal()
                //{
                //    Total = FinalTotal,
                //    TotalDiscount = TotalDiscount



                //};
                //List<FinalTotal> _FinalTotalList = new List<FinalTotal>();
                //_FinalTotalList.Add(_FinalTotal);




                //Report rpt = new Report();
                //rpt.Load(@"Reports\SaleInvoice.frx");
                //rpt.RegisterData(Master, "Header");
                //rpt.RegisterData(_FinalTotalList, "FinalTotal");
                //rpt.RegisterData(saleDetailViewVmList, "Lines");
                // rpt.PrintSettings.ShowDialog = false;
                //  //rpt.Design();
                //rpt.Show();

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

            try
            {
                var RowCount = gvSaleDetail.RowCount;
                var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

                if (RowCount > 0 && FocusRow != null)
                {

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
            }
            catch { 
            
            
            
            }
          
         

          


        }



        private void gcSaleDetail_DataSourceChanged(object sender, EventArgs e)
        {
            txtParCode.ResetText();
        }

       

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //Int64 User_Code = st.GetUser_Code();

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
            Int64 User_Code = st.GetUser_Code();

            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnser"));
            if (result)
            {
                frmCafeInvoiceSearch frm = new frmCafeInvoiceSearch();
                frm.ShowDialog();

            }
            else
            {
                frmAdmin frm = new frmAdmin();
                frm.ShowDialog();
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
                        if (item  != null)
                        {
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
                        //txtParCode.Focus();
                        //cmdItems.Text = null;
                        //cmdItems.Properties.NullValuePrompt = st.isEnglish() ? "Find a product" : "البحث عن منتج";
                        this.SearchStatus = true;
                    }

                       
                
               
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


                        MaterialMessageBox.Show(st.isEnglish() ? "There are no items" : "لا يوجد اصناف", MessageBoxButtons.OK);
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


                        MaterialMessageBox.Show(st.isEnglish() ? "There are no items" : "لا يوجد اصناف", MessageBoxButtons.OK);
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



                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show(st.isEnglish() ? "There are no items" : "لا يوجد اصناف", MessageBoxButtons.OK);
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
                CmdCatagory.Properties.NullValuePrompt = st.isEnglish() ? "Search for a Category" :"البحث عن مجموعة";


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
            CmdCatagory.Properties.NullValuePrompt = st.isEnglish() ? "Search for a Category" : "البحث عن مجموعة";
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

                    if (item != null)
                    {
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
                    //txtParCode.Focus();
                    //cmdItems.Text = null;
                    //cmdItems.Properties.NullValuePrompt = st.isEnglish() ? "Search for products" :"البحث عن منتج";
                    this.SearchStatus = true;
                }
                    


            }
        }

        private void tabItems_Click(object sender, EventArgs e)
        {

        }

        private void btnser_Click(object sender, EventArgs e)
        {
            frmAddCustomer frm = new frmAddCustomer();
            frm.ShowDialog();
        }

        private void btnCustomerHistory_Click(object sender, EventArgs e)
        {
            frmCafeInvoiceSearchForCustomer frm = new frmCafeInvoiceSearchForCustomer();
            frm.ShowDialog();
            txtParCode.Text = "";
            txtParCode.Focus();
        }

        private void slkCustomers_EditValueChanged(object sender, EventArgs e)
        {
            if (slkCustomers.EditValue != null && slkCustomers.EditValue.ToString() != "0" && slkCustomers.Text.Trim() != "")
            {
                btnCustomerHistory.Enabled = true;
            }
            else
            {
                btnCustomerHistory.Enabled = false;
            }
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtQty.Text))
            {

                txtQty.Text = txtQty.Text.Remove(txtQty.Text.Length - 1, 1);
                txtParCode.Focus();
            }
        }

        

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSaleDetail();
            txtParCode.Focus();
        }

        private void repositoryItemButtonEdit3_Click(object sender, EventArgs e)
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

        private void gvSaleDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

          var text =   e.Column.Name;
            if (text == "colQty") {
                List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

                try
                {
                    if (Convert.ToDouble(e.Value) <= 0)
                    {
                        gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Qty = 1.0;
                    }
                    else
                    {

                        gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Qty = Convert.ToDouble(e.Value);
                    }
                }
                catch
                {
                    gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Qty = 1.0;
                }

                double sum = 0;
                gcData.ForEach(x =>
                {
                    x.Total = x.Qty * x.Price;
                    sum += Convert.ToDouble(x.Total);
                });
                gcCafeSaleDetail.DataSource = null;
                gcCafeSaleDetail.DataSource = gcData;
                gcCafeSaleDetail.RefreshDataSource();

                lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                lblFinalBeforDesCound.Text = sum.ToString();
                lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                txtParCode.ResetText();
                txtParCode.Text = "";
                txtParCode.Focus();
            }
            else if(text == "colTotal") {

                List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
                var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

                try
                {
                    if (Convert.ToDouble(e.Value) <= 0)
                    {
                        gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Total = 1.0;
                    }
                    else
                    {

                        gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Total = Convert.ToDouble(e.Value);
                    }
                }
                catch
                {
                    gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Total = 1.0;
                }

                double sum = 0;
                gcData.ForEach(x =>
                {
                    x.Qty  = x.Total / x.Price;
                    sum += Convert.ToDouble(x.Total);
                });
                gcCafeSaleDetail.DataSource = null;
                gcCafeSaleDetail.DataSource = gcData;
                gcCafeSaleDetail.RefreshDataSource();

                lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                lblFinalBeforDesCound.Text = sum.ToString();
                lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                txtParCode.ResetText();
                txtParCode.Text = "";
                txtParCode.Focus();
            }

        }

        private void gvSaleDetail_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
         {
            //List<SaleDetailView> gcData = gcCafeSaleDetail.DataSource as List<SaleDetailView>;
            //var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

            //try
            //{
            //    if (Convert.ToDouble(e.Value) <= 0)
            //    {
            //        gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Qty = 1.0;
            //    }
            //    else
            //    {

            //        gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Qty = Convert.ToDouble(e.Value);
            //    }
            //}
            //catch {
            //    gcData.Where(x => x.ItemCode == FocusRow.ItemCode).ToList()[0].Qty = 1.0;
            //}
          

             
            //double sum = 0;
            //gcData.ForEach(x =>
            //{
            //    x.Total = x.Qty * x.Price;
            //    sum += Convert.ToDouble(x.Total);
            //});
            //gcCafeSaleDetail.DataSource = null;
            //gcCafeSaleDetail.DataSource = gcData;
            //gcCafeSaleDetail.RefreshDataSource();

            //lblItemQty.Text = gvSaleDetail.RowCount.ToString();
            //lblFinalBeforDesCound.Text = sum.ToString();

            //lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
            ////txtParCode.ResetText();


            ////txtParCode.Text = "";
            ////txtParCode.Focus();
        }

        
    }


}
