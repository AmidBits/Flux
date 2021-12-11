namespace Flux
{
  public static partial class Convert
  {
    public static string ToWords(int value)
      => IntegerToWords(value).ToString();
    public static string ToWords(long value)
      => IntegerToWords(value).ToString();

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

    private static System.Collections.Generic.Dictionary<long, string> TensDictionaryNumbers = new System.Collections.Generic.Dictionary<long, string>()
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

    private static System.Collections.Generic.Dictionary<long, string> StandardDictionaryNumbers = new System.Collections.Generic.Dictionary<long, string>()
    {
      { 1000000000000000000L, @"Quintillion" },
      { 1000000000000000L, @"Quadrillion" },
      { 1000000000000L, @"Trillion" },
      { 1000000000L, @"Billion" },
      { 1000000L, @"Million" },
      { 1000L, @"Thousand" },
      { 100L, @"Hundred" },
    };

    /// <summary>Convert the specified integer to words.</summary>
    public static System.Text.StringBuilder IntegerToWords(long value)
    {
      if (value < 0)
        return IntegerToWords(-value).Insert(0, @"Negative ");

      var sb = new System.Text.StringBuilder();
      Ge100(value);
      return sb;

      void Lt20(long value)
      {
        if (sb!.Length > 0) sb.Append(' ');
        sb.Append(ZeroThroughNineteen[(int)value]);
      }

      void Ge20Lt100(long value)
      {
        if (value >= 20)
        {
          var remainder = value % 10;
          if (sb.Length > 0) sb.Append(' ');
          sb.Append(TensDictionaryNumbers[value - remainder]);
          value = remainder;
        }

        if (value > 0)
          Lt20(value);
      }

      void Ge100(long value)
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
