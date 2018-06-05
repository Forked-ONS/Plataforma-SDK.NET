using System;
using Microsoft.Extensions.DependencyInjection;

namespace ONS.SDK.App.Interfaces {
    public interface IApp {
        void Run ();

        void AddServiceContainer (IServiceCollection services);

        IServiceProvider Provider { get; }
    }
}