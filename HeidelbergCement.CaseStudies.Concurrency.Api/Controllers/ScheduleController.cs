using System.Net.Mime;
using HeidelbergCement.CaseStudies.Concurrency.Dto;
using HeidelbergCement.CaseStudies.Concurrency.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeidelbergCement.CaseStudies.Concurrency.Controllers;

[Route("schedule")]
[ApiController]
public class ScheduleController: ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet("draft")]
    [ProducesResponseType(typeof(ScheduleDto), StatusCodes.Status200OK)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ScheduleDto>> GetDraft(int plantCode)
    {
        var schedule = await _scheduleService.GetLatestDraftScheduleForPlant(plantCode);
        return Ok(schedule);
    }
}