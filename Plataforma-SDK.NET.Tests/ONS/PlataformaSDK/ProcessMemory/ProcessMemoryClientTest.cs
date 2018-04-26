using NUnit.Framework;
using ONS.PlataformaSDK.ProcessMemory;
using ONS.PlataformaSDK.Http;
using Moq;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClientTest
    {
        private ProcessMemoryClient ProcessMemoryClient;

        [SetUp]
        public void Setup()
        {
            var httpClientMock = new Mock<HttpClient>();
            ProcessMemoryClient = new ProcessMemoryClient(httpClientMock.Object);
        }

        [Test]
        public void Head()
        {   
            Assert.NotNull(ProcessMemoryClient.Head(""));
        }
    }
}