using System.Linq;

namespace Flux.Csp
{
  public class MetaExpressionInteger
    : ExpressionInteger, IMetaExpression<int>
  {
    private readonly System.Collections.Generic.IList<IVariable<int>> support;

    public System.Collections.Generic.IList<IVariable<int>> Support
    {
      get { return this.support; }
    }

    public MetaExpressionInteger(Expression<int> left, Expression<int> right, System.Collections.Generic.IEnumerable<IVariable<int>> support)
      : base(left, right)
    {
      this.support = support.ToList();
    }

    public MetaExpressionInteger(int integer, System.Collections.Generic.IEnumerable<IVariable<int>> support)
      : base(integer)
    {
      this.support = support.ToList();
    }

    internal MetaExpressionInteger(VariableInteger variable,
      System.Func<ExpressionInteger, ExpressionInteger, int> evaluate,
      System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>> evaluateBounds,
      System.Func<ExpressionInteger, ExpressionInteger, Bounds<int>, ConstraintOperationResult> propagator,
      System.Collections.Generic.IEnumerable<IVariable<int>> support)
      : base(variable, evaluate, evaluateBounds, propagator)
    {
      this.support = support.ToList();
    }
  }
}
