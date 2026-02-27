namespace Flux
{
  public static class OperatingSystemExtensions
  {
    extension(System.OperatingSystem)
    {
      /// <summary>
      /// <para>Gets an array of platform strings.</para>
      /// </summary>
      /// <remarks>The information is derived from <see cref="System.OperatingSystem"/>.</remarks>
      public static string[] GetOsPlatforms()
        => [.. typeof(System.OperatingSystem).GetMethods().Where(mi => mi.ReturnType == typeof(bool) && mi.GetParameters().Length == 0 && mi.Name.StartsWith("Is")).Select(mi => mi.Name[2..]).Order()];

      /// <summary>
      /// <para>Gets the system OS platform.</para>
      /// </summary>
      /// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
      public static string CurrentOsPlatform
        => GetOsPlatforms().First(System.OperatingSystem.IsOSPlatform);

      /// <summary>
      /// <para>Gets the system OS version.</para>
      /// </summary>
      /// <remarks>The information comes from <see cref="System.OperatingSystem"/>.</remarks>
      public static System.Version CurrentOsVersion
      {
        get
        {
          var platform = get_CurrentOsPlatform();

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
    }
  }
}
