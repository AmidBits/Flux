#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGroupingString<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => ConvertToNamedGrouping(value).ToString();

    #region Named Grouping
    private static System.Collections.Generic.List<string> ZeroThroughNineteen()
      => new()
      {
        @"Zero",
        @"One",
        @"Two",
        @"Three",
        @"Four",
        @"Five",
        @"Six",
        @"Seven",
        @"Eight",
        @"Nine",
        @"Ten",
        @"Eleven",
        @"Twelve",
        @"Thirteen",
        @"Fourteen",
        @"Fifteen",
        @"Sixteen",
        @"Seventeen",
        @"Eighteen",
        @"Nineteen"
      };

    private static System.Collections.Generic.Dictionary<TSelf, string> TensDictionaryNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new()
      {
        { TSelf.CreateChecked(20), @"Twenty" },
        { TSelf.CreateChecked(30), @"Thirty" },
        { TSelf.CreateChecked(40), @"Fourty" },
        { TSelf.CreateChecked(50), @"Fifty" },
        { TSelf.CreateChecked(60), @"Sixty" },
        { TSelf.CreateChecked(70), @"Seventy" },
        { TSelf.CreateChecked(80), @"Eighty" },
        { TSelf.CreateChecked(90), @"Ninety" },
        { TSelf.CreateChecked(100), @"Hundred" }
      };

    public static System.Collections.Generic.Dictionary<TSelf, string> StandardDictionaryNumbers<TSelf>()
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => new()
      {
        { TSelf.Parse("1e54", System.Globalization.NumberStyles.AllowExponent, null), @"Septendecillion" },
        { TSelf.Parse("1e51", System.Globalization.NumberStyles.AllowExponent, null), @"Sexdecillion" },
        { TSelf.Parse("1e48", System.Globalization.NumberStyles.AllowExponent, null), @"Quindecillion" },
        { TSelf.Parse("1e45", System.Globalization.NumberStyles.AllowExponent, null), @"Quattuordecillion" },
        { TSelf.Parse("1e42", System.Globalization.NumberStyles.AllowExponent, null), @"Tredecillion" },
        { TSelf.Parse("1e39", System.Globalization.NumberStyles.AllowExponent, null), @"Duodecillion" },
        { TSelf.Parse("1e36", System.Globalization.NumberStyles.AllowExponent, null), @"Undecillion" },
        { TSelf.Parse("1e33", System.Globalization.NumberStyles.AllowExponent, null), @"Decillion" },
        { TSelf.Parse("1e30", System.Globalization.NumberStyles.AllowExponent, null), @"Nonillion" },
        { TSelf.Parse("1e27", System.Globalization.NumberStyles.AllowExponent, null), @"Octillion" },
        { TSelf.Parse("1e24", System.Globalization.NumberStyles.AllowExponent, null), @"Septillion" },
        { TSelf.Parse("1e21", System.Globalization.NumberStyles.AllowExponent, null), @"Sextillion" },
        { TSelf.CreateChecked(1000000000000000000L), @"Quintillion" },
        { TSelf.CreateChecked(1000000000000000L), @"Quadrillion" },
        { TSelf.CreateChecked(1000000000000L), @"Trillion" },
        { TSelf.CreateChecked(1000000000L), @"Billion" },
        { TSelf.CreateChecked(1000000L), @"Million" },
        { TSelf.CreateChecked(1000L), @"Thousand" },
        { TSelf.CreateChecked(100L), @"Hundred" },
      };

    /// <summary>Convert the specified integer to words.</summary>
    private static System.Text.StringBuilder ConvertToNamedGrouping<TSelf>(TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (value < TSelf.Zero)
        return ConvertToNamedGrouping(-value).Insert(0, @"Negative ");

      var sb = new System.Text.StringBuilder();
      Ge100(value);
      return sb;

      void Lt20(TSelf value)
      {
        if (sb!.Length > 0) sb.Append(' ');
        sb.Append(ZeroThroughNineteen()[value.ConvertTo<TSelf, int>(out var ztn)]);
      }
      void Ge20Lt100(TSelf value)
      {
        if (value >= TSelf.CreateChecked(20))
        {
          var remainder = value % TSelf.CreateChecked(10);
          if (sb!.Length > 0) sb.Append(' ');
          sb.Append(TensDictionaryNumbers<TSelf>()[value - remainder]);
          value = remainder;
        }

        if (value > TSelf.Zero)
          Lt20(value);
      }
      void Ge100(TSelf value)
      {
        if (value >= TSelf.CreateChecked(100))
          foreach (var kvp in StandardDictionaryNumbers<TSelf>())
            if (value >= kvp.Key)
            {
              var quotient = value / kvp.Key;
              Ge100(quotient);
              if (sb.Length > 0) sb.Append(' ');
              sb.Append(kvp.Value);
              value -= quotient * kvp.Key;
            }

        if (value > TSelf.Zero)
          Ge20Lt100(value);
      }
    }
    #endregion Named Grouping
  }
}
#endif
