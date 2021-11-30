using HeidelbergCement.CaseStudies.Concurrency.Common.Validation;

namespace HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;

public class ScheduleItem
{
    public ScheduleItem()
    {
    }
    public ScheduleItem(int id, DateTime start, DateTime end, int assetId, DateTime updatedOn)
    {
        DateValidator.ValidateRange(start, end);
        
        ScheduleItemId = id;
        Start = start;
        End = end;
        AssetId = assetId;
        UpdatedOn = updatedOn;
    }
    public int ScheduleItemId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int AssetId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime UpdatedOn { get; set; }
    public Schedule Schedule { get; set; }
    public int ScheduleId { get; set; }
}