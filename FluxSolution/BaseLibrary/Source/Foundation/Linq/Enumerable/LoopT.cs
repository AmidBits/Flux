namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<TResult> Loop<TValue, TResult>(this System.Func<TValue> initializerSelector, System.Func<TValue, int, bool> conditionSelector, System.Func<TValue, int, TValue> iteratorSelector, System.Func<TValue, int, TResult> resultSelector)
    {
      var index = 0;
      for (var current = initializerSelector(); conditionSelector(current, index); current = iteratorSelector(current, index), index++)
        yield return resultSelector(current, index);
    }
    public static System.Collections.Generic.IEnumerable<TResult> Loop<TValue, TResult>(this System.Func<TValue> initializerSelector, System.Func<TValue, bool> conditionSelector, System.Func<TValue, TValue> iteratorSelector, System.Func<TValue, TResult> resultSelector)
      => Loop(initializerSelector, (e, i) => conditionSelector(e), (e, i) => iteratorSelector(e), (e, i) => resultSelector(e));
  }
}
