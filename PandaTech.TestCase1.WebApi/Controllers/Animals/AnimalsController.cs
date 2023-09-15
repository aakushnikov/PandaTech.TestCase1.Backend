using System.ComponentModel;
using System.Net.Mime;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PandaTech.TestCase1.WebApi.Controllers.Animals.AddAnimal;
using PandaTech.TestCase1.WebApi.Controllers.Animals.GetAllAnimals;
using PandaTech.TestCase1.WebApi.Controllers.Animals.RemoveAnimal;

namespace PandaTech.TestCase1.WebApi.Controllers.Animals;

[ApiController]
[Route("api/animals")]
[Consumes(MediaTypeNames.Application.Json)]
[Produces(MediaTypeNames.Application.Json)]
public sealed class AnimalsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimalsController(IMediator mediator) =>
        _mediator = mediator;
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Description("Tests api accesebility for authentificated users")]
    public IActionResult Test()
    {
        return Ok();
    }
    
    [HttpGet("/{page:int}-{limit:int}")]
    [ProducesResponseType(typeof(GetAllAnimalsOut), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Description("Gets animals list")]
    public async Task<IActionResult> GetAllAnimals(
        [FromRoute] int page,
        [FromRoute] int limit,
        CancellationToken cancellationToken)
    {
        var request = new GetAllAnimalsQuery
        {
            Page = page,
            Limit = limit,
        };

        return Prelude.match(
            await _mediator.Send(request, cancellationToken),
            response => (IActionResult)Ok(response),
            error => StatusCode(error.Code, error.Message)
        );
    }
    
    [HttpPost("/add")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Description("Adds new animal to the list")]
    public async Task<IActionResult> AddAnimal(
        AddAnimalCommand request,
        CancellationToken cancellationToken)
    {
        return Prelude.match(
            await _mediator.Send(request, cancellationToken),
            response => (IActionResult)Ok(response),
            error => StatusCode(error.Code, error.Message)
        );
    }
    
    [HttpPost("/remove")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Description("Removes an animal from the list")]
    public async Task<IActionResult> RemoveAnimal(
        RemoveAnimalCommand request,
        CancellationToken cancellationToken)
    {
        return Prelude.match(
            await _mediator.Send(request, cancellationToken),
            response => (IActionResult)Ok(response),
            error => StatusCode(error.Code, error.Message)
        );
    }
}