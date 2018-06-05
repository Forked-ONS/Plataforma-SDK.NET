using System;
using ONS.SDK.Domain.Interfaces;

namespace ONS.SDK.App.Interfaces {
    public interface IAppBuilder {
        IApp Build ();
        IAppBuilder UseDomain<T> () where T : class, IDomainContext;
        IAppBuilder UseEventPayloadType<T> ();

        IAppBuilder UseStartup<T> () where T : class;

        IAppBuilder UseRunner<TRunner> () where TRunner : class, IRunnable;

    }
}