namespace Flux.Geometry
{
  /// <summary></summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct ViewPort
  {
    public static readonly ViewPort Empty;

    private readonly double m_canvasHeight;
    private readonly double m_canvasWidth;
    private readonly int m_rasterHeight;
    private readonly int m_rasterWidth;
    private readonly Quaternion m_worldToCamera;

    public ViewPort(double canvasWidth, double canvasHeight, int rasterWidth, int rasterHeight, Quaternion worldToCamera)
    {
      m_canvasHeight = canvasHeight;
      m_canvasWidth = canvasWidth;

      m_rasterHeight = rasterHeight;
      m_rasterWidth = rasterWidth;

      m_worldToCamera = worldToCamera;
    }
    public ViewPort(Quaternion worldToCamera, int rasterWidth = 1920, int rasterHeight = 1024, float canvasWidth = 2, float canvasHeight = 2)
      : this(canvasWidth, canvasHeight, rasterWidth, rasterHeight, worldToCamera)
    { }

    public double CanvasHeight { get => m_canvasHeight; init => m_canvasHeight = value; }
    public double CanvasWidth { get => m_canvasWidth; init => m_canvasWidth = value; }

    public int RasterHeight { get => m_rasterHeight; init => m_rasterHeight = value; }
    public int RasterWidth { get => m_rasterWidth; init => m_rasterWidth = value; }

    public Quaternion WorldToCamera { get => m_worldToCamera; init => m_worldToCamera = value; }

    /// <summary>Transform the 3D point from world space to camera space.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Vector3 TransformWorldToCamera(Vector3 source)
      => System.Numerics.Vector3.Transform(source.ToVector3(), m_worldToCamera.ToQuaternion()).ToCartesianCoordinate3();

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static Vector2 TransformCameraToCanvas(Vector3 source)
      => new(source.X / -source.Z, source.Y / -source.Z); // camera space vector on the canvas, using perspective projection

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection, output whether the point is within the bounds of the canvas.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Vector2 TransformCameraToCanvas(Vector3 source, out bool visible)
    {
      var screen = TransformCameraToCanvas(source);
      visible = (System.Math.Abs(screen.X) > m_canvasWidth || System.Math.Abs(screen.Y) > m_canvasHeight); // if the absolute value screen space of X or Y is greater than the canvas size, X or Y respectively, the point is not visible
      return screen;
    }

    /// <summary>Convert from screen vector to a normalized device coordinate (NDC). The NDC will be in the range [0.0, 1.0].</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Vector2 TransformCanvasToNdc(Vector2 source)
      => new((source.X + m_canvasWidth / 2) / m_canvasWidth, (source.Y + m_canvasHeight / 2) / m_canvasHeight); // normalize vector, will be in the range [0.0, 1.0]

    /// <summary>Convert from normalize device coordinate (NDC) to pixel coordinate, with the Y coordinate inverted. (Why is that?)</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Vector2 TransformNdcToRaster(Vector2 ndc)
      => new(ndc.X * m_rasterWidth, (1 - ndc.Y) * m_rasterHeight); // pixel coordinate, with the Y coordinate inverted (Why is that?)
  }
}
