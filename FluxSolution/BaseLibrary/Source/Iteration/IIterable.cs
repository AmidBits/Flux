namespace Flux
{
  public interface IIterable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf StepBackward();
    TSelf StepForward();

    // System.Collections.Generic.IEnumerable<TSelf> EnumerateBackward();
    // System.Collections.Generic.IEnumerable<TSelf> EnumerateForward();
  }
}
