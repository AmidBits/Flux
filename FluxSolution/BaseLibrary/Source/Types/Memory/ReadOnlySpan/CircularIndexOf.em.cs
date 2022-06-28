namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the index within source where the rotation of the target begins, or -1 if not found.</summary>
    public static int CircularIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target, System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      if (source.Length != target.Length)
        return -1; // If length is different, target cannot be a rotation in source. They have to be equal in length.

      var ros = new T[source.Length * 2];

      source.CopyTo(ros);
      source.CopyTo(ros, source.Length);

      return new System.ReadOnlySpan<T>(ros).IndexOf(target, equalityComparer);
    }

    public static int CircularIndexOf<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
      => CircularIndexOf(source, target, System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
