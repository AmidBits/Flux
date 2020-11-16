

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCosineX(System.Numerics.BigInteger y1, System.Numerics.BigInteger y2, double mu)
        => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? ((double)y1 * (1.0 - mu2) + (double)y2 * mu2) : throw new System.ArgumentOutOfRangeException(nameof(mu));

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicX(System.Numerics.BigInteger y0, System.Numerics.BigInteger y1, System.Numerics.BigInteger y2, System.Numerics.BigInteger y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return (double)a0 * mu * mu2 + (double)a1 * mu2 + (double)a2 * mu + (double)a3;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicPbX(System.Numerics.BigInteger y0, System.Numerics.BigInteger y1, System.Numerics.BigInteger y2, System.Numerics.BigInteger y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = -0.5 * (double)y0 + 1.5 * (double)y1 - 1.5 * (double)y2 + 0.5 * (double)y3;
      var a1 = (double)y0 - 2.5 * (double)y1 + 2 * (double)y2 - 0.5 * (double)y3;
      var a2 = -0.5 * (double)y0 + 0.5 * (double)y2;
      var a3 = (double)y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermiteX(System.Numerics.BigInteger y0, System.Numerics.BigInteger y1, System.Numerics.BigInteger y2, System.Numerics.BigInteger y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (double)(y1 - y0) * onePbias * oneMtension / 2 + (double)(y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (double)(y2 - y1) * onePbias * oneMtension / 2 + (double)(y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * (double)y1 + a1 * m0 + a2 * m1 + a3 * (double)y2;
    }

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateLinearX(System.Numerics.BigInteger y1, System.Numerics.BigInteger y2, double mu)
      => (double)y1 * (1 - mu) + (double)y2 * mu;

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LcmX(params System.Numerics.BigInteger[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the Least Common Multiple (LCM) of two values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LeastCommonMultipleX(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCosineX(System.Decimal y1, System.Decimal y2, double mu)
        => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? ((double)y1 * (1.0 - mu2) + (double)y2 * mu2) : throw new System.ArgumentOutOfRangeException(nameof(mu));

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicX(System.Decimal y0, System.Decimal y1, System.Decimal y2, System.Decimal y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return (double)a0 * mu * mu2 + (double)a1 * mu2 + (double)a2 * mu + (double)a3;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicPbX(System.Decimal y0, System.Decimal y1, System.Decimal y2, System.Decimal y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = -0.5 * (double)y0 + 1.5 * (double)y1 - 1.5 * (double)y2 + 0.5 * (double)y3;
      var a1 = (double)y0 - 2.5 * (double)y1 + 2 * (double)y2 - 0.5 * (double)y3;
      var a2 = -0.5 * (double)y0 + 0.5 * (double)y2;
      var a3 = (double)y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermiteX(System.Decimal y0, System.Decimal y1, System.Decimal y2, System.Decimal y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (double)(y1 - y0) * onePbias * oneMtension / 2 + (double)(y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (double)(y2 - y1) * onePbias * oneMtension / 2 + (double)(y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * (double)y1 + a1 * m0 + a2 * m1 + a3 * (double)y2;
    }

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateLinearX(System.Decimal y1, System.Decimal y2, double mu)
      => (double)y1 * (1 - mu) + (double)y2 * mu;

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Decimal LcmX(params System.Decimal[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the Least Common Multiple (LCM) of two values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Decimal LeastCommonMultipleX(System.Decimal a, System.Decimal b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCosineX(System.Double y1, System.Double y2, double mu)
        => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? ((double)y1 * (1.0 - mu2) + (double)y2 * mu2) : throw new System.ArgumentOutOfRangeException(nameof(mu));

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicX(System.Double y0, System.Double y1, System.Double y2, System.Double y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return (double)a0 * mu * mu2 + (double)a1 * mu2 + (double)a2 * mu + (double)a3;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicPbX(System.Double y0, System.Double y1, System.Double y2, System.Double y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = -0.5 * (double)y0 + 1.5 * (double)y1 - 1.5 * (double)y2 + 0.5 * (double)y3;
      var a1 = (double)y0 - 2.5 * (double)y1 + 2 * (double)y2 - 0.5 * (double)y3;
      var a2 = -0.5 * (double)y0 + 0.5 * (double)y2;
      var a3 = (double)y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermiteX(System.Double y0, System.Double y1, System.Double y2, System.Double y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (double)(y1 - y0) * onePbias * oneMtension / 2 + (double)(y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (double)(y2 - y1) * onePbias * oneMtension / 2 + (double)(y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * (double)y1 + a1 * m0 + a2 * m1 + a3 * (double)y2;
    }

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateLinearX(System.Double y1, System.Double y2, double mu)
      => (double)y1 * (1 - mu) + (double)y2 * mu;

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Double LcmX(params System.Double[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the Least Common Multiple (LCM) of two values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Double LeastCommonMultipleX(System.Double a, System.Double b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCosineX(System.Single y1, System.Single y2, double mu)
        => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? ((double)y1 * (1.0 - mu2) + (double)y2 * mu2) : throw new System.ArgumentOutOfRangeException(nameof(mu));

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicX(System.Single y0, System.Single y1, System.Single y2, System.Single y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return (double)a0 * mu * mu2 + (double)a1 * mu2 + (double)a2 * mu + (double)a3;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicPbX(System.Single y0, System.Single y1, System.Single y2, System.Single y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = -0.5 * (double)y0 + 1.5 * (double)y1 - 1.5 * (double)y2 + 0.5 * (double)y3;
      var a1 = (double)y0 - 2.5 * (double)y1 + 2 * (double)y2 - 0.5 * (double)y3;
      var a2 = -0.5 * (double)y0 + 0.5 * (double)y2;
      var a3 = (double)y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermiteX(System.Single y0, System.Single y1, System.Single y2, System.Single y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (double)(y1 - y0) * onePbias * oneMtension / 2 + (double)(y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (double)(y2 - y1) * onePbias * oneMtension / 2 + (double)(y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * (double)y1 + a1 * m0 + a2 * m1 + a3 * (double)y2;
    }

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateLinearX(System.Single y1, System.Single y2, double mu)
      => (double)y1 * (1 - mu) + (double)y2 * mu;

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Single LcmX(params System.Single[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the Least Common Multiple (LCM) of two values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Single LeastCommonMultipleX(System.Single a, System.Single b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCosineX(System.Int32 y1, System.Int32 y2, double mu)
        => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? ((double)y1 * (1.0 - mu2) + (double)y2 * mu2) : throw new System.ArgumentOutOfRangeException(nameof(mu));

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicX(System.Int32 y0, System.Int32 y1, System.Int32 y2, System.Int32 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return (double)a0 * mu * mu2 + (double)a1 * mu2 + (double)a2 * mu + (double)a3;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicPbX(System.Int32 y0, System.Int32 y1, System.Int32 y2, System.Int32 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = -0.5 * (double)y0 + 1.5 * (double)y1 - 1.5 * (double)y2 + 0.5 * (double)y3;
      var a1 = (double)y0 - 2.5 * (double)y1 + 2 * (double)y2 - 0.5 * (double)y3;
      var a2 = -0.5 * (double)y0 + 0.5 * (double)y2;
      var a3 = (double)y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermiteX(System.Int32 y0, System.Int32 y1, System.Int32 y2, System.Int32 y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (double)(y1 - y0) * onePbias * oneMtension / 2 + (double)(y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (double)(y2 - y1) * onePbias * oneMtension / 2 + (double)(y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * (double)y1 + a1 * m0 + a2 * m1 + a3 * (double)y2;
    }

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateLinearX(System.Int32 y1, System.Int32 y2, double mu)
      => (double)y1 * (1 - mu) + (double)y2 * mu;

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int32 LcmX(params System.Int32[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the Least Common Multiple (LCM) of two values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int32 LeastCommonMultipleX(System.Int32 a, System.Int32 b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Cosine interpolation is a smoother and perhaps simplest function. A suitable orientated piece of a cosine function serves to provide a smooth transition between adjacent segments.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points, the mu range is [0, 1]. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCosineX(System.Int64 y1, System.Int64 y2, double mu)
        => mu >= 0 && mu <= 1 && ((1.0 - System.Math.Cos(mu * System.Math.PI)) / 2.0) is double mu2 ? ((double)y1 * (1.0 - mu2) + (double)y2 * mu2) : throw new System.ArgumentOutOfRangeException(nameof(mu));

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicX(System.Int64 y0, System.Int64 y1, System.Int64 y2, System.Int64 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = y3 - y2 - y0 + y1;
      var a1 = y0 - y1 - a0;
      var a2 = y2 - y0;
      var a3 = y1;

      return (double)a0 * mu * mu2 + (double)a1 * mu2 + (double)a2 * mu + (double)a3;
    }

    /// <summary>Cubic interpolation is the simplest method that offers true continuity between the segments. As such it requires more than just the two endpoints of the segment but also the two points on either side of them.</summary>
    /// <remarks>Paul Breeuwsma proposes the following coefficients for a smoother interpolated curve, which uses the slope between the previous point and the next as the derivative at the current point. The results is what are generally referred to as Catmull-Rom splines.</remarks>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateCubicPbX(System.Int64 y0, System.Int64 y1, System.Int64 y2, System.Int64 y3, double mu)
    {
      var mu2 = mu * mu;

      var a0 = -0.5 * (double)y0 + 1.5 * (double)y1 - 1.5 * (double)y2 + 0.5 * (double)y3;
      var a1 = (double)y0 - 2.5 * (double)y1 + 2 * (double)y2 - 0.5 * (double)y3;
      var a2 = -0.5 * (double)y0 + 0.5 * (double)y2;
      var a3 = (double)y1;

      return a0 * mu * mu2 + a1 * mu2 + a2 * mu + a3;
    }

    /// <summary>Hermite interpolation like cubic requires 4 points so that it can achieve a higher degree of continuity. In addition it has nice tension and biasing controls. Tension can be used to tighten up the curvature at the known points. The bias is used to twist the curve about the known points. The examples shown here have the default tension and bias values of 0, it will be left as an exercise for the reader to explore different tension and bias values.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <param name="tension">1 is high, 0 normal, -1 is low.</param>
    /// <param name="bias">0 is even, positive is towards first segment, negative towards the other.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateHermiteX(System.Int64 y0, System.Int64 y1, System.Int64 y2, System.Int64 y3, double mu, double tension, double bias)
    {
      var mu2 = mu * mu;
      var mu3 = mu2 * mu;

      var onePbias = 1 + bias;
      var oneMbias = 1 - bias;

      var oneMtension = 1 - tension;

      var m0 = (double)(y1 - y0) * onePbias * oneMtension / 2 + (double)(y2 - y1) * oneMbias * oneMtension / 2;
      var m1 = (double)(y2 - y1) * onePbias * oneMtension / 2 + (double)(y3 - y2) * oneMbias * oneMtension / 2;

      var a0 = 2 * mu3 - 3 * mu2 + 1;
      var a1 = mu3 - 2 * mu2 + mu;
      var a2 = mu3 - mu2;
      var a3 = -2 * mu3 + 3 * mu2;

      return a0 * (double)y1 + a1 * m0 + a2 * m1 + a3 * (double)y2;
    }

    /// <summary>Linear interpolation (a.k.a. lerp) is the simplest method of getting values at positions in between the data points. The points are simply joined by straight line segments. Each segment (bounded by two data points) can be interpolated independently. The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</summary>
    /// <param name="mu">The parameter mu defines where to estimate the value on the interpolated line, it is 0 at the first point and 1 and the second point. For interpolated values between the two points mu ranges between 0 and 1. Values of mu outside the range result in extrapolation.</param>
    /// <see cref="http://paulbourke.net/miscellaneous/interpolation/"/>
    public static double InterpolateLinearX(System.Int64 y1, System.Int64 y2, double mu)
      => (double)y1 * (1 - mu) + (double)y2 * mu;

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int64 LcmX(params System.Int64[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the Least Common Multiple (LCM) of two values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int64 LeastCommonMultipleX(System.Int64 a, System.Int64 b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
  }
}
