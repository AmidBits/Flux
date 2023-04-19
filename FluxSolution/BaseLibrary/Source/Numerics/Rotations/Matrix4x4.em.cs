namespace Flux
{
  public static partial class ExtensionMethodsNumerics
  {
    public static float[,] ToArray(this System.Numerics.Matrix4x4 source)
      => new float[,]
      {
        { source.M11, source.M12, source.M13, source.M14 },
        { source.M21, source.M22, source.M23, source.M24 },
        { source.M31, source.M32, source.M33, source.M34 },
        { source.M41, source.M42, source.M43, source.M44 }
      };

#if NET7_0_OR_GREATER

    public static Numerics.Matrix4 ToMatrix4(this System.Numerics.Matrix4x4 source)
      => new(
        source.M11, source.M12, source.M13, source.M14,
        source.M21, source.M22, source.M23, source.M24,
        source.M31, source.M32, source.M33, source.M34,
        source.M41, source.M42, source.M43, source.M44
      );

#endif
  }
}
