namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the source as a string formatted for use in a file name using "yyyyMMdd HHmmss fffffff".</summary>
    public static string ToStringFileNameFriendly(this System.DateTime source)
      => source.ToString("yyyyMMdd HHmmss fffffff", System.Globalization.CultureInfo.InvariantCulture);

    /// <summary>Returns the System.DateTime 'kind' part only as a string using the format "zzz". This is the <see cref="System.DateTimeKind"/> value used when the value was created.</summary>
    public static string ToStringKind(this System.DateTime source)
      => source.ToString("zzz", System.Globalization.CultureInfo.InvariantCulture);
  }
}
