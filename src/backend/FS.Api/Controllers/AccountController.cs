namespace FS.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Application.Commands.Command;
    using Application.Queries.Query;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [Authorize]
    [ApiController]
    [Route("v1")]
    public class AccountController  : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public AccountController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("account/{id}")]
        public async Task<IActionResult> GetAccount(Guid id)
        {
            return Ok();
        }
        
        [HttpGet("account/user/{id}")]
        public async Task<IActionResult> GetAccountByUserId(Guid id)
        {
            var userInfo = this.GetUserInfo();
            
            if(userInfo is null)
                return BadRequest("No token found!");
            
            var query = new GetAccountByUserIdQuery(userInfo.UserId);

            var result = await _mediator.Send(query);
            
            return Ok(result);
        }

        [HttpPost("account")]
        public async Task<IActionResult> CreatAccount()
        {
            var userInfo = this.GetUserInfo();
            
            if(userInfo is null)
                return BadRequest("No token found!");
            
            var command = new CreateAccountCommand(userInfo.UserId);

            var result = await _mediator.Send(command);

            return Created($"account/{result}", new {id = result});
        }
    }
}