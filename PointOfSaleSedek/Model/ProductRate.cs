using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleSedek.Model
{
    public class ProductRate
    {

        public String Category { get; set; }
        public Int64 ProductCode { get; set; }
        public String BranchName { get; set; }
        public String ProductName { get; set; }
        public String ProductEnName { get; set; }
        public String ProductUnite { get; set; }

        public double ProductQtySale { get; set; }
        public double CategoryQtyItemSealsSale { get; set; }
        public double CategoryPresdentSale { get; set; }

        public double Totla { get; set; }
        public double ProductPresdentSale { get; set; }

    }
}
