namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Adds the elements in <paramref name="span"/> into <paramref name="source"/>.</summary>
    /// <remarks>No checks are being made and the behavior of the <see cref="System.Collections.Generic.ICollection{T}"/> applies.</remarks>
    public static void AddSpan<T>(this System.Collections.Generic.ICollection<T> source, System.ReadOnlySpan<T> span)
    {
      for (var index = 0; index < span.Length; index++)
        source.Add(span[index]);
    }
  }
}
