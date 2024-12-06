namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.HashSet{T}"/> with all elements from <paramref name="source"/> and the specified <paramref name="equalityComparer"/> (or default if null).</para>
    /// </summary>
    public static System.Collections.Generic.HashSet<T> ToHashSet<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int additionalCapacity = 0)
    {
      var target = new System.Collections.Generic.HashSet<T>(source.Length + additionalCapacity, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
      target.AddSpan(source);
      return target;
    }
  }
}
