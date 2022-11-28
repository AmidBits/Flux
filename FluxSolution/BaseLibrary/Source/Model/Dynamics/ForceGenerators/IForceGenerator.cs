namespace Flux.Model.Dynamics.ForceGenerators
{
  public interface IForceGenerator
  {
    void ApplyForce(RigidBody body);
  }
}
