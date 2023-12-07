namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Executes the specified <paramref name="commandText"/> and returns the number of rows affected.</summary>
    /// <returns>The number of rows affected.</returns>
    /// <see cref="System.Data.IDbCommand.ExecuteNonQuery"/>
    public static int ExecuteNonQuery(this System.Data.IDbConnection source, string commandText, int commandTimeout)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var c = source.CreateCommand();

      c.CommandText = commandText;
      c.CommandTimeout = commandTimeout;

      return c.ExecuteNonQuery();
    }
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
