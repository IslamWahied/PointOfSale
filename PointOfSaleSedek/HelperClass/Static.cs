using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleSedek.HelperClass
{
   public class Static
    {
        public static Int64 _User_Code;
        public static Int64 _Project_Code;
        public static string _CPU;
        public void UserName(Int64 User_Code)
        {
            _User_Code = User_Code;


        }
        public Int64 User_Code()
        {
            return _User_Code;


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
