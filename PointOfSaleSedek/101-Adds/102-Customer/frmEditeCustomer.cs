using System;
using PointOfSaleSedek.HelperClass;
using DataRep;

namespace PointOfSaleSedek._101_Adds._102_Customer
{
    public partial class frmEditeCustomer : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmEditeCustomer()
        {
            InitializeComponent();
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      public  void FillSexSlk()
        {

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
             
        }

        void EditItem()
        {
           
            

        }
    }
}