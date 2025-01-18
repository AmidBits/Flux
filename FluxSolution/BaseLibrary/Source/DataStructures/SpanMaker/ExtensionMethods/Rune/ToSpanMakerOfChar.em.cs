namespace Flux
{
  public static partial class Fx
  {
    public static SpanMaker<char> ToSpanMakerOfChar(this SpanMaker<System.Text.Rune> source)
    {
      var sm = new SpanMaker<char>();
      for (var i = 0; i < source.Length; i++)
        sm = sm.Append(source[i].ToString());
      return sm;
    }
  }
}
