using DevExpress.XtraEditors;
using DataRep;
using PointOfSaleSedek._102_MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSaleSedek.Model;
using FastReport;
using PointOfSaleSedek.HelperClass;
using DevExpress.XtraBars.Docking2010;
using PointOfSaleSedek._102_Reports.ProductsRate;
using DevExpress.XtraSplashScreen;

namespace PointOfSaleSedek._102_Reports.ProductsRate
{
    public partial class frmProductsRate : DevExpress.XtraEditors.XtraForm
    {
        POSEntity context = new POSEntity();
        BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities();
        readonly Static st = new Static();
        List<BackOfficeEntity.SaleDetail> listServerDetails = new List<BackOfficeEntity.SaleDetail>();
        List<SaleDetail> listLocalDetails = new List<SaleDetail>();
        public frmProductsRate()
        {
            InitializeComponent();
            langu();
            Int64 branchCode = st.GetBranch_Code();
            if (branchCode != 0)
            {
                fillGrid();
            }
            FillslkWarhouse();
        }

        public void FillslkWarhouse()
        {
            DataTable dt = new DataTable();
            var result = context.Branches.ToList();
            slkWarhouse.Properties.DataSource = result;
            slkWarhouse.Properties.ValueMember = "Branches_Code";
            slkWarhouse.Properties.DisplayMember = "Branches_Name";

            Int64 branchCode = st.GetBranch_Code();
            if (branchCode != 0)
            {
                slkWarhouse.ReadOnly = true;
                slkWarhouse.Enabled = false;
                slkWarhouse.EditValue = branchCode;
                slkWarhouse.Enabled = false;

            }
            
        }

        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            //this.RightToLeftLayout = st.isEnglish() ? true : false;
            this.Text = st.isEnglish() ? "Products Stats" : "احصائيات المنتجات";


            labelControl1.Text = st.isEnglish() ? "From Date" : "من تاريخ";
            labelControl2.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
            labelControl3.Text = st.isEnglish() ? "Branch" : "الفرع";
           

            //btnShow.Text = st.isEnglish() ? "View" : "عرض";

            gridColumn12.Caption = st.isEnglish() ? "Category" : "المجموعة";
            gridColumn7.Caption = st.isEnglish() ? "Product Code" : "كود الصنف";
            gridColumn11.Caption = st.isEnglish() ? "Product AR-Name" : "الاسم العربي للصنف";
            gridColumn4.Caption = st.isEnglish() ? "Product En-Name" : "الاسم الانجليزي للصنف";
            gridColumn1.Caption = st.isEnglish() ? "Unit" : "وحدة القياس";
           
            gridColumn2.Caption = st.isEnglish() ? "Quantity Sold" : "الكمية المباعة";
            gridColumn3.Caption = st.isEnglish() ? "Percentage" : "النسبة المئوية";
           
            gvItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";

            windowsUIButtonPanel.Buttons[0].Properties.Caption = st.isEnglish() ? "Close" : "اغلاق";
            windowsUIButtonPanel.Buttons[2].Properties.Caption = st.isEnglish() ? "Print" : "طباعة";
        }

        private void dtFrom_Properties_EditValueChanged(object sender, EventArgs e)
        {

            fillGrid();

        }



        void fillGrid() {



            if (String.IsNullOrWhiteSpace(dtFrom.Text) || String.IsNullOrWhiteSpace(dtTo.Text)) {


                //MaterialMessageBox.Show(st.isEnglish() ? "Pleast Select From And To Data" : "برجاء اختيار التاريخ بطريقه صحيحه", MessageBoxButtons.OK);
                return;
            
            }

            SplashScreenManager.ShowForm(typeof(WaitForm1));

            try
            {
                listServerDetails = new List<BackOfficeEntity.SaleDetail>();
                listLocalDetails = new List<SaleDetail>();

                DateTime dateTo = new DateTime();

                if (!String.IsNullOrWhiteSpace(dtTo.Text))
                {
                    dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));

                }


                List<ProductRate> productRateList = new List<ProductRate>();
                List<ItemCardView> listItems = context.ItemCardViews.Where(a => a.IsDeleted == 0).ToList().OrderBy(x => x.ItemCode).ToList();
                double totalqt = 0;
               

