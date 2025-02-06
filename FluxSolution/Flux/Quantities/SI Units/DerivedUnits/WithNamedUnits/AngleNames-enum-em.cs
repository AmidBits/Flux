namespace Flux
{
  public static partial class Em
  {
    public static bool IsNamedAngle(this Quantities.AngleNames source, Quantities.Angle angle) => source switch
    {
      Quantities.AngleNames.ZeroAngle => angle == Quantities.Angle.Zero,
      Quantities.AngleNames.AcuteAngle => angle < Quantities.Angle.QuarterTurn,
      Quantities.AngleNames.RightAngle => angle == Quantities.Angle.QuarterTurn,
      Quantities.AngleNames.ObtuseAngle => angle > Quantities.Angle.QuarterTurn && angle < Quantities.Angle.HalfTurn,
      Quantities.AngleNames.StraightAngle => angle == Quantities.Angle.HalfTurn,
      Quantities.AngleNames.ReflexAngle => angle > Quantities.Angle.HalfTurn && angle < Quantities.Angle.FullTurn,
      Quantities.AngleNames.PerigonAngle => angle == Quantities.Angle.FullTurn,
      Quantities.AngleNames.ObliqueAngle => (angle % Quantities.Angle.QuarterTurn) != Quantities.Angle.Zero,
      Quantities.AngleNames.Unknown => angle > Quantities.Angle.FullTurn,
      _ => false,
    };
  }
}
