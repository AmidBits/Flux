namespace Flux.Geometry
{
  /// <summary></summary>
  /// <remarks>NOTE! ViewPort does not currently have any properties.</remarks>
  public struct ViewPort
    : System.IEquatable<ViewPort>
  {
    private float? m_canvasHeight;
    public float CanvasHeight { get => m_canvasHeight ??= 2f; set => m_canvasHeight = value; }
    private float? m_canvasWidth;
    public float CanvasWidth { get => m_canvasWidth ??= 2f; set => m_canvasWidth = value; }

    private int? m_rasterHeight;
    public int RasterHeight { get => m_rasterHeight ??= 1024; set => m_rasterHeight = value; }
    private int? m_rasterWidth;
    public int RasterWidth { get => m_rasterWidth ??= 1920; set => m_rasterWidth = value; }

    private System.Numerics.Quaternion? m_worldToCamera;
    public System.Numerics.Quaternion WorldToCamera { get => m_worldToCamera ??= System.Numerics.Quaternion.Identity; set => m_worldToCamera = value; }

    public ViewPort(float canvasWidth, float canvasHeight, int rasterWidth, int rasterHeight, System.Numerics.Quaternion worldToCamera)
    {
      m_canvasHeight = canvasHeight;
      m_canvasWidth = canvasWidth;

      m_rasterHeight = rasterHeight;
      m_rasterWidth = rasterWidth;

      m_worldToCamera = worldToCamera;
    }

    /// <summary>Transform the 3D point from world space to camera space.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector3 TransformWorldToCamera(System.Numerics.Vector3 source)
      => System.Numerics.Vector3.Transform(source, WorldToCamera);

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformCameraToCanvas(System.Numerics.Vector3 source)
      => new System.Numerics.Vector2(source.X / -source.Z, source.Y / -source.Z); // camera space vector on the canvas, using perspective projection

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
      => new System.Numerics.Vector2((source.X + CanvasWidth / 2f) / CanvasWidth, (source.Y + CanvasHeight / 2f) / CanvasHeight); // normalize vector, will be in the range [0.0, 1.0]

    /// <summary>Convert from normalize device coordinate (NDC) to pixel coordinate, with the Y coordinate inverted. (Why is that?)</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformNdcToRaster(System.Numerics.Vector2 ndc)
      => new System.Numerics.Vector2(ndc.X * RasterWidth, (1f - ndc.Y) * RasterHeight); // pixel coordinate, with the Y coordinate inverted (Why is that?)

    // Operators
    public static bool operator ==(ViewPort a, ViewPort b)
      => a.Equals(b);
    public static bool operator !=(ViewPort a, ViewPort b)
      => !a.Equals(b);

    // IEquatable
    public bool Equals(ViewPort other)
      => ToString() == other.ToString();

    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is ViewPort o && Equals(o);
    public override int GetHashCode()
      => System.HashCode.Combine(typeof(ViewPort));
    public override string? ToString()
      => $"<ViewPort>";
  }
}
