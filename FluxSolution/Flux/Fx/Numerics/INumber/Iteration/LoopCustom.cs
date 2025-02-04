namespace Flux
{
  public static partial class Iteration
  {
    /// <summary>
    /// <para>Creates a sequence of numbers based on the standard for statement using <see cref="System.Func{T, TResult}"/> (for <paramref name="source"/>) and <see cref="System.Func{T1, T2, TResult}"/> (for <paramref name="condition"/> and <paramref name="iterator"/>).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source">Initializes the current-loop-value.</param>
    /// <param name="condition">Conditionally allows/denies the loop to continue. In parameters are (current-loop-value, index).</param>
    /// <param name="iterator">Advances the loop. In parameters (current-loop-value, index).</param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopCustom<TNumber>(this TNumber source, System.Func<TNumber, int, bool> condition, System.Func<TNumber, int, TNumber> iterator)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      var index = 0;

      for (var current = source; condition(current, index); current = iterator(current, index))
      {
        yield return current;

        index++;
      }
    }
  }
}
