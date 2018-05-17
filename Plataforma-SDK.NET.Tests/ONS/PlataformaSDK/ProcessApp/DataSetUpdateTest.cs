using System;
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
            AssertUpdate();
        }

        [Test]
        public void UpdateWithPredicate()
        {
            DomainContext.EventoMudancaEstadoOperativo.Update(entity => entity.Id.Equals("1"));
            AssertUpdate();
        }

        [Test]
        public void UpdateWithNullEntity()
        {
            EventoMudancaEstadoOperativo NullEvento = null;
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Update(NullEvento));
        }
        
        [Test]
        public void UpdateWithNullPredicate()
        {
            Predicate<EventoMudancaEstadoOperativo> NullPredicate = null;
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Update(NullPredicate));
        }

        [Test]
        public void UpdateWithInvalidId()
        {
            var Evento3 = CreateEventoWithId("3");
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Update(Evento3));
        }

        private void AssertUpdate()
        {
            var UpdatedEntity = DomainContext.EventoMudancaEstadoOperativo[0];
            Assert.NotNull(UpdatedEntity._Metadata);
            Assert.AreEqual("master", UpdatedEntity._Metadata.Branch);
            Assert.AreEqual("update", UpdatedEntity._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", UpdatedEntity._Metadata.Type);

            var NonUpdatedEntity = DomainContext.EventoMudancaEstadoOperativo[1];
            Assert.NotNull(NonUpdatedEntity._Metadata);
            Assert.AreEqual("master", NonUpdatedEntity._Metadata.Branch);
            Assert.Null(NonUpdatedEntity._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", NonUpdatedEntity._Metadata.Type);
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