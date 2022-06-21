using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discos.Models
{
    public class Disco
    {
        public string Id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Ano { get; set; }

        public string Preco { get; set; }

        public Boolean Comprado { get; set; }

        public Disco(string id, string titulo, string autor, string ano, string preco)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            Ano = ano;
            Preco = preco;
            Comprado = false;
        }

        public Disco()
        {
            this.Id = Id;
            this.Titulo = Titulo;
            this.Autor = Autor;
            this.Ano = Ano;
            this.Preco = Preco;

        }
    }
}
