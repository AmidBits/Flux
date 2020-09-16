namespace Flux.Model.Gaming
{
  public interface IGameUpdater
  {
    public void UpdateGame(float deltaTime);
  }

  public class GameObject
    : IGameUpdater
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
    public virtual void UpdateGame(float deltaTime)
    {
      if (UpdateDisabled) return;

      Dynamics.Integrate(deltaTime);
    }

    /// <summary>Disables updating, if set to true.</summary>
    public bool UpdateDisabled { get; set; }
  }
}
