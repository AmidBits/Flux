namespace Flux.Dsp.WaveGenerator
{
  /// <summary>A wave sample generator of singular bipolar waveforms in the range [-1.0, 1.0]. The phase is in the range [0, 2PI].</summary>
  /// <seealso cref="https://en.wikipedia.org/wiki/Waveform"/>
  /// <seealso cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables"/>
  public interface IMonoWavePi2Generatable
  {
    /// <seealso cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
    /// <param name="phase2Pi">Phase [0, PI2] is the position of a point in time (an instant) on a waveform cycle. A complete cycle is defined as the interval required for the waveform to return to its arbitrary initial value. The graph to the right shows how one cycle constitutes 360° of phase. The graph also shows how phase is sometimes expressed in radians, where one radian of phase equals approximately 57.3°.</param>
    /// <returns>A wave sample in the range [-1, 1].</returns>
    Waves.IWaveMono<double> GenerateMonoWavePi2(double phasePi2);

    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to 2*PI [0, 2*PI).</summary>
    public static double AbsolutePhasePi2(double phasePi2)
      => phasePi2 < 0
      ? (phasePi2 % double.Tau) + double.Tau // Align phased correctly on positive side.
      : phasePi2 % double.Tau;
  }
}
