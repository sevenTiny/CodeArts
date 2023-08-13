using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.NetCoreConfiguration
{
    [TestClass]
    public class JsonConfigTest
    {
        private IConfigurationRoot config;

        public JsonConfigTest()
        {
            // Build a config object, using env vars and JSON providers.
            config = new ConfigurationBuilder()
                .AddJsonFile(@"appsettings.json")
                .Build();
        }

        [TestMethod]
        public void OptionsMode()
        {
            //Get 和下面示例中的 Bind 是等价的API
            Assert.AreEqual("Production", config.GetRequiredSection("Environment").Get<string>());

            // Get values from the config given their key and their target type.
            Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

            Assert.AreEqual(1, settings?.KeyOne);
            Assert.AreEqual(true, settings?.KeyTwo);
            Assert.AreEqual("Oh, that's nice...", settings?.KeyThree?.Message);
            Assert.AreEqual("1.0.0", settings?.KeyThree?.SupportedVersions?.v1);
            Assert.AreEqual("3.0.7", settings?.KeyThree?.SupportedVersions?.v3);
            Assert.AreEqual(5, settings?.IPAddressRange?.Length);
        }

        [TestMethod]
        public void OptionsMode1()
        {
            Settings settings = new();

            config.Bind("Settings", settings);

            Assert.AreEqual(1, settings?.KeyOne);
        }

        [TestMethod]
        public void OptionsMode2()
        {
            Settings settings = new();

            config.GetSection("Settings").Bind(settings);

            Assert.AreEqual(1, settings?.KeyOne);
        }

        [TestMethod]
        public void ColonGetValueTest()
        {
            Assert.AreEqual("Production", config.GetValue<string>("Environment"));
            Assert.AreEqual(1, config.GetValue<int>("Settings:KeyOne"));
            Assert.AreEqual(true, config.GetValue<bool>("Settings:KeyTwo"));
            Assert.AreEqual("Oh, that's nice...", config.GetValue<string>("Settings:KeyThree:Message"));

            //索引获取数组
            Assert.AreEqual("46.36.198.121", config.GetValue<string>("Settings:IPAddressRange:0"));

            //注：该API不支持复杂类型，例如: 
            //config.GetValue<string[]>("Settings:IPAddressRange");
            //config.GetValue<NestedSettings>("Settings:KeyThree")?.Message;
        }

        [TestMethod]
        public void IndexGetTest()
        {
            Assert.AreEqual("Production", config["Environment"]);
            Assert.AreEqual("1", config["Settings:KeyOne"]);
            Assert.AreEqual("True", config["Settings:KeyTwo"]);
            Assert.AreEqual("Oh, that's nice...", config["Settings:KeyThree:Message"]);
            Assert.AreEqual("46.36.198.121", config["Settings:IPAddressRange:0"]);
        }

        [TestMethod]
        public void SectionGetTest()
        {
            Assert.AreEqual("Production", config.GetSection("Environment").Get<string>());
            //灵活的方式
            Assert.AreEqual(1, config.GetSection("Settings").GetSection("KeyOne").Get<int>());
            Assert.AreEqual(1, config.GetSection("Settings").GetValue<int>("KeyOne"));
            Assert.AreEqual("1", config.GetSection("Settings")["KeyOne"]);
        }

        [TestMethod]
        public void Reload()
        {
            var cfg = new ConfigurationBuilder()
                .AddJsonFile(@"appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var path = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

            var content = File.ReadAllText(path);

            var tempData = cfg.GetValue<string>("TempData");

            File.WriteAllText(path, content.Replace("TempData111", "TempData222"));

            Thread.Sleep(1000);

            //如果是绑定强类型方式，也需要每次call API去获取
            var tempData2 = cfg.GetValue<string>("TempData");

            Assert.AreNotEqual(tempData, tempData2);

#if DEBUG
            //等待直到变更
            //while (tempData == tempData2)
            //{
            //    Trace.WriteLine($"current is {tempData2} change waiting...");
            //    Thread.Sleep(1000);
            //    tempData2 = cfg.GetValue<string>("TempData");
            //}

            //Trace.WriteLine($"current is {tempData2} change finished!");
#endif
        }

        class Settings
        {
            public int KeyOne { get; set; }
            public bool KeyTwo { get; set; }
            public NestedSettings KeyThree { get; set; } = null!;
            public string[] IPAddressRange { get; set; } = null!;
        }

        class NestedSettings
        {
            public string Message { get; set; } = null!;
            public SupportedVersion SupportedVersions { get; set; } = null!;
        }

        class SupportedVersion
        {
            public string v1 { get; set; }
            public string v3 { get; set; }
        }
    }
}