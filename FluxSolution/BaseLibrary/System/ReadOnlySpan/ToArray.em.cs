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
  }
}
