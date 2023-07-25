using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.NetCoreConfiguration
{
    [TestClass]
    public class IniConfigTest
    {
        private IConfigurationRoot config;

        public IniConfigTest()
        {
            // Build a config object, using env vars and JSON providers.
            config = new ConfigurationBuilder()
                .AddIniFile(@"appsettings.ini")
                .Build();
        }

        [TestMethod]
        public void IndexGetTest()
        {
            Assert.AreEqual("Secret key value", config["SecretKey"]);
            Assert.AreEqual("True", config["TransientFaultHandlingOptions:Enabled"]);
            Assert.AreEqual("Information", config["Logging:LogLevel:Default"]);
        }

        [TestMethod]
        public void OptionsMode()
        {
            // Get values from the config given their key and their target type.
            Settings? settings = config.Get<Settings>();

            Assert.AreEqual("Secret key value", settings?.SecretKey);
            Assert.AreEqual(true, settings?.TransientFaultHandlingOptions?.Enabled);
            Assert.AreEqual("00:00:07", settings?.TransientFaultHandlingOptions?.AutoRetryDelay);
        }

        class Settings
        {
            public string SecretKey { get; set; }
            public TransientFaultHandlingOption TransientFaultHandlingOptions { get; set; }
        }

        class TransientFaultHandlingOption
        {
            public bool Enabled { get; set; }
            public string AutoRetryDelay { get; set; }
        }
    }
}
