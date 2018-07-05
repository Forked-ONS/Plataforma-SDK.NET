using System;
using ONS.SDK.Worker;

namespace ONS.SDK.Builder 
{
    public interface IAppBuilder 
    {    
        IAppBuilder AddSDKBind<T>();
        
        IAppBuilder UseStartup<T>() where T : IStartup;

        IApp RunSDK();
    }
}