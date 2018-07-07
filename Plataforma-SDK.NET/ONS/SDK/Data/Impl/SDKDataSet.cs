using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Worker;

namespace ONS.SDK.Data.Impl
{
    public class SDKDataSet<T> : IDataSet<T> where T: BaseEntity
    {
        private readonly ICollection<EntityState<T>> _entities = new List<EntityState<T>>();
        private readonly string _typeName;

        public SDKDataSet(string typeName, ICollection<EntityState<T>> entities = null) 
        {
            if (string.IsNullOrEmpty(typeName)) {
                throw new SDKRuntimeException("Invalid typeName is null.");
            }
            
            _typeName = typeName;

            if (entities != null) {
                foreach(var it in entities) {
                    _entities.Add(it);
                }
            }            
        }

        public IEnumerable<IEntityState> EntitiesStates {
            get {
                return _entities;
            }
        }

        public IEnumerable<EntityState<T>> AllStates {
            get {
                return _entities;
            }
        }

        private EntityState<T> _findEntityState(string id) {
            return _entities.FirstOrDefault(it => string.Equals(it.Entity.Id, id));
        }

        public T FindById(string id) {
            
            if (string.IsNullOrEmpty(id)) {
                throw new SDKRuntimeException("Entity id is null.");
            }
            
            T retorno = null;

            var entityState = _findEntityState(id);
            if (entityState != null) {
                retorno = entityState.Entity;
            }

            return retorno;
        }

        public void Insert(T entity)
        {
            if (entity == null) {
                throw new SDKRuntimeException("System can't insert null entity.");
            }
            if (string.IsNullOrEmpty(entity.Id)) {
                throw new SDKRuntimeException("System can't insert entity with null id.");
            }

            var entityState = _findEntityState(entity.Id);

            if (entityState != null) {
                throw new SDKRuntimeException(
                    string.Format("Entity already exists in context. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name)
                );
            }
            
            entityState = new EntityState<T>() {
                Entity = entity,
                Metadata = new Metadata() {
                    // TODO precisa verificar as demais propriedades.
                    Type = _typeName,
                    ChangeTrack = ChangeTrack.CREATE
                }
            };

            _entities.Add(entityState);
        }

        public void Update(T entity)
        {
            if (entity == null) {
                throw new SDKRuntimeException("System can't update null entity.");
            }
            if (string.IsNullOrEmpty(entity.Id)) {
                throw new SDKRuntimeException("System can't update entity with null id.");
            }

            var entityState = _findEntityState(entity.Id);
            
            if (entityState == null) {
                throw new SDKRuntimeException(
                    string.Format("Entity not found. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            if (entityState.Metadata.ChangeTrack == ChangeTrack.CREATE) {
                throw new SDKRuntimeException(
                    string.Format("Entity already been in create state. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            entityState.Metadata.ChangeTrack = ChangeTrack.UPDATE;
        }

        public void Delete(T entity)
        {
            if (entity == null) {
                throw new SDKRuntimeException("System can't delete null entity.");
            }
            if (string.IsNullOrEmpty(entity.Id)) {
                throw new SDKRuntimeException("System can't delete entity with null id.");
            }

            var entityState = _findEntityState(entity.Id);
            
            if (entityState == null) {
                throw new SDKRuntimeException(
                    string.Format("Entity not found. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            if (entityState.Metadata.ChangeTrack == ChangeTrack.CREATE) {
                throw new SDKRuntimeException(
                    string.Format("Entity already been in create state. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            entityState.Metadata.ChangeTrack = ChangeTrack.DELETE;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _entities.Select(it => it.Entity).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
    }
}