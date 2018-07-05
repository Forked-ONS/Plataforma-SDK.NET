using System;
using Microsoft.Extensions.DependencyInjection;

namespace ONS.SDK.Builder {
    public interface IApp 
    {
        IServiceProvider ServiceProvider { get; }

        void Run ();

    }
}