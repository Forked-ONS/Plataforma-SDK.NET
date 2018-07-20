using System;
using Microsoft.Extensions.DependencyInjection;

namespace ONS.SDK.Builder {

    /// <summary>
    /// Define uma interface de aplicação simples que suporta o SDK da Plataforma.
    /// 
    /// O SDK suporta ser plugado em qualquer aplication padrão dotnet core, através de extensios.
    /// 
    /// Este projeto pode ser utilizado em situações que não será utilizado um application dotnet em particular, 
    /// como por exemplo, projeto de console. Os projetos web padrões do .NET core, 
    /// já extendem um applicationbuilder web específico.
    /// </summary>
    public interface IApp 
    {
        /// <summary>
        /// Obtém o provedor de componentes definidos como dependência de IoC.
        /// </summary>
        /// <value>Provedor de componentes definidos em IoC</value>
        IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Executa o application do sistema.
        /// </summary>
        void Run ();

    }
}