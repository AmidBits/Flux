using System.Linq;

namespace Flux
{
  /// <summary>Represents the types of stores available for application data.</summary>
  public enum AppDataStore
  {
    /// <summary>Gets the root folder in the local app data store. This folder is backed up to the cloud.</summary>
    Local,
    /// <summary>Gets the root folder in the roaming app data store.</summary>
    Roaming,
    /// <summary>Gets the root folder in the temporary app data store.</summary>
    Temp
  }

  public static class Locale
  {
    /// <summary>Returns the file name (without extension) of the process executable.</summary>
    public static string AppDomainName
      => System.AppDomain.CurrentDomain.FriendlyName;
    /// <summary>Returns the file path of the process executable.</summary>
    public static System.Uri AppDomainPath
      => new System.Uri(System.AppDomain.CurrentDomain.RelativeSearchPath ?? System.AppDomain.CurrentDomain.BaseDirectory ?? typeof(Locale).Module.FullyQualifiedName);

    /// <summary>Returns the version of the common language runtime.</summary>
    public static System.Version ClrVersion
      => System.Environment.Version;

    /// <summary>Returns the DNS primary host name of the computer. Includes the fully qualified domain, if the computer is registered in a domain.</summary>
    public static string ComputerDnsPrimaryHostName
      => System.Net.Dns.GetHostEntry(@"LocalHost").HostName;

    /// <summary>Returns the descriptive text of the current platform identifier.</summary>
    public static string EnvironmentOsTitle
      => System.Environment.OSVersion.ToString().Substring(0, System.Environment.OSVersion.ToString().Trim().LastIndexOf(' '));
    /// <summary>Returns the version of the current platform identifier.</summary>
    public static System.Version EnvironmentOsVersion
      => System.Version.TryParse(System.Environment.OSVersion.ToString().Substring(System.Environment.OSVersion.ToString().Trim().LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    /// <summary>Returns the descriptive text of the hosting framework.</summary>
    public static string FrameworkTitle
      => System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString().Substring(0, System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim().LastIndexOf(' '));
    /// <summary>Returns the version of the hosting framework.</summary>
    public static System.Version FrameworkVersion
      => System.Version.TryParse(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString().Substring(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim().LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    /// <summary>Returns a <see cref="System.IO.DirectoryInfo"/> object for the specified <see cref="AppDataStore"/>.</summary>
    public static System.IO.DirectoryInfo? GetDirectoryInfo(AppDataStore store)
      => store switch
      {
        AppDataStore.Local => new System.IO.DirectoryInfo(@"ms-appdata:///local/"),
        AppDataStore.Roaming => new System.IO.DirectoryInfo(@"ms-appdata:///roaming/"),
        AppDataStore.Temp => new System.IO.DirectoryInfo(@"ms-appdata:///temp/"),
        _ => throw new System.ArgumentOutOfRangeException(nameof(store))
      };

    /// <summary>Returns a <see cref="System.IO.DirectoryInfo"/> object for the specified <see cref="System.Environment.SpecialFolder"/>.</summary>
    public static System.IO.DirectoryInfo? GetDirectoryInfo(System.Environment.SpecialFolder specialFolder)
      => System.Environment.GetFolderPath(specialFolder) is var fp && string.IsNullOrEmpty(fp) ? default : new System.IO.DirectoryInfo(fp);

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
      => System.Runtime.InteropServices.RuntimeInformation.OSDescription.Substring(0, System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim().LastIndexOf(' '));
    /// <summary>Returns the version of the hosting operating system from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>.</summary>
    public static System.Version RuntimeOsVersion
      => System.Version.TryParse(System.Runtime.InteropServices.RuntimeInformation.OSDescription.Substring(System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim().LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    /// <summary>Returns a dictionary of special folder names and they respective directory info paths.</summary>
    public static System.Collections.IDictionary SpecialFolders
    {
      get
      {
        var dictionary = new System.Collections.Specialized.OrderedDictionary();

        var ec = new System.ComponentModel.EnumConverter(typeof(System.Environment.SpecialFolder));

        foreach (var name in System.Enum.GetNames(typeof(System.Environment.SpecialFolder)))
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
      => Flux.Reflexion.Types.GetProperties(typeof(Locale));
  }
}

/* Example:
  System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainName)} = \"{Flux.Locale.AppDomainName}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.AppDomainPath)} = \"{Flux.Locale.AppDomainPath}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.CommonLanguageRuntimeVersion)} = \"{Flux.Locale.CommonLanguageRuntimeVersion}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.ComputerDomainName)} = \"{Flux.Locale.ComputerDomainName}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.ComputerHostName)} = \"{Flux.Locale.ComputerHostName}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.ComputerPrimaryDnsName)} = \"{Flux.Locale.ComputerPrimaryDnsName}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkTitle)} = \"{Flux.Locale.FrameworkTitle}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.FrameworkVersion)} = \"{Flux.Locale.FrameworkVersion}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.MachineName)} = \"{Flux.Locale.MachineName}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemTitle)} = \"{Flux.Locale.OperatingSystemTitle}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.OperatingSystemVersion)} = \"{Flux.Locale.OperatingSystemVersion}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.PlatformTitle)} = \"{Flux.Locale.PlatformTitle}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.PlatformVersion)} = \"{Flux.Locale.PlatformVersion}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickCounter)} = \"{Flux.Locale.TimerTickCounter}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.TimerTickResolution)} = \"{Flux.Locale.TimerTickResolution}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.UserDomainName)} = \"{Flux.Locale.UserDomainName}\"");
  System.Console.WriteLine($"{nameof(Flux.Locale.UserName)} = \"{Flux.Locale.UserName}\"");
*/