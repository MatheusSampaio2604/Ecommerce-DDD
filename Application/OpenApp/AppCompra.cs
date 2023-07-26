using Application.Interfaces;
using Domain.Interfaces.InterfaceCompra;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OpenApp
{
    public class AppCompra : InterfaceCompraApp
    {
        private readonly ICompra _iCompra;
        
        public AppCompra(ICompra iCompra)
        {
            _iCompra = iCompra;
        }

        public async Task Add(Compra Objeto)
        {
            await _iCompra.Add(Objeto);
        }

        public async Task Delete(Compra Objeto)
        {
            await _iCompra.Delete(Objeto);
        }

        public async Task<Compra> GetEntityById(int Id)
        {
            return await _iCompra.GetEntityById(Id);
        }

        public async Task<List<Compra>> List()
        {
            return await _iCompra.List();
        }

        public async Task Update(Compra Objeto)
        {
            await _iCompra.Update(Objeto);
        }
    }
}
