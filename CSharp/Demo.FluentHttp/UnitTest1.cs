using Flurl;
using Flurl.Http;

namespace Demo.FluentHttp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BuildUrl()
        {
            var url = "https://www.bing.com"
                .AppendPathSegment("search")
                .SetQueryParam("q", "C#")
                .ToString()
                ;

            Assert.AreEqual("https://www.bing.com/search?q=C%23", url);
        }

        [TestMethod]
        public void HttpGet()
        {
            var result = "https://www.bing.com"
               .AppendPathSegment("search")
               .SetQueryParam("q", "C#")
               .GetAsync()
               .ReceiveString()
               .Result;

            Assert.IsNotNull(result);
        }
    }
}