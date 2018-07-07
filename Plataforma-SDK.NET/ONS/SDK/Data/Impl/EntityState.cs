using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data.Impl
{
    public class EntityState<T>: IEntityState where T: BaseEntity
    {
        public EntityState() {}

        public EntityState(T entity, Metadata metadata) {
            Entity = entity;
            Metadata = metadata;
        }

        public T Entity {get;set;}

        public BaseEntity Enclosure {
            get {
                return Entity;
            }
        }

        public Metadata Metadata {get;set;}
    }
}