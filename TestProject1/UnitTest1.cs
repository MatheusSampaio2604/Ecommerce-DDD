using Domain.Interfaces.InterfaceProduct;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infrastructure.Repository.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace UnitTestWebDDD
{
    [TestClass]
    public class UnitTestEcommerceDDD
    {


        [TestMethod]
        public async Task AddProductComSucesso()
        {
            try
            {
                IProduct _iProduct = new RepositoryProduct();
                IServiceProduct _iServiceProduct = new ServiceProduct(_iProduct);

                var produto = new Produto
                {
                    Descricao = string.Concat("Descrição Teste TDD", DateTime.Now.ToString()),
                    QtdEstoque = 10,
                    Nome = string.Concat("Nome Teste TDD", DateTime.Now.ToString()),
                    Valor = 20,
                    UserId = "7af3595d-d710-4407-8f03-431fe7c3f0f7",

                };
                await _iServiceProduct.AddProduct(produto);
                Assert.IsFalse(produto.Notitycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task AddProductComValidacaoCampoObrigatorio()
        {
            try
            {
                IProduct _iProduct = new RepositoryProduct();
                IServiceProduct _iServiceProduct = new ServiceProduct(_iProduct);

                var produto = new Produto
                {

                };
                await _iServiceProduct.AddProduct(produto);
                Assert.IsTrue(produto.Notitycoes.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task ListarProdutosUsuario()
        {
            try
            {
                IProduct _iProduct = new RepositoryProduct();

                var listaProdutos = await _iProduct.ListarProdutosUsuario("7af3595d-d710-4407-8f03-431fe7c3f0f7");
                Assert.IsTrue(listaProdutos.Any());
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task GetEntityById()
        {
            try
            {
                IProduct _iProduct = new RepositoryProduct();

                var listaProdutos = await _iProduct.ListarProdutosUsuario("7af3595d-d710-4407-8f03-431fe7c3f0f7");

                if (listaProdutos.Count != 0)
                {
                    var produto = await _iProduct.GetEntityById(listaProdutos.LastOrDefault().Id);

                    Assert.IsTrue(produto != null);
                }
                else
                    Assert.IsTrue(listaProdutos.Count == 0);
            }

            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public async Task Delete()
        {
            try
            {
                IProduct _iProduct = new RepositoryProduct();

                var listaProdutos = await _iProduct.ListarProdutosUsuario("7af3595d-d710-4407-8f03-431fe7c3f0f7");

                var ultimoProduto = listaProdutos.LastOrDefault();

                await _iProduct.Delete(ultimoProduto);

                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
