using System;
using System.Collections.Generic;

using System.Data;

using System.Linq;

using System.Windows.Forms;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;

using FastReport;

 
using PointOfSaleSedek._101_Adds._102_Customer;
using PointOfSaleSedek.Model;
using DataRep;

namespace PointOfSaleSedek._101_Adds
{

    public partial class frmSuperMarketSales : Form
    {
        POSEntity context = new POSEntity();
        public String Status = "New";
        public bool SearchStatus = false;
        Static st = new Static();


        public frmSuperMarketSales()
        {
            InitializeComponent();
            //NewForLoad();
            //txtParCode.Focus();


        }


        private void btnEdite_Click(object sender, EventArgs e)
        {





            gcSaleDetail.Enabled = true;

            btnNew.Enabled = true;
            btnSave.Enabled = true;
            btnEdite.Enabled = false;
            btnDiscount.Enabled = true;
            txtParCode.Enabled = true;
            txtParCode.Text = "";
            txtParCode.Focus();
       


            HelperClass.HelperClass.EnableControls(tableLayoutPanel1);
        }


        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void FillSlkPaymentType()
        {
            DataTable dt = new DataTable();
            var result = context.Payment_Type.Where(user => user.IsDeleted == 0).ToList();
            slkPaymentType.Properties.DataSource = result;
            slkPaymentType.Properties.ValueMember = "Code";
            slkPaymentType.Properties.DisplayMember = "PaymentType";

            slkPaymentType.EditValue = result[0].Code;

        }

        private void frmSales_Load(object sender, EventArgs e)
        {

            //timer1.Start();
            this.Status = "New";
            txtParCode.Text = "";
  
            txtParCode.Focus();
            btnEdite.Enabled = false;

            var UserId = Convert.ToInt64(st.GetUser_Code());
            var Check = context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
            if (!Check)
            {
                if (MaterialMessageBox.Show("برجاء اضافة وردية لهذا المستخدم", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    this.Close();
                }

            }

         

             


            NewForLoad();
            txtParCode.Text = "";
            txtParCode.Focus();
        }



        void NewForLoad()
        {
            HelperClass.HelperClass.ClearValues(tableLayoutPanel1);
            FillSlkPaymentType();
            FillSlkCustomer();
            Int64? MaxCode = context.SaleMasters.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
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
            //context.Categories.ForEach(x => tabItems.TabPages.Add(x.CategoryName));

            txtParCode.Text = "";
            txtParCode.Focus();



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
            txtParCode.ResetText();

            txtParCode.Text = "";
            txtParCode.Focus();


        }

        private void gvSaleDetail_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            List<SaleDetailView> gcData = gcSaleDetail.DataSource as List<SaleDetailView>;
            var FocusRow = gvSaleDetail.GetFocusedRow() as SaleDetailView;

            gcData.Where(x => x.ItemCode == FocusRow.ItemCode).Select(x => x.Qty == (Double)e.Value);



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
            txtParCode.ResetText();


            txtParCode.Text = "";
            txtParCode.Focus();

        }

        // //Save SaleMaster Date
        public void SaveSaleMaster(Int64 ShiftCode, Int64 UserCode, Double Discount, Double TotalBeforDiscount, Double FinalTotal, Double QtyTotal, Int64 SaleMasterCode)

