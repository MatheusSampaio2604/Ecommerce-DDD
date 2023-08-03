using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceMontaMenu
    {
        Task<List<MenuSite>> MontaMenuPorPerfil(string userId);
    }
}
