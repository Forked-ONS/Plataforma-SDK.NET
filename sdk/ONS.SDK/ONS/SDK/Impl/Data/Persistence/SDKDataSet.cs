using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Data;
using ONS.SDK.Domain.Base;
using ONS.SDK.Worker;
using ONS.SDK.Configuration;
using ONS.SDK.Context;
using ONS.SDK.Data.Persistence;

namespace ONS.SDK.Impl.Data.Persistence
{
    public class SDKDataSet<T> : IDataSet<T> where T: Model
    {
        private readonly IList<T> _entities = new List<T>();
        private readonly string _typeName;

        public SDKDataSet(string typeName, ICollection<T> entities = null) 
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

        public string MapName {
            get {
                return _typeName;
            }
        }

        public IList<Model> AllEntities {
            get {
                return _entities.Cast<Model>().ToList();
            }
        }

        private ExecutionParameter _executionParameter {
            get {
                return ((IExecutionContext) SDKConfiguration.ServiceProvider.GetService(typeof(IExecutionContext)))
                    .ExecutionParameter;
            }
        }

        public IList<Model> TrackingEntities {
            get {
                return _entities.Where(it => ChangeTrack.IsTracking(it._Metadata.ChangeTrack))
                    .Select(it => (Model)it)
                    .ToList();
            }
        }

        public T FindById(string id) {
            
            if (string.IsNullOrEmpty(id)) {
                throw new SDKRuntimeException("Entity id is null.");
            }
            
            return _entities.FirstOrDefault(it => string.Equals(it.Id, id));
        }

        private int _findIndexOfById(string id) 
        {    
            int retorno = -1;
            for (int i = 0; i < _entities.Count; i++)
            {
                if (string.Equals(_entities[i].Id, id)) {
                    retorno = i;
                    break;
                }
            }
            
            return retorno;
        }

        public void Insert(T entity)
        {
            if (entity == null) {
                throw new SDKRuntimeException("System can't insert null entity.");
            }
            
            if (!string.IsNullOrEmpty(entity.Id)) {

                var entityPersistent = FindById(entity.Id);

                if (entityPersistent != null) {
                    throw new SDKRuntimeException(
                        string.Format("Entity already exists in context. Entity.Id={0}, Entity.Type={1}", 
                        entity.Id, entity.GetType().Name)
                    );
                }
            } else {
                entity.Id = null;
            }

            entity._Metadata = new Metadata() {
                Type = _typeName,
                Branch = _executionParameter.Branch,
                ChangeTrack = ChangeTrack.CREATE
            };

            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            if (entity == null) {
                throw new SDKRuntimeException("System can't update null entity.");
            }
            if (string.IsNullOrEmpty(entity.Id)) {
                throw new SDKRuntimeException("System can't update entity with null id.");
            }
            
            var indexEntity = _findIndexOfById(entity.Id);

            if (indexEntity < 0) {
                throw new SDKRuntimeException(
                    string.Format("Entity not found. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            var entityPersistent = _entities[indexEntity];

            if (entityPersistent._Metadata.ChangeTrack == ChangeTrack.CREATE) {
                throw new SDKRuntimeException(
                    string.Format("Entity already been in create state. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            entity._Metadata = entityPersistent._Metadata;
            entity._Metadata.ChangeTrack = ChangeTrack.UPDATE;
            _entities[indexEntity] = entity;            
        }

        public void DeleteById(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                throw new SDKRuntimeException("System can't delete entity with null id.");
            }

            var entityPersistent = FindById(id);
            
            if (entityPersistent == null) {
                throw new SDKRuntimeException(
                    string.Format("Entity not found. Entity.Id={0}, Entity.Type={1}", 
                    id, typeof(T).Name));
            }

            if (entityPersistent._Metadata.ChangeTrack == ChangeTrack.CREATE) {
                throw new SDKRuntimeException(
                    string.Format("Entity already been in create state. Entity.Id={0}, Entity.Type={1}", 
                    id, typeof(T).Name));
            }

            entityPersistent._Metadata.ChangeTrack = ChangeTrack.DELETE;
        }

        public void Delete(T entity)
        {
            if (entity == null) {
                throw new SDKRuntimeException("System can't delete null entity.");
            }
            if (string.IsNullOrEmpty(entity.Id)) {
                throw new SDKRuntimeException("System can't delete entity with null id.");
            }

            var entityPersistent = FindById(entity.Id);
            
            if (entityPersistent == null) {
                throw new SDKRuntimeException(
                    string.Format("Entity not found. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            if (entityPersistent._Metadata.ChangeTrack == ChangeTrack.CREATE) {
                throw new SDKRuntimeException(
                    string.Format("Entity already been in create state. Entity.Id={0}, Entity.Type={1}", 
                    entity.Id, entity.GetType().Name));
            }

            entityPersistent._Metadata.ChangeTrack = ChangeTrack.DELETE;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _entities.Where(it => !string.Equals(it._Metadata.ChangeTrack, ChangeTrack.DELETE)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
    }
}