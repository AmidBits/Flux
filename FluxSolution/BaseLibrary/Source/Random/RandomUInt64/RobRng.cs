namespace Flux.Random
{
  /// <summary>SimpleRng is a simple random number generator based on George Marsaglia's MWC (multiply with carry) generator.</summary>
  /// <remarks>Although it is very simple, it passes Marsaglia's DIEHARD series of random number generator tests.</remarks>
  public sealed class PakaRng
    : ARandomUInt64
  {
    public const short Int16MaxPrime = 32749;
    public const int Int32MaxPrime = 2147483647;
    public const long Int64MaxPrime = 9223372036854775807L;

    [System.CLSCompliant(false)] public const ushort UInt16MaxPrime = 65521;
    [System.CLSCompliant(false)] public const uint UInt32MaxPrime = 4294967291U;
    [System.CLSCompliant(false)] public const ulong UInt64MaxPrime = 18446744073709551557UL;

    new public static System.Random Shared { get; } = new PakaRng();

    private static ulong m_h;
    private static ulong m_l;

    [System.CLSCompliant(false)]
    public PakaRng(ulong h, ulong l)
    {
      m_h = h;
      m_l = l;
    }

    public PakaRng(int seed1, int seed2) : this(unchecked((uint)seed1), unchecked((uint)seed2)) { }

    public PakaRng()
    {
      m_h = (ulong)System.Diagnostics.Stopwatch.GetTimestamp() & Int64MaxPrime;
      m_l = (ulong)System.Diagnostics.Stopwatch.GetTimestamp() & Int32MaxPrime;
    }

    internal override ulong SampleUInt64()
    {
      m_h = (m_h + Int64MaxPrime) * UInt64MaxPrime;
      m_l = (m_l + Int32MaxPrime) * UInt32MaxPrime;

      return unchecked((m_h << 31) ^ m_l);
    }
  }
}
