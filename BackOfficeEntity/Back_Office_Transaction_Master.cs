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
    
    public partial class Back_Office_Transaction_Master
    {
        public long Id { get; set; }
        public long Master_Code { get; set; }
        public long Created_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public int IsDeleted { get; set; }
        public long To_Branch_Code { get; set; }
        public bool Brach_Take_Update { get; set; }
    }
}
