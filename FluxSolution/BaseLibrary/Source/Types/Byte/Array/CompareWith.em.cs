namespace Flux
{
  public static partial class Xtensions
  {
    /// <summary>Returs a boolean indicating whether the byte array is equal to the specified byte array.</summary>
    public static bool CompareWith(this byte[] source, byte[] other)
      => System.Collections.StructuralComparisons.StructuralEqualityComparer.Equals(source, other);
  }
}
