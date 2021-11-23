namespace Flux.Dsp
{
  /// <summary>An interface with the purpose of producing complex waves in the range [-1, 1].</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/Wave"/>
  /// <seealso cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables"/>
  public interface IOscillator
  {
    static IOscillator Empty => EmptyOscillator.Instance;

    double NextSample();

    private sealed class EmptyOscillator
      : IOscillator
    {
      public static IOscillator Instance = new EmptyOscillator();

      public double NextSample()
        => throw new System.NotImplementedException(nameof(EmptyOscillator));
    }
  }
}
