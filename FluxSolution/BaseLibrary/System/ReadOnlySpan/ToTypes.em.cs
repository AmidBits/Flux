namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Array"/> with all elements from <paramref name="source"/>, and a <paramref name="preLength"/> and a <paramref name="postLength"/> number of default slots.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="preLength">The number of array slots to add before the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
    /// <param name="postLength">The number of array slots to add after the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
    /// <returns></returns>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source, int preLength, int postLength)
    {
      if (preLength < 0) throw new System.ArgumentOutOfRangeException(nameof(preLength));
      if (postLength < 0) throw new System.ArgumentOutOfRangeException(nameof(postLength));

      var target = new T[preLength + source.Length + postLength];
      source.CopyTo(new System.Span<T>(target, preLength, source.Length));
      return target;
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.HashSet{T}"/> with all elements from <paramref name="source"/> and the specified <paramref name="equalityComparer"/> (or default if null).</para>
    /// </summary>
    public static System.Collections.Generic.HashSet<T> ToHashSet<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int additionalCapacity = 0)
    {
      var target = new System.Collections.Generic.HashSet<T>(source.Length + additionalCapacity, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
      target.AddSpan(source);
      return target;
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.List{T}"/> with all elements from <paramref name="source"/>.</para>
    /// </summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, int additionalCapacity = 0)
    {
      var target = new System.Collections.Generic.List<T>(source.Length + additionalCapacity);
      target.AddRange(source);
      return target;
    }
  }
}
