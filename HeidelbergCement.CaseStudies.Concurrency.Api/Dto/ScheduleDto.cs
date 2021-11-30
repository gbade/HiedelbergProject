using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Enums;

namespace HeidelbergCement.CaseStudies.Concurrency.Dto;

public class ScheduleDto
{
    public int ScheduleId { get; set; }
    public int PlantCode { get; set; }
    public Status Status { get; set; }
    public DateTime UpdatedOn { get; set; }
    public ICollection<ScheduleItemDto> ScheduleItems { get; set; }
}