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
    /// <summary>
    /// <para>Obtains the global IP address of the system.</para>
    /// <para>A global IP address is the address that the system has on the outside of the local area network.</para>
    /// </summary>
    public static System.Net.IPAddress GlobalAddress
      => System.Net.IPAddress.TryGetMyGlobalAddress(out var myGlobalAddress) ? myGlobalAddress : myGlobalAddress;

    /// <summary>
    /// <para>Gets the local IP address of the system.</para>
    /// <para>A local IP address is </para>
    /// </summary>
    public static System.Net.IPAddress LocalAddress
      => System.Net.IPAddress.TryGetMyLocalAddress(out var myLocalAddress) ? myLocalAddress : myLocalAddress;

    /// <summary>
    /// <para>Gets all local IP addresses of the system.</para>
    /// </summary>
    public static System.Net.IPAddress[] LocalAddresses
      => System.Net.IPAddress.TryGetMyLocalAddressesViaNics(out var myLocalAddressesViaNics) ? myLocalAddressesViaNics : myLocalAddressesViaNics;

    /// <summary>
    /// <para>Gets an array of PlatformID strings, enumerated from <see cref="System.PlatformID"/>.</para>
    /// </summary>
    public static System.PlatformID[] PlatformIDs
      => [.. System.Enum.GetValues<System.PlatformID>().OrderBy(pid => pid.ToString())];

    /// <summary>
    /// <para>Gets an array of platform strings.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.OperatingSystem"/>.</remarks>
    public static string[] Platforms
      => [.. typeof(System.OperatingSystem).GetMethods().Where(mi => mi.ReturnType == typeof(bool) && mi.GetParameters().Length == 0 && mi.Name.StartsWith("Is")).Select(mi => mi.Name.Substring(2)).Order()];

    /// <summary>
    /// <para>Gets the title and version of the .NET-installation on which a process is running.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription"/>.</remarks>
    public static string RuntimeFrameworkTitle
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim();

        var i = s.LastIndexOf(' ');

        return i >= -1 ? s[..i].TrimEnd() : s;
      }
    }

    /// <summary>
    /// <para>Gets the title and version of the .NET-installation on which a process is running.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription"/>.</remarks>
    public static System.Version RuntimeFrameworkVersion
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim();

        var i = s.LastIndexOf(' ');

        if (!(i >= -1 && System.Version.TryParse(s[i..].TrimStart(), out var version))) version = new();

        return version;
      }
    }

    /// <summary>
    /// <para>Gets the title of the runtime-OS on which the process is running.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSDescription"/>.</remarks>
    public static string RuntimeOsTitle
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();

        var i = s.LastIndexOf(' ');

        return i > -1 ? s[..i].TrimEnd() : s;
      }
    }

    /// <summary>
    /// <para>Gets the version of the runtime-OS on which the process is running.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSDescription"/>.</remarks>
    public static System.Version RuntimeOsVersion
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();

        var i = s.LastIndexOf(' ');

        if (!(i > -1 && System.Version.TryParse(s[i..].TrimStart(), out var version))) version = new();

        return version;
      }
    }

    /// <summary>
    /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, string}"/> with all environment special folders (names and paths).</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Environment.SpecialFolder"/>.</remarks>
    public static System.Collections.Generic.IDictionary<string, string> SpecialFolders
      => System.Enum.GetNames<System.Environment.SpecialFolder>().ToSortedDictionary((e, i) => e, (e, i) => System.Environment.GetFolderPath(System.Enum.Parse<System.Environment.SpecialFolder>(e)));

    /// <summary>
    /// <para>Gets the system OS platform.</para>
    /// </summary>
    /// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
    public static string SystemOsPlatform
      => Platforms.First(System.OperatingSystem.IsOSPlatform);

    /// <summary>
    /// <para>Gets the system OS version.</para>
    /// </summary>
    /// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
    public static System.Version SystemOsVersion
    {
      get
      {
        var platform = SystemOsPlatform;

        var major = 0;
        while (major < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major + 1))
          major++;

        var minor = 0;
        while (minor < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor + 1))
          minor++;

        var build = 0;
        while (build < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor, build + 1))
          build++;

        var revision = 0;
        while (revision < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor, build, revision + 1))
          revision++;

        return new System.Version(major, minor, build, revision);
      }
    }

    /// <summary>
    /// <para>A dictionary of all environment variable names and their values associated with the current machine.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<string, string> VariablesOfMachine
      => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Machine).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary((e, i) => $"{e.Key}", (e, i) => $"{e.Value}");

    /// <summary>
    /// <para>A dictionary of all environment variable names and their values associated with the current process.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<string, string> VariablesOfProcess
      => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.Process).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary((e, i) => $"{e.Key}", (e, i) => $"{e.Value}");

    /// <summary>
    /// <para>A dictionary of all environment variable names and their values associated with the current user.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<string, string> VariablesOfUser
      => System.Environment.GetEnvironmentVariables(EnvironmentVariableTarget.User).Cast<System.Collections.DictionaryEntry>().ToSortedDictionary((e, i) => $"{e.Key}", (e, i) => $"{e.Value}");

    /// <summary>
    /// <para>Returns a <see cref="System.IO.DirectoryInfo"/> object for the specified <see cref="System.Environment.SpecialFolder"/>.</para>
    /// </summary>
    public static System.IO.DirectoryInfo? GetDirectoryInfo(this System.Environment.SpecialFolder specialFolder)
      => System.Environment.GetFolderPath(specialFolder) is var fp && string.IsNullOrEmpty(fp)
      ? default
      : new System.IO.DirectoryInfo(fp);

    /// <summary>
    /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, object?}"/> with all <see cref="Flux.Locale"/> properties (names and values).</para>
    /// </summary>
    /// <returns></returns>
    public static System.Collections.Generic.IDictionary<string, object?> GetProperties()
      => typeof(EnvironmentExtensions).GetMemberDictionary(null).ToOrderedDictionary((kvp, i) => kvp.Key.Name, (kvp, i) => kvp.Value);
  }
}
