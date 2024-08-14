namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Creates a new Van Eck's sequence, starting with the specified <paramref name="number"/> (where 0 yields the original sequence).</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="number"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TSelf> GetVanEckSequence<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (number < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(number));

      var lasts = new System.Collections.Generic.Dictionary<TSelf, TSelf>();
      var last = number;

      for (var index = TSelf.Zero; ; index++)
      {
        yield return last;

        TSelf next;

        if (lasts.TryAdd(last, index)) next = TSelf.Zero;
        else
        {
          next = index - lasts[last];
          lasts[last] = index;
        }

        last = next;
      }
    }
  }
}
