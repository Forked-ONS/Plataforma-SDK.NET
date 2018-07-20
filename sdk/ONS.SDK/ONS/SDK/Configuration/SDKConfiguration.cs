using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Worker;
using ONS.SDK.Context;

namespace ONS.SDK.Configuration
{
    /// <summary>
    /// Mantém as configurações de eventos do SDK.
    /// Configrações de Binds de Eventos para métodos de negócio do micro-serviço.
    /// </summary>
    public class SDKConfiguration
    {
        /// <summary>
        /// Nome da origem do SDK que está executando a funcionalidade. 
        /// </summary>
        public static string AppOriginSDK = "sdk_dotnet";

        /// <summary>
        /// Lista de Binds de métodos de negócio registrado para atender eventos do sistema.
        /// </summary>
        /// <typeparam name="string">Nome do evento de sistema.</typeparam>
        /// <typeparam name="MethodInfo">Método de negócio responsável por responder ao evento.</typeparam>
        /// <returns></returns>
        private static IDictionary<string, MethodInfo> _binds = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// Obtém o provedor de componentes definidos como dependência de IoC.
        /// </summary>
        /// <value>Provedor de componentes definidos em IoC</value>
        public static IServiceProvider ServiceProvider {get;set;}

        public static IDictionary<string, MethodInfo> Binds {
            get{
                return _binds;
            }
        }

        /// <summary>
        /// Realiza a configuração dos métodos definidos em uma classe para atender  
        /// eventos do sistema.
        /// Esse registro depende de configuração de evento do método, 
        /// utilizando o attribute SDKEventAttribute.  
        /// </summary>
        /// <typeparam name="T">Tipo da classe com os métodos que respondem à eventos.</typeparam>
        public static void BindEvents<T>() {

            var type = typeof(T);
            var methodsEvt = _getMethodTypeEvent(type);

            if (!methodsEvt.Any()) {
                throw new BadConfigException(
                    string.Format("No method was found to attend some event for the informed type:[{0}].", type.FullName));
            }

            methodsEvt.ForEach(m => {
                
                var attr = (SDKEventAttribute) m.GetCustomAttributes(typeof(SDKEventAttribute), false).Single();

                _validateIncludeBindEvent(attr.EventName, m);

                _binds[attr.EventName] = m;
            });
        }

        /// <summary>
        /// Indica o tipo da classe do payload do evento de sistema.
        /// Responsável por passar parâmetros do evento.
        /// </summary>
        /// <param name="eventName">Nome do evento de sistema.</param>
        /// <returns>Tipo da classe de parâmetros do evento.</returns>
        public static Type GetTypePayload(string eventName) 
        {
            Type retorno = null;

            if (!string.IsNullOrEmpty(eventName)) {
                
                var methodEvent = GetMethodByEvent(eventName);

                var paramPayload = methodEvent.GetParameters()
                    .FirstOrDefault(p => typeof(IPayload).IsAssignableFrom(p.ParameterType));

                var paramEvent = methodEvent.GetParameters()
                    .FirstOrDefault(p => typeof(IEvent).IsAssignableFrom(p.ParameterType));

                var paramContext = methodEvent.GetParameters()
                    .FirstOrDefault(p => typeof(IContext).IsAssignableFrom(p.ParameterType));

                if (paramPayload != null) {
                    retorno = paramPayload.ParameterType;
                } 
                else if (paramEvent != null) {
                    retorno = paramEvent.ParameterType.GenericTypeArguments.FirstOrDefault();
                }
                else if (paramContext != null) {
                    retorno = paramContext.ParameterType.GenericTypeArguments.FirstOrDefault();
                }
            }
            return retorno;
        }

        /// <summary>
        /// Obtém o método de negócio responsável por atender um evento do sistema.
        /// </summary>
        /// <param name="eventName">Nome do evento de sistema.</param>
        /// <returns>Método de negócio responsável por atender um evento do sistema.</returns>
        public static MethodInfo GetMethodByEvent(string eventName) 
        {
            MethodInfo retorno = null;

            if (_binds.ContainsKey(eventName)) {
                retorno = _binds[eventName];
            } 
            else if (_binds.ContainsKey(SDKEventAttribute.DefaultEvent)) {
                retorno = _binds[SDKEventAttribute.DefaultEvent];
            }
            else {
                // TODO criar tipo de exceção
                throw new Exception(
                    string.Format("Not found bind method to event received. Event: {0}", eventName)
                );
            }

            return retorno;
        }

        private static List<MethodInfo> _getMethodTypeEvent(Type type)
        {
            return type.GetMethods().Where(m => m.GetCustomAttributes(
                typeof(SDKEventAttribute), false
            ).Length > 0).ToList();
        }

        private static void _validateIncludeBindEvent(string eventName, MethodInfo method) 
        {
            if (_binds.Any() && _binds.ContainsKey(SDKEventAttribute.DefaultEvent)) {

                var msg = string.Format(
                    "The system can only be a method set as default. "+
                    "Found event: {0}.{1}, Attempted event: {2}.{3}",
                    _binds[SDKEventAttribute.DefaultEvent].DeclaringType, 
                    _binds[SDKEventAttribute.DefaultEvent].Name,
                    method.DeclaringType,
                    method.Name
                );

                throw new BadConfigException(msg);
            }
            else if (_binds.ContainsKey(eventName)) {

                var msg = string.Format(
                    "The event already configured for the system. Event: {0}, "+
                    "Found event: {1}.{2}, Attempted event: {3}.{4}",
                    eventName,
                    _binds[eventName].DeclaringType, 
                    _binds[eventName].Name,
                    method.DeclaringType,
                    method.Name
                );

                throw new BadConfigException(msg);
            }
        }

    }
}