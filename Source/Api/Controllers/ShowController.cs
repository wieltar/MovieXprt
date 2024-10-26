using Microsoft.AspNetCore.Mvc;
using MovieXprt.Application.UseCases;
using MovieXprt.Common.Models;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController(
            IGetScheduleUsecase getScheduleUsecase
        ) : ControllerBase
    {
        private readonly IGetScheduleUsecase _getScheduleUsecase = getScheduleUsecase ?? throw new ArgumentNullException(nameof(getScheduleUsecase));


        [HttpGet("aired-on")]
        [ProducesResponseType(typeof(List<Show>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Show>>> Get([FromQuery] DateOnly airDate, [FromQuery] string? countryCode, CancellationToken ct)
        {
     
            var shows = await _getScheduleUsecase.Run(airDate, countryCode, ct);
            return Ok(shows);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<Show>), StatusCodes.Status204NoContent)]
        public NoContentResult Delete(int id)
        {
            return NoContent();
        }
    }
}
