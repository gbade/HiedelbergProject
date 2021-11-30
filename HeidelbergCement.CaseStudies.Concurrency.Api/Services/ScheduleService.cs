using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Repositories;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Input;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Response;
using HeidelbergCement.CaseStudies.Concurrency.Extensions;
using HeidelbergCement.CaseStudies.Concurrency.Infrastructure.DbContexts.Schedule;
using HeidelbergCement.CaseStudies.Concurrency.Services.Interfaces;

namespace HeidelbergCement.CaseStudies.Concurrency.Services;

public class SchedulesService: ServiceBase<IScheduleDbContext>, IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    public SchedulesService(IScheduleDbContext dbContext, IScheduleRepository scheduleRepository) : base(dbContext)
    {
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<ScheduleResponseDto> GetLatestDraftScheduleForPlant(int plantCode)
    {
        var currentDraftSchedule = await _scheduleRepository.GetCurrentDraftSchedule(plantCode);
        if (currentDraftSchedule == null)
        {
            throw new Exception($"There is no draft schedule for plant {plantCode}");
        }

        return currentDraftSchedule.MapToScheduleDto();
    }

    public async Task<ScheduleResponseDto> AddItemToSchedule(int scheduleId, ScheduleInputItemDto scheduleItem)
    {
        var scheduleWithId = await _scheduleRepository.GetScheduleById(scheduleId);
        scheduleWithId.AddItem(
            start: scheduleItem.Start,
            end: scheduleItem.End,
            assetId: scheduleItem.AssetId,
            updatedOn: DateTime.UtcNow);
        return scheduleWithId.MapToScheduleDto();
    }
}