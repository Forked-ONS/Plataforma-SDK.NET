using NUnit.Framework;
using ONS.PlataformaSDK.ProcessMemory;

namespace ONS.PlataformaSDK.ProcessMemory
{
    public class ProcessMemoryClientTest
    {
        private ProcessMemoryClient ProcessMemoryClient;

        [SetUp]
        public void Setup()
        {
            ProcessMemoryClient = new ProcessMemoryClient();
        }

        [Test]
        public void Head()
        {   
            Assert.NotNull(ProcessMemoryClient.Head());
        }
    }
}