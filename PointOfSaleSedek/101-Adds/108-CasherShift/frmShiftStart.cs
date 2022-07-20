using DevExpress.XtraEditors;
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
using DataRep;

namespace PointOfSaleSedek._101_Adds.CasherShift
{
    public partial class frmShiftStart : DevExpress.XtraEditors.XtraForm
    {
        SaleEntities Context = new SaleEntities();
        public frmShiftStart()
        {
            InitializeComponent();
            FillSlkUser();
              string DatatimeNow = Convert.ToString(DateTime.Now);
       
            dtFrom.Text = DatatimeNow;
        }
        public void FillSlkUser()
        {

            List<User_View> listUserView = new List<User_View>() ;


            var resultList = Context.User_View.Where(user => user.IsDeleted == 0 && user.IsDeletedEmployee == 0).ToList();

           foreach (var item in resultList)
            {
                bool ressult = Context.Shift_View.Any(Shift => Shift.IsDeleted == 0 && Shift.Emp_Code == item.Employee_Code && Shift.Shift_Flag == true);
                if (!ressult) {

                    listUserView.Add(item);
                }

            }


         //   var result = Context.Shift_View.Where(Shift => Shift.IsDeleted == 0  && Shift.Shift_Flag == false).ToList();
          
            slkUser.Properties.DataSource = listUserView;
            slkUser.Properties.ValueMember = "Employee_Code";
            slkUser.Properties.DisplayMember = "UserName";

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(slkUser.Text))
            {

                MaterialMessageBox.Show("برجاء اختيار المستخدم ", MessageBoxButtons.OK);
                return;

            }
           else if (string.IsNullOrWhiteSpace(dtFrom.Text))
            {

                MaterialMessageBox.Show("برجاء اختيار تاريخ بداية الوردية ", MessageBoxButtons.OK);
                return;

            }
           else if (string.IsNullOrWhiteSpace(txtStartAmount.Text))
            {

                MaterialMessageBox.Show("برجاء ادخال رصيد بداية الوردية ", MessageBoxButtons.OK);
                return;

            }

            var UserId = Convert.ToInt64(slkUser.EditValue);
            var Check = Context.Shifts.Any(x => x.Shift_Flag == true && x.User_Id == UserId && x.IsDeleted == 0);
             Int64 NewShiftCode = Convert.ToInt64(Context.Shifts.Max(u => (Int64?)u.Shift_Code))+1;
            
            if (!Check)
            {
                Shift _shift = new Shift()
                {
                    User_Id = UserId,
                    Shift_Code = NewShiftCode,
                    Shift_Start_Amount = Convert.ToDouble(txtStartAmount.Text),
                    Shift_Start_Notes = txtNote.Text,
                    Shift_Flag = true,
                   
                    Shift_Start_Date = Convert.ToDateTime(dtFrom.EditValue),
                    Last_Modified_User = UserId,
                };
                Context.Shifts.Add(_shift);
                Context.SaveChanges();
               
                txtNote.ResetText();
                txtStartAmount.ResetText();
                slkUser.ResetText();
                string DatatimeNow = Convert.ToString(DateTime.Now);

                dtFrom.Text = DatatimeNow;
                MaterialMessageBox.Show("تم الحفظ", MessageBoxButtons.OK);
                this.Close();

            }
            else
                    {

                MaterialMessageBox.Show("يوجد وردية مفعلة لهذا المستخدم ", MessageBoxButtons.OK);
                return;

               
            }
        }
    }
}