namespace Flux
{
  public static partial class Iteration
  {
    /// <summary>
    /// <para>Creates a sequence of numbers based on the standard for statement using <see cref="System.Func{T, TResult}"/> (for <paramref name="initializer"/>) and <see cref="System.Func{T1, T2, TResult}"/> (for <paramref name="condition"/> and <paramref name="iterator"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="initializer"></param>
    /// <param name="condition"></param>
    /// <param name="iterator"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopCustom<TNumber>(System.Func<TNumber> initializer, System.Func<TNumber, int, bool> condition, System.Func<TNumber, int, TNumber> iterator)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var index = 0;
      for (var current = initializer(); condition(current, index); current = iterator(current, index), index++)
        yield return current;
    }

    /// <summary>
    /// <para>Creates a sequence of numbers starting at <paramref name="source"/> and using <see cref="System.Func{T1, T2, TResult}"/> (for <paramref name="condition"/> and <paramref name="iterator"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="condition"></param>
    /// <param name="iterator"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopCustom<TNumber>(this TNumber source, System.Func<TNumber, int, bool> condition, System.Func<TNumber, int, TNumber> iterator)
      where TNumber : System.Numerics.INumber<TNumber>
      => LoopCustom(() => source, condition, iterator);
  }
}
