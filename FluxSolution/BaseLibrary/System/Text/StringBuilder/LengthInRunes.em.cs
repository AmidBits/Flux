namespace Flux
{
  public static partial class Fx
  {
    public static int LengthInRunes(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.EnumerateRunes().Count();
    }
  }
}
