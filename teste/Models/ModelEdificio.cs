using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace teste.Models
{
    public class ModelEdificio : IReservas
    {
        //Estrutura de Dados para Guardar os Alunos
        public List<Sala> Salas { get; set; }

        public string Edificio_ID { get; set; }

        //Eventos
        public event MetodosSemParametros SalaReservada;
        public event MetodosSemParametros LeituraTerminada;

        //Construtor
        public ModelEdificio()
        {
            Salas = new List<Sala>();
            Edificio_ID = string.Empty;
        }

        public void EscritaXML(string ficheiro)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                        new XElement("Edificio", new XElement("Ocupadas"), new XElement("NaoOcupadas")));

            foreach(Sala sal in Salas)
            {
                //Criar estrutura para as salas
                XElement novo = new XElement("Sala", new XAttribute("Piso", sal.Piso),
                                                      new XAttribute("ID", sal.Id),
                                                      new XElement("Capacidade", sal.Capacidade));

                if (sal.Ocupada == true)
                    doc.Element("Ocupadas").Add(novo);
                else
                    doc.Element("NaoOcupadas").Add(novo);
            }

            doc.Save(ficheiro);
        }

        public void LeituraXML(string ficheiro)
        {
            Salas.Clear();
            XDocument doc = XDocument.Load(ficheiro);

            //Salas Ocupadas
            var Registo = from al in doc.Element("Edificio").Element("Ocupadas").Descendants("Sala")
                          select al;

            foreach(var campo in Registo)
            {
                //Criar objeto com os alunos respetivos
                Sala newSala = new Sala(campo.Attribute("Piso").Value, campo.Attribute("ID").Value,
                                        campo.Element("Capacidade").Value);

                newSala.Ocupada = true;

                //Adicionar à estrutura
                Salas.Add(newSala);
            }

            //Salas não ocupadas
            Registo = from al in doc.Element("Edificio").Element("NaoOcupadas").Descendants("Sala")
                      select al;

            foreach(var campo in Registo)
            {
                Sala newSala = new Sala(campo.Attribute("Piso").Value, campo.Attribute("ID").Value,
                                campo.Element("Capacidade").Value);

                newSala.Ocupada = false;

                //Adicionar à estrutura
                Salas.Add(newSala);
            }

            //Lançamento do evento
            if (LeituraTerminada != null)
                LeituraTerminada();
        }

        public void ReservarSala(string id)
        {
            Sala sal;

            foreach(var item in Salas)
            {
                if (item.Id == id)
                    item.Ocupada = !item.Ocupada;
            }

            if (SalaReservada != null)
                SalaReservada();
        }
    }
}
