using HeidelbergCement.CaseStudies.Concurrency.Dto;

namespace HeidelbergCement.CaseStudies.Concurrency.Services.Interfaces;

public interface IScheduleService
{
    public Task<ScheduleDto> GetLatestDraftScheduleForPlant(int plantCode);
}