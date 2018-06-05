using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ONS.SDK.Infra;
using ONS.SDK.Platform.ProcessMemoryClient;
using ONS.SDK.Utils.Http;

namespace ONS.SDK.Platform.ProcessMemoryTest {

    [TestFixture]
    public class ProcessMemoryServiceTest {


        [Test]
        public void ShouldGetHeadOfProcessMemory () {
            Mock<HttpClient> mock = new Mock<HttpClient> ();
            mock.Setup (http => http.Get (It.IsAny<string> ())).Returns (Task.FromResult (_response));
            var config = new ProcessMemoryConfig ();
            var service = new ProcessMemoryService<Payload> (config, new JsonHttpClient(mock.Object));
            var memory = service.Head("");
            var map = memory.Map;

            Assert.AreEqual(memory.Event.Payload.PersonId, "30696c2d-2ffc-4a2e-97d7-d5140534d3ec");
            Assert.AreEqual(memory.Event.Tag, "b1d77ca4-6819-11e8-8231-0242ac12000c");
            Assert.AreEqual(memory.ProcessId,"7828c3c7-0352-42a5-9342-2673293bc93d");
            Assert.AreEqual(memory.SystemId,"ec498841-59e5-47fd-8075-136d79155705");
            Assert.IsNotNull(map);

            Assert.IsTrue(map.Content.ContainsKey("Operacao"));
            Assert.IsTrue(map.Content.ContainsKey("Conta"));
            Assert.AreEqual(map.Id,"863fa1ef-71e9-4d96-a9b8-5dd0594370a2");
            Assert.AreEqual(map.Name, "ConsolidaSaldo");

            var op = map.Content["Operacao"];

            Assert.AreEqual(op.Model,"operacao");
            Assert.IsTrue(op.Fields.ContainsKey("value"));
            Assert.AreEqual(op.Fields["value"].Column, "valor");
            Assert.AreEqual(op.Filters["byTitular"],"titular_id = :personId");

        }

        class Payload {
            public string PersonId { get; set; }
        }

        #region mock response
        private readonly string _response = @"
        {
    ""event"": {
        ""name"": ""consolida.saldo.request"",
        ""instance_id"": null,
        ""reference_date"": null,
        ""tag"": ""b1d77ca4-6819-11e8-8231-0242ac12000c"",
        ""scope"": ""execution"",
        ""branch"": ""master"",
        ""commands"": [],
        ""reproduction"": {},
        ""reprocessing"": {},
        ""payload"": {
            ""personId"": ""30696c2d-2ffc-4a2e-97d7-d5140534d3ec""
        }
    },
    ""processId"": ""7828c3c7-0352-42a5-9342-2673293bc93d"",
    ""systemId"": ""ec498841-59e5-47fd-8075-136d79155705"",
    ""instanceId"": ""4674f524-8250-4f17-a4c0-7a194ad30231"",
    ""eventOut"": ""consolida-saldo.done"",
    ""commit"": true,
    ""map"": {
        ""_metadata"": {
            ""branch"": ""master"",
            ""instance_id"": null,
            ""modified_at"": ""2018-06-04T17:00:42.000Z"",
            ""origin"": null,
            ""type"": ""map""
        },
        ""content"": {
            ""Operacao"": {
                ""model"": ""operacao"",
                ""fields"": {
                    ""value"": {
                        ""column"": ""valor""
                    },
                    ""type"": {
                        ""column"": ""tipo""
                    },
                    ""personId"": {
                        ""column"": ""titular_id""
                    },
                    ""date"": {
                        ""column"": ""data""
                    }
                },
                ""filters"": {
                    ""byTitular"": ""titular_id = :personId""
                }
            },
            ""Conta"": {
                ""model"": ""conta"",
                ""fields"": {
                    ""personId"": {
                        ""column"": ""id""
                    },
                    ""balance"": {
                        ""column"": ""saldo""
                    }
                },
                ""filters"": {
                    ""byPersonId"": ""id = :personId""
                }
            }
        },
        ""id"": ""863fa1ef-71e9-4d96-a9b8-5dd0594370a2"",
        ""name"": ""ConsolidaSaldo"",
        ""processId"": ""7828c3c7-0352-42a5-9342-2673293bc93d"",
        ""systemId"": ""ec498841-59e5-47fd-8075-136d79155705""
    },
    ""dataset"": {
        ""Operacao"": {
            ""collection"": {
                ""prevSource"": {}
            }
        },
        ""Conta"": {
            ""collection"": {
                ""prevSource"": {}
            }
        },
        ""entities"": {
            ""Operacao"": [
                {
                    ""_metadata"": {
                        ""branch"": ""master"",
                        ""instance_id"": ""5bfc9c08-30c7-4e77-a23d-77416e9927e5"",
                        ""modified_at"": ""2018-06-04T17:06:26.000Z"",
                        ""origin"": null,
                        ""type"": ""Operacao""
                    },
                    ""date"": ""2018-01-01T12:00:00.000Z"",
                    ""id"": ""d2cd1e4f-61ac-4b8e-9b87-b4433fb68574"",
                    ""personId"": ""30696c2d-2ffc-4a2e-97d7-d5140534d3ec"",
                    ""type"": ""credit"",
                    ""value"": 10
                }
            ],
            ""Conta"": [
                {
                    ""_metadata"": {
                        ""branch"": ""master"",
                        ""instance_id"": ""0fe19387-cd07-4197-aab1-1023a73d3f92"",
                        ""modified_at"": ""2018-06-04T17:05:55.000Z"",
                        ""origin"": null,
                        ""type"": ""Conta"",
                        ""changeTrack"": ""update""
                    },
                    ""balance"": 10,
                    ""id"": ""30696c2d-2ffc-4a2e-97d7-d5140534d3ec"",
                    ""personId"": ""30696c2d-2ffc-4a2e-97d7-d5140534d3ec""
                }
            ]
        }
    },
    ""logs"": []
}
        ";
        #endregion
    }
}