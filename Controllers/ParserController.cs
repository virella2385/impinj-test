using Microsoft.AspNetCore.Mvc;
using impinj_assesment.Models;
using System.Threading;
using System.Threading.Tasks;

namespace impinj_assesment.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ParserController : ControllerBase
{
    string path = Directory.GetCurrentDirectory();

    private readonly ILogger<ParserController> _logger;

    public ParserController(ILogger<ParserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public SalesRecordResponse Get()
    {
        List<SalesRecord> records = new List<SalesRecord>();
        string path = Directory.GetCurrentDirectory() + @"/Input/SalesRecord.csv";
        string line;
        string[] row = new string [13];
        StreamReader sr = new StreamReader(path);
        string headerLine = line = sr.ReadLine();

        while ((line = sr.ReadLine()) != null)
        {
            row = line.Split(',');
            SalesRecord record = convertObject(row);
            records.Add(record);
        }
        
        return new SalesRecordResponse
        {
            dateData = processDates(records),
            mostCommonRegion = getMostCommonRegion(records),
            medianUnitCost = getMedian(records.OrderBy(x => x.UnitCost).ToList()),
            sumTotalRevenue = Math.Round(records.Sum(x => x.TotalRevenue).Value, 2).ToString("N2")
        };
    }

    private SalesRecord convertObject(string[] record)
    {
        SalesRecord salesRecord = new SalesRecord();
        salesRecord.Region = record[0] != null ? record[0] : null;
        salesRecord.Country = record[1] != null ? record[1] : null;
        salesRecord.ItemType = record[2] != null ? record[2] : null;
        salesRecord.SalesChannel = record[3] != null ? record[3] : null;
        salesRecord.OrderPriority = record[4] != null ? char.Parse(record[4]) : null;
        salesRecord.OrderDate = record[5] != null ? DateTime.Parse(record[5]) : null;
        salesRecord.OrderId = record[6] != null ? long.Parse(record[6]) : null;
        salesRecord.ShipDate = record [7] != null ? DateTime.Parse(record[7]) : null;
        salesRecord.UnitsSold = record[8] != null ? int.Parse(record[8]) : null;
        salesRecord.UnitPrice = record[9] != null ? double.Parse(record[9]) : null;
        salesRecord.UnitCost = record[10] != null ? double.Parse(record[10]) : null;
        salesRecord.TotalRevenue = record[11] != null ? double.Parse(record[11]) : null;
        salesRecord.TotalCost = record[12] != null ? double.Parse(record[12]) : null;
        salesRecord.TotalProfit = record[13] != null ? double.Parse(record[13]) : null;
        return salesRecord;
        
    }

    public double getMedian(List<SalesRecord> records)
    {
        int midIdx = (records.Count - 1) / 2;
        return records.ElementAt(midIdx).UnitCost.Value;
    }

    public string getMostCommonRegion(List<SalesRecord> records)
    {
        return records.GroupBy(x => x.Region).OrderByDescending(g => g.Count()).First().ToList().FirstOrDefault().Region;
    }

    public DateData processDates(List<SalesRecord> records)
    {
        DateTime lastDate = records.OrderByDescending(x => x.OrderDate).First().OrderDate.Value;
        DateTime firstDate = records.OrderByDescending(x => x.OrderDate).Last().OrderDate.Value;
        return new DateData
        {
            EarliestDate = firstDate,
            LatestDate = lastDate,
            DaysBetween = (lastDate - firstDate).TotalDays
        };
    }
}
