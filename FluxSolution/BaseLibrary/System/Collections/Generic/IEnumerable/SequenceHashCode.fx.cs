namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Computes a hash code, representing all elements in <paramref name="source"/>, using the .NET built-in <see cref="System.HashCode"/> functionality.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int SequenceHashCode<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source.Aggregate(new System.HashCode(), (hc, e) => { hc.Add(e); return hc; }, hc => hc.ToHashCode());

    /// <summary>
    /// <para>Computes a hash code, representing all elements in <paramref name="source"/>, by xor'ing the <see cref="{T}.GetHashCode()"/> of the elements.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int SequenceHashCodeByXor<T>(this System.Collections.Generic.IEnumerable<T> source)
      => source.Aggregate(0, (hc, e) => hc ^ (e?.GetHashCode() ?? 0));
  }
}
