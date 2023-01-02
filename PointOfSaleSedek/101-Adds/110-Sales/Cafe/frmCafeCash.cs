
using DataRep;
using PointOfSaleSedek.HelperClass;
using System;
using System.Linq;
using System.Windows.Forms;

namespace PointOfSaleSedek._101_Adds
{
    public partial class frmCafeCash : DevExpress.XtraEditors.XtraForm
    {

        POSEntity context = new POSEntity();
        Static st = new Static();
      public  double total = 0;
        public frmCafeCash()
        {
            InitializeComponent();
            langu();
            txtVisa.Text = "0";
            txtCash.Text = "0";
            lblTotal.Text = total.ToString();
        }


        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            //tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;

            //simpleButton1.Text = st.isEnglish() ? "Invoices" : "الفواتير";
            //btnNew.Text = st.isEnglish() ? "New" : "جديد";
            //btnEdite.Text = st.isEnglish() ? "Edite" : "تعديل";
            //btnSave.Text = st.isEnglish() ? "Pay" : "دفع";
            //btnser.Text = st.isEnglish() ? "New" : "جديد";
            //btnCustomerHistory.Text = st.isEnglish() ? "invoices" : "فواتيره";
            //gridColumn5.Caption = st.isEnglish() ? "Name" : "الاسم";
            //gridColumn4.Caption = st.isEnglish() ? "Mobile" : "الموبيل";
            //paymentType.Text = st.isEnglish() ? "Payment" : "طريقة الدفع";
            //gridColumn3.Caption = st.isEnglish() ? "Payment Type" : "طريقة الدفع";
            //btnDiscount.Text = st.isEnglish() ? "Discount" : "خصم";
            //btnPrint.Text = st.isEnglish() ? "Print" : "طباعة";
            //BtnExit.Text = st.isEnglish() ? "Exit" : "خروج";
            //labelControl1.Text = st.isEnglish() ? "User" : "المستخدم";
            //labelControl3.Text = st.isEnglish() ? "Items" : "الاصناف";
            //labelControl4.Text = st.isEnglish() ? "Invoice" : "الفاتورة";
            //labelControl5.Text = st.isEnglish() ? "Total Before Discount" : "الاجمالي قبل الخصم";
            //labelControl7.Text = st.isEnglish() ? "Customers" : "العملاء";
            //colTotal.Caption = st.isEnglish() ? "Total" : "الاجمالي";
            //labelControl2.Text = st.isEnglish() ? "Discount Value" : "قيمة الخصم ";
            //lblCurrency.Text = st.isEnglish() ? "Qatari Riyal" : "ريال قطري";
            //colName.Caption = st.isEnglish() ? "Name" : "الاسم";
            //colName.FieldName = st.isEnglish() ? "Name_En" : "Name";
            //colPrice.Caption = st.isEnglish() ? "Price" : "السعر";
            //colQty.Caption = st.isEnglish() ? "Qty" : "الكمية";
            //materialContextMenuStrip1.Items[0].Text = st.isEnglish() ? "Remove" : "حذف";



        }

        

