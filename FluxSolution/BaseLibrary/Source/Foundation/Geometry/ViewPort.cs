namespace Flux.Geometry
{
  /// <summary></summary>
  public struct ViewPort
    : System.IEquatable<ViewPort>
  {
    public static readonly ViewPort Empty;

    public readonly float CanvasHeight;
    public readonly float CanvasWidth;
    public readonly int RasterHeight;
    public readonly int RasterWidth;
    public readonly System.Numerics.Quaternion WorldToCamera;

    public ViewPort(float canvasWidth, float canvasHeight, int rasterWidth, int rasterHeight, System.Numerics.Quaternion worldToCamera)
    {
      CanvasHeight = canvasHeight;
      CanvasWidth = canvasWidth;

      RasterHeight = rasterHeight;
      RasterWidth = rasterWidth;

      WorldToCamera = worldToCamera;
    }
    public ViewPort(System.Numerics.Quaternion worldToCamera, int rasterWidth = 1920, int rasterHeight = 1024, float canvasWidth = 2, float canvasHeight = 2)
      : this(canvasWidth, canvasHeight, rasterWidth, rasterHeight, worldToCamera)
    { }

    /// <summary>Transform the 3D point from world space to camera space.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector3 TransformWorldToCamera(System.Numerics.Vector3 source)
      => System.Numerics.Vector3.Transform(source, WorldToCamera);

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformCameraToCanvas(System.Numerics.Vector3 source)
      => new(source.X / -source.Z, source.Y / -source.Z); // camera space vector on the canvas, using perspective projection

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection, output whether the point is within the bounds of the canvas.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformCameraToCanvas(System.Numerics.Vector3 source, out bool visible)
    {
      var screen = TransformCameraToCanvas(source);
      visible = (System.Math.Abs(screen.X) > CanvasWidth || System.Math.Abs(screen.Y) > CanvasHeight); // if the absolute value screen space of X or Y is greater than the canvas size, X or Y respectively, the point is not visible
      return screen;
    }

    /// <summary>Convert from screen vector to a normalized device coordinate (NDC). The NDC will be in the range [0.0, 1.0].</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformCanvasToNdc(System.Numerics.Vector2 source)
      => new((source.X + CanvasWidth / 2f) / CanvasWidth, (source.Y + CanvasHeight / 2f) / CanvasHeight); // normalize vector, will be in the range [0.0, 1.0]

    /// <summary>Convert from normalize device coordinate (NDC) to pixel coordinate, with the Y coordinate inverted. (Why is that?)</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformNdcToRaster(System.Numerics.Vector2 ndc)
      => new(ndc.X * RasterWidth, (1f - ndc.Y) * RasterHeight); // pixel coordinate, with the Y coordinate inverted (Why is that?)

    #region Overloaded operators
    public static bool operator ==(ViewPort a, ViewPort b)
      => a.Equals(b);
    public static bool operator !=(ViewPort a, ViewPort b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IEquatable
    public bool Equals(ViewPort other)
      => CanvasHeight == other.CanvasHeight && CanvasWidth == other.CanvasWidth && RasterHeight == other.RasterHeight && RasterWidth == other.RasterWidth && WorldToCamera == other.WorldToCamera;
    #endregion Implemented interfaces

    #region Object overrides
    public override bool Equals(object? obj)
      => obj is ViewPort o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(CanvasHeight, CanvasWidth, RasterHeight, RasterWidth, WorldToCamera);
    public override string? ToString()
      => $"<{GetType().Name}: Canvas=({CanvasWidth}, {CanvasHeight}), Raster=({RasterWidth}, {RasterHeight}), W2C=({WorldToCamera})>";
    #endregion Object overrides
  }
}
