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

        public override bool Equals(object obj)
        {
            var @DomainTestContext = obj as DomainTestContext;
            return @DomainTestContext != null &&
                   EventoMudancaEstadoOperativo.Equals(@DomainTestContext.EventoMudancaEstadoOperativo);
        }

        public override int GetHashCode()
        {
            var hashCode = -629850613;
            hashCode = hashCode * -1521134295 + EqualityComparer<List<EventoMudancaEstadoOperativo>>.Default.GetHashCode(EventoMudancaEstadoOperativo);
            return hashCode;
        }
    }
}