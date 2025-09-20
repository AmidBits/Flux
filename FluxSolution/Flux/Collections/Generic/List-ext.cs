namespace Flux
{
  public static partial class Lists
  {
    extension<T>(System.Collections.Generic.List<T> source)
    {
      /// <summary>
      /// <para>Flux non-allocating conversion (casting) to <see cref="System.ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.ReadOnlySpan<T> AsReadOnlySpan()
        => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source);

      /// <summary>
      /// <para>Flux non-allocating conversion (casting) to <see cref="System.Span{T}"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <returns></returns>
      public System.Span<T> AsSpan()
        => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source);
    }

    ///// <summary>
    ///// <para></para>
    ///// </summary>
    ///// <typeparam name="T"></typeparam>
    ///// <param name="source"></param>
    ///// <param name="equalityComparer"></param>
    //public static void RemoveAdjacentDuplicates<T>(this System.Collections.Generic.List<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    //{
    //  if (source.Count < 2) return;

    //  equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

    //  var removeIndex = 1;

    //  for (var index = 1; index < source.Count; index++)
    //    if (!equalityComparer.Equals(source[removeIndex - 1], source[index]))
    //      source[removeIndex++] = source[index];

    //  source.RemoveRange(removeIndex, source.Count - removeIndex);
    //}
  }
}
