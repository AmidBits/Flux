namespace Flux.Midi.Protocol
{
  public enum RealtimeStatus
  {
    /// <summary>Sent 24 times per quarter note when synchronization is required.</summary>
    TimingClock = 0xF8,
    /// <summary>Undefined (reserved).</summary>
    UndefinedF9 = 0xF9,
    /// <summary>Start the current sequence playing. (This message will be followed with Timing Clocks).</summary>
    Start = 0xFA,
    /// <summary>Continue at the point the sequence was Stopped.</summary>
    Continue = 0xFB,
    /// <summary>Stop the current sequence.</summary>
    Stop = 0xFC,
    /// <summary>Undefined (reserved).</summary>
    UndefinedFD = 0xFD,
    /// <summary>This message is intended to be sent repeatedly to tell the receiver that a connection is alive. Use of this message is optional. When initially received, the receiver will expect to receive another Active Sensing message each 300ms (max), and if it does not then it will assume that the connection has been terminated. At termination, the receiver will turn off allChannels and return to normal (non- active sensing) operation.</summary>
    ActiveSensing = 0xFE,
    /// <summary>Reset all receivers in the system to power-up status. This should be used sparingly, preferably under manual control. In particular, it should not be sent on power-up.</summary>
    Reset = 0xFF
  }
}