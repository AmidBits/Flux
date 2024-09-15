namespace Flux
{
  public static class Locale
  {
    /// <summary>
    /// <para>Gets the file name (without extension) of the process executable from <see cref="System.AppDomain.CurrentDomain"/>[.FriendlyName].</para>
    /// </summary>
    public static string AppDomainName
      => System.AppDomain.CurrentDomain.FriendlyName;

    /// <summary>
    /// <para>Gets the file path of the process executable from one (and in order of) <see cref="System.AppDomain.CurrentDomain"/>[.RelativeSearchPath], <see cref="System.AppDomain.CurrentDomain"/>[.BaseDirectory] or <see cref="Locale.Module"/>[.FullyQualifiedName].</para>
    /// </summary>
    public static System.Uri AppDomainPath
      => new(System.AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory ?? typeof(Locale).Module.FullyQualifiedName);

    /// <summary>
    /// <para>Gets the version of the common language runtime (CLR) from <see cref="System.Environment"/>[.Version].</para>
    /// </summary>
    public static System.Version ClrVersion
      => System.Environment.Version;

    /// <summary>
    /// <para>Gets the DNS primary host name of the computer from <see cref="System.Net.Dns.GetHostEntry(string)"/>[.HostName]. Includes the fully qualified domain, if the computer is registered in a domain.</para>
    /// </summary>
    public static string ComputerDnsPrimaryHostName
      => System.Net.Dns.GetHostEntry("LocalHost").HostName;

    /// <summary>
    /// <para>Gets the identifier of the current platform extracted from <see cref="System.Environment.OSVersion"/>[.ToString()].</para>
    /// </summary>
    public static string EnvironmentOsTitle
      => System.Environment.OSVersion.ToString().Trim() is var s ? s[..s.LastIndexOf(' ')] : string.Empty;

    /// <summary>
    /// <para>Gets the version of the current platform from <see cref="System.Environment.OSVersion"/>[.Version].</para>
    /// </summary>
    public static System.Version EnvironmentOsVersion
      => System.Environment.OSVersion.Version; // System.Environment.OSVersion.ToString().Trim() is var s && System.Version.TryParse(s[s.LastIndexOf(' ')..].Trim(), out var version) ? version : throw new System.NotSupportedException();

    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.IDictionary{string, string?}"/> with all environment variable keys and values from <see cref="System.Environment.GetEnvironmentVariables()"/>.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<string, string?> EnvironmentVariables
      => System.Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>().ToSortedDictionary((e, i) => (string)e.Key, (e, i) => (string?)e.Value);

    /// <summary>
    /// <para>Gets the title of the hosting framework extracted from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>[.FrameworkDescription].</para>
    /// </summary>
    public static string FrameworkTitle
        => System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim() is var s ? s[..s.LastIndexOf(' ')] : string.Empty;

    /// <summary>
    /// <para>Gets the version of the hosting framework parsed from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>[.FrameworkDescription].</para>
    /// </summary>
    public static System.Version FrameworkVersion
      => System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim() is var s && System.Version.TryParse(s[s.LastIndexOf(' ')..].Trim(), out var version) ? version : new();

    /// <summary>
    /// <para>Gets the NETBIOS name of this local computer from <see cref="System.Environment"/>[.MachineName].</para>
    /// </summary>
    public static string MachineName
      => System.Environment.MachineName;

    /// <summary>
    /// <para>Gets the computer domain namn, or string.Empty, from <see cref="System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()"/>[.DomainName] if the computer is not registered in a domain.</para>
    /// </summary>
    public static string NetworkDomainName
      => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

    /// <summary>
    /// <para>Gets the host name of the computer from <see cref="System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()"/>[.HostName].</para>
    /// </summary>
    public static string NetworkHostName
      => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().HostName;

    /// <summary>
    /// <para>Gets the new-line character for older Macintosh operating systems.</para>
    /// </summary>
    public static char NewLineMacintosh { get; } = '\r';

    /// <summary>
    /// <para>Gets the new-line character for Unix like operating systems.</para>
    /// </summary>
    public static char NewLineUnix { get; } = '\n';

    /// <summary>
    /// <para>Gets the new-line string for Windows operating systems.</para>
    /// </summary>
    public static string NewLineWindows { get; } = "\r\n";

    /// <summary>
    /// <para>Gets the "OneDrive" (personal) path from <see cref="System.Environment.GetEnvironmentVariable(string)"/></para>
    /// </summary>
    public static string? OneDrivePath
      => System.Environment.GetEnvironmentVariable("OneDrive");

    /// <summary>
    /// <para>Gets the "OneDriveCommercial" path from <see cref="System.Environment.GetEnvironmentVariable(string)"/></para>
    /// </summary>
    public static string? OneDriveCommercialPath
      => System.Environment.GetEnvironmentVariable("OneDriveCommercial");

    /// <summary>
    /// <para>Gets the "OneDriveConsumer" path from <see cref="System.Environment.GetEnvironmentVariable(string)"/></para>
    /// </summary>
    public static string? OneDriveConsumerPath
      => System.Environment.GetEnvironmentVariable("OneDriveConsumer");

    /// <summary>
    /// <para>Gets an array of platforms that this version of .NET can interrogate, enumerated from <see cref="System.OperatingSystem"/> by reflection.</para>
    /// </summary>
    public static string[] Platforms
      => typeof(System.OperatingSystem).GetMethods().Where(mi => mi.ReturnType == typeof(bool) && mi.GetParameters().Length == 0 && mi.Name.StartsWith("Is")).Select(mi => mi.Name[2..]).ToArray();

    /// <summary>
    /// <para>Gets the process architecture of the currently running process.</para>
    /// </summary>
    public static string ProcessArchitecture
      => System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString();

    /// <summary>
    /// <para>Gets the descriptive text of the platform architecture on which the current process is running extracted from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSArchitecture"/>[.ToString()].</para>
    /// </summary>
    public static string RuntimeOsArchitecture
      => System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

    /// <summary>
    /// <para>Gets the title of the hosting operating system extracted from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>[.OSDescription].</para>
    /// </summary>
    public static string RuntimeOsTitle
      => System.Runtime.InteropServices.RuntimeInformation.OSDescription is var s ? s[..s.Trim().LastIndexOf(' ')] : string.Empty;

    /// <summary>
    /// <para>Gets the version of the hosting operating system parsed from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>[.OSDescription].</para>
    /// </summary>
    public static System.Version RuntimeOsVersion
      => System.Runtime.InteropServices.RuntimeInformation.OSDescription is var s && System.Version.TryParse(s[s.Trim().LastIndexOf(' ')..], out var version) ? version : throw new System.NotSupportedException();

    /// <summary>
    /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, string}"/> with all special folders (names and paths) using <see cref="System.Environment.GetFolderPath(Environment.SpecialFolder)"/>.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<string, string> SpecialFolders
      => System.Enum.GetNames<System.Environment.SpecialFolder>().ToSortedDictionary((e, i) => e, (e, i) => System.Environment.GetFolderPath(System.Enum.Parse<System.Environment.SpecialFolder>(e)));

    /// <summary>
    /// <para>Gets the interrogated operating system platform found in <see cref="System.OperatingSystem"/>. If no title can be determined, an empty string is returned.</para>
    /// </summary>
    public static string SystemOsTitle
    {
      get
      {
        for (var index = Platforms.Length - 1; index >= 0; index--)
          if (Platforms[index] is var platform && System.OperatingSystem.IsOSPlatform(platform))
            return platform;

        return string.Empty;
      }
    }

    /// <summary>
    /// <para>Gets a computed operating system platform version found in <see cref="System.OperatingSystem"/>. If no version can be determined, 0.0.0.0 is returned.</para>
    /// </summary>
    public static System.Version SystemOsVersion
    {
      get
      {
        var platform = SystemOsTitle;

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
    /// <para>Gets the path of the current user's temporary folder using <see cref="System.IO.Path.GetTempPath()"/>.</para>
    /// </summary>
    public static string TempPath
      => System.IO.Path.GetTempPath();

    /// <summary>
    /// <para>Gets the current number of ticks in the timer mechanism from <see cref="System.Diagnostics.Stopwatch.GetTimestamp()"/>.</para>
    /// </summary>
    public static long TimerTickCounter
      => System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    /// <para>Gets the number of ticks per seconds of the timer mechanism from <see cref="System.Diagnostics.Stopwatch"/>[.Frequency].</para>
    /// </summary>
    public static long TimerTickResolution
      => System.Diagnostics.Stopwatch.Frequency;

    /// <summary>
    /// <para>Gets the user domain name from <see cref="System.Environment"/>[.UserDomainName].</para>
    /// </summary>
    public static string UserDomainName
      => System.Environment.UserDomainName;

    /// <summary>
    /// <para>Gets the user name from <see cref="System.Environment"/>[.UserName].</para>
    /// </summary>
    public static string UserName
      => System.Environment.UserName;

    /// <summary>
    /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, object?}"/> with all <see cref="Flux.Locale"/> properties (names and values).</para>
    /// </summary>
    /// <returns></returns>
    public static System.Collections.Generic.IDictionary<string, object?> GetProperties()
      => Fx.GetPropertyInfos(typeof(Locale)).ToOrderedDictionary((e, i) => e.Name, (e, i) => e.GetValue(null));
  }
}
