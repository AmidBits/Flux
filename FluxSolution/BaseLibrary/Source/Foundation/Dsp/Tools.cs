using System.Linq;

namespace Flux.Dsp
{
  // https://github.com/safchain/sa_dsp/blob/master/jni/dsp/limit/SimpleLimit.cpp
  public static class Tools
  {
    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to unit interval [0, 1).</summary>
    public static double AbsolutePhaseUi(double phaseMu)
      => phaseMu < 0
      ? (phaseMu % 1) + 1 // Align phased correctly on positive side.
      : phaseMu % 1;

    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to 2*PI [0, 2*PI).</summary>
    public static double AbsolutePhasePi2(double phasePi2)
      => phasePi2 < 0
      ? (phasePi2 % Maths.PiX2) + Maths.PiX2 // Align phased correctly on positive side.
      : phasePi2 % Maths.PiX2;

    /// <summary>Compute the root mean square (RMS) of the samples in the sequence.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Root_mean_square"/>
    public static SampleMono ComputeRms(System.Collections.Generic.IEnumerable<SampleMono> sampleBuffer)
      => new(System.Math.Sqrt(sampleBuffer.Average(sample => sample.FrontCenter * sample.FrontCenter)));
  }
}
