using System;

namespace ProjetocSharp.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnoPublicacao { get; set; }
        public string Genero { get; set; }

        public Livro()
        {
        }

        public Livro(int id, string titulo, string autor, int anoPublicacao, string genero)
        {
            Id = id;
            Titulo = titulo ?? throw new ArgumentNullException(nameof(titulo));
            Autor = autor ?? throw new ArgumentNullException(nameof(autor));
            AnoPublicacao = anoPublicacao;
            Genero = genero ?? throw new ArgumentNullException(nameof(genero));
        }
    }
}