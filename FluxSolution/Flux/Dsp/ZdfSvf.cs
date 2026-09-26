namespace Flux.Dsp
{
  /// <summary>
  /// <para>The canonical Zavalishin state variable filter (SVF).</para>
  /// </summary>
  public class ZdfSvf
  {
    #region Private ZDF SVF fields

    // Zavalishin’s canonical ZDF SVF topology parameters: g, R, h.
    public double m_g; // Integrator coefficient
    public double m_R; // Damping (1/Q).
    public double m_h; // Normalization factor.

    private double m_lp; // LowPass integrator.
    private double m_bp; // BandPass integrator.
    private double m_hp; // HighPass output.
    private double m_peak; // Peak output.

    #endregion

    public ZdfSvf()
      => SetIdentity();

    public double AllPass => m_lp + m_bp + m_hp;
    public double BandPass => m_bp;
    public double HighPass => m_hp;
    public double LowPass => m_lp;
    public double Notch => m_lp + m_hp;
    public double Peak => m_peak;

    /// <summary>
    /// <para>Canonical ZDF SVF step. Produces hp, updates bp/lp.</para>
    /// </summary>
    /// <param name="x">Input signal.</param>
    public void Process(double x)
    {
      m_hp = (x - (m_R + m_g) * m_bp - m_lp) * m_h;

      m_bp = m_bp + m_g * m_hp;
      m_lp = m_lp + m_g * m_bp;

      m_peak = m_bp + x;
    }

    /// <summary>
    /// <para>Reset the filter state to zero.</para>
    /// </summary>
    public void Reset()
    {
      m_lp = 0.0;
      m_bp = 0.0;
      m_hp = 0.0;
      m_peak = 0.0;
    }

    public void SetIdentity() => SetParameters(0, 0, 1);

    public void SetParameters(double g, double R, double h)
    {
      m_g = g;
      m_R = R;
      m_h = h;
    }

    public static class Designer
    {
      /// <summary>
      /// <para>Setup a Zavalishin canonical ZDF SVF.</para>
      /// </summary>
      /// <param name="zdfsvf"></param>
      /// <param name="fs"></param>
      /// <param name="f0"></param>
      /// <param name="Q"></param>
      public static void Setup(ZdfSvf zdfsvf, double fs, double f0, double Q)
      {
        var g = double.TanPi(f0 / fs);
        var R = 1.0 / Q;
        var h = 1.0 / (1.0 + R * g + g * g); // Zavalishin normalization.

        zdfsvf.SetParameters(g, R, h);

        zdfsvf.Reset();
      }
    }
  }
}
