namespace Flux.Media
{
  public class SampleRate
  {
    public Units.Frequency Frequency { get; set; }

    public SampleRate(Units.Frequency frequency)
      => Frequency = frequency;
    public SampleRate()
      : this(new Units.Frequency(44100))
    {
    }
  }
}
