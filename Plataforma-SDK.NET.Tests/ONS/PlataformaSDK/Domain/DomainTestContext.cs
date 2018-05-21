using System.Collections.Generic;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class DomainTestContext : IDomainContext
    {
        public List<EventoMudancaEstadoOperativo> EventoMudancaEstadoOperativo { get; set; }

        public DomainTestContext()
        { 
            EventoMudancaEstadoOperativo = new List<EventoMudancaEstadoOperativo>();
        }

        public List<BaseEntity> GetPersistList()
        {
            var DomainList = new List<BaseEntity>();
            DomainList.AddRange(EventoMudancaEstadoOperativo);
            return DomainList;
        }
    }
}