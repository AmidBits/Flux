using System.Linq;
using System.Reflection;

namespace Flux.Model
{
	public static partial class RulesEngine
	{
		public static System.Linq.Expressions.Expression BuildExpression<T>(Rule rule, System.Linq.Expressions.ParameterExpression parameter)
		{
			var left = System.Linq.Expressions.MemberExpression.Property(parameter, rule.Name);
			var propertyType = typeof(T).GetProperty(rule.Name)?.PropertyType ?? throw new System.ArgumentNullException(nameof(rule), $"There is no property named {rule.Name} in the type.");

			if (System.Linq.Expressions.ExpressionType.TryParse(rule.Operator, out System.Linq.Expressions.ExpressionType binaryOperator))
			{
				var rightExpression = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.Value, propertyType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

				return System.Linq.Expressions.Expression.MakeBinary(binaryOperator, left, rightExpression);
			}
			else
			{
				var methodInfo = propertyType.GetMethods().Where(mi => mi.Name == rule.Operator && mi.GetParameters() is var p && p.Length == 1 && p[0].ParameterType.GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo())).First();
				var parameterType = methodInfo.GetParameters()[0].ParameterType;

				var argument = System.Linq.Expressions.Expression.Constant(System.Convert.ChangeType(rule.Value, parameterType, null) ?? throw new System.ArgumentNullException(nameof(rule), $"The rule value cannot be converted to {propertyType.Name}."));

				return System.Linq.Expressions.Expression.Call(left, methodInfo, argument);
			}
		}
	}
}
