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
    
    public partial class Customer_Info_Back_Office
    {
        public long Id { get; set; }
        public long Customer_Code_Back_Office { get; set; }
        public string Customer_Name { get; set; }
        public string Customer_Phone { get; set; }
        public string Customer_Address { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public Nullable<long> Last_Modified_User { get; set; }
        public int IsDeleted { get; set; }
        public string Customer_Notes { get; set; }
        public string National_Id { get; set; }
        public Nullable<int> SexTypeCode { get; set; }
        public string Jop_Descrption { get; set; }
        public string Email { get; set; }
        public string CustomerFavourit { get; set; }
        public long Branch_Code { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}
