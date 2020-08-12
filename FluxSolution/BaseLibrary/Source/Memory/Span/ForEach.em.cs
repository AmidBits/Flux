namespace Flux
{
  public static partial class XtensionsSpan
  {
    /// <summary>Determines whether the end of this ReadOnlySpan instance matches a specified target ReadOnlySpan.</summary>
    public static void ForEach<T>(this System.Span<T> source, System.Action<T> action)
    {
      var sourceLength = source.Length;

      for (var index = 0; index < sourceLength; index++)
        (action ?? throw new System.ArgumentNullException(nameof(action)))(source[index]);
    }
  }
}
