namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Collections.Generic.IEnumerable<(System.DateTime begin, System.DateTime end)> GetQuarters(this System.DateTime source)
    {
      yield return (new System.DateTime(source.Year, 1, 1), new System.DateTime(source.Year, 3, 31));
      yield return (new System.DateTime(source.Year, 4, 1), new System.DateTime(source.Year, 6, 30));
      yield return (new System.DateTime(source.Year, 7, 1), new System.DateTime(source.Year, 9, 30));
      yield return (new System.DateTime(source.Year, 10, 1), new System.DateTime(source.Year, 12, 31));
    }
  }
}
