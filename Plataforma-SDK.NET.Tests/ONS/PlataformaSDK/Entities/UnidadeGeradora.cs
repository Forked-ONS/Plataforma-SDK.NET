using ONS.PlataformaSDK.Domain;

namespace ONS.PlataformaSDK.Entities
{
    public class UnidadeGeradora : BaseEntity
    {
        public string DataInicioOperacao{get; set;}
        public string IdUge{get; set;}
        public string idUsina{get; set;}
        public string PotenciaDisponivel{get; set;}
    }
}