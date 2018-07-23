using System.Collections.Generic;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Services
{
    /// <summary>
    /// Define os métodos básicos para atender serviços de uma entidade do core.
    /// </summary>
    /// <typeparam name="T">Tipo de entidade do core relacionado ao serviço</typeparam>
    public interface ICoreService<T>
    {
         void Save (List<Model> entities);

        void Save (Model entity);

        List<T> Find(Criteria criteria);

        List<T> FindByName(string name);

        List<T> FindBySystemId(string id);

        List<T> FindByProcessId(string id);

        List<T> FindById(string id);
    }
}