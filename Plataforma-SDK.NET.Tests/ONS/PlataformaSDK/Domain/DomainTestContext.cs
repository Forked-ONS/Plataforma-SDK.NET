using System.Collections.Generic;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class DomainTestContext : IDomainContext
    {
        public IEnumerable<EventoMudancaEstadoOperativo> EventoMudancaEstadoOperativo{get;set;}

    }
}