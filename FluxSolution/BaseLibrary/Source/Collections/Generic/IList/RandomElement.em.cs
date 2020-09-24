using System.Linq;

namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returns a random element from the list in the output variable. Uses the specified random number generator (the .NET cryptographic if null).</summary>
    public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result, System.Random rng)
    {
      if (source.ThrowOnNull().Any())
      {
        result = source[(rng ?? Flux.Random.NumberGenerator.Crypto).Next(source.Count)];
        return true;
      }

      result = default!;
      return false;
    }
    /// <summary>Returns a random element from the list in the output variable. Uses the .NET cryptographic random number generator.</summary>
    public static bool RandomElement<T>(this System.Collections.Generic.IList<T> source, out T result)
      => RandomElement(source, out result, Flux.Random.NumberGenerator.Crypto);
  }
}
