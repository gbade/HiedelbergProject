using HeidelbergCement.CaseStudies.Concurrency.Dto;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Input;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Response;

namespace HeidelbergCement.CaseStudies.Concurrency.Services.Interfaces;

public interface IScheduleService
{
    public Task<ScheduleResponseDto> GetLatestDraftScheduleForPlant(int plantCode);
    public Task<ScheduleResponseDto> AddItemToSchedule(int scheduleId, ScheduleInputItemDto scheduleItem);
}