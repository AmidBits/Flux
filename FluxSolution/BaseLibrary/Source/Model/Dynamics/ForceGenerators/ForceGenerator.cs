namespace Flux.Model.Dynamics.ForceGenerators
{
  public class ForceGenerator
    : IForceGenerator
  {
    public virtual bool Disabled { get; set; }

    public virtual void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies)
    {
    }
  }
}
