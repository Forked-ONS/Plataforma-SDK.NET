using System.Collections.Generic;
using NUnit.Framework;
using ONS.PlataformaSDK.Domain;
using ONS.PlataformaSDK.Entities;
using ONS.PlataformaSDK.Exception;
using ONS.PlataformaSDK.Util;

namespace ONS.PlataformaSDK.ProcessApp
{
    public class DataSetInsertTest
    {

        [Test]
        public void Insert()
        {
            var DomainContext = new DomainTestContext();
            DomainContext.EventoMudancaEstadoOperativo.Add(new EventoMudancaEstadoOperativo());
            DomainContext.EventoMudancaEstadoOperativo.Insert(new EventoMudancaEstadoOperativo());

            Assert.Null(DomainContext.EventoMudancaEstadoOperativo[0]._Metadata);
            var InsertedEntity = DomainContext.EventoMudancaEstadoOperativo[1];
            Assert.NotNull(InsertedEntity._Metadata);
            Assert.AreEqual("master", InsertedEntity._Metadata.Branch);
            Assert.AreEqual("create", InsertedEntity._Metadata.ChangeTrack);
            Assert.AreEqual("EventoMudancaEstadoOperativo", InsertedEntity._Metadata.Type);
        }

        [Test]
        public void InsertNull()
        {
            var DomainContext = new DomainTestContext();
            Assert.Throws<PlataformaException>(() => DomainContext.EventoMudancaEstadoOperativo.Insert(null));
        }

    }
}