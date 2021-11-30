using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;
using HeidelbergCement.CaseStudies.Concurrency.Dto;

namespace HeidelbergCement.CaseStudies.Concurrency.Extensions;

public static class Mappers
{
    public static ScheduleItemDto MapToScheduleItemDto(this ScheduleItem scheduleItem)
    {
        return new ScheduleItemDto
        {
            End = scheduleItem.End,
            Start = scheduleItem.Start,
            AssetId = scheduleItem.AssetId,
            IsDeleted = scheduleItem.IsDeleted,
            ScheduleId = scheduleItem.ScheduleId,
            UpdatedOn = scheduleItem.UpdatedOn,
            ScheduleItemId = scheduleItem.ScheduleItemId
        };
    }
    public static ScheduleDto MapToScheduleDto(this Schedule schedule)
    {
        return new ScheduleDto
        {
            Status = schedule.Status,
            PlantCode = schedule.PlantCode,
            ScheduleId = schedule.ScheduleId,
            UpdatedOn = schedule.UpdatedOn,
            ScheduleItems = schedule.ScheduleItems.Select(MapToScheduleItemDto).ToList()
        };

    }
}