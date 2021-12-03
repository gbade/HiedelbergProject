using HeidelbergCement.CaseStudies.Concurrency.Common.Validation;

namespace HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;

public class ScheduleItem
{
    public ScheduleItem()
    {
    }
    public ScheduleItem(DateTime start, DateTime end, string cementType, DateTime updatedOn)
    {
        DateValidator.ValidateRange(start, end);
        
        Start = start;
        End = end;
        CementType = cementType;
        UpdatedOn = updatedOn;
    }
    public int ScheduleItemId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string CementType { get; set; }
    public DateTime UpdatedOn { get; set; }
    public Schedule Schedule { get; set; }
    public int ScheduleId { get; set; }
}