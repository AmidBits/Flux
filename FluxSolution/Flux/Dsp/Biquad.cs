namespace Flux.Dsp
{
  /// <summary>
  /// <para>A Transposed Direct Form I (DF1T) second‑order IIR filter implemented as a two‑delay recursive structure with three feedforward taps and two feedback taps, arranged in the transposed topology</para>
  /// </summary>
  public sealed class Biquad
  {
    #region Private biquad fields

    private double m_b0, m_b1, m_b2;
    private double m_a0, m_a1, m_a2;
    private double m_z1, m_z2;

    #endregion

    public Biquad()
      => SetIdentity();

    #region Public biquad properties

    public double B0 => m_b0;
    public double B1 => m_b1;
    public double B2 => m_b2;

    public double A0 => m_a0;
    public double A1 => m_a1;
    public double A2 => m_a2;

    public double Z1 => m_z1;
    public double Z2 => m_z2;

    #endregion

    public Biquad(double b0, double b1, double b2, double a0, double a1, double a2)
    {
      SetCoefficients(b0, b1, b2, a0, a1, a2);

      Reset();
    }

    public void Deconstruct(out double b0, out double b1, out double b2, out double a0, out double a1, out double a2, out double z1, out double z2)
    {
      b0 = m_b0;
      b1 = m_b1;
      b2 = m_b2;
      a0 = m_a0;
      a1 = m_a1;
      a2 = m_a2;
      z1 = m_z1;
      z2 = m_z2;
    }

    /// <summary>
    /// <para>Process a single sample through the biquad filter.</para>
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public double Process(double x)
    {
      var y = m_b0 * x + m_z1;
      m_z1 = m_b1 * x + m_z2 - m_a1 * y;
      m_z2 = m_b2 * x - m_a2 * y;
      return y;
    }

    /// <summary>
    /// <para>Resets the internal filter state.</para>
    /// </summary>
    public void Reset()
    {
      m_z1 = 0.0;
      m_z2 = 0.0;
    }

    public void SetCoefficients(double b0, double b1, double b2, double a0, double a1, double a2)
    {
      var invA0 = 1.0 / a0;

      m_b0 = b0 * invA0;
      m_b1 = b1 * invA0;
      m_b2 = b2 * invA0;

      m_a0 = a0;
      m_a1 = a1 * invA0;
      m_a2 = a2 * invA0;
    }

    public void SetIdentity()
    {
      m_b0 = 1.0;
      m_b1 = 0.0;
      m_b2 = 0.0;

      m_a0 = 1.0;
      m_a1 = 0.0;
      m_a2 = 0.0;

      m_z1 = 0.0;
      m_z2 = 0.0;
    }

    #region Static methods

    public static void Process4Filters(double x, Biquad[] bq, out System.Runtime.Intrinsics.Vector256<double> y)
    {
      var xv = System.Runtime.Intrinsics.Vector256.Create(x);

      var b0v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_b0, bq[1].m_b0, bq[2].m_b0, bq[3].m_b0); // Load coefficients into SIMD lanes
      var b1v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_b1, bq[1].m_b1, bq[2].m_b1, bq[3].m_b1);
      var b2v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_b2, bq[1].m_b2, bq[2].m_b2, bq[3].m_b2);
      var a1v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_a1, bq[1].m_a1, bq[2].m_a1, bq[3].m_a1);
      var a2v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_a2, bq[1].m_a2, bq[2].m_a2, bq[3].m_a2);

      var z1v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_z1, bq[1].m_z1, bq[2].m_z1, bq[3].m_z1); // Load state
      var z2v = System.Runtime.Intrinsics.Vector256.Create(bq[0].m_z2, bq[1].m_z2, bq[2].m_z2, bq[3].m_z2);

      var yv = (b0v * xv) + z1v; // y = b0*x + z1
      var z1_new = (b1v * xv) + z2v - (a1v * yv); // z1 = b1*x + z2 - a1*y
      var z2_new = (b2v * xv) - (a2v * yv); // z2 = b2*x - a2*y

      y = yv;

      for (int i = 0; i < 4; i++) // Write back state to each biquad.
      {
        bq[i].m_z1 = z1_new[i];
        bq[i].m_z2 = z2_new[i];
      }
    }

    public static void Process4Samples(System.Runtime.Intrinsics.Vector256<double> x, Biquad bq, out System.Runtime.Intrinsics.Vector256<double> y)
    {
      var b0v = System.Runtime.Intrinsics.Vector256.Create(bq.m_b0); // Broadcast coefficients
      var b1v = System.Runtime.Intrinsics.Vector256.Create(bq.m_b1);
      var b2v = System.Runtime.Intrinsics.Vector256.Create(bq.m_b2);
      var a1v = System.Runtime.Intrinsics.Vector256.Create(bq.m_a1);
      var a2v = System.Runtime.Intrinsics.Vector256.Create(bq.m_a2);

      var z1v = System.Runtime.Intrinsics.Vector256.Create(bq.m_z1); // Broadcast state
      var z2v = System.Runtime.Intrinsics.Vector256.Create(bq.m_z2);

      var yv = (b0v * x) + z1v; // y = b0*x + z1
      var z1_new = (b1v * x + z2v) - (a1v * yv); // z1 = b1*x + z2 - a1*y
      var z2_new = (b2v * x) - (a2v * yv); // z2 = b2*x - a2*y

      y = yv;

      bq.m_z1 = z1_new[3]; // Update scalar state using the LAST SIMD lane (sample 3)
      bq.m_z2 = z2_new[3];
    }

    public static void Process8Samples(System.Runtime.Intrinsics.Vector512<double> x, Biquad bq, out System.Runtime.Intrinsics.Vector512<double> y)
    {
      var b0v = System.Runtime.Intrinsics.Vector512.Create(bq.m_b0); // Broadcast coefficients
      var b1v = System.Runtime.Intrinsics.Vector512.Create(bq.m_b1);
      var b2v = System.Runtime.Intrinsics.Vector512.Create(bq.m_b2);
      var a1v = System.Runtime.Intrinsics.Vector512.Create(bq.m_a1);
      var a2v = System.Runtime.Intrinsics.Vector512.Create(bq.m_a2);

      var z1v = System.Runtime.Intrinsics.Vector512.Create(bq.m_z1); // Broadcast state
      var z2v = System.Runtime.Intrinsics.Vector512.Create(bq.m_z2);

      var yv = (b0v * x) + z1v; // y = b0*x + z1
      var z1_new = (b1v * x + z2v) - (a1v * yv); // z1 = b1*x + z2 - a1*y
      var z2_new = (b2v * x) - (a2v * yv); // z2 = b2*x - a2*y

      y = yv;

      bq.m_z1 = z1_new[7]; // Update scalar state from lane 7 (last lane)
      bq.m_z2 = z2_new[7];
    }

    #endregion

    /// <summary>
    /// <para>Robert Bristow-Johnson (RBJ) biquad filter design formulas.</para>
    /// <para>A Transposed Direct Form I (DF1T) second‑order IIR filter implemented as a two‑delay recursive structure with three feedforward taps and two feedback taps, arranged in the transposed topology</para>
    /// </summary>
    public static class RbjDesigner
    {
      private static void Setup(double fs, double f0, double dBgain, double Q, out double A, out double w0, out double sin, out double cos, out double alpha)
      {
        A = double.Pow(10.0, dBgain / 40.0); // gain factor for EQ/shelves peaking/shelves
        w0 = double.Tau * f0 / fs;
        (sin, cos) = double.SinCos(w0);
        alpha = sin / (2 * Q);
      }

      /// <summary>
      /// <para>Allpass filter provides phase rotation without magnitude change—great for phasers, group‑delay tricks, and time‑alignment.</para>
      /// <para>What it does? </para>
      /// <para>Typical use: </para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      public static void AllPass(Biquad bq, double fs, double f0, double Q)
      {
        Setup(fs, f0, 0, Q, out _, out _, out _, out double cos, out double alpha);

        var b0 = 1.0 - alpha;
        var b1 = -2.0 * cos;
        var b2 = 1.0 + alpha;
        var a0 = 1.0 + alpha;
        var a1 = -2.0 * cos;
        var a2 = 1.0 - alpha;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Constant‑Peak‑Gain Band‑Pass</para>
      /// <para>The RBJ “constant‑peak‑gain” band‑pass biquad.</para>
      /// <para>Perfect for spectral shaping, analysis filters, filter banks, EQ‑style band‑pass.</para>
      /// <para>What it does? Passes a range, cuts both sides.</para>
      /// <para>Typical use: Tone isolation, vocoder bands, biomedical processing.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      public static void BandPassConstantPeakGain(Biquad bq, double fs, double f0, double Q)
      {
        Setup(fs, f0, 0, Q, out _, out _, out _, out double cos, out double alpha);

        var b0 = alpha;
        var b1 = 0.0;
        var b2 = -alpha;
        var a0 = 1.0 + alpha;
        var a1 = -2.0 * cos;
        var a2 = 1.0 - alpha;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Constant‑Skirt‑Gain Band‑Pass</para>
      /// <para>The RBJ “constant‑skirt‑gain” band‑pass biquad.</para>
      /// <para>Perfect for resonators, formants, modal synthesis, vocoder bands.</para>
      /// <para>What it does? Passes a range, cuts both sides.</para>
      /// <para>Typical use: Tone isolation, vocoder bands, biomedical processing.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      public static void BandPassConstantSkirtGain(Biquad bq, double fs, double f0, double Q)
      {
        Setup(fs, f0, 0, Q, out _, out _, out var sin, out var cos, out var alpha);

        var b0 = sin / 2.0;
        var b1 = 0.0;
        var b2 = -sin / 2.0;
        var a0 = 1 + alpha;
        var a1 = -2 * cos;
        var a2 = 1 - alpha;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Second‑order RBJ <b>band‑stop</b> (notch) filter. This design removes a narrow band of frequencies centered at <paramref name="f0"/> while passing frequencies above and below the stopband.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      /// <remarks>This BandStop is a semantic alias for <see cref="Notch(Biquad, double, double, double)"/>. It exists to provide the classical DSP term “band‑stop” while internally using the RBJ notch implementation.</remarks>
      public static void BandStop(Biquad bq, double fs, double f0, double Q) => Notch(bq, fs, f0, Q);

      /// <summary>
      /// <para>Butterworth highpass filter.</para>
      /// <para>Uses the RBJ LP/HP formulas, sets Q = 0.7071, match the Butterworth pole pattern and produce the classic –3 dB cutoff behavior.</para>
      /// <para>What it does? Passes highs, cuts lows below a corner frequency.</para>
      /// <para>Typical use: Removing rumble, DC blocking, tweeter protection.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</param>
      public static void HighPass(Biquad bq, double fs, double f0, double Q)
      {
        Setup(fs, f0, 0, Q, out var A, out _, out _, out var cos, out var alpha);

        var b0 = (1 + cos) / 2.0;
        var b1 = -(1 + cos);
        var b2 = (1 + cos) / 2.0;
        var a0 = 1 + alpha;
        var a1 = -2 * cos;
        var a2 = 1 - alpha;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>High‑shelf filter.</para>
      /// <para>RBJ High‑Shelf biquad, specifically the “gain‑controlled shelving EQ” type. It’s the standard "treble shelf" filter in digital EQs.</para>
      /// <para>What it does? Boosts or cuts everything above a frequency.</para>
      /// <para>Typical use: Air, brightness, taming harshness.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="dBgain">Gain in decibels.</param>
      /// <param name="S">The slope, typically 0.1 ≤ <paramref name="S"/> ≤ 2.0 for musical EQ, where:
      /// <list type="bullet">
      /// <item>S = 1 → “normal” shelf (the default in most EQs)</item>
      /// <item>S > 1 → steeper, more resonant transition</item>
      /// <item>S &lt; 1 → gentler, more gradual transition</item>
      /// </list>
      /// </param>
      public static void HighShelf(Biquad bq, double fs, double f0, double dBgain, double S = 1.0)
      {
        Setup(fs, f0, dBgain, 1.0, out var A, out _, out var sin, out var cos, out _);

        var beta = double.Sqrt(A) / S;

        var b0 = A * ((A + 1) + (A - 1) * cos + 2 * beta * sin);
        var b1 = -2 * A * ((A - 1) + (A + 1) * cos);
        var b2 = A * ((A + 1) + (A - 1) * cos - 2 * beta * sin);
        var a0 = (A + 1) - (A - 1) * cos + 2 * beta * sin;
        var a1 = 2 * ((A - 1) - (A + 1) * cos);
        var a2 = (A + 1) - (A - 1) * cos - 2 * beta * sin;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Butterworth lowpass filter.</para>
      /// <para>Uses the RBJ LP/HP formulas, sets Q = 0.7071, match the Butterworth pole pattern and produce the classic –3 dB cutoff behavior.</para>
      /// <para>What it does? Passes lows, cuts highs above a corner frequency.</para>
      /// <para>Typical use: Subwoofer crossovers, anti-aliasing, smoothing sensor data.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      public static void LowPass(Biquad bq, double fs, double f0, double Q)
      {
        Setup(fs, f0, 0, Q, out _, out _, out _, out var cos, out var alpha);

        var b0 = (1 - cos) / 2.0;
        var b1 = 1 - cos;
        var b2 = (1 - cos) / 2.0;
        var a0 = 1 + alpha;
        var a1 = -2 * cos;
        var a2 = 1 - alpha;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Low‑shelf filter.</para>
      /// <para>RBJ Low‑Shelf biquad, specifically the “gain‑controlled shelving EQ” type. It’s the standard "bass shelf" filter in digital EQs.</para>
      /// <para>What it does? Boosts or cuts everything below a frequency.</para>
      /// <para>Typical use: Bass tilt, warming up a mix.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="dBgain">Gain in decibels.</param>
      /// <param name="S">The slope, typically 0.1 ≤ <paramref name="S"/> ≤ 2.0 for musical EQ, where:
      /// <list type="bullet">
      /// <item>S = 1 → “normal” shelf (the default in most EQs)</item>
      /// <item>S > 1 → steeper, more resonant transition</item>
      /// <item>S &lt; 1 → gentler, more gradual transition</item>
      /// </list>
      /// </param>
      public static void LowShelf(Biquad bq, double fs, double f0, double dBgain, double S = 1.0)
      {
        Setup(fs, f0, dBgain, 1.0, out var A, out _, out var sin, out var cos, out _);

        var beta = double.Sqrt(A) / S;

        var b0 = A * ((A + 1) - (A - 1) * cos + 2 * beta * sin);
        var b1 = 2 * A * ((A - 1) - (A + 1) * cos);
        var b2 = A * ((A + 1) - (A - 1) * cos - 2 * beta * sin);
        var a0 = (A + 1) + (A - 1) * cos + 2 * beta * sin;
        var a1 = -2 * ((A - 1) + (A + 1) * cos);
        var a2 = (A + 1) + (A - 1) * cos - 2 * beta * sin;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Notch filter.</para>
      /// <para>RBJ “band‑stop” biquad, specifically the constant‑skirt‑gain band‑stop type.</para>
      /// <para>What it does? Cuts a narrow band, passes everything else.</para>
      /// <para>Typical use: Killing 50/60 Hz hum, removing feedback squeal, room resonance.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      public static void Notch(Biquad bq, double fs, double f0, double Q)
      {
        Setup(fs, f0, 0, Q, out _, out _, out _, out var cos, out var alpha);

        var b0 = 1.0;
        var b1 = -2 * cos;
        var b2 = 1.0;
        var a0 = 1 + alpha;
        var a1 = -2 * cos;
        var a2 = 1 - alpha;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Peaking EQ filter.</para>
      /// <para>RBJ Parametric Peak/Notch biquad — the standard, canonical parametric EQ filter used in almost every digital EQ plugin on the planet.</para>
      /// <para>What it does? Boosts or cuts a specific band, leaves the rest flat.</para>
      /// <para>Typical use: Parametric EQ bands, surgical mix corrections.</para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="dBgain">Gain in decibels.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      public static void ParametricPeak(Biquad bq, double fs, double f0, double dBgain, double Q)
      {
        Setup(fs, f0, dBgain, Q, out var A, out _, out _, out var cos, out var alpha);

        var b0 = 1 + alpha * A;
        var b1 = -2 * cos;
        var b2 = 1 - alpha * A;
        var a0 = 1 + alpha / A;
        var a1 = -2 * cos;
        var a2 = 1 - alpha / A;

        bq.SetCoefficients(b0, b1, b2, a0, a1, a2);
      }

      /// <summary>
      /// <para>Resonator filter.</para>
      /// <para>A special case of the RBJ constant‑skirt‑gain band‑pass biquad, configured with a very high Q so it behaves like a physical resonant mode rather than a normal band‑pass filter.</para>
      /// <para>It is a high‑Q RBJ band‑pass, which turns the biquad into a digital resonant system.</para>
      /// <para>What it does? </para>
      /// <para>Typical use: </para>
      /// </summary>
      /// <param name="bq">The <see cref="Biquad"/> to compute.</param>
      /// <param name="fs">The sample rate.</param>
      /// <param name="f0">The center (or resonant) frequency of a second‑order pole/zero pair.</param>
      /// <param name="Q">
      /// <para>The quality factor of a second‑order filter section. It describes how narrow, sharp, or resonant the filter’s response is around its center frequency f0.</para>
      /// <list type="bullet">
      /// <item>Q = 0.5 → very broad</item>
      /// <item>Q = 1 → moderate</item>
      /// <item>Q = 2–5 → narrow</item>
      /// <item>Q > 10 → extremely narrow, surgical</item>
      /// </list>
      /// </param>
      /// <remarks>This Resonator method is a semantic alias for <see cref = "BandPassConstantSkirtGain(Biquad, double, double, double)" />. It exists to provide the audio‑DSP term “resonator,” which refers to using a high‑Q band‑pass section as a digital resonant mode.The underlying RBJ topology is identical; only the intended use and naming differ.</remarks>
      public static void Resonator(Biquad bq, double fs, double f0, double Q) => BandPassConstantSkirtGain(bq, fs, f0, Q);
    }
  }
}
