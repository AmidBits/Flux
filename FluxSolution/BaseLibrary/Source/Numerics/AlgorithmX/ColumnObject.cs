namespace Flux.KnuthsAlgorithmX
{
  /// <summary>
  /// <para></para>
  /// </summary>
  internal sealed class ColumnObject
    : DataObject
  {
    private readonly int m_index;

    public int Index => m_index;
    public int Size { get; set; }

    public ColumnObject(int index)
      : base(null!)
    {
      Column = this;

      m_index = index;
    }
  }
}