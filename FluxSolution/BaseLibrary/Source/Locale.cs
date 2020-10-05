using System.Linq;
using System.Reflection;
using System.Resources;

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
    /// <summary>Returns the file name of the process executable.</summary>
    public static string AppDomainName
      => System.AppDomain.CurrentDomain.FriendlyName;

    /// <summary>Returns the version of the common language runtime.</summary>
    public static System.Version ClrVersion
      => System.Environment.Version;

    /// <summary>Returns the computer domain namn, or string.Empty if the computer is not registered in a domain.</summary>
    public static string ComputerDomainName
      => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;

    /// <summary>Returns the host name of the computer.</summary>
    public static string ComputerHostName
      => System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().HostName;

    /// <summary>Returns the primary DNS name of the computer. Includes the fully qualified domain, if the computer is registered in a domain.</summary>
    public static string ComputerPrimaryDnsName
      => System.Net.Dns.GetHostEntry(@"LocalHost").HostName;

    /// <summary>Returns the descriptive text of the current platform identifier.</summary>
    public static string PlatformTitle
      => System.Environment.OSVersion.ToString().Substring(0, System.Environment.OSVersion.ToString().LastIndexOf(' '));
    /// <summary>Returns the version of the current platform identifier.</summary>
    public static System.Version PlatformVersion
      => System.Version.TryParse(System.Environment.OSVersion.ToString().Substring(System.Environment.OSVersion.ToString().LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    /// <summary>Returns the descriptive text of the hosting framework.</summary>
    public static string FrameworkTitle
      => System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString().Substring(0, System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString().LastIndexOf(' '));
    /// <summary>Returns the version of the hosting framework.</summary>
    public static System.Version FrameworkVersion
      => System.Version.TryParse(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString().Substring(System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString().LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    public static System.IO.DirectoryInfo? GetDirectoryInfo(AppDataStore store)
      => store switch
      {
        AppDataStore.Local => new System.IO.DirectoryInfo("ms-appdata:///local/"),
        AppDataStore.Roaming => new System.IO.DirectoryInfo("ms-appdata:///roaming/"),
        AppDataStore.Temp => new System.IO.DirectoryInfo("ms-appdata:///temp/"),
        _ => throw new System.ArgumentOutOfRangeException(nameof(store))
      };

    public static System.IO.DirectoryInfo? GetDirectoryInfo(System.Environment.SpecialFolder specialFolder)
      => System.Environment.GetFolderPath(specialFolder) is var fp && string.IsNullOrEmpty(fp) ? default : new System.IO.DirectoryInfo(fp);

    /// <summary>Returns the computer name from <see cref="System.Environment"/>.</summary>
    public static string MachineName
      => System.Environment.MachineName;

    /// <summary>Returns the descriptive text of the hosting operating system from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>.</summary>
    public static string OperatingSystemTitle
      => System.Runtime.InteropServices.RuntimeInformation.OSDescription.Substring(0, System.Runtime.InteropServices.RuntimeInformation.OSDescription.LastIndexOf(' '));
    /// <summary>Returns the version of the hosting operating system from <see cref="System.Runtime.InteropServices.RuntimeInformation"/>.</summary>
    public static System.Version OperatingSystemVersion
      => System.Version.TryParse(System.Runtime.InteropServices.RuntimeInformation.OSDescription.Substring(System.Runtime.InteropServices.RuntimeInformation.OSDescription.LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

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
  }
}
