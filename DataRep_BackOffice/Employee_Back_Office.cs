//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRep_BackOffice
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employee_Back_Office
    {
        public long Id { get; set; }
        public long Employee_Code_Back_Office { get; set; }
        public string Employee_Name { get; set; }
        public string Employee_National_Id { get; set; }
        public string Employee_Address { get; set; }
        public string Employee_Mobile_1 { get; set; }
        public string Employee_Mobile_2 { get; set; }
        public string Employee_Email { get; set; }
        public long Jop_Code { get; set; }
        public long Branch_ID { get; set; }
        public string img_Url { get; set; }
        public Nullable<System.DateTime> Employee_Start_Jop { get; set; }
        public Nullable<System.DateTime> Employee_End_Jop { get; set; }
        public string Employee_Notes { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public long Last_Modified_User { get; set; }
        public int SexTypeCode { get; set; }
        public bool isUploaded { get; set; }
    }
}