namespace Flux.Model.Dynamics.ForceGenerators
{
  /// <summary></summary>
  /// <seealso cref="https://msdn.microsoft.com/en-us/library/microsoft.xna.framework.vector3.dot.aspx"/>
  public sealed class GravityForce
    : IForceGenerator
  {
    public const float GravityOnEarth = 9.780F;
    public const float GravityOnMars = 3.711F;
    public const float GravityOnTheMoon = 1.622F;

    public System.Numerics.Vector3 GravitationalPull { get; set; } = new System.Numerics.Vector3(0, GravityOnEarth, 0);

    public void ApplyForce(RigidBody body)
    {
      System.ArgumentNullException.ThrowIfNull(body);

      // Dot product: >0 = <90, <0 = >90, =0 = 90, =1 = parallel same dir, =-1 = parallel opposite dir.
      // If the gravitational pull and the body position is within 90 degrees then apply gravity.

      //if (System.Numerics.Vector3.Dot(System.Numerics.Vector3.Normalize(GravitationalPull), System.Numerics.Vector3.Normalize(body.Position)) > 0)
      body.ApplyForce(System.Numerics.Vector3.Zero, GravitationalPull);
    }
  }
}
