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
    
    public partial class ProjectMangerInfo_Back_Office
    {
        public long Id { get; set; }
        public long MangerCode_Back_Office { get; set; }
        public string MangerName { get; set; }
        public long ProjectId { get; set; }
        public bool IsActive { get; set; }
        public string MangerMobile { get; set; }
        public bool isUploaded { get; set; }
        public long Back_Office_Master_Code { get; set; }
        public long Back_Office_Detail_Code { get; set; }
    }
}