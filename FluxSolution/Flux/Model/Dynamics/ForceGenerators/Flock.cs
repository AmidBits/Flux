//using Flux.Random;
//using System.Collections.Generic;
//using System.Linq;

namespace Flux.Model.Dynamics.ForceGenerators
{
  //  //public class Neighborhood
  //  //  : RigidBody
  //  //{
  //  //  public System.Numerics.Vector2 AverageDifference = new System.Numerics.Vector2();
  //  //  public System.Numerics.Vector2 AverageVelocity = new System.Numerics.Vector2();
  //  //  public System.Numerics.Vector2 AveragePosition = new System.Numerics.Vector2();

  //  //  public RigidBody2D ClosestNeighbor = null;
  //  //  public float ClosestNeighborDistance = float.NaN;
  //  //  public System.Numerics.Vector2 ClosestNeighborVector = default(System.Numerics.Vector2);

  //  //  public bool Enabled { get; set; }

  //  //  public System.Numerics.Vector2 PositionDifference = new System.Numerics.Vector2();
  //  //  public System.Numerics.Vector2 PositionSum = new System.Numerics.Vector2();
  //  //  public System.Numerics.Vector2 VelocitySum = new System.Numerics.Vector2();

  //  //  public float PerimeterRadius = 21.5f;

  //  //  public void FindNeighbors(RigidBody2D body, RigidBodies2D bodies)
  //  //  {
  //  //    if (!Enabled)
  //  //      return;

  //  //    Clear();

  //  //    AverageVelocity = default(System.Numerics.Vector2);
  //  //    AveragePosition = default(System.Numerics.Vector2);

  //  //    ClosestNeighbor = null;
  //  //    ClosestNeighborDistance = float.MaxValue;
  //  //    ClosestNeighborVector = default(System.Numerics.Vector2);

  //  //    foreach (RigidBody2D boid in bodies)
  //  //    {
  //  //      if (!boid.Equals(body))
  //  //      {
  //  //        System.Numerics.Vector2 point = boid.Position - body.Position;
  //  //        float distance = point.Length();

  //  //        if (distance < PerimeterRadius)
  //  //        {
  //  //          Add(boid);
  //  //          PositionDifference -= point;
  //  //          PositionSum += boid.Velocity;
  //  //          VelocitySum += boid.Velocity;
  //  //        }

  //  //        if (distance < ClosestNeighborDistance)
  //  //        {
  //  //          ClosestNeighbor = boid;
  //  //          ClosestNeighborDistance = distance;
  //  //          ClosestNeighborVector = point;
  //  //        }
  //  //      }
  //  //    }

  //  //    if (Count == 0)
  //  //      Add(ClosestNeighbor);

  //  //    AveragePosition = PositionSum / Count;
  //  //    AverageVelocity = VelocitySum / Count;
  //  //  }
  //  //}
  //  public class FlockBehavior
  //    : IForceGenerator
  //  {
  //    public bool Disabled { get; set; }

  //    public float PerimeterRadius = 21.5f;

  //    public float CohesionWeight { get; set; }

  //    public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
  //    {
  //      var neighbors = bodies;
  //      var centroidOfNeighbors = Media.Geometry.Shapes.Polygon.SurfaceCentroid(neighbors.Select(rb => rb.Position));

  //      // Flock cohesion (centering).
  //      body.Force += ((centroidOfNeighbors - body.Position) * (CohesionWeight * body.Mass));
  //      //body.Force += ((body.Neighborhood.AveragePosition - body.Position) * CohesionWeight);

  //      // Flock separation (distancing).
  //      body.Force += neighbors.AverageDifference * (SeparationWeight * body.Mass);
  //    }
  //  }

  //public class FlockCentering
  //  : IForceGenerator
  //{
  //  public bool Disabled { get; set; }

  //  public float CohesionWeight { get; set; }

  //  public FlockCentering(float cohesionWeight = 0.01f) { CohesionWeight = cohesionWeight; }

