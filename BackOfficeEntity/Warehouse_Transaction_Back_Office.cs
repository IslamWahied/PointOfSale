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
    
    public partial class Warehouse_Transaction_Back_Office
    {
        public long Id_Back_Office { get; set; }
        public long From_Warehouse_Code { get; set; }
        public long To_Warehouse_Code { get; set; }
        public int IsDeleted { get; set; }
        public long Created_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public bool is_Back_Office_Updated { get; set; }
        public long Branch_Code { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}
