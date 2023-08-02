using Application.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogger<BaseController> logger;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly InterfaceLogSistemaApp _interfaceLogSistemaApp;


        public BaseController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, InterfaceLogSistemaApp interfaceLogSistemaApp)
        {
            this.logger = logger;
            _userManager = userManager;
            _interfaceLogSistemaApp = interfaceLogSistemaApp;
        }

        public async Task<string> RetornarIdUsuarioLogado()
        {
            if (_userManager != null)
            {
                var idUsuario = await _userManager.GetUserAsync(User);
                return idUsuario != null ?idUsuario.Id : string.Empty;
            }
            return string.Empty;
            
        }

        public async Task<bool> UsuarioAdministrador()
        {
            if (_userManager != null)
            {
                var idUsuario = await _userManager.GetUserAsync(User);
                if(idUsuario != null && idUsuario.Tipo != null)
                {
                    if((TipoUsuario)idUsuario.Tipo == TipoUsuario.Administrador)
                    {
                        return true;
                    }
                }
            }
            return false;

        }

        public async Task LogEcommerce(EnumTipoLog tipoLog, Object objeto)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await _interfaceLogSistemaApp.Add(new LogSistema
            {
                TipoLog = tipoLog,
                JsonInformacao = JsonConvert.SerializeObject(objeto),
                UserId = await RetornarIdUsuarioLogado(),
                NomeAction = actionName,
                NomeController = controllerName,

            });

        }
    }
}
