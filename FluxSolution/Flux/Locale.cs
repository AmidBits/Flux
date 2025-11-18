///*
//		System.Numerics.BigInteger n = 10;
//		System.Numerics.BigInteger r = 3;

//		System.Console.WriteLine($"Permutations with repetitions: {System.Numerics.BigInteger.Pow(n, (int)r)}");

//		n = 16;
//		r = 3;

//		System.Console.WriteLine($"Permutations without repetitions: {Factorial(n) / Factorial(n - r)}");

//		n = 16;
//		r = 3;

//		System.Console.WriteLine($"Combinations without repetitions: {BinomialCoefficient(n, r)}");

//		n = 5;
//		r = 3;

//		System.Console.WriteLine($"Combinations with repetitions: {Factorial(r + n - 1) / (Factorial(r) * Factorial(n - 1))}");
//*/
//namespace Flux
//{
//  public static class Locale
//  {
//    ////public static System.Data.DataTable GetLocaleData()
//    ////{
//    ////  var dt = new System.Data.DataTable("Locale");

//    ////  dt.Columns.Add("PropertyGroup", typeof(string));
//    ////  dt.Columns.Add("PropertyName", typeof(string));
//    ////  dt.Columns.Add("PropertyValue", typeof(object));

//    ////  dt.Rows.Add(["Environment", "ClrVersion", System.Environment.Version]);
//    ////  dt.Rows.Add(["Environment", "CurrentDirectory", System.Environment.CurrentDirectory]);
//    ////  dt.Rows.Add(["Environment", "MachineName", System.Environment.MachineName]);

//    ////  foreach (var ev in System.Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>())
//    ////    dt.Rows.Add(["EnvironmentVariable", (string)ev.Key, (string?)ev.Value]);

//    ////  foreach (var sf in System.Enum.GetNames<System.Environment.SpecialFolder>())
//    ////    dt.Rows.Add(["SpecialFolder", sf, System.Environment.GetFolderPath(System.Enum.Parse<System.Environment.SpecialFolder>(sf))]);

//    ////  return dt;
//    ////}

//    ///////// <summary>
//    ///////// <para>Gets the file name (without extension) of the process executable from <see cref="System.AppDomain.CurrentDomain"/>[.FriendlyName].</para>
//    ///////// <para>Gets the file path of the process executable from one (and in order of) <see cref="System.AppDomain.CurrentDomain"/>[.RelativeSearchPath], <see cref="System.AppDomain.CurrentDomain"/>[.BaseDirectory] or <see cref="Locale.Module"/>[.FullyQualifiedName].</para>
//    ///////// </summary>
//    //////public static string AppDomainPath => System.AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory ?? typeof(Locale).Module.FullyQualifiedName;
//    ////public static string AppDomainName
//    ////  => System.AppDomain.CurrentDomain.FriendlyName;

//    ///////// <summary>
//    ///////// <para>Gets the file path of the process executable from one (and in order of) <see cref="System.AppDomain.CurrentDomain"/>[.RelativeSearchPath], <see cref="System.AppDomain.CurrentDomain"/>[.BaseDirectory] or <see cref="Locale.Module"/>[.FullyQualifiedName].</para>
//    ///////// </summary>
//    //////public static string AppDomainPath => System.AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory ?? typeof(Locale).Module.FullyQualifiedName;
//    ////public static System.IO.DirectoryInfo AppDomainPath
//    ////  => new System.IO.DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);

//    ///// <summary>
//    ///// <para>The Flux assembly.</para>
//    ///// </summary>
//    //public static System.Reflection.Assembly Assembly
//    //  => typeof(Locale).Assembly;

//    /////// <summary>
//    /////// <para>Gets the version of the common language runtime (CLR).</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Environment.Version"/>.</remarks>
//    ////public static System.Version ClrVersion
//    ////  => System.Environment.Version;

