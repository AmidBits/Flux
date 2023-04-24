namespace Flux
{
  public static class Locale
  {
    /// <summary>Returns the file name (without extension) of the process executable.</summary>
    public static string AppDomainName
      => System.AppDomain.CurrentDomain.FriendlyName;

    /// <summary>Returns the file path of the process executable.</summary>
    public static System.Uri AppDomainPath
      => new(System.AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory ?? typeof(Locale).Module.FullyQualifiedName);

    /// <summary>Returns the version of the common language runtime.</summary>
    public static System.Version ClrVersion
      => System.Environment.Version;

    /// <summary>Returns the DNS primary host name of the computer. Includes the fully qualified domain, if the computer is registered in a domain.</summary>
    public static string ComputerDnsPrimaryHostName
      => System.Net.Dns.GetHostEntry("LocalHost").HostName;

    /// <summary>Returns the descriptive text of the current platform identifier.</summary>
    public static string EnvironmentOsTitle
    {
      get
      {
        var s = System.Environment.OSVersion.ToString();

        return s[..s.Trim().LastIndexOf(' ')];
      }
    }

    /// <summary>Returns the version of the current platform identifier.</summary>
    public static System.Version EnvironmentOsVersion
    {
      get
      {
        var s = System.Environment.OSVersion.ToString();

        if (System.Version.TryParse(s[s.Trim().LastIndexOf(' ')..], out var version))
          return version;

        throw new System.NotSupportedException();
      }
    }

    /// <summary>Returns the descriptive text of the hosting framework.</summary>
    public static string FrameworkTitle
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

        return s[..s.Trim().LastIndexOf(' ')];
      }
    }

    /// <summary>Returns the version of the hosting framework.</summary>
    public static Version FrameworkVersion
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

        if (Version.TryParse(s[s.Trim().LastIndexOf(' ')..], out var version))
          return version;

        return new();
      }
    }

    /// <summary>Returns the computer name from <see cref="System.Environment"/>.</summary>
    public static string MachineName
      => System.Environment.MachineName;

    /// <summary>Returns the computer domain namn, or string.Empty if the computer is not registered in a domain.</summary>
    public static string NetworkDomainName
      => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

    /// <summary>Returns the host name of the computer.</summary>
    public static string NetworkHostName
      => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().HostName;

    /// <summary>Returns the descriptive text of the hosting operating system from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>.</summary>
    public static string RuntimeOsArchitecture
      => System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

    /// <summary>Returns the descriptive text of the hosting operating system from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>.</summary>
    public static string RuntimeOsTitle
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

        return s[..s.Trim().LastIndexOf(' ')];
      }
    }

    /// <summary>Returns the version of the hosting operating system from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>.</summary>
    public static System.Version RuntimeOsVersion
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription;

        if (System.Version.TryParse(s[s.Trim().LastIndexOf(' ')..], out var version))
          return version;

        throw new System.NotSupportedException();
      }
    }

    /// <summary>Returns a dictionary of special folder names and they respective directory info paths.</summary>
    public static System.Collections.IDictionary SpecialFolders
    {
      get
      {
        var dictionary = new System.Collections.Specialized.OrderedDictionary();

        var ec = new System.ComponentModel.EnumConverter(typeof(System.Environment.SpecialFolder));

        var names = System.Enum.GetNames(typeof(System.Environment.SpecialFolder));
        System.Array.Sort(names);

        for (var index = 0; index < names.Length; index++)
        {
          var name = names[index];

          var fp = System.Environment.GetFolderPath((System.Environment.SpecialFolder?)ec.ConvertFromString(name) ?? throw new System.NullReferenceException());

          if (!string.IsNullOrEmpty(fp))
            dictionary.Add(name, new System.IO.DirectoryInfo(fp));
        }

        return dictionary;
      }
    }

    private static string[] m_platformStrings = new string[] { @"Android", @"Browser", @"FreeBSD", @"iOS", @"Linux", @"tvOS", @"watchOS", @"Windows" };

    /// <summary>Returns the enumerated operating system platform found in <see cref="System.OperatingSystem"/>. If no title can be determined, an empty string is returned.</summary>
    public static string SystemOsTitle
    {
      get
      {
        for (var index = m_platformStrings.Length - 1; index >= 0; index--)
          if (m_platformStrings[index] is var platformString && System.OperatingSystem.IsOSPlatform(platformString))
            return platformString;

        return string.Empty;
      }
    }

    /// <summary>Returns the enumerated operating system platform version found in <see cref="System.OperatingSystem"/>. If no version can be determined, 0.0.0.0 is returned.</summary>
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

    /// <summary>Returns the number of ticks in the timer mechanism from <see cref="System.Diagnostics.Stopwatch"/>.</summary>
    public static long TimerTickCounter
      => System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>Returns the number of ticks per seconds of the timer mechanism from <see cref="System.Diagnostics.Stopwatch"/>.</summary>
    public static long TimerTickResolution
      => System.Diagnostics.Stopwatch.Frequency;

    /// <summary>Returns the user domain name from <see cref="System.Environment"/>.</summary>
    public static string UserDomainName
      => System.Environment.UserDomainName;

    /// <summary>Returns the user name from <see cref="System.Environment"/>.</summary>
    public static string UserName
      => System.Environment.UserName;

    public static System.Collections.Generic.IDictionary<string, object?> GetProperties()
      => System.Linq.Enumerable.ToDictionary(Reflection.GetPropertyInfos(typeof(Locale)), pi => pi.Name, pi => pi.GetValue(null));
  }
}
