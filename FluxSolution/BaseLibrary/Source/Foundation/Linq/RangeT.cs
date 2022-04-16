namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<TResult> Range<TValue, TResult>(this System.Func<TValue> initializerSelector, System.Func<TValue, bool> conditionSelector, System.Func<TValue, TValue> iteratorSelector, System.Func<TValue, TResult> resultSelector)
    {
      for (var current = initializerSelector(); conditionSelector(current); current = iteratorSelector(current))
        yield return resultSelector(current);
    }
  }
}
