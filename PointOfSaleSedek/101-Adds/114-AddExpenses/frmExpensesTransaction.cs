using DevExpress.XtraEditors;
using DataRep;
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
        readonly SaleEntities context = new SaleEntities();
        public frmExpensesTransaction()
        {
            InitializeComponent();
            FillSlkExpenses();
            FillSlkEmployees();
        }
        public void FillSlkEmployees()
        {
            var result = context.Employee_View.Where(user => user.IsDeleted == 0 && user.Employee_Code != 0).ToList();
            slkEmployees.Properties.DataSource = result;
            slkEmployees.Properties.ValueMember = "Employee_Code";
            slkEmployees.Properties.DisplayMember = "Employee_Name";

          
            

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
            Int64 UserCode = st.User_Code();
            var isShiftActive = context.Shift_View.Any(x => x.User_Id == UserCode && x.Shift_Flag == true);

            if (!isShiftActive) {
                MaterialMessageBox.Show("برجاء اضافة وردية للمستخدم", MessageBoxButtons.OK);
                return;
            }

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
                var ShiftCode = context.Shift_View.Where(x => x.User_Id == UserCode && x.Shift_Flag == true).Select(xx => xx.Shift_Code).SingleOrDefault();
                // No Select Employee
                if (!string.IsNullOrWhiteSpace(slkEmployees.Text))
                {
                    ExpensesTransaction _Expenses = new ExpensesTransaction()
                    {
                        ExpensesCode = Convert.ToInt64(slkExpenses.EditValue),
                        ExpensesNotes = txtNote.Text,
                        ExpensesQT = Convert.ToInt64(txtDiscount.Text),
                        Date = DateTime.Now,
                        IsDeleted = 0,
                        
                        Shift_Code = ShiftCode,
                        Last_Modified_By = st.User_Code(),
                        Last_Modified_Date = DateTime.Now,
                        Emp_Code = Convert.ToInt64(slkEmployees.EditValue)


                    };
                    context.ExpensesTransactions.Add(_Expenses);
                    

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
                        Shift_Code = ShiftCode,
                        Last_Modified_By = st.User_Code(),
                        Last_Modified_Date = DateTime.Now,
                        Emp_Code = Convert.ToInt64(0)

                    };
                    context.ExpensesTransactions.Add(_Expenses);
                    


                }


                context.SaveChanges();
                txtDiscount.Text = "0";
                txtNote.ResetText();
               
             
                MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);

            }


        }
    }
}