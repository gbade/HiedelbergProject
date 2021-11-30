using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Enums;
using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Repositories;
using HeidelbergCement.CaseStudies.Concurrency.Infrastructure.DbContexts.Schedule;
using Microsoft.EntityFrameworkCore;

namespace HeidelbergCement.CaseStudies.Concurrency.Infrastructure.Repositories.Schedule;

public class ScheduleRepository: GenericRepository<Domain.Schedule.Models.Schedule>, IScheduleRepository
{
    public ScheduleRepository(IScheduleDbContext context) : base(context)
    {
    }

    public Task<Domain.Schedule.Models.Schedule> GetCurrentDraftSchedule(int plantCode)
    {
        return GetAllSchedulesIncludingItems().SingleAsync(it => it.Status == Status.Draft);
    }

    public Task<Domain.Schedule.Models.Schedule> GetScheduleById(int scheduleId)
    {
        return FindByInclude(it => it.ScheduleId == scheduleId, it => it.ScheduleItems).SingleAsync();
    }

    private IQueryable<Domain.Schedule.Models.Schedule> GetAllSchedulesIncludingItems()
    {
        return GetAllIncluding(it => it.ScheduleItems);
    }
    
}