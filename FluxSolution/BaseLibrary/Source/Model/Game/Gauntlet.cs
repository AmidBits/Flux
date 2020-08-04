namespace Flux.Model.Gaming
{
  public class Gauntlet
  {
    public class Object : Gaming.GameObject
    {
      public Object(string name)
        : base(name)
      {

      }
    }

    /// <summary>This is an adventurer that a player controls.</summary>
    public class Adventurer : Creature
    {
      public Adventurer(string name)
        : base(name)
      {
      }
    }

    public class Creature : Object
    {
      public Creature(string name)
        : base(name)
      {
      }
    }

    /// <summary>This is a hive where monsters spawn from.</summary>
    public class Hive : Object
    {
      public int MaxSize { get; set; }

      public System.Collections.Generic.List<Creature> Population { get; private set; }

      public System.TimeSpan SpawnInterval { get; set; }
      public float SpawnTriggerRadius { get; set; }

      public int Type { get; set; }

      public Hive()
        : base(nameof(Hive))
      {
        MaxSize = 25;

        Population = new System.Collections.Generic.List<Creature>();

        SpawnInterval = new System.TimeSpan(0, 0, 5);
        SpawnTriggerRadius = 10;

        Type = 1000;
      }
    }

    /// <summary>This is the hive where monsters spawn from.</summary>
    public class Level : Object
    {
      public System.Collections.Generic.List<Adventurer> Adventurers { get; private set; }
      public System.Collections.Generic.List<Hive> Hives { get; private set; }
      public System.Collections.Generic.List<Portal> Portals { get; private set; }

      public Level()
        : base(nameof(Level))
      {
        Adventurers = new System.Collections.Generic.List<Adventurer>();
        Hives = new System.Collections.Generic.List<Hive>();
        Portals = new System.Collections.Generic.List<Portal>();
      }

      public override void Update(float deltaTime)
      {
        foreach (var adventurer in Adventurers)
        {
          if (!adventurer.UpdateDisabled)
          {
            adventurer.Update(deltaTime);
          }
        }

        foreach (var hive in Hives)
        {
          if (!hive.UpdateDisabled)
          {
            hive.Update(deltaTime);
          }
        }

        foreach (var portal in Portals)
        {
          if (!portal.UpdateDisabled)
          {
            portal.Update(deltaTime);
          }
        }
      }
    }

    /// <summary>This is the monster that players fight.</summary>
    public class Monster : Creature
    {
      public Monster(string name)
        : base(name)
      {
      }
    }

    /// <summary>This is the portal that players can send their adventurers through.</summary>
    public class Portal : Object
    {
      public bool IsOpen { get; set; }

      public Portal(string name)
        : base(name)
      {
      }
    }
  }
}
