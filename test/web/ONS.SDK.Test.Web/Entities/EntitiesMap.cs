using ONS.SDK.Impl.Data;

namespace ONS.SDK.Test.Web.Entities
{
    public class EntitiesMap: AbstractDataMapCollection
    {
        protected override void Load() {
            BindMap<Conta>();
        }
    }
}