namespace Flux
{
  public static partial class Convert
  {
    /// <summary>Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGrouping(this System.Numerics.BigInteger value)
      => ConvertToNamedGrouping(value).ToString();

    /// <summary>Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGrouping(this int value)
      => ConvertToNamedGrouping(value).ToString();
    /// <summary>Converts an integer to named grouping, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static string ToNamedGrouping(this long value)
      => ConvertToNamedGrouping(value).ToString();

    private static System.Collections.Generic.List<string> ZeroThroughNineteen = new System.Collections.Generic.List<string>()
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

    private static System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string> TensDictionaryNumbers = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string>()
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

    private static System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string> StandardDictionaryNumbers = new System.Collections.Generic.Dictionary<System.Numerics.BigInteger, string>()
    {
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000000000000000000000"), @"Septendecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000000000000000000"), @"Sexdecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000000000000000"), @"Quindecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000000000000"), @"Quattuordecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000000000"), @"Tredecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000000"), @"Duodecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000000"), @"Undecillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000000"), @"Decillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000000"), @"Nonillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000000"), @"Octillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000000"), @"Septillion" },
      { System.Numerics.BigInteger.Parse("1000000000000000000000"), @"Sextillion" },
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
