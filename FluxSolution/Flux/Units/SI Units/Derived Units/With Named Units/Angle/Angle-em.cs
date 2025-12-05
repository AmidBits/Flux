namespace Flux
{
  public static partial class UnitsExtensions
  {
    public static bool TryGetAngleNames(this Units.Angle angle, out Units.AngleNames angleName)
    {
      angleName = Units.AngleNames.Unknown;

      var absAngleValue = double.Abs(angle.Value);

      var fullTurn = double.Tau;
      var halfTurn = double.Pi;
      var quarterTurn = double.Pi / 2;

      if (absAngleValue == 0)
        angleName |= Units.AngleNames.ZeroAngle;

      if (absAngleValue < quarterTurn)
        angleName |= Units.AngleNames.AcuteAngle;

      if (absAngleValue == quarterTurn)
        angleName |= Units.AngleNames.RightAngle;

      if (absAngleValue > quarterTurn && absAngleValue < halfTurn)
        angleName |= Units.AngleNames.ObtuseAngle;

      if (absAngleValue == halfTurn)
        angleName |= Units.AngleNames.StraightAngle;

      if (absAngleValue > halfTurn && absAngleValue < fullTurn)
        angleName |= Units.AngleNames.ReflexAngle;

      if (absAngleValue == fullTurn)
        angleName |= Units.AngleNames.PerigonAngle;

      if (absAngleValue % quarterTurn != 0)
        angleName |= Units.AngleNames.ObliqueAngle;

      return angleName != Units.AngleNames.Unknown;
    }
  }
}
