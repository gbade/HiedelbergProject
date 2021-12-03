using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;
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
    
    public async Task<ScheduleResponseDto> GetScheduleForPlant(int plantCode)
    {
        var currentDraftSchedule = await _scheduleRepository.GetLastUpdatedScheduleForPlant(plantCode);
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
            cementType: scheduleItem.CementType,
            now: DateTime.UtcNow);
        await _scheduleRepository.Update(scheduleWithId);
        return scheduleWithId.MapToScheduleDto();
    }

    public async Task<ScheduleResponseDto> AddNewSchedule(int plantCode, ScheduleInputItemDto[]? scheduleInputItems)
    {
        var now = DateTime.UtcNow;
        var schedule = new Schedule(plantCode, now);
        if (scheduleInputItems != null)
        {
            foreach (var scheduleInputScheduleItem in scheduleInputItems)
            {
                schedule.AddItem(
                    start: scheduleInputScheduleItem.Start,
                    end: scheduleInputScheduleItem.End,
                    cementType: scheduleInputScheduleItem.CementType,
                    now: now);
            }
        }

        await _scheduleRepository.Insert(schedule);
        return schedule.MapToScheduleDto();
    }
}