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
    
    public partial class SaleMasterView
    {
        public long UserCode { get; set; }
        public long Operation_Type_Id { get; set; }
        public long SaleMasterCode { get; set; }
        public double TotalBeforDiscount { get; set; }
        public double Discount { get; set; }
        public double QtyTotal { get; set; }
        public Nullable<System.DateTime> LastDateModif { get; set; }
        public System.DateTime EntryDate { get; set; }
        public double FinalTotal { get; set; }
        public int IsDeleted { get; set; }
        public string UserName { get; set; }
        public long Emp_Code { get; set; }
        public long Shift_Code { get; set; }
        public string PaymentType { get; set; }
        public long Payment_Type { get; set; }
        public long UserIdTakeOrder { get; set; }
        public long Customer_Code { get; set; }
        public bool isUploaded { get; set; }
        public long Branches_Code { get; set; }
        public string Branches_Name { get; set; }
        public double Cash { get; set; }
        public double Visa { get; set; }
    }
}
