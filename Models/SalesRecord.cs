namespace impinj_assesment.Models
{
    public class SalesRecord
    {
        public string? Region { get; set; }
        public string? Country { get; set; }
        public string? ItemType { get; set; }
        public string? SalesChannel { get; set; }

        public char? OrderPriority { get; set; }
        public DateTime? OrderDate { get; set; }
        public long? OrderId { get; set; }
        public DateTime? ShipDate { get; set; }
        public int? UnitsSold { get; set; }
        public double? UnitPrice { get; set; }
        public double? UnitCost { get; set; }
        public double? TotalRevenue { get; set; }
        public double? TotalCost { get; set; }
        public double? TotalProfit { get; set; }
    }
}