namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a random element from the <paramref name="source"/>. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException"/>
    public static char Random(this System.Text.StringBuilder source, System.Random? rng = null)
      => source[(rng ?? System.Random.Shared).Next(source.Length)];

    /// <summary>
    /// <para>Attempts to fetch a random element from the <paramref name="source"/> into <paramref name="result"/> and returns whether successful. Uses the specified <paramref name="rng"/>, or <see cref="System.Random.Shared"/> if null.</para>
    /// </summary>
    public static bool TryRandom(this System.Text.StringBuilder source, out char result, System.Random? rng = null)
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
