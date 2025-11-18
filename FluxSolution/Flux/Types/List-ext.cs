namespace Flux
{
  public static partial class ListExtensions
  {
    extension<T>(System.Collections.Generic.List<T> source)
    {
      /// <summary>
      /// <para>Create a non-allocating <see cref="System.ReadOnlySpan{T}"/> to the internal array of a <see cref="System.Collections.Generic.List{T}"/>.</para>
      /// </summary>
      /// <returns></returns>
      public System.ReadOnlySpan<T> AsReadOnlySpan()
        => System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source);

      /// <summary>
      /// <para>Create a non-allocating <see cref="System.Span{T}"/> to the internal array of a <see cref="System.Collections.Generic.List{T}"/>.</para>
      /// </summary>
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
