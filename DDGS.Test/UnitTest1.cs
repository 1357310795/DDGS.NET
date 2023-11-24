namespace DDGS.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var ddg = new DDGS();
            var res = ddg.Text("C# HttpClient");
            Assert.Pass();
        }
    }
}