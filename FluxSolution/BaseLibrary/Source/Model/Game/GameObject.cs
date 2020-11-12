namespace Flux.Model.Gaming
{
	public interface IGameObject
	{
		public string Name { get; }
	}

	public interface IParentObject
	{
		System.Collections.Generic.List<IGameObject> Children { get; }
	}

	public interface IObjectStartable
	{
		void StartObject();
	}

	public interface IObjectUpdatable
	{
		void UpdateObject(float deltaTime);
	}

	public interface IObjectTransform
	{
		System.Numerics.Vector3 ObjectPosition { get; set; }
		System.Numerics.Quaternion ObjectRotation { get; set; }
		System.Numerics.Vector3 ObjectScale { get; set; }
	}
}
