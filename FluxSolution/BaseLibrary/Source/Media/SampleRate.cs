namespace Flux.Media
{
  public class SampleRate
  {
    public Frequency Frequency { get; set; }

    public SampleRate(Frequency frequency)
      => Frequency = frequency;
    public SampleRate()
      : this(new Frequency(44100))
    {
    }
  }
}
