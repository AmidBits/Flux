using System.Linq;

namespace Flux.Model
{
  public enum Attributes
  {
    Armor,
    Attack,
    Charisma,
    Constitution,
    Defense,
    Dexterity,
    Health,
    HitPoints,
    Intelligence,
    Luck,
    Magic,
    Perception,
    Speed,
    Strength,
    Willpower,
    Wisdom
  }

  [System.Serializable]
  [System.Runtime.Serialization.DataContract]
  public class Container : System.Collections.Generic.List<Item>
  {
    public System.Guid ID { get; set; }
    public string Name { get; set; }
  }

  [System.Serializable]
  public class Item : Dynamics.RigidBody
  {
    public System.Guid Guid { get; set; }
    public string Name { get; set; }

    public System.Collections.Generic.List<Container> Containers { get; set; }

    public System.Collections.Generic.Dictionary<Attributes, double> Attributes = new System.Collections.Generic.Dictionary<Model.Attributes, double>();

    public System.Collections.Generic.List<Item> Items = new System.Collections.Generic.List<Item>();

    //public void AddItem(string path, Item item)
    //{
    //  var leaf = this;

    //  if (leaf.Items is null)
    //  {
    //    leaf.Items = new System.Collections.Generic.List<Item>();
    //  }

    //  foreach (var name in path.Split('/'))
    //  {
    //    var sub = int.TryParse(name, out var index) ? leaf.Items[index] : leaf.Items.FirstOrDefault(t => t.Name == name);

    //    if (sub is null)
    //    {
    //      sub = new Item() { Name = name };

    //      leaf.Items.Add(sub);
    //    }

    //    if (sub.Items is null)
    //    {
    //      sub.Items = new System.Collections.Generic.List<Item>();
    //    }

    //    leaf = sub;
    //  }

    //  leaf.Items.Add(item);
    //}

    public Item AddItem(string path, Item item)
    {
      Item parent = this, child = null;

      foreach (var split in path.Split('/'))
      {
        if (parent.Items is null)
        {
          parent.Items = new System.Collections.Generic.List<Item>();
        }

        child = int.TryParse(split, out var index) ? parent.Items[index] : parent.Items.FirstOrDefault(t => t.Name == split);

        if (child is null)
        {
          child = new Item() { Name = split };

          parent.Items.Add(child);
        }

        parent = child;
      }

      parent.Items.Add(item);

      return parent;
    }

    public Item ChildItem(string nameOrID)
    {
      if (this.Items != null && this.Items.Count > 0)
      {
        if (int.TryParse(nameOrID, out var index) && index >= 0 && index < this.Items.Count)
        {
          return this;
        }
        else if (this.Items.Any(t => t.Name == nameOrID || t.Guid.ToString() == nameOrID))
        {
          return this;
        }
      }

      return null;
    }

    public Item FindItem(string nameOrID)
    {
      if (this.Items != null && this.Items.Count > 0)
      {
        foreach (var item in this.Items)
        {
          if (item.Name == nameOrID || item.Guid.ToString() == nameOrID)
          {
            return item;
          }
          else if(item.FindItem(nameOrID) is Item subItem && subItem!=null)
          {
            return item;
          }
        }
      }

      return null;
    }

    public Item RemoveItem(string path, string nameOrID)
    {
      Item child = this, current = null;

      foreach (var split in path.Split('/'))
      {
        current = child;

        child = current.ChildItem(nameOrID);

        if (child == null)
        {
          return null;
        }
      }

      current.Items.Remove(child);

      return child;
    }
  }
}