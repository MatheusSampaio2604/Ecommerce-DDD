using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        private readonly IUsuario _iUsuario;


        public ServiceUsuario(IUsuario iUsuario)
        {
            _iUsuario = iUsuario;
        }

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userId)
        {
            var usuario = await _iUsuario.ObterUsuarioPeloId(userId);
            if(usuario != null && usuario.Tipo == TipoUsuario.Administrador)
            {
                return await _iUsuario.List();
            }

            return new List<ApplicationUser>();
        }
    }
}
