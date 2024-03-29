﻿/*
     一个填空题最多有10个填空，每个填空最多5个答案，前提条件，开启按空给分并且不区分顺序，并且每个填空的得分相同(简化需求了)
    用户在作答完成之后，需要计算出这个填空题能得多少分。(每个填空只能得一次分，且需要按照最多正确的填空来得分)
    举例：
    
    正确答案 
    填空1： A，B
    填空2： A，C
    填空3： B
    
    用户答案：
    填空1： A（正确）
    填空2： B（正确）
    填空3： C（正确）
    
    正确答案 
    填空1： A，B
    填空2： A，C
    填空3： D
    
    用户答案：
    填空1： A（正确）
    填空2： B （正确）
    
    数学问题
    有一个一维数组a，长度为n，n小于等于10
    有一个数组集合b，长度为n，n小于等于10，集合每一项都是一个数组，长度为m，m小于等于5
    求解：一维数组中的每一项是否被数组集合中某一个数组包含，如果包含则为true，如果不包含则为false。另：数组集合中每一个数组只能使用一次，且需要保证最终为true的项最多。
 */
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CodeArts.Topic
{
    [TestClass]
    public class AnswerMatch
    {
        #region 全排列算法
        [TestMethod]
        [DataRow("A,B,C", "A,B|A,C|B")]
        [DataRow("A,B,C,D", "A,B|A,C|B|D,E,F")]
        [DataRow("A,B,C,D,E", "A,B|A,C|B|D,E,F|E")]
        [DataRow("A,B,C,D,E", "A,B|A,C|C|D,E,F|E")]
        [DataRow("A,B,C,D,E", "A,B|A,C|C,D|E,F|E")]
        [DataRow("A,B,C,D,E,F", "A,B|A,C|C,D|E,F|E|F")]
        [DataRow("A,B,C,D,E,F,A", "A,B|A,C|C,D|E,F|E|F|A,C,E")]
        [DataRow("A,B,C,D,E,F,A,B", "A,B|A,C|C,D|E,F|E|F|A,C,E|D")]
        //[DataRow("A,B,C,D,E,F,A,B,C", "A,B|A,C|C,D|E,F|E|F|A,C,E|D|C")]
        //[DataRow("A,B,C,D,E,F,G,H,I,J", "A,B|A,C|C,D|E,F|E|F|G|H|I|O")]
        public void Method1(string input, string answer)
        {
            //输入答案数组
            string[] inputs = input.Split(',');
            //标准答案
            string[][] answers = answer.Split('|').Select(t => t.Split(',')).ToArray();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            //1. 转成1/0方形矩阵
            /*
                A=[1,1,0,0]
                B=[1,0,1,0]
                C=[0,1,0,0]
                D=[0,0,0,1]
             */
            var rec = inputs.Select(t => Enumerable.Range(0, inputs.Length).Select(i => i < answers.Length ? (answers[i].Contains(t) ? 1 : 0) : 0).ToArray()).ToArray();

            //Debug时候输出方形矩阵
            for (int i = 0; i < inputs.Length; i++)
                Trace.WriteLine($"{inputs[i]}=[{string.Join(",", rec[i])}]");

            //2. 进行全排列，找到最优解
            /*
                1,0,0,0 A
                1,0,0,0 A
                1,0,0,0 A
                1,0,0,1 A
                1,0,1,0 A,C
                1,0,1,0 A,C
                ...
            */
            var max = new Store() { Sum = 0, Indexs = new int[inputs.Length], IndexsTmp = new int[inputs.Length] };

            GetMax(0, rec, new bool[inputs.Length], 0, new List<int>(), max);

            stopwatch.Stop();

            //输出结果
            Trace.WriteLine($"{inputs.Length} Inputs\tElapsedMilliseconds={stopwatch.ElapsedMilliseconds}\tresult={string.Join(",", max.Levels.Select(t => inputs[t]))}\tscore={max.Levels.Count}");
        }
        private void GetMax(int level, int[][] rec, bool[] hasExist, int lineSum, List<int> selected, Store max)
        {
            //当前层结果
            var currentLevel = rec[level];
            //当前层是否最后一层
            bool isLast = level >= hasExist.Length - 1;
            //当前链路的标记情况
            bool[] lineExist = hasExist.Clone() as bool[];

            for (int i = 0; i < currentLevel.Length; i++)
            {
                var store = new List<int>(selected);
                var origin = lineExist[i];

                if (currentLevel[i] == 1)
                {
                    //如果已经使用过该位置数据作为有效数据，则不再选择
                    if (lineExist[i])
                        continue;

                    lineExist[i] = true;
                    max.IndexsTmp[level] = i;
                    store.Add(level);
                }
                else if (isLast)
                {
                    //最后一层不是1就不用继续计算了
                    continue;
                }

                var sum = lineSum + currentLevel[i];

                //最后一层
                if (isLast)
                {
                    if (sum > max.Sum)
                    {
                        max.Sum = sum;
                        max.Indexs = max.IndexsTmp.Clone() as int[];
                        max.Levels = store;
                    }

                    continue;
                }

                GetMax(level + 1, rec, lineExist, sum, store, max);

                lineExist[i] = origin;
            }
        }
        class Store
        {
            public int Sum { get; set; }
            /// <summary>
            /// 层级（也就是答案）
            /// </summary>
            public List<int> Levels { get; set; }
            /// <summary>
            /// 锚中的索引index
            /// </summary>
            public int[] Indexs { get; set; }
            public int[] IndexsTmp { get; set; }
        }
        #endregion

        #region 投票选举法(暂时这个方法不准，在8个的时候计算是错误的)
        [TestMethod]
        //[DataRow("A,B,C", "A,B|A,C|B", 3)]
        //[DataRow("A,B,C,D", "A,B|A,C|B|D,E,F", 4)]
        //[DataRow("A,B,C,D,E", "A,B|A,C|B|D,E,F|E", 0)]
        //[DataRow("A,B,C,D,E", "A,B|A,C|C|D,E,F|E", 0)]
        [DataRow("A,B,C,D,E", "A,B|A,C|C,D|E,F|E", 0)]
        [DataRow("A,B,C,D,E,F", "A,B|A,C|C,D|E,F|E|F", 0)]
        [DataRow("A,B,C,D,E,F,A", "A,B|A,C|C,D|E,F|E|F|A,C,E", 0)]
        [DataRow("A,B,C,D,E,F,A,B", "A,B|A,C|C,D|E,F|E|F|A,C,E|D", 0)]
        //[DataRow("A,B,C,D,E,F,A,B,C", "A,B|A,C|C,D|E,F|E|F|A,C,E|D|C")]
        //[DataRow("A,B,C,D,E,F,G,H,I,J", "A,B|A,C|C,D|E,F|E|F|G|H|I|O")]
        public void Method2(string input, string answer, int exPectedScore)
        {
            //输入答案数组
            string[] inputs = input.Split(',');
            //标准答案
            string[][] answers = answer.Split('|').Select(t => t.Split(',')).ToArray();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            //1. 转成1/0方形矩阵
            /*
                A=[1,1,0,0]
                B=[1,0,1,0]
                C=[0,1,0,0]
                D=[0,0,0,1]
             */
            var rec = inputs.Select(t => Enumerable.Range(0, inputs.Length).Select(i => i < answers.Length ? (answers[i].Contains(t) ? 1d : 0) : 0).ToArray()).ToArray();

#if DEBUG
            //Debug时候输出方形矩阵
            for (int i = 0; i < inputs.Length; i++)
                Trace.WriteLine($"{inputs[i]}=[{string.Join(",", rec[i])}]");
#endif
            //票数容器
            var tickts = new double[rec.Length];

            for (int i = 0; i < rec.Length; i++)
            {
                var currentLine = rec[i];
                //每列的权重
                var weight = (double)1 / currentLine.Count(t => t == 1);

                for (int j = 0; j < rec.Length; j++)
                {
                    var currentPoint = currentLine[j];

                    if (currentPoint == 0)
                        continue;

                    var add = tickts[j] + weight;

                    tickts[j] = add > 1 ? 1 : add;
                }
            }

            //整理票数
            var result = Math.Round(tickts.Sum(), MidpointRounding.AwayFromZero);

            stopwatch.Stop();

            //输出结果
            Trace.WriteLine($"{inputs.Length} Inputs\tElapsedMilliseconds={stopwatch.ElapsedMilliseconds}\ttickts={string.Join(",", tickts.Select(t => t.ToString("0.##")))}\tsum={tickts.Sum().ToString("0.##")}\tscore={result}");
        }
        #endregion

        #region 匈牙利算法：https://zhuanlan.zhihu.com/p/96229700
        [TestMethod]
        //[DataRow("A,B,C", "A,B|A,C|B", 3)]
        //[DataRow("A,B,C,D", "A,B|A,C|B|D,E,F", 4)]
        //[DataRow("A,B,C,D,E", "A,B|A,C|B|D,E,F|E", 0)]
        //[DataRow("A,B,C,D,E", "A,B|A,C|C|D,E,F|E", 0)]
        [DataRow("A,B,C,D,E", "A,B|A,C|C,D|E,F|E", 0)]
        [DataRow("A,B,C,D,E,F", "A,B|A,C|C,D|E,F|E|F", 0)]
        [DataRow("A,B,C,D,E,F,A", "A,B|A,C|C,D|E,F|E|F|A,C,E", 0)]
        [DataRow("A,B,C,D,E,F,A,B", "A,B|A,C|C,D|E,F|E|F|A,C,E|D", 0)]
        //[DataRow("A,B,C,D,E,F,A,B,C", "A,B|A,C|C,D|E,F|E|F|A,C,E|D|C")]
        //[DataRow("A,B,C,D,E,F,G,H,I,J", "A,B|A,C|C,D|E,F|E|F|G|H|I|O")]
        public void Method3(string input, string answer, int exPectedScore)
        {
            //输入答案数组
            string[] inputs = input.Split(',');
            //标准答案
            string[][] answers = answer.Split('|').Select(t => t.Split(',')).ToArray();

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            //1. 转成1/0方形矩阵
            /*
                A=[1,1,0,0]
                B=[1,0,1,0]
                C=[0,1,0,0]
                D=[0,0,0,1]
             */
            var rec = inputs.Select(t => Enumerable.Range(0, inputs.Length).Select(i => i < answers.Length ? (answers[i].Contains(t) ? 1d : 0) : 0).ToArray()).ToArray();

#if DEBUG
            //Debug时候输出方形矩阵
            for (int i = 0; i < inputs.Length; i++)
                Trace.WriteLine($"{inputs[i]}=[{string.Join(",", rec[i])}]");
#endif

            int M = rec.Length;                     //M表示集合的元素数量
            var Map = new int[rec.Length][];        //邻接矩阵存图
            var p = new int[rec.Length];            //记录当前右侧元素所对应的左侧元素
            var vis = new bool[rec.Length];         //记录右侧元素是否已被访问过

            //开始算法
            int cnt = 0;
            for (int i = 1; i <= M; ++i)
            {
                vis.Initialize();
                if (match(i))
                    cnt++;
            }

            bool match(int i)
            {
                for (int j = 1; j <= M; ++j)
                    if (Map[i][j]==1 && !vis[j]) //有边且未访问
                    {
                        vis[j] = true;                 //记录状态为访问过
                        if (p[j] == 0 || match(p[j])) //如果暂无匹配，或者原来匹配的左侧元素可以找到新的匹配
                        {
                            p[j] = i;    //当前左侧元素成为当前右侧元素的新匹配
                            return true; //返回匹配成功
                        }
                    }
                return false; //循环结束，仍未找到匹配，返回匹配失败
            }
        }
        #endregion
    }
}