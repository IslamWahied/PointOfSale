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
    
    public partial class Tab_Info_Back_Office
    {
        public long Id { get; set; }
        public long Tab_Code_Back_Office { get; set; }
        public string Tab_Name { get; set; }
        public string Tab_Description { get; set; }
        public Nullable<System.DateTime> Created_Date { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public Nullable<long> Last_Modified_User { get; set; }
        public int IsDeleted { get; set; }
        public bool InActive { get; set; }
        public string Tab_Description_Eng { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}
