namespace Flux
{
  public static partial class Em
  {
    public static System.Numerics.BigInteger GetUnitFactor(this Units.DigitalInformationUnit unit)
      => unit switch
      {
        Units.DigitalInformationUnit.Byte => 1,

        Units.DigitalInformationUnit.kibiByte => 1024,
        Units.DigitalInformationUnit.mebiByte => System.Numerics.BigInteger.Pow(1024, 2),
        Units.DigitalInformationUnit.gibiByte => System.Numerics.BigInteger.Pow(1024, 3),
        Units.DigitalInformationUnit.tebiByte => System.Numerics.BigInteger.Pow(1024, 4),
        Units.DigitalInformationUnit.pebiByte => System.Numerics.BigInteger.Pow(1024, 5),
        Units.DigitalInformationUnit.exbiByte => System.Numerics.BigInteger.Pow(1024, 6),
        Units.DigitalInformationUnit.zebiByte => System.Numerics.BigInteger.Pow(1024, 7),
        Units.DigitalInformationUnit.yobiByte => System.Numerics.BigInteger.Pow(1024, 8),

        _ => System.Numerics.BigInteger.Zero
      };

    public static bool TryGetUnitFactor(this Units.DigitalInformationUnit unit, out System.Numerics.BigInteger factor)
      => (factor = unit.GetUnitFactor()) != System.Numerics.BigInteger.Zero;

    public static string GetUnitName(this Units.DigitalInformationUnit unit, bool preferPlural)
      => unit.ToString().ToPluralUnitName(preferPlural);

    public static string GetUnitSymbol(this Units.DigitalInformationUnit unit, bool preferUnicode = false)
      => unit switch
      {
        Units.DigitalInformationUnit.Byte => "B",

        Units.DigitalInformationUnit.kibiByte => "KiB",
        Units.DigitalInformationUnit.mebiByte => "MiB",
        Units.DigitalInformationUnit.gibiByte => "GiB",
        Units.DigitalInformationUnit.tebiByte => "TiB",
        Units.DigitalInformationUnit.pebiByte => "PiB",
        Units.DigitalInformationUnit.exbiByte => "EiB",
        Units.DigitalInformationUnit.zebiByte => "ZiB",
        Units.DigitalInformationUnit.yobiByte => "YiB",

        _ => string.Empty,
      };

    public static bool TryGetUnitSymbol(this Units.DigitalInformationUnit unit, out string symbol, bool preferUnicode = false)
      => !string.IsNullOrEmpty(symbol = unit.GetUnitSymbol(preferUnicode));
  }
}
