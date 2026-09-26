namespace Flux
{
  /// <summary>
  /// <para>The XYZ tile scheme is a standard way of addressing and retrieving map tiles in web mapping systems. It uses three numbers — z, x, and y — to uniquely identify each tile in a grid.</para>
  /// <para>Most XYZ tile services use a URL template like: <c>https://server.com/tiles/{z}/{x}/{y}.png</c>.</para>
  /// <para>z (zoom level): The detail level of the map. At z = 0, the entire world fits into a single 256×256 px tile. Each time you zoom in, the number of tiles doubles horizontally and vertically:
  /// <list type="bullet">
  /// <item>z = 1 → 2×2 = 4 tiles</item>
  /// <item>z = 2 → 4×4 = 16 tiles</item>
  /// <item>z = 3 → 8×8 = 64 tiles<i>, and so on...</i></item>
  /// </list>
  /// </para>
  /// <para>x (column): The horizontal position of the tile in the grid (0 to 2^z − 1 at zoom z).</para>
  /// <para>y (row): The vertical position of the tile in the grid (0 to 2^z − 1 at zoom z).</para>
  /// <para>Here, {z}, {x}, and {y} are replaced with the zoom level and tile coordinates. For example, <see href="https://tile.openstreetmap.org/2/1/2.png"/> requests the tile at zoom  2, column  1, row  2.</para>
  /// </summary>
  public static class XyzTileScheme
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Convert geographic coordinates with a zoom value to the XYZ Tile Scheme.</para>
      /// <para>To get a tile from https://tile.openstreetmap.org/z/x/y.png the z, x, and y must be provided. These are the tile coordinates in the standard “slippy map” system used by OpenStreetMap, Google Maps, Mapbox, and most web‑mapping platforms. They tell the tile server exactly which 256×256‑pixel map tile you want.</para>
      /// </summary>
      /// <param name="latitude"></param>
      /// <param name="longitude"></param>
      /// <param name="zoom">
      /// <para>Controls how many tiles exists:</para>
      /// <list type="bullet">
      /// <item>zoom = 0 → 1 tile</item>
      /// <item>zoom = 1 → 2×2 tiles</item>
      /// <item>zoom = 2 → 4×4 tiles</item>
      /// <item>zoom = 18–19 → very detailed street‑level tiles</item>
      /// </list>
      /// </param>
      /// <returns></returns>
      public static void LatLonToTile<TFloat>(TInteger zoom, TFloat latitude, TFloat longitude, out TInteger x, out TInteger y)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>, System.Numerics.IPowerFunctions<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var lat = TFloat.DegreesToRadians(latitude);
        var lon = TFloat.DegreesToRadians(longitude);

        var n = TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(zoom)); // Number of tiles at this zoom level.

        x = TInteger.CreateChecked(TFloat.Floor((lon + TFloat.Pi) / TFloat.Tau * n)); // Tile X (column).
        y = TInteger.CreateChecked(TFloat.Floor((TFloat.One - TFloat.Log(TFloat.Tan(lat) + TFloat.One / TFloat.Cos(lat)) / TFloat.Pi) / TFloat.CreateChecked(2) * n)); // Tile Y (row).
      }

      /// <summary>
      /// <para>Convert XYZ Tile Scheme (z, x, y) to geographic coordinates.</para>
      /// </summary>
      /// <param name="z">
      /// <para>Controls how many tiles exists:</para>
      /// <list type="bullet">
      /// <item>zoom = 0 → 1 tile</item>
      /// <item>zoom = 1 → 2×2 tiles</item>
      /// <item>zoom = 2 → 4×4 tiles</item>
      /// <item>zoom = 18–19 → very detailed street‑level tiles</item>
      /// </list>
      /// </param>
      /// <param name="x">
      /// <list type="bullet">
      /// <item>x = 0 is the far west</item>
      /// <item>x increases as you move east</item>
      /// </list>
      /// </param>
      /// <param name="y">
      /// <list type="bullet">
      /// <item>y = 0 is the top(north)</item>
      /// <item>y increases as you move south</item>
      /// </list>
      /// </param>
      /// <returns></returns>
      public static void TileToLatLon<TFloat>(TInteger z, TInteger x, TInteger y, out TFloat latitude, out TFloat longitude)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.IHyperbolicFunctions<TFloat>, System.Numerics.IPowerFunctions<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
      {
        var n = TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(z));

        var lon = TFloat.CreateChecked(x) / n * TFloat.Tau - TFloat.Pi; // West corner of tile.
        var lat = TFloat.Atan(TFloat.Sinh(TFloat.Pi * (TFloat.One - TFloat.CreateChecked(2) * TFloat.CreateChecked(y) / n))); // North corner of tile.

        longitude = TFloat.RadiansToDegrees(lon);
        latitude = TFloat.RadiansToDegrees(lat);
      }
    }
  }
}
