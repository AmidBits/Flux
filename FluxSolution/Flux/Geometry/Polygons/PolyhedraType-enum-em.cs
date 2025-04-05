namespace Flux
{
  public static partial class Em
  {
    /// <summary>A regular polyhedron that has q regular p-sided polygon faces around each vertex is represented by {p,q} (using System.ValueType(p, q) in .NET).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Schl%C3%A4fli_symbol"/>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static (int p, int q) GetSchläfliSymbol(this Geometry.Polygons.PlatonicSolid source)
      => source switch
      {
        Geometry.Polygons.PlatonicSolid.Tetrahedron => (3, 3),
        Geometry.Polygons.PlatonicSolid.Cube => (4, 3),
        Geometry.Polygons.PlatonicSolid.Octahedron => (3, 4),
        Geometry.Polygons.PlatonicSolid.Dodecahedron => (5, 3),
        Geometry.Polygons.PlatonicSolid.Icosahedron => (3, 5),
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
  }
}
