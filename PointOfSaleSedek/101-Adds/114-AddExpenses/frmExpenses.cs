using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using PointOfSaleSedek._102_MaterialSkin;
using DevExpress.XtraGrid;
using DataRep;


namespace PointOfSaleSedek._114_Adds
{
    public partial class frmExpenses : DevExpress.XtraEditors.XtraForm
    {
        readonly SaleEntities context = new SaleEntities();
        public frmExpenses()
        {
            InitializeComponent();

            FillExpensesGrid();
        }

        
       


        public void FillExpensesGrid()
        {
            var result = (from a in context.Expenses where a.IsDeleted == 0 select a).ToList();
            gcExpenses.DataSource = result;
        }
        
      

        void AddExpenses()
        {
            if (string.IsNullOrWhiteSpace(txtExpenses.Text))
            {

                MaterialMessageBox.Show("!برجاء كتابة اسم المصروف ", MessageBoxButtons.OK);
                return;
            }

            bool TestUpdate = context.Expenses.Any(Unit => Unit.ExpensesName == txtExpenses.Text && Unit.IsDeleted == 0);
            if (TestUpdate)
            {


                MaterialMessageBox.Show("!تم نسجبلة من قبل", MessageBoxButtons.OK);

            }
            else
            {
                Int64? NewCode = context.Expenses.Max(u => (Int64?)u.ExpensesCode);
                if (NewCode == null)
                {

                    NewCode = 0;


                }
                NewCode += 1;


                Expens _Expenses = new Expens()
                {
                    ExpensesCode = Convert.ToInt64(NewCode),
                    ExpensesName = txtExpenses.Text,
                   
                };
                context.Expenses.Add(_Expenses);
                context.SaveChanges();
                FillExpensesGrid();
                txtExpenses.ResetText();
                //MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);

            }

        }



       
     

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            AddExpenses();
        }

        private void txtUnitName_KeyDown(object sender, KeyEventArgs e)
       {
            if (e.KeyCode == Keys.Enter)
            {

                AddExpenses();



            }
        }

        private void repdelete1_Click(object sender, EventArgs e)
        {
            if (gvUnitCard.RowCount <= 0)
            {

                MaterialMessageBox.Show("!لا يوجد بيانات للحذف", MessageBoxButtons.OK);
                return;
            }

            Expens xx = gvUnitCard.GetFocusedRow() as Expens;
            Int64 ExpensCode = Convert.ToInt64(xx.ExpensesCode);
            bool CheckRelation = context.ExpensesTransactions.Where(x => x.IsDeleted == 0  ).Any(x => x.ExpensesCode == ExpensCode);

            if (CheckRelation)
            {

                MaterialMessageBox.Show("لا يمكن حذف هذا المصروف لوجود عمليات عليه", MessageBoxButtons.OK);
                return;

            }
            using (SaleEntities Context = new SaleEntities())
            {
                Expens deptDelete = Context.Expenses.Where(x=>x.ExpensesCode== ExpensCode).FirstOrDefault();
                Context.Expenses.Remove(deptDelete);
                Context.SaveChanges();
                FillExpensesGrid();
            }
        }

        
    }
}