        {
            if (slkCustomers.EditValue == null)
            {
                slkCustomers.EditValue = 0;
            }


            if (slkCustomers.EditValue == null) {
                slkCustomers.EditValue = 0;
            }

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

        public void SaveSaleDetail()
        {


            if (slkPaymentType.EditValue == null || slkPaymentType.EditValue.ToString() == "0" || slkPaymentType.Text.Trim() == "")
            {
                MaterialMessageBox.Show("برجاء اختيار طريقة الدفع", MessageBoxButtons.OK);
                return;
            }



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
            
            Int64 UserCode = st.GetUser_Code();
            var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
            Int64 SaleMasterCode = Int64.Parse(lblSaleMasterId.Text);


            if (this.Status == "Old")
            {
                bool TestUpdate = context.SaleMasters.Any(SaleMaster => SaleMaster.SaleMasterCode == SaleMasterCode && SaleMaster.Operation_Type_Id == 2 && SaleMaster.EntryDate.Day == DateTime.Today.Day && SaleMaster.EntryDate.Month == DateTime.Today.Month && SaleMaster.EntryDate.Year == DateTime.Today.Year);

                if (TestUpdate)
                {

                    var SaleDetails = context.SaleDetails.Where(Masters => Masters.SaleMasterCode == SaleMasterCode && Masters.Operation_Type_Id == 2 && Masters.EntryDate.Day == DateTime.Today.Day && Masters.EntryDate.Month == DateTime.Today.Month && Masters.EntryDate.Year == DateTime.Today.Year);


                    // Vildate QTy
                    using (POSEntity ContVald = new POSEntity())
                    {
                        GetDataFromGrid.ForEach(item =>
                        {

                            var qty = item.Qty;

                            var DayOfYears = item.EntryDate.Day;
                            var Years = item.EntryDate.Year;
                            var Months = item.EntryDate.Month;

                            var Item_Qty_List_Tran_History = ContVald.Item_History_transaction.Where(w => w.Item_Id == item.ItemCode && w.IsDeleted == 0 && w.SaleMasterCode == item.SaleMasterCode && w.OrderDate.Day == DayOfYears && w.OrderDate.Month == Months && w.OrderDate.Year == Years).ToList();
                            var item_Qt_Tran_History = Item_Qty_List_Tran_History.Where(x => x.Item_Id == item.ItemCode && x.SaleMasterCode == item.SaleMasterCode).Sum(x => x.Trans_Out);


                            var Item_Qty_List = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList();
                            var item_Qt = Item_Qty_List.Where(x => x.Item_Id == item.ItemCode && x.IsFinshed == false).Sum(x => x.Current_Qty_Now);
                            var Total = item_Qt_Tran_History + item_Qt;

                            if (Total < qty)
                            {
                                MaterialMessageBox.Show("كمية" + item.Name + "غير كافية في المخزن حيث يوجد " + item_Qt + "فقط هل تريد اكمال العملية ", MessageBoxButtons.OKCancel);
                                return;
                            }
                        });

                    }

                    using (POSEntity context2 = new POSEntity())
                    {
                        var Details = context2.SaleDetails.Where(w => w.SaleMasterCode == SaleMasterCode && w.EntryDate.Day == DateTime.Today.Day && w.EntryDate.Month == DateTime.Today.Month && w.EntryDate.Year == DateTime.Today.Year);
                        context2.SaleDetails.RemoveRange(Details);
                        context2.SaveChanges();



                    }




                    List<Item_History_transaction> _Item_History_transaction = new List<Item_History_transaction>();

                    var DayOfYear = DateTime.Today.Day;
                    var Year = DateTime.Today.Year;
                    var Month = DateTime.Today.Month;
                    _Item_History_transaction = context.Item_History_transaction.Where(w => w.SaleMasterCode == SaleMasterCode && w.OrderDate.Day == DayOfYear && w.OrderDate.Month == Month && w.OrderDate.Year == Year && w.IsDeleted == 0).ToList();

                    _Item_History_transaction.ForEach(x => {

                        Item_History item_History;
                        item_History = context.Item_History.SingleOrDefault(Item => Item.Item_Id == x.Item_Id && Item.Id == x.Item_History_Id);
                        item_History.Current_Qty_Now -= x.Trans_Out;

                        item_History.IsFinshed = false;
                        context.SaveChanges();




                        List<Item_History_transaction> __Item_History_transaction = new List<Item_History_transaction>();
                        __Item_History_transaction = context.Item_History_transaction.Where(Item => Item.SaleMasterCode == x.SaleMasterCode && Item.Item_Id == x.Item_Id && Item.IsDeleted == 0).ToList();
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
                            SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
                            SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
                            shiftCode = item.shiftCode,
                            Operation_Type_Id = 2,
                            UserId = st.GetUser_Code()



                        };

                        ArryOfSaleDetail.Add(_SaleDetail);

                    }


                    context.SaleDetails.AddRange(ArryOfSaleDetail);
                    context.SaveChanges();

                    SaveSaleMaster(
                         ShiftCode: ShiftCode,
                         UserCode: st.GetUser_Code(),
                         Discount: double.Parse(lblDiscount.Text),
                         TotalBeforDiscount: double.Parse(lblFinalBeforDesCound.Text),
                         FinalTotal: double.Parse(lblFinalTotal.Text),
                         QtyTotal: double.Parse(lblItemQty.Text),
                         SaleMasterCode: Int64.Parse(lblSaleMasterId.Text)
                        );



                    //var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year select a).ToList();
                    //var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year select a).ToList();
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
                using (POSEntity ContVald = new POSEntity())
                {
                    foreach (var item in GetDataFromGrid)
                    {


                       

                        var Item_Qty_List = ContVald.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false).ToList().Sum(x => x.Current_Qty_Now);
                     


                        if (Item_Qty_List < item.Qty)
                        {

                            if (MaterialMessageBox.Show("كمية   " + item.Name + " غير كافية في المخزن حيث يوجد  " + Item_Qty_List + "  فقط  ", MessageBoxButtons.YesNo) != DialogResult.OK)
                            {


                                return;
                            }
                        }


                    }

                }
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
                        Total = item.Total,
                        shiftCode = ShiftCode,
                        EntryDate = DateTime.Now,
                        SaleDetailCode = Int64.Parse(lblSaleMasterId.Text),
                        SaleMasterCode = Int64.Parse(lblSaleMasterId.Text),
                        UserId = st.GetUser_Code(),
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
                         UserCode: st.GetUser_Code(),
                         
                         Discount: double.Parse(lblDiscount.Text),
                         TotalBeforDiscount: double.Parse(lblFinalBeforDesCound.Text),
                         FinalTotal: double.Parse(lblFinalTotal.Text),
                         QtyTotal: double.Parse(lblItemQty.Text),
                         SaleMasterCode: Int64.Parse(lblSaleMasterId.Text)
                        );


                New();

               List<SaleMasterView> Master;
               List<SaleDetailView> Detail;
                using (POSEntity context23 = new POSEntity())
                {
                    var ShiftCode2 = context23.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
                    Master = (from a in context23.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.Shift_Code  == ShiftCode2 select a).ToList();
                    Detail = (from a in context23.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.shiftCode == ShiftCode2 select a).ToList();

                }

                Report rpt = new Report();
                rpt.Load(@"Reports\SaleInvoice.frx");
                rpt.RegisterData(Master, "Header");
                rpt.RegisterData(Detail, "Lines");
                rpt.PrintSettings.ShowDialog = false;
                rpt.Show();
                //rpt.Print();


            }




        }


   
        //void Update_Item_Qty_Oly(Int64 Sale_Master_Code, double Qty,DateTime OrderDate, Int64 History_Id, Int64 Item_Id)
        //{