//    /////// <summary>
//    /////// <para>Gets the fully qualified path of the current working directory.</para>
//    /////// </summary>
//    /////// <remarks>The information is from <see cref="System.Environment.CurrentDirectory"/>.</remarks>
//    ////public static System.IO.DirectoryInfo CurrentDirectory
//    ////  => new(System.Environment.CurrentDirectory);

//    /////// <summary>
//    /////// <para>Gets the fully qualified DNS host name (including domain name, if the computer is registered in a domain) for local computer and a list of IP addresses associated with the host.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())"/>.</remarks>
//    ////public static System.Net.IPAddress[] DnsHostAddresses
//    ////  => System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList;

//    /////// <summary>
//    /////// <para>Gets a list of IP addresses associated with the host.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName())"/>.</remarks>
//    ////public static string DnsHostName
//    ////  => System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).HostName;

//    /////// <summary>
//    /////// <para>Gets the current platform identifier.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Environment.OSVersion"/>.</remarks>
//    ////public static System.PlatformID EnvironmentOsPlatform
//    ////  => System.Environment.OSVersion.Platform;

//    /////// <summary>
//    /////// <para>Gets the current platform version.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Environment.OSVersion"/>.</remarks>
//    ////public static System.Version EnvironmentOsVersion
//    ////  => System.Environment.OSVersion.Version;

//    ///// <summary>
//    ///// <para>Creates a new <see cref="System.Collections.Generic.IDictionary{string, string?}"/> with all environment variable keys and values from <see cref="System.Environment.GetEnvironmentVariables()"/>.</para>
//    ///// </summary>
//    //public static System.Collections.Generic.IDictionary<string, string?> EnvironmentVariables
//    //  => System.Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>().ToSortedDictionary((e, i) => (string)e.Key, (e, i) => (string?)e.Value);

//    ///// <summary>
//    ///// <para>Obtains the global IP address of the system.</para>
//    ///// <para>A global IP address is the address that the system has on the outside of the local area network.</para>
//    ///// </summary>
//    //public static System.Net.IPAddress GlobalAddress
//    //  => Net.IPAddresses.TryGetMyGlobalAddress(out var myGlobalAddress) ? myGlobalAddress : myGlobalAddress;

//    ///// <summary>
//    ///// <para>Gets the local IP address of the system.</para>
//    ///// <para>A local IP address is </para>
//    ///// </summary>
//    //public static System.Net.IPAddress LocalAddress
//    //  => Net.IPAddresses.TryGetMyLocalAddress(out var myLocalAddress) ? myLocalAddress : myLocalAddress;

//    ///// <summary>
//    ///// <para>Gets all local IP addresses of the system.</para>
//    ///// </summary>
//    //public static System.Net.IPAddress[] LocalAddresses
//    //  => Net.IPAddresses.TryGetMyLocalAddressesViaNics(out var myLocalAddressesViaNics) ? myLocalAddressesViaNics : myLocalAddressesViaNics;

//    /////// <summary>
//    /////// <para>Gets the NETBIOS name of this local computer.</para>
//    /////// </summary>
//    /////// <remarks>This information is from <see cref="System.Environment.MachineName"/>.</remarks>
//    ////public static string MachineName { get; } = System.Environment.MachineName;

//    /////// <summary>
//    /////// <para>The most likely local IPv4 address.</para>
//    /////// </summary>
//    /////// <remarks>This functionality is supported on "windows" platforms.</remarks>
//    ////[System.Runtime.Versioning.SupportedOSPlatform("windows")]
//    ////public static System.Net.IPAddress MoreLikelyLocalIPv4Address
//    ////  => MyIpAddresses.TryGetMyLocalAddressViaNics(out var myMoreLikelyLocalAddressViaNics) ? myMoreLikelyLocalAddressViaNics : myMoreLikelyLocalAddressViaNics;
//    ////    {
//    ////      get
//    ////      {
//    ////        System.Net.NetworkInformation.UnicastIPAddressInformation? mostLikelyIp = null;

