using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleSedek.Model
{
    public class SaleDetailViewVm
    {
        public long ItemCode { get; set; }
        public Nullable<long> CategoryCode { get; set; }
        public string ParCode { get; set; }
        public string Name { get; set; }
        public double Qty { get; set; }
        public double Total { get; set; }
        public System.DateTime EntryDate { get; set; }
        public double Price { get; set; }
        public int IsDeleted { get; set; }
        public string UnitName { get; set; }
        public Nullable<long> UnitCode { get; set; }
        public Nullable<long> SaleMasterCode { get; set; }
        public long Operation_Type_Id { get; set; }
        public double PriceBuy { get; set; }
        public double OrederTotal { get; set; }
        public double Discount { get; set; }
        public string CategoryName { get; set; }
        public Nullable<bool> AddItem { get; set; }
        public double Item_Count_InStoreg { get; set; }
        public double FinalTotal { get; set; }
        public string UserName { get; set; }
        public long Emp_Code { get; set; }
        public long Id { get; set; }
        public long shiftCode { get; set; }
    }
}
