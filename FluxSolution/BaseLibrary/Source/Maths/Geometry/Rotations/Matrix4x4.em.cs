namespace Flux
{
  public static partial class GeometryExtensionMethods
  {
    public static float[,] ToArray(this System.Numerics.Matrix4x4 source)
      => new float[,]
      {
        { source.M11, source.M12, source.M13, source.M14 },
        { source.M21, source.M22, source.M23, source.M24 },
        { source.M31, source.M32, source.M33, source.M34 },
        { source.M41, source.M42, source.M43, source.M44 }
      };

    public static Geometry.EulerAngles ToEulerAnglesTaitBryanZYX(this System.Numerics.Matrix4x4 source)
      => new(
        System.Math.Atan2(source.M11, source.M21),
        System.Math.Atan2(System.Math.Sqrt(1 - source.M31 * source.M31), -source.M31),
        System.Math.Atan2(source.M33, source.M32)
      );

    public static Geometry.EulerAngles ToEulerAnglesProperEulerZXZ(this System.Numerics.Matrix4x4 source)
      => new(
        System.Math.Atan2(-source.M23, source.M13),
        System.Math.Atan2(source.M33, System.Math.Sqrt(1 - source.M33 * source.M33)),
        System.Math.Atan2(source.M32, source.M31)
      );
  }
}
