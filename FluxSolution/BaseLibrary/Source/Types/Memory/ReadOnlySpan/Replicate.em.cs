namespace Flux
{
  public static partial class ReadOnlySpanEm
  {
    /// <summary>Returns the source replicated (copied) the specified number of times.</summary>
    public static T[] Replicate<T>(this System.ReadOnlySpan<T> source, int count)
    {
      var sourceLength = source.Length;
      var span = new T[sourceLength * (count + 1)];
      for (var index = 0; index < count; index++)
        source.CopyTo(span, count * index);
      return span;
    }
  }
}
