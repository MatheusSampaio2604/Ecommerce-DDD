using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities.Entities;
using Infrastructure.Configuration;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Web_ECommerce.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace UI.Controllers
{
    [Authorize]
    [LogActionFilter]
    public class LogSistemasController : BaseController
    {
        public LogSistemasController(UserManager<ApplicationUser> userManager, ILogger<ProdutosController> logger, InterfaceLogSistemaApp interfaceLogSistemaApp)
            : base(logger, userManager, interfaceLogSistemaApp)
        {
        }

        // GET: LogSistemas
        public async Task<IActionResult> Index()
        {
            if(!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");
            
            return View(await _interfaceLogSistemaApp.List());
        }

        // GET: LogSistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!await UsuarioAdministrador())
                return RedirectToAction("Index", "Home");

            if (id == null)
            {
                return NotFound();
            }

            var logSistema = await _interfaceLogSistemaApp.GetEntityById((int)id);
               
            if (logSistema == null)
            {
                return NotFound();
            }

            return View(logSistema);
        }

    }
}
