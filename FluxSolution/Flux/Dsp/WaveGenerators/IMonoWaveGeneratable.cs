//namespace Flux.Dsp
//{
//  /// <summary>A wave sample generator of singular bipolar waveforms in the range [-1.0, 1.0]. The phase is in the range [0, 2PI].</summary>
//  /// <seealso cref="https://en.wikipedia.org/wiki/Waveform"/>
//  /// <seealso cref="https://en.wikibooks.org/wiki/Sound_Synthesis_Theory/Oscillators_and_Wavetables"/>
//  public interface IMonoWaveGeneratable
//  {
//    /// <summary>
//    /// <para>The position in a wave at some frequency.</para>
//    /// <see cref="https://en.wikipedia.org/wiki/Phase_(waves)"/>
//    /// </summary>
//    /// <param name="phase">Phase [0, <paramref name="cycle"/>) is a position in a waveform cycle at some frequency.</param>
//    /// <param name="cycle">The width or size of a complete periodic wave.</param>
//    /// <returns>A wave sample in the range [-1, 1].</returns>
//    IWaveMono<double> GenerateMonoWave(double phase, double cycle);

//    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to 2*PI [0, 2*PI).</summary>
//    public static double AbsolutePhase(double phase, double cycle)
//      => cycle < 0
//      ? (cycle % cycle) + cycle // Align negative phase on the positive side within the cycle.
//      : phase % cycle; // Align positive phase within the cycle.
//  }
//}
