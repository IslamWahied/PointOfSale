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

namespace PointOfSaleSedek._101_Adds._114_AddExpenses
{
    public partial class frmProductsRate : DevExpress.XtraEditors.XtraForm
    {
        POSEntity context = new POSEntity();
        BackOfficeEntity.db_a8f74e_posEntities _server = new BackOfficeEntity.db_a8f74e_posEntities();
        readonly Static st = new Static();
        public frmProductsRate()
        {
            InitializeComponent();
            langu();
            fillGrid();
        }



        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            //this.RightToLeftLayout = st.isEnglish() ? true : false;
            this.Text = st.isEnglish() ? "Products Stats" : "احصائيات المنتجات";


            labelControl1.Text = st.isEnglish() ? "From Date" : "من تاريخ";
            labelControl2.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
           

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

                List<BackOfficeEntity.SaleDetail> listServerDetails = new List<BackOfficeEntity.SaleDetail>();
              
              
                // if Search By From  And To
                if (!String.IsNullOrWhiteSpace(dtFrom.Text) && !String.IsNullOrWhiteSpace(dtTo.Text))
                {
                  

                    listServerDetails = _server.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo).ToList();
                }
                // Search By From only
                else if (String.IsNullOrWhiteSpace(dtTo.Text))
                {
                    listServerDetails = _server.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate >= dtFrom.DateTime ).ToList();
                }
                // // Search By To only
                else if (String.IsNullOrWhiteSpace(dtFrom.Text))
                {
                  
                    listServerDetails = _server.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 &&  a.EntryDate <= dateTo).ToList();

                }


                // Search By All
                else
                {
                    listServerDetails = _server.SaleDetails.Where(a => a.IsDeleted == 0).ToList();
                }

              
                 totalqt = listServerDetails.Sum(x => x.Qty);
                listItems.ForEach(x => {
                    double itemSaleQty = Convert.ToDouble(listServerDetails.Where(a => a.ItemCode == x.ItemCode).ToList().Sum(c => c.Qty));
                    ProductRate model = new ProductRate()
                    {

                        Category = x.CategoryName ?? "",
                        ProductCode = x.ItemCode,
                        
                        ProductName = x.Name,
                        ProductEnName = x.Name_En,
                      
                        ProductUnite = x.UnitName,
                        ProductQtySale = itemSaleQty > 0 ? itemSaleQty : Convert.ToDouble(0),
                        ProductPresdentSale = (itemSaleQty / totalqt) >0 ? (itemSaleQty / totalqt) : Convert.ToDouble(0)

                    };
                    productRateList.Add(model);
                });

            }

            // Local
            else {


                List<SaleDetail> listLocalDetails = new List<SaleDetail>();
               

                // if Search By From  And To
                if (!String.IsNullOrWhiteSpace(dtFrom.Text) && !String.IsNullOrWhiteSpace(dtTo.Text))
                {
                  

                    listLocalDetails = context.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate >= dtFrom.DateTime && a.EntryDate <= dateTo).ToList();
                }
                // Search By From only
                else if (String.IsNullOrWhiteSpace(dtTo.Text))
                {
                    listLocalDetails = context.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate >= dtFrom.DateTime).ToList();
                }
                // // Search By To only
                else if (String.IsNullOrWhiteSpace(dtFrom.Text))
                {
                   
                    listLocalDetails = context.SaleDetails.Where(a => a.IsDeleted == 0 && a.Operation_Type_Id == 2 && a.EntryDate <= dateTo).ToList();

                }


                // Search By All
                else
                {
                    listLocalDetails = context.SaleDetails.Where(a => a.IsDeleted == 0).ToList();
                }


                totalqt = listLocalDetails.Sum(x => x.Qty);
                listItems.ForEach(x => {
                    double itemSaleQty = Convert.ToDouble(listLocalDetails.Where(a => a.ItemCode == x.ItemCode).ToList().Sum(c => c.Qty));
                    ProductRate model = new ProductRate()
                    {

                        Category = x.CategoryName ?? "",
                        ProductCode = x.ItemCode,

                        ProductName = x.Name,
                        ProductEnName = x.Name_En,
                        
                        ProductUnite = x.UnitName,
                        ProductQtySale = itemSaleQty > 0 ? itemSaleQty : Convert.ToDouble(0),
                        ProductPresdentSale = (itemSaleQty / totalqt) > 0 ? (itemSaleQty / totalqt) : Convert.ToDouble(0)

                    };
                    productRateList.Add(model);
                });

            }


            gcItemCard.DataSource = productRateList.ToList();
            gcItemCard.RefreshDataSource();
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
    }
}