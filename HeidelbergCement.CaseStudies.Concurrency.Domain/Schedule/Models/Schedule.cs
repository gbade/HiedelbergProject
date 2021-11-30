using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Enums;
using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Validation;

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

    public void AddItem(DateTime start, DateTime end, int assetId, DateTime updatedOn)
    {
        this.ValidateScheduleCanBeModified();
        var item = new ScheduleItem(start, end, assetId, updatedOn);
        item.ValidateDoesNotOverlapWithItems(ScheduleItems.ToList());
        ScheduleItems.Add(item);
    }
}