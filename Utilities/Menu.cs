using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading; 
using ProjetocSharp.Models;
using ProjetocSharp.Services;

namespace ProjetocSharp.Utilities
{
    public class Menu
    {
        private readonly EmprestimoService _emprestimoService;
        private readonly BibliotecaService _bibliotecaService;

        public Menu(EmprestimoService emprestimoService, BibliotecaService bibliotecaService)
        {
            _emprestimoService = emprestimoService ?? throw new ArgumentNullException(nameof(emprestimoService));
            _bibliotecaService = bibliotecaService ?? throw new ArgumentNullException(nameof(bibliotecaService));
        }

        public void ExibirMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Biblioteca");
                Console.WriteLine("1. Adicionar Empréstimo");
                Console.WriteLine("2. Listar Empréstimos");
                Console.WriteLine("3. Buscar Empréstimo por ID");
                Console.WriteLine("4. Listar Empréstimos por Usuário");
                Console.WriteLine("5. Listar Empréstimos por Livro");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");

                var opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        AdicionarEmprestimo();
                        break;
                    case "2":
                        ListarEmprestimos();
                        break;
                    case "3":
                        BuscarEmprestimoPorId();
                        break;
                    case "4":
                        ListarEmprestimosPorUsuario();
                        break;
                    case "5":
                        ListarEmprestimosPorLivro();
                        break;
                    case "0":
                        return;
                    default:
                        MostrarMensagem("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private void AdicionarEmprestimo()
        {
            Console.Write("Digite o título do livro: ");
            var titulo = Console.ReadLine();
            var livro = _bibliotecaService.BuscarLivroPorTitulo(titulo);

            if (livro == null)
            {
                MostrarMensagem("Livro não encontrado.");
                return;
            }

            Console.Write("Digite o e-mail do usuário: ");
            var email = Console.ReadLine();
            var usuario = _bibliotecaService.BuscarUsuarioPorEmail(email);

            if (usuario == null)
            {
                MostrarMensagem("Usuário não encontrado.");
                return;
            }

            var emprestimo = new Emprestimo
            {
                Livro = livro,
                Usuario = usuario,
                DataEmprestimo = DateTime.Now
            };

            _emprestimoService.AdicionarEmprestimo(emprestimo);
            MostrarMensagem("Empréstimo adicionado com sucesso.");
        }

        private void ListarEmprestimos()
        {
            var emprestimos = _emprestimoService.ListarEmprestimos();

            if (!emprestimos.Any())
            {
                MostrarMensagem("Nenhum empréstimo encontrado.");
                return;
            }

            foreach (var emprestimo in emprestimos)
            {
                Console.WriteLine($"ID: {emprestimo.Id}, Livro: {emprestimo.Livro.Titulo}, Usuário: {emprestimo.Usuario.Nome}, Data: {emprestimo.DataEmprestimo}");
            }
            MostrarMensagem("Pressione qualquer tecla para continuar...");
        }

        private void BuscarEmprestimoPorId()
        {
            Console.Write("Digite o ID do empréstimo: ");
            if (int.TryParse(Console.ReadLine(), out var id))
            {
                var emprestimo = _emprestimoService.BuscarEmprestimoPorId(id);

                if (emprestimo == null)
                {
                    MostrarMensagem("Empréstimo não encontrado.");
                }
                else
                {
                    Console.WriteLine($"ID: {emprestimo.Id}, Livro: {emprestimo.Livro.Titulo}, Usuário: {emprestimo.Usuario.Nome}, Data: {emprestimo.DataEmprestimo}");
                }
            }
            else
            {
                MostrarMensagem("ID inválido.");
            }
        }

        private void ListarEmprestimosPorUsuario()
        {
            Console.Write("Digite o e-mail do usuário: ");
            var email = Console.ReadLine();
            var usuario = _bibliotecaService.BuscarUsuarioPorEmail(email);

            if (usuario == null)
            {
                MostrarMensagem("Usuário não encontrado.");
                return;
            }

            var emprestimos = _emprestimoService.ListarEmprestimosPorUsuario(usuario);

            if (!emprestimos.Any())
            {
                MostrarMensagem("Nenhum empréstimo encontrado para este usuário.");
                return;
            }

            foreach (var emprestimo in emprestimos)
            {
                Console.WriteLine($"ID: {emprestimo.Id}, Livro: {emprestimo.Livro.Titulo}, Data: {emprestimo.DataEmprestimo}");
            }
            MostrarMensagem("Pressione qualquer tecla para continuar...");
        }

        private void ListarEmprestimosPorLivro()
        {
            Console.Write("Digite o título do livro: ");
            var titulo = Console.ReadLine();
            var livro = _bibliotecaService.BuscarLivroPorTitulo(titulo);

            if (livro == null)
            {
                MostrarMensagem("Livro não encontrado.");
                return;
            }

            var emprestimos = _emprestimoService.ListarEmprestimosPorLivro(livro);

            if (!emprestimos.Any())
            {
                MostrarMensagem("Nenhum empréstimo encontrado para este livro.");
                return;
            }

            foreach (var emprestimo in emprestimos)
            {
                Console.WriteLine($"ID: {emprestimo.Id}, Usuário: {emprestimo.Usuario.Nome}, Data: {emprestimo.DataEmprestimo}");
            }
            MostrarMensagem("Pressione qualquer tecla para continuar...");
        }

       private void MostrarMensagem(string mensagem)
        {
            Console.Clear();
            Console.WriteLine(mensagem);
            Thread.Sleep(5000); 
        }
    }
}