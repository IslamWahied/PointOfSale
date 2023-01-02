 
using PointOfSaleSedek._102_MaterialSkin;
using System;
 
using System.Data;
 
using System.Linq;
 
using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;
using DataRep;
using FastReport;
using PointOfSaleSedek.Model;
using System.Collections.Generic;

namespace PointOfSaleSedek._101_Adds.CasherShift
{
    public partial class frmShiftEnd : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity Context = new POSEntity();
        readonly Static st = new Static();
        public frmShiftEnd()
        {
            InitializeComponent();
            langu();
            FillSlkShiftsOPen();
            string DatatimeNow = Convert.ToString(DateTime.Now);
            dtEnd.Text = DatatimeNow;
        }


        void langu()
        {
            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;

            this.Text = st.isEnglish() ? "Close Shift" : "اغلاق الوردية";

            labelControl1.Text = st.isEnglish() ? "Open Shifts" : "الورديات المفتوحة";

            labelControl5.Text = st.isEnglish() ? "Shift Start Date" : "تاريخ بداية الوردية";


            labelControl2.Text = st.isEnglish() ? "Shift End Date" : "تاريخ نهاية الوردية";


            labelControl7.Text = st.isEnglish() ? "Shift Start Balance" : "رصيد بداية الوردية";


            labelControl9.Text = st.isEnglish() ? "Total sales" : "اجمالي المبيعات";
            labelControl10.Text = st.isEnglish() ? "Total Expenses" : "اجمالي المصروفات";


            labelControl8.Text = st.isEnglish() ? "Balance Of End Shift" : "رصيد نهاية الوردية";



            
            labelControl3.Text = st.isEnglish() ? "Decrease or Increase" : "العجز او الزيادة";


            labelControl4.Text = st.isEnglish() ? "Shift Start Note" : "ملاحظات بداية الوردية";
            labelControl6.Text = st.isEnglish() ? "Shift End Note" : "ملاحظات نهاية الوردية";
            btnSave.Text = st.isEnglish() ? "Save" : "اضافة";
            btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
            gridColumn2.Caption  = st.isEnglish() ? "Name" : "الاسم";
        }
        private void labelControl2_Click(object sender, EventArgs e)
        {

        }
        public void FillSlkShiftsOPen()
        {
            
            var result = Context.Shift_View.Where(Shift => Shift.IsDeleted == 0  &&Shift.Shift_Flag==true && Shift.Shift_Code != 0).ToList();
            slkShiftsOpen.Properties.DataSource = result;
            slkShiftsOpen.Properties.ValueMember = "Shift_Code";
            slkShiftsOpen.Properties.DisplayMember = "UserName";

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void slkShiftsOpen_Properties_EditValueChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(slkShiftsOpen.Text))
            {
                string DatatimeNow = Convert.ToString(DateTime.Now);
                dtEnd.Text = DatatimeNow;
                txtAmountEnd.ResetText();
                
                txtAmountStart.ResetText();
                txtdateStart.ResetText();
                txtNoteEnd.ResetText();
                txtNoteStart.ResetText();
                txtShiftIncrseOrDibilty.ResetText();
                dtEnd.ReadOnly = true;
                txtAmountEnd.ReadOnly = true;
                txtNoteEnd.ReadOnly = true;
            }
            else
            {
                dtEnd.ReadOnly = false;
                
                txtAmountEnd.ReadOnly = false;
                txtNoteEnd.ReadOnly = false;
                Int64 ShiftCode = Convert.ToInt64(slkShiftsOpen.EditValue);
                var ShiftSelected = Context.Shift_View.Where(Shift => Shift.IsDeleted == 0 &&  Shift.Shift_Flag == true&&Shift.Shift_Code==ShiftCode).FirstOrDefault();
                txtAmountStart.Text = ShiftSelected.Shift_Start_Amount.ToString();
                txtNoteStart.Text = ShiftSelected.Shift_Start_Notes;
                txtdateStart.Text = Convert.ToString(ShiftSelected.Shift_Start_Date);

                try
                {

                    txtTotalSale.Text = Context.SaleMasterViews.Where(x => x.IsDeleted == 0 && x.Operation_Type_Id == 2 && x.Shift_Code == ShiftCode).Sum(x => x.FinalTotal).ToString();

                }
                catch
                {
                    txtTotalSale.Text = "0";


                }


                try
                {

                    txtExpenses.Text = Context.ExpensesTransactions.Where(x => x.IsDeleted == 0 && x.Shift_Code == ShiftCode).Sum(x => x.ExpensesQT).ToString();

                }
                catch
                {
                    txtExpenses.Text = "0";


                }


                try
                {

                    txtShiftIncrseOrDibilty.Text = Convert.ToString(Convert.ToDouble(txtAmountEnd.Text) + Convert.ToDouble(txtExpenses.Text) - (Convert.ToDouble(txtAmountStart.Text) + Convert.ToDouble(txtTotalSale.Text)));

                }
                catch
                {
                    txtShiftIncrseOrDibilty.Text = "0";


                }





            }

        }

        private void txtAmountEnd_EditValueChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtAmountStart.Text) && !string.IsNullOrWhiteSpace(txtAmountEnd.Text))
            {
                txtShiftIncrseOrDibilty.Text = Convert.ToString((Convert.ToDouble(txtAmountEnd.Text) + Convert.ToDouble(txtExpenses.Text)) - (Convert.ToDouble(txtAmountStart.Text)+ Convert.ToDouble(txtTotalSale.Text)));

            }
            else
            {

                
                try
                {
                    txtShiftIncrseOrDibilty.Text = Convert.ToString((Convert.ToDouble(0) + Convert.ToDouble(txtExpenses.Text)) - (Convert.ToDouble(txtAmountStart.Text) + Convert.ToDouble(txtTotalSale.Text)));
                    txtAmountEnd.Text = 0.ToString();
                }
                catch (Exception)
                {

                    txtShiftIncrseOrDibilty.Text = "0";
                }
                


            }
             
        }
        void Rest()
        {
            txtAmountStart.ResetText();
            txtdateStart.ResetText();
            txtNoteEnd.ResetText();
            txtNoteStart.ResetText();
            txtShiftIncrseOrDibilty.ResetText();
            dtEnd.ReadOnly = true;
            txtAmountEnd.ReadOnly = true;
            txtNoteEnd.ReadOnly = true;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(slkShiftsOpen.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please choose a Shift" :"برجاء اختيار وردية", MessageBoxButtons.OK);
                return;
            }


            if (dtEnd.EditValue == null || string.IsNullOrWhiteSpace(dtEnd.Text))
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please select a shift closing date" :"برجاء اختيار تاريخ اقفال الوردية", MessageBoxButtons.OK);
                return;
            }
            else
            {

              
            Int64 ShiftCode = Convert.ToInt64(slkShiftsOpen.EditValue);
            Shift _Shift;
            _Shift = Context.Shifts.SingleOrDefault(shft => shft.Shift_Code == ShiftCode && shft.Shift_Flag==true&&shft.IsDeleted==0);
                _Shift.Shift_Start_Amount = Convert.ToDouble(txtAmountStart.Text);
                _Shift.Shift_End_Amount = Convert.ToDouble(txtAmountEnd.Text);
                _Shift.Shift_Increase_disability = Convert.ToDouble(txtShiftIncrseOrDibilty.Text);
                _Shift.Shift_End_Date = Convert.ToDateTime(dtEnd.EditValue); /*HelperClass.HelperClass.EncryptPassword(TxtPassword.Text);*/
                _Shift.Shift_End_Notes = txtNoteEnd.Text; /*HelperClass.HelperClass.EncryptPassword(TxtPassword.Text);*/
                _Shift.TotalSale = Convert.ToDouble(txtTotalSale.Text??"0");
                _Shift.Expenses = Convert.ToDouble(txtExpenses.Text??"0");
                _Shift.Shift_Flag =false;
                _Shift.Last_Modified_Date = DateTime.Now;
                _Shift.Last_Modified_User = st.GetUser_Code();
                Context.SaveChanges();


                List<EndShiftReport> listendShiftReport = new List<EndShiftReport>();

                EndShiftReport endShiftReport = new EndShiftReport()
                {

                    ShiftCode = ShiftCode.ToString(),
                    ProfitOrExpenses = Convert.ToString((Convert.ToDouble(txtAmountEnd.Text) + Convert.ToDouble(txtExpenses.Text)) - (Convert.ToDouble(txtAmountStart.Text) + Convert.ToDouble(txtTotalSale.Text))),
                    ShiftEndBalance = txtAmountEnd.Text,
                    ShiftEndDate = dtEnd.EditValue.ToString(),
                    ShiftEndNote = txtNoteEnd.Text,
                    ShiftExpenses = txtExpenses.Text,
                    ShiftSales = txtTotalSale.Text,
                    ShiftStartBalance = txtAmountStart.Text,
                    ShiftStartDate = txtdateStart.Text,
                    ShiftStartNote = txtNoteStart.Text,
                    UserName = slkShiftsOpen.Text

                };

                listendShiftReport.Add(endShiftReport);


                Report rpt = new Report();
                rpt.Load(@"Reports\endShift.frx");
                rpt.RegisterData(listendShiftReport, "Lines");
                rpt.PrintSettings.ShowDialog = false;
                rpt.Print();
              //  rpt.Print();


                Rest();
                MaterialMessageBox.Show(st.isEnglish() ? "Shift Closed" :"تم اقفال الوردية ", MessageBoxButtons.OK);
                this.Close();

            }
        }

        
    }
}