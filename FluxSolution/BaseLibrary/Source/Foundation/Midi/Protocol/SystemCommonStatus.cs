namespace Flux.Midi
{
  public enum SystemCommonStatus
  {
    /// <summary>This message type allows manufacturers to create their own messages (such as bulk dumps, patch parameters, and other non-spec data) and provides a mechanism for creating additional MIDI Specification messages. The Manufacturer's ID code (assigned by MMA or AMEI) is either 1 byte (0iiiiiii) or 3 bytes (0iiiiiii 0iiiiiii 0iiiiiii). Two of the 1 Byte IDs are reserved for extensions called Universal Exclusive Messages, which are not manufacturer-specific. If a device recognizes the ID code as its own (or as a supported Universal message) it will listen to the rest of the message (0ddddddd). Otherwise, the message will be ignored. (Note: Only Real-Time messages may be interleaved with a System Exclusive.)</summary>
    StartOfExclusive = 0xF0,
    /// <summary></summary>
    MidiTimeCodeQuarterFrame = 0xF1,
    /// <summary></summary>
    SongPositionPointer = 0xF2,
    /// <summary></summary>
    SongSelect = 0xF3,
    /// <summary>Undefined (reserved).</summary>
    UndefinedF4 = 0xF4,
    /// <summary>Undefined (reserved).</summary>
    UndefinedF5 = 0xF5,
    /// <summary>Upon receiving a Tune Request, all analog synthesizers should tune their oscillators.</summary>
    TuneRequest = 0xF6,
    /// <summary>Used to terminate a System Exclusive dump (see above).</summary>
    EndOfExclusive = 0xF7
  }
}
