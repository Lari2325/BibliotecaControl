using System;
using System.Collections.Generic;
using System.Linq;
using ProjetocSharp.Models;

namespace ProjetocSharp.Services
{
    public class EmprestimoService
    {
        private readonly BibliotecaService _bibliotecaService;
        private readonly List<Emprestimo> _emprestimos;

        public EmprestimoService(BibliotecaService bibliotecaService)
        {
            _bibliotecaService = bibliotecaService ?? throw new ArgumentNullException(nameof(bibliotecaService));
            _emprestimos = new List<Emprestimo>();
        }

        public void AdicionarEmprestimo(Emprestimo emprestimo)
        {
            if (emprestimo == null)
                throw new ArgumentNullException(nameof(emprestimo));

            var livro = _bibliotecaService.BuscarLivroPorTitulo(emprestimo.Livro.Titulo);
            var usuario = _bibliotecaService.BuscarUsuarioPorEmail(emprestimo.Usuario.Email);

            if (livro == null)
                throw new ArgumentException("Livro não encontrado.");
            if (usuario == null)
                throw new ArgumentException("Usuário não encontrado.");

            _emprestimos.Add(emprestimo);
        }

        public Emprestimo BuscarEmprestimoPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID deve ser maior que zero.", nameof(id));
            return _emprestimos.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Emprestimo> ListarEmprestimos()
        {
            return _emprestimos;
        }

        public IEnumerable<Emprestimo> ListarEmprestimosPorUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            return _emprestimos.Where(e => e.Usuario.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Emprestimo> ListarEmprestimosPorLivro(Livro livro)
        {
            if (livro == null)
                throw new ArgumentNullException(nameof(livro));
            return _emprestimos.Where(e => e.Livro.Titulo.Equals(livro.Titulo, StringComparison.OrdinalIgnoreCase));
        }
    }
}