using Microsoft.AspNetCore.Mvc;
using MovieXprt.Application.UseCases;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowController(
            IQueryShowsUsecase queryShowsUsecase
        ) : ControllerBase
    {
        private readonly IQueryShowsUsecase _queryShowsUsecase = queryShowsUsecase ?? throw new ArgumentNullException(nameof(queryShowsUsecase));


        [HttpGet("")]
        public string Get([FromQuery] DateOnly? airDate, [FromQuery] string? query)
        {
            _queryShowsUsecase.Run(query);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
