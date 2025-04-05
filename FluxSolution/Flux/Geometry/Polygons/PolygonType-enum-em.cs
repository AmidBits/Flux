namespace Flux
{
  public static partial class Fx
  {
    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="apothem"></param>
    /// <returns></returns>
    public static double GetRegularPolygonAreaByApothem(this Geometry.Polygons.PolygonType source, double apothem) => (int)source * (apothem * apothem) * double.Tan(double.Pi / (int)source);

    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double GetRegularPolygonAreaBySideLength(this Geometry.Polygons.PolygonType source, double sideLength) => (int)source * (sideLength * sideLength) * Units.Angle.Cot(double.Pi / (int)source) / 4;

    /// <summary>
    /// <para></para>
    /// <para>The apothem (sometimes abbreviated as apo) of a regular polygon is a line segment from the center to the midpoint of one of its sides. Equivalently, it is the line drawn from the center of the polygon that is perpendicular to one of its sides.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="apothem"></param>
    /// <returns></returns>
    public static double GetRegularPolygonCircumradiusByApothem(this Geometry.Polygons.PolygonType source, double apothem) => apothem / double.Cos(double.Pi / (int)source);

    /// <summary></summary>
    /// <param name="source"></param>
    /// <param name="sideLength"></param>
    /// <returns></returns>
    public static double GetRegularPolygonCircumradiusBySideLength(this Geometry.Polygons.PolygonType source, double sideLength) => sideLength / (2 * double.Sin(double.Pi / (int)source));

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/Regular_polygon#Angles"/>
    /// <returns></returns>
    public static Units.Angle GetRegularPolygonInteriorAngle(this Geometry.Polygons.PolygonType source) => new(((int)source - 2) * double.Pi / ((int)source));

    /// <summary>An n-sided convex regular polygon is denoted by its Schläfli symbol {n}. For n < 3, we have two degenerate cases (Monogon and Digon).</summary>
    /// <see href="https://en.wikipedia.org/wiki/Schl%C3%A4fli_symbol"/>
    public static int GetSchläfliSymbol(this Geometry.Polygons.PolygonType source) => (int)source;

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
}
