using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _11e12.Models
{
    public class ModelInscricoes
    {
        //Estrutura de Dados para Guardar os Alunos
        public Dictionary<string, Alunos> Dados { get; set; }

        //Eventos
        public event MetodosSemParametros LeituraTerminada;
        public event MetodosSemParametros InscricaoAtualizada;

        //Construtor
        public ModelInscricoes()
        {
            Dados = new Dictionary<string, Alunos>();
        }

        //Métodos
        public void LeituraFicheiroTxt(string ficheiro)
        {
            Dados.Clear();
            StreamReader sr = new StreamReader(ficheiro);

            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] campos = line.Split(';');

                Alunos al = new Alunos(campos[0], campos[1], campos[2]);

                if(campos[3] == "Inscrito")
                {
                    al.Inscrito = true;
                }
                else
                    al.Inscrito= false;

                //Adicionar Aluno ao Dicionario
                Dados.Add(al.Numero, al);
            }
            sr.Close();

            //Lança Notificação do Dicionário
            if (LeituraTerminada != null)
                LeituraTerminada();
        }

        public void LeituraXML(string ficheiro)
        {
            Dados.Clear();
            XDocument doc = XDocument.Load(ficheiro);

            //Efetuar a consulta usando o LINQ

            //Alunos Inscritos
            var Registo = from al in doc.Element("Alunos").Element("Inscritos").Descendants("Aluno")
                          select al;

            foreach(var campo in Registo)
            {
                //criar objeto aluno com os dados respetivos
                Alunos newAluno = new Alunos(campo.Attribute("Numero").Value,
                                            campo.Element("Nome").Value,
                                            campo.Element("Curso").Value);

                newAluno.Inscrito = true;

                //Adicionar Aluno ao Dicionário
                Dados.Add(newAluno.Numero, newAluno);
            }

            //Alunos Nao Inscritos
            Registo = from al in doc.Element("Alunos").Element("NaoInscrito").Descendants("Aluno")
                      select al;

            foreach(var campo in Registo)
            {
                //Criar objeto aluno com os dados respetivos
                Alunos newAluno = new Alunos(campo.Attribute("Numero").Value,
                                            campo.Element("Nome").Value,
                                            campo.Element("Curso").Value);

                newAluno.Inscrito = false;

                //Adicionar Aluno ao Dicionário
                Dados.Add(newAluno.Numero, newAluno);
            }

            if (LeituraTerminada != null)
                LeituraTerminada();
        }

        public void EscritaFicheiroTxt(string ficheiro)
        {
            StreamWriter sw = new StreamWriter(ficheiro);
            foreach(Alunos al in Dados.Values)
            {
                string line = al.Numero + ";" + al.Nome + ";" + al.Curso + ";";

                if (al.Inscrito)
                    line += "Inscrito";
                else
                    line += "NaoInscrito";

                sw.WriteLine(line);
            }
            sw.Close();
        }

        public void EscritaXML(string ficheiro)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.ToString(), "yes"),
                                        new XComment("Listagem dos Alunos"),
                                        new XElement("Alunos", new XElement("Inscritos"), new XElement("NaoInscritos")));

            foreach(Alunos al in Dados.Values)
            {
                //criar estrutura xml para os alunos
                XElement novo = new XElement("Aluno", new XAttribute("Numero", al.Numero),
                                                      new XElement("Nome", al.Nome),
                                                      new XElement("Curso", al.Curso));

                if (al.Inscrito == true)
                    doc.Element("Alunos").Element("Inscritos").Add(novo);
                else
                    doc.Element("Alunos").Element("NaoInscritos").Add(novo);
            }

            doc.Save(ficheiro);
        }

        public void AlteraInscricao(string numero)
        {
            Alunos al;

            //Pesquisar Aluno no dicionário pelo Número da coleção
            Dados.TryGetValue(numero, out al);

            if (al != null)
                al.Inscrito = !al.Inscrito;

            if (InscricaoAtualizada != null)
                InscricaoAtualizada();
        }
    }
}
