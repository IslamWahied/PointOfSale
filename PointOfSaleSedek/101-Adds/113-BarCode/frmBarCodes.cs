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


namespace PointOfSaleSedek._101_Adds._113_BarCode
{
    public partial class frmBarCodes : DevExpress.XtraEditors.XtraForm
    {
        readonly SaleEntities context = new SaleEntities();
        public frmBarCodes()
        {
            InitializeComponent();
            FillSlkItems();
        }
            public void FillSlkItems()
            {
                DataTable dt = new DataTable();
                var result = context.ItemCardViews.Where(user => user.IsDeleted == 0).ToList();
                slkItem.Properties.DataSource = result;
                slkItem.Properties.ValueMember = "ParCode";
                slkItem.Properties.DisplayMember = "Name";

            }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Zen.Barcode.Code128BarcodeDraw barCode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            //var x = slkItem.EditValue.ToString();
            //pictureBox1.Image = barCode.Draw(slkItem.EditValue.ToString(), 50);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(slkItem.Text))
            {

                MaterialMessageBox.Show("برجاء اختيار منتج", MessageBoxButtons.OK);
                return;


            }

            else if (string.IsNullOrWhiteSpace(txtNumberOfPaper.Text)|| txtNumberOfPaper.Text =="0")
            {

                MaterialMessageBox.Show("برجاء ادخال عدد النسخ", MessageBoxButtons.OK);
                return;


            }



            else {


                var NumberOfPage = Convert.ToInt64(txtNumberOfPaper.Text);
                if (NumberOfPage > 0)
                {

                    btnPrint.Enabled = false;
                    btnPrint.Text =  "جاري الطباعة"; 
                   
                for (int i = 0; i < NumberOfPage; i++)
                {


                       
            Report rpt = new Report();
            var result = context.ItemCardViews.Where(user => user.IsDeleted == 0 && user.ParCode == slkItem.EditValue.ToString()).ToList();
            rpt.Load(@"Reports\Untitled.frx");
            rpt.RegisterData(result, "Header");
                        
                        rpt.PrintSettings.ShowDialog = false;
                         rpt.Print();
                    }
                    btnPrint.Enabled = true;
                    btnPrint.Text = "الطباعة";
                    txtNumberOfPaper.Text = "1";
                   
                }



            }



            //rpt.Design();



            //PrintDialog pd = new PrintDialog();
            //PrintDocument doc = new PrintDocument();
            //doc.PrintPage += Doc_PrintPage;
            //pd.Document = doc;
            //if (pd.ShowDialog() == DialogResult.OK)
            //{
            //    doc.Print();

            //}
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            //Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //pictureBox1.DrawToBitmap(bm, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            //e.Graphics.DrawImage(bm, 0, 0);

            //bm.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmBarCodes_Load(object sender, EventArgs e)
        {

        }

        private void slkItem_EditValueChanged(object sender, EventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw barCode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
          //  var x = slkItem.EditValue.ToString();
          //  pictureBox1.Image = barCode.Draw(slkItem.EditValue.ToString(), 50);
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}