namespace Flux
{
  public static partial class LinqEx
  {
    public static System.Func<TArgL, TArgR, TResult> CreateExpressionBinary<TArgL, TArgR, TResult>(System.Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.BinaryExpression> body)
    {
      if (body is null) throw new System.ArgumentNullException(nameof(body));

      var a = System.Linq.Expressions.Expression.Parameter(typeof(TArgL), @"a");
      var b = System.Linq.Expressions.Expression.Parameter(typeof(TArgR), @"b");

      try
      {
        return System.Linq.Expressions.Expression.Lambda<System.Func<TArgL, TArgR, TResult>>(body(a, b), a, b).Compile();
      }
      catch (System.Exception ex)
      {
        return delegate { throw new System.InvalidOperationException((ex.Message as string)); };
      }
    }
    public static System.Func<TArgL, TArgR, TResult> CreateExpressionBinaryWithCast<TArgL, TArgR, TResult>(System.Func<System.Linq.Expressions.Expression, System.Linq.Expressions.Expression, System.Linq.Expressions.BinaryExpression> body)
    {
      if (body is null) throw new System.ArgumentNullException(nameof(body));

      var a = System.Linq.Expressions.Expression.Parameter(typeof(TArgL), @"a");
      var b = System.Linq.Expressions.Expression.Parameter(typeof(TArgR), @"b");

      try
      {
        try
        {
          return System.Linq.Expressions.Expression.Lambda<System.Func<TArgL, TArgR, TResult>>(body(a, b), a, b).Compile();
        }
        catch (System.InvalidOperationException)
        {
          if (typeof(TArgL) != typeof(TResult) || typeof(TArgR) != typeof(TResult))
          {
            var castA = typeof(TArgL) == typeof(TResult) ? (System.Linq.Expressions.Expression)a : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Convert(a, typeof(TResult));
            var castB = typeof(TArgR) == typeof(TResult) ? (System.Linq.Expressions.Expression)b : (System.Linq.Expressions.Expression)System.Linq.Expressions.Expression.Convert(b, typeof(TResult));

            return System.Linq.Expressions.Expression.Lambda<System.Func<TArgL, TArgR, TResult>>(body(castA, castB), a, b).Compile();
          }
          else throw;
        }
      }
      catch (System.Exception ex)
      {
        return delegate { throw new System.InvalidOperationException((ex.Message as string)); };
      }
    }

    public static System.Func<TArg, TResult> CreateExpressionUnary<TArg, TResult>(System.Func<System.Linq.Expressions.Expression, System.Linq.Expressions.UnaryExpression> body)
    {
      if (body is null) throw new System.ArgumentNullException(nameof(body));

      System.Linq.Expressions.ParameterExpression v = System.Linq.Expressions.Expression.Parameter(typeof(TArg), "v");

      return System.Linq.Expressions.Expression.Lambda<System.Func<TArg, TResult>>(body(v), v).Compile();
    }
  }
}
