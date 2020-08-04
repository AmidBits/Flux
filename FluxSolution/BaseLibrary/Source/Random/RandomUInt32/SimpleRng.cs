namespace Flux.Random
{
  /// <summary>SimpleRng is a simple random number generator based on George Marsaglia's MWC (multiply with carry) generator.</summary>
  /// <remarks>Although it is very simple, it passes Marsaglia's DIEHARD series of random number generator tests.</remarks>
  public class SimpleRng
    : RandomUInt32
  {
    public enum SeedEnum
    {
      MarsagliaDefault,
      TimerMechanism
    }

    private static uint m_w;
    private static uint m_z;

    [System.CLSCompliant(false)]
    public SimpleRng(uint seed1, uint seed2)
    {
      m_w = seed1;
      m_z = seed2;
    }
    public SimpleRng(int seed1, int seed2) : this(unchecked((uint)seed1), unchecked((uint)seed2)) { }
    public SimpleRng(SeedEnum seed)
    {
      switch (seed)
      {
        case SeedEnum.MarsagliaDefault:
          m_w = 521288629;
          m_z = 362436069;
          break;
        case SeedEnum.TimerMechanism:
          m_w = (uint)((ulong)System.Diagnostics.Stopwatch.GetTimestamp() & 0xFFFFFFFF);
          m_z = (uint)((ulong)System.Diagnostics.Stopwatch.GetTimestamp() & 0xFFFFFFFF);
          break;
      }
    }
    public SimpleRng() : this(SeedEnum.TimerMechanism) { }

    internal override uint SampleUInt32() => unchecked(((m_z = 36969 * (m_z & 65535) + (m_z >> 16)) << 16) + (m_w = 18000 * (m_w & 65535) + (m_w >> 16)));
  }
}
