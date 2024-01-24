namespace Flux
{
  public enum OrthantNumbering
  {
    /// <summary>Returns the orthant (quadrant, octant, etc.) of the vector based on the specified center axis vector. This is the more traditional octant.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Quadrant_(plane_geometry)"/>
    /// <see href="https://en.wikipedia.org/wiki/Octant_(solid_geometry)"/>
    Traditional,
    /// <summary>Returns the orthant (quadrant, octant, etc.) of the vector using binary numbering: X = 1, Y = 2 and Z = 4, which are then added up, based on the sign of the respective component.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Orthant"/>
    BinaryNegativeAs1,
    /// <summary>Returns the orthant (quadrant, octant, etc.) of the vector using binary numbering: -X = 1, -Y = 2 and -Z = 4, which are then added up, based on the sign of the respective component.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Orthant"/>
    BinaryPositiveAs1,
  }
}
