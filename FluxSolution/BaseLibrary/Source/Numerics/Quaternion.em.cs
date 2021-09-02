namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Numerics.Vector3 EulerAngles(this System.Numerics.Quaternion source)
    {
      double sqw = source.W * source.W;
      double sqx = source.X * source.X;
      double sqy = source.Y * source.Y;
      double sqz = source.Z * source.Z;

      double unit = sqx + sqy + sqz + sqw; // if normalised is one, otherwise is correction factor
      double test = source.X * source.Y + source.Z * source.W;

      if (test > 0.499 * unit) // singularity at north pole
      {
        return new System.Numerics.Vector3((float)(System.Math.Atan2(source.X, source.W) * 2), (float)Maths.PiOver2, 0);
      }
      else if (test < -0.499 * unit) // singularity at south pole
      {
        return new System.Numerics.Vector3((float)(System.Math.Atan2(source.X, source.W) * -2), (float)-Maths.PiOver2, 0);
      }
      else
      {
        var heading = System.Math.Atan2(2 * source.Y * source.W - 2 * source.X * source.Z, sqx - sqy - sqz + sqw);
        var attitude = System.Math.Asin(2 * test / unit);
        var bank = System.Math.Atan2(2 * source.X * source.W - 2 * source.Y * source.Z, -sqx + sqy - sqz + sqw);

        return new System.Numerics.Vector3((float)heading, (float)attitude, (float)bank);
      }
    }
  }

  public static class QuaternionEx
  {
    /// <summary>Returns a quaternion from two vectors.</summary>
    /// <see cref="http://lolengine.net/blog/2013/09/18/beautiful-maths-quaternion-from-vectors"/>
    public static System.Numerics.Quaternion FromTwoVectors(System.Numerics.Vector3 u, System.Numerics.Vector3 v)
    {
      var norm_u_norm_v = (float)System.Math.Sqrt(System.Numerics.Vector3.Dot(u, u) * System.Numerics.Vector3.Dot(v, v));
      var real_part = norm_u_norm_v + System.Numerics.Vector3.Dot(u, v);

      System.Numerics.Vector3 w;

      if (real_part < Maths.Epsilon1E7 * norm_u_norm_v)
      {
        real_part = 0.0f;

        // If u and v are exactly opposite, rotate 180 degrees around an arbitrary orthogonal axis. Axis normalisation can happen later, when we normalise the quaternion.
        w = System.Math.Abs(u.X) > System.Math.Abs(u.Z) ? new System.Numerics.Vector3(-u.Y, u.X, 0f) : new System.Numerics.Vector3(0f, -u.Z, u.Y);
      }
      else
      {
        w = System.Numerics.Vector3.Cross(u, v);
      }

      var quaternion = new System.Numerics.Quaternion(w.X, w.Y, w.Z, real_part);

      return System.Numerics.Quaternion.Normalize(quaternion);
    }
  }
}
