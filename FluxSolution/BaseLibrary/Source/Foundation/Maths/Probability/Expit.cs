namespace Flux
{
  public static partial class Maths
  {
    /// <summary>The expit, which is the inverse of the natural logit, yields the logistic function of any number x (i.e. this is the same as the logistic function with default arguments).</summary>
    /// <param name="x">The value in the domain of real numbers from [-infinity, +infinity].</param>
    public static double Expit(double x)
      => 1 / (System.Math.Exp(-x) + 1);
  }
}
