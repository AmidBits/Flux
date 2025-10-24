namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    extension<T>(System.ReadOnlySpan<T> source)
    {
      #region CircularCopyTo

      /// <summary>
      /// <para>Copies the specified <paramref name="count"/> from <paramref name="source"/> starting at <paramref name="sourceIndex"/> into <paramref name="target"/> at the specified <paramref name="targetIndex"/>. If the <paramref name="count"/> wraps the <paramref name="target"/>, it will be wrapped to the beginning in a circular fashion. The <paramref name="source"/> is treated the same way.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="sourceIndex"></param>
      /// <param name="target"></param>
      /// <param name="targetIndex"></param>
      /// <param name="count"></param>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public void CircularCopyTo(int sourceIndex, System.Span<T> target, int targetIndex, int count)
      {
        if (sourceIndex < 0 || sourceIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceIndex));
        if (targetIndex < 0 || targetIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetIndex));
        if (count < 0) throw new System.ArgumentOutOfRangeException(nameof(count));

        for (var index = 0; index < count; index++)
          target[(targetIndex + index) % target.Length] = source[(sourceIndex + index) % source.Length];
      }

      #endregion

      #region CircularIndexOf

      /// <summary>
      /// <para>Returns the index in <paramref name="source"/> where the rotation of the <paramref name="target"/> begins, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int CircularIndexOf(System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
      {
        if (source.Length >= target.Length) // If source length is less than target length, target cannot be a rotation within source.
        {
          equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

          for (var si = 0; si < source.Length; si++)
          {
            for (var ti = 0; ti < target.Length; ti++)
            {
              if (!equalityComparer.Equals(source[(si + ti) % source.Length], target[ti]))
                break;
              else if (ti == target.Length - 1)
                return si;
            }
          }
        }

        return -1;
      }

      #endregion
    }
  }
}
