namespace Flux
{
  public static partial class Em
  {
    public static bool TryGetLocalIPAddressByUdp(this System.Net.IPEndPoint remote, out System.Net.IPAddress result)
    {
      try
      {
        remote ??= new(System.Net.IPAddress.Parse(@"4.3.2.1"), 4321);

        using var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

        socket.Connect(remote);

        result = (socket.LocalEndPoint as System.Net.IPEndPoint)?.Address ?? throw new System.NullReferenceException();
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
