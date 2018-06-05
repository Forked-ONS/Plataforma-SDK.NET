using NUnit.Framework;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Utils.HttpTest {
    [TestFixture]
    public class HttpClientTest {
        [Test]
        public void ShouldConnectWithExternalService () {
            var client = new HttpClient ();
            var response = client.Post ("https://pruu.herokuapp.com/dump/teste", "hello");
            var ok = response.Result;
            var getResponse = client.Get ("https://pruu.herokuapp.com/dump/teste");
            var html = getResponse.Result;

            Assert.AreEqual ("OK", ok);
            Assert.IsTrue (html.Contains ("hello"));
        }
    }
}