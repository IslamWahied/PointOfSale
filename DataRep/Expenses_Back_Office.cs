//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataRep
{
    using System;
    using System.Collections.Generic;
    
    public partial class Expenses_Back_Office
    {
        public int Id_Back_Office { get; set; }
        public string ExpensesName { get; set; }
        public int IsDeleted { get; set; }
        public long ExpensesCode { get; set; }
        public long Branch_Code { get; set; }
        public int Event_Code { get; set; }
        public bool IsWindow_Transfer { get; set; }
        public bool Is_Brach_Take_Update { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}
