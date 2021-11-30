using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Repositories;
using HeidelbergCement.CaseStudies.Concurrency.Dto;
using HeidelbergCement.CaseStudies.Concurrency.Extensions;
using HeidelbergCement.CaseStudies.Concurrency.Infrastructure.DbContexts.Schedule;
using HeidelbergCement.CaseStudies.Concurrency.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace HeidelbergCement.CaseStudies.Concurrency.Services;

public class SchedulesService: ServiceBase<IScheduleDbContext>, IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    public SchedulesService(IScheduleDbContext dbContext, IScheduleRepository scheduleRepository) : base(dbContext)
    {
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<ScheduleDto> GetLatestDraftScheduleForPlant(int plantCode)
    {
        var currentDraftSchedule = await _scheduleRepository.GetCurrentDraftSchedule(plantCode);
        if (currentDraftSchedule == null)
        {
            throw new Exception($"There is no draft schedule for plant {plantCode}");
        }

        return currentDraftSchedule.MapToScheduleDto();
    }
}