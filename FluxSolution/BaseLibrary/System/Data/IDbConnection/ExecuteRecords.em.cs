namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns a sequence of <see cref="System.Data.IDataRecord"/> from all rows.</summary>
    /// <returns>A sequence of <see cref="System.Data.IDataRecord"/>.</returns>
    public static System.Collections.Generic.IEnumerable<System.Data.IDataRecord> ExecuteRecords(this System.Data.IDbConnection source, string commandText, int commandTimeout)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      using var c = source.CreateCommand();

      c.CommandText = commandText;
      c.CommandTimeout = commandTimeout;

      using var idr = c.ExecuteReader();

      do
      {
        while (idr.Read())
          yield return idr;
      }
      while (idr.NextResult());
    }

    /// <summary>Returns a sequence of <see cref="System.Data.IDataRecord"/> and provides a SchemaTable from each of the result sets with all records.</summary>
    /// <returns>A sequence of <typeparamref name="TResult"/>.</returns>
    public static System.Collections.Generic.IEnumerable<TResult> ExecuteRecords<TResult>(this System.Data.IDbConnection source, string commandText, int commandTimeout, System.Func<System.Data.IDataRecord, System.Data.DataTable?, TResult> recordSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (recordSelector is null) throw new System.ArgumentNullException(nameof(recordSelector));

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
