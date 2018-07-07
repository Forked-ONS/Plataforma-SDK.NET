using System;
using ONS.SDK.Data;
using ONS.SDK.Worker;

namespace ONS.SDK.Builder 
{
    public interface IAppBuilder 
    {    
        IAppBuilder BindEvents<T>();
        
        IAppBuilder UseStartup<T>() where T : IStartup;

        IAppBuilder BindDataMap<T>(string mapName);

        IAppBuilder BindDataMap<T>();

        IAppBuilder UseDataMap<T>() where T: IDataMapCollection;

        IApp RunSDK();
    }
}