using Application.Features.FriendFeature;
using Application.Features.FriendFeature.ChangeStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FriendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateRequest(InitiateFriendshipRequest request, CancellationToken token)
        {
            var response = await _mediator.Send(request, token);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult> ChangeInitiationStatus(int id, ChangeInitiationStatusRequest request, CancellationToken token)
        {
            request.Id = id;
            var response = await _mediator.Send(request, token);
            return Ok(response);
        }
    }
}
