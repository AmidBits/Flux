namespace Flux
{
  public static partial class Em
  {
    public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunesReverse(this SpanBuilder<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = source.Length - 1; index >= 0; index--)
      {
        var ri = source[index];

        if (char.IsLowSurrogate(ri))
        {
          var riM1 = source[--index];

          if (!char.IsHighSurrogate(riM1))
            throw new System.InvalidOperationException("Missing high surrogate. (Reverse enumeration!)");

          list.Add(new System.Text.Rune(riM1, ri));
        }
        else if (char.IsHighSurrogate(ri))
          throw new System.InvalidOperationException("Unexpected high surrogate (missing low surrogate). (Reverse enumeration!)");
        else
          list.Add(new System.Text.Rune(ri));
      }

      return list;
    }
  }
}
