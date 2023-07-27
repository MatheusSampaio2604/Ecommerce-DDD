using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using Application.Interfaces;
using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Controllers;

namespace Web_ECommerce.Controllers
{

    [Authorize]

    [LogActionFilter]
    public class ProdutosController : BaseController
    {
        
        public readonly InterfaceProductApp _InterfaceProductApp;
        public readonly InterfaceCompraUsuarioApp _InterfaceCompraUsuarioApp;
        private readonly IWebHostEnvironment _environment;
        

        public ProdutosController(InterfaceProductApp InterfaceProductApp, UserManager<ApplicationUser> userManager, InterfaceCompraUsuarioApp InterfaceCompraUsuarioApp, IWebHostEnvironment environment, ILogger<ProdutosController> logger, InterfaceLogSistemaApp interfaceLogSistemaApp) 
            : base(logger, userManager, interfaceLogSistemaApp)
        {
            _InterfaceProductApp = InterfaceProductApp;
            _InterfaceCompraUsuarioApp = InterfaceCompraUsuarioApp;
            _environment = environment;
        }

        // GET: ProdutosController
        public async Task<IActionResult> Index()
        {
            var idUsuario = await RetornarIdUsuarioLogado();

            return View(await _InterfaceProductApp.ListarProdutosUsuario(idUsuario));
        }

        // GET: ProdutosController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        // GET: ProdutosController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdutosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            try
            {
                var idUsuario = await RetornarIdUsuarioLogado();

                produto.UserId = idUsuario;

                await _InterfaceProductApp.AddProduct(produto);
                if (produto.Notitycoes.Any())
                {
                    foreach (var item in produto.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }

                    return View("Create", produto);
                }

                await SalvarImagemProduto(produto);

                await LogEcommerce(EnumTipoLog.Informativo, produto);
            }
            catch(Exception erro)
            {
                await LogEcommerce(EnumTipoLog.Erro, erro);
                return View("Create", produto);
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: ProdutosController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Produto produto)
        {
            try
            {
                await _InterfaceProductApp.UpdateProduct(produto);
                await SalvarImagemProduto(produto);
                if (produto.Notitycoes.Any())
                {
                    foreach (var item in produto.Notitycoes)
                    {
                        ModelState.AddModelError(item.NomePropriedade, item.mensagem);
                    }

                    ViewBag.Alerta = true;
                    ViewBag.Mensagem = "Verifique, ocorreu algum erro!";

                    //await LogEcommerce(EnumTipoLog.Erro, produto);

                    return View("Edit", produto);
                }
                
            }
            catch(Exception erro)
            {
                await LogEcommerce(EnumTipoLog.Erro, erro);
                return View("Edit", produto);
            }

            await LogEcommerce(EnumTipoLog.Informativo, produto);

            return RedirectToAction(nameof(Index));
        }

        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return View(await _InterfaceProductApp.GetEntityById(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _InterfaceProductApp.GetEntityById(id);

                await _InterfaceProductApp.Delete(produtoDeletar);
                await LogEcommerce(EnumTipoLog.Informativo, produtoDeletar);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception erro)
            {
                await LogEcommerce(EnumTipoLog.Erro, erro);
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet("/api/ListarProdutosComEstoque")]
        public async Task<JsonResult> ListarProdutosComEstoque(string descricao)
        {
            var item = await _InterfaceProductApp.ListarProdutosComEstoque(descricao);
            
            return Json(item);
        }

        public async Task<IActionResult> ListarProdutosCarrinhoUsuario()
        {
            var idUsuario = await RetornarIdUsuarioLogado();
            return View(await _InterfaceProductApp.ListarProdutosCarrinhoUsuario(idUsuario));

        }



        // GET: ProdutosController/Delete/5
        public async Task<IActionResult> RemoverCarrinho(int id)
        {
            return View(await _InterfaceProductApp.ObterProdutoCarrinho(id));
        }

        // POST: ProdutosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverCarrinho(int id, Produto produto)
        {
            try
            {
                var produtoDeletar = await _InterfaceCompraUsuarioApp.GetEntityById(id);

                await _InterfaceCompraUsuarioApp.Delete(produtoDeletar);

                return RedirectToAction(nameof(ListarProdutosCarrinhoUsuario));
            }
            catch(Exception erro)
            {
                await LogEcommerce(EnumTipoLog.Erro, erro);
                return View();
            }
        }



        public async Task SalvarImagemProduto(Produto produtoTela)
        {
            try
            {
                var produto = await _InterfaceProductApp.GetEntityById(produtoTela.Id);

                if (produtoTela.Imagem != null)
                {
                    var webRoot = _environment.WebRootPath;

                    var extension = System.IO.Path.GetExtension(produtoTela.Imagem.FileName);
                    var nomeArquivo = string.Concat(produto.Id.ToString(), extension);
                    var diretorioArquivoSalvar = System.IO.Path.Combine(webRoot, "imgProdutos", nomeArquivo);

                    using (var stream = new FileStream(diretorioArquivoSalvar, FileMode.Create))
                    {
                        await produtoTela.Imagem.CopyToAsync(stream);
                    }

                    produto.Url = $"https://localhost:5001/imgProdutos/{nomeArquivo}";

                    await _InterfaceProductApp.UpdateProduct(produto);
                }
            }
            catch (Exception erro)
            {
                await LogEcommerce(EnumTipoLog.Erro, erro);
                // Lide com a exceção de alguma forma apropriada
            }


        }


    }
}
