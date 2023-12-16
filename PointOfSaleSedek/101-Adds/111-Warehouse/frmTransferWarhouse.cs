//using DataRep;
using BackOfficeEntity;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraSplashScreen;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSaleSedek._101_Adds._111_Warehouse
{
    public partial class TransferWarhouse : DevExpress.XtraEditors.XtraForm
    {

        readonly db_a8f74e_posEntities _server = new db_a8f74e_posEntities();
       // readonly POSEntity context = new POSEntity();
        Static st = new Static();
        public TransferWarhouse()
        {
            InitializeComponent();
            langu();
            FillslkFromWarhouse();
            FillslkToWarhouse();

        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

 
  
            gvFromWarehouse.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
            gvToWarehouse.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";

            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "Save" : "حفظ";
         
 
            windowsUIButtonPanel.Buttons[6].Properties.Caption = st.isEnglish() ? "Exit" : "خروج";
            
            
            materialContextMenuStrip2.Items[0].Text = st.isEnglish() ? "Delete" : "حذف";
            label50.Text = st.isEnglish() ? "From" : "من";
            label2.Text = st.isEnglish() ? "To" : "الي";
            labelControl1.Text = st.isEnglish() ? "Count" : "الكمية";
            labelControl5.Text = st.isEnglish() ? "Count" : "الكمية";
            labelControl4.Text = st.isEnglish() ? "Item" : "المنتج";
            
            gridColumn27.Caption = st.isEnglish() ? "Name" : "الاسم";
            gridColumn28.Caption = st.isEnglish() ? "Name" : "الاسم";

            gridColumn1.Caption = st.isEnglish() ? "Last Date Updated" : "تاريخ اخر تحديث";
            gridColumn2.Caption = st.isEnglish() ? "Item Name" : "اسم الصنف";
            gridColumn13.Caption = st.isEnglish() ? "Count" : "العدد";
            gridColumn7.Caption = st.isEnglish() ? "Category" : "المجموعة";
            gridColumn10.Caption = st.isEnglish() ? "Parcode" : "الباركود";



            gridColumn16.Caption = st.isEnglish() ? "Item Name" : "اسم الصنف";
            gridColumn13.Caption = st.isEnglish() ? "Count" : "العدد";
            gridColumn25.Caption = st.isEnglish() ? "Count" : "العدد";
            gridColumn7.Caption = st.isEnglish() ? "Category" : "المجموعة";
            gridColumn14.Caption = st.isEnglish() ? "Category" : "المجموعة";
            gridColumn10.Caption = st.isEnglish() ? "Parcode" : "الباركود";
            gridColumn18.Caption = st.isEnglish() ? "Parcode" : "الباركود";


        }


        public void FillslkToWarhouse()
        {
            DataTable dt = new DataTable();
            var result = _server.Warehouse.Where(user => user.isDelete == 0).ToList();
            slkToWarhouse.Properties.DataSource = result;
            slkToWarhouse.Properties.ValueMember = "Warehouse_Code";
            slkToWarhouse.Properties.DisplayMember = "Warehouse_Name";
            
        }

      


        

       

      

        List<ItemCardView> _ItemSelectedCardView = new List<ItemCardView>();

       

        public void FillslkFromWarhouse()
        {
            DataRep.POSEntity context = new DataRep.POSEntity();
            DataTable dt = new DataTable();
            var result = context.Warehouses.Where(user => user.isDelete == 0).ToList();
            slkFromWarhouse.Properties.DataSource = result;
            slkFromWarhouse.Properties.ValueMember = "Warehouse_Code";
            slkFromWarhouse.Properties.DisplayMember = "Warehouse_Name";
          

        }
        public void Fill_BackOffice_Gride()
        {
            List<ItemCardView> _ItemCardView = new List<ItemCardView>();
              
            using (DataRep.POSEntity _context23 = new DataRep.POSEntity()) {
                var GetAllItems = _context23.ItemCardViews.Where(x => x.IsDeleted == 0 && x.Branch_Code == 0 ).ToList();
                var ItemTransactions = _context23.Item_History.Where(x => !x.IsFinshed && x.Warhouse_Code == 0 ).ToList();

                foreach (var item1 in GetAllItems)
                {


                    GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Item_Count_InStoreg = ItemTransactions.Where(x => x.Item_Id == item1.ItemCode).Sum(xx => xx.Current_Qty_Now);
                    GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Last_Update_Date = DateTime.Now;

                }

                grdFromWarehouse.DataSource = GetAllItems;
                grdFromWarehouse.RefreshDataSource();
            }
              

        }



        public void Fill_Branch_Gride(Int64 warhousCode)
        {
            Int64 branchCOde = Convert.ToInt64(st.GetBranch_Code());
            List<ItemCardView> _ItemCardView = new List<ItemCardView>();

            using (DataRep.POSEntity _context23 = new DataRep.POSEntity())
            {
                var GetAllItems = _context23.ItemCardViews.Where(x => x.IsDeleted == 0 && x.Branch_Code == branchCOde).ToList();
                BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities();
                var item_Balance = _server.Item_Balance.Where(x => x.WareHouse_Code == warhousCode && x.Branch_Code == warhousCode && x.IsDeleted == 0).ToList();

                foreach (var item1 in GetAllItems)
                {
                    try
                    {

                        GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Item_Count_InStoreg = item_Balance.Where(x => x.Item_Code == item1.ItemCode).Sum(xx => xx.Balance);
                    }
                    catch
                    {
                    }
                    try
                    {
                        GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Price = item_Balance.FirstOrDefault(x => x.Item_Code == item1.ItemCode).Price;
                    }
                    catch
                    {
                    }




                    try
                    {

                        DateTime lasstDate = item_Balance.FirstOrDefault(x => x.Item_Code == item1.ItemCode).Last_Update;
                        GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Last_Update_Date = lasstDate;
                    }
                    catch
                    {
                    }


                }

                grdFromWarehouse.DataSource = GetAllItems;
                grdFromWarehouse.RefreshDataSource();
            }


        }

        //public void Fill_Branch_Gride(Int64 warhousCode)
        //{
        //    List<ItemCardView> _ItemCardView = new List<ItemCardView>();

        //    using (db_a8f74e_posEntities _server = new db_a8f74e_posEntities())
        //    {
        //        var GetAllItems = _server.ItemCardViews.Where(x => x.IsDeleted == 0 && x.Branch_Code  == warhousCode).ToList();
        //        var item_Balance = _server.Item_Balance.Where(x => x.WareHouse_Code == warhousCode && x.Branch_Code == warhousCode && x.IsDeleted == 0 ).ToList();

        //        foreach (var item1 in GetAllItems)
        //        {


        //            GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Item_Count_InStoreg = item_Balance.Where(x => x.Item_Code == item1.ItemCode && x.Branch_Code == warhousCode && x.WareHouse_Code == warhousCode && x.IsDeleted == 0).Sum(xx => xx.Balance);

        //            try
        //            {

        //                DateTime lasstDate = item_Balance.FirstOrDefault(x => x.Item_Code == item1.ItemCode && x.Branch_Code == warhousCode && x.WareHouse_Code == warhousCode && x.IsDeleted == 0).Last_Update;
        //                GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Last_Update_Date = lasstDate;
        //                GetAllItems.FirstOrDefault(x => x.ItemCode == item1.ItemCode).Price = item_Balance.FirstOrDefault(x => x.Item_Code == item1.ItemCode && x.Branch_Code == warhousCode && x.WareHouse_Code == warhousCode && x.IsDeleted == 0).Price;
        //            }
        //            catch { 
        //            }


        //        }

        //        grdFromWarehouse.DataSource = GetAllItems;
        //        grdFromWarehouse.RefreshDataSource();
        //    }


        //}

        private void slkFromWarhouse_EditValueChanged_1(object sender, EventArgs e)
        {
            SplashScreenManager.ShowForm(typeof(WaitForm1));
            Int64 warhouseCode = Convert.ToInt64(slkFromWarhouse.EditValue);
            grdToWarehouse.DataSource = null;
            grdToWarehouse.RefreshDataSource();

            textEdit1.Text = "0";
            labelControl2.Text = "";
            labelControl3.Text = "";
            try
            {
                if (warhouseCode == 0)
                {
                    Fill_BackOffice_Gride();

                }
                else
                {
                    Fill_Branch_Gride(warhouseCode);

                }
            }
            catch {
                grdFromWarehouse.DataSource = null;
                grdFromWarehouse.RefreshDataSource();
            }

            SplashScreenManager.CloseForm();

        }
        private void gvFromWarehouse_RowClick(object sender, RowClickEventArgs e)
        {

            try
            {
                var FocusRow = gvFromWarehouse.GetFocusedRow() as DataRep.ItemCardView;

                labelControl2.Text = FocusRow.Name_En;
                labelControl3.Text = FocusRow.Item_Count_InStoreg.ToString();
                textEdit1.Focus();
                textEdit1.SelectAll();
            }
            catch {
                labelControl2.Text = "";
                labelControl3.Text = "";
            }
           

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            gvToWarehouse.DeleteSelectedRows();
            grdToWarehouse.RefreshDataSource();
        }



        void Update_Item_Qty_Oly(Int64 Po_code,Int64 Max_Warehouse_Transaction_Code, Int64 Sale_Master_Code, double Qty, DateTime OrderDate,
            Int64 to_Warhouse, Int64 from_Warhouse, Int64 History_Id, Int64 Item_Id, Int64 shiftCode)
        {

            //var Warehouse_Code = st.Get_Warehouse_Code();
            //var Branch_Code = st.GetBranch_Code();
            
            
                DataRep.POSEntity local = new DataRep.POSEntity();
            db_a8f74e_posEntities server = new db_a8f74e_posEntities();


                DataRep.Item_History _item_History  = new DataRep.Item_History();

                _item_History = local.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == from_Warhouse);
                

                Item_History_Back_Office _Item_History_Back_Office_transaction = new Item_History_Back_Office()
                {
                    CreatedDate = _item_History.CreatedDate,
                    From_Branch_Id = from_Warhouse,
                    From_Warhouse_Code = from_Warhouse,
                    Is_Branch_Take_Update = false,
                    IsDeleted = 0,
                    Item_Id = Item_Id,
                    Main_Item_History_Code = _item_History.Id,
                    Main_Master_Code = _item_History.Sale_Master_Code,
                    Price_Buy = _item_History.Price_Buy,
                    To_Branch_Code = to_Warhouse,
                    Qty = Qty,
                    PO_Code = Po_code,
                    To_Warhouse_Code = to_Warhouse,
                    Warhouse_Transfer_Code = Max_Warehouse_Transaction_Code

                };
            server.Item_History_Back_Office.Add(_Item_History_Back_Office_transaction);
            server.SaveChanges();
             


                DataRep.Item_History item_History = local.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == from_Warhouse);
                item_History.Current_Qty_Now -= Qty;
                item_History.Is_Used = (bool)true;
                item_History.IsFinshed = (bool)false;
            local.SaveChanges();

                Item_History_transaction _Item_History_transaction = new Item_History_transaction()
                {
                    from_Warhouse_Code = from_Warhouse,
                    OrderDate = OrderDate,

                    Item_History_Id = History_Id,
                    CreatedDate = DateTime.Now,
                    Trans_In = 0,
                    Trans_Out = Qty,
                    Warhouse_Transfer_Code = Max_Warehouse_Transaction_Code,
                    shiftCode = shiftCode,
                    IsDeleted = 0,
                    Item_Id = Item_Id,
                    Branch_Id = from_Warhouse,
                    To_Warhouse_Code = to_Warhouse,
                    SaleMasterCode = Sale_Master_Code

                };
            server.Item_History_transaction.Add(_Item_History_transaction);
            server.SaveChanges();


            

        }

        void Update_Item_Qty_And_Finshed(Int64 Po_code,Int64 Max_Warehouse_Transaction_Code, Int64 Sale_Master_Code, double Qty,
            DateTime OrderDate, Int64 to_Warhouse, Int64 from_Warhouse, Int64 History_Id, Int64 Item_Id, Int64 shiftCode)
        {



            //var Warehouse_Code = st.Get_Warehouse_Code();
            //var Branch_Code = st.GetBranch_Code();


            DataRep.POSEntity local = new DataRep.POSEntity();
            db_a8f74e_posEntities server = new db_a8f74e_posEntities();


            DataRep.Item_History _item_History = new DataRep.Item_History();

            _item_History = local.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == from_Warhouse);


            Item_History_Back_Office _Item_History_Back_Office_transaction = new Item_History_Back_Office()
            {
                CreatedDate = _item_History.CreatedDate,
                From_Branch_Id = from_Warhouse,
                From_Warhouse_Code = from_Warhouse,
                Is_Branch_Take_Update = false,
                IsDeleted = 0,
                Item_Id = Item_Id,
                Main_Item_History_Code = _item_History.Id,
                Main_Master_Code = _item_History.Sale_Master_Code,
                Price_Buy = _item_History.Price_Buy,
                To_Branch_Code = to_Warhouse,
                Qty = Qty,
                PO_Code = Po_code,
                To_Warhouse_Code = to_Warhouse,
                Warhouse_Transfer_Code = Max_Warehouse_Transaction_Code

            };
            server.Item_History_Back_Office.Add(_Item_History_Back_Office_transaction);
            server.SaveChanges();



            DataRep.Item_History item_History = local.Item_History.SingleOrDefault(Item => Item.Id == History_Id && Item.Warhouse_Code == from_Warhouse);
            item_History.Current_Qty_Now -= Qty;
            item_History.Is_Used = (bool)true;
            item_History.IsFinshed = (bool)true;
            local.SaveChanges();

            Item_History_transaction _Item_History_transaction = new Item_History_transaction()
            {
                from_Warhouse_Code = from_Warhouse,
                OrderDate = OrderDate,
                is_Back_Office_Updated = false,
                Item_History_Id = History_Id,
                CreatedDate = DateTime.Now,
                Trans_In = 0,
                Trans_Out = Qty,
                Warhouse_Transfer_Code = Max_Warehouse_Transaction_Code,
                shiftCode = shiftCode,
                IsDeleted = 0,
                Item_Id = Item_Id,
                Branch_Id = from_Warhouse,
                To_Warhouse_Code = to_Warhouse,
                SaleMasterCode = Sale_Master_Code

            };
            server.Item_History_transaction.Add(_Item_History_transaction);
            server.SaveChanges();





        }





        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "حفظ" || btn.Caption == "Save")
            {
                SplashScreenManager.ShowForm(typeof(WaitForm1));
                

                // Add New Item Bulance in Warehouse 2 

                if (_ItemSelectedCardView.Count == 0) {
                    SplashScreenManager.CloseForm();
                    MaterialMessageBox.Show(st.isEnglish() ? "No transfer data to be saved" : "لا يوجد بيانات تحويل ليتم حفظها", MessageBoxButtons.OK);
                   
                    return;
                }
                else if (String.IsNullOrWhiteSpace(slkToWarhouse.Text)) {

                    SplashScreenManager.CloseForm();
                    MaterialMessageBox.Show(st.isEnglish() ? "Please select the store you are transferring to" : "برجاء اختيار المخزن المحول اليه", MessageBoxButtons.OK);
                    return;
                }
                else if (String.IsNullOrWhiteSpace(slkFromWarhouse.Text)) {
                    SplashScreenManager.CloseForm();
                    MaterialMessageBox.Show(st.isEnglish() ? "Please select the store you are transferring from" : "برجاء اختيار المخزن المحول منه", MessageBoxButtons.OK);
                    return;
                }

                else  {
                  
                    try
                    {

                        Int64 UserCode = st.GetUser_Code();

                        Int64 ShiftCode = 0;
                        try
                        {
                            ShiftCode = _server.Shift_View.Where(x3 => x3.User_Id == UserCode && x3.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
                        }
                        catch
                        {
                            ShiftCode = 0;
                        }

                        Int64 FromWarhouseCode = Convert.ToInt64(slkFromWarhouse.EditValue);
                        Int64 ToWarhouseCode = Convert.ToInt64(slkToWarhouse.EditValue);
                        Int64 Max_Warehouse_Transaction_Code = 0;
                        // Get DetailCode
                        using (db_a8f74e_posEntities Contexts2 = new db_a8f74e_posEntities())
                        {
                            Max_Warehouse_Transaction_Code = Convert.ToInt64(Contexts2.Warehouse_Transaction.Max(u => (Int64?)u.Code + 1));
                            if (Max_Warehouse_Transaction_Code == 0)
                            {
                                Max_Warehouse_Transaction_Code = 1;
                            }


                            Warehouse_Transaction _Warehouse_Transaction = new Warehouse_Transaction()
                            {


                                Created_By = st.GetUser_Code(),
                                Created_Date = DateTime.Now,
                                From_Warehouse_Code = FromWarhouseCode,
                                IsDeleted = 0,

                                Code = Max_Warehouse_Transaction_Code,
                                Branch_Code = 0,
                                To_Warehouse_Code = ToWarhouseCode,



                            };
                            Contexts2.Warehouse_Transaction.Add(_Warehouse_Transaction);
                            Contexts2.SaveChanges();
                        }


                        _ItemSelectedCardView.ForEach(x =>
                        {


                            using (db_a8f74e_posEntities _server = new db_a8f74e_posEntities())
                            {
                                DataRep.POSEntity context = new DataRep.POSEntity();
                                List<Item_History> OldList = new List<Item_History>();
                                if (FromWarhouseCode == 0)
                                {
                                    var OldList3 = context.Item_History.Where(w => w.Item_Id == x.ItemCode && w.IsFinshed == false && w.Warhouse_Code == FromWarhouseCode).ToList();
                                    OldList3.ForEach(cc =>
                                    {

                                        Item_History fff = new Item_History()
                                        {
                                            Branch_Id = cc.Branch_Id,
                                            Id = cc.Id,
                                            CreatedDate = cc.CreatedDate,
                                            Current_Qty_Now = cc.Current_Qty_Now,
                                            IsFinshed = cc.IsFinshed,
                                            is_Back_Office_Updated = false,
                                            Is_Used = cc.Is_Used,
                                            Item_Id = cc.Item_Id,
                                            Price_Buy = cc.Price_Buy,
                                            Price_Sale = cc.Price_Sale,
                                            Qty = cc.Qty,
                                            Sale_Master_Code = cc.Sale_Master_Code,
                                            Warhouse_Code = cc.Warhouse_Code,
                                            Warhouse_Transfer_Code = cc.Warhouse_Transfer_Code,

                                        };
                                        OldList.Add(fff);

                                    });

                                }
                                else
                                {
                                    OldList = _server.Item_History.Where(w => w.Item_Id == x.ItemCode && w.IsFinshed == false && w.Warhouse_Code == FromWarhouseCode).ToList();
                                }



                                PO pO = new PO()
                                {
                                    From_WareHouse_Code = FromWarhouseCode,
                                    To_WareHouseCode = ToWarhouseCode,
                                    Item_Code = x.ItemCode,
                                    IsDeleted = 0,
                                    Warehouse_Transaction_Code = Max_Warehouse_Transaction_Code,
                                    PO_Item_Qty = Convert.ToDouble(x.Item_Risk_limit),
                                    Creted_By_User = st.GetUser_Code(),
                                    Created_Date = DateTime.Now,
                                    PO_Request_State = FromWarhouseCode == 0 ? 2 : 1
                                };

                                _server.PO.Add(pO);
                                _server.SaveChanges();
                                _server.Dispose();

                                Int64 Creted_By_User = st.GetUser_Code();
                                int PO_Request_State = FromWarhouseCode == 0 ? 2 : 1;
                                var cvcv = Convert.ToDouble(x.Item_Risk_limit);

                                db_a8f74e_posEntities _server2 = new db_a8f74e_posEntities();
                                Int64 Po_code = _server2.PO.Where(xx => xx.To_WareHouseCode == ToWarhouseCode && xx.Item_Code == x.ItemCode &&
                                xx.Warehouse_Transaction_Code == Max_Warehouse_Transaction_Code && xx.PO_Item_Qty == cvcv && xx.Creted_By_User == Creted_By_User && xx.PO_Request_State == PO_Request_State).Max(f => f.id);


                                if (FromWarhouseCode == 0)
                                {
                                    double qty = Convert.ToDouble(x.Item_Risk_limit);
                                    OldList.ForEach(x2 =>
                                    {

                                        if (qty > 0)
                                        {
                                        // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
                                        if (qty >= x2.Current_Qty_Now)
                                            {

                                                Update_Item_Qty_And_Finshed(Po_code, Max_Warehouse_Transaction_Code, x2.Sale_Master_Code, x2.Current_Qty_Now, x2.CreatedDate, ToWarhouseCode, FromWarhouseCode, x2.Id, x2.Item_Id, shiftCode: ShiftCode);
                                                qty = qty - x2.Current_Qty_Now;

                                                if (qty < 0)
                                                {
                                                    qty = 0;
                                                }
                                            }

                                            else if (qty < x2.Current_Qty_Now)
                                            {


                                                Update_Item_Qty_Oly(Po_code, Max_Warehouse_Transaction_Code, x2.Sale_Master_Code, qty, x2.CreatedDate, ToWarhouseCode, FromWarhouseCode, x2.Id, x2.Item_Id, shiftCode: ShiftCode);
                                                qty = 0;

                                            }



                                        }


                                    });
                                }





                            }







                        });
                        _ItemSelectedCardView = new List<ItemCardView>();
                        grdToWarehouse.DataSource = null;
                        grdToWarehouse.RefreshDataSource();
                        textEdit1.Text = "0";
                        labelControl2.Text = "";
                        labelControl3.Text = "";
                        if (FromWarhouseCode == 0)
                        {
                            Fill_BackOffice_Gride();
                        }

                        SplashScreenManager.CloseForm();
                        MaterialMessageBox.Show(st.isEnglish() ? "Converted successfully" : "تم التحويل بنجاح", MessageBoxButtons.OK);
                        return;


                    }
                    catch {

                        SplashScreenManager.CloseForm();
                    }


           }


              
            }
          
            else if (btn.Caption == "خروج" || btn.Caption == "Exit")
            {

                this.Close();


            }
            
        }

        private void slkToWarhouse_EditValueChanged(object sender, EventArgs e)
        {
            if (slkFromWarhouse.Text == slkToWarhouse.Text) {

                MaterialMessageBox.Show(st.isEnglish() ? "It is not possible to transfer to the same store" : "لا يمكن التحويل لنفس المخزن", MessageBoxButtons.OK);
                slkToWarhouse.EditValue = 0;
                slkToWarhouse.Text = "";
                return;
               
            }

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataRep.ItemCardView focusRow = gvFromWarehouse.GetFocusedRow() as DataRep.ItemCardView;

            if (String.IsNullOrWhiteSpace(textEdit1.Text) )
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please enter a valid quantity" : "برجاء ادخال كمية صحيحه", MessageBoxButtons.OK);
                return;
            }

            if (String.IsNullOrWhiteSpace(slkToWarhouse.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select the store to be transferred to" : "برجاء اختيار المخزن المحول اليه", MessageBoxButtons.OK);
                return;
            }
            if (focusRow != null)
            {

                Int64 branchCode = st.GetBranch_Code();

                var count = Convert.ToDouble(textEdit1.Text ?? "0");
                if (count == 0)
                {
                    MaterialMessageBox.Show(st.isEnglish() ? "Please enter a valid quantity" : "برجاء ادخال كمية صحيحه", MessageBoxButtons.OK);
                }
                else if (count > focusRow.Item_Count_InStoreg)
                {
                    MaterialMessageBox.Show(st.isEnglish() ? "There is not enough quantity" : "لا يوجد كمية كافية", MessageBoxButtons.OK);
                }
                else
                {


                    bool isInSelectedList = _ItemSelectedCardView.Any(x => x.IsDeleted == 0 && x.ItemCode == focusRow.ItemCode && x.Branch_Code == branchCode );
                    if (isInSelectedList)
                    {

                        _ItemSelectedCardView.FirstOrDefault(x => x.IsDeleted == 0 && x.ItemCode == focusRow.ItemCode && x.Branch_Code == branchCode).Item_Risk_limit = count;

                    }
                    else
                    {
                        ItemCardView GetItems;
                        if (branchCode != 0)
                        {

                            GetItems = _server.ItemCardView.FirstOrDefault(x => x.IsDeleted == 0 && x.ItemCode == focusRow.ItemCode && x.Branch_Code == branchCode);
                            _ItemSelectedCardView.Add(GetItems);
                        }
                        else {
                           DataRep.POSEntity local = new DataRep.POSEntity();




                        var    localItem = local.ItemCardViews.FirstOrDefault(x => x.IsDeleted == 0 && x.ItemCode == focusRow.ItemCode && x.Branch_Code == branchCode);

                            ItemCardView items = new ItemCardView() { 
                            Branch_Code = localItem.Branch_Code,
                            AddItem = localItem.AddItem,
                            CategoryCode = localItem.CategoryCode,
                            CategoryName = localItem.CategoryName,
                            IsDeleted = localItem.IsDeleted,
                            ItemCode = localItem.ItemCode,
                            Item_Count_InStoreg = localItem.Item_Count_InStoreg,
                            Item_Risk_limit = localItem.Item_Risk_limit,
                            Last_Update_Date = localItem.Last_Update_Date,
                            Name = localItem.Name,
                            Name_En = localItem.Name_En,
                            ParCode = localItem.ParCode,
                            Price = localItem.Price,
                            PriceBuy = localItem.PriceBuy,
                            UnitCode = localItem.UnitCode,
                            UnitName = localItem.UnitName

                            };

                            _ItemSelectedCardView.Add(items);
                        }

                     
 

                       


                        _ItemSelectedCardView.FirstOrDefault(xx => xx.IsDeleted == 0 && xx.ItemCode == focusRow.ItemCode && xx.Branch_Code == branchCode).Item_Risk_limit = count;





                    }


                    grdToWarehouse.DataSource = _ItemSelectedCardView;
                    grdToWarehouse.RefreshDataSource();

                    textEdit1.Text = "0";
                    labelControl2.Text = "";
                    labelControl3.Text = "";


                }


            }
            else
            {

                MaterialMessageBox.Show(st.isEnglish() ? "There are no items" : "لا يوجد اصناف", MessageBoxButtons.OK);
                return;

            }
        }
    }
}