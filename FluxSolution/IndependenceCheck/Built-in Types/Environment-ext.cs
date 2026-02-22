namespace Flux
{
  public static partial class EnvironmentExtensions
  {
    extension(System.Environment)
    {
      /// <summary>
      /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, string}"/> with all environment special folders (names and paths).</para>
      /// </summary>
      /// <remarks>The information is derived from <see cref="System.Environment.SpecialFolder"/>.</remarks>
      public static System.Collections.Generic.IDictionary<string, System.IO.DirectoryInfo?> SpecialFolders
        => System.Enum.GetValues<System.Environment.SpecialFolder>().ToSortedDictionary(e => e.ToString(), GetDirectoryInfo);

      ///// <summary>
      ///// <para>A dictionary of all environment variable names and their values associated with the current machine.</para>
      ///// </summary>
      //public static System.Collections.Generic.IDictionary<string, string> VariablesOfMachine
      //  => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary(e => $"{e.Key}", e => $"{e.Value}");

      ///// <summary>
      ///// <para>A dictionary of all environment variable names and their values associated with the current process.</para>
      ///// </summary>
      //public static System.Collections.Generic.IDictionary<string, string> VariablesOfProcess
      //  => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary(e => $"{e.Key}", e => $"{e.Value}");

      ///// <summary>
      ///// <para>A dictionary of all environment variable names and their values associated with the current user.</para>
      ///// </summary>
      //public static System.Collections.Generic.IDictionary<string, string> VariablesOfUser
      //  => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary(e => $"{e.Key}", e => $"{e.Value}");

      /// <summary>
      /// <para>Returns a <see cref="System.IO.DirectoryInfo"/> object for the specified <see cref="System.Environment.SpecialFolder"/>.</para>
      /// </summary>
      public static System.IO.DirectoryInfo? GetDirectoryInfo(System.Environment.SpecialFolder specialFolder)
        => System.Environment.GetFolderPath(specialFolder) is var fp && string.IsNullOrEmpty(fp)
        ? default
        : new System.IO.DirectoryInfo(fp);
    }
  }
}
