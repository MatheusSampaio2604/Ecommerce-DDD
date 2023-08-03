using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface InterfaceMontaMenu
    {
        Task<List<MenuSite>> MontaMenuPorPerfil(string userId);
    }
}
