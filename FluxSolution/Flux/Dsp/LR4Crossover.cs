namespace Flux.Dsp
{
  /// <summary>
  /// <para>A Linkwitz–Riley 4th‑order crossover filter — a pair of Butterworth 2nd‑order filters cascaded to produce perfectly flat acoustic summing at the crossover frequency.</para>
  /// <para>The industry‑standard way to split audio into low/high bands without bumps, dips, or phase weirdness.</para>
  /// </summary>
  public class LR4Crossover
  {
    private readonly Biquad m_lp1 = new();
    private readonly Biquad m_lp2 = new();
    private readonly Biquad m_hp1 = new();
    private readonly Biquad m_hp2 = new();

    public LR4Crossover(double fs, double fc)
    {
      // Use your cookbook's Butterworth generators

      Biquad.RbjDesigner.LowPass(m_lp1, fs, fc, Q: 0.7071f);
      Biquad.RbjDesigner.LowPass(m_lp2, fs, fc, Q: 0.7071f);

      Biquad.RbjDesigner.HighPass(m_hp1, fs, fc, Q: 0.7071f);
      Biquad.RbjDesigner.HighPass(m_hp2, fs, fc, Q: 0.7071f);
    }

    public void Process(double x, out double low, out double high)
    {
      low = m_lp2.Process(m_lp1.Process(x)); // Cascade LP.
      high = m_hp2.Process(m_hp1.Process(x)); // Cascade HP.
    }

    public void Process4Samples(System.Runtime.Intrinsics.Vector256<double> x, out System.Runtime.Intrinsics.Vector256<double> low, out System.Runtime.Intrinsics.Vector256<double> high)
    {
      // LP path: lp1 → lp2
      Biquad.Process4Samples(x, m_lp1, out var lp1_out);
      Biquad.Process4Samples(lp1_out, m_lp2, out low);

      // HP path: hp1 → hp2
      Biquad.Process4Samples(x, m_hp1, out var hp1_out);
      Biquad.Process4Samples(hp1_out, m_hp2, out high);
    }

    public void Process8Samples(System.Runtime.Intrinsics.Vector512<double> x, out System.Runtime.Intrinsics.Vector512<double> low, out System.Runtime.Intrinsics.Vector512<double> high)
    {
      // LP path: lp1 → lp2
      Biquad.Process8Samples(x, m_lp1, out var lp1_out);
      Biquad.Process8Samples(lp1_out, m_lp2, out low);

      // HP path: hp1 → hp2
      Biquad.Process8Samples(x, m_hp1, out var hp1_out);
      Biquad.Process8Samples(hp1_out, m_hp2, out high);
    }
  }
}
