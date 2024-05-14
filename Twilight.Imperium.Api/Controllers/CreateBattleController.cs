using Microsoft.AspNetCore.Mvc;
using Twilight.Imperium.Application.Generic;
using Twilight.Imperium.Domain.Battle.Commands;

namespace Twilight.Imperium.Api.Controllers
{
  [ApiController]
  [Route( "api/[controller]" )]
  public class CreateBattleController : ControllerBase
  {
    [HttpPost]
    public async Task<IActionResult> Execute( [FromServices] IInitialCommandUseCase<CreateBattleCommand> useCase )
    {
      try
      {
        return StatusCode( StatusCodes.Status200OK, await useCase.Execute( new CreateBattleCommand() ) );
      }
      catch (Exception ex)
      {
        return BadRequest( ex.Message );
      }
    }
  }
}
