using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceUsuario
{

    public interface IUsuario : IGeneric<ApplicationUser>
    {
        Task<ApplicationUser> ObterUsuarioPeloId(string userId);

        Task AtualizarTipoUsuario(string userId, TipoUsuario tipousuario);


    }
}
