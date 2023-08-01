using ApplicationApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSite.Controllers
{
    [Authorize]
    public class ProdutoAPIController : Controller
    {
        public readonly InterfaceProductApp _interfaceProductApp;
        public readonly InterfaceCompraUsuarioApp _interfaceCompraUsuarioApp;

        public ProdutoAPIController(InterfaceProductApp interfaceProductApp, InterfaceCompraUsuarioApp interfaceCompraUsuarioApp)
          
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
