using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceProduct
    {
        Task AddProduct(Produto produto);
        Task UpdateProduct(Produto produto);

        Task<List<Produto>> ListarProdutosComEstoque(string descricao);
    }
}