        //    using (POSEntity2 _context2 = new POSEntity2())
        //    {
        //        Item_History item_History;
        //        item_History = _context2.Item_History.SingleOrDefault(Item => Item.Id == History_Id);
        //        item_History.Current_Qty_Now -= Qty;
        //        item_History.Is_Used = (bool)true;
        //        _context2.SaveChanges();
        //    }
        //    using (POSEntity2 _context = new POSEntity2())
        //    {
        //        Item_History_transaction _Item_History_transaction = new Item_History_transaction()
        //        {
        //            OrderDate = OrderDate,
        //            Item_History_Id = History_Id,
        //            CreatedDate = DateTime.Now,
        //            Trans_In = 0,
        //            Trans_Out = Qty,
        //            IsDeleted = 0,
                    
        //            Item_Id = Item_Id,
        //            SaleMasterCode = Sale_Master_Code

        //        };
        //        _context.Item_History_transaction.Add(_Item_History_transaction);
        //        _context.SaveChanges();
        //    }
        //}

        //void Update_Item_Qty_And_Finshed(Int64 Sale_Master_Code, double Qty, DateTime OrderDate, Int64 History_Id, Int64 Item_Id)
        //{
        //    Item_History item_History;
        //    item_History = context.Item_History.SingleOrDefault(Item => Item.Id == History_Id);
        //    item_History.Current_Qty_Now -=  Qty;
        //    item_History.Is_Used = (bool)true;
        //    item_History.IsFinshed = (bool)true;
        //    context.SaveChanges();


