# 01-Ocelot极简单Demo及负载均衡的配置

## 本Demo包含部分：
1. Ocelot网关api的创建
2. 三个下游Demo微服务接口的创建

## 详：

## 一、创建下游微服务
1. 新建3个asp.net core webapi项目,分别命名为Service1，Service2，Service3
2. 将三个项目的启动端口分别设置为39991，39992，39993
3. 将默认的/api/values接口的返回值稍做修改，让其比较明显对应三个端口

## 二、Ocelot网关Api
1. 新建一个asp.net core webapi项目，命名为OcelotGateway
2. 该项目引入Nuget：	
	Ocelot 最新稳定版
3. 根目录添加配置文件Ocelot.json,添加上述三个api地址信息
4. 调整OcelotGateway项目的Programe和Start需要配置的相关代码（详情见代码）

## 三、运行
1. 先同时运行三个下游微服务，可以看到三个浏览器窗口分别返回了三个接口对应的返回值
2. 运行OcelotGateway项目，可以看到返回了第一个下游微服务的返回值
3. 刷新Gateway项目的窗口，可以看到根据我们配置的轮询复杂均衡策略分别轮询地返回了三个接口的结果


（完）

7tiny
2019年3月7日23点49分
