namespace Flux
{
  public static partial class Em
  {
    public static bool HasUnitSpacing(this Units.AngleUnit unit, bool preferUnicode)
      => !((unit == Units.AngleUnit.Degree && preferUnicode)
      || (unit == Units.AngleUnit.Gradian && preferUnicode)
      || (unit == Units.AngleUnit.Radian && preferUnicode)
      || unit == Units.AngleUnit.Arcminute
      || unit == Units.AngleUnit.Arcsecond);
  }
}
