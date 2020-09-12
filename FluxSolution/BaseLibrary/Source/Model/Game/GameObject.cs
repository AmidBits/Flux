namespace Flux.Model.Gaming
{
  public class GameObject
  {
    public System.Collections.Generic.List<GameObject> Children { get; } = new System.Collections.Generic.List<GameObject>();

    public System.Guid ID { get; protected set; } = System.Guid.NewGuid();

    public string Name { get; protected set; }

    public System.Collections.Generic.Dictionary<string, object> Properties { get; } = new System.Collections.Generic.Dictionary<string, object>();

    public Dynamics.RigidBody Dynamics { get; set; } = new Dynamics.RigidBody();

    public GameObject(string name)
    {
      Name = name;
    }

    /// <summary>This is the object delta updater.</summary>
    public virtual void Update(float deltaTime)
    {
      if (!UpdateDisabled)
      {
        Dynamics.Integrate(deltaTime);
      }
    }

    /// <summary>Disables updating, if set to true.</summary>
    public bool UpdateDisabled { get; set; }
  }
}
