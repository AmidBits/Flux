namespace Flux.Dsp.AudioProcessor
{
  public enum InverterMode
  {
    Bypass,
    /// <summary>Invert the negative polarity only.</summary>
    NegativePeekOnly,
    /// <summary>Invert each polarity independently of each other.</summary>
    PeeksIndependently,
    /// <summary>Invert across both polarities.</summary>
    PeekToPeek,
    /// <summary>Invert the positive polarity only.</summary>
    PositivePeekOnly
  }

  public class MonoInverter
    : IWaveProcessorMono
  {
    public InverterMode Mode { get; internal set; }

    public MonoInverter(InverterMode mode)
      => Mode = mode;
    public MonoInverter()
      : this(InverterMode.PeekToPeek)
    {
    }

    public double ProcessAudio(double sample) => (Mode switch
    {
      InverterMode.PeekToPeek => (-sample),
      InverterMode.PeeksIndependently when sample < 0 => (-sample - 1),
      InverterMode.NegativePeekOnly when sample < 0 => (-sample - 1),
      InverterMode.PeeksIndependently when sample > 0 => (-sample + 1),
      InverterMode.PositivePeekOnly when sample > 0 => (-sample + 1),
      _ => (sample),
    });

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
