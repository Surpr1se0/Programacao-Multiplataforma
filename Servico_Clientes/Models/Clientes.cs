using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servico_Clientes.Models
{
    public class Clientes
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }

        public List<Clientes> Lista_Clientes { get; set; }

        


    }
}
