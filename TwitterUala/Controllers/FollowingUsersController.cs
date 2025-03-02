using Microsoft.AspNetCore.Mvc;
using TwitterUala.Application.Contracts.Applicaction;

namespace TwitterUala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowingUsersController(ILogger<FollowingUsersController> logger, IFollowUserService followUserService) : ControllerBase
    {
        private readonly ILogger<FollowingUsersController> _logger = logger;
        private readonly IFollowUserService _followUserService = followUserService;

        [HttpPost(Name = "FollowUser")]
        public void FollowUser(long userId, long userToFollowId)
        {
            _followUserService.FollowUser(userId, userToFollowId);
        }
    }
}
