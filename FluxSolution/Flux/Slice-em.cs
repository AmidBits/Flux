namespace Flux
{
  public static partial class Em
  {
    public static Slice ToSlice(this System.Range range, int collectionLength) => new(range, collectionLength);
  }
}
