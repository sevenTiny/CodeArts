<Query Kind="Program">
  <NuGetReference>Bogus</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Bogus</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	Console.WriteLine(JsonConvert.SerializeObject(GetEnumerableData()));
}

IEnumerable<Customer> GetEnumerableData()
{
	//Randomizer.Seed = new Random(123456);

	return new Faker<Customer>()
		.RuleFor(c => c.Id, Guid.NewGuid())
		.RuleFor(c => c.Name, f => f.Company.CompanyName())
		.RuleFor(c => c.Address, f => f.Address.FullAddress())
		.RuleFor(c => c.City, f => f.Address.City())
		.RuleFor(c => c.Country, f => f.Address.Country())
		.RuleFor(c => c.ZipCode, f => f.Address.ZipCode())
		.RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())
		.RuleFor(c => c.Email, f => f.Internet.Email())
		.RuleFor(c => c.ContactName, (f, c) => f.Name.FullName())
		.RuleFor(c => c.Orders, f =>
			new Faker<Order>()
				.RuleFor(o => o.Id, Guid.NewGuid)
				.RuleFor(o => o.Date, f => f.Date.Past(3))
				.RuleFor(o => o.OrderValue, f => f.Finance.Amount(0, 10000))
				.RuleFor(o => o.Shipped, f => f.Random.Bool(0.9f))
				.Generate(f.Random.Number(10)).ToList())
 		.Generate(10);
}

// You can define other methods, fields, classes and namespaces here

class Customer
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Address { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
	public string ZipCode { get; set; }
	public string Phone { get; set; }
	public string Email { get; set; }
	public string ContactName { get; set; }
	public IEnumerable<Order> Orders { get; set; }
}

class Order
{
	public Guid Id { get; set; }
	public DateTime Date { get; set; }
	public Decimal OrderValue { get; set; }
	public bool Shipped { get; set; }
}