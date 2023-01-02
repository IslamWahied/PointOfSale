using System;
using System.Windows.Forms;

namespace PointOfSaleSedek.HelperClass
{
   public class Static
    {

       public static String ProjectType = "Cafe";
  // public static String ProjectType = "Perfum";
      //  public static String ProjectType = "SuperMarket";

        public static Int64 _User_Code;
        public static Int64 _Branch_Code;
        public static String _Branch_Name;
        public static Int64 _Warehouse_Code;
        public static String _User_Name;
        public static Int64 _Project_Code;
        public static bool _isEnglish;
        public static string _CPU;

     

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }

        public void SetUserName(String userName)
        {
            _User_Name = userName;
        }



        public String GetUserName()
        {
            return _User_Name;
        }


        public void ChangeLangu(bool isEnglish)
        {
            _isEnglish = isEnglish;


        }


        public bool isEnglish()
        {
            return _isEnglish;


        }

        public Int64 GetUser_Code()
        {
            return _User_Code;


        }

        public void SetUser_Code(Int64 User_Code)
        {
            _User_Code = User_Code;


        }





        public Int64 GetBranch_Code()
        {
            return _Branch_Code;


        }

        public void SetBranch_Code(Int64 Branch_Code)
        {
            _Branch_Code = Branch_Code;


        }




        public String GetBranch_Name()
        {
            return _Branch_Name;


        }

        public void SetBranch_Name(String Branch_Name)
        {
            _Branch_Name = Branch_Name;


        }


        public Int64 Get_Warehouse_Code()
        {
            return _Warehouse_Code;


        }

        public void Set_Warehouse_Code(Int64 Warehouse_Code)
        {
            _Warehouse_Code = Warehouse_Code;


        }






        public String Project_Type()
        {
            return ProjectType;


        }



        public void SetCPU(string CPU)
        {
            _CPU = CPU;


        }
        public string Get_CPU()
        {
            return _CPU;


        }


        public void Set_Project_Code(Int64 Project_Code)
        {
            _Project_Code = Project_Code;


        }
        public Int64 Get_Project_Code()
        {
            return _Project_Code;


        }
    }
}
