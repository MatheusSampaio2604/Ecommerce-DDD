using Application.Interfaces;
using Domain.Interfaces.InterfaceUsuario;
using Domain.Services;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class AppUsuario : InterfaceUsuarioApp
    {
        private readonly IUsuario _iUsuario;
        private readonly IServiceUsuario _iServiceUsuario;

        public AppUsuario(IUsuario iUsuario, IServiceUsuario iServiceUsuario)
        {
            _iUsuario = iUsuario;
            _iServiceUsuario = iServiceUsuario;
        }

        public async Task AtualizarTipoUsuario(string userId, TipoUsuario tipoUsuario)
        {
            await _iUsuario.AtualizarTipoUsuario(userId, tipoUsuario);
        }

        public async Task<ApplicationUser> ObterUsuarioPeloId(string userId)
        {
            return await _iUsuario.ObterUsuarioPeloId(userId);
        }

        public async Task<List<ApplicationUser>> ListarUsuarioSomenteParaAdministradores(string userId)
        {

            return await _iServiceUsuario.ListarUsuarioSomenteParaAdministradores(userId);
        }

        public async Task Add(ApplicationUser Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(ApplicationUser Objeto)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetEntityById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationUser>> List()
        {
            return await _iUsuario.List();
        }


        public async Task Update(ApplicationUser Objeto)
        {
            throw new NotImplementedException();
        }

        
    }
}
