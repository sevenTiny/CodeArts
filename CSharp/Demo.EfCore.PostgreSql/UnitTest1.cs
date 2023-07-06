namespace Demo.EfCore.PostgreSql
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void InsertData()
        {
            using (var dbContext = new ExampleDbContext())
            {
                dbContext.Students.Add(new Student
                {
                    Id = Guid.NewGuid(),
                    Name = "Seven",
                    Age = 99,
                    Class = "History"
                });

                dbContext.SaveChanges();
            }
        }
    }
}