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
    
    public partial class ExpensesView
    {
        public string ExpensesName { get; set; }
        public string UserName { get; set; }
        public int IsDeleted { get; set; }
        public Nullable<System.DateTime> Last_Modified_Date { get; set; }
        public Nullable<long> Last_Modified_By { get; set; }
        public string ExpensesNotes { get; set; }
        public double ExpensesQT { get; set; }
        public long ExpensesCode { get; set; }
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public long Shift_Code { get; set; }
        public string Employee_Name { get; set; }
        public Nullable<long> Emp_Code { get; set; }
        public bool isUploaded { get; set; }
        public string Warehouse_Name { get; set; }
        public long Warehouse_Code { get; set; }
        public long Branches_Code { get; set; }
        public string Branches_Name { get; set; }
    }
}
