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
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds._114_AddExpenses
{
    public partial class FrmCancelExpenses : DevExpress.XtraEditors.XtraForm
    {
        POSEntity context = new POSEntity();
        Static st = new Static();
        public FrmCancelExpenses()
        {
            InitializeComponent();
            langu();
        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.tableLayoutPanel2.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;

            this.Text = st.isEnglish() ? "Cancel Expense" : "اللغاء مصروف";
            this.labelControl5.Text = st.isEnglish() ? "From Date" : "من تاريخ";
            this.labelControl1.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
            btnShow.Text = st.isEnglish() ? "View" : "عرض";

            this.gridColumn7.Caption = st.isEnglish() ? "Invoice No" : "رقم الفاتورة";
            this.gridColumn1.Caption = st.isEnglish() ? "Total" : "المبلغ";
            this.gridColumn2.Caption = st.isEnglish() ? "Note" : "الملاحظات";
            this.gridColumn10.Caption = st.isEnglish() ? "Date" : "التاريخ";
            this.gridColumn3.Caption = st.isEnglish() ? "UserName" : "المستخدم";
            contextMenuStrip1.Items[0].Text = st.isEnglish() ? "Delete" : "حذف";
            gvItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";



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
                MaterialMessageBox.Show(st.isEnglish()?"There are no results for this date":"لا يوجد نتائج لهذا التاريخ", MessageBoxButtons.OK);
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
            if (MaterialMessageBox.Show(st.isEnglish()? "Are you sure to delete?" : "تاكيد الحذف", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                ExpensesView xx = gvItemCard.GetFocusedRow() as ExpensesView;
                ExpensesTransaction _Expens = new ExpensesTransaction();
                if (_Expens != null) {
                    _Expens = context.ExpensesTransactions.SingleOrDefault(item => item.Id == xx.Id);
                    _Expens.IsDeleted = 1;
                    context.SaveChanges();
                    FillGride();
                }
                
            }
        }
    }
}