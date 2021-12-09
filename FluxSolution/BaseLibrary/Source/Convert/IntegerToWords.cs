namespace Flux
{
  public static partial class Convert
  {
    private static string[] ZeroThroughNineteen = { @"Zero", @"One", @"Two", @"Three", @"Four", @"Five", @"Six", @"Seven", @"Eight", @"Nine", @"Ten", @"Eleven", @"Twelve", @"Thirteen", @"Fourteen", @"Fifteen", @"Sixteen", @"Seventeen", @"Eighteen", @"Nineteen" };

    private static System.Collections.Generic.Dictionary<string, long> StandardDictionaryNumbers = new System.Collections.Generic.Dictionary<string, long>()
    {
      { @"Hundred", 100L },
      { @"Thousand", 1000L },
      { @"Million", 1000000L },
      { @"Billion", 1000000000L },
      { @"Trillion", 1000000000000L },
      { @"Quadrillion", 1000000000000000L },
      { @"Quintillion", 1000000000000000000L },
      { string.Empty, long.MaxValue }
    };

    /// <summary>Convert the specified integer to words.</summary>
    public static System.Text.StringBuilder ToWords(long integer)
    {
      if (integer < 0) return ToWords(-integer).Insert(0, @"Negative ");

      var sb = new System.Text.StringBuilder();

      if (integer >= 0 && integer < 20)
        sb.Append(ZeroThroughNineteen[integer]);
      else
      {
        long quotient = 0, remainder = 0;

        if (integer >= 20 && integer < 100)
        {
          remainder = integer % 10;

          sb.Append((integer - remainder) switch { 20 => @"Twenty", 30 => @"Thirty", 40 => @"Fourty", 50 => @"Fifty", 60 => @"Sixty", 70 => @"Seventy", 80 => @"Eighty", 90 => @"Ninety", _ => string.Empty });
        }
        else
        {
          foreach (var pair in StandardDictionaryNumbers.PartitionTuple2(false, (low, high, index) => (low, high)))
            if (integer >= pair.low.Value && integer < pair.high.Value)
            {
              quotient = System.Math.DivRem(integer, pair.low.Value, out remainder);
              sb.Append(ToWords(quotient));
              sb.Append(' ');
              sb.Append(pair.low.Key);
              break;
            }
        }

        if (remainder > 0)
        {
          sb.Append(' ');
          sb.Append(ToWords(remainder));
        }
      }

      return sb;
    }
  }
}
