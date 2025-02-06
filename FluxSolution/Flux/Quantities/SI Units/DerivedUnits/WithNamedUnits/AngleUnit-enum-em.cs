namespace Flux
{
  public static partial class Em
  {
    public static bool HasUnitSpacing(this Quantities.AngleUnit unit, bool preferUnicode)
      => !((unit == Quantities.AngleUnit.Degree && preferUnicode)
      || (unit == Quantities.AngleUnit.Gradian && preferUnicode)
      || (unit == Quantities.AngleUnit.Radian && preferUnicode)
      || unit == Quantities.AngleUnit.Arcminute
      || unit == Quantities.AngleUnit.Arcsecond);
  }
}
