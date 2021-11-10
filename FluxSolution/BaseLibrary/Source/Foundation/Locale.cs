using System.Linq;

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
      => System.Net.Dns.GetHostEntry(@"LocalHost").HostName;

    /// <summary>Returns the descriptive text of the current platform identifier.</summary>
    public static string EnvironmentOsTitle
    {
      get
      {
        var s = System.Environment.OSVersion.ToString();

        return s.Substring(0, s.Trim().LastIndexOf(' '));
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

        return s.Substring(0, s.Trim().LastIndexOf(' '));
      }
    }

    /// <summary>Returns the version of the hosting framework.</summary>
    public static System.Version FrameworkVersion
    {
      get
      {
        var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription;

        if (System.Version.TryParse(s[s.Trim().LastIndexOf(' ')..], out var version))
          return version;

        throw new System.NotSupportedException();
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

        return s.Substring(0, s.Trim().LastIndexOf(' '));
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

        foreach (var name in System.Enum.GetNames(typeof(System.Environment.SpecialFolder)).OrderBy(n => n))
        {
          var ev = (System.Environment.SpecialFolder)ec.ConvertFromString(name);

          var fp = System.Environment.GetFolderPath(ev);

          if (!string.IsNullOrEmpty(fp))
            dictionary.Add(name, new System.IO.DirectoryInfo(fp));
        }

        return dictionary;
      }
    }

    /// <summary>Returns the enumerated operating system platform found in <see cref="System.OperatingSystem"/>. If no title can be determined, an empty string is returned.</summary>
    public static string SystemOsTitle
    {
      get
      {
        var platforms = new string[] { @"Android", @"Browser", @"FreeBSD", @"iOS", @"Linux", @"macOS", @"tvOS", @"watchOS", @"Windows" };

        for (var index = platforms.Length - 1; index >= 0; index--)
          if (System.OperatingSystem.IsOSPlatform(platforms[index]))
            return platforms[index];

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
      => Reflect.GetPropertyInfos(typeof(Locale)).ToDictionary(pi => pi.Name, pi => Reflect.GetValueEx(pi, typeof(Locale)));
  }
}

#if ZAMPLEZ
namespace Flux
{
  public static partial class Zamplez
  {
    /// <summary>Run the locale zample.</summary>
    public static void RunLocale()
    {
      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainName)} = \"{Flux.Locale.AppDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainPath)} = \"{Flux.Locale.AppDomainPath}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ClrVersion)} = \"{Flux.Locale.ClrVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.ComputerDnsPrimaryHostName)} = \"{Flux.Locale.ComputerDnsPrimaryHostName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.EnvironmentOsTitle)} = \"{Flux.Locale.EnvironmentOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.EnvironmentOsVersion)} = \"{Flux.Locale.EnvironmentOsVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkTitle)} = \"{Flux.Locale.FrameworkTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkVersion)} = \"{Flux.Locale.FrameworkVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.MachineName)} = \"{Flux.Locale.MachineName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.NetworkDomainName)} = \"{Flux.Locale.NetworkDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.NetworkHostName)} = \"{Flux.Locale.NetworkHostName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsArchitecture)} = \"{Flux.Locale.RuntimeOsArchitecture}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsTitle)} = \"{Flux.Locale.RuntimeOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.RuntimeOsVersion)} = \"{Flux.Locale.RuntimeOsVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.SystemOsTitle)} = \"{Flux.Locale.SystemOsTitle}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.SystemOsVersion)} = \"{Flux.Locale.SystemOsVersion}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickCounter)} = \"{Flux.Locale.TimerTickCounter}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickResolution)} = \"{Flux.Locale.TimerTickResolution}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserDomainName)} = \"{Flux.Locale.UserDomainName}\"");
      System.Console.WriteLine($"{nameof(Flux.Locale.UserName)} = \"{Flux.Locale.UserName}\"");
      System.Console.WriteLine();
    }
  }
}
#endif