        //    Item_History_transaction _Item_History_transaction = new Item_History_transaction()
        //    {
        //        OrderDate = OrderDate,
        //        Item_History_Id = History_Id,
        //        CreatedDate = DateTime.Now,
        //        Trans_In = 0,
        //        Trans_Out = Qty,
        //        IsDeleted = 0,
        //        Item_Id = Item_Id,
        //        SaleMasterCode = Sale_Master_Code

        //    };
        //    context.Item_History_transaction.Add(_Item_History_transaction);
        //    context.SaveChanges();



        //}


        

        
        void New()
        {
           
            Int64? MaxCode = context.SaleMasters.Where(x => x.EntryDate.Day == DateTime.Today.Day && x.EntryDate.Month == DateTime.Today.Month && x.EntryDate.Year == DateTime.Today.Year && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64?)u.SaleMasterCode + 1);
            if (MaxCode == null || MaxCode == 0)
            {
                MaxCode = 1;
            }   
            gcSaleDetail.Enabled = true;
            txtParCode.Enabled = true;
            btnAddCustomer.Enabled = true;

            btnCustomerHistory.Enabled = false;

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
                gcSaleDetail.RefreshDataSource() ;
           
            }


            slkCustomers.Text = "";
            slkCustomers.SelectedText = "";
            slkCustomers.Enabled =true;
            slkCustomers.EditValue = 0;
            slkCustomers.Reset();
            slkCustomers.Refresh();
            txtParCode.ResetText();
            txtParCode.Text = "";
            txtParCode.Focus();

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
            txtParCode.Text = "";
            txtParCode.Focus();

