namespace Flux
{
  // <summary>
  // <para>Two-dimensional arrays are arbitrary in terms of rows and columns, we simply choose the "native storage orientation".</para>
  // <para>The native storage orientation is row-major order (see link). I.e. the array consists of elements by row then by column, as opposed to first by column then by row.</para>
  // <para>This is the order in which .NET yield elements using the IEnumerate interface of such an array.</para>
  // <see href="https://en.wikipedia.org/wiki/Row-_and_column-major_order"/>
  // </summary>
  public enum ArrayAxis
  {
    // <summary>
    // <para>The row is represented as dimension 0.</para>
    // </summary>
    // <remark>The concept of dimension 0 as the row is entirely a matter of choice, and simply a matter of adoption.</remark>
    Row = 0,
    // <summary>
    // <para>The column is represented as dimension 1.</para>
    // </summary>
    // <remark>The concept of dimension 1 as the column is entirely a matter of choice, and simply a matter of adoption.</remark>
    Column = 1
  }
}