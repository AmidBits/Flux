namespace Flux
{
  public static class Locale
  {
    ///// <summary>
    ///// <para>Gets the file name (without extension) of the process executable from <see cref="System.AppDomain.CurrentDomain"/>[.FriendlyName].</para>
    ///// <para>Gets the file path of the process executable from one (and in order of) <see cref="System.AppDomain.CurrentDomain"/>[.RelativeSearchPath], <see cref="System.AppDomain.CurrentDomain"/>[.BaseDirectory] or <see cref="Locale.Module"/>[.FullyQualifiedName].</para>
    ///// </summary>
    //public static string AppDomainPath => System.AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory ?? typeof(Locale).Module.FullyQualifiedName;
    public static (string Name, string Path) AppDomain
    {
      get
      {
        var cd = System.AppDomain.CurrentDomain;

        return (cd.FriendlyName, cd.BaseDirectory);
      }
    }

    /// <summary>
    /// <para>The Flux assembly.</para>
    /// </summary>
    public static System.Reflection.Assembly Assembly => typeof(Locale).Assembly;

    /// <summary>
    /// <para>Gets the version of the common language runtime (CLR).</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Environment.Version"/>.</remarks>
    public static System.Version ClrVersion => System.Environment.Version;

    /// <summary>
    /// <para>Gets the fully qualified path of the current working directory.</para>
    /// </summary>
    /// <remarks>The information is from <see cref="System.Environment.CurrentDirectory"/>.</remarks>
    public static string CurrentDirectory => System.Environment.CurrentDirectory;

    /// <summary>
    /// <para>Gets the fully qualified DNS host name (including domain name, if the computer is registered in a domain) for local computer and a list of IP addresses associated with the host.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())"/>.</remarks>
    public static (string Name, System.Net.IPAddress[] Addresses) DnsHostEntry
    {
      get
      {
        var hostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

        return (hostEntry.HostName, hostEntry.AddressList);
      }
    }

    /// <summary>
    /// <para>Gets the current platform identifier and version.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Environment.OSVersion"/>.</remarks>
    public static (string PlatformID, System.Version Version, string VersionString) EnvironmentOs
      => (System.Environment.OSVersion.Platform.ToString(), System.Environment.OSVersion.Version, System.Environment.OSVersion.VersionString);

    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.IDictionary{string, string?}"/> with all environment variable keys and values from <see cref="System.Environment.GetEnvironmentVariables()"/>.</para>
    /// </summary>
    public static System.Collections.Generic.IDictionary<string, string?> EnvironmentVariables
      => Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>().ToSortedDictionary((e, i) => (string)e.Key, (e, i) => (string?)e.Value);

    private static System.Net.NetworkInformation.UnicastIPAddressInformation[] LocalIPv4UnicastAdresses()
      => System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
      .Where(ni => ni.IsOperationallyUp())
      .Select(ni => ni.GetIPProperties())
      .Where(p => p.GatewayAddresses.Any(ga => ga.Address.IsIPv4()))
      .SelectMany(ipp => ipp.UnicastAddresses)
      .Where(uai => uai.Address.IsIPv4() && !System.Net.IPAddress.IsLoopback(uai.Address))
      .ToArray();

    public static System.Net.IPAddress LessLikelyLocalIPv4Address
      => LocalIPv4UnicastAdresses().RandomOrValue(default).Item?.Address ?? default!;

    public static System.Net.IPAddress[] LocalIPv4Addresses
      => LocalIPv4UnicastAdresses()
      .Select(ua => ua.Address)
      .ToArray();

    /// <summary>
    /// <para>Gets the NETBIOS name of this local computer.</para>
    /// </summary>
    /// <remarks>This information is from <see cref="System.Environment.MachineName"/>.</remarks>
    public static string MachineName { get; } = System.Environment.MachineName;

    /// <summary>
    /// <para>The most likely local IPv4 address.</para>
    /// </summary>
    /// <remarks>This functionality is supported on "windows" platforms.</remarks>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static System.Net.IPAddress MoreLikelyLocalIPv4Address
    {
      get
      {
        System.Net.NetworkInformation.UnicastIPAddressInformation? mostLikelyIp = null;

        foreach (var ua in LocalIPv4UnicastAdresses())
        {
          if (!ua.IsDnsEligible)
          {
            if (mostLikelyIp is null)
              mostLikelyIp = ua;

            continue;
          }

          if (ua.PrefixOrigin != System.Net.NetworkInformation.PrefixOrigin.Dhcp)
          {
            if (mostLikelyIp is null || !mostLikelyIp.IsDnsEligible)
              mostLikelyIp = ua;

            continue;
          }

          return ua.Address;
        }

        return mostLikelyIp is not null ? mostLikelyIp.Address : System.Net.IPAddress.None;
      }
    }

    ///// <summary>
    ///// <para>An array of likely local IPv4 addresses.</para>
    ///// </summary>
    ///// <remarks>This functionality is unsupported on "macOS" and "OSX" platforms.</remarks>
    //[System.Runtime.Versioning.UnsupportedOSPlatform("macOS")]
    //[System.Runtime.Versioning.UnsupportedOSPlatform("OSX")]
    //public static System.Net.IPAddress[] MoreLikelyLocalIPv4Addresses
    //  => LocalIPv4UnicastAdresses()
    //  .Select(ua => ua.Address)
    //  .ToArray();

    /// <summary>
    /// <para>Gets the computer domain-name and host-name of the local computer.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()"/>.</remarks>
    public static (string DomainName, string HostName) NetworkGlobalProperties
    {
      get
      {
        var ipgp = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties();

        return (ipgp.DomainName, ipgp.HostName);
      }
    }

    /// <summary>
    /// <para>Gets the new-line strings for operating systems.</para>
    /// </summary>
    /// <remarks>The "Macintosh" new-line is for older Apple operating systems.</remarks>
    public static (char Macintosh, char Unix, string Windows) NewLines { get; } = ('\r', '\n', "\r\n");

    /// <summary>
    /// <para>Gets the "OneDrive" (personal), "OneDriveCommercial" and "OneDriveConsumer" paths for the current process.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Environment.GetEnvironmentVariable(string)"/></remarks>
    public static (string? OneDrive, string? OneDriveCommercial, string? OneDriveConsumer) OneDrivePaths
      => (System.Environment.GetEnvironmentVariable("OneDrive"), System.Environment.GetEnvironmentVariable("OneDriveCommercial"), System.Environment.GetEnvironmentVariable("OneDriveConsumer"));

    ///// <summary>
    ///// <para>Gets an array of PlatformID strings, enumerated from <see cref="System.PlatformID"/>.</para>
    ///// </summary>
    //public static string[] PlatformIDs { get; } = System.Enum.GetValues<System.PlatformID>().Select(pid => pid.ToString()).Order().ToArray();

    /// <summary>
    /// <para>Gets an array of platform strings.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.OperatingSystem"/>.</remarks>
    public static string[] Platforms { get; } = typeof(System.OperatingSystem).GetMethods().Where(mi => mi.ReturnType == typeof(bool) && mi.GetParameters().Length == 0 && mi.Name.StartsWith("Is")).Select(mi => mi.Name[2..]).Order().ToArray();

    /// <summary>
    /// <para>Gets the number of processors on which the threads in this process can be scheduled to run.</para>
    /// </summary>
    /// <remarks>
    /// <para>The information is derived from <see cref="System.Diagnostics.Process.ProcessorAffinity"/>.</para>
    /// <para>This functionality is supported on "linux" and "windows" platforms.</para>
    /// </remarks>
    [System.Runtime.Versioning.SupportedOSPlatform("linux")]
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static int ProcessorAffinityCount => System.Numerics.BitOperations.PopCount((ulong)System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity);

    /// <summary>
    /// <para>Gets the public IP address.</para>
    /// </summary>
    public static System.Net.IPAddress PublicIp
        => PublicIpAddress.TryGetIPAddress(out var ip) ? ip : System.Net.IPAddress.None;

    ///// <summary>
    ///// <para>Gets the process-architecture of the currently running process.</para>
    ///// </summary>
    ///// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture"/>.</remarks>
    //public static string ProcessArchitecture => System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString();

    /// <summary>
    /// <para>Gets the title and version of the .NET-installation on which a process is running.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription"/>.</remarks>
    public static (string Title, System.Version Version) RuntimeFramework
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim();

        var i = s.LastIndexOf(' ');

        var title = i >= -1 ? s[..i].TrimEnd() : s;

        if (!(i >= -1 && System.Version.TryParse(s[i..].TrimStart(), out var version))) version = new();

        return (title, version);
      }
    }

    /// <summary>
    /// <para>Gets the architecture, title and version of the runtime-OS on which the process is running.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSArchitecture"/> and <see cref="System.Runtime.InteropServices.RuntimeInformation.OSDescription"/>.</remarks>
    public static (string Architecture, string Title, System.Version Version) RuntimeOs
    {
      get
      {
        var architecture = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

        var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();

        var i = s.LastIndexOf(' ');

        var title = i > -1 ? s[..i].TrimEnd() : s;

        if (!(i > -1 && System.Version.TryParse(s[i..].TrimStart(), out var version))) version = new();

        return (architecture, title, version);
      }
    }

    /// <summary>
    /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, string}"/> with all environment special folders (names and paths).</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Environment.SpecialFolder"/>.</remarks>
    public static System.Collections.Generic.IDictionary<string, string> SpecialFolders
      => System.Enum.GetNames<System.Environment.SpecialFolder>().ToSortedDictionary((e, i) => e, (e, i) => System.Environment.GetFolderPath(System.Enum.Parse<System.Environment.SpecialFolder>(e)));

    /// <summary>
    /// <para>Gets the current number of ticks, and the frequency as the number of ticks per seconds, from the timer mechanism.</para>
    /// </summary>
    /// <remarks>The information is derived from <see cref="System.Diagnostics.Stopwatch"/>.</remarks>
    public static (long Counter, long Frequency) Stopwatch
      => (System.Diagnostics.Stopwatch.GetTimestamp(), System.Diagnostics.Stopwatch.Frequency);

    /// <summary>
    /// <para>Gets the system OS platform and version.</para>
    /// </summary>
    /// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
    public static (string Platform, System.Version Version) SystemOs
    {
      get
      {
        var platform = Platforms.First(System.OperatingSystem.IsOSPlatform);

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

        var version = new System.Version(major, minor, build, revision);

        return (platform, version);
      }
    }

    /// <summary>
    /// <para>Gets the path of the current user's temporary folder.</para>
    /// </summary>
    /// <remarks>The information is from <see cref="System.IO.Path.GetTempPath()"/>.</remarks>
    public static string TempPath => System.IO.Path.GetTempPath();

    /// <summary>
    /// <para>Gets the user-domain-name (associated with the current user) and user-name (associated with the current thread).</para>
    /// </summary>
    /// <remarks>The information is from <see cref="System.Environment"/>.</remarks>
    public static (string DomainName, string Name) User
      => (System.Environment.UserDomainName, System.Environment.UserName);

    /// <summary>
    /// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, object?}"/> with all <see cref="Flux.Locale"/> properties (names and values).</para>
    /// </summary>
    /// <returns></returns>
    public static System.Collections.Generic.IDictionary<string, object?> GetProperties()
      => Fx.GetPropertyInfos(typeof(Locale)).ToOrderedDictionary((e, i) => e.Name, (e, i) => e.GetValue(null));
  }
}
