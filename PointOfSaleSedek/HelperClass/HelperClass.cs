using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;


namespace PointOfSaleSedek.HelperClass
{
    public class HelperClass
    {
        public static byte[] SaveImage(PictureEdit myPic)
        {
            MemoryStream ms = new MemoryStream();
            myPic.Image.Save(ms, myPic.Image.RawFormat);
            byte[] image = ms.ToArray();

            return image;
        }

        public static byte[] RetrieveImage(byte[] arrImage,PictureEdit myPic)
        {
            
            byte[] myImage = arrImage;
          
            MemoryStream ms = new MemoryStream(myImage);
            myPic.Image = Image.FromStream(ms);

            return myImage;
        }

        public static void ClearValues(TableLayoutPanel tp)
        {
            tp.SuspendLayout();
            foreach (Control item in tp.Controls)
            {
                if (item is TextEdit) { item.Text = ""; }
                if (item is ComboBoxEdit) { item.Text = ""; }
                if (item is DateEdit) { item.Text = ""; }
                if (item is MemoEdit) { item.Text = ""; }
                //if (item is GridLookUpEdit) { item.Text = ""; }
            }
            tp.ResumeLayout();
        }
        
        public static void EnableControls(TableLayoutPanel tp)
        {
            tp.SuspendLayout();
            foreach (Control item in tp.Controls)
            {
                if (item is TextEdit) { item.Enabled = true; }
                if (item is ComboBoxEdit) { item.Enabled = true; }
                if (item is DateEdit) { item.Enabled = true; }
                //if (item is GridLookUpEdit) { item.Enabled = true; }
                if (item is SimpleButton) { item.Enabled = true; }
                if (item is CheckEdit) { item.Enabled = true; }

            }
            tp.ResumeLayout();
        }

        public static void DisableControls(TableLayoutPanel tp)
        {
            tp.SuspendLayout();
            foreach (Control item in tp.Controls)
            {
                if (item is TextEdit) { item.Enabled = false; }
                if (item is ComboBoxEdit) { item.Enabled = false; }
                if (item is DateEdit) { item.Enabled = false; }
                //if (item is GridLookUpEdit) { item.Enabled = false; }
                if (item is SimpleButton) { item.Enabled = false; }
                if (item is CheckEdit) { item.Enabled = false; }
            }
            tp.ResumeLayout();
        }

        public static void StartOperation(Form frm)
        {
            frm.Cursor = Cursors.WaitCursor;
            frm.Enabled = false;
        }

        public static void EndOperation(Form frm)
        {
            frm.Cursor = Cursors.Default;
            frm.Enabled = true;
        }

        public static string EncryptPassword(string s)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(s));
            byte[] Result = md5.Hash;

            StringBuilder str = new StringBuilder();

            for (int i = 1; i < Result.Length; i++)
            {
                str.Append(Result[i].ToString("x4"));
            }

            return str.ToString();
        }
    }
}
