using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ONS.SDK.Builder {
    
    /// <summary>
    /// Classe de inicialização da aplicação básica com suporte a SDK da plataforma.
    /// </summary>
    public interface IStartup 
    {
        /// <summary>
        /// Configura a aplicação básica com suporte a SDK.
        /// </summary>
        /// <param name="app">Construtor da Aplicação básica com suporte do SDK.</param>
        void Configure(IAppBuilder app);

        /// <summary>
        /// Configura todos os serviços para injeção de dependência.
        /// </summary>
        /// <param name="services">Serviços para configuração de injeção de dependência.</param>
        void ConfigureServices(IServiceCollection services);
        
    }
}