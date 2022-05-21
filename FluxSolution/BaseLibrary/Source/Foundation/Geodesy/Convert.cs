namespace Flux
{
  public static partial class Convert
  {
    public static void DecimalDegreesToDms(double decimalDegrees, out double degrees, out double decimalMinutes, out double minutes, out double seconds)
    {
      var absDegrees = System.Math.Abs(decimalDegrees);
      var floorAbsDegrees = System.Math.Floor(absDegrees);

      degrees = System.Math.Sign(decimalDegrees) * floorAbsDegrees;
      decimalMinutes = 60 * (absDegrees - floorAbsDegrees);
      minutes = System.Math.Floor(decimalMinutes);
      seconds = 60 * decimalMinutes - 60 * minutes;
    }

    public static void DmsToDecimalDegrees(double degrees, double minutes, double seconds, out double decimalDegrees) 
      => decimalDegrees = degrees + minutes / 60 + seconds / 3600;
  }
}
