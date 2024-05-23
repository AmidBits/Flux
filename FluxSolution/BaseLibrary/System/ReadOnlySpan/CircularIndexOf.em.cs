namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the index in <paramref name="source"/> where the rotation of the target begins, or -1 if not found. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static int CircularIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (source.Length != target.Length)
        return -1; // If length is different, target cannot be a rotation in source. They have to be equal in length.

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var ros = new T[source.Length * 2];

      source.CopyTo(ros);
      source.CopyTo(ros, source.Length);

      return new System.ReadOnlySpan<T>(ros).IndexOf(target, equalityComparer);
    }
  }
}
