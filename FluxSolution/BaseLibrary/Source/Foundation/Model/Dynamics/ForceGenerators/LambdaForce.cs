namespace Flux.Model.Dynamics.ForceGenerators
{
  public sealed class LambdaForce
    : IForceGenerator
  {
    public System.Func<RigidBody, System.Numerics.Vector3>? ForcePosition { get; set; }
    public System.Func<RigidBody, System.Numerics.Vector3>? DirectionMagnitude { get; set; }

    public void ApplyForce(RigidBody body)
      => (body ?? throw new System.ArgumentNullException(nameof(body))).ApplyForce(ForcePosition?.Invoke(body) ?? System.Numerics.Vector3.Zero, DirectionMagnitude?.Invoke(body) ?? System.Numerics.Vector3.Zero);
  }
}
