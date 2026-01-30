namespace Flux.Probabilities
{
  // A crypto-strength, threadsafe, all-static RNG.
  // Still not a great API. We can do better.
  public static class BetterRandom
  {
    private static readonly System.Threading.ThreadLocal<System.Security.Cryptography.RandomNumberGenerator> crng = new(System.Security.Cryptography.RandomNumberGenerator.Create);
    private static readonly System.Threading.ThreadLocal<byte[]> bytes = new(() => new byte[sizeof(int)]);

    public static int NextInt()
    {
      if (bytes.Value is null) throw new System.NullReferenceException();

      crng.Value!.GetBytes(bytes.Value);

      return System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(bytes.Value) & int.MaxValue;
    }

    public static double NextDouble()
    {
      while (true)
      {
        long x = NextInt() & 0x001FFFFF;
        x <<= 31;
        x |= (long)NextInt();
        double n = x;
        const double d = 1L << 52;
        double q = n / d;
        if (q != 1.0)
          return q;
      }
    }
  }
}
