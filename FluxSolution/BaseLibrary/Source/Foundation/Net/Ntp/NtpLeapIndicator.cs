﻿namespace Flux.Net
{
  /// <see cref="https://en.wikipedia.org/wiki/Network_Time_Protocol"/>
  public enum NtpLeapIndicator
  {
    NoWarning = 0b00,
    LastMinuteHas61Seconds = 0b01,
    LastMinuteHas59Seconds = 0b10,
    ClockNotSynchronized = 0x11,
  }
}