                // Server
                Int64 branchCode = st.GetBranch_Code();
                if (branchCode == 0)
                {

                    Int64 selectedBranch = 0;
                    String selectedBranchName = "";

                    if (!String.IsNullOrWhiteSpace(slkWarhouse.Text))
                    {
                        selectedBranch = Convert.ToInt64(slkWarhouse.EditValue);
                        selectedBranchName = slkWarhouse.Text;
                    }



                    if (!String.IsNullOrWhiteSpace(slkWarhouse.Text))
                    {



                        // if Search By From  And To
                        if (!String.IsNullOrWhiteSpace(dtFrom.Text) && !String.IsNullOrWhiteSpace(dtTo.Text))
                        {


                            listServerDetails = _server.SaleDetail.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo && a.Branch_Id == selectedBranch).ToList();
                        }
                          


                        totalqt = listServerDetails.Sum(x => x.Qty);

                   

                        listItems.ForEach(x => {
                            double itemSaleQty = Convert.ToDouble(listServerDetails.Where(a => a.ItemCode == x.ItemCode).ToList().Sum(c => c.Qty));

                            double itemTotal = Convert.ToDouble(listServerDetails.Where(a => a.ItemCode == x.ItemCode).ToList().Sum(c => c.Total));

                            



                          

                            ProductRate model = new ProductRate()
                            {

                                Category = x.CategoryName ?? "",
                                ProductCode = x.ItemCode,
                                BranchName = selectedBranchName,
                                ProductName = x.Name,
                               
                                ProductEnName = x.Name_En,
                                Totla = itemTotal,
                                ProductUnite = x.UnitName,
                                ProductQtySale = itemSaleQty > 0 ? itemSaleQty : Convert.ToDouble(0),
                                ProductPresdentSale = (itemSaleQty / totalqt) > 0 ? (itemSaleQty / totalqt) : Convert.ToDouble(0)

                            };
                            productRateList.Add(model);
                        });







                    }

                }

                // Local
                else
                {


                    List<SaleDetail> listLocalDetails = new List<SaleDetail>();


                    // if Search By From  And To
                    if (!String.IsNullOrWhiteSpace(dtFrom.Text) && !String.IsNullOrWhiteSpace(dtTo.Text))
                    {


                        listLocalDetails = context.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo).ToList();
                    }
                    
 


                    totalqt = listLocalDetails.Sum(x => x.Qty);
                    listItems.ForEach(x => {
                        double itemSaleQty = Convert.ToDouble(listLocalDetails.Where(a => a.ItemCode == x.ItemCode).ToList().Sum(c => c.Qty));
                        double itemTotal = Convert.ToDouble(listLocalDetails.Where(a => a.ItemCode == x.ItemCode).ToList().Sum(c => c.Total));
                        ProductRate model = new ProductRate()
                        {
                            Category = x.CategoryName ?? "",
                            ProductCode = x.ItemCode,
                            Totla = itemTotal,
                            BranchName = slkWarhouse.Text??"",
                             
                          
                            
                            ProductName = x.Name,
                            ProductEnName = x.Name_En,
                            ProductUnite = x.UnitName,
                            ProductQtySale = itemSaleQty > 0 ? itemSaleQty : Convert.ToDouble(0),
                            ProductPresdentSale = (itemSaleQty / totalqt) > 0 ? (itemSaleQty / totalqt) : Convert.ToDouble(0)

                        };
                        productRateList.Add(model);
                    });



                }




                productRateList.ForEach(x => {

                    double v1 = productRateList.Sum(cc => cc.ProductQtySale);

                    double totalCategorySaleQty = productRateList.Where(ee => ee.Category == x.Category).Sum(cc => cc.ProductQtySale);

                    double CategoryPresdentSale = productRateList.Where(ee => ee.Category == x.Category).Sum(cc => cc.ProductPresdentSale);

                    productRateList.Where(ff=>ff.ProductCode == x.ProductCode).FirstOrDefault().CategoryQtyItemSealsSale =  totalCategorySaleQty;
                    productRateList.Where(ff=>ff.ProductCode == x.ProductCode).FirstOrDefault().CategoryPresdentSale = CategoryPresdentSale;

              
                    

                });



                gcItemCard.DataSource = productRateList.ToList();
                gcItemCard.RefreshDataSource();
                SplashScreenManager.CloseForm();
            }
            catch {

                SplashScreenManager.CloseForm();
            }

           
        }

      

        private void windowsUIButtonPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            WindowsUIButton btn = e.Button as WindowsUIButton;
            if (btn.Caption == "اغلاق" || btn.Caption == "Close")
            {
                this.Close();
            }
            else if (btn.Caption == "طباعة" || btn.Caption == "Print") {

                if (gvItemCard.RowCount > 0)
                {
                    List<ProductRate> productRateList = new List<ProductRate>();

                    productRateList = gcItemCard.DataSource as List<ProductRate>;

                    DataTable dtToDateTime = new DataTable();
                    dtToDateTime.Columns.Add("FromDate", typeof(string));
                    dtToDateTime.Columns.Add("ToDate", typeof(string));
                    DataRow drr = dtToDateTime.NewRow();
                    drr[0] = dtFrom.Text;
                    drr[1] = dtTo.Text;
                    dtToDateTime.Rows.Add(drr);
                    Report rpt = new Report();
                    rpt.Load(@"Reports\ProductRate.frx");
                    rpt.RegisterData(productRateList, "productRateList");
                    rpt.RegisterData(dtToDateTime, "dtToDateTime");
                    rpt.PrintSettings.ShowDialog = false;
                    rpt.Show();
                }
                else {
                    MaterialMessageBox.Show(st.isEnglish() ? " No Date To Print" : "لا يوجد بيانات", MessageBoxButtons.OK);
                    
                }



            }
             
        }

        private void dtTo_EditValueChanged_1(object sender, EventArgs e)
        {
            fillGrid();
        }

       

        private void slkWarhouse_EditValueChanged(object sender, EventArgs e)
        {
            fillGrid();
        }

     

        private void detailToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmItemRateDetail frm = new frmItemRateDetail();
          
            var selectedRow = gvItemCard.GetFocusedRow() as ProductRate;
            Int64 selectedBranch = 0;
            DateTime dateTo = new DateTime();

            if (!String.IsNullOrWhiteSpace(dtTo.Text))
            {
                dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));

            }

            if (!String.IsNullOrWhiteSpace(slkWarhouse.Text))
            {
                selectedBranch = Convert.ToInt64(slkWarhouse.EditValue);
             
            }


           
            frm.gridControl1.DataSource = listServerDetails.Where(x=>x.ItemCode == selectedRow.ProductCode && x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo && x.Branch_Id == selectedBranch).ToList();
            frm.ShowDialog();





            //Report rpt = new Report();
            //rpt.Load(@"Reports\ProductRateDetail.frx");

            //rpt.RegisterData(listServerDetails.ToList(), "productRateList");


            //rpt.PrintSettings.ShowDialog = false;
            //rpt.Design();
        }

        private void categoryDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoryRateDetail frm = new frmCategoryRateDetail();
            var selectedRow = gvItemCard.GetFocusedRow() as ProductRate;
            frm.lblCategoryName.Text = selectedRow.Category;
            frm.label4.Text = selectedRow.CategoryPresdentSale.ToString();
            frm.label5.Text = selectedRow.CategoryQtyItemSealsSale.ToString();
            frm.ShowDialog();

        }

        private void itemDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmItemRateDetail frm = new frmItemRateDetail();

            var selectedRow = gvItemCard.GetFocusedRow() as ProductRate;
            Int64 selectedBranch = 0;
            DateTime dateTo = new DateTime();

            if (!String.IsNullOrWhiteSpace(dtTo.Text))
            {
                dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));

            }

            if (!String.IsNullOrWhiteSpace(slkWarhouse.Text))
            {
                selectedBranch = Convert.ToInt64(slkWarhouse.EditValue);

            }



            frm.gridControl1.DataSource = listServerDetails.Where(x => x.ItemCode == selectedRow.ProductCode && x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.EntryDate >= dtFrom.DateTime && x.EntryDate <= dateTo && x.Branch_Id == selectedBranch).ToList();
            frm.ShowDialog();

        }

        private void categoryDetailToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmCategoryRateDetail frm = new frmCategoryRateDetail();
            var selectedRow = gvItemCard.GetFocusedRow() as ProductRate;
            frm.lblCategoryName.Text = selectedRow.Category;
            frm.label4.Text = selectedRow.CategoryPresdentSale.ToString();
            frm.label5.Text = selectedRow.CategoryQtyItemSealsSale.ToString();
            frm.ShowDialog();
        }
    }
}