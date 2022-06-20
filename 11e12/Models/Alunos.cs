using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11e12.Models
{
    public class Alunos
    {
        //Atributos da classe Aluno
        public string Numero { get; set; }

        public string Nome { get; set; }

        public string Curso { get; set; }

        public Boolean Inscrito { get; set; }

        //Construtor
        public Alunos (string numero, string nome, string curso)
        {
            Numero = numero;
            Nome = nome;
            Curso = curso;
            Inscrito = false;
        }
    }
}
