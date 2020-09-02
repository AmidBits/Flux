namespace Flux.Random
{
  public static class NumberGenerator
  {
    private static System.Threading.ThreadLocal<System.Random> m_crng = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.Cryptographic());
    /// <summary>Get or set the static pseudo random number generator.</summary>
#pragma warning disable CS8603 // Possible null reference return.
    public static System.Random Crypto => m_crng.Value;
#pragma warning restore CS8603 // Possible null reference return.

    private static System.Threading.ThreadLocal<System.Random> m_prng = new System.Threading.ThreadLocal<System.Random>(() => new System.Random());
    /// <summary>Get or set the static pseudo random number generator.</summary>
#pragma warning disable CS8603 // Possible null reference return.
    public static System.Random Pseudo => m_prng.Value;
#pragma warning restore CS8603 // Possible null reference return.
  }
}
