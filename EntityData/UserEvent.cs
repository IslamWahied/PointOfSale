//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityData
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserEvent
    {
        public long Id { get; set; }
        public long EventCode { get; set; }
        public Nullable<System.DateTime> EventDate { get; set; }
        public string EventName { get; set; }
        public Nullable<long> SaleMasterCode { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<double> FinalTotal { get; set; }
        public long UserCode { get; set; }
        public int Operation_Type_Id { get; set; }
    }
}
