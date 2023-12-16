using DevExpress.XtraEditors;
using DataRep;
using FastReport;
using PointOfSaleSedek._102_MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSaleSedek.HelperClass;

namespace PointOfSaleSedek._101_Adds._113_BarCode
{
    public partial class frmPrinterCopiesNumber : DevExpress.XtraEditors.XtraForm
    {
        readonly POSEntity context = new POSEntity();
        readonly Static st = new Static();
        public frmPrinterCopiesNumber()
        {
            InitializeComponent();
            FilltxtNumberOfPaper(); 
            langu();
        }



        void langu()
        {

            this.RightToLeft = st.isEnglish() ? RightToLeft.No : RightToLeft.Yes;
            this.tableLayoutPanel1.RightToLeft = st.isEnglish() ? RightToLeft.Yes : RightToLeft.No;
            this.Text = st.isEnglish() ? " Printer Copies Number" : "عدد مرات الطباعة";
            this.label1.Text = st.isEnglish() ? "New Copies Number" : "العدد الجديد";
            this.label2.Text = st.isEnglish() ? "Old Number Of Prints" : "عدد مرات القديم";
           btnPrint.Text = st.isEnglish() ? "Save" : "حفظ";
           btnCancel.Text = st.isEnglish() ? "Close" : "اغلاق";
          
        }


        public void FilltxtNumberOfPaper()
            {
       
                var CopyNumber = context.PrintCopyNumbers.ToList().FirstOrDefault().PrintCopyNumber1;
            txtNumberOfPaper.Text = CopyNumber.ToString();

            }

      
 
 
        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textEdit1.Text) || textEdit1.Text == "0")
            {

                MaterialMessageBox.Show(st.isEnglish() ? "Please enter the number of copies" : "برجاء ادخال عدد النسخ", MessageBoxButtons.OK);
                return;


            }
            else {


              var _printCopyNumber = context.PrintCopyNumbers.SingleOrDefault();

                _printCopyNumber.PrintCopyNumber1 = int.Parse(textEdit1.Text);


                context.SaveChanges();
                st.SetPrintCopyNumber(int.Parse(textEdit1.Text));

                MaterialMessageBox.Show(st.isEnglish() ? "Saved successfully" : "تم الحفظ", MessageBoxButtons.OK);
                this.Close();
                 
            }

        }
    }
}