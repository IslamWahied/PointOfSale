using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSaleSedek._101_Adds._113_BarCode
{
    public partial class frmBarCode : MaterialSkin.Controls.MaterialForm
    {
        public frmBarCode()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Zen.Barcode.Code128BarcodeDraw barCode =  Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;

            pictureBox1.Image = barCode.Draw(textBox1.Text,50);

            //Zen.Barcode.CodeQrBarcodeDraw  QRCode =  Zen.Barcode.BarcodeDrawFactory.CodeQr;

            //pictureBox1.Image = QRCode.Draw(textBox1.Text,25);



        }

        private void frmBarCode_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("IDAutomationHC39M", 12, FontStyle.Regular);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            doc.PrintPage += Doc_PrintPage;
            pd.Document = doc;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();

            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm,new Rectangle(0,0,pictureBox1.Width,pictureBox1.Height));
            e.Graphics.DrawImage(bm, 0, 0);

                bm.Dispose();
        }
    }
}
