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
    
    public partial class SaleMaster
    {
        public long Id { get; set; }
        public long UserCode { get; set; }
        public long Operation_Type_Id { get; set; }
        public long SaleMasterCode { get; set; }
        public double QtyTotal { get; set; }
        public double TotalBeforDiscount { get; set; }
        public double Discount { get; set; }
        public double FinalTotal { get; set; }
        public System.DateTime EntryDate { get; set; }
        public Nullable<System.DateTime> LastDateModif { get; set; }
        public int IsDeleted { get; set; }
        public long ShiftCode { get; set; }
        public long Payment_Type { get; set; }
        public long UserIdTakeOrder { get; set; }
        public long Customer_Code { get; set; }
        public bool isUploaded { get; set; }
        public long Branch_Id { get; set; }
        public double Cash { get; set; }
        public double Visa { get; set; }
        public bool is_Back_Office_Updated { get; set; }
    }
}
