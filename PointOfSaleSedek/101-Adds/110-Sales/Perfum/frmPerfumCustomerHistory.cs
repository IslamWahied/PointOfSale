

using DataRep;
using PointOfSaleSedek.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PointOfSaleSedek._101_Adds._110_Sales
{
    public partial class frmPerfumCustomerHistory : DevExpress.XtraEditors.XtraForm
    {
        POSEntity context = new POSEntity();
        public frmPerfumCustomerHistory()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            if (Application.OpenForms.OfType<frmPerfumSales>().Any())
            {

                List<SaleDetailPrfumViewVm> listSaleDetailPrfumViewVm = new List<SaleDetailPrfumViewVm>();

                frmPerfumSales frm = (frmPerfumSales)Application.OpenForms["frmPerfumSales"];
                var customerCode = Convert.ToInt64(frm.slkCustomers.EditValue);

                Customer_Info customer = context.Customer_Info.Where(x => x.Customer_Code == customerCode).First();


                customer.CustomerFavourit = txtHistory.Text??"";

                context.SaveChanges();
                this.Close();


            }

             
        }
    }
}