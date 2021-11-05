namespace Flux.Net
{
  public static partial class IP
  {
    public static readonly System.Net.IPEndPoint RemoteTest = new(System.Net.IPAddress.Parse(@"4.3.2.1"), 4321);

    public static bool TryGetLocalIPAddressByUdp(out System.Net.IPAddress result)
    {
      try
      {
        using var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

        socket.Connect(RemoteTest);

        if (socket.LocalEndPoint is null)
          throw new System.NullReferenceException();

        result = ((System.Net.IPEndPoint)socket.LocalEndPoint).Address;
        return true;
      }
      catch { }

      result = default!;
      return false;
    }
  }
}
