namespace Flux.Geometry
{
  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Polyhedron"/>
  public enum PolyhedronType
    : byte
  {
    #region Convex Regular polyhedron.
    Tetrahedron = 4,
    Cube = 6,
    Octahedron = 8,
    Dodecahedron = 12,
    Icosahedron = 20,
    #endregion Convex Regular polyhedron.
  }

  /// <summary></summary>
  /// <see href="https://en.wikipedia.org/wiki/Polyhedron"/>
  public enum PlatonicSolid
    : byte
  {
    #region Convex Regular polyhedron.
    Tetrahedron = PolyhedronType.Tetrahedron,
    Cube = PolyhedronType.Cube,
    Octahedron = PolyhedronType.Octahedron,
    Dodecahedron = PolyhedronType.Dodecahedron,
    Icosahedron = PolyhedronType.Icosahedron,
    #endregion Convex Regular polyhedron.
  }
}
