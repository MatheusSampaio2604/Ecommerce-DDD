using Domain.Interfaces.Generics;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceUsuario
{

    public interface IUsuario : IGeneric<ApplicationUser>
    {
        Task<ApplicationUser> ObterUsuarioPeloId(string userId);

        Task AtualizarTipoUsuario(string userId, TipoUsuario tipousuario);


    }
}
