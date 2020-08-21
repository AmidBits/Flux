using System;

namespace Flux
{
  public static partial class XtendCollections
  {
    /// <summary>Applies an accumulator over a sequence. The specified seed value is used as the inital accumulator value, and the specified functions are used to select the accumulated value and result value, respectively.</summary>
    /// <remarks>Unlike the LINQ versions, this one also includes the ordinal index of the element while aggregating.</remarks>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TAccumulate"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="seed">The initial accumulator value.</param>
    /// <param name="func"></param>
    /// <param name="resultSelector"></param>
    /// <returns></returns>
    public static TResult Aggregate<T, TAccumulate, TResult>(this System.Collections.Generic.IEnumerable<T> source, TAccumulate seed, System.Func<TAccumulate, T, int, TAccumulate> func, System.Func<TAccumulate, int, TResult> resultSelector)
    {
      if (func is null) throw new System.ArgumentNullException(nameof(func));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      var accumulated = seed;

      var index = 0;

      foreach (var element in source.ThrowOnNull())
      {
        accumulated = func(accumulated, element, index++);
      }

      return resultSelector(accumulated, index);
    }
  }
}
