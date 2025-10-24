namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region Count

      /// <summary>
      /// <para>Computes how many elements satisfying the <paramref name="predicate"/> is found in the <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int Count(System.Func<T, int, bool> predicate)
      {
        var count = 0;

        for (var index = source.Length - 1; index >= 0; index--)
          if (predicate(source[index], index))
            count++;

        return count;
      }

      /// <summary>
      /// <para>Computes how many elements satisfying the <paramref name="predicate"/> is found in the <paramref name="source"/>.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="predicate"></param>
      /// <returns></returns>
      public int Count(System.Func<T, bool> predicate)
        => Count(source, (e, i) => predicate(e));

      /// <summary>
      /// <para>Computes how many elements of <paramref name="target"/> is found in the <paramref name="source"/>. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int Count(T target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer)
      {
        equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

        return Count(source, e => equalityComparer.Equals(e, target));
      }

      #endregion
    }
  }
}
