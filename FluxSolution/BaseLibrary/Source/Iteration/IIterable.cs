namespace Flux
{
  public interface IIterable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf StepBackward();
    TSelf StepForward();

    System.Collections.Generic.IEnumerable<TSelf> EnumerateBackward()
    {
      while(true)
        yield return StepBackward();
    }
    System.Collections.Generic.IEnumerable<TSelf> EnumerateForward()
    {
      while(true)
        yield return StepForward();
    }
  }
}
