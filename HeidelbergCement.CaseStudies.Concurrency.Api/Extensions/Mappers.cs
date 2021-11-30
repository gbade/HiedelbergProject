using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;
using HeidelbergCement.CaseStudies.Concurrency.Dto;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Response;

namespace HeidelbergCement.CaseStudies.Concurrency.Extensions;

public static class Mappers
{
    public static ScheduleItemResponseDto MapToScheduleItemDto(this ScheduleItem scheduleItem)
    {
        return new ScheduleItemResponseDto
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
    public static ScheduleResponseDto MapToScheduleDto(this Schedule schedule)
    {
        return new ScheduleResponseDto
        {
            Status = schedule.Status,
            PlantCode = schedule.PlantCode,
            ScheduleId = schedule.ScheduleId,
            UpdatedOn = schedule.UpdatedOn,
            ScheduleItems = schedule.ScheduleItems.Select(MapToScheduleItemDto).ToList()
        };
    }
}