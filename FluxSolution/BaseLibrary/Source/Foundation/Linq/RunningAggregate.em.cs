namespace Flux
{
  public static partial class Enumerable
  {
    public static System.Collections.Generic.IEnumerable<(TAccumulate cumulative, TSource element, int index)> RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate initial, System.Func<TAccumulate, TSource, int, TAccumulate> func)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (func is null) throw new System.ArgumentNullException(nameof(func));

      var cumulative = initial;

      var index = 0;

      foreach (var item in source)
      {
        cumulative = func(cumulative, item, index);

        yield return (cumulative, item, index);

        index++;
      }
    }

    public static int RunningAggregate<TSource, TAccumulate>(this System.Collections.Generic.IEnumerable<TSource> source, TAccumulate initial, System.Func<TAccumulate, TSource, int, TAccumulate> func, out TAccumulate cumulative)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (func is null) throw new System.ArgumentNullException(nameof(func));

      cumulative = initial;

      var index = 0;

      foreach (var item in source)
      {
        cumulative = func(cumulative, item, index);

        index++;
      }

      return index;
    }
  }
}
