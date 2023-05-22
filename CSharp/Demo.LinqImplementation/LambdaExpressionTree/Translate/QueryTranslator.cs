using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTree.Translate
{
    internal class QueryTranslator : ExpressionVisitor
    {
        StringBuilder _execute;
        StringBuilder _where;

        private const string _SELECT_ = "[_SELECT_]";
        private const string _LIMIT_ = "[_LIMIT_]";
        private const string _WHERE_ = "[_WHERE_]";
        private const string _TABLE_ = "[_TABLE_]";

        /// <summary>
        /// 类型和别名字典，用于查询中相同类型使用相同的别名
        /// </summary>
        private Dictionary<Type, string> _typeAliasDic = new Dictionary<Type, string>();


        private const int op_where = 2;

        private Stack<int> _operateStack = new Stack<int>();

        internal QueryTranslator() { }

        internal string Translate(Expression expression)
        {
            //init
            this._execute = new StringBuilder();
            _where = new StringBuilder();
            _operateStack.Clear();

            _where.Append("WHERE 1=1");
            _execute.Append("[_SELECT_] FROM [_TABLE_] [_WHERE_] [_LIMIT_]");

            this.Visit(expression);


            return this._execute.ToString().Replace(_WHERE_, _where.ToString());
        }

        private static Expression StripQuotes(Expression e)
        {
            while (e.NodeType == ExpressionType.Quote)
            {
                e = ((UnaryExpression)e).Operand;
            }
            return e;
        }

        protected override Expression VisitMethodCall(MethodCallExpression m)
        {
            if (m.Method.DeclaringType != typeof(Queryable))
            {
                throw new NotSupportedException("individe operate");
            }

            var methodName = m.Method.Name;

            if (methodName == "Where")
            {
                _operateStack.Push(op_where);

                _where.Append(" AND (");
                this.Visit(m.Arguments[1]);
                _where.Append(")");
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[1]);
                this.Visit(lambda.Body);

                _operateStack.Pop();
                return m;
            }
            else if (methodName == "FirstOrDefault")
            {
                _execute.Replace(_LIMIT_, "LIMIT 1");

                this.Visit(m.Arguments[0]);
                //_execute.Append(") AS T WHERE ");
                LambdaExpression lambda = (LambdaExpression)StripQuotes(m.Arguments[0]);
                this.Visit(lambda.Body);
                return m;
            }
            else if (methodName == "ToList")
            {
                _execute.Replace(_SELECT_, "SELECT *");
                _execute.Replace(_LIMIT_, string.Empty);
            }
            throw new NotSupportedException(string.Format("The method '{0}' is not supported", m.Method.Name));
        }

        protected override Expression VisitUnary(UnaryExpression u)
        {
            switch (u.NodeType)
            {
                case ExpressionType.Not:
                    _execute.Append(" NOT ");
                    this.Visit(u.Operand);
                    break;
                default:
                    this.Visit(u.Operand);
                    break;
            }
            return u;
        }

        protected override Expression VisitBinary(BinaryExpression b)
        {
            this.Visit(b.Left);
            switch (b.NodeType)
            {
                case ExpressionType.And:
                    _execute.Append(" AND ");
                    break;
                case ExpressionType.Or:
                    _execute.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    _execute.Append(" = ");
                    break;
                case ExpressionType.NotEqual:
                    _execute.Append(" <> ");
                    break;
                case ExpressionType.LessThan:
                    _execute.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _execute.Append(" <= ");
                    break;
                case ExpressionType.GreaterThan:
                    _execute.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _execute.Append(" >= ");
                    break;
                default:
                    throw new NotSupportedException(string.Format("The binary operator '{0}' is not supported", b.NodeType));
            }
            this.Visit(b.Right);
            _execute.Append(")");
            return b;
        }

        protected override Expression VisitConstant(ConstantExpression c)
        {
            IQueryable q = c.Value as IQueryable;
            if (q != null)
            {
                // assume constant nodes w/ IQueryables are table references
                //sb.Append("SELECT * FROM ");
                if (_operateStack.FirstOrDefault() != op_where)
                {
                    _execute.Clear();
                    _execute.Append(q.ElementType.Name);
                }
            }
            else if (c.Value == null)
            {
                _execute.Append("NULL");
            }
            else
            {
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        _execute.Append(((bool)c.Value) ? 1 : 0);
                        break;
                    case TypeCode.String:
                        _execute.Append("'");
                        _execute.Append(c.Value);
                        _execute.Append("'");
                        break;
                    case TypeCode.Object:
                        throw new NotSupportedException(string.Format("The constant for '{0}' is not supported", c.Value));
                    default:
                        _execute.Append(c.Value);
                        break;
                }
            }
            return c;
        }

        protected override Expression VisitMember(MemberExpression m)
        {
            if (m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
            {
                _execute.Append(m.Member.Name);
                return m;
            }
            throw new NotSupportedException(string.Format("The member '{0}' is not supported", m.Member.Name));
        }
    }
}
