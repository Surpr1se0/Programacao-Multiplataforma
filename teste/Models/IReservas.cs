using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste.Models
{
    public interface IReservas
    {
        event MetodosSemParametros SalaReservada;

        void LeituraXML(string ficheiro);
        void EscritaXML(string ficheiro);
        void ReservarSala(string id);
    }
}
