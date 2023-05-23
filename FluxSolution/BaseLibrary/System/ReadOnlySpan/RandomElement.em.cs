namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Returns a random element from the span. Uses the specified random number generator.</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static T RandomElement<T>(this System.ReadOnlySpan<T> source, System.Random rng)
      => source[(rng ?? new System.Random()).Next(source.Length)];

    /// <summary>Attempts to select a random element from the span in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool TryRandomElement<T>(this System.ReadOnlySpan<T> source, out T result, System.Random rng)
    {
      try
      {
        result = RandomElement(source, rng);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
