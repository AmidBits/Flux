namespace Flux
{
  public static partial class SystemIndex
  {
    extension(System.Index source)
    {
      public System.Index GetOffsetFromEnd(int collectionLength)
        => new(collectionLength - source.GetOffset(collectionLength), true);
    }
  }
}
