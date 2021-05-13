//namespace Flux.Media
//{
//  public static partial class Geometry
//  {
//    /// <summary>(2D/3D) Calculate the angle between the source vector and the specified target vector. 
//    /// When dot eq 0 then the vectors are perpendicular.
//    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
//    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
//    /// </summary>
//    //public static double AngleBetween(this System.Numerics.Vector3 source, System.Numerics.Vector3 target)
//    //  => System.Math.Acos(Flux.Maths.Clamp(System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(source), System.Numerics.Vector3.Normalize(target)), -1, 1));
//    /// <summary>(2D) Calculate the angle between the source vector and the specified target vector.
//    /// When dot eq 0 then the vectors are perpendicular.
//    /// When dot gt 0 then the angle is less than 90 degrees (dot=1 can be interpreted as the same direction).
//    /// When dot lt 0 then the angle is greater than 90 degrees (dot=-1 can be interpreted as the opposite direction).
//    /// </summary>
//    //public static double AngleBetween(this System.Numerics.Vector2 source, System.Numerics.Vector2 target)
//    //  => System.Math.Acos(Flux.Maths.Clamp(System.Numerics.Vector2.Dot(System.Numerics.Vector2.Normalize(source), System.Numerics.Vector2.Normalize(target)), -1, 1));

//    ///// <summary>Returns an intermediary point between the two specified points. 0 equals a, 0.5 equals the midpoint and 1 equals b.</summary>>
//    //public static System.Numerics.Vector2 IntermediaryPoint(System.Numerics.Vector2 a, System.Numerics.Vector2 b, float scalar = 0.5f)
//    //  => (a + b) * scalar;
//    ///// <summary>Returns an intermediary point between the two specified points. 0 equals a, 0.5 equals the midpoint and 1 equals b.</summary>>
//    //public static System.Numerics.Vector3 IntermediaryPoint(System.Numerics.Vector3 a, System.Numerics.Vector3 b, float scalar = 0.5f)
//    //  => (a + b) * scalar;

//    /// <summary>Returns the midpoint of the two specified points</summary>>
//    //public static System.Numerics.Vector2 Midpoint(System.Numerics.Vector2 a, System.Numerics.Vector2 b) => (a + b) / 2;
//    /// <summary>Returns the midpoint of the two specified points</summary>>
//    //public static System.Numerics.Vector3 Midpoint(System.Numerics.Vector3 a, System.Numerics.Vector3 b) => (a + b) / 2;

//    /// <summary>Returns a point -90 degrees perpendicular to the point, i.e. the point rotated 90 degrees counter clockwise. Only X and Y.</summary>
//    //public static System.Numerics.Vector2 PerpendicularCcw(this System.Numerics.Vector2 source)
//    //  => new System.Numerics.Vector2(-source.Y, source.X);
//    /// <summary>Returns a point 90 degrees perpendicular to the point, i.e. the point rotated 90 degrees clockwise. Only X and Y.</summary>
//    //public static System.Numerics.Vector2 PerpendicularCw(this System.Numerics.Vector2 source)
//    //  => new System.Numerics.Vector2(source.Y, -source.X);

//    /// <summary>Rotate the vector around the specified axis.</summary>
//    //public static System.Numerics.Vector2 RotateAroundAxis(this System.Numerics.Vector2 source, System.Numerics.Vector3 axis, float angle)
//    //  => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromAxisAngle(axis, angle));
//    /// <summary>Rotate the vector around the world axes.</summary>
//    //public static System.Numerics.Vector2 RotateAroundWorldAxes(this System.Numerics.Vector2 source, float yaw, float pitch, float roll)
//    //  => System.Numerics.Vector2.Transform(source, System.Numerics.Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

//    //public static System.Numerics.Vector2 ToVector2(this System.Numerics.Vector3 source)
//    //  => new System.Numerics.Vector2(source.X, source.Y);
//    //public static System.Numerics.Vector3 ToVector3(this System.Numerics.Vector2 source)
//    //  => new System.Numerics.Vector3(source, 0);

//    //public static System.Collections.Generic.IEnumerable<System.Numerics.Vector2> ToVector2(this System.Collections.Generic.IEnumerable<System.Numerics.Vector3> source)
//    //  => source.Select(v3 => new System.Numerics.Vector2(v3.X, v3.Y));
//    //public static System.Collections.Generic.IEnumerable<System.Numerics.Vector3> ToVector3(this System.Collections.Generic.IEnumerable<System.Numerics.Vector2> source)
//    //  => source.Select(v2 => new System.Numerics.Vector3(v2, 0));
//  }
//}