        private void btnAdd_Click(object sender, System.EventArgs e)
        {

            if (String.IsNullOrWhiteSpace(txtCash.Text))
            {

                txtCash.Text = "0";
            }

            if (String.IsNullOrWhiteSpace(txtVisa.Text))
            {

                txtVisa.Text = "0";
            }




            if (Application.OpenForms.OfType<frmCafeSales>().Any())
            {


                frmCafeSales frm = (frmCafeSales)Application.OpenForms["frmCafeSales"];
                if (frm.Status == "Old")
                {


                    // Remove All Old Invoce Detail  (Sale Detail)
                    var Details = context.SaleDetails.Where(w => w.SaleMasterCode == frm.SaleMasterCode && w.shiftCode == frm.ShiftCode && w.Warhouse_Code == frm.Warehouse_Code);
                    context.SaleDetails.RemoveRange(Details);
                    context.SaveChanges();



                    // عمل ارجاع للكميات الماخوذه من المخزن

                    var historyTrans = context.Item_History_transaction.Where(w => w.SaleMasterCode == frm.SaleMasterCode && w.from_Warhouse_Code == frm.Warehouse_Code && w.shiftCode == frm.ShiftCode && w.IsDeleted == 0).ToList();



                    historyTrans.ForEach(x => {

                        using (POSEntity context100 = new POSEntity())
                        {
                            // Update Item History  
                            frm.item_History = context100.Item_History.SingleOrDefault(ItemHis => ItemHis.Item_Id == x.Item_Id && ItemHis.Warhouse_Code == frm.Warehouse_Code && ItemHis.Id == x.Item_History_Id);
                            frm.item_History.Current_Qty_Now += x.Trans_Out;
                            frm.item_History.Is_Used = true;
                            frm.item_History.IsFinshed = false;
                            context100.SaveChanges();

                        }

                        using (POSEntity context110 = new POSEntity())
                        {
                            // Update Item History  
                            // Update _Item_History_transaction 
                            frm._Item_History_transaction = context110.Item_History_transaction.SingleOrDefault(Item => Item.Id == x.Id && Item.from_Warhouse_Code == frm.Warehouse_Code);
                            frm._Item_History_transaction.IsDeleted = 1;
                            context110.SaveChanges();

                        }

                    });






                    //''''''''''''''''''''''''''''''
                    using (POSEntity context120 = new POSEntity())
                    {
                        foreach (var item in frm.GetDataFromGrid)
                        {

                            var qty = item.Qty;

                            #region


                            frm.Item_Qty_List = context.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false && w.Warhouse_Code == frm.Warehouse_Code).ToList();


                            frm.Item_Qty_List.ForEach(x =>
                            {
                                if (qty > 0)
                                {
                                    // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
                                    if (qty > x.Current_Qty_Now)
                                    {
                                        qty = qty - x.Current_Qty_Now;

                                       frm.Update_Item_Qty_And_Finshed(frm.SaleMasterCode, x.Current_Qty_Now, x.CreatedDate, x.Id, x.Item_Id, shiftCode: frm.ShiftCode);
                                    }
                                    else if (qty == x.Current_Qty_Now)
                                    {

                                        frm.Update_Item_Qty_And_Finshed(frm.SaleMasterCode, qty, x.CreatedDate, x.Id, x.Item_Id, shiftCode: frm.ShiftCode);
                                        qty = 0;

                                    }
                                    else if (qty < x.Current_Qty_Now)
                                    {


                                        frm.Update_Item_Qty_Oly(frm.SaleMasterCode, qty, x.CreatedDate, x.Id, x.Item_Id, shiftCode: frm.ShiftCode);
                                        qty = 0;

                                    }



                                }


                            });
                            #endregion

                            SaleDetail _SaleDetail = new SaleDetail()
                            {
                                ItemCode = item.ItemCode,
                                Price = item.Price,
                                Qty = item.Qty,
                                Total = item.Total,
                                EntryDate = DateTime.Now,
                                shiftCode = frm.ShiftCode,
                                LineSequence = item.LineSequence,
                                CustomerCode = 0,
                                Branch_Id = frm.Branch_Code,
                                Warhouse_Code = frm.Warehouse_Code,

                                SaleDetailCode = Int64.Parse(frm.lblSaleMasterId.Text),
                                SaleMasterCode = Int64.Parse(frm.lblSaleMasterId.Text),
                                LastDateModif = DateTime.Now,
                                Operation_Type_Id = 2,
                                UserId = st.GetUser_Code()



                            };

                            frm.ArryOfSaleDetail.Add(_SaleDetail);

                        }
                        context120.SaleDetails.AddRange(frm.ArryOfSaleDetail);
                        context120.SaveChanges();
                        frm.SaveSaleMaster(
                            
                             ShiftCode: frm.ShiftCode,

                             UserCode: st.GetUser_Code(),
                             Discount: double.Parse(frm.lblDiscount.Text),
                             TotalBeforDiscount: double.Parse(frm.lblFinalBeforDesCound.Text),
                             FinalTotal: double.Parse(frm.lblFinalTotal.Text),
                             QtyTotal: double.Parse(frm.lblItemQty.Text),
                             SaleMasterCode: frm.SaleMasterCode,
                             cash:Convert.ToDouble(txtCash.Text) ,
                             visa : Convert.ToDouble(txtVisa.Text)
                            );
                    }
                    frm.printSaleInvoice(shiftCode: frm.ShiftCode, SaleMasterCode: frm.SaleMasterCode);

                }
                else
                {

                    // Get New SaleMaster Code
                    Int64 newMasterCode = 0;
                    using (POSEntity context100 = new POSEntity())
                    {
                        try
                        {
                            newMasterCode = context100.SaleMasters.Where(x => x.ShiftCode == frm.ShiftCode && x.Branch_Id == frm.Branch_Code && (x.Operation_Type_Id == 2 || x.Operation_Type_Id == 3)).Max(u => (Int64)u.SaleMasterCode + 1);
                        }
                        catch
                        {
                            newMasterCode = 1;
                        }


                    }




                    // Save Header
                    frm.SaveSaleMaster(
                         cash: Convert.ToDouble(txtCash.Text),
                             visa: Convert.ToDouble(txtVisa.Text),
                   ShiftCode: frm.ShiftCode,
                   UserCode: st.GetUser_Code(),
                   Discount: double.Parse(frm.lblDiscount.Text),
                   TotalBeforDiscount: double.Parse(frm.lblFinalBeforDesCound.Text),
                   FinalTotal: double.Parse(frm.lblFinalTotal.Text),
                   QtyTotal: double.Parse(frm.lblItemQty.Text),
                   SaleMasterCode: newMasterCode
                  );

                    foreach (var item in frm.GetDataFromGrid)
                    {

                        var qty = item.Qty;

                        #region




                        using (POSEntity context100 = new POSEntity())
                        {
                            frm.Item_Qty_List = context100.Item_History.Where(w => w.Item_Id == item.ItemCode && w.IsFinshed == false && w.Warhouse_Code == frm.Warehouse_Code).ToList();


                            frm.Item_Qty_List.ForEach(x =>
                            {
                                if (qty > 0)
                                {
                                    // لو الكمية المطلوبه اكبر من الكمية الموجوده في الصف 
                                    if (qty >= x.Current_Qty_Now)
                                    {

                                        frm.Update_Item_Qty_And_Finshed(newMasterCode, x.Current_Qty_Now, x.CreatedDate, x.Id, x.Item_Id, shiftCode: frm.ShiftCode);
                                        qty = qty - x.Current_Qty_Now;

                                        if (qty < 0)
                                        {
                                            qty = 0;
                                        }
                                    }

                                    else if (qty < x.Current_Qty_Now)
                                    {


                                        frm.Update_Item_Qty_Oly(newMasterCode, qty, x.CreatedDate, x.Id, x.Item_Id, shiftCode: frm.ShiftCode);
                                        qty = 0;

                                    }



                                }


                            });
                        }

                        #endregion

                        SaleDetail _SaleDetail = new SaleDetail()
                        {
                            ItemCode = item.ItemCode,
                            Price = item.Price,
                            Qty = item.Qty,
                            Total = item.Total,
                            EntryDate = DateTime.Now,
                            shiftCode = frm.ShiftCode,
                            LineSequence = item.LineSequence,
                            CustomerCode = 0,
                            Warhouse_Code = frm.Warehouse_Code,
                            Branch_Id = frm.Branch_Code,

                            SaleDetailCode = newMasterCode,
                            SaleMasterCode = newMasterCode,
                            LastDateModif = DateTime.Now,
                            Operation_Type_Id = 2,
                            UserId = st.GetUser_Code()



                        };

                        frm.ArryOfSaleDetail.Add(_SaleDetail);

                    }


                    context.SaleDetails.AddRange(frm.ArryOfSaleDetail);
                    context.SaveChanges();
                    while (frm.gvSaleDetail.RowCount > 0)
                    {
                        frm.gvSaleDetail.SelectAll();
                        frm.gvSaleDetail.DeleteSelectedRows();
                        frm.gcCafeSaleDetail.RefreshDataSource();

                    }



                    frm.printSaleInvoice2(shiftCode: frm.ShiftCode, SaleMasterCode: newMasterCode);
                }
                txtVisa.Text = "0";
                txtCash.Text = "0";
                this.Close();
            }
            

        }

        private void txtCash_EditValueChanged(object sender, EventArgs e)
        {
            double cash, visa;
            if (String.IsNullOrWhiteSpace(txtCash.Text))
            {
                cash = 0.0;
            }
            else
            {

                cash = Convert.ToDouble(txtCash.Text);
            }
            if (String.IsNullOrWhiteSpace(txtVisa.Text))
            {
                visa = 0.0;
            }
            else
            {

                visa = Convert.ToDouble(txtVisa.Text);
            }

            


            lblTotal.Text = ((cash + visa) + total).ToString();


        }

        private void txtVisa_EditValueChanged(object sender, EventArgs e)
        {
            double cash, visa;
            if (String.IsNullOrWhiteSpace(txtCash.Text))
            {
                cash = 0.0;
            }
            else
            {

                cash = Convert.ToDouble(txtCash.Text);
            }
            if (String.IsNullOrWhiteSpace(txtVisa.Text))
            {
                visa = 0.0;
            }
            else
            {

                visa = Convert.ToDouble(txtVisa.Text);
            }

             


            lblTotal.Text = ((cash + visa) + total).ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}