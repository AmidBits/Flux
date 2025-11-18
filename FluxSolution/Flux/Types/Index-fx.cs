namespace Flux
{
  public static partial class IndexExtensions
  {
    extension(System.Index source)
    {
      public System.Index GetOffsetFromEnd(int collectionLength)
        => new(collectionLength - source.GetOffset(collectionLength), true);
    }
  }
}
