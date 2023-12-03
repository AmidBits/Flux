namespace Flux
{
  public static partial class Em
  {
    public static System.Collections.Generic.List<System.Text.Rune> EnumerateRunes(this SpanBuilder<char> source)
    {
      var list = new System.Collections.Generic.List<System.Text.Rune>();

      for (var index = 0; index < source.Length; index++)
      {
        var ri = source[index];

        if (char.IsHighSurrogate(ri))
        {
          var riP1 = source[++index];

          if (!char.IsLowSurrogate(riP1))
            throw new System.InvalidOperationException("Missing low surrogate.");

          list.Add(new System.Text.Rune(ri, riP1));
        }
        else if (char.IsLowSurrogate(ri))
          throw new System.InvalidOperationException("Unexpected low surrogate (missing high surrogate).");
        else
          list.Add(new System.Text.Rune(ri));
      }

      return list;
    }
  }
}
