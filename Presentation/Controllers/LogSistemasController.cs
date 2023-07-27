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

namespace UI.Controllers
{
    public class LogSistemasController : Controller
    {
        private readonly InterfaceLogSistemaApp _interfaceLogSistemaApp;
        

        public LogSistemasController(InterfaceLogSistemaApp interfaceLogSistemaApp)
        {
            _interfaceLogSistemaApp = interfaceLogSistemaApp;
        }

        // GET: LogSistemas
        public async Task<IActionResult> Index()
        {
            
            return View(await _interfaceLogSistemaApp.List());
        }

        // GET: LogSistemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