  //  public void ApplyForce(RigidBody body, IEnumerable<RigidBody> bodies)
  //  {
  //    body.Force += ((bodies.Centroid - body.Position) * (CohesionWeight * body.Mass));
  //    //body.Force += ((body.Neighborhood.AveragePosition - body.Position) * CohesionWeight);
  //  }
  //}

  //public class AlignAverageDistancing
  //  : IForceGenerator
  //{
  //  public bool Disabled { get; set; }

  //  public System.Numerics.Vector3 AverageDifference { get; set; }
  //  public float Weight { get; set; } = 15f;

  //  public void ApplyForce(RigidBody body)
  //  {
  //    if (body is null) throw new System.ArgumentNullException(nameof(body));
  //    if (Disabled) return;

  //    body.Force += AverageDifference * (Weight * body.Mass);
  //  }
  //}

  //public class AlignAverageSpeed
  //    : IForceGenerator
  //{
  //  public bool Disabled { get; set; }

  //  public System.Numerics.Vector3 AverageLinearVelocity { get; set; }
  //  public float Weight { get; set; } = 0.00125f;

  //  public void ApplyForce(RigidBody body)
  //  {
  //    if (body is null) throw new System.ArgumentNullException(nameof(body));
  //    if (Disabled) return;

  //    body.Force += AverageLinearVelocity * Weight;
  //  }
  //}

  /// <summary>Align direction and speed with local mates.</summary>
  public sealed class BoidAlignment
  : IForceGenerator
  {
    public bool Disabled { get; set; }

    public System.Numerics.Vector3 AverageLinearVelocity { get; set; }
    public float Weight { get; set; } = 0.00125f;

    public void ApplyForce(RigidBody body)
    {
      System.ArgumentNullException.ThrowIfNull(body);

      if (Disabled) return;

      body.Force += AverageLinearVelocity * Weight;
    }
  }

  /// <summary>Strive for cohesion of centroid with local mates.</summary>
  public sealed class BoidCohesion
    : IForceGenerator
  {
    public bool Disabled { get; set; }

    public System.Numerics.Vector3 Centroid { get; set; }
    public float Weight { get; set; } = 0.00125f;

    public void ApplyForce(RigidBody body)
    {
      System.ArgumentNullException.ThrowIfNull(body);

      if (Disabled) return;

      body.Force += (Centroid - body.ObjectPosition) * (Weight * body.Mass);
    }
  }

  /// <summary>Maintain separation with local mates.</summary>
  public sealed class BoidSeparation
    : IForceGenerator
  {
    public bool Disabled { get; set; }

    public System.Numerics.Vector3 AverageDifference { get; set; }
    public float Weight { get; set; } = 15f;

    public void ApplyForce(RigidBody body)
    {
      System.ArgumentNullException.ThrowIfNull(body);

      if (Disabled) return;

      body.Force += AverageDifference * (Weight * body.Mass);
    }
  }

  public sealed class PointOfInterest
    : IForceGenerator
  {
    public bool Disabled { get; set; }

    public System.Numerics.Vector3 Point { get; set; }
    public float Weight { get; set; } = 0.017f;

    public void ApplyForce(RigidBody body)
    {
      System.ArgumentNullException.ThrowIfNull(body);

      if (Disabled) return;

      body.Force += ((Point - body.ObjectPosition) * (Weight * body.Mass));
    }
  }

  public sealed class RandomWandering
   : IForceGenerator
  {
    public bool Disabled { get; set; }

    public float Weight { get; set; } = 1f;

    public void ApplyForce(RigidBody body)
    {
      System.ArgumentNullException.ThrowIfNull(body);

      if (Disabled) return;

      body.Force += (Weight * body.Mass) * new System.Numerics.Vector3((float)RandomNumberGenerators.SscRng.Shared.NextDouble() - 0.5f, (float)RandomNumberGenerators.SscRng.Shared.NextDouble() - 0.5f, (float)RandomNumberGenerators.SscRng.Shared.NextDouble() - 0.5f);
    }
  }
}
