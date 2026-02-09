namespace Flux
{
  public static class IEquatableExtensions
  {
    extension<IEquatable>(IEquatable source)
      where IEquatable : System.IEquatable<IEquatable>
    {
      /// <summary>
      /// <para>Indicates whether an <see cref="System.IEquatable{T}"/> equals <see langword="default"/>.</para>
      /// </summary>
      /// <returns></returns>
      public bool EqualsDefault()
        => source.Equals(default);
    }
  }
}
