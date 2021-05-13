using System.Linq;

namespace Flux.Media.Dsp
{
  // https://github.com/safchain/sa_dsp/blob/master/jni/dsp/limit/SimpleLimit.cpp
  public static class Tools
  {
    /// <summary>Compute the root mean square (RMS) of the samples in the sequence.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Root_mean_square"/>
    public static double ComputeRms(System.Collections.Generic.IEnumerable<double> sampleBuffer) => System.Math.Sqrt(sampleBuffer.Average(sample => sample * sample));
    /// <summary>Compute the root mean square (RMS) of the specified number of samples in the sequence.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Root_mean_square"/>
    public static double ComputeRms(System.Collections.Generic.IEnumerable<double> sampleBuffer, int count) => ComputeRms(sampleBuffer.Take(count));
    /// <summary>Compute the root mean square (RMS) of the specified number of samples in the sequence starting at the specified position.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Root_mean_square"/>
    public static double ComputeRms(System.Collections.Generic.IEnumerable<double> sampleBuffer, int startAt, int count) => ComputeRms(sampleBuffer.Skip(startAt), count);

    /// <summary>Mix one or more mono signals. One mono signal will be returned as is.</summary>
    public static double MixMono(System.Collections.Generic.IEnumerable<double> mono) => mono.Count() is var count && count > 1 ? mono.Sum() / System.Math.Sqrt(count) : count == 1 ? mono.First() : throw new System.ArgumentOutOfRangeException(nameof(mono));
    /// <summary>Mix one or more stereo signals. One stereo signal will be returned as is.</summary>
    public static (double left, double right) MixStereo(System.Collections.Generic.IEnumerable<(double left, double right)> stereo) => stereo.Count() is var count && count > 1 && System.Math.Sqrt(count) is var scalar ? (stereo.Sum(s => s.left) / scalar, stereo.Sum(s => s.right) / scalar) : count == 1 ? stereo.First() : throw new System.ArgumentOutOfRangeException(nameof(stereo));
  }
}
