#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static string GetOrdinalIndicator<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      return (source % TSelf.CreateChecked(10) is var d && d < TSelf.CreateChecked(4) && source % TSelf.CreateChecked(100) is var dd && (dd < TSelf.CreateChecked(11) || dd > TSelf.CreateChecked(13)) ? d : TSelf.Zero) switch
      {
        0 => "th",
        1 => "st",
        2 => "nd",
        3 => "rd",
        _ => throw new System.IndexOutOfRangeException()
      };
    }
  }
}
#endif
