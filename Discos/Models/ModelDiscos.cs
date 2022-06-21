using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Discos.Models
{
    public class ModelDiscos 
    {
        //Atributos
        public Dictionary<string, Disco> Dados { get; set; }

        //Eventos
        public event MetodosSemParametros LeituraTerminada;
        public event MetodosSemParametros DiscoComprado;

        //Construtor
        public ModelDiscos()
        {
            Dados = new Dictionary<string, Disco>();
        }

        public void ComprarDisco(string id)
        {
            Disco di;

            Dados.TryGetValue(id, out di);
            if (di != null)
                di.Comprado = !di.Comprado;

            if (DiscoComprado != null)
                DiscoComprado();
        }

        public void EscritaTexto(string ficheiro)
        {
            StreamWriter sw = new StreamWriter(ficheiro);
            foreach(Disco di in Dados.Values)
            {
                string line = di.Id + ";" + di.Titulo + ";" + di.Autor + ";" + di.Ano + ";" + di.Preco + ";";

                if (di.Comprado)
                    line += "Comprado";
                else
                    line += "NaoComprado";

                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void EscritaXML(string ficheiro)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                            new XComment("Listagem dos Albuns"),
                            new XElement("Albuns", new XElement("Comprado"), new XElement("NaoComprado")));
            
            foreach(Disco di in Dados.Values)
            {
                XElement novo = new XElement("Album",
                                    new XAttribute("Id", di.Id),
                                    new XElement("Titulo", di.Titulo),
                                    new XElement("Autor", di.Autor),
                                    new XElement("Ano", di.Ano),
                                    new XElement("Preco", di.Preco));

                if (di.Comprado == true)
                    doc.Element("Albuns").Element("Comprado").Add(novo);
                else
                    doc.Element("Albuns").Element("NaoComprado").Add(novo);
            }
            doc.Save(ficheiro);
        }

        public void LeituraTexto(string ficheiro)
        {
            Dados.Clear();
            StreamReader sr = new StreamReader(ficheiro);

            while(!sr.EndOfStream)
            {
                string linha = sr.ReadLine();
                string[] campos = linha.Split(";");

                Disco di = new Disco(campos[0], campos[1], campos[2], campos[3], campos[4]);

                if (campos[5] == "Comprado")
                {
                    di.Comprado = true;
                }
                else
                    di.Comprado = false;

                Dados.Add(di.Id, di);
            }
            sr.Close();

            if (LeituraTerminada != null)
                LeituraTerminada();
        }

        public void LeituraXML(string ficheiro)
        {
            Dados.Clear();
            XDocument doc = XDocument.Load(ficheiro);

            var Registo = from al in doc.Element("Albuns").Element("Comprado").Descendants("Album")
                          select al;

            foreach(var campo in Registo)
            {
                Disco newDisco = new Disco(campo.Attribute("Id").Value,
                                          campo.Element("Titulo").Value,
                                          campo.Element("Autor").Value,
                                          campo.Element("Ano").Value,
                                          campo.Element("Preco").Value);
                newDisco.Comprado = true;

                Dados.Add(newDisco.Id, newDisco);
            }

            Registo = from al in doc.Element("Albuns").Element("NaoComprado").Descendants("Album")
                      select al;

            foreach(var campo in Registo)
            {
                Disco newDisco = new Disco(campo.Attribute("Id").Value,
                                            campo.Element("Titulo").Value,
                                            campo.Element("Autor").Value,
                                            campo.Element("Ano").Value,
                                            campo.Element("Preco").Value);
                newDisco.Comprado = false;

                Dados.Add(newDisco.Id, newDisco);
            }
            if (LeituraTerminada != null)
                LeituraTerminada();
        }
    }
}
