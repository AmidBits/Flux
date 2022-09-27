#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Gets the ordinal indicator for the number. E.g. "st" for 1 and "nd" for 122.</summary>
    public static string GetOrdinalIndicatorString<TSelf>(TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var ones = source % TSelf.CreateChecked(10);

      var lessThan4 = ones < TSelf.CreateChecked(4);

      var tens = source % TSelf.CreateChecked(100);

      var between11And13 = (tens < TSelf.CreateChecked(11) || tens > TSelf.CreateChecked(13));

      var i = lessThan4 && between11And13 ? ones : TSelf.Zero;

      if (TSelf.IsZero(i)) return "th"; // If 0.
      else if (TSelf.IsZero(--i)) return "st"; // If 1.
      else if (TSelf.IsZero(--i)) return "nd"; // If 2.
      else if (TSelf.IsZero(--i)) return "rd"; // If 3.
      else throw new System.IndexOutOfRangeException();

      //  return (t < TSelf.CreateChecked(4) && (h < TSelf.CreateChecked(11) || h > TSelf.CreateChecked(13)) ? t : TSelf.Zero) switch
      //{
      //  0 => "th",
      //  1 => "st",
      //  2 => "nd",
      //  3 => "rd",
      //  _ => throw new System.IndexOutOfRangeException()
      //};
    }

    /// <summary>PREVIEW! Creates a new string with the number and the ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</summary>
    public static string ToOrdinalIndicatorString<TSelf>(this TSelf source)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => $"{source}{GetOrdinalIndicatorString(source)}";
  }
}
#endif
