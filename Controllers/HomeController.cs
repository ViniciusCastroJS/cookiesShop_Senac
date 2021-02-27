using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cookie_Shop.Models;
using Asp.netComBanco.Models;

namespace Cookie_Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CookiesRepository Database = new CookiesRepository();
            List<Cookie> cookies = Database.Listagem();
            return View("Index", cookies);
        }

        public IActionResult Detalhes(int id)
        {
            CookiesRepository cookiesRepository = new CookiesRepository();
            Cookie cookie = cookiesRepository.FindById(id);
            return View("Detalhes", cookie);
        }

        public IActionResult Carrinho(int user) 
        {
            CookiesRepository cookiesRepository = new CookiesRepository();
            List<Cookie> cookie = cookiesRepository.Carrinho(user);

            return View("Carrinho", cookie);
        }

        public IActionResult FazerPedido(int id, int user)
        {
            PedidoRepository pedidoRepository = new PedidoRepository();
            CookiesRepository cookiesRepository = new CookiesRepository();
            Cookie cookie = cookiesRepository.FindById(id);
            pedidoRepository.FazerPedido(cookie.id, user);

            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
