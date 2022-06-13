using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using PointOfSaleSedek._101_Adds._103_Authentication;
using PointOfSaleSedek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PointOfSaleSedekSedek
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmLogin());
        }
    }
}
