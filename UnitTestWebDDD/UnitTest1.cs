using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Domain.Interfaces.InterfaceProduct;


namespace UnitTestWebDDD
{
    [TestClass]
    public class UnitTestEcommerceDDD
    {


        [TestMethod]
        public async Task AddProductComSucesso()
        {
            IProduct _iProduct = new RepositoryProduct();
        }

        [TestMethod]
        public async Task AddProductComValidacaoCampoObrigatorio()
        {

        }

        [TestMethod]
        public async Task ListarProdutosUsuario()
        {

        }

        [TestMethod]
        public async Task GetEntityById()
        {

        }

        [TestMethod]
        public async Task Delete()
        {

        }
    }
}
