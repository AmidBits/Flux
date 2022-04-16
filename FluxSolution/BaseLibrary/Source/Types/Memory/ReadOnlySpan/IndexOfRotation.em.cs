namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Returns the index within source where the rotation of the target begins, or -1 if not found.</summary>
    public static int IndexOfRotation<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> target)
    {
      if (source.Length != target.Length)
        return -1; // If length is different, target cannot be a rotation in source. They have to be equal in length.

      return new System.ReadOnlySpan<T>(source.Replicate(1)).IndexOf(target);
    }
  }
}
