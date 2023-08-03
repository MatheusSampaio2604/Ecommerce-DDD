using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IServiceUsuario
    {
        Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userId);
    }
}
