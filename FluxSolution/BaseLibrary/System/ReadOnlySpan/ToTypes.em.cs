namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Array"/> from <paramref name="source"/> with a <paramref name="preLength"/> and a <paramref name="postLength"/>.</para>
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
      source.CopyTo(new System.Span<T>(target).Slice(preLength, source.Length));
      return target;
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Array"/> from <paramref name="source"/> with a <paramref name="preLength"/>, the <paramref name="source"/> elements at the <paramref name="indices"/>, and a <paramref name="postLength"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="preLength">The number of array slots to add before the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
    /// <param name="postLength">The number of array slots to add after the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
    /// <param name="indices">The indices and their order to copy to the new <see cref="System.Array"/>.</param>
    /// <returns></returns>
    public static T[] ToArray<T>(this System.ReadOnlySpan<T> source, int preLength, int postLength, params int[] indices)
    {
      var target = new T[preLength + indices.Length + postLength];

      for (var index = 0; index < indices.Length; index++)
        target[preLength + index] = source[indices[index]];

      return target;
    }

    /// <summary>Creates a new <see cref="System.Collections.Generic.HashSet{T}"/> from <paramref name="source"/> and the specified <paramref name="equalityComparer"/>, default if null.</summary>
    public static System.Collections.Generic.HashSet<T> ToHashSet<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      var target = new System.Collections.Generic.HashSet<T>(equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
      target.AddSpan(source);
      return target;
    }

    /// <summary>Creates a new <see cref="System.Collections.Generic.List{T}"/> from <paramref name="source"/> and optionally selected <paramref name="indices"/>.</summary>
    public static System.Collections.Generic.List<T> ToList<T>(this System.ReadOnlySpan<T> source, params int[] indices)
    {
      var target = new System.Collections.Generic.List<T>(source.Length);

      for (var index = 0; index < source.Length; index++)
      {
        if (indices is null || indices.Length == 0)
          target.Add(source[index]);
        else
        {
          var sourceIndex = indices[index];

          target.Add(source[sourceIndex]);
        }
      }

      return target;
    }

    public static string ToString<T>(this System.ReadOnlySpan<T> source)
    {
      var sb = new System.Text.StringBuilder();
      for (var index = 0; index < source.Length; index++)
        sb.Append(source[index]?.ToString() ?? string.Empty);
      return sb.ToString();
    }
  }
}
