using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodeArts.Topic
{
    /// <summary>
    /// 寻找大于左面左右且小于右边所有的数
    /// 例如：1，2，4，3，5，6
    /// 结果：5
    /// </summary>
    [TestClass]
    public class CaculateMiddleNumber
    {
        static int _DEBUGCOUNT = 0;
        [TestMethod]
        public void Execute()
        {
            var sources = new int[][]
            {
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 3 },//空
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 3 },//空
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 8, 6 },//5
                new int[] { 1, 2, 3, 4, 2, 5, 6, 7, 1, 4 },//空
                new int[] { 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1 },//空
                new int[] { 1, 4, 2, 3, 5, 6, 7, 8, 9, 10},//5,6,7,8,9
                new int[] { 1, 4, 2, 3, 5, 4, 6, 7, 8, 9 },//空
                new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },//2,3,4,5,6,7,8,9
                new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 },//空
            };

            Trace.WriteLine("---以双链表消除法实现");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = GetResult(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---以分段计算法实现");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = ReturnIntArray(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }

            Trace.WriteLine("---双层循环嵌套实现LHP");
            foreach (var source in sources)
            {
                _DEBUGCOUNT = 0;

                var result = GetResultLHP(source);

                Trace.WriteLine($"总共执行了 {_DEBUGCOUNT} 次循环 \t 计算结果：{string.Join(",", result)}, ");
            }
        }

        #region 双链表消除法
        public IEnumerable<int> GetResult(int[] source)
        {
            LinkedList<int> result = new LinkedList<int>();

            bool preEnd = false;
            for (int i = 1, minIdex = 0, maxIndex = source.Length - 1; i < source.Length; i++)
            {
                _DEBUGCOUNT++;

                if (i >= maxIndex)
                    break;

                int min = source[minIdex];
                int current = source[i];
                int max = source[maxIndex];

                if (min >= max)
                {
                    RemoveLast(result, max);
                    break;
                }

                if (min < current && current < max && !preEnd)
                {
                    result.AddLast(current);
                    minIdex = i;
                }
                else if (current <= min)
                {
                    RemoveLast(result, current);
                }
                else if (current >= max)
                {
                    preEnd = true;
                }
                else
                {
                    RemoveLast(result, min);
                }
            }

            return result;
        }

        private void RemoveLast(LinkedList<int> result, int min)
        {
            while (result.Count > 0 && result.Last.Value >= min)
            {
                _DEBUGCOUNT++;
                result.RemoveLast();
            }
        }
        #endregion

        #region 以分段计算法实现
        /// <summary>
        /// 找出大于前面所有数，小于后面所有数的数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<int> ReturnIntArray(int[] input)
        {
            var result = new List<int>();

            for (int i = 1; i < input.Length - 1; i++)
            {
                _DEBUGCOUNT++;

                var beforeMax = BeforeMax(input, i + 1);
                var afterMin = AfterMin(input, i + 1);
                if (input[i] >= beforeMax && input[i] < afterMin)
                    result.Add(input[i]);
            }

            return result;
        }

        private static int BeforeMax(int[] input, int beforeNum)
        {
            var max = input[0];

            for (int i = 0; i < beforeNum; i++)
            {
                _DEBUGCOUNT++;

                if (input[i] > max)
                    max = input[i];
            }

            return max;
        }

        private static int AfterMin(int[] input, int afterNumber)
        {
            var min = input[afterNumber];

            for (int i = afterNumber; i < input.Length; i++)
            {
                _DEBUGCOUNT++;

                if (input[i] < min)
                    min = input[i];
            }

            return min;
        }
        #endregion

        #region 循环嵌套
        public List<int> GetResultLHP(int[] arr)
        {
            List<int> arry = new List<int>();

            for (int i = 1; i < arr.Length - 1; i++)
            {
                _DEBUGCOUNT++;

                var isBreak = false;
                for (int j = 0; j < arr.Length; j++)
                {
                    _DEBUGCOUNT++;

                    if (j != i)
                    {
                        if (j < i)
                        {
                            if (arr[i] <= arr[j])
                            {
                                isBreak = true;
                                break;
                            }
                        }
                        else
                        {
                            if (arr[i] >= arr[j])
                            {
                                isBreak = true;
                                break;
                            }
                        }
                    }
                }
                if (!isBreak)
                {
                    arry.Add(arr[i]);
                }
            }

            return arry;
        }
        #endregion
    }
}
