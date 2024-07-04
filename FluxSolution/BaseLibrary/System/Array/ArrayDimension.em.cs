namespace Flux
{
  // <summary>
  // <para>Since two dimensional arrays are arbitrary in terms of row/column orientation, we choose the native storage orientation.</para>
  // <para>This just happens to be row-major order, i.e. the array consists of elements by row then by column, as opposed to first by column then by row.</para>
  // <see href="https://en.wikipedia.org/wiki/Row-_and_column-major_order"/>
  // </summary>
  public enum ArrayDimension
  {
    // <summary>
    // <para>The row is represented as dimension 0.</para>
    // </summary>
    // <remark>The concept of dimension 0 being the row is entirely arbitrary, and simply a matter of adoption.</remark>
    Row = 0,
    // <summary>
    // <para>The column is represented as dimension 1.</para>
    // </summary>
    // <remark>The concept of dimension 1 being the column is entirely arbitrary, and simply a matter of adoption.</remark>
    Column = 1
  }
}
