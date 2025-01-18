namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="SpanMaker{T}"/> with the <paramref name="source"/> as content.</summary>
    public static SpanMaker<char> ToSpanMaker(this string source, int additionalCapacity = 0)
    {
      var sm = new SpanMaker<char>(source.Length + additionalCapacity);
      sm = sm.Append(source);
      return sm;
    }
  }
}
