namespace HeidelbergCement.CaseStudies.Concurrency.Dto.Response;

public class ScheduleItemResponseDto
{
    public int ScheduleItemId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int AssetId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int ScheduleId { get; set; }
}