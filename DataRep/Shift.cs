//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRep
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shift
    {
        public long Id { get; set; }
        public long Shift_Code { get; set; }
        public long User_Id { get; set; }
        public System.DateTime Shift_Start_Date { get; set; }
        public Nullable<System.DateTime> Shift_End_Date { get; set; }
        public double Shift_Start_Amount { get; set; }
        public Nullable<double> Shift_End_Amount { get; set; }
        public Nullable<double> Shift_Increase_disability { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public Nullable<long> Last_Modified_User { get; set; }
        public int IsDeleted { get; set; }
        public bool Shift_Flag { get; set; }
        public string Shift_Start_Notes { get; set; }
        public string Shift_End_Notes { get; set; }
    }
}
