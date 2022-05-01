'''
参考：https://mp.weixin.qq.com/s/gAL0_fKFrsp3sjdfITQqkQ
Dynaconf 是一个Python的第三方模块，旨在成为在 Python 中管理配置的最佳选择。
它可以从各种来源读取设置，包括环境变量、文件、服务器配置等。
它适用于任何类型的 Python 程序，包括 Flask 和 Django 扩展。
'''
'''
使用说明
1. 安装：
pip install dynaconf

2. 初始化：
在你的项目的根目录中运行  dynaconf init  命令。
cd path/to/your/project/
dynaconf init -f toml

会有类似如下的输出，说明初始化完成：
⚙️ Configuring your Dynaconf environment
------------------------------------------
🐍 The file `config.py` was generated.

🎛️ settings.toml created to hold your settings.

🔑 .secrets.toml created to hold your secrets.

🙈 the .secrets.* is also included in `.gitignore`
beware to not push your secrets to a public repo.

🎉 Dynaconf is configured! read more on https://dynaconf.com

刚刚初始化的时候我们选择了 toml 格式。实际上你还可以选择  toml|yaml|json|ini|py ，不过 toml 是默认的，也是最推荐的配置格式。

初始化完成后会创建以下文件：
.
├── config.py # 需要被导入的配置脚本
├── .secrets.toml # 像密码等敏感信息配置
└── settings.toml # 应用配置

3. 编写配置
初始化完成后你就可以编写你的配置，编辑settings.toml：

key = "value"
a_boolean = false
number = 1234
a_float = 56.8
a_list = [1, 2, 3, 4]
a_dict = {hello="world"}

[a_dict.nested]
other_level = "nested value"

然后就可以在你的代码中导入并使用这些配置：
from config import settings

assert settings.key == "value"
assert settings.number == 789
assert settings.a_dict.nested.other_level == "nested value"
assert settings['a_boolean'] is False
assert settings.get("DONTEXIST", default=1) == 1

如果是密码等敏感信息，你可以配置在 .secrets.toml 中:
password = "s3cr3t"
token = "dfgrfg5d4g56ds4gsdf5g74984we5345-"
message = "This file doesn't go to your pub repo"

.secrets.toml 文件会被自动加入到 .gitignore 文件中，这些信息不会被上传到Git仓库上。
'''
from config import settings

assert settings.key == '7tiny'