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

  public static partial class AppDataStoreExtensionMethods
  {
    /// <summary>Returns a <see cref="System.IO.DirectoryInfo"/> object for the specified <see cref="AppDataStore"/>.</summary>
    public static System.IO.DirectoryInfo GetDirectoryInfo(this AppDataStore store)
      => new(GetMsAppDataPath(store));
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
