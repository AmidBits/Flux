namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGrouping(System.Numerics.BigInteger value)
      => ConvertToNamedGrouping(value).ToString();

    /// <summary>Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGrouping(int value)
      => ConvertToNamedGrouping(value).ToString();
    /// <summary>Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGrouping(long value)
      => ConvertToNamedGrouping(value).ToString();

    private static readonly System.Collections.Generic.List<string> ZeroThroughNineteen = new()
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

    private static readonly System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string> TensDictionaryNumbers = new()
    {
      { 20, @"Twenty" },
      { 30, @"Thirty" },
      { 40, @"Fourty" },
      { 50, @"Fifty" },
      { 60, @"Sixty" },
      { 70, @"Seventy" },
      { 80, @"Eighty" },
      { 90, @"Ninety" },
      { 100, @"Hundred" }
    };

    public static readonly System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string> StandardDictionaryNumbers = new()
    {
      { System.Numerics.BigInteger.Parse("1e54", System.Globalization.NumberStyles.AllowExponent), @"Septendecillion" },
      { System.Numerics.BigInteger.Parse("1e51", System.Globalization.NumberStyles.AllowExponent), @"Sexdecillion" },
      { System.Numerics.BigInteger.Parse("1e48", System.Globalization.NumberStyles.AllowExponent), @"Quindecillion" },
      { System.Numerics.BigInteger.Parse("1e45", System.Globalization.NumberStyles.AllowExponent), @"Quattuordecillion" },
      { System.Numerics.BigInteger.Parse("1e42", System.Globalization.NumberStyles.AllowExponent), @"Tredecillion" },
      { System.Numerics.BigInteger.Parse("1e39", System.Globalization.NumberStyles.AllowExponent), @"Duodecillion" },
      { System.Numerics.BigInteger.Parse("1e36", System.Globalization.NumberStyles.AllowExponent), @"Undecillion" },
      { System.Numerics.BigInteger.Parse("1e33", System.Globalization.NumberStyles.AllowExponent), @"Decillion" },
      { System.Numerics.BigInteger.Parse("1e30", System.Globalization.NumberStyles.AllowExponent), @"Nonillion" },
      { System.Numerics.BigInteger.Parse("1e27", System.Globalization.NumberStyles.AllowExponent), @"Octillion" },
      { System.Numerics.BigInteger.Parse("1e24", System.Globalization.NumberStyles.AllowExponent), @"Septillion" },
      { System.Numerics.BigInteger.Parse("1e21", System.Globalization.NumberStyles.AllowExponent), @"Sextillion" },
      { 1000000000000000000L, @"Quintillion" },
      { 1000000000000000L, @"Quadrillion" },
      { 1000000000000L, @"Trillion" },
      { 1000000000L, @"Billion" },
      { 1000000L, @"Million" },
      { 1000L, @"Thousand" },
      { 100L, @"Hundred" },
    };

    /// <summary>Convert the specified integer to words.</summary>
    private static System.Text.StringBuilder ConvertToNamedGrouping(System.Numerics.BigInteger value)
    {
      if (value < 0)
        return ConvertToNamedGrouping(-value).Insert(0, @"Negative ");

      var sb = new System.Text.StringBuilder();
      Ge100(value);
      return sb;

      void Lt20(System.Numerics.BigInteger value)
      {
        if (sb!.Length > 0) sb.Append(' ');
        sb.Append(ZeroThroughNineteen[(int)value]);
      }
      void Ge20Lt100(System.Numerics.BigInteger value)
      {
        if (value >= 20)
        {
          var remainder = value % 10;
          if (sb!.Length > 0) sb.Append(' ');
          sb.Append(TensDictionaryNumbers[value - remainder]);
          value = remainder;
        }

        if (value > 0)
          Lt20(value);
      }
      void Ge100(System.Numerics.BigInteger value)
      {
        if (value >= 100)
          foreach (var kvp in StandardDictionaryNumbers)
            if (value >= kvp.Key)
            {
              var quotient = value / kvp.Key;
              Ge100(quotient);
              if (sb.Length > 0) sb.Append(' ');
              sb.Append(kvp.Value);
              value -= quotient * kvp.Key;
            }

        if (value > 0)
          Ge20Lt100(value);
      }
    }
  }
}
