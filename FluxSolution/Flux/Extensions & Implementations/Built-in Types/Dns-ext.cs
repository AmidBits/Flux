namespace Flux
{
  public static partial class DnsExtensions
  {
    extension(System.Net.Dns)
    {
      /// <summary>
      /// <para>Gets a fully qualified domain name and outputs the <paramref name="hostName"/> of the computer.</para>
      /// </summary>
      /// <param name="hostName"></param>
      /// <returns>A fully qualified domain name.</returns>
      public static string GetFullyQualifiedDomainName(out string hostName)
      {
        hostName = System.Net.Dns.GetHostName();

        try // Try DNS resolution first (works cross-platform if DNS is configured).
        {
          var hostEntry = System.Net.Dns.GetHostEntry(hostName);

          if (!string.IsNullOrWhiteSpace(hostEntry.HostName))
            return hostEntry.HostName;
        }
        catch { }

        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)) // Windows fallback: use USERDNSDOMAIN environment variable.
        {
          var userDnsDomain = System.Environment.GetEnvironmentVariable("USERDNSDOMAIN");

          if (!string.IsNullOrWhiteSpace(userDnsDomain))
            return $"{hostName}.{userDnsDomain}".TrimEnd('.');
        }
        else // Linux/macOS fallback: sometimes GetHostEntry returns only short name..
        {
          try // Try 'hostname -f' to get the FQDN from the OS.
          {
            using var process = new System.Diagnostics.Process();

            var processStartInfo = new System.Diagnostics.ProcessStartInfo()
            {
              FileName = "hostname",
              Arguments = "-f",
              RedirectStandardOutput = true,
              UseShellExecute = false,
              CreateNoWindow = true
            };

            process.Start();
            string processOutput = process?.StandardOutput.ReadLine()?.Trim() ?? string.Empty; // Read output from 'hostname -f'.
            process?.WaitForExit(1000);

            if (!string.IsNullOrWhiteSpace(processOutput))
              return processOutput;
          }
          catch { }
        }

        return string.Empty;
      }

      /// <summary>
      /// <para>Indicates whether a name is (fully or partially) qualified and outputs the <paramref name="labelCount"/> of the name.</para>
      /// </summary>
      /// <param name="name"></param>
      /// <param name="labelCount">The label count found in the name.</param>
      /// <returns>Whether the name is (fully or partially) qualified.</returns>
      public static bool IsQualifiedDomainName(System.ReadOnlySpan<char> name, out int labelCount)
      {
        labelCount = name.Count('.');

        if (name.Length > 0)
          labelCount++;

        return labelCount > 1;
      }
    }
  }
}
