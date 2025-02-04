namespace Flux.Dsp
{
  /// <summary>A wave sample generator of singular bipolar waveforms in the range [-1.0, 1.0]. The phase is in the range [0, 1].</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/Waveform"/>
  /// <seealso cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables"/>
  public interface IMonoWaveUiGeneratable
  {
    /// <seealso cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    /// <param name="phaseUi">Phase [0, 1] is the position of a point in time (an instant) on a waveform cycle. A complete cycle is defined as the interval required for the waveform to return to its arbitrary initial value. The graph to the right shows how one cycle constitutes 360° of phase. The graph also shows how phase is sometimes expressed in radians, where one radian of phase equals approximately 57.3°.</param>
    /// <returns>A wave sample in the range [-1, 1].</returns>
    IWaveMono<double> GenerateMonoWaveUi(double phaseUi);

    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to unit interval [0, 1).</summary>
    public static double AbsolutePhaseUi(double phaseUi)
      => phaseUi < 0
      ? (phaseUi % 1) + 1 // Align phased correctly on positive side.
      : phaseUi % 1;
  }
}
