namespace HeidelbergCement.CaseStudies.Concurrency.Dto.Input;

public class ScheduleInputItemDto
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int AssetId { get; set; }
}