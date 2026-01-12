/*
		System.Numerics.BigInteger n = 10;
		System.Numerics.BigInteger r = 3;

		System.Console.WriteLine($"Permutations with repetitions: {System.Numerics.BigInteger.Pow(n, (int)r)}");

		n = 16;
		r = 3;
		
		System.Console.WriteLine($"Permutations without repetitions: {Factorial(n) / Factorial(n - r)}");
		
		n = 16;
		r = 3;
		
		System.Console.WriteLine($"Combinations without repetitions: {BinomialCoefficient(n, r)}");
		
		n = 5;
		r = 3;
		
		System.Console.WriteLine($"Combinations with repetitions: {Factorial(r + n - 1) / (Factorial(r) * Factorial(n - 1))}");
*/
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
      public static System.Collections.Generic.IDictionary<string, string> SpecialFolders
        => System.Enum.GetValues<System.Environment.SpecialFolder>().ToSortedDictionary(e => e.ToString(), System.Environment.GetFolderPath);

      /// <summary>
      /// <para>A dictionary of all environment variable names and their values associated with the current machine.</para>
      /// </summary>
      public static System.Collections.Generic.IDictionary<string, string> VariablesOfMachine
        => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary(e => $"{e.Key}", e => $"{e.Value}");

      /// <summary>
      /// <para>A dictionary of all environment variable names and their values associated with the current process.</para>
      /// </summary>
      public static System.Collections.Generic.IDictionary<string, string> VariablesOfProcess
        => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary(e => $"{e.Key}", e => $"{e.Value}");

      /// <summary>
      /// <para>A dictionary of all environment variable names and their values associated with the current user.</para>
      /// </summary>
      public static System.Collections.Generic.IDictionary<string, string> VariablesOfUser
        => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary(e => $"{e.Key}", e => $"{e.Value}");

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
