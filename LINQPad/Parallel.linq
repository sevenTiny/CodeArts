<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	Console.WriteLine("核心数="+Environment.ProcessorCount);
	var range = Enumerable.Range(1, 300);
	var opt = new ParallelOptions { MaxDegreeOfParallelism = 16 };
	Parallel.ForEach(range, opt, current =>
	{
		Console.WriteLine($"{DateTime.Now}\tcurrent={current}\tThreadId={Thread.CurrentThread.ManagedThreadId}");
		Thread.Sleep(30000);
	});
}

// You can define other methods, fields, classes and namespaces here
