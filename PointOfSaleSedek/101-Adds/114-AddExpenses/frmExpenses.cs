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
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._114_Adds
{
    public partial class frmExpenses : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();

        public frmExpenses()
        {
            InitializeComponent();

            FillExpensesGrid();
            langu();


        }


        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.Text = st.isEnglish() ? "Add Expenses" : "اضافة مصروف";
            this.labelControl2.Text = st.isEnglish() ? "Name" : "الاسم";
            this.gridColumn2.Caption = st.isEnglish() ? "Expenses Name" : "اسم المصروف";
            this.colDelete1.Caption = st.isEnglish() ? "Delete" : "حذف";
            gvUnitCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
           
           
        }


        public void FillExpensesGrid()
        {
            var branchCode = st.GetBranch_Code();
            var result = (from a in context.Expenses where a.IsDeleted == 0 && a.Branch_Code == branchCode select a).ToList();
            gcExpenses.DataSource = result;
        }
        
      

        void AddExpenses()
        {
            if (string.IsNullOrWhiteSpace(txtExpenses.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please write the name of the expense !" : "!برجاء كتابة اسم المصروف ", MessageBoxButtons.OK);
                return;
            }

            bool TestUpdate = context.Expenses.Any(Unit => Unit.ExpensesName == txtExpenses.Text && Unit.IsDeleted == 0);
            if (TestUpdate)
            {


                MaterialMessageBox.Show(st.isEnglish() ? "Expenses has been registered" : "!تم نسجبلة من قبل", MessageBoxButtons.OK);

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
                    Branch_Code = 0,
                    
                   
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

                MaterialMessageBox.Show(st.isEnglish() ? "No data to delete!" : "!لا يوجد بيانات للحذف", MessageBoxButtons.OK);
                return;
            }

            Expens xx = gvUnitCard.GetFocusedRow() as Expens;
            Int64 ExpensCode = Convert.ToInt64(xx.ExpensesCode);
            bool CheckRelation = context.ExpensesTransactions.Where(x => x.IsDeleted == 0  ).Any(x => x.ExpensesCode == ExpensCode);
            bool CheckBackOfficeRelation = context.Expenses_Back_Office.Where(x => x.IsDeleted == 0 && x.Event_Code != 3).Any(x => x.ExpensesCode == ExpensCode);

            if (CheckRelation)
            {

                MaterialMessageBox.Show(st.isEnglish() ? "This expense cannot be deleted due to operations on it":"لا يمكن حذف هذا المصروف لوجود عمليات عليه", MessageBoxButtons.OK);
                return;

            }
            using (POSEntity Context = new POSEntity())
            {
                Expens deptDelete = Context.Expenses.Where(x=>x.ExpensesCode== ExpensCode).FirstOrDefault();
                Context.Expenses.Remove(deptDelete);
                Context.SaveChanges();
                FillExpensesGrid();
            }
        }

        
    }
}