//    ////        foreach (var ua in LocalIPv4UnicastAdresses())
//    ////        {
//    ////          if (!ua.IsDnsEligible)
//    ////          {
//    ////            if (mostLikelyIp is null)
//    ////              mostLikelyIp = ua;

//    ////            continue;
//    ////          }

//    ////          if (ua.PrefixOrigin != System.Net.NetworkInformation.PrefixOrigin.Dhcp)
//    ////          {
//    ////            if (mostLikelyIp is null || !mostLikelyIp.IsDnsEligible)
//    ////              mostLikelyIp = ua;

//    ////            continue;
//    ////          }

//    ////return ua.Address;
//    ////        }

//    ////        return mostLikelyIp is not null ? mostLikelyIp.Address : System.Net.IPAddress.None;
//    ////      }
//    ////    }

//    /////// <summary>
//    /////// <para>An array of likely local IPv4 addresses.</para>
//    /////// </summary>
//    /////// <remarks>This functionality is unsupported on "macOS" and "OSX" platforms.</remarks>
//    ////[System.Runtime.Versioning.UnsupportedOSPlatform("macOS")]
//    ////[System.Runtime.Versioning.UnsupportedOSPlatform("OSX")]
//    ////public static System.Net.IPAddress[] MoreLikelyLocalIPv4Addresses
//    ////  => LocalIPv4UnicastAdresses()
//    ////  .Select(ua => ua.Address)
//    ////  .ToArray();

//    /////// <summary>
//    /////// <para>Gets the computer domain-name of the local computer.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()"/>.</remarks>
//    ////public static string NicDomainName
//    ////  => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

//    /////// <summary>
//    /////// <para>Gets the computer host-name of the local computer.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()"/>.</remarks>
//    ////public static string NicHostName
//    ////  => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().HostName;

//    /////// <summary>
//    /////// <para>Gets the new-line for the older Macintosh operating system.</para>
//    /////// </summary>
//    /////// <remarks>The "Macintosh" new-line is for older Apple operating systems.</remarks>
//    ////public static char NewLineMacintosh
//    ////  => '\r';

//    /////// <summary>
//    /////// <para>Gets the new-line for the Unix operating system.</para>
//    /////// </summary>
//    ////public static char NewLineUnix
//    ////  => '\n';

//    /////// <summary>
//    /////// <para>Gets the new-line for the Windows operating system.</para>
//    /////// </summary>
//    ////public static string NewLineWindows
//    ////  => "\r\n";

//    /////// <summary>
//    /////// <para>Gets the "OneDrive" (personal) path for the current process.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Environment.GetEnvironmentVariable(string)"/></remarks>
//    ////public static System.IO.DirectoryInfo OneDrivePath { get; }
//    ////  = new System.IO.DirectoryInfo(System.Environment.GetEnvironmentVariable("OneDrive")!);

//    /////// <summary>
//    /////// <para>Gets the "OneDriveCommercial" path for the current process.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Environment.GetEnvironmentVariable(string)"/></remarks>
//    ////public static System.IO.DirectoryInfo OneDrivePathCommercial
//    ////  => new System.IO.DirectoryInfo(System.Environment.GetEnvironmentVariable("OneDriveCommercial")!);

//    /////// <summary>
//    /////// <para>Gets the "OneDriveConsumer" path for the current process.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Environment.GetEnvironmentVariable(string)"/></remarks>
//    ////public static System.IO.DirectoryInfo OneDrivePathConsumer
//    ////  => new System.IO.DirectoryInfo(System.Environment.GetEnvironmentVariable("OneDriveConsumer")!);

//    ///// <summary>
//    ///// <para>Gets an array of PlatformID strings, enumerated from <see cref="System.PlatformID"/>.</para>
//    ///// </summary>
//    //public static System.PlatformID[] PlatformIDs
//    //  => [.. System.Enum.GetValues<System.PlatformID>().OrderBy(pid => pid.ToString())];

