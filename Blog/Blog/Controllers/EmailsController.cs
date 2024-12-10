using Blog.Application.Commands.SendEmail;
using Blog.Models.Emails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmailsController(
        ISender mediator) : ControllerBase
    {
        private readonly ISender _mediator = mediator;


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmail(SendEmailRequest emailRequest)
        {
            await _mediator.Send(new SendEmailCommand(emailRequest.Email, emailRequest.Subject, emailRequest.Message));

            return NoContent();
        }
    }
}
