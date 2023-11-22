namespace Flux
{
  /// <summary>
  /// <para>Represents the types of stores available for application data.</para>
  /// <see href="https://learn.microsoft.com/en-us/uwp/api/windows.storage.applicationdata"/>
  /// </summary>
  [System.Runtime.Versioning.SupportedOSPlatform("Windows")]
  public enum AppDataStore
  {
    /// <summary>Gets the root folder in the local app data store. This folder is backed up to the cloud.</summary>
    Local,
    /// <summary>Gets the root folder in the roaming app data store.</summary>
    Roaming,
    /// <summary>Gets the root folder in the temporary app data store.</summary>
    Temp
  }

  [System.Runtime.Versioning.SupportedOSPlatform("Windows")]
  public static partial class AppDataStoreExtensionMethods
  {
    /// <summary>Creates a <see cref="System.IO.DirectoryInfo"/> for the specified <see cref="AppDataStore"/>.</summary>
    public static System.IO.DirectoryInfo GetDirectoryInfo(this AppDataStore store)
      => new(GetMsAppDataPath(store));

    /// <summary>
    /// <para>Returns a "ms-appdata:" path for the specified <see cref="AppDataStore"/>.</para>
    /// <see href="https://learn.microsoft.com/en-us/uwp/api/windows.storage.applicationdata"/>
    /// </summary>
    public static string GetMsAppDataPath(this AppDataStore store)
      => store switch
      {
        AppDataStore.Local => @"ms-appdata:///local/",
        AppDataStore.Roaming => @"ms-appdata:///roaming/",
        AppDataStore.Temp => @"ms-appdata:///temp/",
        _ => throw new System.ArgumentOutOfRangeException(nameof(store))
      };
  }
}
