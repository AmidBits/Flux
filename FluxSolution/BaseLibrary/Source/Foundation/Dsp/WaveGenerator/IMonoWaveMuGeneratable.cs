namespace Flux.Dsp
{
  /// <summary>A wave sample generator of singular bipolar waveforms in the range [-1.0, 1.0]. The phase is in the range [0, 2PI].</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/Waveform"/>
  /// <seealso cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables"/>
  public interface IMonoWaveMuGeneratable
  {
    /// <seealso cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    /// <param name="phaseMu">Phase [0, 1] is the position of a point in time (an instant) on a waveform cycle. A complete cycle is defined as the interval required for the waveform to return to its arbitrary initial value. The graph to the right shows how one cycle constitutes 360° of phase. The graph also shows how phase is sometimes expressed in radians, where one radian of phase equals approximately 57.3°.</param>
    /// <returns>A wave sample in the range [-1, 1].</returns>
    double GenerateMonoWaveMu(double phaseMu);

    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to unit interval [0, 1).</summary>
    public static double AbsolutePhaseMu(double phaseMu)
      => phaseMu < 0
      ? (phaseMu % 1) + 1
      : phaseMu % 1;
  }
}