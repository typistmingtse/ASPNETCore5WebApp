using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace ASPNETCore5Demo.Controllers
{
    public class ErrorController : ControllerBase
    {
        //[HttpGet("/error")]
        //public IActionResult Index()
        //{
        //    return Problem();
        //}

        [HttpGet("/error")]
        public ActionResult Error([FromServices] IHostEnvironment webHostEnvironment)
        {
            //return Problem();
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = feature?.Error;
            var isDev = webHostEnvironment.IsDevelopment();
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = isDev ? $"{ex.GetType().Name}: {ex.Message}" : "An error occurred.",
                Detail = isDev ? ex.StackTrace : null,
            };

            return StatusCode(problemDetails.Status.Value, problemDetails);
        }
    }
}
