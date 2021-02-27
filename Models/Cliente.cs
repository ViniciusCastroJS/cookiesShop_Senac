using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookie_Shop.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string CPF { get; set; }
    }
}
