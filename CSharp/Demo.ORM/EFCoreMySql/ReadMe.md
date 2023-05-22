# Nuget包

- Microsoft.EntityFrameworkCore.Tools
- Pomelo.EntityFrameworkCore.MySql

# 执行命令从数据库生成实体类

> 先打开 Package Manager Console 窗口，选择项目，并将项目设置为启动项目（framework或者netcore项目，不支持.netstandard类库）

输入命令

```nuget
Scaffold-DbContext -Connection "server=127.0.0.1;Port=3306;database=SevenTinyTest;uid=sa;pwd=xxxxxx;Allow User Variables=true;SslMode=none;" Pomelo.EntityFrameworkCore.MySql -OutputDir "要输出的文件夹"
```
