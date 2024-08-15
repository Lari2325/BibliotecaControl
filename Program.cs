using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetocSharp.Services;
using ProjetocSharp.Utilities;

class Program
{
    static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        
        // Resolve Menu e inicia a interação com o usuário
        var menu = host.Services.GetRequiredService<Menu>();
        menu.ExibirMenu();
        
        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Registre todos os serviços necessários
                services.AddSingleton<BibliotecaService>();
                services.AddSingleton<EmprestimoService>(provider =>
                {
                    var bibliotecaService = provider.GetRequiredService<BibliotecaService>();
                    return new EmprestimoService(bibliotecaService);
                });
                services.AddSingleton<Menu>();
            });
}