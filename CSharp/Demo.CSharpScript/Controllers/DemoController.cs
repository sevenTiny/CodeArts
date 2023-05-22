using Demo.CSharpScript.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CS = Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Demo.CSharpScript.Controllers
{
    public class DemoController : Controller
    {
        const string SCRIPT_FILE_NAME_Before = "before.script";
        const string SCRIPT_FILE_NAME_After = "after.script";

        /// <summary>
        /// 保存脚本
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public IActionResult ConfigSave(string before, string after)
        {
            ViewData["Before"] = GetOrSetScript(SCRIPT_FILE_NAME_Before, before);
            ViewData["After"] = GetOrSetScript(SCRIPT_FILE_NAME_After, after);
            //重定向到获取数据的action
            return Redirect("/Demo/GetData");
        }

        public IActionResult GetData(string name)
        {
            try
            {
                //获取配置脚本的内容
                ViewData["Before"] = GetOrSetScript(SCRIPT_FILE_NAME_Before);
                ViewData["After"] = GetOrSetScript(SCRIPT_FILE_NAME_After);

                //测试数据
                var data = DemoModel.GetDemoDatas();

                //执行before脚本，控制传入的字段
                var ss = ExecuteBeforeScript(name);

                //利用before脚本中返回的值过滤Name字段（模拟利用before脚本控制搜索条件）
                if (!string.IsNullOrEmpty(ss))
                {
                    data = data.Where(t => t.Name.Contains(ss)).ToList();
                }

                //执行After脚本（模拟利用After脚本控制返回结果）
                var result = ExecuteAfterScript(data);

                ViewData["AfterData"] = result;
                ViewData["ErrorData"] = "查询成功！";
            }
            catch (Exception ex)
            {
                ViewData["ErrorData"] = ex.ToString();
            }
            return View();
        }

        /// <summary>
        /// 执行Before脚本
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string ExecuteBeforeScript(string name)
        {
            //拼接脚本，脚本里的方法不能被动过，只能动方法内容，正式环境这里要进行校验
            StringBuilder builder = new StringBuilder();
            builder.Append("public class DataDeal");
            builder.Append("{");
            builder.Append(GetOrSetScript(SCRIPT_FILE_NAME_Before));
            builder.Append("}");
            builder.Append("return DataDeal.Before(arg1);");

            var result = CS.CSharpScript.RunAsync<string>(builder.ToString(), globals: new Arg { arg1 = name }, globalsType: typeof(Arg)).Result;
            return result.ReturnValue;
        }

        /// <summary>
        /// 执行After脚本
        /// </summary>
        /// <param name="demoModels"></param>
        /// <returns></returns>
        private List<DemoModel> ExecuteAfterScript(List<DemoModel> demoModels)
        {
            //拼接脚本，脚本里的方法不能被动过，只能动方法内容，正式环境这里要进行校验
            StringBuilder builder = new StringBuilder();
            builder.Append("public class DataDeal");
            builder.Append("{");
            builder.Append(GetOrSetScript(SCRIPT_FILE_NAME_After));
            builder.Append("}");
            builder.Append("return DataDeal.After(models);");

            var result = CS.CSharpScript.RunAsync<List<DemoModel>>(builder.ToString()
                //using 提前引入
                , ScriptOptions.Default
                    .AddReferences("System.Linq", "Demo.CSharpScript")
                    .AddImports("System.Collections.Generic", "System.Linq", "Demo.CSharpScript.Models")
                , globals: new Arg { models = demoModels }
                , globalsType: typeof(Arg)).Result;
            return result.ReturnValue;
        }

        /// <summary>
        /// 获取或设置脚本内容
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        private string GetOrSetScript(string file, string script = null)
        {
            //修改
            if (!string.IsNullOrEmpty(script))
            {
                if (!System.IO.File.Exists(file))
                    System.IO.File.Create(file);

                //false means overwrite the file
                using (StreamWriter writer = new StreamWriter(file, false))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(script);
                }

                return script;
            }

            //查询
            if (System.IO.File.Exists(file))
                return System.IO.File.ReadAllText(file, Encoding.Default);

            return null;
        }
    }
}