namespace Flux.Dsp
{
  public static partial class Convert
  {
    public const double Decibel2Logarithm = 0.11512925464970228420089957273422; // System.Math.Log(10.0) / 20.0
    public const double Logarithm2Decibel = 8.6858896380650365530225783783321; // 20.0 / System.Math.Log(10.0)

    /// <summary>The energy quantity scalar for conversions.</summary>
    public const double EnergyQuantity = 10;
    /// <summary>The field quantity scalar for conversions.</summary>
    public const double FieldQuantity = 20;

    /// <summary>Convert a bipolar [-1, 1] sample to a unipolar [0, 1] sample. The sample is scaled down to fit the smaller range.</summary>
    public static double BipolarToUnipolar(double sample) => sample / 2 + 0.5;
    /// <summary>Convert a unipolar [0, 1] sample to a bipolar [-1, 1] sample. The sample is scaled up to match the larger range.</summary>
    public static double UnipolarToBipolar(double sample) => sample * 2 - 1;

    /// <summary>Conversion from gain (decibels) to a factor (ratio)</summary>
    /// <param name="dB">The decibel to convert into the factor (ratio).</param>
    /// <param name="referenceFactor">The reference factor for 0 dB.</param>
    /// <param name="quantityScalar">When 10 then compute energy quantity (factor y) and when 20 then compute field quantity (factor x).</param>
    /// <returns>The factor (as a ratio).</returns>
    /// <seealso cref="http://www.sengpielaudio.com/calculator-FactorRatioLevelDecibel.htm"/>
    public static double DecibelsToRatio(double dB, double referenceFactor = 1, double quantityScalar = FieldQuantity)
      => referenceFactor * System.Math.Pow(10, dB / quantityScalar);
    /// <summary>Conversion from factor (ratio) to gain (decibels).</summary>
    /// <param name="ratio">The factor (ratio) to convert into decibels.</param>
    /// <param name="referenceFactor">The reference factor for 0 dB.</param>
    /// <param name="quantityScalar">When 10 then compute energy quantity (factor y) and when 20 then compute field quantity (factor x).</param>
    /// <returns>The gain (in decibels).</returns>
    /// <seealso cref="http://www.sengpielaudio.com/calculator-FactorRatioLevelDecibel.htm"/>
    public static double RatioToDecibels(double ratio, double referenceFactor = 1, double quantityScalar = FieldQuantity)
      => quantityScalar * System.Math.Log10(ratio / referenceFactor);

    /// <summary>Convert a mono sample into a set of stereo samples.</summary>
    public static (double sampleL, double sampleR) MonoToStereo(double sampleM) => (sampleM, sampleM);
    /// <summary>Convert a set of stereo samples into a mono sample.</summary>
    public static double StereoToMono(double sampleL, double sampleR) => (sampleL + sampleR) / 2;

    /// <summary>Convert Fl/Fr (stereo) into Fl/Fc/Fr (stereo plus center) signals. This is a stab, not a/the mathematical solution.</summary>
    /// <param name="sampleFl">Front left.</param>
    /// <param name="sampleFr">Front right.</param>
    /// <see href="https://hydrogenaud.io/index.php?PHPSESSID=1n6jd2rvvcdc1g2cjmdbml0g17&topic=17267.msg174102#msg174102"/>
    public static (double sampleFl, double sampleFc, double sampleFr) FlFr2FlFcFr(double sampleFl, double sampleFr)
      => (sampleFl - sampleFr / 2, (sampleFl + sampleFr) / 2, sampleFr - sampleFl / 2);
  }
}
