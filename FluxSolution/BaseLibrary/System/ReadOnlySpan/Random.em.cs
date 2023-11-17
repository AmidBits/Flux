namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>Returns a random element from the <see cref="System.ReadOnlySpan{T}"/>. Uses the specified <paramref name="rng"/> (default if null).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static T Random<T>(this System.ReadOnlySpan<T> source, System.Random? rng = null)
    {
      rng ??= new System.Random();

      return source[rng.Next(source.Length)];
    }

    /// <summary>Attempts to fetch a random element from the <see cref="System.ReadOnlySpan{T}"/> into <paramref name="result"/> and indicates whether successful. Uses the specified <paramref name="rng"/> (default if null).</summary>
    public static bool TryRandom<T>(this System.ReadOnlySpan<T> source, out T result, System.Random? rng = null)
    {
      try
      {
        result = Random(source, rng);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
