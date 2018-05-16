using System.Collections.Generic;
using NUnit.Framework;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Exception;
using ONS.PlataformaSDK.Util;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetUpdateTest
    {

        private DomainTestContext DomainContext;
        private EventoMudancaEstadoOperativo Evento1;

        [SetUp]
        public void Setup()
        {
            DomainContext = new DomainTestContext();
            Evento1 = CreateEventoWithId("1");
            DomainContext.EventoMudancaEstadoOperativo.Add(Evento1);
            var Evento2 = CreateEventoWithId("2");
            DomainContext.EventoMudancaEstadoOperativo.Add(Evento2);
        }


        [Test]
        public void Update()
        {

            DomainContext.EventoMudancaEstadoOperativo.Update(Evento1);

            var DeletedEntity = DomainContext.EventoMudancaEstadoOperativo[0];
            Assert.NotNull(DeletedEntity._Metadata);
            Assert.AreEqual("master", DeletedEntity._Metadata.Branch);
            Assert.AreEqual("update", DeletedEntity._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", DeletedEntity._Metadata.Type);

            var NonDeletedEntity = DomainContext.EventoMudancaEstadoOperativo[1];
            Assert.NotNull(DeletedEntity._Metadata);
            Assert.AreEqual("master", NonDeletedEntity._Metadata.Branch);
            Assert.Null(NonDeletedEntity._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", NonDeletedEntity._Metadata.Type);

            new List<BaseEntity>().Find(dbEntity => dbEntity.Id.Equals("1"));
        }

        [Test]
        public void UpdateWithNullEntity()
        {
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Update(null));
        }

        [Test]
        public void UpdateWithInvalidId()
        {
            var Evento3 = CreateEventoWithId("3");
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Update(Evento3));
        }
        private static EventoMudancaEstadoOperativo CreateEventoWithId(string id)
        {
            var Evento = new EventoMudancaEstadoOperativo();
            Evento.Id = id;
            Evento._Metadata = new Metadata("master", "EventoMudancaEstadoOperativo", null);
            return Evento;
        }
    }
}