namespace Flux
{
  /// <summary>
  /// <para>Represents the logical axes of a two-dimensional array.</para>
  /// <para>By convention, dimension 0 represents rows and dimension 1 represents columns.</para>
  /// <see href="https://en.wikipedia.org/wiki/Row-_and_column-major_order"/>
  /// </summary>
  /// <remarks>In .NET, a rectangular array (<see cref="System.Array"/>) is stored and enumerated in row-major order: all elements of a row are traversed before moving to the next row.</remarks>
  public enum ArrayAxis
  {
    /// <summary>
    /// <para>The row axis (dimension 0).</para>
    /// </summary>
    /// <remarks>
    /// <para>Indexes position along rows. In a two-dimensional array, the first dimension corresponds to the row coordinate.</para>
    /// </remarks>
    Row = 0,
    /// <summary>
    /// <para>The column axis (dimension 1).</para>
    /// </summary>
    /// <remarks>
    /// <para>Indexes position along columns. In a two-dimensional array, the second dimension corresponds to the column coordinate.</para>
    /// </remarks>
    Column = 1
  }
}