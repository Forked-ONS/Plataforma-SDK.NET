using System;
using System.Collections.Generic;
using NUnit.Framework;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Exception;
using ONS.PlataformaSDK.Util;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetDeleteTest
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
            var Evento3 = CreateEventoWithId("3");
            DomainContext.EventoMudancaEstadoOperativo.Add(Evento3);
        }


        [Test]
        public void Delete()
        {
            DomainContext.EventoMudancaEstadoOperativo.Delete(Evento1);
            AssertDelete();
        }

        [Test]
        public void DeleteWithPredicate()
        {
            DomainContext.EventoMudancaEstadoOperativo.Delete(bdEntity => bdEntity.Id.Equals("1"));
            AssertDelete();
        }

        [Test]
        public void DeleteWithNullPredicate()
        {
            Predicate<EventoMudancaEstadoOperativo> NullPredicate = null;
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Delete(NullPredicate));
        }


        [Test]
        public void DeleteWithNullEntity()
        {
            EventoMudancaEstadoOperativo NullEvento = null;
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Delete(NullEvento));
        }

        [Test]
        public void DeleteWithInvalidId()
        {
            var Evento4 = CreateEventoWithId("4");
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Delete(Evento4));
        }

        private void AssertDelete()
        {
            var DeletedEntity = DomainContext.EventoMudancaEstadoOperativo[0];
            Assert.NotNull(DeletedEntity._Metadata);
            Assert.AreEqual("master", DeletedEntity._Metadata.Branch);
            Assert.AreEqual("destroy", DeletedEntity._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", DeletedEntity._Metadata.Type);

            var NonDeletedEntity1 = DomainContext.EventoMudancaEstadoOperativo[1];
            Assert.NotNull(NonDeletedEntity1._Metadata);
            Assert.AreEqual("master", NonDeletedEntity1._Metadata.Branch);
            Assert.Null(NonDeletedEntity1._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", NonDeletedEntity1._Metadata.Type);
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