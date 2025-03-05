using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class CreateUserService(IUnitOfWork unitOfWork) : ICreateUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public void CreateUser(string username)
        {
            User user = new User();
            user.Username = username;

            _unitOfWork.GetRepository<User>().Add(user);
            _unitOfWork.SaveChangesAsync();
        }
    }
}
