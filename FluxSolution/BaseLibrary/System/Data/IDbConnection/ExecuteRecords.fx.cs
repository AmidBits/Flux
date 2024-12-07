namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns a sequence of <see cref="System.Data.IDataRecord"/> from all rows.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="commandText"></param>
    /// <param name="commandTimeout"></param>
    /// <returns>A sequence of <see cref="System.Data.IDataRecord"/>.</returns>
    public static System.Collections.Generic.IEnumerable<System.Data.IDataRecord> ExecuteRecords(this System.Data.IDbConnection source, string commandText, int commandTimeout)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var command = source.CreateCommand();

      command.CommandText = commandText;
      command.CommandTimeout = commandTimeout;

      using var idr = command.ExecuteReader();

      do
      {
        if (idr.Read())
        {
          //var st = idr.GetSchemaTableEx();

          do
          {
            yield return idr;
          }
          while (idr.Read());

          //st.Dispose();
        }
      }
      while (idr.NextResult());

      //do
      //{
      //  while (idr.Read())
      //    yield return idr;
      //}
      //while (idr.NextResult());
    }

    /// <summary>
    /// <para>Returns a sequence of <see cref="System.Data.IDataRecord"/> and provides a SchemaTable from each of the result sets with all records.</para>
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <param name="commandText"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="recordSelector"></param>
    /// <returns>A sequence of <typeparamref name="TResult"/>.</returns>
    public static System.Collections.Generic.IEnumerable<TResult> ExecuteRecords<TResult>(this System.Data.IDbConnection source, string commandText, int commandTimeout, System.Func<System.Data.IDataRecord, System.Data.DataTable?, TResult> recordSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(recordSelector);

      using var c = source.CreateCommand();

      c.CommandText = commandText;
      c.CommandTimeout = commandTimeout;

      using var idr = c.ExecuteReader();

      do
      {
        var dt = idr.GetSchemaTableEx();

        while (idr.Read())
          yield return recordSelector(idr, dt);

        dt.Dispose();
      }
      while (idr.NextResult());
    }
  }
}
