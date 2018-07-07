using System.Collections;
using System.Collections.Generic;
using ONS.SDK.Data.Impl;
using ONS.SDK.Domain.Base;

namespace ONS.SDK.Data
{
    public interface IEntityState
    {
        BaseEntity Enclosure {get;}

         Metadata Metadata {get;}
    }

}