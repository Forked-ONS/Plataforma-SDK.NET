using System.Collections.Generic;
using System.Linq;
using ONS.SDK.Context;
using ONS.SDK.Domain.Base;
using ONS.SDK.Domain.Core;
using ONS.SDK.Services;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Services.Domain {
    public interface IDomainService {
        
        IDomainService OnBranch(string branch);

        T FindById<T>(string map, string type, string id) where T: Model;

        List<T> Query<T>(string map, string type, string filterName = null, 
            IDictionary<string, object> filters = null) where T: Model;

        List<T> QueryByFilter<T>(Filter filter) where T: Model;

        void Persist(string map, IList<Model> entities);
        
    }
}