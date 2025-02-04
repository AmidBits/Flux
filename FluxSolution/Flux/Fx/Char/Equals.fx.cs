namespace Flux
{
  public static partial class Fx
  {
    public static bool Equals(this char source, char target, System.StringComparison comparisonType)
      => string.Equals(source.ToString(), target.ToString(), comparisonType);
  }
}
