using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infra.Repository.Repositories
{
    public class RepositoryUsuario : RepositoryGenerics<ApplicationUser>, IUsuario
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryUsuario()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task AtualizarTipoUsuario(string userId, TipoUsuario tipousuario)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                var usuario = await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
                if (usuario != null)
                {
                    usuario.Tipo = tipousuario;
                    banco.ApplicationUser.Update(usuario);
                    await banco.SaveChangesAsync();
                }
            }
        }

        public async Task<ApplicationUser> ObterUsuarioPeloId(string userId)
        {
            using (var banco = new ContextBase(_optionsBuilder))
            {
                return await banco.ApplicationUser.FirstOrDefaultAsync(u => u.Id.Equals(userId));
            }

        }
    }
}
