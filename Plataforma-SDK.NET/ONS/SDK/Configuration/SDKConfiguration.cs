using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Worker;
using ONS.SDK.Context;

namespace ONS.SDK.Configuration
{
    public class SDKConfiguration
    {
        private static IDictionary<string, MethodInfo> _binds = new Dictionary<string, MethodInfo>();

        public static IServiceProvider ServiceProvider {get;set;}

        public static IDictionary<string, MethodInfo> Binds {
            get{
                return _binds;
            }
        }

        public static void Bind<T>() {

            // TODO validar tipo
            var type = typeof(T);
            var methodsEvt = _getMethodTypeEvent(type);

            if (!methodsEvt.Any()) {
                // TODO error
                throw new Exception(
                    string.Format("No method was found to attend some event for the informed type:[{0}].", type.FullName));
            }

            methodsEvt.ForEach(m => {
                
                var attr = (SDKEventAttribute) m.GetCustomAttributes(typeof(SDKEventAttribute), false).Single();

                _validateInclude(attr.EventName, m);

                _binds[attr.EventName] = m;
            });
        }

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

        private static void _validateInclude(string eventName, MethodInfo method) 
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

        public static void ValidateBinds() {
            // TODO validar os binds configurados com os descritos nos mapas
        }

    }
}