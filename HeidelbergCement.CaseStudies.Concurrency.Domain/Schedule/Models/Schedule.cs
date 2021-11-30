using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Enums;

namespace HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;

public class Schedule
{
    public Schedule()
    {
        ScheduleItems = new List<ScheduleItem>();
    }

    public Schedule(int plantCode, DateTime now)
    {
        PlantCode = plantCode;
        UpdatedOn = now;
        Status = Status.Draft;
        ScheduleItems = new List<ScheduleItem>();
    }
    public int ScheduleId { get; set; }
    public int PlantCode { get; set; }
    public Status Status { get; set; }
    public DateTime UpdatedOn { get; set; }
    public ICollection<ScheduleItem> ScheduleItems { get; set; }
}