using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HeidelbergCement.CaseStudies.Concurrency.Api.Test.Infrastructure;
using HeidelbergCement.CaseStudies.Concurrency.Dto;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Input;
using HeidelbergCement.CaseStudies.Concurrency.Dto.Response;
using Xunit;
using Xunit.Abstractions;

namespace HeidelbergCement.CaseStudies.Concurrency.Api.Test;

public class ItemsTests: IntegrationTestBase
{
    public ItemsTests(IntegrationTestFixture integrationTestFixture, ITestOutputHelper output) : base(integrationTestFixture, output)
    {
    }
    
    [Fact]
    public async Task GivenScheduleWithNoItems_WhenTwoSimultaneousIdenticalAddItemRequests_ThenOneItemIsAddedAndTheOtherRejected()
    {
        //Setup
        var itemToAdd = new ScheduleInputItemDto
        {
            Start = DateTime.UtcNow,
            End = DateTime.UtcNow.AddHours(1),
            AssetId = 1
        };

        var draftScheduleRequest = NewRequest
            .AddRoute("schedule/draft")
            .AddQueryParams("plantCode", "1234");
        var addItemForScheduleRequest = (string scheduleId) => NewRequest
            .AddRoute("schedule/items")
            .AddQueryParams("scheduleId", scheduleId);
        
        // Exercise
        // First let's get the schedule before adding any items. This schedule is currently empty..
        var scheduleBeforeAddition = await draftScheduleRequest.Get<ScheduleResponseDto>();

        var scheduleId = scheduleBeforeAddition.ScheduleId.ToString();
        var addItemRequest = addItemForScheduleRequest(scheduleId);

        // Simultaneously start two tasks that will make the same exact item addition request.
        // This is a race condition, the first request should pass and the second should fail.
        var itemAddResponses = await Task.WhenAll(addItemRequest.Post(itemToAdd, false), addItemRequest.Post(itemToAdd, false));

        //Finally let's get the schedule after the item addition requests. It should have only one item in it.
        var scheduleAfterAddition = await draftScheduleRequest.Get<ScheduleResponseDto>();

        // Verify
        scheduleBeforeAddition.ScheduleItems.Count.Should().Be(0);
        
        var failures = itemAddResponses.ToList().Where(it => it.IsSuccessStatusCode == false);
        var successes = itemAddResponses.ToList().Where(it => it.IsSuccessStatusCode == true);

        failures.Count().Should().Be(1);
        successes.Count().Should().Be(1);
        
        scheduleAfterAddition.ScheduleItems.Count.Should().Be(1);
    }
}