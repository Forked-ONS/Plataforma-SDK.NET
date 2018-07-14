using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using System.Threading;
using Microsoft.Extensions.Configuration;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data;
using ONS.SDK.Services;
using ONS.SDK.Services.Domain;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Newtonsoft.Json;
using ONS.SDK.Domain.ProcessMemmory;
using ONS.SDK.Data.Persistence;
using ONS.SDK.Worker;

namespace ONS.SDK.Impl.Worker
{
    public static class WorkerHelper
    {
        public static object GetRunner(Type type) {
            if (SDKConfiguration.ServiceProvider == null) {
                throw new SDKRuntimeException("ServiceProvider not configurated in SDKConfiguration, verify configuration.");
            }
            return SDKConfiguration.ServiceProvider.GetService(type);
        }

        public static object[] ResolveArgs(MethodInfo methodInfo, IContext context) 
        {    
            var retorno = new List<object>();
            var parameters = methodInfo.GetParameters();

            foreach(var parameter in parameters) {
                var type = parameter.ParameterType;

                if (typeof(IEvent).IsAssignableFrom(type)) {
                    retorno.Add(context.GetEvent());
                }
                else if (typeof(IPayload).IsAssignableFrom(type)) {
                    retorno.Add(context.GetEvent().GetPayload());
                }
                else if (typeof(IContext).IsAssignableFrom(type)) {
                    retorno.Add(context);
                }
                else if (typeof(IDataContext).IsAssignableFrom(type)) {
                    retorno.Add(context.DataContext);
                }
                else if (typeof(IDataSet).IsAssignableFrom(type)) {
                    
                    var methodSet = context.DataContext.GetType().GetMethods()
                        .FirstOrDefault(m => m.Name == "Set" && m.IsGenericMethod);
                    
                    IDataSet dataSet = null;
                    if (methodSet != null) {
                        var typeGeneric = type.GetGenericArguments();
                        methodSet = methodSet.MakeGenericMethod(typeGeneric);
                        dataSet = (IDataSet) methodSet.Invoke(context.DataContext, new object[0]);
                    }
                    retorno.Add(dataSet);
                }
            }

            return retorno.ToArray();
        }

    }
}