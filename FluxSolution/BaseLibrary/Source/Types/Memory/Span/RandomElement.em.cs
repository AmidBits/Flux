namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a random element from the sequence in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool RandomElement<T>(this System.Span<T> source, out T result, System.Random rng)
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
    public static bool RandomElement<T>(this System.Span<T> source, out T result)
      => RandomElement(source, out result, Flux.Random.NumberGenerator.Crypto);

    /// <summary>Returns a random element from the list in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result, System.Random rng)
      => RandomElement((System.Span<T>)(T[])source, out result, rng);
    /// <summary>Returns a random element from the list in the output variable. Uses the .NET cryptographic random number generator.</summary>
    public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result)
      => RandomElement((System.Span<T>)(T[])source, out result, Flux.Random.NumberGenerator.Crypto);
  }
}