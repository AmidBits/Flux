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
}
