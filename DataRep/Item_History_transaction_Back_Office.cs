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
    
    public partial class Item_History_transaction_Back_Office
    {
        public long Id_Back_Office { get; set; }
        public long SaleMasterCode { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public long Item_History_Id { get; set; }
        public int IsDeleted { get; set; }
        public double Trans_In { get; set; }
        public double Trans_Out { get; set; }
        public System.DateTime OrderDate { get; set; }
        public long Item_Id { get; set; }
        public long shiftCode { get; set; }
        public long Branch_Id { get; set; }
        public long Warhouse_Code { get; set; }
        public long Warhouse_Transfer_Code { get; set; }
        public bool is_Back_Office_Updated { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}