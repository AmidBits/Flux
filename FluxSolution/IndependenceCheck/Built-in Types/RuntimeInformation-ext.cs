namespace Flux
{
  public static class RuntimeInformationExtensions
  {
    extension(System.Runtime.InteropServices.RuntimeInformation)
    {
      /// <summary>
      /// <para>Gets the title and version of the .NET-installation on which a process is running.</para>
      /// </summary>
      /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription"/>.</remarks>
      public static (string Title, System.Version Version) Framework
      {
        get
        {
          var s = System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription.Trim().AsSpan();

          var i = s.LastIndexOf(' ');

          if (i > -1)
          {
            var title = s[..i].TrimEnd();

            if (!System.Version.TryParse(s[i..].TrimStart(), out var version))
              version = new();

            return (title.ToString(), version);
          }
          else
            return (s.ToString(), new());
        }
      }

      /// <summary>
      /// <para>Gets all <see cref="System.Runtime.InteropServices.OSPlatform"/>s available.</para>
      /// </summary>
      /// <returns></returns>
      public static System.Runtime.InteropServices.OSPlatform[] GetOsPlatforms()
        => [.. typeof(System.Runtime.InteropServices.OSPlatform).GetProperties().Where(pi => pi.PropertyType == typeof(System.Runtime.InteropServices.OSPlatform)).Select(pi => pi.GetValue(null)).Cast<System.Runtime.InteropServices.OSPlatform>()];

      /// <summary>
      /// <para>Gets the current runtime OS-platform.</para>
      /// </summary>
      /// <remarks>Note that to compare what the current OS platform is, do not use this. Query the <see cref="System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform)"/> instead.</remarks>
      /// <returns></returns>
      /// <exception cref="System.NotImplementedException"></exception>
      public static System.Runtime.InteropServices.OSPlatform OsPlatform
        => GetOsPlatforms().Single(System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform);

      /// <summary>
      /// <para>Gets the title and version of the runtime-OS on which the process is running.</para>
      /// </summary>
      /// <remarks>The information is derived from <see cref="System.Runtime.InteropServices.RuntimeInformation.OSDescription"/>.</remarks>
      public static (string Title, System.Version Version) OS
      {
        get
        {
          var s = System.Runtime.InteropServices.RuntimeInformation.OSDescription.Trim().AsSpan();

          var i = s.LastIndexOf(' ');

          if (i > -1)
          {
            var title = s[..i].TrimEnd();

            if (!System.Version.TryParse(s[i..].TrimStart(), out var version))
              version = new();

            return (title.ToString(), version);
          }
          else
            return (s.ToString(), new());
        }
      }
    }
  }
}
