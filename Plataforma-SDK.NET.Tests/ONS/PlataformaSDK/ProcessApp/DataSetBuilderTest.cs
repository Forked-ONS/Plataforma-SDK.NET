using System;
using NUnit.Framework;
using ONS.PlataformaSDK.Entities;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetBuilderTest
    {
        private const string CONTENT_STR = "unidadegeradora:\n  model: tb_unidade_geradora\n  fields:\n    idUge:\n      column: id_uge\n    potenciaDisponivel:\n      column: pot_disp\n    dataInicioOperacao:\n      column: data_inicio_operacao\n    idUsina:\n      column: id_usina\n  filters:\n    byIdUsina:  \"id_usina in ($idsUsinas)\"\neventomudancaestadooperativo:\n  model: tb_evt_estado_oper\n  fields:\n    idEvento:\n      column: id_evento\n    idUge:\n      column: id_uge\n    idClassificacaoOrigem:\n      column: id_class_origem\n    idEstadoOperativo:\n      column: id_tp_estado_oper\n    idCondicaoOperativa:\n      column: id_cond_oper\n    dataVerificada:\n      column: data_verificada\n    potenciaDisponivel:\n      column: pot_disp\n    numONS: \n      column: numero_ons\n    eversao:\n      column: eversao  \n  filters:\n    menorQueData: \"data_verificada < :data order by data_verificada\"\n    maiorQueData: \"data_verificada > :data order by data_verificada\"\n    byIntervaloDatas: \"data_verificada >= :dataInicial and data_verificada <= :dataFinal and id_uge in ($idsUges) order by data_verificada\"\n    byIdsEventos:  \"id_evento in ($idsEventos!)\"\n    all:";
        private DataSetBuilder DataSetBuilder;

        [SetUp]
        public void Setup()
        {
            DataSetBuilder = new DataSetBuilder();
        }

        [Test]
        public void Build()
        {
            DataSetBuilder.Build(CreatePlatformMap());
        }

        private PlatformMap CreatePlatformMap()
        {
            var PlatformMap = new PlatformMap();
            PlatformMap.Content = CONTENT_STR;
            return PlatformMap;
        }
    }

}