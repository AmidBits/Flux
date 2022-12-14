namespace Flux.Geometry
{
  /// <summary></summary>
  [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
  public readonly record struct ViewPort<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    public static readonly ViewPort<TSelf> Empty;

    private readonly TSelf m_canvasHeight;
    private readonly TSelf m_canvasWidth;
    private readonly TSelf m_rasterHeight;
    private readonly TSelf m_rasterWidth;
    private readonly Numerics.Quaternion<TSelf> m_worldToCamera;

    public ViewPort(TSelf canvasWidth, TSelf canvasHeight, TSelf rasterWidth, TSelf rasterHeight, Numerics.Quaternion<TSelf> worldToCamera)
    {
      m_canvasHeight = canvasHeight;
      m_canvasWidth = canvasWidth;

      m_rasterHeight = rasterHeight;
      m_rasterWidth = rasterWidth;

      m_worldToCamera = worldToCamera;
    }
    public ViewPort(Numerics.Quaternion<TSelf> worldToCamera)
      : this(TSelf.CreateChecked(2), TSelf.CreateChecked(2), TSelf.CreateChecked(1920), TSelf.CreateChecked(1024), worldToCamera)
    { }

    public TSelf CanvasHeight { get => m_canvasHeight; init => m_canvasHeight = value; }
    public TSelf CanvasWidth { get => m_canvasWidth; init => m_canvasWidth = value; }

    public TSelf RasterHeight { get => m_rasterHeight; init => m_rasterHeight = value; }
    public TSelf RasterWidth { get => m_rasterWidth; init => m_rasterWidth = value; }

    public Numerics.Quaternion<TSelf> WorldToCamera { get => m_worldToCamera; init => m_worldToCamera = value; }

    /// <summary>Transform the 3D point from world space to camera space.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Numerics.CartesianCoordinate3<TSelf> TransformWorldToCamera(Numerics.CartesianCoordinate3<TSelf> source)
      => System.Numerics.Vector3.Transform(source.ToVector3<TSelf>(), m_worldToCamera.ToQuaternion()).ToCartesianCoordinate3<TSelf>();

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public static Numerics.CartesianCoordinate2<TSelf> TransformCameraToCanvas(Numerics.CartesianCoordinate3<TSelf> source)
      => new(source.X / -source.Z, source.Y / -source.Z); // camera space vector on the canvas, using perspective projection

    /// <summary>Transform from camera space to vector on the canvas. Use perspective projection, output whether the point is within the bounds of the canvas.</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Numerics.CartesianCoordinate2<TSelf> TransformCameraToCanvas(Numerics.CartesianCoordinate3<TSelf> source, out bool visible)
    {
      var screen = TransformCameraToCanvas(source);
      visible = (TSelf.Abs(screen.X) > m_canvasWidth || TSelf.Abs(screen.Y) > m_canvasHeight); // if the absolute value screen space of X or Y is greater than the canvas size, X or Y respectively, the point is not visible
      return screen;
    }

    /// <summary>Convert from screen vector to a normalized device coordinate (NDC). The NDC will be in the range [0.0, 1.0].</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Numerics.CartesianCoordinate2<TSelf> TransformCanvasToNdc(Numerics.CartesianCoordinate2<TSelf> source)
      => new((source.X + m_canvasWidth.Divide(2)) / m_canvasWidth, (source.Y + m_canvasHeight.Divide(2)) / m_canvasHeight); // normalize vector, will be in the range [0.0, 1.0]

    /// <summary>Convert from normalize device coordinate (NDC) to pixel coordinate, with the Y coordinate inverted. (Why is that?)</summary>
    /// <seealso cref="http://www.scratchapixel.com/lessons/3d-basic-rendering/computing-pixel-coordinates-of-3d-point/mathematics-computing-2d-coordinates-of-3d-points"/>
    public Numerics.CartesianCoordinate2<TSelf> TransformNdcToRaster(Numerics.CartesianCoordinate2<TSelf> ndc)
      => new(ndc.X * m_rasterWidth, (TSelf.One - ndc.Y) * m_rasterHeight); // pixel coordinate, with the Y coordinate inverted (Why is that?)
  }
}
