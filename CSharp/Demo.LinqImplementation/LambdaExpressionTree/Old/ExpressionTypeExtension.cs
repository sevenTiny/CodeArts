using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTree
{
    public static class ExpressionTypeExtension
    {
        public static string TranslateExpressionType(this ExpressionType expressionType)
        {
            string result = string.Empty;
            switch (expressionType)
            {
                case ExpressionType.Equal:
                    result = " = ";
                    break;
                case ExpressionType.GreaterThan:
                    result = " > ";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    result = " >= ";
                    break;
                case ExpressionType.LessThan:
                    result = " < ";
                    break;
                case ExpressionType.LessThanOrEqual:
                    result = " <= ";
                    break;
                case ExpressionType.NotEqual:
                    result = " != ";
                    break;
                case ExpressionType.AndAlso:
                    result = " And ";
                    break;
                case ExpressionType.OrElse:
                    result = " Or ";
                    break;
            }
            return result;
        }
    }
}
