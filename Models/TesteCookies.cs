using Xunit;
using System.Collections.Generic;
using Asp.netComBanco.Models;

namespace Cookie_Shop.Models
{
    public class TesteCookies
    {
        
        [Fact]
        public void CookiesPedido()
        {

            PedidoRepository pedidoRepository = new PedidoRepository();
            CookiesRepository cookiesRepository = new CookiesRepository();
            Cookie cookie = cookiesRepository.FindById(2);
            Assert.Equal( "Cookies de Morango", cookie.nome);
        }
    }
}