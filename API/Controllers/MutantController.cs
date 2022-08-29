using Application.ADN.Commands;
using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class MutantController : BaseController
    {
       
        [HttpPost]
        public async Task<IActionResult> Mutant(Save.Command command)
        {
            try
            {
                var result = await Mediator.Send(command);
                if (!result)
                    return StatusCode((int)HttpStatusCode.Forbidden);

                return Ok();
            }
            catch (ValidateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
