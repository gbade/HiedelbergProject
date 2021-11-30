using System.ComponentModel.DataAnnotations;
using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;

namespace HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Validation;

public static class ScheduleItemValidation
{
    public static void ValidateDoesNotOverlapWithItems(this ScheduleItem currentItem, List<ScheduleItem> scheduleItems)
    {
        var itemsWithMatchingAsset = scheduleItems
            .Where(it => it.AssetId == currentItem.AssetId)
            .ToList();
        if (itemsWithMatchingAsset.Any(scheduleItem => currentItem.Start < scheduleItem.End && scheduleItem.Start < currentItem.End))
        {
            throw new ValidationException("There is a conflict with the other planned item.");
        }
    }
}