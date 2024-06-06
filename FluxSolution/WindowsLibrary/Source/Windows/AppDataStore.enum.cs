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
}
