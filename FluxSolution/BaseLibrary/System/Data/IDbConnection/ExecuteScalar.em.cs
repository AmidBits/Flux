namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Executes the specified command text and returns the value in the first column of the first row in the first resultset returned by the query. Additional columns, rows and resultsets are ignored.</summary>
    /// <returns>The value in the first column of the first row in the first resultset.</returns>
    /// <see cref="System.Data.IDbCommand.ExecuteScalar"/>
    public static object? ExecuteScalar(this System.Data.IDbConnection source, string commandText, int commandTimeout)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var c = source.CreateCommand();

      c.CommandText = commandText;
      c.CommandTimeout = commandTimeout;

      return c.ExecuteScalar();
    }
    public static bool TryExecuteScalar(this System.Data.IDbConnection source, string commandText, int commandTimeout, out object? result)
    {
      try
      {
        result = ExecuteScalar(source, commandText, commandTimeout);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
