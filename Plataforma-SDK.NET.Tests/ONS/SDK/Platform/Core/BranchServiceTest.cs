using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONS.SDK.Configuration;
using ONS.SDK.Domain.Core;
using ONS.SDK.Platform.Core;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.CoreTest
{
    [TestFixture]
    public class BranchServiceTest
    {
        [Test]
        public void ShouldGetBranchOnApiCore(){
            Mock<HttpClient> mock = new Mock<HttpClient> ();
            mock.Setup (http => http.Get ("://:/core/branch?filter=bySystemIdAndOwner&systemId=1&owner=2")).Returns (Task.FromResult (branchResponse));
            var service = new BranchService(new CoreConfig(), new JsonHttpClient(mock.Object));
            var list = service.FindBySystemIdAndOwner("1","2");

            Assert.Greater(list.Count,0);
            Assert.AreEqual(list[0].Id, "f15f6641-dcee-4982-9fe5-a7c26207207a");
        }

        [Test]
        public void ShouldSaveNewBranch(){
            Mock<HttpClient> mock = new Mock<HttpClient> ();
            mock.Setup (http => http.Post (It.IsAny<string>(), It.IsAny<string>())).Returns (Task.FromResult (branchResponse));
            var service = new BranchService(new CoreConfig(), new JsonHttpClient(mock.Object));
            var branch = new Branch(){
                Name="cenario",
                Description="descricao"
            };
            //branch._Metadata.ChangeTrack = "create";

            service.Save(branch);
            mock.Verify(http => http.Post (It.IsAny<string>(), branchPersistBody));
        }



        private readonly string branchResponse = @"
        [
            {
                ""_metadata"": {
                    ""branch"": ""master"",
                    ""instance_id"": null,
                    ""modified_at"": ""2018-06-04T17:00:42.074582"",
                    ""origin"": null,
                    ""type"": ""branch""
                },
                ""description"": ""faz a modificacao de operação"",
                ""id"": ""f15f6641-dcee-4982-9fe5-a7c26207207a"",
                ""name"": ""cenario-1"",
                ""owner"": ""anonymous"",
                ""startedAt"": ""2018-06-05T20:41:46"",
                ""status"": ""open"",
                ""systemId"": ""ec498841-59e5-47fd-8075-136d79155705""
            }
        ]
        ";

        private readonly string branchPersistBody = "[{\"name\":\"cenario\",\"description\":\"descricao\",\"_Metadata\":{\"type\":\"branch\",\"changeTrack\":\"create\"}}]";
    }
}