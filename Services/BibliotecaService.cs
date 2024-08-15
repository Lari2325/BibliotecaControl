using System;
using System.Collections.Generic;
using System.Linq;
using ProjetocSharp.Models;

namespace ProjetocSharp.Services
{
    public class BibliotecaService
    {
        private readonly List<Livro> _livros;
        private readonly List<Usuario> _usuarios;

        public BibliotecaService()
        {
            _livros = new List<Livro>();
            _usuarios = new List<Usuario>();
        }

        public void AdicionarLivro(Livro livro)
        {
            if (livro == null)
                throw new ArgumentNullException(nameof(livro));
            if (_livros.Any(l => l.Titulo.Equals(livro.Titulo, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Livro já existe na biblioteca.");

            _livros.Add(livro);
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));
            if (_usuarios.Any(u => u.Email.Equals(usuario.Email, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Usuário já existe na biblioteca.");

            _usuarios.Add(usuario);
        }

        public Livro BuscarLivroPorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                throw new ArgumentException("Título não pode ser nulo ou vazio.", nameof(titulo));

            return _livros.FirstOrDefault(l => l.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }

        public Livro BuscarLivroPorId(int id)
        {
            return _livros.FirstOrDefault(l => l.Id == id);
        }

        public Usuario BuscarUsuarioPorEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser nulo ou vazio.", nameof(email));

            return _usuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Usuario BuscarUsuarioPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Livro> ListarLivros()
        {
            return _livros;
        }

        public IEnumerable<Usuario> ListarUsuarios()
        {
            return _usuarios;
        }
    }
}