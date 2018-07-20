using System;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Worker;

namespace ONS.SDK.Builder 
{
    /// <summary>
    /// Define a interface de construção de application com suporte o SDK da Plataforma.
    /// </summary>
    public interface IAppBuilder 
    {    
        /// <summary>
        /// Registra classes de tipos que definem métodos de serviços que atendem
        /// a eventos do sistema.
        ///
        /// Os métodos da classe devem ser decorados com o tipo SDKEventAttribute.
        /// Ex: [SDKEvent("<eventName>")] public void <MethodName>() {}
        /// 
        /// </summary>
        /// <typeparam name="T">Tipo da classe que define os métodos que atendem os serviços do sistema.</typeparam>
        /// <returns>Próprio construtor da aplicação.</returns>
        IAppBuilder BindEvents<T>();
        
        /// <summary>
        /// Indica a classe que será utilizada para inicialização dos recursos do sistema, incluindo do SDK.
        /// Registra os componentes no framework de injeção de dependência.
        /// </summary>
        /// <typeparam name="T">Define a classe de inicialização.</typeparam>
        /// <returns>Próprio construtor da aplicação.</returns>
        IAppBuilder UseStartup<T>() where T : IStartup;

        /// <summary>
        /// Registra uma classe de mapeamento para uma entidade do mapa.
        /// Mapa é a configuração de mapeamento de dados das entidades de domínio para o microserviço.
        /// </summary>
        /// <param name="mapName">Nome da entidade no mapa.</param>
        /// <typeparam name="T">Tipo da classe que representa a entidade do mapa.</typeparam>
        /// <returns>Construtor da aplicação.</returns>
        IAppBuilder BindDataMap<T>(string mapName) where T: Model;

        /// <summary>
        /// Registra uma classe de mapeamento para uma entidade do mapa.
        /// Mapa é a configuração de mapeamento de dados das entidades de domínio para o microserviço.
        /// Como não é passado o nome da entidade no mapa, a classe precisa ser decorada 
        /// com o attribute: "DataMapAttribute".
        /// </summary>
        /// <typeparam name="T">Tipo da classe que representa a entidade do mapa.</typeparam>
        /// <returns>Construtor da aplicação.</returns>
        IAppBuilder BindDataMap<T>() where T: Model;

        /// <summary>
        /// Indica a classe com coleção de classes de mapeamento de entidades do mapa.
        /// Mapa é a configuração de mapeamento de dados das entidades de domínio para o microserviço.
        /// </summary>
        /// <typeparam name="T">Tipo da classe que fornece uma coleção de classes de mapeamento.</typeparam>
        /// <returns>Construtor da aplicação.</returns>
        IAppBuilder UseDataMap<T>() where T: IDataMapCollection;

        /// <summary>
        /// Cria o application que está sendo construído pelo builder, e executa o SDK conforme parâmetros recebidos.
        /// </summary>
        /// <returns>Application construído durante a execução do SDK.</returns>
        IApp RunSDK();
    }
}