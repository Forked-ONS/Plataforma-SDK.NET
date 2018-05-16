using System;
using System.Collections.Generic;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Domain;

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

        public static void Insert<T>(this List<T> list, T entity)
        {
            var Metadata = new Metadata(MASTER_BRANCH, entity.GetType().Name, CHANGE_TRACK_CREATE);
            ((BaseEntity)(object)entity)._Metadata = Metadata;
            list.Add(entity);
        }

    }
}
