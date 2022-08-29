using Application.ADN.Queries;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    public class StatsController : BaseController
    {
        [HttpGet]
        public async Task<StatsDto> Stats()
        {
            return await Mediator.Send(new List.Query());
        }
    }
}
