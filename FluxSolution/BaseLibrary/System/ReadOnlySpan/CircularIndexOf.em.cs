namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the index in <paramref name="source"/> where the rotation of the <paramref name="target"/> begins, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static int CircularIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
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
  }
}
