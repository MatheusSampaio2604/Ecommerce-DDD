using Application.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ECommerce.Controllers;

namespace UI.Controllers
{
    public class MontaMenu : BaseController
    {
        private readonly InterfaceMontaMenu _interfaceMontaMenu;

        public MontaMenu(UserManager<ApplicationUser> userManager, ILogger<ProdutosController> logger, InterfaceLogSistemaApp interfaceLogSistemaApp, InterfaceMontaMenu interfaceMontaMenu)
            : base (logger, userManager, interfaceLogSistemaApp)
        {
            _interfaceMontaMenu = interfaceMontaMenu;
        }

        [AllowAnonymous]
        [HttpGet("/api/ListarMenu")]
        public async Task<IActionResult> ListarMenu()
        {
            List<MenuSite> listaMenu = new();

            var usuario = await RetornarIdUsuarioLogado();

            listaMenu = await _interfaceMontaMenu.MontaMenuPorPerfil(usuario);

            return Json(new { listaMenu });
        }

    }
}
