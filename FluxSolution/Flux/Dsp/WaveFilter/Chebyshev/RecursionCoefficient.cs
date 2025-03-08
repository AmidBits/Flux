namespace Flux.Dsp.WaveFilter.Chebyshev
{
  /// <see cref="http://www.dspguide.com/ch20.htm"/>
  public static class RecursionCoefficient
  {
    /// <summary>An easy entry method to the function as presented by "The Scientist and Engineer's Guide to Digital Signal Processing" By Steven W.Smith, Ph.D.</summary>
    /// <param name="cutoffFrequency">The cutoff frequency in Hz up to half the sample rate.</param>
    /// <param name="function">The filter type is an option between a low pass or a high pass filter.</param>
    /// <param name="rippleInUnitInterval">Percent ripple as a unit interval (0-1).</param>
    /// <param name="evenNumberOfPoles">An even number of poles in the filter between 2 and 20.</param>
    /// <see cref="http://www.dspguide.com/CH20.PDF"/>
    public static (double[] A, double[] B) Calculate(RecursionCoefficientFrequencyFunction frequencyFunction, double cutoffFrequency, double rippleInUnitInterval, int evenNumberOfPoles, double sampleRate = 44100.0)
    {
      var function = frequencyFunction switch
      {
        RecursionCoefficientFrequencyFunction.LowPass => 0,
        RecursionCoefficientFrequencyFunction.HighPass => 1,
        _ => throw new System.ArgumentOutOfRangeException(nameof(frequencyFunction))
      };
      var normalizedFrequency = cutoffFrequency / sampleRate;
      var ripple = rippleInUnitInterval * 29; // Max 29%
      var poleCount = (evenNumberOfPoles & 1) == 0 ? evenNumberOfPoles : throw new System.ArgumentOutOfRangeException(nameof(evenNumberOfPoles));

      return Calculate(normalizedFrequency, function, ripple, poleCount);
    }

    /// <summary>This extends the maximum number of poles that can be used, by implementing a filter in stages. For example, a six pole filter starts out as a cascade of three stages of two poles each. This method combines these three stages into a single set of recursion coefficients for easier programming. However, the filter is more stable if carried out as the original three separate stages. This requires knowing the "a" and "b" coefficients for each of the stages.</summary>
    /// <param name="FC">The cutoff frequency, FC, is expressed as a fraction of the sampling frequency, i.e. cutoffFrequency / sampleRate, and therefore must be in the range: 0 to 0.5.</param>
    /// <param name="LH">The variable, LH, is set to a value of 1 for a high-pass filter, and 0 for a low-pass filter.</param>
    /// <param name="PR">The value entered for PR must be in the range of 0.0 to 29,  corresponding to 0 to 29% ripple in the filter's frequency response.</param>
    /// <param name="NP">The number of poles in the filter, entered in the variable NP, must be an even integer between 2 and 20.</param>
    /// <remarks></remarks>
    /// <see cref="http://www.dspguide.com/ch20/4.htm"/>
    public static (double[] A, double[] B) Calculate(double FC, int LH, double PR, int NP)
    {
      if (FC < 0 || FC > 0.5) { throw new System.ArgumentOutOfRangeException($"The normalized cutoff frequency (FC={FC}) must be between 0 and 0.5."); }
      if (LH < 0 || LH > 1) { throw new System.ArgumentOutOfRangeException($"The filter type (LH={LH}) must be between 0 and 1."); }
      if (PR < 0 || PR > 29) { throw new System.ArgumentOutOfRangeException($"The ripple (PR={PR}) must be between 0 and 29 percent."); }
      if ((NP & 1) == 1 || NP < 2 || NP > 20) { throw new System.ArgumentOutOfRangeException($"The number of poles (NP={NP}) must be between 2 and 20."); }

      var A = new double[22];
      var B = new double[22];
      var TA = new double[22];
      var TB = new double[22];

      A[2] = 1.0;
      B[2] = 1.0;

      for (var P = 1; P <= (NP / 2); P++) // Loop for each pole-pair.
      {
        var (A0, A1, A2, B1, B2) = Subroutine(FC, LH, PR, NP, P);

        for (var i = 0; i < 22; i++) // Add coefficients to the cascade.
        {
          TA[i] = A[i];
          TB[i] = B[i];
        }

        for (var i = 2; i < 22; i++) // Cascade already computed coefficients.
        {
          A[i] = A0 * TA[i] + A1 * TA[i - 1] + A2 * TA[i - 2];
          B[i] = TB[i] - B1 * TB[i - 1] - B2 * TB[i - 2];
        }
      }

      B[2] = 0.0;

      for (var i = 0; i < 20; i++) // Finish combining coefficient.
      {
        A[i] = A[i + 2];
        B[i] = -B[i + 2];
      }

      // Normalize the gain.

      var SA = 0.0;
      var SB = 0.0;

      for (var i = 0; i < 20; i++)
      {
        switch (LH)
        {
          case 0:
            SA += A[i];
            SB += B[i];
            break;
          case 1:
            SA += A[i] * double.Pow(-1.0, i);
            SB += B[i] * double.Pow(-1.0, i);
            break;
        }
      }

      var gain = SA / (1.0 - SB);

      for (var i = 0; i < 20; i++)
      {
        A[i] /= gain;
      }

      // The final recursion coefficients are in A[NP + 1] and B[NP + 1] 
      return (A.Take(NP + 1).ToArray(), B.Take(NP + 1).ToArray());
    }

    /// <summary>This subroutine is called once for each stage in the cascade. For example, it is called three times for a six pole filter. At the completion of the subroutine, five variables are return to the main program: A0, A1, A2, B1, & B2. These are the recursion coefficients for the two pole stage being worked on, and can be used to implement the filter in stages.</summary>
    /// <param name="FC">The cutoff frequency, FC, is expressed as a fraction of the sampling frequency, i.e. cutoffFrequency / sampleRate, and therefore must be in the range: 0 to 0.5.</param>
    /// <param name="LH">The variable, LH, is set to a value of 1 for a high-pass filter, and 0 for a low-pass filter.</param>
    /// <param name="PR">The value entered for PR must be in the range of 0.0 to 29,  corresponding to 0 to 29% ripple in the filter's frequency response.</param>
    /// <param name="NP">The number of poles in the filter, entered in the variable NP, must be an even integer between 2 and 20.</param>
    /// <param name="P">The current pole being computed, starting with 1.</param>
    /// <see cref="http://www.dspguide.com/ch20/4.htm"/>
    public static (double A0, double A1, double A2, double B1, double B2) Subroutine(double FC, int LH, double PR, int NP, int P)
    {
      var poleAngle = double.Pi / (NP * 2) + (P - 1) * (double.Pi / NP);

      // Calculate the pole location on the unit circle.
      var RP = -double.Cos(poleAngle);
      var IP = double.Sin(poleAngle);

      if (PR > 0) // Warp from a circle to an ellipse.
      {
        var ES = double.Sqrt(double.Pow(100.0 / (100.0 - PR), 2.0) - 1.0);
        var NPi = 1.0 / NP;
        var ESi = 1 / ES;
        var ES2i = 1 / (ES * ES);
        var VX = NPi * double.Log(ESi + double.Sqrt(ES2i + 1.0));
        var KX = NPi * double.Log(ESi + double.Sqrt(ES2i - 1.0));
        KX = (double.Exp(KX) + double.Exp(-KX)) / 2.0;
        var expVX = double.Exp(VX);
        var expVXn = double.Exp(-VX);
        RP = RP * ((expVX - expVXn) / 2.0) / KX;
        IP = IP * ((expVX + expVXn) / 2.0) / KX;
      }

      // s-domain to z-domain conversion
      var T = 2.0 * double.Tan(0.5);
      var T2 = (T * T);
      var RPmulT = RP * T;
      var W = (double.Pi / 2) * FC;
      var M = (RP * RP) + (IP * IP);
      var MmulT2 = M * T2;
      var D = 4.0 - 4.0 * RPmulT + MmulT2;
      var X0 = T2 / D;
      var X1 = 2.0 * X0;
      var X2 = X0;
      var Y1 = (8.0 - 2.0 * MmulT2) / D;
      var Y2 = (-4.0 - 4.0 * RPmulT - MmulT2) / D;

      // LP TO LP, or LP TO HP transform 
      var K = W / 2.0; // Initialize with optimization rather than just 0;
      switch (LH)
      {
        case 0:
          K = double.Sin(0.5 - K) / double.Sin(0.5 + K);
          break;
        case 1:
          K = -double.Cos(K + 0.5) / double.Cos(K - 0.5);
          break;
      }
      var K2 = (K * K);

      D = 1 + Y1 * K - Y2 * K2;

      var A0 = (X0 - X1 * K + X2 * K2) / D;
      var A1 = (-2.0 * X0 * K + X1 + X1 * K2 - 2.0 * X2 * K) / D;
      var A2 = (X0 * K2 - X1 * K + X2) / D;
      var B1 = (2.0 * K + Y1 + Y1 * K2 - 2.0 * Y2 * K) / D;
      var B2 = (-K2 - Y1 * K + Y2) / D;

      if (LH == 1)
      {
        A1 = -A1;
        B1 = -B1;
      }

      return (A0, A1, A2, B1, B2);
    }
  }
}
