namespace Flux
{
  public interface IIterable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf StepBackward();
    TSelf StepForward();
  }
}
