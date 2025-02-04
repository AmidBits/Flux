namespace Flux.Geometry
{
  /// <summary></summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct ViewPort
  {
    private readonly double m_canvasHeight;
    private readonly double m_canvasWidth;
    private readonly double m_rasterHeight;
    private readonly double m_rasterWidth;
    private readonly System.Numerics.Quaternion m_worldToCamera;

    public ViewPort(double canvasWidth, double canvasHeight, double rasterWidth, double rasterHeight, System.Numerics.Quaternion worldToCamera)
    {
      m_canvasHeight = canvasHeight;
      m_canvasWidth = canvasWidth;

      m_rasterHeight = rasterHeight;
      m_rasterWidth = rasterWidth;

      m_worldToCamera = worldToCamera;
    }
    public ViewPort(System.Numerics.Quaternion worldToCamera)
      : this(2, 2, 1920, 1024, worldToCamera)
    { }

    public void Deconstruct(out double canvasWidth, out double canvasHeight, out double rasterWidth, out double rasterHeight, out System.Numerics.Quaternion worldToCamera) { canvasWidth = m_canvasWidth; canvasHeight = m_canvasHeight; rasterWidth = m_rasterWidth; rasterHeight = m_rasterHeight; worldToCamera = m_worldToCamera; }

    public double CanvasHeight { get => m_canvasHeight; init => m_canvasHeight = value; }
    public double CanvasWidth { get => m_canvasWidth; init => m_canvasWidth = value; }

    public double RasterHeight { get => m_rasterHeight; init => m_rasterHeight = value; }
    public double RasterWidth { get => m_rasterWidth; init => m_rasterWidth = value; }

    public System.Numerics.Quaternion WorldToCamera { get => m_worldToCamera; init => m_worldToCamera = value; }

    /// <summary>Transform the 3D point from world space to camera space.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector3 TransformWorldToCamera(System.Numerics.Vector3 source)
      => System.Numerics.Vector3.Transform(source, m_worldToCamera);

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static System.Numerics.Vector2 TransformCameraToCanvas(System.Numerics.Vector3 source)
      => new(source.X / -source.Z, source.Y / -source.Z); // camera space vector on the canvas, using perspective projection

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection, output whether the point is within the bounds of the canvas.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformCameraToCanvas(System.Numerics.Vector3 source, out bool visible)
    {
      var screen = TransformCameraToCanvas(source);
      visible = (double.Abs(screen.X) > m_canvasWidth || double.Abs(screen.Y) > m_canvasHeight); // if the absolute value screen space of X or Y is greater than the canvas size, X or Y respectively, the point is not visible
      return screen;
    }

    /// <summary>Convert from screen vector to a normalized device coordinate (NDC). The NDC will be in the range [0.0, 1.0].</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformCanvasToNdc(System.Numerics.Vector2 source)
      => new((float)((source.X + m_canvasWidth / 2) / m_canvasWidth), (float)((source.Y + m_canvasHeight / 2) / m_canvasHeight)); // normalize vector, will be in the range [0.0, 1.0]

    /// <summary>Convert from normalize device coordinate (NDC) to pixel coordinate, with the Y coordinate inverted. (Why is that?)</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public System.Numerics.Vector2 TransformNdcToRaster(System.Numerics.Vector2 ndc)
      => new((float)(ndc.X * m_rasterWidth), (float)((1 - ndc.Y) * m_rasterHeight)); // pixel coordinate, with the Y coordinate inverted (Why is that?)
  }
}
