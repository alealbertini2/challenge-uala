using TwitterUala.Application.Contracts.Applicaction;
using TwitterUala.Application.Contracts.Infrastructure;
using TwitterUala.Domain.Entities;

namespace TwitterUala.Application.UseCases
{
    public class CreateUserService(IUnitOfWork unitOfWork) : ICreateUserService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task CreateUserAsync(string username)
        {
            try
            {
                if (username.Length <= 0)
                {
                    throw new Exception("El nombre del usuario no puede ser vacío");
                }

                if (username.Length > 50)
                {
                    throw new Exception("El nombre del usuario no puede ser mayor a 50 caracteres");
                }

                var existUsername = await _unitOfWork.GetRepository<User>().FirstOrDefaultAsync(x => x.Username == username) != null;
                if (existUsername)
                {
                    throw new Exception("Ya existe un usuario con el mismo nombre de usuario");
                }

                User user = new User();
                user.Username = username;

                _unitOfWork.GetRepository<User>().Add(user);
                _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }
    }
}
