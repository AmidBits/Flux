﻿namespace Flux.Numerics.AmbOps
{
  public interface IValue<T>
  {
    T Value { get; }
    string ToString();
  }
}
