//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackOfficeEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item_Balance
    {
        public long ItemBalance_Code { get; set; }
        public long Item_Code { get; set; }
        public long Branch_Code { get; set; }
        public long WareHouse_Code { get; set; }
        public double Balance { get; set; }
        public System.DateTime Last_Update { get; set; }
        public int IsDeleted { get; set; }
        public double Price { get; set; }
    }
}
