using System.Threading.Tasks;
using FluentAssertions;
using HeidelbergCement.CaseStudies.Concurrency.Api.Test.Infrastructure;
using HeidelbergCement.CaseStudies.Concurrency.Dto;
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
        var scheduleResponse = await NewRequest
            .AddRoute("schedule/draft")
            .AddQueryParams("plantCode", "1234")
            .Get<ScheduleDto>();
        scheduleResponse.ScheduleItems.Count.Should().Be(0);
    }
}