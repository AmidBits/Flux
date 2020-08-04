namespace Flux.Random
{
  public static class NumberGenerator
  {
    private static System.Threading.ThreadLocal<System.Random> m_crng = new System.Threading.ThreadLocal<System.Random>(() => new Flux.Random.Cryptographic());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Crypto => m_crng?.Value ?? throw new System.NullReferenceException();

    private static System.Threading.ThreadLocal<System.Random> m_prng = new System.Threading.ThreadLocal<System.Random>(() => new System.Random());
    /// <summary>Get or set the static pseudo random number generator.</summary>
    public static System.Random Pseudo => m_prng?.Value ?? throw new System.NullReferenceException();
  }
}
