using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Application.Features.Projects.Commands.CreateProject;
using ProjectManagement.Application.Features.Projects.Commands.DeleteProject;
using ProjectManagement.Application.Features.Projects.Commands.UpdateProject;
using ProjectManagement.Application.Features.Projects.DTOs;
using ProjectManagement.Application.Features.Projects.Queries.GetAllProjects;
using ProjectManagement.Application.Features.Projects.Queries.GetProjectById;

namespace ProjectManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> Create(CreateProjectCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponseDto>>> GetAll([FromQuery] GetAllProjectsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectResponseDto>>GetById(Guid id)
    {
        var result = await _mediator.Send( new GetProjectByIdQuery(id));
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProjectResponseDto>>
    Update(Guid id,UpdateProjectCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Route Id and Body Id mismatch");
        }
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProjectCommand(id));

        return NoContent();
    }
}