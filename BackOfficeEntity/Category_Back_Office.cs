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
    
    public partial class Category_Back_Office
    {
        public long Id { get; set; }
        public long CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public int IsDeleted { get; set; }
        public long Branch_Code { get; set; }
        public int Event_Code { get; set; }
        public bool IsWindow_Transfer { get; set; }
        public bool Is_Brach_Take_Update { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}
