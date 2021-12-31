using System.Linq;

namespace Flux.Dsp
{
  // https://github.com/safchain/sa_dsp/blob/master/jni/dsp/limit/SimpleLimit.cpp
  public static class Tools
  {
    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to unit interval [0, 1).</summary>
    public static double AbsolutePhaseMu(double phaseMu)
      => phaseMu < 0
      ? (phaseMu % 1) + 1
      : phaseMu % 1;
    /// <summary>Returns the absolute phase (negative phase align correctly on positive side) normalized to 2*PI [0, 2*PI).</summary>
    public static double AbsolutePhasePiX2(double phasePiX2)
      => phasePiX2 < 0
      ? (phasePiX2 % Maths.PiX2) + Maths.PiX2
      : phasePiX2 % Maths.PiX2;

    /// <summary>Compute the root mean square (RMS) of the samples in the sequence.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Root_mean_square"/>
    public static double ComputeRms(System.Collections.Generic.IEnumerable<double> sampleBuffer)
      => System.Math.Sqrt(sampleBuffer.Average(sample => sample * sample));

    /// <summary>Mix one or more mono signals. One mono signal will be returned as is.</summary>
    public static double MixMono(System.Collections.Generic.IEnumerable<double> mono)
    {
      using var e = mono.GetEnumerator();

      if (e.MoveNext())
      {
        var count = 1;
        var sum = e.Current;

        while (e.MoveNext())
        {
          count++;
          sum += e.Current;
        }

        return count > 1 ? sum / System.Math.Sqrt(count) : sum;
      }
      else throw new System.ArgumentException(@"The sequence is empty.");

    }
    /// <summary>Mix one or more stereo signals. One stereo signal will be returned as is.</summary>
    public static (double left, double right) MixStereo(System.Collections.Generic.IEnumerable<(double left, double right)> stereo)
    {
      using var e = stereo.GetEnumerator();

      if (e.MoveNext())
      {
        var count = 1;
        var current = e.Current;
        var sumL = current.left;
        var sumR = current.right;

        while (e.MoveNext())
        {
          count++;
          current = e.Current;
          sumL += current.left;
          sumR += current.right;
        }

        return count > 1 && System.Math.Sqrt(count) is var sqrtCount ? (sumL / sqrtCount, sumR / sqrtCount) : (sumL, sumR);
      }
      else throw new System.ArgumentException(@"The sequence is empty.");
    }
  }
}
