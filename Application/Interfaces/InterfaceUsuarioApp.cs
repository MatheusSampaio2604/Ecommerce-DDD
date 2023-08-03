using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface InterfaceUsuarioApp : InterfaceGenericaApp<ApplicationUser>
    {
        Task<ApplicationUser> ObterUsuarioPeloId(string userId);
        Task AtualizarTipoUsuario(string userId, TipoUsuario tipoUsuario);

        Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userId);
    }
}
