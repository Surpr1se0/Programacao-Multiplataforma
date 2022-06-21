using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste.Models
{
    public class Sala
    {
        public string Id { get; set; }

        public string Capacidade { get; set; }

        public string Piso { get; set; }

        public Boolean Ocupada { get; set; }

        public Sala(string id, string capacidade, string piso)
        {
            Id = id;
            Capacidade = capacidade;
            Piso = piso;
            Ocupada = false;
        }
    }
}
