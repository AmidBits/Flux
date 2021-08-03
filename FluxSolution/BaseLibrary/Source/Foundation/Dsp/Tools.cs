using System.Linq;

namespace Flux.Dsp
{
  // https://github.com/safchain/sa_dsp/blob/master/jni/dsp/limit/SimpleLimit.cpp
  public static class Tools
  {
    /// <summary>Returns the absolute phase (negative phase align correctly on positive side).</summary>
    public static double AbsolutePhasePi2(double phase2PI)
      => phase2PI < 0
      ? phase2PI % Maths.PiX2 + Maths.PiX2
      : phase2PI % Maths.PiX2;
    /// <summary>Returns the absolute phase (negative phase align correctly on positive side).</summary>
    public static double AbsolutePhaseMu(double phaseMU)
      => phaseMU < 0
      ? phaseMU % 1 + 1
      : phaseMU % 1;

    /// <summary>Compute the root mean square (RMS) of the samples in the sequence.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Root_mean_square"/>
    public static double ComputeRms(System.Collections.Generic.IEnumerable<double> sampleBuffer)
      => System.Math.Sqrt(sampleBuffer.Average(sample => sample * sample));

    /// <summary>Mix one or more mono signals. One mono signal will be returned as is.</summary>
    public static double MixMono(System.Collections.Generic.IEnumerable<double> mono)
      => mono.Count() is var count && count > 1 ? mono.Sum() / System.Math.Sqrt(count) : count == 1 ? mono.First() : throw new System.ArgumentOutOfRangeException(nameof(mono));
    /// <summary>Mix one or more stereo signals. One stereo signal will be returned as is.</summary>
    public static (double left, double right) MixStereo(System.Collections.Generic.IEnumerable<(double left, double right)> stereo)
      => stereo.Count() is var count && count > 1 && System.Math.Sqrt(count) is var scalar ? (stereo.Sum(s => s.left) / scalar, stereo.Sum(s => s.right) / scalar) : count == 1 ? stereo.First() : throw new System.ArgumentOutOfRangeException(nameof(stereo));
  }
}
