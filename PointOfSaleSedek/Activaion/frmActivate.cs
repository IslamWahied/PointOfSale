

using PointOfSaleSedek._Activaion._102_MaterialSkin;
 
 
using DataRep;
using DevExpress.Utils.Extensions;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraRichEdit.Import.OpenXml;
//using PointOfSaleSedek._101_Adds._103_Authentication;
using PointOfSaleSedek._102_MaterialSkin;
using PointOfSaleSedek.HelperClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PointOfSaleSedek._0_Authentication;

namespace PointOfSaleSedek._Activaion
{
    public partial class frmActivate : MaterialSkin.Controls.MaterialForm
    {
            public string CPU_Code { get; set; }
        readonly POSEntity Context = new POSEntity();
  
        Static st = new Static();
         


        public frmActivate()
        {

            InitializeComponent();
            var mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            var mbsList = mbs.Get();

            foreach (ManagementObject mo in mbsList)
            {
                var cpuid = mo["ProcessorID"].ToString();

            }


        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MaterialMessageBox.Show("تاكيد الخروج", MessageBoxButtons.YesNo) == DialogResult.OK)
            {
                this.Hide();
                FrmLogin frm = new FrmLogin();
                frm.ShowDialog();
            }
        }
        public void Wait(int time)
        {
            Thread thread = new Thread(delegate ()
            {
                System.Threading.Thread.Sleep(time);
            });
            thread.Start();
            while (thread.IsAlive)
                Application.DoEvents();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
        var mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            var mbsList = mbs.Get();

            foreach (ManagementObject mo in mbsList)
            {
                  CPU_Code = mo["ProcessorID"].ToString();
            }
            
            bool CheckPC = Context.AppActivations.Any(cpu => cpu.CPU_Code == CPU_Code && cpu.ActivaionState);
          
            if (CheckPC)
            {

                MaterialMessageBox.Show(st.isEnglish()? "This point has already been activated" : "تم تفعيل هذه النقطة سابقا", MessageBoxButtons.OK);
                return;
            }
            if (!CheckPC)
            {

                Int64 Breanch_Code = st.GetBranch_Code();

                AppActivation _CPU = Context.AppActivations.FirstOrDefault();
                _CPU.CPU_Code = CPU_Code;
                _CPU.ActivaionState = true;
                _CPU.Activation_Date = DateTime.Now.AddYears(1);
                _CPU.BranchId = Breanch_Code;
                _CPU.Login_Offline_Count = 0;
                Context.SaveChanges();
                label1.Visible = true;
                progressBarControl1.EditValue = 0;
                progressBarControl1.Properties.Maximum = 100;
                for (int i = 0; i <= 100; i += 10)
                {

                    if (i > 0)
                        progressBarControl1.EditValue = i;

                    progressBarControl1.Update();
                    Wait(1000);
                   
                }


                simpleButton1.Text = st.isEnglish()? "Point Activated" : "تم التفعيل";
                simpleButton1.ForeColor = Color.White;
                simpleButton1.Enabled = false;
                label1.Visible = false;

            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
