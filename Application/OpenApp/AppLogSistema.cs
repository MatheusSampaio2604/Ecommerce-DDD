using Application.Interfaces;
using Domain.Interfaces.InterfaceLogSistema;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class AppLogSistema : InterfaceLogSistemaApp
    {
        private readonly ILogSistema _iLogSistema;



        public AppLogSistema(ILogSistema iLogSistema)
        {
            _iLogSistema = iLogSistema;
        }


        public async Task Add(LogSistema Objeto)
        {
            await _iLogSistema.Add(Objeto);
        }

        public async Task Delete(LogSistema Objeto)
        {
            await _iLogSistema.Delete(Objeto);
        }

        public async Task<LogSistema> GetEntityById(int Id)
        {
            return await _iLogSistema.GetEntityById(Id);
        }

        public async Task<List<LogSistema>> List()
        {
            return await _iLogSistema.List();
        }

        public async Task Update(LogSistema Objeto)
        {
            await _iLogSistema.Update(Objeto);
        }
    }
}
