namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static float[,] ToArray(this System.Numerics.Matrix4x4 source)
      => new float[,]
      {
            { source.M11, source.M12, source.M13, source.M14 },
            { source.M21, source.M22, source.M23, source.M24 },
            { source.M31, source.M32, source.M33, source.M34 },
            { source.M41, source.M42, source.M43, source.M44 }
      };

    public static Numerics.Matrix4<float> ToMatrix4(this System.Numerics.Matrix4x4 source)
      => new(
        source.M11, source.M12, source.M13, source.M14,
        source.M21, source.M22, source.M23, source.M24,
        source.M31, source.M32, source.M33, source.M34,
        source.M41, source.M42, source.M43, source.M44
      );

    public static Numerics.Matrix4<TResult> ToMatrix4<TResult>(this System.Numerics.Matrix4x4 source)
      where TResult : System.Numerics.IFloatingPointIeee754<TResult>
      => new(
        TResult.CreateChecked(source.M11), TResult.CreateChecked(source.M12), TResult.CreateChecked(source.M13), TResult.CreateChecked(source.M14),
        TResult.CreateChecked(source.M21), TResult.CreateChecked(source.M22), TResult.CreateChecked(source.M23), TResult.CreateChecked(source.M24),
        TResult.CreateChecked(source.M31), TResult.CreateChecked(source.M32), TResult.CreateChecked(source.M33), TResult.CreateChecked(source.M34),
        TResult.CreateChecked(source.M41), TResult.CreateChecked(source.M42), TResult.CreateChecked(source.M43), TResult.CreateChecked(source.M44)
      );
  }
}
