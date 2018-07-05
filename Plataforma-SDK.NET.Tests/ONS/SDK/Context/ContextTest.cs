using NUnit.Framework;
using ONS.SDK.Context;

namespace Test.ONS.SDK.Context {
    [TestFixture]
    public class ContextTest
    {

        /*public ContextBuilder<string> GetBuilder() {
            return new ContextBuilder<string>();
        }*/

        [Test]
        public void ContextShouldHaveAnEvent()
        {
            //var context = this.GetBuilder().Build("");
            //Assert.IsNotNull(context.Event);
        }

        [Test]
        public void ContextShouldHaveDataSet(){
            //var context = this.GetBuilder().Build("");
            //Assert.IsNotNull(context.DataSet);
        }
    }
}