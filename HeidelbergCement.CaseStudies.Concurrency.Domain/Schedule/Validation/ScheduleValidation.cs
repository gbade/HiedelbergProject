using System.ComponentModel.DataAnnotations;
using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Enums;

namespace HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Validation;

public static class ScheduleValidation
{
    public static void ValidateScheduleCanBeModified(this Models.Schedule schedule)
    {
        if (schedule.Status == Status.Submitted)
        {
            throw new ValidationException("Cannot modify a submitted schedule");
        }
    }
}