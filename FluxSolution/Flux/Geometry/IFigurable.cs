namespace Flux.Geometry
{
  internal interface IFigurable
  {
    double Perimeter { get; }

    double SurfaceArea { get; }

    bool Contains(double x, double y);
  }
}
