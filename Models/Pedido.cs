using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookie_Shop.Models
{
    public class Pedido
    {
        public int id { get; set; }
        public int idUsuario { get; set; }
        public int idCookie { get; set; }
        public int quantidade { get; set; }
        public bool pago { get; set; }

    }
}
