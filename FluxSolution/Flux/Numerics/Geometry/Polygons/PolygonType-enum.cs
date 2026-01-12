namespace Flux
{
  public static partial class PolygonExtension
  {
    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="apothem"></param>
    /// <returns></returns>
    public static double GetRegularPolygonAreaByApothem(this Numerics.Geometry.Polygons.PolygonType source, double apothem)
      => (int)source * (apothem * apothem) * double.Tan(double.Pi / (int)source);

    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double GetRegularPolygonAreaBySideLength(this Numerics.Geometry.Polygons.PolygonType source, double sideLength)
      => (int)source * (sideLength * sideLength) * double.Cot(double.Pi / (int)source) / 4;

    /// <summary>
    /// <para></para>
    /// <para>The apothem (sometimes abbreviated as apo) of a regular polygon is a line segment from the center to the midpoint of one of its sides. Equivalently, it is the line drawn from the center of the polygon that is perpendicular to one of its sides.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="apothem"></param>
    /// <returns></returns>
    public static double GetRegularPolygonCircumradiusByApothem(this Numerics.Geometry.Polygons.PolygonType source, double apothem)
      => apothem / double.Cos(double.Pi / (int)source);

    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double GetRegularPolygonCircumradiusBySideLength(this Numerics.Geometry.Polygons.PolygonType source, double sideLength)
      => sideLength / (2 * double.Sin(double.Pi / (int)source));

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Regular_polygon#Angles"/>
    /// <returns></returns>
    public static Units.Angle GetRegularPolygonInteriorAngle(this Numerics.Geometry.Polygons.PolygonType source)
      => new(((int)source - 2) * double.Pi / ((int)source));

    /// <summary>An n-sided convex regular polygon is denoted by its Schläfli symbol {n}. For n < 3, we have two degenerate cases (Monogon and Digon).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Schl%C3%A4fli_symbol"/>
    public static int GetSchläfliSymbol(this Numerics.Geometry.Polygons.PolygonType source)
      => (int)source;

    //  // https://en.wikipedia.org/wiki/List_of_polygons#List_of_n-gons_by_Greek_numerical_prefixes
    //  public static string GetSystematicPolygonName(this int source)
    //  {
    //    var sb = new System.Text.StringBuilder();

    //    while (source > 0)
    //    {
    //      var max = SystematicPolygonNames.Last(kvp => kvp.Key <= source);

    //      sb.Append(max.Value);

    //      source -= max.Key;
    //    }

    //    sb.Append(sb.Length > 0 ? "Gon" : "Unknown");

    //    return sb.ToLower(1, sb.Length - 1, System.Globalization.CultureInfo.InvariantCulture).ToString();
    //  }

    //  // https://en.wikipedia.org/wiki/List_of_polygons#List_of_n-gons_by_Greek_numerical_prefixes
    //  public static System.Collections.Generic.SortedDictionary<int, string> SystematicPolygonNames = new()
    //  {
    //    { 1, "Hena" },
    //    { 2, "Di" },
    //    { 3, "Tri" },
    //    { 4, "Tetra" },
    //    { 5, "Penta" },
    //    { 6, "Hexa" },
    //    { 7, "Hepta" },
    //    { 8, "Octa" },
    //    { 9, "Ennea" },
    //    { 10, "Deca" },
    //    { 11, "HenDeca" },
    //    { 12, "DoDeca" },
    //    { 13, "TriDeca" },
    //    { 14, "TetraDeca" },
    //    { 15, "PentaDeca" },
    //    { 16, "HexaDeca" },
    //    { 17, "HeptaDeca" },
    //    { 18, "OctaDeca" },
    //    { 19, "EnneaDeca" },
    //    { 20, "Icosa" },
    //    { 30, "TriConta" },
    //    { 40, "TetraConta" },
    //    { 50, "PentaConta" },
    //    { 60, "HexaConta" },
    //    { 70, "HeptaConta" },
    //    { 80, "OctaConta" },
    //    { 90, "EnneaConta" },
    //    { 100, "Hecta" },
    //    { 200, "DiHecta" },
    //    { 300, "TriHecta" },
    //    { 400, "TetraHecta" },
    //    { 500, "PentaHecta" },
    //    { 600, "HexaHecta" },
    //    { 700, "HeptaHecta" },
    //    { 800, "OctaHecta" },
    //    { 900, "EnneaHecta" },
    //    { 1000, "Chilia" },
    //  };
  }

  namespace Numerics.Geometry.Polygons
  {
    public enum PolygonType
    : byte
    {
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
      //Icosagon = 20,
      //Icosihenagon = 21,
      //Icosidigon = 22,
      //Icositrigon = 23,
      //Icositetragon = 24,
      //Icosipentagon = 25,
      //Icosihexagon = 26,
      //Icosiheptagon = 27,
      //Icosioctagon = 28,
      //Icosienneagon = 29,
      //Triacontagon = 30,
      //Triacontahenagon = 31,
      //Triacontadigon = 32,
      //Triacontatrigon = 33,
      //Triacontatetragon = 34,
      //Triacontapentagon = 35,
      //Triacontahexagon = 36,
      //Triacontaheptagon = 37,
      //Triacontaoctagon = 38,
      //Triacontaenneagon = 39,
      //Tetracontagon = 40,
      //Tetracontahenagon = 41,
      //Tetracontadigon = 42,
      //Tetracontatrigon = 43,
      //Tetracontatetragon = 44,
      //Tetracontapentagon = 45,
      //Tetracontahexagon = 46,
      //Tetracontaheptagon = 47,
      //Tetracontaoctagon = 48,
      //Tetracontaenneagon = 49,
      //Pentacontagon = 50,
      //Pentacontahenagon = 51,
      //Pentacontadigon = 52,
      //Pentacontatrigon = 53,
      //Pentacontatetragon = 54,
      //Pentacontapentagon = 55,
      //Pentacontahexagon = 56,
      //Pentacontaheptagon = 57,
      //Pentacontaoctagon = 58,
      //Pentacontaenneagon = 59,
      //Hexacontagon = 60,
      //Hexacontahenagon = 61,
      //Hexacontadigon = 62,
      //Hexacontatrigon = 63,
      //Hexacontatetragon = 64,
      //Hexacontapentagon = 65,
      //Hexacontahexagon = 66,
      //Hexacontaheptagon = 67,
      //Hexacontaoctagon = 68,
      //Hexacontaenneagon = 69,
      //Heptacontagon = 70,
      //Heptacontahenagon = 71,
      //Heptacontadigon = 72,
      //Heptacontatrigon = 73,
      //Heptacontatetragon = 74,
      //Heptacontapentagon = 75,
      //Heptacontahexagon = 76,
      //Heptacontaheptagon = 77,
      //Heptacontaoctagon = 78,
      //Heptacontaenneagon = 79,
      //Octacontagon = 80,
      //Octacontahenagon = 81,
      //Octacontadigon = 82,
      //Octacontatrigon = 83,
      //Octacontatetragon = 84,
      //Octacontapentagon = 85,
      //Octacontahexagon = 86,
      //Octacontaheptagon = 87,
      //Octacontaoctagon = 88,
      //Octacontaenneagon = 89,
      //Enneacontagon = 90,
      //Enneacontahenagon = 91,
      //Enneacontadigon = 92,
      //Enneacontatrigon = 93,
      //Enneacontatetragon = 94,
      //Enneacontapentagon = 95,
      //Enneacontahexagon = 96,
      //Enneacontaheptagon = 97,
      //Enneacontaoctagon = 98,
      //Enneacontaenneagon = 99,
      //Hectogon = 100
    }
  }
}
