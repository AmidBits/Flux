namespace Flux.Model.Dynamics.ForceGenerators
{
  public class DragForce
    : ForceGenerator
  {
    public const float DensityOfAir = 1.29F;
    public const float DensityOfWater = 1.94F;

    public float DragDensity { get; set; } = DensityOfAir;

    public override void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
    {
      if (body is null) throw new System.ArgumentNullException(nameof(body));

      var dragForce = 0.5f * DragDensity * (float)System.Math.Pow(body.LinearVelocity.Length(), 2) * body.CoefficientOfDrag;

      body.ApplyForce(System.Numerics.Vector3.Zero, System.Numerics.Vector3.Negate(body.LinearVelocity * dragForce));
    }
  }
}
