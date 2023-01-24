namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Attempts to select a random element from the span in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool TryRandomElement<T>(this System.ReadOnlySpan<T> source, out T result, System.Random rng)
    {
      if (rng is not null && source.Length is var sourceLength && sourceLength > 0)
      {
        result = source[rng.Next(sourceLength)];
        return true;
      }

      result = default!;
      return false;
    }

    /// <summary>Returns a random element from the span. Uses the specified random number generator.</summary>
    public static T RandomElement<T>(this System.ReadOnlySpan<T> source, System.Random rng)
      => TryRandomElement(source, out var re, rng ?? throw new System.ArgumentNullException(nameof(rng))) ? re : throw new System.InvalidOperationException();
  }
}
