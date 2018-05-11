using System.Collections.Generic;
using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class DomainContext : IDomainContext
    {
        public List<EventoMudancaEstadoOperativo> EventoMudancaEstadoOperativo;
        public List<UnidadeGeradora> UnidadeGeradora;
    }
}