using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetBuilderTest
    {
        private const string CONTENT_STR = "unidadegeradora:\n  model: tb_unidade_geradora\n  fields:\n    idUge:\n      column: id_uge\n    potenciaDisponivel:\n      column: pot_disp\n    dataInicioOperacao:\n      column: data_inicio_operacao\n    idUsina:\n      column: id_usina\n  filters:\n    byIdUsina:  \"id_usina in ($idsUsinas)\"\neventomudancaestadooperativo:\n  model: tb_evt_estado_oper\n  fields:\n    idEvento:\n      column: id_evento\n    idUge:\n      column: id_uge\n    idClassificacaoOrigem:\n      column: id_class_origem\n    idEstadoOperativo:\n      column: id_tp_estado_oper\n    idCondicaoOperativa:\n      column: id_cond_oper\n    dataVerificada:\n      column: data_verificada\n    potenciaDisponivel:\n      column: pot_disp\n    numONS: \n      column: numero_ons\n    eversao:\n      column: eversao  \n  filters:\n    menorQueData: \"data_verificada < :data order by data_verificada\"\n    maiorQueData: \"data_verificada > :data order by data_verificada\"\n    byIntervaloDatas: \"data_verificada >= :dataInicial and data_verificada <= :dataFinal and id_uge in ($idsUges) order by data_verificada\"\n    byIdsEventos:  \"id_evento in ($idsEventos!)\"\n    all:";
        private const string DATA = "2018-10-11";
        private const string MANTER_TAREFAS_MAP_NAME = "mantertarefas";
        private DataSetBuilder DataSetBuilder;
        private DomainTestContext DomainContext;
        private Mock<DomainClient> DomainClientMock;

        [SetUp]
        public void Setup()
        {
            DomainContext = new DomainTestContext();
            DomainClientMock = new Mock<DomainClient>();
            DataSetBuilder = new DataSetBuilder(DomainContext, DomainClientMock.Object);
            SetupDomainClientMock();
        }

        private void SetupDomainClientMock()
        {
            var EventosTask = Task.FromResult(GetEventosList());
            DomainClientMock.Setup(mock =>
                mock.FindByFilterAsync<EventoMudancaEstadoOperativo>(It.IsAny<EntityFilter>(), It.IsAny<Filter>())).Returns(EventosTask);
        }

        private List<EventoMudancaEstadoOperativo> GetEventosList()
        {
            var Eventos = new List<EventoMudancaEstadoOperativo>();
            for (int i = 0; i < 3; i++)
            {
                Eventos.Add(new EventoMudancaEstadoOperativo());
            }
            return Eventos;
        }

        [Test]
        public async Task BuildAsync()
        {
            var Payload = new TesteEntity();
            Payload.data = DATA;
            await DataSetBuilder.BuildAsync(CreatePlatformMap(), Payload);
            AssertFiltroUnidadeGeradora();
            AssertFiltroMudancaEstadoOperativo();
            AssertDomainClient();
            AssertDomainContext();
        }

        private void AssertFiltroUnidadeGeradora()
        {
            var FilterUnidadeGeradora = DataSetBuilder.EntitiesFilters[0];
            Assert.AreEqual(MANTER_TAREFAS_MAP_NAME, FilterUnidadeGeradora.MapName);
            Assert.AreEqual("unidadegeradora", FilterUnidadeGeradora.EntityName);
            Assert.AreEqual("byIdUsina", FilterUnidadeGeradora.Filters[0].Name);
            Assert.AreEqual("id_usina in ($idsUsinas)", FilterUnidadeGeradora.Filters[0].Query);
            Assert.False(FilterUnidadeGeradora.Filters[0].ShouldBeExecuted);
        }

        private void AssertFiltroMudancaEstadoOperativo()
        {
            EntityFilter FilterEventoOperativo = DataSetBuilder.EntitiesFilters[1];
            Assert.AreEqual(MANTER_TAREFAS_MAP_NAME, FilterEventoOperativo.MapName);
            Assert.AreEqual(5, FilterEventoOperativo.Filters.Count);

            Assert.AreEqual("eventomudancaestadooperativo", FilterEventoOperativo.EntityName);
            Assert.AreEqual("menorQueData", FilterEventoOperativo.Filters[0].Name);
            Assert.AreEqual("data_verificada < :data order by data_verificada", FilterEventoOperativo.Filters[0].Query);
            Assert.True(FilterEventoOperativo.Filters[0].ShouldBeExecuted);
            Assert.AreEqual(DATA, FilterEventoOperativo.Filters[0].Parameters["data"]);

            Assert.AreEqual("maiorQueData", FilterEventoOperativo.Filters[1].Name);
            Assert.AreEqual("data_verificada > :data order by data_verificada", FilterEventoOperativo.Filters[1].Query);
            Assert.True(FilterEventoOperativo.Filters[1].ShouldBeExecuted);
            Assert.AreEqual(DATA, FilterEventoOperativo.Filters[1].Parameters["data"]);

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

        private void AssertDomainClient()
        {
            var EntityFilter = DataSetBuilder.EntitiesFilters[1];
            var MenorQueDataFilter = EntityFilter.Filters[0];
            DomainClientMock.Verify(domainClient => domainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, MenorQueDataFilter), Times.Once);
            var MaiorQueDataFilter = EntityFilter.Filters[1];
            DomainClientMock.Verify(domainClient => domainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, MaiorQueDataFilter), Times.Once);
            var ByIntervaloDatasFilter = EntityFilter.Filters[2];
            DomainClientMock.Verify(domainClient => domainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, ByIntervaloDatasFilter), Times.Never);
            var ByIdsEventosFilter = EntityFilter.Filters[3];
            DomainClientMock.Verify(domainClient => domainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, ByIdsEventosFilter), Times.Never);
            var AllFilter = EntityFilter.Filters[4];
            DomainClientMock.Verify(domainClient => domainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, AllFilter), Times.Once);
        }

        private void AssertDomainContext()
        {
            Assert.AreEqual(9, Enumerable.Count<EventoMudancaEstadoOperativo>(DomainContext.EventoMudancaEstadoOperativo));
        }

        private PlatformMap CreatePlatformMap()
        {
            var PlatformMap = new PlatformMap();
            PlatformMap.Name = MANTER_TAREFAS_MAP_NAME;
            PlatformMap.Content = CONTENT_STR;
            return PlatformMap;
        }
    }

}