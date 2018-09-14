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
        void ThrowException();
    }

    public class BusinessClass : IBusinessClass
    {
        public static IBusinessClass Instance = DynamicProxy.CreateProxyOfRealize<IBusinessClass, BusinessClass, DynamicProxyInterceptor>();

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

        public void ThrowException()
        {
            throw new ArgumentException("arguments can not be null");
        }
    }

    public class BusinessClassVirtual : IBusinessClass
    {
        [DynamicProxyAction]
        public virtual void Test()
        {
            //do nothing;
        }

        public virtual bool GetBool(bool bo)
        {
            return bo;
        }

        public virtual DateTime GetDateTime(DateTime time)
        {
            return time;
        }

        public virtual decimal GetDecimal(decimal dec)
        {
            return dec;
        }

        public virtual double GetDouble(double dou)
        {
            return dou;
        }

        public virtual float GetFloat(float fl)
        {
            return fl;
        }

        public virtual int GetInt(int age)
        {
            return age;
        }

        public virtual object GetObject(object obj)
        {
            return obj;
        }

        public virtual OperateResult GetOperateResult(int code, string message)
        {
            return new OperateResult { Code = code, Message = message };
        }

        public virtual List<OperateResult> GetOperateResults(List<OperateResult> operateResults)
        {
            return operateResults;
        }

        public virtual string GetString(string str)
        {
            return str;
        }

        public virtual void ThrowException()
        {
            throw new ArgumentException("arguments can not be null");
        }
    }
}