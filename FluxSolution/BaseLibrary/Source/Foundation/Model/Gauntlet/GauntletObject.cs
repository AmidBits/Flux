namespace Flux.Model.Gauntlet
{
	public class GameObject
	{
		public string Name { get; }

		public GameObject(string name)
		{
			Name = name;
		}

		public bool UpdateDisabled { get; set; }
	}

	public class GauntletObject : GameObject
	{
		public GauntletObject(string name)
			: base(name)
		{
		}

		public virtual void Update(float deltaTime)
		{
		}
	}
}
