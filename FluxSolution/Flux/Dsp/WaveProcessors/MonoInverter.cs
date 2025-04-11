namespace Flux.Dsp.WaveProcessors
{
  /// <summary>Inverts the wave signal.</summary>
  public record class MonoInverter
    : IMonoWaveProcessable
  {
    public MonoInverterMode Mode { get; internal set; }

    public MonoInverter(MonoInverterMode mode)
      => Mode = mode;
    public MonoInverter()
      : this(MonoInverterMode.PeekToPeek)
    { }

    public double ProcessMonoWave(double wave) => (Mode switch
    {
      MonoInverterMode.PeekToPeek => -wave,
      MonoInverterMode.PeeksIndependently when wave < 0 => -wave - 1,
      MonoInverterMode.NegativePeekOnly when wave < 0 => -wave - 1,
      MonoInverterMode.PeeksIndependently when wave > 0 => -wave + 1,
      MonoInverterMode.PositivePeekOnly when wave > 0 => -wave + 1,
      _ => wave,
    });

    public Waves.IWaveMono<double> ProcessMonoWave(Waves.IWaveMono<double> mono) => (Waves.WaveMono<double>)ProcessMonoWave(mono.Wave);

    public static double InvertNegativePeekOnly(double sample)
      => sample < 0.0 ? -sample - 1.0 : sample;
    public static double InvertPeeksIndependently(double sample)
      => sample < 0.0 ? -sample - 1.0 : sample > 0.0 ? -sample + 1.0 : sample;
    public static double InvertPeekToPeek(double sample)
      => -sample;
    public static double InvertPositivePeekOnly(double sample)
      => sample > 0.0 ? -sample + 1.0 : sample;
  }
}
