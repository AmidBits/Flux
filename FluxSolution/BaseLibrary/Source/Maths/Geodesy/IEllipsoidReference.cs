namespace Flux
{
  public interface IEllipsoidReference
  {
    double InverseFlattening { get; }
    double SemiMajorAxis { get; }
    double SemiMinorAxis { get; }

    Units.Length EquatorialRadius { get; }
    Units.Length PolarRadius { get; }
  }
}
