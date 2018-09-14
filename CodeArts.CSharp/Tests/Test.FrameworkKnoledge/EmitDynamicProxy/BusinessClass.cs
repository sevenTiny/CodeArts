using CodeArts.FrameworkKnowledge.EmitDynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Test.FrameworkKnoledge.EmitDynamicProxy
{
    public class OperateResult
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public interface IBusinessClass
    {
        void Test();
        bool GetBool(bool bo);
        int GetInt(int age);
        string GetString(string str);
        double GetDouble(double dou);
        decimal GetDecimal(decimal dec);
        float GetFloat(float fl);
        DateTime GetDateTime(DateTime time);
        object GetObject(object obj);
        OperateResult GetOperateResult(int code, string message);
        List<OperateResult> GetOperateResults(List<OperateResult> operateResults);
    }

    public class BusinessClass : IBusinessClass
    {
        public static IBusinessClass Instance = DynamicProxy.Create<IBusinessClass, BusinessClass, DynamicProxyInterceptor>();

        public static IBusinessClass Instance2 = new BusinessClass();

        [DynamicProxyAction]
        public void Test()
        {
            //do nothing;
        }

        public bool GetBool(bool bo)
        {
            return bo;
        }

        public DateTime GetDateTime(DateTime time)
        {
            return time;
        }

        public decimal GetDecimal(decimal dec)
        {
            return dec;
        }

        public double GetDouble(double dou)
        {
            return dou;
        }

        public float GetFloat(float fl)
        {
            return fl;
        }

        public int GetInt(int age)
        {
            return age;
        }

        public object GetObject(object obj)
        {
            return obj;
        }

        public OperateResult GetOperateResult(int code, string message)
        {
            return new OperateResult { Code = code, Message = message };
        }

        public List<OperateResult> GetOperateResults(List<OperateResult> operateResults)
        {
            return operateResults;
        }

        public string GetString(string str)
        {
            return str;
        }
    }
}