using System;
using NUnit.Framework;
using ONS.PlataformaSDK.Entities;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetBuilderTest
    {
        private const string CONTENT_STR = "unidadegeradora:\n  model: tb_unidade_geradora\n  fields:\n    idUge:\n      column: id_uge\n    potenciaDisponivel:\n      column: pot_disp\n    dataInicioOperacao:\n      column: data_inicio_operacao\n    idUsina:\n      column: id_usina\n  filters:\n    byIdUsina:  \"id_usina in ($idsUsinas)\"\neventomudancaestadooperativo:\n  model: tb_evt_estado_oper\n  fields:\n    idEvento:\n      column: id_evento\n    idUge:\n      column: id_uge\n    idClassificacaoOrigem:\n      column: id_class_origem\n    idEstadoOperativo:\n      column: id_tp_estado_oper\n    idCondicaoOperativa:\n      column: id_cond_oper\n    dataVerificada:\n      column: data_verificada\n    potenciaDisponivel:\n      column: pot_disp\n    numONS: \n      column: numero_ons\n    eversao:\n      column: eversao  \n  filters:\n    menorQueData: \"data_verificada < :data order by data_verificada\"\n    maiorQueData: \"data_verificada > :data order by data_verificada\"\n    byIntervaloDatas: \"data_verificada >= :dataInicial and data_verificada <= :dataFinal and id_uge in ($idsUges) order by data_verificada\"\n    byIdsEventos:  \"id_evento in ($idsEventos!)\"\n    all:";
        private DataSetBuilder<TesteEntity> DataSetBuilder;

        [SetUp]
        public void Setup()
        {
            DataSetBuilder = new DataSetBuilder<TesteEntity>();
        }

        [Test]
        public void Build()
        {
            var TesteEntity = new TesteEntity();
            TesteEntity.data = "2018-10-11";
            DataSetBuilder.Build(CreatePlatformMap(), TesteEntity);
            AssertFiltroUnidadeGeradora(DataSetBuilder);
            AssertFiltroMudancaEstadoOperativo(DataSetBuilder);
        }

        private void AssertFiltroUnidadeGeradora(DataSetBuilder<TesteEntity> dataSetBuilder)
        {
            var FilterUnidadeGeradora = DataSetBuilder.EntitiesFilters[0];
            Assert.AreEqual("unidadegeradora", FilterUnidadeGeradora.EntityName);
            Assert.AreEqual("byIdUsina", FilterUnidadeGeradora.Filters[0].Name);
            Assert.AreEqual("id_usina in ($idsUsinas)", FilterUnidadeGeradora.Filters[0].Query);
        }

        private void AssertFiltroMudancaEstadoOperativo(DataSetBuilder<TesteEntity> dataSetBuilder)
        {
            EntityFilter FilterEventoOperativo = DataSetBuilder.EntitiesFilters[1];
            Assert.AreEqual(5, FilterEventoOperativo.Filters.Count);
            Assert.AreEqual("eventomudancaestadooperativo", FilterEventoOperativo.EntityName);
            Assert.AreEqual("menorQueData", FilterEventoOperativo.Filters[0].Name);
            Assert.AreEqual("data_verificada < :data order by data_verificada", FilterEventoOperativo.Filters[0].Query);
            Assert.True(FilterEventoOperativo.Filters[0].ShouldBeExecuted);
            Assert.AreEqual("maiorQueData", FilterEventoOperativo.Filters[1].Name);
            Assert.AreEqual("data_verificada > :data order by data_verificada", FilterEventoOperativo.Filters[1].Query);
            Assert.True(FilterEventoOperativo.Filters[1].ShouldBeExecuted);
            Assert.AreEqual("byIntervaloDatas", FilterEventoOperativo.Filters[2].Name);
            Assert.AreEqual("data_verificada >= :dataInicial and data_verificada <= :dataFinal and id_uge in ($idsUges) order by data_verificada", FilterEventoOperativo.Filters[2].Query);
            Assert.False(FilterEventoOperativo.Filters[2].ShouldBeExecuted);
            Assert.AreEqual("byIdsEventos", FilterEventoOperativo.Filters[3].Name);
            Assert.AreEqual("id_evento in ($idsEventos!)", FilterEventoOperativo.Filters[3].Query);
            Assert.False(FilterEventoOperativo.Filters[3].ShouldBeExecuted);
            Assert.AreEqual("all", FilterEventoOperativo.Filters[4].Name);
            Assert.Null(FilterEventoOperativo.Filters[4].Query);
            Assert.True(FilterEventoOperativo.Filters[4].ShouldBeExecuted);
        }

        private PlatformMap CreatePlatformMap()
        {
            var PlatformMap = new PlatformMap();
            PlatformMap.Content = CONTENT_STR;
            return PlatformMap;
        }
    }

}