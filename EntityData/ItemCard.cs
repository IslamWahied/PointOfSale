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
    
    public partial class ItemCard
    {
        public long Id { get; set; }
        public long ItemCode { get; set; }
        public Nullable<long> CategoryCode { get; set; }
        public string ParCode { get; set; }
        public string Name { get; set; }
        public Nullable<long> UnitCode { get; set; }
        public Nullable<double> Item_Risk_limit { get; set; }
        public double PriceBuy { get; set; }
        public double Price { get; set; }
        public Nullable<bool> AddItem { get; set; }
        public int IsDeleted { get; set; }
        public double Item_Count_InStoreg { get; set; }
    }
}
