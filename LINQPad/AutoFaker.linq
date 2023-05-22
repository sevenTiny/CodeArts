<Query Kind="Program">
  <NuGetReference>AutoBogus</NuGetReference>
  <NuGetReference>Bogus</NuGetReference>
  <NuGetReference>Bogus.Tools.Analyzer</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>AutoBogus</Namespace>
  <Namespace>Bogus</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var data = new AutoFaker<ClassA>()
		.RuleForType(typeof(double), f => Math.Round(f.Random.Double(1, 100), 2))
		.Generate(2);

	Console.WriteLine(JsonConvert.SerializeObject(data.First()));
}

internal class ClassA
{
	public string PhoneNumber { get; set; }
	public string CultivationSchemeId { get; set; }
}