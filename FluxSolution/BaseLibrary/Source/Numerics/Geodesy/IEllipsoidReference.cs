namespace Flux
{
  public interface IEllipsoidReference
  {
    double InverseFlattening { get; }
    double SemiMajorAxis { get; }
    double SemiMinorAxis { get; }

    double EquatorialRadius { get; }
    double PolarRadius { get; }
  }
}
