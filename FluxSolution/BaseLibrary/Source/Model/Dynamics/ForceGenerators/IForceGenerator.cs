namespace Flux.Model.Dynamics.ForceGenerators
{
  public interface IForceGenerator
  {
    bool Disabled { get; set; }

    void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies);
  }
}
