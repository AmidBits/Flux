namespace Flux
{
  public static partial class Fx
  {
    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="apothem"></param>
    /// <returns></returns>
    public static double GetRegularPolygonAreaByApothem(this PolygonType source, double apothem)
      => (int)source * (apothem * apothem) * System.Math.Tan(System.Math.PI / (int)source);
    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double GetRegularPolygonAreaBySideLength(this PolygonType source, double sideLength)
      => 0.25 * (int)source * (sideLength * sideLength) * Quantities.Angle.Cot(System.Math.PI / (int)source);

    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="apothem"></param>
    /// <returns></returns>
    public static double GetRegularPolygonCircumradiusByApothem(this PolygonType source, double apothem)
      => apothem / System.Math.Cos(System.Math.PI / (int)source);
    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double GetRegularPolygonCircumradiusBySideLength(this PolygonType source, double sideLength)
      => sideLength / (2 * System.Math.Sin(System.Math.PI / (int)source));

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Regular_polygon#Angles"/>
    /// <returns></returns>
    public static Quantities.Angle GetRegularPolygonInteriorAngle(this PolygonType source)
      => new(((int)source - 2) * System.Math.PI / ((int)source));

    /// <summary>An n-sided convex regular polygon is denoted by its Schläfli symbol {n}. For n < 3, we have two degenerate cases (Monogon and Digon).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Schl%C3%A4fli_symbol"/>
    public static int GetSchläfliSymbol(this PolygonType source)
      => (int)source;
  }

  public enum PolygonType
      : byte
  {
    #region Enumeration of polygon types named from the number of edges or vertices it consists of.
    Monogon = 1,
    Digon = 2,
    Trigon = 3,
    Tetragon = 4,
    Pentagon = 5,
    Hexagon = 6,
    Heptagon = 7,
    Octagon = 8,
    Nonagon = 9,
    Decagon = 10,
    Hendecagon = 11,
    Dodecagon = 12,
    Tridecagon = 13,
    Tetradecagon = 14,
    Pentadecagon = 15,
    Hexadecagon = 16,
    Heptadecagon = 17,
    Octadecagon = 18,
    Enneadecagon = 19,
    Icosagon = 20,
    Icosihenagon = 21,
    Icosidigon = 22,
    Icositrigon = 23,
    Icositetragon = 24,
    Icosipentagon = 25,
    Icosihexagon = 26,
    Icosiheptagon = 27,
    Icosioctagon = 28,
    Icosienneagon = 29,
    Triacontagon = 30,
    Triacontahenagon = 31,
    Triacontadigon = 32,
    Triacontatrigon = 33,
    Triacontatetragon = 34,
    Triacontapentagon = 35,
    Triacontahexagon = 36,
    Triacontaheptagon = 37,
    Triacontaoctagon = 38,
    Triacontaenneagon = 39,
    Tetracontagon = 40,
    Tetracontahenagon = 41,
    Tetracontadigon = 42,
    Tetracontatrigon = 43,
    Tetracontatetragon = 44,
    Tetracontapentagon = 45,
    Tetracontahexagon = 46,
    Tetracontaheptagon = 47,
    Tetracontaoctagon = 48,
    Tetracontaenneagon = 49,
    Pentacontagon = 50,
    Pentacontahenagon = 51,
    Pentacontadigon = 52,
    Pentacontatrigon = 53,
    Pentacontatetragon = 54,
    Pentacontapentagon = 55,
    Pentacontahexagon = 56,
    Pentacontaheptagon = 57,
    Pentacontaoctagon = 58,
    Pentacontaenneagon = 59,
    Hexacontagon = 60,
    Hexacontahenagon = 61,
    Hexacontadigon = 62,
    Hexacontatrigon = 63,
    Hexacontatetragon = 64,
    Hexacontapentagon = 65,
    Hexacontahexagon = 66,
    Hexacontaheptagon = 67,
    Hexacontaoctagon = 68,
    Hexacontaenneagon = 69,
    Heptacontagon = 70,
    Heptacontahenagon = 71,
    Heptacontadigon = 72,
    Heptacontatrigon = 73,
    Heptacontatetragon = 74,
    Heptacontapentagon = 75,
    Heptacontahexagon = 76,
    Heptacontaheptagon = 77,
    Heptacontaoctagon = 78,
    Heptacontaenneagon = 79,
    Octacontagon = 80,
    Octacontahenagon = 81,
    Octacontadigon = 82,
    Octacontatrigon = 83,
    Octacontatetragon = 84,
    Octacontapentagon = 85,
    Octacontahexagon = 86,
    Octacontaheptagon = 87,
    Octacontaoctagon = 88,
    Octacontaenneagon = 89,
    Enneacontagon = 90,
    Enneacontahenagon = 91,
    Enneacontadigon = 92,
    Enneacontatrigon = 93,
    Enneacontatetragon = 94,
    Enneacontapentagon = 95,
    Enneacontahexagon = 96,
    Enneacontaheptagon = 97,
    Enneacontaoctagon = 98,
    Enneacontaenneagon = 99,
    Hectogon = 100
    #endregion Enumeration of polygon types named from the number of edges or vertices it consists of.
  }
}
