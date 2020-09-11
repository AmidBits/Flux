namespace Flux.Model.Dynamics.ForceGenerators
{
  public class LambdaForce
    : ForceGenerator
  {
    public System.Func<RigidBody, System.Collections.Generic.IEnumerable<RigidBody>, System.Numerics.Vector3>? ForcePosition { get; set; }
    public System.Func<RigidBody, System.Collections.Generic.IEnumerable<RigidBody>, System.Numerics.Vector3>? DirectionMagnitude { get; set; }

    public override void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
      => (body ?? throw new System.ArgumentNullException(nameof(body))).ApplyForce(ForcePosition?.Invoke(body, bodies) ?? System.Numerics.Vector3.Zero, DirectionMagnitude?.Invoke(body, bodies) ?? System.Numerics.Vector3.Zero);
  }
}
