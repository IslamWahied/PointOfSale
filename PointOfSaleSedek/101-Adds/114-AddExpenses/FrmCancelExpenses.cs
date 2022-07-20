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

namespace PointOfSaleSedek._101_Adds._114_AddExpenses
{
    public partial class FrmCancelExpenses : DevExpress.XtraEditors.XtraForm
    {
        SaleEntities context = new SaleEntities();
        public FrmCancelExpenses()
        {
            InitializeComponent();
        }

        private void FrmCancelExpenses_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
           
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
           var datefrom = Convert.ToDateTime(Convert.ToDateTime(dtFrom.EditValue).AddDays(1));
            var Master = context.ExpensesViews.Where(a => a.Date >= dtFrom.DateTime && a.Date <= datefrom && a.IsDeleted == 0).ToList();
            if (Master.Count == 0)

            {
                MaterialMessageBox.Show("لا يوجد نتائج لهذا التاريخ", MessageBoxButtons.OK);
                return;

            }
            else
            {

                gcItemCard.DataSource = null;
                gcItemCard.DataSource = Master;
                gcItemCard.RefreshDataSource(); ;

            
            
            }
             
        }

        void FillGride()
        {
            var datefrom = Convert.ToDateTime(Convert.ToDateTime(dtFrom.EditValue).AddDays(1));
            var Master = context.ExpensesViews.Where(a => a.Date >= dtFrom.DateTime && a.Date <= datefrom && a.IsDeleted == 0).ToList();
            if (Master.Count != 0)

            {
                gcItemCard.DataSource = null;
                gcItemCard.DataSource = Master;
                gcItemCard.RefreshDataSource();
                gcItemCard.Enabled = true;

            }
            else
            {

                gcItemCard.DataSource = null;
              
                gcItemCard.RefreshDataSource(); ;

                gcItemCard.Enabled = false;

            }
        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show("تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                ExpensesView xx = gvItemCard.GetFocusedRow() as ExpensesView;
                ExpensesTransaction _Expens = new ExpensesTransaction();
                _Expens = context.ExpensesTransactions.SingleOrDefault(item => item.Id == xx.Id );
                _Expens.IsDeleted = 1;
                context.SaveChanges();
                FillGride();
            }
        }
    }
}