using System;
using System.Threading.Tasks;

namespace ONS.PlataformaSDK.Domain
{
    public class BaseEntity
    {
        public string id{get;set;}
        public Metadata _metadata{get; set;}
    }
}