            this.Status = "New";
             

        }
        private void gcSaleDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
              {

                     if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                     {
                    frmSuperMarketSearchItems f3 = new frmSuperMarketSearchItems();
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
            Int64 User_Code = st.GetUser_Code();

            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnser"));
            if (result)
            {
                frmSuperMarketInvoiceSearch frm = new frmSuperMarketInvoiceSearch();
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


            double FinalTotal = 0;
            double TotalDiscount = 0;



             Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
             var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;
            var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year  select a).ToList();
                 var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year select a).ToList();


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
                // rpt.PrintSettings.ShowDialog = false;
                   rpt.Design();
             //   rpt.Show();

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

            //    var Master = (from a in context.SaleMasterViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year  select a).ToList();
            //    var Detail = (from a in context.SaleDetailViews where a.SaleMasterCode == SaleMasterCode && a.Operation_Type_Id == 2 && a.EntryDate.Day == DateTime.Today.Day && a.EntryDate.Month == DateTime.Today.Month && a.EntryDate.Year == DateTime.Today.Year select a).ToList();
            //    if (Master.Count == 0 || Detail.Count == 0)
            //    {

            //        MaterialMessageBox.Show("!برجاء اختيار فاتوره للطباعة", MessageBoxButtons.OK);
            //        return;
            //    }
            //    else
            //    {

            //        Report rpt = new Report();
            //        rpt.Load(@"Reports\SaleInvoice.frx");
            //        rpt.RegisterData(Master, "Header");
            //        rpt.RegisterData(Detail, "Lines");
            //        //rpt.PrintSettings.ShowDialog = false;
            //        //rpt.Design();
            //        rpt.Print();
            //    }

            //}


            //btnEdite.Enabled = false;
            //txtParCode.Focus();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

 

            Int64 User_Code = st.GetUser_Code();

            var result = context.Auth_View.Any(View => View.User_Code == User_Code && (View.User_IsDeleted == 0) && (View.Tab_Name == "btnDiscount"));
            if (result)
            {
                frmSuperMarketDiscount frm = new frmSuperMarketDiscount();
                frm.lblFinalAmount = Convert.ToDecimal(lblFinalBeforDesCound.Text);
                frm.ShowDialog();

            }
            else
            {
                frmAdmin frm = new frmAdmin();
                frm.authName = "btnDiscount";
                frm.Show();
            }
            txtParCode.Text = "";
            txtParCode.Focus();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
            txtParCode.Text = "";
            txtParCode.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            frmSuperMarketSearchItems frm = new frmSuperMarketSearchItems();
            frm.ShowDialog();
        }

       

        

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
            txtParCode.ResetText();

            txtParCode.Text = "";
            txtParCode.Focus();
        }

        private void btnEdite_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmSuperMarketSearchItems f3 = new frmSuperMarketSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;

                    decimal finaltotal = Convert.ToInt64(lblFinalTotal.Text);

                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
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
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
           

            
                SaveSaleDetail();
            txtParCode.Text = "";
            txtParCode.Focus();




        }






        public void FillSlkCustomer()
        {
            var result = context.Customer_View.Where(Customer => Customer.IsDeleted == 0).ToList();
            slkCustomers.Properties.DataSource = result;
            slkCustomers.Properties.ValueMember = "Customer_Code";
            slkCustomers.Properties.DisplayMember = "Customer_Name";
        }

        private void txtParCode_KeyUp_1(object sender, KeyEventArgs e)
        {
            // Get GrideData
            var RowCount = gvSaleDetail.RowCount;
            if (RowCount > 0)
            {
                try
                {
                    List<SaleDetailView> gcData = gcSaleDetail.DataSource as List<SaleDetailView>;

                    var item = context.ItemCardViews.FirstOrDefault(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0);

                    bool TestUpdate = gcData.Any(User => User.ParCode == txtParCode.Text);

                    double Qty = 1;

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

                        gcSaleDetail.DataSource = gcData;
                        gcSaleDetail.RefreshDataSource();

                        lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                        lblFinalBeforDesCound.Text = sum.ToString();

                        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                        txtParCode.ResetText();

                        txtParCode.Text = "";
            

                        txtParCode.Focus();

                    }


                    else
                    {



                        ItemCardView ii = context.ItemCardViews.Where(x => x.ParCode == txtParCode.Text && x.IsDeleted == 0).FirstOrDefault();
                        if (ii == null)
                        {
                            return;

                        }

                        SaleDetailView _SaleDetailView = new SaleDetailView()
                        {
                            ItemCode = ii.ItemCode,
                            EntryDate = DateTime.Now,
                            Price = Convert.ToDouble(ii.Price),
                            Qty = 1,
                            Total = Convert.ToDouble(1) * Convert.ToDouble(ii.Price),
                            Name = ii.Name,
                            UnitCode = ii.UnitCode,
                            Operation_Type_Id = 2,
                            CategoryCode = ii.CategoryCode,
                            ParCode = ii.ParCode



                        };

                        gcData.Add(_SaleDetailView);
                        gcSaleDetail.DataSource = gcData;
                        double sum = 0;
                        gcData.ForEach(x =>
                        {
                            sum += Convert.ToDouble(x.Total);
                        });
                        gcSaleDetail.RefreshDataSource();

                        lblItemQty.Text = gvSaleDetail.RowCount.ToString();
                        lblFinalBeforDesCound.Text = sum.ToString();

                        lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                        txtParCode.ResetText();

                        txtParCode.Text = "";
                  
                        txtParCode.Focus();

                    }


                }
                catch
                {

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



                    SaleDetailView _SaleDetailView = new SaleDetailView()
                    {
                        ItemCode = ii.ItemCode,
                        EntryDate = DateTime.Now,
                        Price = Convert.ToDouble(ii.Price),
                        Qty = 1,
                        Total = Convert.ToDouble(1) * Convert.ToDouble(ii.Price),
                        Name = ii.Name,
                        UnitCode = ii.UnitCode,
                        CategoryCode = ii.CategoryCode,
                        ParCode = ii.ParCode,
                        Operation_Type_Id = 2



                    };

                    gcdata.Add(_SaleDetailView);
                    gcSaleDetail.DataSource = gcdata;
                    double sum = 0;
                    gcdata.ForEach(x =>
                    {
                        sum += Convert.ToDouble(x.Total);
                    });


                    lblFinalBeforDesCound.Text = sum.ToString();

                    lblFinalTotal.Text = Convert.ToString(sum - Convert.ToDouble(lblDiscount.Text));
                    lblItemQty.Text = (RowCount + 1).ToString();
                    txtParCode.ResetText();
                    //  txtQty.ResetText();
                    txtParCode.Text = "";
          
                    txtParCode.Focus();
                    //  btnAdd.Enabled = true;
                }
                catch
                {
                    if (gvSaleDetail.RowCount > 0)
                    {
                        //  btnAdd.Enabled = true;

                    }
                    else
                    {

                        //    btnAdd.Enabled = false;

                    }
                }
            }
            //txtParCode.Focus();
        }

        private void txtParCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Control && e.KeyCode == Keys.S)//here you can choose any key you want
                {
                    frmSuperMarketSearchItems f3 = new frmSuperMarketSearchItems();
                    f3.ShowDialog();
                }
                else if (e.KeyCode == Keys.Escape)
                {

                    Int64 SaleMasterCode = Convert.ToInt64(lblSaleMasterId.Text);
                    var GetDataFromGrid = gcSaleDetail.DataSource as List<SaleDetailView>;

                    decimal finaltotal = Convert.ToInt64(lblFinalTotal.Text);

                    if (GetDataFromGrid == null || GetDataFromGrid.Count <= 0)
                    {


                        MaterialMessageBox.Show("لا يوجد اصناف", MessageBoxButtons.OK);
                        return;


                    }

                    else if (finaltotal < 0)
                    {

                        MaterialMessageBox.Show("لا يمكن قبول قيمة فاتورة اقل من الصفر", MessageBoxButtons.OK);
                        return;
                    }


                    

                        SaveSaleDetail();


                   


                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }

        private void simpleButton3_Click_2(object sender, EventArgs e)
        {
            frmSuperMarketSearchItems frm = new frmSuperMarketSearchItems();
            frm.ShowDialog();
            txtParCode.Text = "";
         
            txtParCode.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            frmAddCustomer frm = new frmAddCustomer();
            frm.ShowDialog();
        //    txtParCode.Focus();

        }

        private void slkCustomers_EditValueChanged(object sender, EventArgs e)
        {
            if (slkCustomers.EditValue != null && slkCustomers.EditValue.ToString() != "0" && slkCustomers.Text.Trim() != "")
            {
                btnCustomerHistory.Enabled = true;
            }
            else {
                btnCustomerHistory.Enabled = false;
            }
           
        }

       

       
        private void btnCustomerHistory_Click(object sender, EventArgs e)
        {
            frmSuperMarketInvoiceSearchForCustomer frm = new frmSuperMarketInvoiceSearchForCustomer();
            frm.ShowDialog();
            txtParCode.Text = "";
            txtParCode.Focus();
        }

        private void repositoryItemButtonEdit7_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
            txtParCode.ResetText();

            txtParCode.Text = "";
            txtParCode.Focus();
        }
    }


}
