namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Executes the specified <paramref name="commandText"/> and returns the number of rows affected.</para>
    /// <para><see cref="System.Data.IDbCommand.ExecuteNonQuery"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="commandText"></param>
    /// <param name="commandTimeout"></param>
    /// <returns>The number of rows affected.</returns>
    public static int ExecuteNonQuery(this System.Data.IDbConnection source, string commandText, int commandTimeout)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var command = source.CreateCommand();

      command.CommandText = commandText;
      command.CommandTimeout = commandTimeout;

      return command.ExecuteNonQuery();
    }

    /// <summary>
    /// <para>Executes the specified <paramref name="commandText"/> and returns the number of rows affected.</para>
    /// <para><see cref="System.Data.IDbCommand.ExecuteNonQuery"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="commandText"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="result"></param>
    /// <returns>The number of rows affected.</returns>
    public static bool TryExecuteNonQuery(this System.Data.IDbConnection source, string commandText, int commandTimeout, out int result)
    {
      try
      {
        result = ExecuteNonQuery(source, commandText, commandTimeout);
        return true;
      }
      catch { }

      result = default;
      return false;
    }
  }
}
