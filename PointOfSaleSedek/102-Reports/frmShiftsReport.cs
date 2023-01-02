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

namespace PointOfSaleSedek._101_Adds._114_AddExpenses
{
    public partial class frmShiftsReport : DevExpress.XtraEditors.XtraForm
    {
        POSEntity Context = new POSEntity();
        readonly Static st = new Static();
        public frmShiftsReport()
        {
            InitializeComponent();
            langu();
        }



        void langu()
        {

            //this.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            //this.RightToLeftLayout = st.isEnglish() ? true : false;
            this.Text = st.isEnglish() ? "Canceled Bills" : "الفواتير الملغاه";


            labelControl5.Text = st.isEnglish() ? "From Date" : "من تاريخ";
            labelControl1.Text = st.isEnglish() ? "To Date" : "الي تاريخ";
            labelControl2.Text = st.isEnglish() ? "UserName" : "اسم المستخدم";

            btnShow.Text = st.isEnglish() ? "View" : "عرض";

            gridColumn7.Caption = st.isEnglish() ? "Shift No" : "رقم الوردية";
            gridColumn3.Caption = st.isEnglish() ? "UserName" : "اسم المستخدم";
            gridColumn5.Caption = st.isEnglish() ? "Shift Start Date" : "تاريخ بداية الوردية";
            gridColumn10.Caption = st.isEnglish() ? "Shift Start Balance" : "رصيد بداية الوردية";
            gridColumn1.Caption = st.isEnglish() ? "Total sales" : "اجمالي المبيعات";
            gridColumn2.Caption = st.isEnglish() ? "Total Expenses" : "اجمالي المصروفات";
            gridColumn8.Caption = st.isEnglish() ? "Balance End Shift" : "رصيد نهاية الوردية";
            gridColumn6.Caption = st.isEnglish() ? "Increase or Decrease" : "الزيادة او العجز";
            gridColumn9.Caption = st.isEnglish() ? "Shift Close Date" : "تاريخ غلق الوردية";
            gridColumn4.Caption = st.isEnglish() ? "Name" : "الاسم";

            gvItemCard.GroupPanelText = st.isEnglish() ? "Drag the field here to collect" : "اسحب الحقل هنا للتجميع";
             contextMenuStrip1.Items[0].Text = st.isEnglish() ? "Print" : "طباعة";
        }


        private void FrmCancelExpenses_Load(object sender, EventArgs e)
        {
            string DatatimeNow = Convert.ToString(DateTime.Now.ToString("MM/dd/yyyy"));
            dtFrom.Text = DatatimeNow;
            dtTo.Text = DatatimeNow;
            FillSlkShiftsUsers();


        }

        



        public void FillSlkShiftsUsers()
        {

            var result = Context.Users.Where(Shift => Shift.IsDeleted == 0).ToList();
            slkShiftsUsers.Properties.DataSource = result;
            slkShiftsUsers.Properties.ValueMember = "Emp_Code";
            slkShiftsUsers.Properties.DisplayMember = "UserName";

        }

        //void FillGride()
        //{
        //    var datefrom = Convert.ToDateTime(Convert.ToDateTime(dtFrom.EditValue).AddDays(1));
        //    var Master = Context.Shift_View.Where(a => a.Shift_Start_Date >= dtFrom.DateTime && a.Shift_Start_Date <= datefrom && a.IsDeleted == 0).ToList();
        //    if (Master.Count != 0)

        //    {
        //        gridControl1.DataSource = null;
        //        gridControl1.DataSource = Master;
        //        gridControl1.RefreshDataSource();
        //        gridControl1.Enabled = true;

        //    }
        //    else
        //    {

        //        gridControl1.DataSource = null;

        //        gridControl1.RefreshDataSource(); ;

        //        gridControl1.Enabled = false;

        //    }
        //}

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MaterialMessageBox.Show(st.isEnglish() ? "Are you Sure To Delete?": "هل انت متأكد من الحذف؟", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                ////ExpensesView xx = gridControl1.GetFocusedRow() as ExpensesView;
                //ExpensesTransaction _Expens = new ExpensesTransaction();
                //_Expens = context.ExpensesTransactions.SingleOrDefault(item => item.Id == xx.Id );
                //_Expens.IsDeleted = 1;
                //context.SaveChanges();
                //FillGride();
            }
        }

        private void btnShow_Click_1(object sender, EventArgs e)
        {

            var dateTo = Convert.ToDateTime(Convert.ToDateTime(dtTo.EditValue).AddDays(1));
            List<Shift_View> Master = new List<Shift_View>();


            // if Search By User
            if (!String.IsNullOrWhiteSpace(slkShiftsUsers.Text))
            {

                Int64 UserCode = Convert.ToInt64(slkShiftsUsers.EditValue);
                  Master = Context.Shift_View.Where(a => a.Shift_Start_Date >= dtFrom.DateTime && a.Shift_Start_Date <= dateTo && a.User_Id == UserCode && a.IsDeleted == 0 && a.Shift_Flag == false).ToList();

                if (Master.Count == 0)

                {
                    gcItemCard.DataSource = null;
                    gcItemCard.DataSource = Master;
                    gcItemCard.RefreshDataSource();
                    gcItemCard.Enabled = false;

                }
                else
                {
                   

                    gcItemCard.DataSource = null;
                    gcItemCard.DataSource = Master;
                    gcItemCard.RefreshDataSource();
                    gcItemCard.Enabled = true;

                }

            }
            else {

                
                Master = Context.Shift_View.Where(a => a.Shift_Start_Date >= dtFrom.DateTime && a.Shift_Start_Date <= dateTo && a.IsDeleted == 0 && a.Shift_Flag == false).ToList();

                if (Master.Count == 0)

                {
                    gcItemCard.DataSource = null;
                    gcItemCard.DataSource = Master;
                    gcItemCard.RefreshDataSource();
                    gcItemCard.Enabled = false;

                }
                else
                {
                   

                    gcItemCard.DataSource = null;
                    gcItemCard.DataSource = Master;
                    gcItemCard.RefreshDataSource();
                    gcItemCard.Enabled = true;

                }

            }



            // if Search By Date Only

        }

        private void حذفToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            Shift_View ShiftView = gvItemCard.GetFocusedRow() as Shift_View;


            if (ShiftView != null) {
                List<EndShiftReport> listendShiftReport = new List<EndShiftReport>();

                EndShiftReport endShiftReport = new EndShiftReport()
                {
                    ShiftCode = ShiftView.Shift_Code.ToString(),
                    ProfitOrExpenses = ShiftView.Shift_Increase_disability.ToString(),
                    ShiftEndBalance = ShiftView.Shift_End_Amount.ToString(),
                    ShiftEndDate = ShiftView.Shift_End_Date.ToString(),

                    ShiftEndNote = ShiftView.Shift_End_Notes.ToString(),
                    ShiftExpenses = ShiftView.Expenses.ToString(),
                    ShiftSales = ShiftView.TotalSale.ToString(),
                    ShiftStartBalance = ShiftView.Shift_Start_Amount.ToString(),
                    ShiftStartDate = ShiftView.Shift_Start_Date.ToString(),
                    ShiftStartNote = ShiftView.Shift_Start_Notes.ToString(),
                    UserName = ShiftView.UserName.ToString()

                };

                listendShiftReport.Add(endShiftReport);


                Report rpt = new Report();
                rpt.Load(@"Reports\endShift.frx");
                rpt.RegisterData(listendShiftReport, "Lines");
                rpt.PrintSettings.ShowDialog = false;
                rpt.Show();
                //rpt.Print();
            }


        }
    }
}