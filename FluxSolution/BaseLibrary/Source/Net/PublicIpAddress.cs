namespace Flux
{
  public static class PublicIpAddress
  {
    public static System.Collections.Generic.IReadOnlyList<System.Uri> Hosts { get; } = [new System.Uri("https://api.ipify.org"), new System.Uri("https://ipinfo.io/ip"), new System.Uri("https://checkip.amazonaws.com")];

    public static bool TryGetIPAddress(System.Uri host, out System.Net.IPAddress result)
    {
      try
      {
        using var hc = new System.Net.Http.HttpClient();

        result = System.Net.IPAddress.Parse(hc.GetStringAsync(host).Result);
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    public static bool TryGetIPAddress(out System.Net.IPAddress result)
    {
      result = Hosts.AsParallel().Select(host => TryGetIPAddress(host, out var ip) ? ip : default).FirstOrDefault(ip => ip != default)!;
      return result != default;
    }
  }
}
