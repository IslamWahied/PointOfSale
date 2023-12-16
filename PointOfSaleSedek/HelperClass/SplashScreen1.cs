using DataRep;
using DevExpress.XtraSplashScreen;
using PointOfSaleSedek._0_Authentication;

//using PointOfSaleSedek._101_Adds._103_Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PointOfSaleSedek.HelperClass
{
    public partial class SplashScreen1 : SplashScreen
    {
        readonly Static st = new Static();
        readonly POSEntity Context = new POSEntity();
        public SplashScreen1()
        {
            InitializeComponent();

         
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        public enum SplashScreenCommand
        {
        }

        private void SplashScreen1_Load(object sender, EventArgs e)
        {
            
        }

        private void SplashScreen1_Shown(object sender, EventArgs e)
        {
            var CheckUser = Context.Tab_Info.Any(Users => Users.InActive );
            st.wait(6000);
            this.Hide();
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }
    }
}