//    ///// <summary>
//    ///// <para>Gets an array of platform strings.</para>
//    ///// </summary>
//    ///// <remarks>The information is derived from <see cref="System.OperatingSystem"/>.</remarks>
//    //public static string[] Platforms
//    //  => [.. typeof(System.OperatingSystem).GetMethods().Where(mi => mi.ReturnType == typeof(bool) && mi.GetParameters().Length == 0 && mi.Name.StartsWith("Is")).Select(mi => mi.Name[2..]).Order()];

//    /////// <summary>
//    /////// <para>Gets the number of processors on which the threads in this process can be scheduled to run.</para>
//    /////// </summary>
//    /////// <remarks>
//    /////// <para>The information is derived from <see cref="System.Diagnostics.Process.ProcessorAffinity"/>.</para>
//    /////// <para>This functionality is supported on "linux" and "windows" platforms.</para>
//    /////// </remarks>
//    ////[System.Runtime.Versioning.SupportedOSPlatform("linux")]
//    ////[System.Runtime.Versioning.SupportedOSPlatform("windows")]
//    ////public static int ProcessorAffinityCount
//    ////  => System.Numerics.BitOperations.PopCount((ulong)System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity);

//    /////// <summary>
//    /////// <para>Gets the public IP address.</para>
//    /////// </summary>
//    ////public static System.Net.IPAddress PublicIp
//    ////  => Net.MyIpAddresses.TryGetMyGlobalAddressesViaHosts(out var ipas) ? ipas.SingleOrDefault(e => e.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) : System.Net.IPAddress.None;

//    /////// <summary>
//    /////// <para>Gets the process-architecture of the currently running process.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture"/>.</remarks>
//    ////public static string ProcessArchitecture => System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture.ToString();

//    ///// <summary>
//    ///// <para>Gets the title and version of the .NET-installation on which a process is running.</para>
//    ///// </summary>
//    ///// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription"/>.</remarks>
//    //public static string RuntimeFrameworkTitle
//    //{
//    //  get
//    //  {
//    //    var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim();

//    //    var i = s.LastIndexOf(' ');

//    //    return i >= -1 ? s[..i].TrimEnd() : s;
//    //  }
//    //}

//    ///// <summary>
//    ///// <para>Gets the title and version of the .NET-installation on which a process is running.</para>
//    ///// </summary>
//    ///// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription"/>.</remarks>
//    //public static System.Version RuntimeFrameworkVersion
//    //{
//    //  get
//    //  {
//    //    var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim();

//    //    var i = s.LastIndexOf(' ');

//    //    if (!(i >= -1 && System.Version.TryParse(s[i..].TrimStart(), out var version))) version = new();

//    //    return version;
//    //  }
//    //}

//    /////// <summary>
//    /////// <para>Gets the architecture on which the process is running.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSArchitecture"/>.</remarks>
//    ////public static string RuntimeOsArchitecture
//    ////  => System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

//    ///// <summary>
//    ///// <para>Gets the title of the runtime-OS on which the process is running.</para>
//    ///// </summary>
//    ///// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSDescription"/>.</remarks>
//    //public static string RuntimeOsTitle
//    //{
//    //  get
//    //  {
//    //    var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();

//    //    var i = s.LastIndexOf(' ');

//    //    return i > -1 ? s[..i].TrimEnd() : s;
//    //  }
//    //}

//    ///// <summary>
//    ///// <para>Gets the version of the runtime-OS on which the process is running.</para>
//    ///// </summary>
//    ///// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSDescription"/>.</remarks>
//    //public static System.Version RuntimeOsVersion
//    //{
//    //  get
//    //  {
//    //    var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();

//    //    var i = s.LastIndexOf(' ');

//    //    if (!(i > -1 && System.Version.TryParse(s[i..].TrimStart(), out var version))) version = new();

