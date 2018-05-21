using System;
using System.Collections.Generic;
using ONS.PlataformaSDK.Constants;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Exception;

namespace ONS.PlataformaSDK.Util
{
    public static class CollectionUtilExtensions
    {
        public static bool isEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static void Insert<T>(this List<T> list, T entity) where T : BaseEntity
        {
            VerifyEntity(entity);
            var Metadata = new Metadata(DomainConstants.MASTER_BRANCH, entity.GetType().Name, DomainConstants.CHANGE_TRACK_CREATE);
            entity._Metadata = Metadata;
            list.Add(entity);
        }

        public static void Delete<T>(this List<T> list, T entity) where T : BaseEntity
        {
            VerifyEntity(entity);
            var DbEntity = FindById(list, entity);
            VerifyEntity(DbEntity);
            DbEntity._Metadata.ChangeTrack = DomainConstants.CHANGE_TRACK_DELETE;
        }

        public static void Delete<T>(this List<T> list, Predicate<T> filter) where T : BaseEntity
        {
            VerifyFilter(filter);
            var DbEntities = list.FindAll(filter);
            foreach (var DbEntity in DbEntities)
            {
                DbEntity._Metadata.ChangeTrack = DomainConstants.CHANGE_TRACK_DELETE;
            }
        }

        public static void Update<T>(this List<T> list, T entity) where T : BaseEntity
        {
            VerifyEntityToUpdate(entity);
            var Index = IndexById(list, entity);
            entity._Metadata.ChangeTrack = DomainConstants.CHANGE_TRACK_UPDATE;
            list[Index] = entity;
        }

        private static T FindById<T>(List<T> list, T entity) where T : BaseEntity
        {
            return list.Find(dbEntity => dbEntity.Id.Equals(entity.Id));
        }

        private static int IndexById<T>(List<T> list, T entity) where T : BaseEntity
        {
            var Index = list.FindLastIndex(dbEntity => dbEntity.Id.Equals(entity.Id));
            VerifyIndex(Index, entity);
            return Index;
        }

        private static void VerifyEntity<T>(T entity)
        {
            if (entity == null)
            {
                throw new PlataformaException("Entity not found.");
            }
        }

        private static void VerifyIndex<T>(int index, T entity) where T : BaseEntity
        {
            if (index < 0)
            {
                throw new PlataformaException("Entity not found");
            }
        }

        private static void VerifyEntityToUpdate<T>(T entity) where T : BaseEntity
        {
            if (entity == null || entity.Id == null)
            {
                throw new PlataformaException("Entity is not defined");
            }
        }

        private static void VerifyFilter<T>(Predicate<T> filter) where T : BaseEntity
        {
            if (filter == null)
            {
                throw new PlataformaException("Filter is not defined");
            }

        }

    }
}
