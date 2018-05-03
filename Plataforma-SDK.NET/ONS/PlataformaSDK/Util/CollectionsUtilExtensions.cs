using System;
using System.Collections.Generic;
using ONS.PlataformaSDK.Core;

namespace ONS.PlataformaSDK.Util
{
    public static class CollectionUtilExtensions
    {
        public static bool isEmpty<T>(this List<T> list) {
            return list == null || list.Count == 0;
        }
    }
}
