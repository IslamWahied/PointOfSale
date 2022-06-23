using System;
using PointOfSaleSedek.HelperClass;
using EntityData;

namespace PointOfSaleSedek._101_Adds._102_Customer
{
    public partial class frmEditeCustomer : DevExpress.XtraEditors.XtraForm
    {
        readonly PointOfSaleEntities2 context = new PointOfSaleEntities2();
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