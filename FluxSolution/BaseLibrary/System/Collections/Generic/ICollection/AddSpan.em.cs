namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Adds the <paramref name="items"/> to the <see cref="System.Collections.Generic.ICollection{T}"/>.</summary>
    /// <remarks>No checks are being made and the behavior of the <see cref="System.Collections.Generic.ICollection{T}.Add(T)"/> applies.</remarks>
    public static void AddSpan<T>(this System.Collections.Generic.ICollection<T> source, System.ReadOnlySpan<T> items)
    {
      for (var index = 0; index < items.Length; index++)
        source.Add(items[index]);
    }
  }
}
