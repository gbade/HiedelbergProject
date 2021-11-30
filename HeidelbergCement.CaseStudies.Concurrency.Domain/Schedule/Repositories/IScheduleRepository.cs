using HeidelbergCement.CaseStudies.Concurrency.Domain.Common.Repository;

namespace HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Repositories;

public interface IScheduleRepository: IGenericRepository<Models.Schedule>
{

    public Task<Models.Schedule?> GetCurrentDraftSchedule(int plantCode); 
}