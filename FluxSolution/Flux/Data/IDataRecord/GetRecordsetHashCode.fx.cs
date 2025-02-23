namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int GetRecordSetHash(this System.Data.IDataRecord source)
      => System.HashCode.Combine(source.GetNames().SequenceHashCode(), source.GetFieldTypes().SequenceHashCode());

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int GetRecordHash(this System.Data.IDataRecord source)
      => System.HashCode.Combine(source.GetRecordSetHash(), source.GetValues().SequenceHashCode());
  }
}
