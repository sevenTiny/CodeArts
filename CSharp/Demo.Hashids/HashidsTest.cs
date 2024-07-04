using HashidsNet;

namespace Demo.HashidsTest
{
    [TestClass]
    public class HashidsTest
    {
        [TestMethod]
        public void EncodeAndDecodeNumber()
        {
            var hashids = new Hashids("this is my salt");
            var hash = hashids.Encode(12345);

            // use different salt will generate different hash
            Assert.AreEqual("NkK9", hash);

            var numbers = hashids.Decode(hash);

            Assert.AreEqual(12345, numbers[0]);
        }

        [TestMethod]
        public void EncodeAndDecodeLong()
        {
            var hashids = new Hashids("this is my salt");
            var hash = hashids.EncodeLong(666555444333222L);

            // use different salt will generate different hash
            Assert.AreEqual("KVO9yy1oO5j", hash);

            var numbers = hashids.DecodeLong(hash);

            Assert.AreEqual(666555444333222L, numbers[0]);
        }

        [TestMethod]
        public void EncodeAndDecodeString()
        {
            var hashids = new Hashids("this is my salt");
            var hash = hashids.EncodeHex("12345");

            // use different salt will generate different hash
            Assert.AreEqual("MR2aB", hash);

            var str = hashids.DecodeHex(hash);

            Assert.AreEqual("12345", str);
        }
    }
}