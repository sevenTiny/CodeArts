using Demo.FreeSql.Models;
using FreeSql;
using FreeSql.Internal;
using FreeSql.Internal.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Demo.FreeSql
{
    [TestClass]
    public class PostgresTests
    {
        private IFreeSql _fsql;

        public PostgresTests()
        {
            _fsql = new FreeSqlBuilder()
                .UseConnectionString(DataType.PostgreSQL, @"Host=192.168.1.5;Port=5432;Username=postgres;Password=postgres123456;Database=test;ArrayNullabilityMode=Always;Pooling=true;Maximum Pool Size=2")
                .UseNameConvert(NameConvertType.ToLower)
                .Build();

            _fsql.UseJsonMap();
        }

        [TestMethod]
        public void InitData()
        {
            // clear data
            _fsql.Delete<ChameleonApp>().Where("1=1").ExecuteAffrows();

            // init data
            var dataList = new List<ChameleonApp>();

            for (int i = 0; i < 100; i++)
            {
                dataList.Add(new ChameleonApp
                {
                    _id = Guid.NewGuid(),
                    Number = i,
                    Name = $"Demo_{i}",
                    Code = $"Code_{i}",
                    Icon = "xx",
                    Description = $"Description {i} some blablablabla",
                    CreatedBy = "admin",
                    CreatedTime = DateTime.Now,
                    IsDeleted = false,
                    Type = Models.Type.Senior,
                    User = new User
                    {
                        Name = $"mike_{i}",
                        Age = i
                    },
                    User2 = JObject.FromObject(new User
                    {
                        Name = $"json_mike_{i}",
                        Age = i
                    }),
                    StrArray = ["1", "2", "3"],
                    IntArray = [1, 2, 3]
                });
            }

            _fsql.Insert(dataList).ExecuteAffrows();
        }

        [TestMethod]
        public void QueryData()
        {
            var list = _fsql.Select<ChameleonApp>().Where(a => a.IsDeleted == false).ToList();

            Assert.AreEqual(100, list.Count);
        }

        #region dynamic query

        [TestMethod]
        public void AddUseDict()
        {
            var id = Guid.NewGuid();

            // add
            var dic = new Dictionary<string, object>
            {
                {"_id", id },
                { "name", "Demo_dic_1" },
                { "code", "Code_dic_1" },
                { "icon", "dic" },
                { "description", "Description dic 1 some blablablabla" },
                { "createdby", "admin" },
                { "createdtime", DateTime.Now },
                { "isdeleted", false }
            };

            _fsql.InsertDict(dic).AsTable("chameleonapp").ExecuteAffrows();

            // update
            var update = new Dictionary<string, object>
            {
                {"_id", id },
                {"name", "Demo_dic_1_update"}
            };

            _fsql.UpdateDict(update).AsTable("chameleonapp").WherePrimary("_id").ExecuteAffrows();

            // delete
            var delete = new Dictionary<string, object>
            {
                {"_id", id }
            };
            _fsql.DeleteDict(delete).AsTable("chameleonapp").ExecuteAffrows();
        }

        [TestMethod]
        public void BatchUpdateUseDict()
        {
            var batchUpdate = new List<Dictionary<string, object>> {
                new() {
                    {"_id", Guid.Parse("7b78b088-94f7-47d1-8412-1f2f0f20309c") },
                    {"number", 101},
                    { "icon",DateTime.Now.ToString()}
                },
                new() {
                    {"_id", Guid.Parse("0a7f7073-6c75-43e8-945b-a6d4d7e4e782") },
                    {"number", 102},
                    { "icon",DateTime.Now.ToString()}
                }
            };

            _fsql.UpdateDict(batchUpdate).AsTable("chameleonapp").WherePrimary("_id").ExecuteAffrows();
        }

        [TestMethod]
        public void BatchDelete()
        {
            // delete
            var batch = new List<Dictionary<string, object>> {
                new() {
                    {"_id", Guid.Parse("7b78b088-94f7-47d1-8412-1f2f0f20309c") },
                },
                new() {
                    {"_id", Guid.Parse("0a7f7073-6c75-43e8-945b-a6d4d7e4e782") },
                    {"number", 99},
                }
            };

            _fsql.DeleteDict(batch).AsTable("chameleonapp").ExecuteAffrows();
        }

        [TestMethod]
        public void QueryUseDict_ById()
        {
            var filter = new DynamicFilterInfo
            {
                Logic = DynamicFilterLogic.And,
                Filters =
                [
                    new DynamicFilterInfo
                    {
                        Field = "_id",
                        Operator = DynamicFilterOperator.Equals,
                        Value = Guid.Parse("0a7f7073-6c75-43e8-945b-a6d4d7e4e782")
                    }
                ]
            };
            var query = _fsql.Select<object>().AsTable((a, b) => "chameleonapp").WhereDynamicFilter(filter);

            var list = query.ToList();
        }

        [TestMethod]
        public void QueryUseDict_EQ()
        {
            var dyfilter = JsonConvert.DeserializeObject<DynamicFilterInfo>(@"
{
  ""Logic"": ""And"",
  ""Filters"":
  [
    { ""Field"": ""_id"", ""Operator"": ""Equals"", ""Value"": ""0a7f7073-6c75-43e8-945b-a6d4d7e42782"" },
  ]
}");

            var query = _fsql.Select<object>().AsTable((a, b) => "chameleonapp").WhereDynamicFilter(dyfilter);

            var list = query.ToList();

            var table = query.ToDataTable();
        }

        [TestMethod]
        public void QueryUseDict_EQ2()
        {
            var dyfilter = new DynamicFilterInfo
            {
                Field = "_id",
                Operator = DynamicFilterOperator.Equals,
                Value = Guid.Parse("0a7f7073-6c75-43e8-945b-a6d4d7e4e782")
            };

            var query = _fsql.Select<object>().AsTable((a, b) => "chameleonapp").WhereDynamicFilter(dyfilter);

            var list = query.ToList();
        }

        [TestMethod]
        public void Query_Any()
        {
            var filter = new DynamicFilterInfo
            {
                Field = "_id",
                Operator = DynamicFilterOperator.Any,
                Value = new[] { "0a7f7073-6c75-43e8-945b-a6d4d7e4e782" }
            };

            var query = _fsql.Select<object>().AsTable((a, b) => "chameleonapp").WhereDynamicFilter(filter);

            var list = query.ToList();
        }

        [TestMethod]
        public void QueryUseDict_GreaterThanOrEqual()
        {
            var dyfilter = JsonConvert.DeserializeObject<DynamicFilterInfo>(@"
{
  ""Logic"": ""And"",
  ""Filters"":
  [
    { ""Field"": ""number"", ""Operator"": ""GreaterThanOrEqual"", ""Value"": 90 },
  ]
}");

            var query = _fsql.Select<object>().AsTable((a, b) => "chameleonapp").WhereDynamic(dyfilter);

            var list = query.ToList();
        }

        #endregion

        [TestMethod]
        public void MultiTenant()
        {
            var fsql = new FreeSqlCloud<string>();

            fsql.Register("db1", () =>
            {
                return new FreeSqlBuilder()
                    .UseConnectionString(DataType.PostgreSQL, "Host=192.168.1.5;Port=5432;Username=postgres;Password=postgres123456;Database=test;ArrayNullabilityMode=Always;Pooling=true;Maximum Pool Size=2")
                    .UseAutoSyncStructure(false)
                    .UseNameConvert(NameConvertType.ToLower)
                    .Build();
            });

            fsql.Register("db1", () =>
            {
                return new FreeSqlBuilder()
                    .UseConnectionString(DataType.PostgreSQL, "Host=192.168.1.5;Port=5432;Username=postgres;Password=postgres123456;Database=test;ArrayNullabilityMode=Always;Pooling=true;Maximum Pool Size=2")
                    .UseAutoSyncStructure(false)
                    .UseNameConvert(NameConvertType.ToLower)
                    .Build();
            });

            var list = fsql.Use("db1").Select<ChameleonApp>().Where(a => a.IsDeleted == false).ToList();

            Assert.AreEqual(100, list.Count);
        }
    }
}