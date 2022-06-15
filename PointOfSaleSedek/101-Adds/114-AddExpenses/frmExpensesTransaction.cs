using DevExpress.XtraEditors;
using EntityData;
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

namespace PointOfSaleSedek._101_Adds._114_AddExpenses
{
    public partial class frmExpensesTransaction : DevExpress.XtraEditors.XtraForm
    {
        Static st = new Static();
        readonly PointOfSaleEntities2 context = new PointOfSaleEntities2();
        public frmExpensesTransaction()
        {
            InitializeComponent();
            FillSlkExpenses();
        }

        public void FillSlkExpenses()
        {
                var Expenses = context.Expenses.Where(x => x.IsDeleted == 0).ToList();
                slkExpenses.Properties.DataSource = Expenses;
                slkExpenses.Properties.ValueMember = "ExpensesCode";
                slkExpenses.Properties.DisplayMember = "ExpensesName";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(slkExpenses.Text))
            {
                MaterialMessageBox.Show("برجاءاختيار المصروف", MessageBoxButtons.OK);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                MaterialMessageBox.Show("برجاءادخال قيمة المصروف", MessageBoxButtons.OK);
                return;
            }
            else
            {


                ExpensesTransaction _Expenses = new ExpensesTransaction()
                {
                    ExpensesCode = Convert.ToInt64(slkExpenses.EditValue),
                    ExpensesNotes = txtNote.Text,
                    ExpensesQT = Convert.ToInt64(txtDiscount.Text),
                    Date = DateTime.Now,
                    IsDeleted = 0,
                    Last_Modified_By = st.User_Code(),
                    Last_Modified_Date = DateTime.Now
                
                 
                };
                context.ExpensesTransactions.Add(_Expenses);
                context.SaveChanges();

               
                txtDiscount.Text = "0";
                txtNote.ResetText();
               
             
                MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);

            }


        }
    }
}