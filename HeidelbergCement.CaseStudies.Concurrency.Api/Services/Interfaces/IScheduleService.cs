using HeidelbergCement.CaseStudies.Concurrency.Dto.Input;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Response;

namespace HeidelbergCement.CaseStudies.Concurrency.Services.Interfaces;

public interface IScheduleService
{
    public Task<ScheduleResponseDto> GetLatestScheduleForPlant(int plantCode);
    public Task<(ScheduleResponseDto? Response, string ErrorMessage)> AddItemToSchedule(int scheduleId, ScheduleInputItemDto scheduleItem);
    public Task<ScheduleResponseDto> AddNewSchedule(int plantCode, List<ScheduleInputItemDto> scheduleInput);
    public Task<(ScheduleResponseDto? Response, string ErrorMessage)> ChangeScheduleItem(int scheduleId, int itemId, ScheduleInputItemDto scheduleInputItem);
}