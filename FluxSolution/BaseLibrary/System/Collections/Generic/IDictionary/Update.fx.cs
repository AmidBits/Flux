namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Updates <paramref name="source"/> with elements from the <paramref name="other"/> <see cref="System.Collections.Generic.IDictionary{TKey, TValue}"/> or enumerable of <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>.</para>
    /// <para>Essentially a merge of the elements in <paramref name="other"/> into <paramref name="source"/>. If keys are equal, <paramref name="other"/> values will overwrite the values in <paramref name="source"/>.</para>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="other"></param>
    public static void Update<TKey, TValue>(this System.Collections.Generic.IDictionary<TKey, TValue> source, System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TKey, TValue>> other)
    {
      foreach (var (key, value) in other)
        source[key] = value;
    }
  }
}
