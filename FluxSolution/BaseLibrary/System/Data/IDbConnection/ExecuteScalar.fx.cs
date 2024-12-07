namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Executes the specified command text and returns the value in the first column of the first row in the first resultset returned by the query. Additional columns, rows and resultsets are ignored.</para>
    /// <para><see cref="System.Data.IDbCommand.ExecuteScalar"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="commandText"></param>
    /// <param name="commandTimeout"></param>
    /// <returns>The value in the first column of the first row in the first resultset.</returns>
    public static object? ExecuteScalar(this System.Data.IDbConnection source, string commandText, int commandTimeout)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var command = source.CreateCommand();

      command.CommandText = commandText;
      command.CommandTimeout = commandTimeout;

      return command.ExecuteScalar();
    }

    /// <summary>
    /// <para>Tries to execute the specified command text and returns the value in the first column of the first row in the first resultset returned by the query. Additional columns, rows and resultsets are ignored.</para>
    /// <para><see cref="System.Data.IDbCommand.ExecuteScalar"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="commandText"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="result"></param>
    /// <returns>The value in the first column of the first row in the first resultset.</returns>
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
