namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns a random element from the sequence in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool RandomElement<T>(this System.ReadOnlySpan<T> source, out T result, System.Random rng)
    {
      if (rng is null) throw new System.ArgumentNullException(nameof(rng));

      if (source.Length > 0)
      {
        result = source[rng.Next(source.Length)];
        return true;
      }

      result = default!;
      return false;
    }
    /// <summary>Returns a random element from the sequence in the output variable. Uses the .NET cryptographic random number generator.</summary>
    public static bool RandomElement<T>(this System.ReadOnlySpan<T> source, out T result)
      => RandomElement(source, out result, Randomization.NumberGenerator.Crypto);
  }
}
