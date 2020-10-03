using System.Linq;
using System.Reflection;

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

    /// <summary>Returns the description of the hosting operating system.</summary>
    public static System.OperatingSystem EnvironmentOperatingSystem
      => System.Environment.OSVersion;
    //public static System.Version OperatingSystemVersion
    //  => System.Version.TryParse(OperatingSystemDescription.Substring(OperatingSystemDescription.LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    public static System.PlatformID EnvironmentOsPlatform
      => System.Environment.OSVersion.Platform;
    public static System.Version EnvironmentOsVersion
      => System.Environment.OSVersion.Version;

    /// <summary>Returns the description of the hosting framework.</summary>
    public static string FrameworkDescription
      => System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.ToString();
    public static System.Version FrameworkVersion
      => System.Version.TryParse(FrameworkDescription.Substring(FrameworkDescription.LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

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

    /// <summary>Returns the description of the hosting operating system.</summary>
    public static string OperatingSystemDescription
      => System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim();
    public static System.Version OperatingSystemVersion
      => System.Version.TryParse(OperatingSystemDescription.Substring(OperatingSystemDescription.LastIndexOf(' ')), out var version) ? version : throw new System.NotSupportedException();

    /// <summary>Returns the user domain name from <see cref="System.Environment"/>.</summary>
    public static string UserDomainName
      => System.Environment.UserDomainName;

    /// <summary>Returns the user name from <see cref="System.Environment"/>.</summary>
    public static string UserName
      => System.Environment.UserName;
  }
}
