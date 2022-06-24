namespace impinj_assesment.Models
{
    public class SalesRecordResponse
    {
        public DateData dateData { get; set; }
        public string mostCommonRegion { get; set; }
        public double medianUnitCost { get; set; }
        public string sumTotalRevenue { get; set; }
    }
}