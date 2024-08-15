using System;

namespace ProjetocSharp.Models
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Usuario()
        {
        }

        public Usuario(int id, string nome, string email, string telefone)
        {
            Id = id;
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
        }
    }
}