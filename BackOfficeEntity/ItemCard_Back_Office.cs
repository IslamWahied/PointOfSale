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
    
    public partial class ItemCard_Back_Office
    {
        public long Id { get; set; }
        public long ItemCode_Back_Office { get; set; }
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
        public string Name_En { get; set; }
        public bool is_Back_Office_Updated { get; set; }
        public long Branch_Code { get; set; }
        public bool IsWindow_Transfer { get; set; }
        public bool Is_Brach_Take_Update { get; set; }
        public int Event_Code { get; set; }
        public long ItemCode { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}
