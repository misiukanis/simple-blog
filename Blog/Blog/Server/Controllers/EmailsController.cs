using Blog.Application.Commands.SendEmail;
using Blog.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EmailsController : ControllerBase
    {
        private readonly ISender _mediator;

        public EmailsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SendEmail(CreateEmailMessageDTO emailMessage)
        {
            await _mediator.Send(new SendEmailCommand(emailMessage.Email, emailMessage.Subject, emailMessage.Message));

            return NoContent();
        }
    }
}
