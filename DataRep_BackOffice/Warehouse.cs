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
    
    public partial class Warehouse
    {
        public long id { get; set; }
        public long Warehouse_Code { get; set; }
        public string Warehouse_Name { get; set; }
        public int isDelete { get; set; }
        public bool is_Back_Office_Updated { get; set; }
        public long Branch_Code { get; set; }
    }
}