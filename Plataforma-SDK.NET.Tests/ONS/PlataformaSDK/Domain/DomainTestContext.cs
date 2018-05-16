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

    }
}