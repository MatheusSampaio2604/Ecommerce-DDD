using Application.Interfaces;
using ApplicationApp.Interfaces;
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
    [Authorize]
    [LogActionFilter]
    public class ProdutoAPIController : BaseController
    {
        public readonly InterfaceProductApp _interfaceProductApp;
        public readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;
        
        public ProdutoAPIController(InterfaceProductApp interfaceProductApp, UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp,  ILogger<ProdutoAPIController> logger, InterfaceLogSistemaApp interfaceLogSistemaApp)
            : base(logger, userManager, interfaceLogSistemaApp)
        {
            _interfaceProductApp = interfaceProductApp;
            _interfaceCompraUsuarioApp = interfaceCompraUsuarioApp;
        }

        [HttpGet("/api/ListaProdutos")]
        public async Task<JsonResult> ListaProdutos(string descricao)
        {
            return Json(await _interfaceProductApp.ListarProdutosComEstoque(descricao));
        }


    }
}
