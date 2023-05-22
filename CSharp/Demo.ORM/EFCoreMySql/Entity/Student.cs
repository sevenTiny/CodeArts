using System;
using System.Collections.Generic;

namespace EFCoreMySql.Entity
{
    public partial class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime? SchoolTime { get; set; }
    }
}
