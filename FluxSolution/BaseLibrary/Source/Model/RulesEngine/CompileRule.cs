namespace Flux.Model
{
	public static partial class RulesEngine
	{
		public static System.Func<T, bool> CompileRule<T>(Rule rule)
		{
			var parameter = System.Linq.Expressions.Expression.Parameter(typeof(T));
			var expression = BuildExpression<T>(rule, parameter);

			return System.Linq.Expressions.Expression.Lambda<System.Func<T, bool>>(expression, parameter).Compile();
		}
	}
}
