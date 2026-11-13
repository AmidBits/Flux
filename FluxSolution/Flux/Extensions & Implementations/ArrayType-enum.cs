namespace Flux
{
  /// <summary>
  /// <para>Represents the type of an array.</para>
  /// </summary>
  public enum ArrayType
  {
    /// <summary>
    /// <para>Represents a type that is not an array.</para>
    /// </summary>
    NotAnArray,
    /// <summary>
    /// <para>Represents a one-dimensional array, e.g. <c>int[]</c>.</para>
    /// </summary>
    OneDimensionalArray,
    /// <summary>
    /// <para>Represents a two-dimensional array, e.g. <c>int[,]</c>.</para>
    /// </summary>
    TwoDimensionalArray,
    /// <summary>
    /// <para>Represents a multi-dimensional array, e.g. <c>int[,,]</c>.</para>
    /// </summary>
    MultiDimensionalArray,
    /// <summary>
    /// <para>Represents a jagged array, e.g. <c>int[][]</c>.</para>
    /// </summary>
    JaggedArray,
  }
}
