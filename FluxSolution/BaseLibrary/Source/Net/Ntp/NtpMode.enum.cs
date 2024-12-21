namespace Flux.Net
{
  /// <see href="https://en.wikipedia.org/wiki/Network_Time_Protocol"/>
  public enum NtpMode
  {
    Reserved = 0b000,
    SymmetricActive = 0b001,
    SymmetricPassive = 0b010,
    Client = 0b011,
    Server = 0b100,
    Broadcast = 0b101,
    NtpControlMessage = 0b110,
    ReservedForPrivateUse = 0b111,
  }
}
