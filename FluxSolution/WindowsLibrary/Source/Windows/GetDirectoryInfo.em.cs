namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Creates a <see cref="System.IO.DirectoryInfo"/> for the specified <see cref="AppDataStore"/>.</para>
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("Windows")]
    public static System.IO.DirectoryInfo GetDirectoryInfo(this AppDataStore store)
      => new(GetMsAppDataPath(store));

    /// <summary>
    /// <para>Returns a "ms-appdata:" path for the specified <see cref="AppDataStore"/>.</para>
    /// <see href="https://learn.microsoft.com/en-us/uwp/api/windows.storage.applicationdata"/>
    /// </summary>
    [System.Runtime.Versioning.SupportedOSPlatform("Windows")]
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
