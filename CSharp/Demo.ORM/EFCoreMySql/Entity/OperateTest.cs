using System;
using System.Collections.Generic;

namespace EFCoreMySql.Entity
{
    public partial class OperateTest
    {
        public int Id { get; set; }
        public int Key2 { get; set; }
        public string StringKey { get; set; }
        public int IntKey { get; set; }
        public int? IntNullKey { get; set; }
        public DateTime? DateNullKey { get; set; }
        public DateTime? DateTimeNullKey { get; set; }
        public double? DoubleNullKey { get; set; }
        public float? FloatNullKey { get; set; }
    }
}
