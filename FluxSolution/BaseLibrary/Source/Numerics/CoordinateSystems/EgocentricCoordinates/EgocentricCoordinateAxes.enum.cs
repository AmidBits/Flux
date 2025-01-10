namespace Flux
{
  //public static partial class EgocentricCoordinates
  //{

  //  private static string[] SynonymsForBackward = new string[] { "aft", "bow", "back", "stern", "astern", };
  //  private static string[] SynonymsForDown = new string[] { "low", "down", "below", "under", "beneath", "declin", };
  //  private static string[] SynonymsForForward = new string[] { "bow", "prow", "ahead", "front", "forward", };
  //  private static string[] SynonymsForLeft = new string[] { "aport", "left", "port", "sinist", };
  //  private static string[] SynonymsForRight = new string[] { "dext", "right", "starboard", "astarboard" };
  //  private static string[] SynonymsForUp = new string[] { "up", "high", "over", "above", "raise", "inclin", };

  //  public static bool TryParseEgocentricCoordinateAxes(this string source, out EgocentricCoordinateAxes result)
  //  {
  //    if (SynonymsForBackward.Any(s => source.StartsWith(s, StringComparison.InvariantCultureIgnoreCase)))
  //    {
  //      result = EgocentricCoordinateAxes.Backward;
  //      return true;
  //    }

  //    if (SynonymsForDown.Any(s => source.StartsWith(s, StringComparison.InvariantCultureIgnoreCase)))
  //    {
  //      result = EgocentricCoordinateAxes.Down;
  //      return true;
  //    }

  //    if (SynonymsForForward.Any(s => source.StartsWith(s, StringComparison.InvariantCultureIgnoreCase)))
  //    {
  //      result = EgocentricCoordinateAxes.Forward;
  //      return true;
  //    }

  //    if (SynonymsForLeft.Any(s => source.StartsWith(s, StringComparison.InvariantCultureIgnoreCase)))
  //    {
  //      result = EgocentricCoordinateAxes.Left;
  //      return true;
  //    }

  //    if (SynonymsForRight.Any(s => source.StartsWith(s, StringComparison.InvariantCultureIgnoreCase)))
  //    {
  //      result = EgocentricCoordinateAxes.Right;
  //      return true;
  //    }

  //    if (SynonymsForUp.Any(s => source.StartsWith(s, StringComparison.InvariantCultureIgnoreCase)))
  //    {
  //      result = EgocentricCoordinateAxes.Up;
  //      return true;
  //    }

  //    result = default;
  //    return false;
  //  }
  //}

  namespace Coordinates
  {
    /// <summary>
    /// <para>Body relative directions (a.k.a. egocentric-coordinates) are geometrical orientations relative to a body, such as a human person's.</para>
    /// <see href="https://en.wikipedia.org/wiki/Body_relative_direction"/>
    /// </summary>
    /// <remarks><see cref="EgocentricCoordinateAxes"/> represents the most common ones, i.e. the ones from <see cref="EgocentricCoordinateAxisLR"/>, <see cref="EgocentricCoordinateAxisFB"/> and <see cref="EgocentricCoordinateAxisUD"/>. They form three pairs of orthogonal axes.</remarks>
    [System.Flags]
    public enum EgocentricCoordinateAxes
    {
      Left = EgocentricCoordinateAxisLR.Left,
      Right = EgocentricCoordinateAxisLR.Right,
      Forward = EgocentricCoordinateAxisFB.Forward,
      Backward = EgocentricCoordinateAxisFB.Backward,
      Up = EgocentricCoordinateAxisUD.Up,
      Down = EgocentricCoordinateAxisUD.Down,
    };
  }
}
