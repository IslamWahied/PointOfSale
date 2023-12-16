

namespace PointOfSaleSedek.Model
{
    public class SaleDetailPrfumViewVm
    {
        public long OilItemCode { get; set; }
        public long GlassItemCode { get; set; }


        public string OilIName { get; set; }
        public string GlassIName { get; set; }


        public double OilQty { get; set; }
        public double GlassQty { get; set; }


        public double OilPrice { get; set; }
        public double GlassPrice { get; set; }


        public double Total { get; set; }



        public long LineSequence { get; set; }
    }
}
