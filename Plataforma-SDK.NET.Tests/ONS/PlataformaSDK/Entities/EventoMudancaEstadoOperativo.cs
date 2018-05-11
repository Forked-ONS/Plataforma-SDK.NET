using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class EventoMudancaEstadoOperativo : BaseEntity
    {
        public string DataVerificada{get; set;}
        public string Eversao{get; set;}
        public string IdClassificacaoOrigem{get; set;}
        public string IdCondicaoOperativa{get; set;}
        public string IdEstadoOperativo{get; set;}
        public string IdEvento{get; set;}
        public string IdUge{get; set;}
        public string NumONS{get; set;}
        public string PotenciaDisponivel{get; set;}
    }
}