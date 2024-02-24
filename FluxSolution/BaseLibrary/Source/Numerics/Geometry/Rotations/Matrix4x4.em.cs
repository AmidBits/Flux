namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>The determinant is a scalar value that is a function of the entries of a square matrix.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Determinant"/></para>
    /// </summary>
    /// <returns>The determinant.</returns>
    public static float GetDeterminant(this System.Numerics.Matrix4x4 source) =>
      source.M14 * source.M23 * source.M32 * source.M41 - source.M13 * source.M24 * source.M32 * source.M41 -
      source.M14 * source.M22 * source.M33 * source.M41 + source.M12 * source.M24 * source.M33 * source.M41 +
      source.M13 * source.M22 * source.M34 * source.M41 - source.M12 * source.M23 * source.M34 * source.M41 -
      source.M14 * source.M23 * source.M31 * source.M42 + source.M13 * source.M24 * source.M31 * source.M42 +
      source.M14 * source.M21 * source.M33 * source.M42 - source.M11 * source.M24 * source.M33 * source.M42 -
      source.M13 * source.M21 * source.M34 * source.M42 + source.M11 * source.M23 * source.M34 * source.M42 +
      source.M14 * source.M22 * source.M31 * source.M43 - source.M12 * source.M24 * source.M31 * source.M43 -
      source.M14 * source.M21 * source.M32 * source.M43 + source.M11 * source.M24 * source.M32 * source.M43 +
      source.M12 * source.M21 * source.M34 * source.M43 - source.M11 * source.M22 * source.M34 * source.M43 -
      source.M13 * source.M22 * source.M31 * source.M44 + source.M12 * source.M23 * source.M31 * source.M44 +
      source.M13 * source.M21 * source.M32 * source.M44 - source.M11 * source.M23 * source.M32 * source.M44 -
      source.M12 * source.M21 * source.M33 * source.M44 + source.M11 * source.M22 * source.M33 * source.M44;

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
