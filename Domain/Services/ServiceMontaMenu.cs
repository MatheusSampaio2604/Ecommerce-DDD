using Domain.Interfaces.InterfaceServices;
using Domain.Interfaces.InterfaceUsuario;
using Entities.Entities;
using Entities.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMontaMenu : IServiceMontaMenu
    {
        private readonly IUsuario _iUsuario;

        public ServiceMontaMenu(IUsuario iUsuario)
        {
            _iUsuario = iUsuario;
        }


        public async Task<List<MenuSite>> MontaMenuPorPerfil(string userId)
        {
            List<MenuSite> retorno = new();

            retorno.Add(new MenuSite { Controller = "Home", Action = "Index", Descricao = "Loja Virtual" });
            if (!String.IsNullOrWhiteSpace(userId))
            {
                retorno.Add(new MenuSite { Controller = "Produtos", Action = "Index", Descricao = "Meus Produtos" });
                retorno.Add(new MenuSite { Controller = "CompraUsuario", Action = "MinhasCompras", Descricao = "Minhas Compras" });

                var usuario = await _iUsuario.ObterUsuarioPeloId(userId);
                if(usuario != null && usuario.Tipo != null)
                {
                    switch ((TipoUsuario)usuario.Tipo)
                    {
                        case TipoUsuario.Administrador:
                            retorno.Add(new MenuSite { Controller = "LogSistemas", Action = "Index", Descricao = "LOG" });
                            retorno.Add(new MenuSite { Controller = "Usuarios", Action = "ListarUsuarios", Descricao = "Controle Usuários" });
                            break;
                        case TipoUsuario.Comum:

                            break;
                        default:
                            break;
                    }
                }

                retorno.Add(new MenuSite { Controller = "Produtos", Action = "ListarProdutosCarrinhoUsuario", Descricao = "", IdCampo = "qtdCarrinho", UrlImagem = "../img/carrinho.png" });
            
            }



            return retorno;
        }
    }
}
