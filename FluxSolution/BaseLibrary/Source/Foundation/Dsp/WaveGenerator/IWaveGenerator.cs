namespace Flux.Dsp
{
  /// <summary>An interface with the purpose of generating singular bipolar waveforms in the range [-1, 1] based on a specified phase in the range [0, 2PI].</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/Waveform"/>
  /// <seealso cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables"/>
  public interface IWaveGenerator
  {
    static IWaveGenerator Empty => EmptyWaveGenerator.Instance;

    /// <seealso cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    /// <param name="phase2Pi">Phase is the position of a point in time (an instant) on a waveform cycle. A complete cycle is defined as the interval required for the waveform to return to its arbitrary initial value. The graph to the right shows how one cycle constitutes 360° of phase. The graph also shows how phase is sometimes expressed in radians, where one radian of phase equals approximately 57.3°.</param>
    /// <returns>A wave sample in the [-1, 1] range.</returns>
    double GenerateWave(double phase);

    private sealed class EmptyWaveGenerator
      : IWaveGenerator
    {
      public static IWaveGenerator Instance = new EmptyWaveGenerator();

      public double GenerateWave(double phase)
        => throw new System.NotImplementedException(nameof(EmptyWaveGenerator));
    }
  }
}
