namespace Flux
{
  public static partial class Format
  {
    public static string DoubleFixedPoint => "0." + new string('#', 339);

    public static string UpTo1Decimal { get; } = "0.#";
    public static string UpTo3Decimals { get; } = "0.###";
    public static string UpTo6Decimals { get; } = "0.######";
  }
}
