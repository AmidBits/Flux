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
        try
        {
          hostName = System.Net.Dns.GetHostName();

          return System.Net.Dns.GetHostEntry(hostName).HostName;
        }
        catch { }

        hostName = string.Empty;

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
