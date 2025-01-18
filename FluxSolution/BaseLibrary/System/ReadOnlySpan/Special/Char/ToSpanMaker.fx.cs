namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the {<paramref name="prepend"/> + <paramref name="source"/>} as content.</summary>
    public static SpanMaker<char> ToSpanMaker(this System.ReadOnlySpan<char> source, string? prepend = null)
    {
      var sm = new SpanMaker<char>(prepend);
      sm = sm.Append(source);
      return sm;
    }
  }
}
