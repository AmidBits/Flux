//using System.Collections.Generic;
//using System.Linq;

//namespace Flux.Model.Dynamics.ForceGenerators
//{
//  public abstract class ForceGenerator
//    : IForceGenerator
//  {
//    public virtual bool Enabled { get; set; }

//    public abstract void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies);
//  }

//  public class ForceGeneratorList
//    : System.Collections.Generic.List<IForceGenerator>, IForceGenerator
//  {
//    public bool Enabled { get; set; }

//    public abstract void ApplyForce(RigidBody body, System.Collections.Generic.IEnumerable<RigidBody> bodies);
//  }
//}
