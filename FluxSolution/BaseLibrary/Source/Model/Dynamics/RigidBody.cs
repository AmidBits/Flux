namespace Flux.Model.Dynamics
{
  // http://www.enchantedage.com/node/68
  public class RigidBody
    : Game.Engine.IObjectPositionable, Game.Engine.IObjectRotatable, Game.Engine.IObjectScalable
  {
    public System.Collections.Generic.List<ForceGenerators.IForceGenerator> ForceGenerators { get; } = new System.Collections.Generic.List<ForceGenerators.IForceGenerator>();

    public float CoefficientOfDrag { get; set; } = 0.35F;
    public float CoefficientOfRestitution { get; set; } = 0.95F;

    public float Mass { get; set; } = 7F;

    public System.Numerics.Vector3 LinearVelocity { get; set; }
    public System.Numerics.Vector3 Force { get; set; }

    public System.Numerics.Vector3 AngularVelocity { get; set; }
    public System.Numerics.Vector3 Torque { get; set; }

    public System.Numerics.Vector3 ObjectPosition { get; set; }
    public System.Numerics.Quaternion ObjectRotation { get; set; } = System.Numerics.Quaternion.Identity;
    public System.Numerics.Vector3 ObjectScale { get; set; } = System.Numerics.Vector3.One;

    public System.Numerics.Vector3 Volume { get; set; } = new System.Numerics.Vector3(0.5F, 2F, 0.25F);

    public void Integrate(float deltaTime)
    {
      foreach (var forceGenerator in ForceGenerators)
        forceGenerator.ApplyForce(this);

      var linearAcceleration = Force / Mass;
      LinearVelocity += linearAcceleration * deltaTime;
      Force = System.Numerics.Vector3.Zero;

      var angularAcceleration = Torque / Mass;
      AngularVelocity += angularAcceleration * deltaTime;
      Torque = System.Numerics.Vector3.Zero;

      ObjectPosition += LinearVelocity * deltaTime;

      ObjectRotation += new System.Numerics.Quaternion(AngularVelocity * deltaTime, 0) * ObjectRotation;
      System.Numerics.Quaternion.Normalize(ObjectRotation);
    }

    public void ApplyForce(System.Numerics.Vector3 forcePosition, System.Numerics.Vector3 directionMagnitude)
    {
      if (directionMagnitude.LengthSquared() is float lengthSquared && lengthSquared > float.Epsilon * 100)
      {
        Force += directionMagnitude;

        if (forcePosition != default)
          Torque += System.Numerics.Vector3.Cross(directionMagnitude, forcePosition - ObjectPosition);
      }
    }

    public System.Numerics.Vector3 PointVelocity(System.Numerics.Vector3 worldPoint)
      => System.Numerics.Vector3.Cross(AngularVelocity, worldPoint - ObjectPosition) + LinearVelocity;

    public override string ToString() 
      => $"P:{ObjectPosition}, O:{ObjectRotation}, LV:{LinearVelocity}, AV:{AngularVelocity}";
  }

  //public class RigidBodyList
  //: System.Collections.Generic.List<RigidBody>
  //{
  //  public System.Numerics.Vector3 Centroid { get; set; }

  //  public System.Numerics.Vector3 CalculateCentroid()
  //  {
  //    Centroid = new System.Numerics.Vector3();
  //    foreach (RigidBody body in this)
  //      Centroid += body.Position;
  //    Centroid /= Count;
  //    return Centroid;
  //  }

  //  public ForceGenerators.ForceGeneratorList ForceGenerators { get; set; } = new ForceGenerators.ForceGeneratorList();

  //  public virtual void Update(float deltaTime)
  //  {
  //    Centroid = Polygon.ComputeCentroid(this.Select(rb => rb.Position));

  //    foreach (RigidBody body in this)
  //    {
  //      //body.Neighborhood.FindNeighbors(body, this);

  //      //body.ComputeForces(ForceGenerators, this);

  //      body.Integrate(deltaTime);

  //      // HandleCollisions(body, this) { }
  //    }
  //  }
  //}
}
