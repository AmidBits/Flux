namespace Flux.Geometry
{
  public readonly record struct Point
  {
    private readonly System.Runtime.Intrinsics.Vector128<double> m_v;

    public Point(System.Runtime.Intrinsics.Vector128<double> point) => m_v = point;
    public Point(double x, double y) => m_v = System.Runtime.Intrinsics.Vector128.Create(x, y);

    public System.Runtime.Intrinsics.Vector128<double> V => m_v;

    public double X => m_v[0];
    public double Y => m_v[1];

    public System.Drawing.PointF ToPointF() => new((float)X, (float)Y);

    public System.Numerics.Vector2 ToVector2() => new((float)X, (float)Y);
  }
}
