using Microsoft.AspNetCore.Mvc;
using TwitterUala.Application.Contracts.Applicaction;

namespace TwitterUala.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(ICreateUserService createUserService) : Controller
    {
        private readonly ICreateUserService _createUserService = createUserService;

        [HttpPost(Name = "User")]
        public void CreateUser(string username)
        {
            try
            {
                _createUserService.CreateUser(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear el usuario: {ex.Message}", ex);
            }
        }
    }
}
