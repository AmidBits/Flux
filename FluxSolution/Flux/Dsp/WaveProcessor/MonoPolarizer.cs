namespace Flux.Dsp.WaveProcessor
{
  public enum MonoPolarizerMode
  {
    Bypass,
    BipolarToUnipolarNegative,
    BipolarToUnipolarPositive,
    UnipolarNegativeToBipolar,
    UnipolarPositiveToBipolar,
  }

  /// <summary>Enables inline conversion of the polarity range, e.g. bipolar to unipolar (or the other way around).</summary>
  /// <see href="https://en.wikipedia.org/wiki/Polarization_(waves)"/>
  public record class MonoPolarizer
    : IMonoWaveProcessable
  {
    public MonoPolarizerMode Mode { get; }

    public MonoPolarizer(MonoPolarizerMode mode)
    {
      Mode = mode;
    }
    public MonoPolarizer()
      : this(MonoPolarizerMode.BipolarToUnipolarPositive)
    { }

    public double ProcessMonoWave(double wave) => (Mode switch
    {
      MonoPolarizerMode.BipolarToUnipolarNegative => wave / 2.0 - 0.5,
      MonoPolarizerMode.BipolarToUnipolarPositive => wave / 2.0 + 0.5,
      MonoPolarizerMode.UnipolarNegativeToBipolar => wave < 0.0 ? wave * 2.0 + 1.0 : 0.0,
      MonoPolarizerMode.UnipolarPositiveToBipolar => wave > 0.0 ? wave * 2.0 - 1.0 : 0.0,
      _ => wave,
    });

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);

    public static double ApplyBipolarToUnipolarNegative(double sample)
      => sample / 2.0 - 0.5;
    public static double ApplyBipolarToUnipolarPositive(double sample)
      => sample / 2.0 + 0.5;
    public static double ApplyUnipolarNegativeToBipolar(double sample)
      => sample < 0.0 ? sample * 2.0 + 1.0 : 0.0;
    public static double ApplyUnipolarPositiveToBipolar(double sample)
      => sample > 0.0 ? sample * 2.0 - 1.0 : 0.0;
  }
}
