namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Attempts to select a random element from the span in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool TryRandomElement<T>(this System.ReadOnlySpan<T> source, out T result, System.Random random)
    {
      if (random is not null && source.Length is var sourceLength && sourceLength > 0)
      {
        result = source[random.Next(sourceLength)];
        return true;
      }

      result = default!;
      return false;
    }
    /// <summary>Attempts to select a random element from the span in the output variable. Uses the .NET cryptographic random number generator.</summary>
    public static bool TryRandomElement<T>(this System.ReadOnlySpan<T> source, out T result)
      => TryRandomElement(source, out result, Randomization.NumberGenerator.Crypto);

    /// <summary>Returns a random element from the span. Uses the specified random number generator.</summary>
    public static T RandomElement<T>(this System.ReadOnlySpan<T> source, System.Random random)
      => TryRandomElement(source, out var re, random) ? re : throw new System.InvalidOperationException();
    /// <summary>Returns a random element from the span. Uses the .NET cryptographic random number generator.</summary>
    public static T RandomElement<T>(this System.ReadOnlySpan<T> source)
      => TryRandomElement(source, out var re, Randomization.NumberGenerator.Crypto) ? re : throw new System.InvalidOperationException();
  }
}
