namespace Flux
{
  public static partial class Fx
  {
    public static T Clamp<T>(this T source, T min, T max)
      where T : System.IComparable<T>
      => source.CompareTo(min) < 0 ? min : source.CompareTo(max) > 0 ? max : source;
  }
}
