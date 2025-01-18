namespace Flux
{
  public static partial class Fx
  {
    public static SpanMaker<System.Text.Rune> ToSpanMakerOfRune(this SpanMaker<char> source)
    {
      var sm = new SpanMaker<System.Text.Rune>();

      for (var i = 0; i < source.Length; i++)
      {
        var ri = source[i];

        if (char.IsHighSurrogate(ri))
        {
          if (++i >= source.Length - 1)
            throw new System.InvalidOperationException("Unexpected end of sequence after high surrogate.");

          if (source[i] is var riP1 && !char.IsLowSurrogate(riP1))
            throw new System.InvalidOperationException("Missing low surrogate after high surrogate.");

          sm.Append(new System.Text.Rune(ri, riP1));
        }
        else if (char.IsLowSurrogate(ri))
          throw new System.InvalidOperationException("Unexpected low surrogate without high surrogate.");
        else
          sm.Append(new System.Text.Rune(ri));
      }

      return sm;
    }
  }
}
