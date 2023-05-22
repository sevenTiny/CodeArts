using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTree
{
    public class ExpressionAnalyze : ExpressionVisitor
    {
        public StringBuilder sb = new StringBuilder();

        public string Sql
        {
            get { return sb.ToString(); }
        }

        public Expression ResolveExpression(Expression<Func<Person, bool>> expression)
        {
            return this.Visit(expression.Body);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            //Console.WriteLine($"BinaryType={node.NodeType}");
            sb.Append("(");
            Visit(node.Left);
            sb.Append(node.NodeType.TranslateExpressionType());
            Visit(node.Right);
            sb.Append(")");
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            //Console.WriteLine($"Constant={node.Value}");
            if (node.Type == typeof(string))
            {
                sb.Append("'" + node.Value + "'");
            }
            else if (node.Type == typeof(int))
            {
                sb.Append(node.Value);
            }
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            //Console.WriteLine($"MemberName={node.Member.Name}");
            sb.Append(node.Member.Name);
            return node;
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            //Console.WriteLine($"ParamName={p.Name}");
            return p;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitBlock(BlockExpression node)
        {
            return base.VisitBlock(node);
        }

        protected override CatchBlock VisitCatchBlock(CatchBlock node)
        {
            return base.VisitCatchBlock(node);
        }
        protected override Expression VisitConditional(ConditionalExpression node)
        {
            return base.VisitConditional(node);
        }
        protected override Expression VisitDebugInfo(DebugInfoExpression node)
        {
            return base.VisitDebugInfo(node);
        }
        protected override Expression VisitDefault(DefaultExpression node)
        {
            return base.VisitDefault(node);
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            return base.VisitDynamic(node);
        }

        protected override ElementInit VisitElementInit(ElementInit node)
        {
            return base.VisitElementInit(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            return base.VisitExtension(node);
        }
        protected override Expression VisitGoto(GotoExpression node)
        {
            return base.VisitGoto(node);
        }

        protected override Expression VisitIndex(IndexExpression node)
        {
            return base.VisitIndex(node);
        }

        protected override Expression VisitInvocation(InvocationExpression node)
        {
            return base.VisitInvocation(node);
        }

        protected override Expression VisitLabel(LabelExpression node)
        {
            return base.VisitLabel(node);
        }

        protected override LabelTarget VisitLabelTarget(LabelTarget node)
        {
            return base.VisitLabelTarget(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return base.VisitLambda(node);
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            return base.VisitListInit(node);
        }

        protected override Expression VisitLoop(LoopExpression node)
        {
            return base.VisitLoop(node);
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            return base.VisitMemberAssignment(node);
        }
        protected override MemberBinding VisitMemberBinding(MemberBinding node)
        {
            return base.VisitMemberBinding(node);
        }
        protected override Expression VisitMemberInit(MemberInitExpression node)
        {
            return base.VisitMemberInit(node);
        }
        protected override MemberListBinding VisitMemberListBinding(MemberListBinding node)
        {
            return base.VisitMemberListBinding(node);
        }
        protected override MemberMemberBinding VisitMemberMemberBinding(MemberMemberBinding node)
        {
            return base.VisitMemberMemberBinding(node);
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var re = Expression.Lambda(node).Compile().DynamicInvoke().ToString();
            return base.VisitMethodCall(node);
        }
        protected override Expression VisitNew(NewExpression node)
        {
            return base.VisitNew(node);
        }
        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return base.VisitNewArray(node);
        }
        protected override Expression VisitRuntimeVariables(RuntimeVariablesExpression node)
        {
            return base.VisitRuntimeVariables(node);
        }
        protected override Expression VisitSwitch(SwitchExpression node)
        {
            return base.VisitSwitch(node);
        }
        protected override SwitchCase VisitSwitchCase(SwitchCase node)
        {
            return base.VisitSwitchCase(node);
        }
        protected override Expression VisitTry(TryExpression node)
        {
            return base.VisitTry(node);
        }
        protected override Expression VisitTypeBinary(TypeBinaryExpression node)
        {
            return base.VisitTypeBinary(node);
        }
        protected override Expression VisitUnary(UnaryExpression node)
        {
            return base.VisitUnary(node);
        }
    }
}
