using Microsoft.AspNetCore.Mvc;
using Api.Mapper;
using Api.Contracts;
using MovieXprt.Domain.UseCases;


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
        [ProducesResponseType(typeof(List<ShowContract>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ShowContract>>> Get([FromQuery] DateOnly airDate, [FromQuery] string? countryCode, CancellationToken ct)
        {
     
            var shows = await _getScheduleUsecase.Run(airDate, countryCode, ct);
            return Ok(shows.Select(x => x.MapToContract()));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<ShowContract>), StatusCodes.Status204NoContent)]
        public NoContentResult Delete(int id)
        {
            return NoContent();
        }
    }
}
