/**
 * 字节跳动一面编程题
 * 一天服务登陆日志
 * UserId，LoginTime,LogoutTime(h)
 * 求：用户在线峰值和持续时间
 * */
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace CodeArts.Topic
{
    [TestClass]
    public class LoginLogoutLogsAnalysis
    {
        [TestMethod]
        public void Main()
        {
            //模拟数据
            var logs = new List<Log>{
              new Log{ LoginTime=2, LogoutTime=5 },
              new Log{ LoginTime=3, LogoutTime=6 },
              new Log{ LoginTime=3, LogoutTime=4 },
              new Log{ LoginTime=4, LogoutTime=8 },
              new Log{ LoginTime=4, LogoutTime=9 },
              new Log{ LoginTime=4, LogoutTime=10 },
              new Log{ LoginTime=3, LogoutTime=4 },
              new Log{ LoginTime=4, LogoutTime=8 },
              new Log{ LoginTime=5, LogoutTime=6 },
          };

            //当天数据容器
            var array = new int[24];

            //初始化数据
            foreach (var item in logs)
            {
                //只记录当前在线人数即可
                for (int i = item.LoginTime; i < item.LogoutTime; i++)
                {
                    array[i]++;
                }
            }

            //统计部分
            int biggest = 0;        //峰值
            int biggestLength = 0;  //持续时长
            int biggestTime = 0;    //最大开始时间

            for (int i = 0; i < array.Length; i++)
            {
                //当前在线人数
                var currentValue = array[i];

                //存储最大峰值
                if (currentValue > biggest)
                {
                    biggest = currentValue;
                    biggestLength = 0;
                    biggestTime = i;
                }

                if (currentValue == biggest)
                    biggestLength++;
            }

            //输出部分
            for (int i = 0; i < array.Length; i++)
            {
                Trace.WriteLine($"当前时间:{i} \t 在线人数：{array[i]}");
            }

            Trace.WriteLine($"当天最大峰值：{biggest}人，开始时间：{biggestTime}点，持续时间：{biggestLength}h");
        }

        /// <summary>
        /// 登录日志
        /// </summary>
        public class Log
        {
            /// <summary>
            /// 登录时间
            /// </summary>
            public int LoginTime { get; set; }
            /// <summary>
            /// 登出时间
            /// </summary>
            public int LogoutTime { get; set; }
        }
    }
}
