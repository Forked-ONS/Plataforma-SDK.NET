using System;
using System.Collections.Generic;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Exception;

namespace ONS.PlataformaSDK.Util
{
    public static class CollectionUtilExtensions
    {
        private const string MASTER_BRANCH = "master";
        private const string CHANGE_TRACK_CREATE = "create";
        private const string CHANGE_TRACK_UPDATE = "update";
        private const string CHANGE_TRACK_DELETE = "destroy";

        public static bool isEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static void Insert<T>(this List<T> list, T entity) where T : BaseEntity
        {
            VerifyEntity(entity);
            var Metadata = new Metadata(MASTER_BRANCH, entity.GetType().Name, CHANGE_TRACK_CREATE);
            entity._Metadata = Metadata;
            list.Add(entity);
        }

        public static void Delete<T>(this List<T> list, T entity) where T : BaseEntity
        {
            VerifyEntity(entity);
            var DbEntity = FindById(list, entity);
            VerifyEntity(DbEntity);
            DbEntity._Metadata.ChangeTrack = CHANGE_TRACK_DELETE;
        }

        public static void Delete<T>(this List<T> list, Predicate<T> filter) where T : BaseEntity
        {
            VerifyFilter(filter);
            var DbEntities = list.FindAll(filter);
            foreach (var DbEntity in DbEntities)
            {
                DbEntity._Metadata.ChangeTrack = CHANGE_TRACK_DELETE;
            }
        }

        public static void Update<T>(this List<T> list, T entity) where T : BaseEntity
        {
            VerifyEntity(entity);
            var DbEntity = FindById(list, entity);
            VerifyEntity(DbEntity);
            DbEntity._Metadata.ChangeTrack = CHANGE_TRACK_UPDATE;
        }

        public static void Update<T>(this List<T> list, Predicate<T> filter) where T : BaseEntity
        {
            VerifyFilter(filter);
            var DbEntities = list.FindAll(filter);
            foreach (var DbEntity in DbEntities)
            {
                DbEntity._Metadata.ChangeTrack = CHANGE_TRACK_UPDATE;
            }
        }

        private static T FindById<T>(List<T> list, T entity) where T : BaseEntity
        {
            return list.Find(dbEntity => dbEntity.Id.Equals(entity.Id));
        }

        private static void VerifyEntity<T>(T entity)
        {
            if (entity == null)
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
