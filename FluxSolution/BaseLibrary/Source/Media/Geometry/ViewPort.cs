namespace Flux.Media.Shapes
{
  public struct ViewPort
    : System.IEquatable<ViewPort>, System.IFormattable
  {
    /// <summary>Transform the 3D point from world space to camera space.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector3 TransformToCameraSpace(System.Numerics.Vector3 source, ref System.Numerics.Matrix4x4 worldToCamera)
      => System.Numerics.Vector3.Transform(source, worldToCamera);

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformToScreen(System.Numerics.Vector3 source)
      => new System.Numerics.Vector2(source.X / -source.Z, source.Y / -source.Z); // camera space vector on the canvas, using perspective projection

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection, output whether the point is within the bounds of the canvas.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformToScreen(System.Numerics.Vector3 source, out bool visible, float canvasHeight = 2, float canvasWidth = 2)
    {
      var screen = TransformToScreen(source);

      visible = (System.Math.Abs(screen.X) > canvasWidth || System.Math.Abs(screen.Y) > canvasHeight); // if the absolute value screen space of X or Y is greater than the canvas size, X or Y respectively, the point is not visible

      return screen;
    }

    /// <summary>Convert from screen vector to a normalized device coordinate (NDC). The NDC will be in the range [0.0, 1.0].</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformToNdc(System.Numerics.Vector2 source, float canvasHeight = 2, float canvasWidth = 2)
      => new System.Numerics.Vector2((source.X + canvasWidth / 2F) / canvasWidth, (source.Y + canvasHeight / 2F) / canvasHeight); // normalize vector, will be in the range [0.0, 1.0]

    /// <summary>Convert from normalize device coordinate (NDC) to pixel coordinate, with the Y coordinate inverted. (Why is that?)</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformToRaster(System.Numerics.Vector2 source, float rasterHeight, float rasterWidth)
      => new System.Numerics.Vector2(source.X * rasterWidth, (1F - source.Y) * rasterHeight); // pixel coordinate, with the Y coordinate inverted (Why is that?)

    // Operators
    public static bool operator ==(ViewPort a, ViewPort b)
      => a.Equals(b);
    public static bool operator !=(ViewPort a, ViewPort b)
      => !a.Equals(b);
    // IEquatable
    public bool Equals(ViewPort other)
      => ToString() == other.ToString(); // NOTE THAT ViewPort does not currently have any properties.
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? provider)
      => $"<ViewPort>";
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is ViewPort && Equals(obj);
    public override int GetHashCode()
      => Flux.HashCode.CombineCore(0);
    public override string? ToString()
      => base.ToString();
  }
}
