﻿namespace Flux
{
  public static partial class Fx
  {
    /// <summary></summary>
    /// <remarks>The quaternion must be normalized.</remarks>
    public static EuclideanRotations.AxisAngle ToAxisAngle(this System.Numerics.Quaternion source)
    {
      var n = System.Numerics.Quaternion.Normalize(source); // If w>1 acos and sqrt will produce errors, this will not happen if quaternion is normalized.

      var w = n.W;

      var angle = double.Acos(w);
      angle += angle; // Times 2!

      var s = double.Sqrt(1 - w * w); // Assuming quaternion normalized then w is less than 1, so term always positive.

      if (s < 0.001) // Test to avoid divide by zero. If s close to zero then direction of axis not important.
        return new(angle, Units.AngleUnit.Radian, 1, Units.LengthUnit.Meter, 0, Units.LengthUnit.Meter, 0, Units.LengthUnit.Meter); // If it is important that axis is normalised then replace with x=1; y=z=0;

      return new(angle, Units.AngleUnit.Radian, n.X / s, Units.LengthUnit.Meter, n.Y / s, Units.LengthUnit.Meter, n.Z / s, Units.LengthUnit.Meter);
    }

    public static EuclideanRotations.EulerAngles ToEulerAngles(this System.Numerics.Quaternion source) // yaw (Z), pitch (Y), roll (X)
    {
      var x = source.X;
      var y = source.Y;
      var z = source.Z;
      var w = source.W;

      var sqx = x * x;
      var sqy = y * y;
      var sqz = z * z;
      var sqw = w * w;

      var unit = sqx + sqy + sqz + sqw; // If unit = 1 then normalized, otherwise unit is correction factor.
      var test = x * y + z * w;

      if (test > 0.499 * unit) // Singularity at north pole when pitch approaches +90.
      {
        var xw = double.Atan2(x, w);
        return new(double.CreateChecked(xw + xw), Units.AngleUnit.Radian, double.Pi / 2, Units.AngleUnit.Radian, 0, Units.AngleUnit.Radian);
      }

      if (test < -0.499 * unit) // Singularity at south pole when pitch approaches -90.
      {
        var xw = double.Atan2(x, w);
        return new(double.CreateChecked(-(xw + xw)), Units.AngleUnit.Radian, -double.Pi / 2, Units.AngleUnit.Radian, 0, Units.AngleUnit.Radian);
      }

      var h = double.Atan2((y + y) * w - (x + x) * z, sqx - sqy - sqz + sqw);
      var a = double.Asin((test + test) / unit);
      var b = double.Atan2((x + x) * w - (y + y) * z, -sqx + sqy - sqz + sqw);

      return new(double.CreateChecked(h), Units.AngleUnit.Radian, double.CreateChecked(a), Units.AngleUnit.Radian, double.CreateChecked(b), Units.AngleUnit.Radian);
    }

    public static System.Numerics.Matrix4x4 ToMatrix4<TSelf>(this System.Numerics.Quaternion source)
    {
      var xx = source.X * source.X;
      var yy = source.Y * source.Y;
      var zz = source.Z * source.Z;

      var xy = source.X * source.Y;
      var wz = source.Z * source.W;
      var xz = source.Z * source.X;
      var wy = source.Y * source.W;
      var yz = source.Y * source.Z;
      var wx = source.X * source.W;

      return new(
        1 - (yy + zz) * 2, (xy + wz) * 2, (xz - wy) * 2, 0,
        (xy - wz) * 2, 1 - (zz + xx) * 2, (yz + wx) * 2, 0,
        (xz + wy) * 2, (yz - wx) * 2, 1 - (yy + xx) * 2, 0,
        0, 0, 0, 1
      );
    }
  }
}
