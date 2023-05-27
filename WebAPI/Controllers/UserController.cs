using Application.Features.UserFeature.CreateUser;
using Application.Features.UserFeature.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }   

        [HttpGet]
        public string[] Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(CreateUserRequest request, CancellationToken token)
        {
            var response = await _mediator.Send(request, token);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserRequest request, CancellationToken token)
        {
            var response = await _mediator.Send(request, token);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
