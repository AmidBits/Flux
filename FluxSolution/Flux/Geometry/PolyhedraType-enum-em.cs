namespace Flux
{
  public static partial class Em
  {
    /// <summary>A regular polyhedron that has q regular p-sided polygon faces around each vertex is represented by {p,q} (using System.ValueType(p, q) in .NET).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Schl%C3%A4fli_symbol"/>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static (int p, int q) GetSchläfliSymbol(this Geometry.PlatonicSolid source)
      => source switch
      {
        Geometry.PlatonicSolid.Tetrahedron => (3, 3),
        Geometry.PlatonicSolid.Cube => (4, 3),
        Geometry.PlatonicSolid.Octahedron => (3, 4),
        Geometry.PlatonicSolid.Dodecahedron => (5, 3),
        Geometry.PlatonicSolid.Icosahedron => (3, 5),
        _ => throw new System.ArgumentOutOfRangeException(nameof(source)),
      };
  }
}
