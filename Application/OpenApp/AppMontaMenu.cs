using Application.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class AppMontaMenu : InterfaceMontaMenu
    {
        private readonly IServiceMontaMenu _iServiceMontaMenu;
        public AppMontaMenu(IServiceMontaMenu iServiceMontaMenu)
        {
            _iServiceMontaMenu = iServiceMontaMenu;
        }

        public async Task<List<MenuSite>> MontaMenuPorPerfil(string userId)
        {
            return await _iServiceMontaMenu.MontaMenuPorPerfil(userId);
        }
    }
}
