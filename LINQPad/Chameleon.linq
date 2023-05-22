<Query Kind="Program">
  <NuGetReference>Chameleon.Common</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Chameleon.Common.Models</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	var dt = new DataOperationModel();

	dt.Datas = new List<DataModel>(){
		new DataModel() { MetaObjectName = "111",Fields = new List<FieldData>{ new FieldData { Code="Test",Value="value"} }}
	};
	
	Console.WriteLine(JsonConvert.SerializeObject(dt));
}

// You can define other methods, fields, classes and namespaces here

/// <summary>
/// 数据操作模型
/// </summary>
public class DataOperationModel
{
	/// <summary>
	/// create/edit/delete
	/// </summary>
	public string State { get; set; }
	/// <summary>
	/// 数据集合
	/// </summary>
	public List<DataModel> Datas { get; set; }
	/// <summary>
	/// 实体
	/// </summary>
	public string MetaObjectName { get; set; }
}

public class DataModel
{
	/// <summary>
	/// Id
	/// </summary>
	public string Id { get; set; }
	/// <summary>
	/// 字段
	/// </summary>
	public List<FieldData> Fields { get; set; }
}