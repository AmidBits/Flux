namespace Flux
{
  public static partial class Em
  {
    public static bool IsNamedAngle(this Units.AngleNames source, Units.Angle angle) => source switch
    {
      Units.AngleNames.ZeroAngle => angle == Units.Angle.Zero,
      Units.AngleNames.AcuteAngle => angle < Units.Angle.QuarterTurn,
      Units.AngleNames.RightAngle => angle == Units.Angle.QuarterTurn,
      Units.AngleNames.ObtuseAngle => angle > Units.Angle.QuarterTurn && angle < Units.Angle.HalfTurn,
      Units.AngleNames.StraightAngle => angle == Units.Angle.HalfTurn,
      Units.AngleNames.ReflexAngle => angle > Units.Angle.HalfTurn && angle < Units.Angle.FullTurn,
      Units.AngleNames.PerigonAngle => angle == Units.Angle.FullTurn,
      Units.AngleNames.ObliqueAngle => (angle % Units.Angle.QuarterTurn) != Units.Angle.Zero,
      Units.AngleNames.Unknown => angle > Units.Angle.FullTurn,
      _ => false,
    };
  }
}
