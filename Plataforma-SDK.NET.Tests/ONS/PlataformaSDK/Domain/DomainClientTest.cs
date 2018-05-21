using Moq;
using NUnit.Framework;
using ONS.PlataformaSDK.Core;
using ONS.PlataformaSDK.Http;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Environment;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ONS.PlataformaSDK.Constants;

namespace ONS.PlataformaSDK.Domain
{
    public class DomainClientTest
    {
        private Mock<HttpClient> HttpClientMock;
        private EnvironmentProperties EnvironmentProperties;
        private DomainClient DomainClient;

        [SetUp]
        public void Setup()
        {
            HttpClientMock = new Mock<HttpClient>();
            var EntityTask = Task.FromResult(GetJsonEventoOperativo());
            HttpClientMock.Setup(mock => mock.Get("http://localhost:8087/mantertarefas/eventomudancaestadooperativo?filter=menorQueData" +
                "&data=2015-01-01")).Returns(EntityTask);
            HttpClientMock.Setup(mock => mock.Get("http://localhost:8087/mantertarefas/eventomudancaestadooperativo?filter=byIntervaloDatas" +
                "&dataInicial=2015-01-01&dataFinal=2015-01-10")).Returns(EntityTask);
            HttpClientMock.Setup(mock => mock.Get("http://localhost:8087/mantertarefas/eventomudancaestadooperativo")).Returns(EntityTask);
            HttpClientMock.Setup(mock => mock.Post(It.IsAny<string>(), It.IsAny<string>()));
            EnvironmentProperties = new EnvironmentProperties("http", "localhost", "8087");
            DomainClient = new DomainClient(HttpClientMock.Object, EnvironmentProperties);
        }

        [Test]
        public void GetAsync()
        {
            var EntityFilter = new EntityFilter();
            EntityFilter.MapName = "mantertarefas";
            EntityFilter.EntityName = "eventomudancaestadooperativo";
            var Filter = new Filter("menorQueData", "data_verificada < :data order by data_verificada");
            Filter.Parameters.Add("data", "2015-01-01");
            var DomainTask = DomainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, Filter);
            var Eventos = DomainTask.Result;
            Assert.NotNull(Eventos);
        }

        [Test]
        public void GetAsyncWithMultipleParameters()
        {
            var EntityFilter = new EntityFilter();
            EntityFilter.MapName = "mantertarefas";
            EntityFilter.EntityName = "eventomudancaestadooperativo";
            var Filter = new Filter("byIntervaloDatas", "data_verificada >= :dataInicial and data_verificada <= :dataFinal");
            Filter.Parameters.Add("dataInicial", "2015-01-01");
            Filter.Parameters.Add("dataFinal", "2015-01-10");
            var DomainTask = DomainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, Filter);
            var Eventos = DomainTask.Result;
            Assert.NotNull(Eventos);
        }
        
        [Test]
        public void GetAll()
        {
            var EntityFilter = new EntityFilter();
            EntityFilter.MapName = "mantertarefas";
            EntityFilter.EntityName = "eventomudancaestadooperativo";
            var Filter = new Filter("all", "");
            var DomainTask = DomainClient.FindByFilterAsync<EventoMudancaEstadoOperativo>(EntityFilter, Filter);
            var Eventos = DomainTask.Result;
            Assert.NotNull(Eventos);
        }

        [Test]
        public void Persist()
        {
            var Eventos = new List<EventoMudancaEstadoOperativo>();
            Eventos.Add(CriarEvento("1"));
            Eventos.Add(CriarEvento("2"));
            DomainClient.Persist(Eventos, "ManterEvento");
            HttpClientMock.Verify(client => client.Post("http://localhost:8087/ManterEvento/persist", GetJsonEventoList()));
        }

        private EventoMudancaEstadoOperativo CriarEvento(string Id)
        {
            var Evento = new EventoMudancaEstadoOperativo();
            var Metadata = new Metadata("master", "EventoMudancaEstadoOperativo", DomainConstants.CHANGE_TRACK_CREATE);
            Evento._Metadata = Metadata;
            Evento.IdClassificacaoOrigem = "TEST";
            return Evento;
        }

        private string GetJsonEventoOperativo()
        {
            return "[{\"_metadata\": {\"branch\": \"master\", \"type\": \"eventomudancaestadooperativo\"}," +
                    "\"dataVerificada\": \"1997-08-01T03:00:00\"," +
                    "\"eversao\": 1," +
                    "\"id\": \"6b163f44-09ae-49c3-8508-debd4b0ada16\"," +
                    "\"idClassificacaoOrigem\": \"GAG\"," +
                    "\"idCondicaoOperativa\": \"\"," +
                    "\"idEstadoOperativo\": \"DCA\"," +
                    "\"idEvento\": \"400872\"," +
                    "\"idUge\": \"ALUXG-0UG1\"," +
                    "\"numONS\": \"COSR-NE 90488/1997\"," +
                    "\"potenciaDisponivel\": 0}]";
        }

        private string GetJsonEventoList()
        {
            return "[{\"IdClassificacaoOrigem\":\"TEST\",\"_Metadata\":{\"Branch\":\"master\",\"Type\":\"EventoMudancaEstadoOperativo\",\"ChangeTrack\":\"create\"}}," + 
                     "{\"IdClassificacaoOrigem\":\"TEST\",\"_Metadata\":{\"Branch\":\"master\",\"Type\":\"EventoMudancaEstadoOperativo\",\"ChangeTrack\":\"create\"}}]";
        }
    }
}