//    //    return version;
//    //  }
//    //}

//    ///// <summary>
//    ///// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, string}"/> with all environment special folders (names and paths).</para>
//    ///// </summary>
//    ///// <remarks>The information is derived from <see cref="System.Environment.SpecialFolder"/>.</remarks>
//    //public static System.Collections.Generic.IDictionary<string, string> SpecialFolders //{ get; }
//    //  => System.Enum.GetNames<System.Environment.SpecialFolder>().ToSortedDictionary((e, i) => e, (e, i) => System.Environment.GetFolderPath(System.Enum.Parse<System.Environment.SpecialFolder>(e)));

//    /////// <summary>
//    /////// <para>Gets the current number of ticks in the timer mechanism.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Diagnostics.Stopwatch"/>.</remarks>
//    ////public static long StopwatchCounter
//    ////  => System.Diagnostics.Stopwatch.GetTimestamp();

//    /////// <summary>
//    /////// <para>Gets the frequency of the timer mechanism as the number of ticks per seconds.</para>
//    /////// </summary>
//    /////// <remarks>The information is derived from <see cref="System.Diagnostics.Stopwatch"/>.</remarks>
//    ////public static long StopwatchFrequency
//    ////  => System.Diagnostics.Stopwatch.Frequency;

//    ///// <summary>
//    ///// <para>Gets the system OS platform.</para>
//    ///// </summary>
//    ///// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
//    //public static string SystemOsPlatform
//    //  => Platforms.First(System.OperatingSystem.IsOSPlatform);

//    ///// <summary>
//    ///// <para>Gets the system OS version.</para>
//    ///// </summary>
//    ///// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
//    //public static System.Version SystemOsVersion
//    //{
//    //  get
//    //  {
//    //    var platform = SystemOsPlatform;

//    //    var major = 0;
//    //    while (major < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major + 1))
//    //      major++;

//    //    var minor = 0;
//    //    while (minor < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor + 1))
//    //      minor++;

//    //    var build = 0;
//    //    while (build < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor, build + 1))
//    //      build++;

//    //    var revision = 0;
//    //    while (revision < int.MaxValue && System.OperatingSystem.IsOSPlatformVersionAtLeast(platform, major, minor, build, revision + 1))
//    //      revision++;

//    //    return new System.Version(major, minor, build, revision);
//    //  }
//    //}

//    /////// <summary>
//    /////// <para>Gets the path of the current user's temporary folder.</para>
//    /////// </summary>
//    /////// <remarks>The information is from <see cref="System.IO.Path.GetTempPath()"/>.</remarks>
//    ////public static System.IO.DirectoryInfo TempDirectory
//    ////  => new(System.IO.Path.GetTempPath());

//    /////// <summary>
//    /////// <para>Gets the user-domain-name (associated with the current user) and user-name (associated with the current thread).</para>
//    /////// </summary>
//    /////// <remarks>The information is from <see cref="System.Environment.UserDomainName"/>.</remarks>
//    ////public static string UserDomainName
//    ////  => System.Environment.UserDomainName;

//    /////// <summary>
//    /////// <para>Gets the user-name (associated with the current thread).</para>
//    /////// </summary>
//    /////// <remarks>The information is from <see cref="System.Environment.UserName"/>.</remarks>
//    ////public static string UserName
//    ////  => System.Environment.UserName;

//    ///// <summary>
//    ///// <para>Creates a <see cref="System.Collections.Generic.IDictionary{string, object?}"/> with all <see cref="Flux.Locale"/> properties (names and values).</para>
//    ///// </summary>
//    ///// <returns></returns>
//    //public static System.Collections.Generic.IDictionary<string, object?> GetProperties()
//    //  => typeof(XtensionEnvironment).GetMemberDictionary(null).ToOrderedDictionary((kvp, i) => kvp.Key.Name, (kvp, i) => kvp.Value);
//  }
//}
