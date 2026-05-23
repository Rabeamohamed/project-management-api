using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Features.Tasks.Commands.DeleteTask;
using ProjectManagement.Application.Features.Tasks.Commands.UpdateTaskStatus;
using ProjectManagement.Application.Features.Tasks.DTOs;
using ProjectManagement.Application.Features.Tasks.Queries.GetTasksByProject;

namespace ProjectManagement.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<TaskResponseDto>>Create(CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult>UpdateStatus(Guid id, UpdateTaskStatusDto dto)
    {
        var command =new UpdateTaskStatusCommand(id,dto.Status);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<List<TaskResponseDto>>>GetByProject(Guid projectId)
    {
        var result = await _mediator.Send( new GetTasksByProjectQuery(projectId));
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTaskCommand(id));
        return NoContent();
    }
}