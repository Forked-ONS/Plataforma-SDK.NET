using System.Collections.Generic;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class DomainContext : IDomainContext
    {
        public IEnumerable<EventoMudancaEstadoOperativo> EventoMudancaEstadoOperativo{get;set;}
        public IEnumerable<UnidadeGeradora> UnidadeGeradora{get;set;}
    }
}