using System;

namespace ProjetocSharp.Models
{
    public class Emprestimo
    {
        public int Id { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public bool EmAtraso => DataDevolucao.HasValue && DataDevolucao.Value.Date < DateTime.Now.Date;

        // Construtor padrão
        public Emprestimo()
        {
        }

        public Emprestimo(int id, Livro livro, Usuario usuario, DateTime dataEmprestimo, DateTime? dataDevolucao = null)
        {
            Id = id;
            Livro = livro ?? throw new ArgumentNullException(nameof(livro));
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
            DataEmprestimo = dataEmprestimo;
            DataDevolucao = dataDevolucao;
        }
    }
}