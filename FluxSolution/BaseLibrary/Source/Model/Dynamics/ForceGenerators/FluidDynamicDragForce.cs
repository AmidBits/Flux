namespace Flux.Model.Dynamics.ForceGenerators
{
  public class FluidDynamicDragForce
    : IForceGenerator
  {
    public const float DensityOfAir = 1.29F;
    public const float DensityOfWater = 1.94F;

    public float DragDensity { get; set; } = DensityOfAir;

    public void ApplyForce(RigidBody body)
    {
      if (body is null) throw new System.ArgumentNullException(nameof(body));

      var dragForce = 0.5f * DragDensity * (float)System.Math.Pow(body.LinearVelocity.Length(), 2) * body.CoefficientOfDrag;

      body.ApplyForce(System.Numerics.Vector3.Zero, System.Numerics.Vector3.Negate(body.LinearVelocity * dragForce));
    }
  }
}