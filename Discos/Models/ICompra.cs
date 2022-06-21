using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discos.Models
{
    public interface ICompra
    {
        event MetodosSemParametros LeituraTerminada;
        event MetodosSemParametros DiscoComprado;

        void EscritaTexto(string ficheiro);
        void LeituraTexto(string ficheiro);
        void LeituraXML(string ficheiro);
        void EscritaXML(string ficheiro);
        void ComprarDisco(string id);
    }
}
