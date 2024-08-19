namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Adds all elements from <paramref name="other"/> to the current <see cref="System.Collections.Generic.List{T}"/>.</summary>
    public static void AddSpan<T>(this System.Collections.Generic.List<T> source, System.ReadOnlySpan<T> other)
    {
      var sourceCount = source.Count;
      System.Runtime.InteropServices.CollectionsMarshal.SetCount(source, sourceCount + other.Length);
      var target = System.Runtime.InteropServices.CollectionsMarshal.AsSpan(source);
      other.CopyTo(target[sourceCount..]);
    }
  }
}
