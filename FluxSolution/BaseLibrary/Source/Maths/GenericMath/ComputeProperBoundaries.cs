namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Asserts the number is a valid nth root (throws an exception if not).</summary>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static void SelectProperBoundaries<TSelf>(TSelf value, TSelf tzOrigin, TSelf afzOrigin, TSelf tzProper, TSelf afzProper, out TSelf tz, out TSelf afz)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      tz = (tzOrigin == value) ? tzProper : tzOrigin;
      afz = (afzOrigin == value) ? afzProper : afzOrigin;
    }

#else

#endif
  }
}
